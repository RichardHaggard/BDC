// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterValueEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class DataFilterValueEditorElement : DataFilterEditorElement
  {
    public DataFilterValueEditorElement(BaseDataFilterNodeElement dataFilterNodeElement)
      : base(dataFilterNodeElement)
    {
    }

    public override void Synchronize(DataFilterCriteriaNode criteriaNode)
    {
      base.Synchronize(criteriaNode);
      this.Text = criteriaNode.GetFormattedValue();
      RadDataFilterElement treeViewElement = criteriaNode.TreeViewElement as RadDataFilterElement;
      if (treeViewElement == null)
        return;
      this.EditorType = treeViewElement.GetEditorType(criteriaNode.ValueType);
    }
  }
}
