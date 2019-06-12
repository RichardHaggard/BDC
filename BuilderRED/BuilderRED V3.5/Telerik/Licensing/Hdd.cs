// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Hdd
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.Licensing
{
  internal class Hdd : Device
  {
    private readonly string[] _wmiProperties = new string[3]{ "SerialNumber", "Model", "InterfaceType" };
    private readonly List<string> _forbiddenInterafces = new List<string>((IEnumerable<string>) new string[1]{ "USB" });
    private const string WmiClass = "Win32_DiskDrive";
    private const string InterfaceKey = "InterfaceType";

    public Hdd()
      : base("Win32_DiskDrive")
    {
    }

    protected List<string> InterfaceBlacklist
    {
      get
      {
        return this._forbiddenInterafces;
      }
    }

    public override string[] GetWmiProperties()
    {
      return this._wmiProperties;
    }

    protected override bool ValidateProperties(IDictionary<string, string> dict)
    {
      if (dict.ContainsKey("InterfaceType"))
        return this.IsInterfaceValid(dict["InterfaceType"]);
      return false;
    }

    private bool IsInterfaceValid(string interfaceName)
    {
      return !this.InterfaceBlacklist.Contains(interfaceName);
    }

    public static string GetId()
    {
      return Device.GetId(typeof (Hdd));
    }
  }
}
