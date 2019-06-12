// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Serialization.RadArchivePackage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Telerik.WinControls.Serialization
{
  [XmlInclude(typeof (RadThemePackage))]
  [Serializable]
  public class RadArchivePackage
  {
    private List<RadArchiveStream> streams;
    private PackageFormat format;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [NonSerialized]
    public bool throwException;

    public RadArchivePackage()
      : this((RadArchiveStream[]) null)
    {
    }

    public RadArchivePackage(params RadArchiveStream[] streamInfos)
    {
      this.format = this.DefaultFormat;
      this.streams = new List<RadArchiveStream>();
      if (streamInfos == null)
        return;
      this.streams.AddRange((IEnumerable<RadArchiveStream>) streamInfos);
    }

    protected virtual PackageFormat DefaultFormat
    {
      get
      {
        return PackageFormat.Binary;
      }
    }

    [DefaultValue(PackageFormat.Binary)]
    [XmlAttribute]
    public PackageFormat Format
    {
      get
      {
        return this.format;
      }
      set
      {
        this.format = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeFormat()
    {
      return this.format != this.DefaultFormat;
    }

    public List<RadArchiveStream> Streams
    {
      get
      {
        return this.streams;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeStreams()
    {
      return this.streams.Count > 0;
    }

    public Stream Compress()
    {
      if (this.streams.Count == 0)
        return (Stream) null;
      int byteCount = this.PrepareStreams();
      if (byteCount == 0)
        return (Stream) null;
      return this.SaveToStreamCore(byteCount);
    }

    public bool Compress(string fileName)
    {
      Stream stream = this.Compress();
      if (stream == null)
        return false;
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite);
        byte[] buffer = new byte[stream.Length];
        stream.Position = 0L;
        stream.Read(buffer, 0, buffer.Length);
        fileStream.Write(buffer, 0, buffer.Length);
        return true;
      }
      catch (Exception ex)
      {
        if (this.throwException)
          throw ex;
        return false;
      }
      finally
      {
        stream.Close();
        fileStream?.Close();
      }
    }

    protected virtual Stream SaveToStreamCore(int byteCount)
    {
      MemoryStream memoryStream = new MemoryStream(byteCount);
      try
      {
        if (this.format == PackageFormat.Binary)
          new BinaryFormatter().Serialize((Stream) memoryStream, (object) this);
        else
          new XmlSerializer(this.GetType()).Serialize((Stream) memoryStream, (object) this);
        return (Stream) memoryStream;
      }
      catch (Exception ex)
      {
        memoryStream?.Close();
        return (Stream) null;
      }
    }

    private int PrepareStreams()
    {
      int num = 0;
      foreach (RadArchiveStream stream in this.streams)
      {
        stream.Zip();
        num += stream.ByteCount;
      }
      return num;
    }

    public static RadArchivePackage Decompress(Stream stream)
    {
      return RadArchivePackage.DecompressBinary(stream);
    }

    public static RadArchivePackage Decompress(string fileName)
    {
      return RadArchivePackage.DecompressFile(fileName, (Type) null);
    }

    public static RadArchivePackage Decompress(Stream stream, Type type)
    {
      return RadArchivePackage.DecompressXML(stream, type);
    }

    public static RadArchivePackage Decompress(string fileName, Type type)
    {
      return RadArchivePackage.DecompressFile(fileName, type);
    }

    private static RadArchivePackage DecompressFile(string fileName, Type type)
    {
      if (!File.Exists(fileName))
        return (RadArchivePackage) null;
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);
        if ((object) type != null)
          return RadArchivePackage.Decompress((Stream) fileStream, type);
        return RadArchivePackage.Decompress((Stream) fileStream);
      }
      catch (Exception ex)
      {
        return (RadArchivePackage) null;
      }
      finally
      {
        fileStream?.Close();
      }
    }

    private static RadArchivePackage DecompressXML(Stream stream, Type type)
    {
      try
      {
        return new XmlSerializer(type).Deserialize(stream) as RadArchivePackage;
      }
      catch
      {
        return (RadArchivePackage) null;
      }
    }

    private static RadArchivePackage DecompressBinary(Stream stream)
    {
      try
      {
        return new BinaryFormatter().Deserialize(stream) as RadArchivePackage;
      }
      catch
      {
        return (RadArchivePackage) null;
      }
    }
  }
}
