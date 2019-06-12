// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCheckboxCriteriaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class DataFilterCheckboxCriteriaElement : DataFilterCriteriaElement
  {
    protected override DataFilterValueEditorElement CreateValueElement()
    {
      return (DataFilterValueEditorElement) new DataFilterCheckBoxValueEditorElement((BaseDataFilterNodeElement) this);
    }

    public override bool IsCompatible(RadTreeNode data, object context)
    {
      if (!base.IsCompatible(data, context))
        return false;
      DataFilterCriteriaNode filterCriteriaNode = data as DataFilterCriteriaNode;
      return filterCriteriaNode != null && (object) filterCriteriaNode.ValueType == (object) typeof (bool);
    }
  }
}
