// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SelfReferenceRelation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class SelfReferenceRelation : ObjectRelation
  {
    private string parentName;
    private string childName;

    internal SelfReferenceRelation(object list, string parentName, string childName)
      : base(list)
    {
      this.parentName = parentName;
      this.childName = childName;
    }

    public override string[] ParentRelationNames
    {
      get
      {
        return new string[1]{ this.parentName };
      }
    }

    public override string[] ChildRelationNames
    {
      get
      {
        return new string[1]{ this.childName };
      }
    }

    protected override void Initialize()
    {
    }
  }
}
