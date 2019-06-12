// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Processor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class Processor : Device
  {
    private readonly string[] _wmiProperties = new string[4]{ "UniqueId", "ProcessorId", "Name", "Manufacturer" };
    private const string WmiClass = "Win32_Processor";

    public Processor()
      : base("Win32_Processor")
    {
    }

    public override string[] GetWmiProperties()
    {
      return this._wmiProperties;
    }

    public static string GetId()
    {
      return Device.GetId(typeof (Processor));
    }
  }
}
