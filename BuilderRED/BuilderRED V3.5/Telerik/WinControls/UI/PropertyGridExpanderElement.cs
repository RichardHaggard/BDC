// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridExpanderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class PropertyGridExpanderElement : PropertyGridContentElement
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (PropertyGridExpanderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (PropertyGridExpanderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsControlInactiveProperty = RadProperty.Register(nameof (IsInactive), typeof (bool), typeof (PropertyGridExpanderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsInEditModeProperty = RadProperty.Register("IsInEditMode", typeof (bool), typeof (PropertyGridExpanderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private ExpanderItem expanderItem;

    static PropertyGridExpanderElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridExpanderElementStateManager(), typeof (PropertyGridExpanderElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = false;
      this.DrawBorder = false;
      this.NotifyParentOnMouseInput = true;
      this.ClipDrawing = true;
      this.Class = "PropertyGridItemExpanderItem";
      this.StretchHorizontally = false;
      this.StretchVertically = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.expanderItem = new ExpanderItem();
      this.expanderItem.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.expanderItem.DrawSignBorder = true;
      this.expanderItem.DrawSignFill = false;
      this.expanderItem.SignSize = (SizeF) new Size(9, 9);
      this.expanderItem.SignPadding = new Padding(1, 1, 1, 1);
      this.expanderItem.SignBorderColor = Color.LightGray;
      this.expanderItem.SignBorderWidth = 1f;
      this.expanderItem.ForeColor = Color.Black;
      this.expanderItem.SignStyle = SignStyles.PlusMinus;
      this.expanderItem.SignBorderColor = Color.Gray;
      this.expanderItem.ShowHorizontalLine = false;
      this.expanderItem.NotifyParentOnMouseInput = true;
      this.expanderItem.DrawFill = false;
      this.expanderItem.DrawBorder = false;
      this.expanderItem.PropertyChanged += new PropertyChangedEventHandler(this.expanderItem_PropertyChanged);
      this.Children.Add((RadElement) this.expanderItem);
    }

    protected override void DisposeManagedResources()
    {
      if (this.expanderItem != null)
        this.expanderItem.PropertyChanged -= new PropertyChangedEventHandler(this.expanderItem_PropertyChanged);
      base.DisposeManagedResources();
    }

    public ExpanderItem ExpanderItem
    {
      get
      {
        return this.expanderItem;
      }
    }

    public bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(PropertyGridExpanderElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridExpanderElement.IsSelectedProperty, (object) value);
      }
    }

    public bool IsExpanded
    {
      get
      {
        return this.expanderItem.Expanded;
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridExpanderElement.IsExpandedProperty, (object) value);
      }
    }

    public bool IsInactive
    {
      get
      {
        return (bool) this.GetValue(PropertyGridExpanderElement.IsControlInactiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridExpanderElement.IsControlInactiveProperty, (object) value);
      }
    }

    public virtual void Synchronize()
    {
      PropertyGridItemBase data = this.VisualItem.Data;
      this.IsExpanded = data.Expanded;
      this.IsSelected = data.Selected;
      if (data.Expandable)
        this.expanderItem.Visibility = ElementVisibility.Visible;
      else
        this.expanderItem.Visibility = ElementVisibility.Hidden;
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      if (visualItem == null)
        return;
      bool flag = visualItem.IsInEditMode;
      if (flag && data.Level == 0 && data.GridItems.Count > 0)
        flag = false;
      int num = (int) this.SetValue(PropertyGridExpanderElement.IsInEditModeProperty, (object) flag);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != PropertyGridExpanderElement.IsExpandedProperty)
        return;
      this.expanderItem.Expanded = (bool) e.NewValue;
    }

    private void expanderItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Expanded"))
        return;
      this.VisualItem.Data.Expanded = this.ExpanderItem.Expanded;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = (SizeF) Size.Empty;
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      PropertyGridTableElement gridTableElement = this.PropertyGridTableElement;
      if (gridTableElement != null && visualItem != null && visualItem.Data is PropertyGridItem)
      {
        empty.Width = (float) gridTableElement.ItemIndent;
        empty.Height = !float.IsPositiveInfinity(availableSize.Height) ? availableSize.Height : (float) gridTableElement.ItemHeight;
      }
      return empty;
    }
  }
}
