// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.UnknownExtraField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class UnknownExtraField : ExtraFieldBase
  {
    private ushort headerId;

    public UnknownExtraField(ushort fieldHeaderId)
    {
      this.headerId = fieldHeaderId;
    }

    protected override ushort HeaderId
    {
      get
      {
        return this.headerId;
      }
    }

    private byte[] ExtraFieldData { get; set; }

    protected override byte[] GetBlock()
    {
      return this.ExtraFieldData;
    }

    protected override void ParseBlock(byte[] extraData)
    {
      this.ExtraFieldData = extraData;
    }
  }
}
