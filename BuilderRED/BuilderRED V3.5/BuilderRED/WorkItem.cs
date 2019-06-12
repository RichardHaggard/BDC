// Decompiled with JetBrains decompiler
// Type: BuilderRED.WorkItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace BuilderRED
{
  public class WorkItem
  {
    public enum StatusType
    {
      Funded = 1,
      InProgress = 2,
      InDesign = 3,
      Completed = 4,
      Canceled = 5,
      Deferred = 6,
      OnHold = 7,
      AwaitingFunds = 8,
      AwaitingContractAward = 9,
      Budgeted = 10, // 0x0000000A
      Funded_ServiceCall = 11, // 0x0000000B
      Funded_SpecialProject = 12, // 0x0000000C
      AwaitingFunds_SpecialProject = 13, // 0x0000000D
    }
  }
}
