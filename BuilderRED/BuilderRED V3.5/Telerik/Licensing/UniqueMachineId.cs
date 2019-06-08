// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.UniqueMachineId
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  public class UniqueMachineId
  {
    private const string DefaultId = "ZGVmYXVsdF9pZA==";
    private readonly IHashingService _hashService;

    public UniqueMachineId(IHashingService service)
    {
      this._hashService = service;
      this.Id = this.ReadKey();
    }

    public string Id { get; private set; }

    public static string GetIdWithDefaultHash()
    {
      return new UniqueMachineId(HashService.GetInstance()).Id;
    }

    public override string ToString()
    {
      return this.Id;
    }

    private string ReadKey()
    {
      try
      {
        return this._hashService.Sha256(Bios.GetId() + Hdd.GetId() + Processor.GetId());
      }
      catch
      {
        return "ZGVmYXVsdF9pZA==";
      }
    }
  }
}
