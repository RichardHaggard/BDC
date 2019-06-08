// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Bios
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class Bios : Device
  {
    private readonly string[] _wmiProperties = new string[3]{ "Manufacturer", "SerialNumber", "Name" };
    private const string WmiClass = "Win32_BIOS";

    public Bios()
      : base("Win32_BIOS")
    {
    }

    public override string[] GetWmiProperties()
    {
      return this._wmiProperties;
    }

    public static string GetId()
    {
      return Device.GetId(typeof (Bios));
    }
  }
}
