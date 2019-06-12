// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnChooserVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ColumnChooserVisualElement : GridVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrop = true;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      GridViewColumn dataContext = dragObject.GetDataContext() as GridViewColumn;
      if (!dataContext.AllowHide)
        return;
      GridViewGroupColumn gridViewGroupColumn = dataContext as GridViewGroupColumn;
      if (gridViewGroupColumn != null && !gridViewGroupColumn.Group.AllowHide)
        return;
      dataContext.IsVisible = false;
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      GridViewColumn dataContext = dragObject.GetDataContext() as GridViewColumn;
      if (dataContext != null && dataContext.OwnerTemplate != null && !(dataContext.OwnerTemplate.ViewDefinition is HtmlViewDefinition))
        return dataContext.IsVisible;
      return false;
    }
  }
}
