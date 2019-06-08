// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupExpanderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class GridGroupExpanderCellElement : GridVirtualizedCellElement
  {
    public static RadProperty ExpandedProperty = RadProperty.Register(nameof (Expanded), typeof (bool), typeof (GridGroupExpanderCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private GridExpanderItem expander;
    private bool suspendExpanderPropertyChanged;

    static GridGroupExpanderCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridGroupExpanderCellElementStateManager(), typeof (GridGroupExpanderCellElement));
    }

    public GridGroupExpanderCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "GroupExpanderCell";
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      GridViewIndentColumn viewIndentColumn = data as GridViewIndentColumn;
      return viewIndentColumn != null && viewIndentColumn.IndentLevel == -1;
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      if (this.RowInfo is GridViewGroupRowInfo)
        this.expander.ThemeRole = "GroupExpander";
      else
        this.expander.ThemeRole = "HierarchyExpander";
      this.expander.Expanded = row.Data.IsExpanded;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.expander = new GridExpanderItem();
      this.expander.StretchHorizontally = true;
      this.expander.RadPropertyChanged += new RadPropertyChangedEventHandler(this.expander_RadPropertyChanged);
      this.Children.Add((RadElement) this.expander);
    }

    public override void UpdateInfo()
    {
      base.UpdateInfo();
      if (this.ViewTemplate == null || !this.ViewTemplate.IsSelfReference)
        return;
      if ((this.RowInfo.ChildRows.Count > 0 && this.RowInfo.ChildRows[0].ViewTemplate == this.ViewTemplate || this.ViewTemplate.IsSelfReference) && !(this.RowInfo is GridViewGroupRowInfo))
        this.expander.Visibility = ElementVisibility.Collapsed;
      else
        this.expander.Visibility = ElementVisibility.Visible;
    }

    [Category("Appearance")]
    public virtual GridExpanderItem Expander
    {
      get
      {
        return this.expander;
      }
    }

    [Category("Appearance")]
    public bool Expanded
    {
      get
      {
        return (bool) this.GetValue(GridGroupExpanderCellElement.ExpandedProperty);
      }
    }

    protected override void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnRowPropertyChanged(e);
      if (!(e.PropertyName == "IsExpanded"))
        return;
      int num1 = (int) this.SetValue(GridGroupExpanderCellElement.ExpandedProperty, (object) this.RowInfo.IsExpanded);
      this.suspendExpanderPropertyChanged = true;
      int num2 = (int) this.expander.SetValue(ExpanderItem.ExpandedProperty, (object) this.RowInfo.IsExpanded);
      this.suspendExpanderPropertyChanged = false;
    }

    private void expander_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (this.suspendExpanderPropertyChanged || e.Property != ExpanderItem.ExpandedProperty)
        return;
      this.RowInfo.IsExpanded = this.expander.Expanded;
    }
  }
}
