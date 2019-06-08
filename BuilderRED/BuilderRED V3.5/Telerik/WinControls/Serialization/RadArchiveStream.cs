// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Serialization.RadArchiveStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace Telerik.WinControls.Serialization
{
  [XmlInclude(typeof (RadXmlThemeArchiveStream))]
  [Serializable]
  public abstract class RadArchiveStream
  {
    private string name;
    private byte[] zippedBytes;
    [NonSerialized]
    private byte[] rawBytes;
    [NonSerialized]
    private object context;

    public RadArchiveStream()
    {
    }

    public RadArchiveStream(object context)
    {
      this.context = context;
    }

    public void Zip()
    {
      if (this.context == null || this.zippedBytes != null)
        return;
      if (this.context == null)
        return;
      try
      {
        this.zippedBytes = this.GetZippedBytesCore();
      }
      catch (Exception ex)
      {
      }
    }

    protected virtual byte[] GetZippedBytesCore()
    {
      this.rawBytes = this.GetRawBytes();
      if (this.rawBytes == null)
        return (byte[]) null;
      MemoryStream memoryStream = new MemoryStream();
      DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Compress, true);
      deflateStream.Write(this.rawBytes, 0, this.rawBytes.Length);
      deflateStream.Close();
      byte[] array = memoryStream.ToArray();
      memoryStream.Close();
      return array;
    }

    protected virtual byte[] GetRawBytes()
    {
      return (byte[]) null;
    }

    public object Unzip()
    {
      if (this.context != null)
        return this.context;
      if (this.zippedBytes != null)
      {
        if (this.zippedBytes.Length != 0)
        {
          try
          {
            this.rawBytes = this.Unzip(this.zippedBytes);
            this.context = this.Deserialize(this.rawBytes);
            return this.context;
          }
          catch (Exception ex)
          {
            return (object) null;
          }
        }
      }
      return (object) null;
    }

    protected virtual object Deserialize(byte[] rawBytes)
    {
      return (object) null;
    }

    protected virtual byte[] Unzip(byte[] zippedBytes)
    {
      MemoryStream memoryStream1 = new MemoryStream(zippedBytes);
      DeflateStream deflateStream = new DeflateStream((Stream) memoryStream1, CompressionMode.Decompress, true);
      MemoryStream memoryStream2 = new MemoryStream();
      byte[] buffer = new byte[4096];
      int count;
      do
      {
        count = deflateStream.Read(buffer, 0, 4096);
        if (count > 0)
          memoryStream2.Write(buffer, 0, count);
      }
      while (count == 4096);
      deflateStream.Close();
      byte[] array = memoryStream2.ToArray();
      memoryStream2.Close();
      memoryStream1.Close();
      return array;
    }

    [XmlIgnore]
    public object Context
    {
      get
      {
        return this.context;
      }
      set
      {
        if (this.context == value)
          return;
        this.context = value;
        this.rawBytes = (byte[]) null;
        this.zippedBytes = (byte[]) null;
      }
    }

    [XmlIgnore]
    public byte[] RawBytes
    {
      get
      {
        return this.rawBytes;
      }
    }

    public byte[] ZippedBytes
    {
      get
      {
        return this.zippedBytes;
      }
      set
      {
        this.zippedBytes = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeZippedBytes()
    {
      if (this.zippedBytes != null)
        return this.zippedBytes.Length > 0;
      return false;
    }

    public int ByteCount
    {
      get
      {
        if (this.rawBytes != null)
          return this.rawBytes.Length;
        if (this.zippedBytes != null)
          return this.zippedBytes.Length;
        return 0;
      }
    }

    [XmlAttribute]
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeName()
    {
      return !string.IsNullOrEmpty(this.name);
    }

    public abstract StreamFormat Format { get; }
  }
}
