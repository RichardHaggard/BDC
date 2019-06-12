// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterFieldEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class DataFilterFieldEditorElement : DataFilterEditorElement
  {
    public DataFilterFieldEditorElement(BaseDataFilterNodeElement dataFilterNodeElement)
      : base(dataFilterNodeElement)
    {
    }

    public override void Synchronize(DataFilterCriteriaNode criteriaNode)
    {
      base.Synchronize(criteriaNode);
      if (criteriaNode == null)
        return;
      string str = criteriaNode.Descriptor.PropertyName;
      if (TelerikHelper.StringIsNullOrWhiteSpace(str))
        str = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("FieldNullText");
      PropertyDisplayNameNeededEventArgs e = new PropertyDisplayNameNeededEventArgs(criteriaNode.Descriptor.PropertyName, str);
      this.DataFilterNodeElement.DataFilterElement.OnPropertyDisplayNameNeeded((object) this, e);
      this.Text = e.DisplayName;
    }
  }
}
