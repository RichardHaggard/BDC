// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckedDropDownListElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class CheckedDropDownListElementProvider : ListElementProvider
  {
    public CheckedDropDownListElementProvider(RadListElement listElement)
      : base(listElement)
    {
    }

    public override IVirtualizedElement<RadListDataItem> CreateElement(
      RadListDataItem data,
      object context)
    {
      return (IVirtualizedElement<RadListDataItem>) (this.listElement.OnCreatingVisualListItem(!string.IsNullOrEmpty(this.listElement.DescriptionTextMember) || data is DescriptionTextCheckedListDataItem ? (RadListVisualItem) new DescriptionTextCheckedListVisualItem() : (RadListVisualItem) new RadCheckedListVisualItem()) ?? (!string.IsNullOrEmpty(this.listElement.DescriptionTextMember) || data is DescriptionTextCheckedListDataItem ? (RadListVisualItem) new DescriptionTextCheckedListVisualItem() : (RadListVisualItem) new RadCheckedListVisualItem()));
    }
  }
}
