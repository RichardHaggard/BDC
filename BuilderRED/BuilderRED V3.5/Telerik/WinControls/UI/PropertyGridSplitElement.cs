// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridSplitElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridSplitElement : LightVisualElement
  {
    private bool isResizing;
    private float cachedHelpElementHeight;
    private bool isHelpHidden;
    private PropertyGridTableElement propertyTableElement;
    private PropertyGridSizeGripElement sizeGripElement;
    private PropertyGridHelpElement helpElement;

    public PropertyGridSplitElement()
    {
      this.propertyTableElement.ContextMenu = (RadContextMenu) new PropertyGridDefaultContextMenu(this.propertyTableElement);
      this.propertyTableElement.SelectedGridItemChanged += new RadPropertyGridEventHandler(this.propertyGridTableElement_SelectedItemChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.propertyTableElement.SelectedGridItemChanged -= new RadPropertyGridEventHandler(this.propertyGridTableElement_SelectedItemChanged);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.isHelpHidden = false;
      this.isResizing = false;
      this.cachedHelpElementHeight = -1f;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.propertyTableElement = this.CreateTableElement();
      this.sizeGripElement = this.CreateSizeGripElement();
      this.helpElement = this.CreateHelpElement();
      this.Children.Add((RadElement) this.propertyTableElement);
      this.Children.Add((RadElement) this.sizeGripElement);
      this.Children.Add((RadElement) this.helpElement);
    }

    protected virtual PropertyGridTableElement CreateTableElement()
    {
      return new PropertyGridTableElement();
    }

    protected virtual PropertyGridSizeGripElement CreateSizeGripElement()
    {
      return new PropertyGridSizeGripElement();
    }

    protected virtual PropertyGridHelpElement CreateHelpElement()
    {
      return new PropertyGridHelpElement();
    }

    public PropertyGridTableElement PropertyTableElement
    {
      get
      {
        return this.propertyTableElement;
      }
    }

    public PropertyGridSizeGripElement SizeGripElement
    {
      get
      {
        return this.sizeGripElement;
      }
    }

    public PropertyGridHelpElement HelpElement
    {
      get
      {
        return this.helpElement;
      }
    }

    public float HelpElementHeight
    {
      get
      {
        return this.helpElement.HelpElementHeight;
      }
      set
      {
        this.helpElement.HelpElementHeight = value;
        this.InvalidateMeasure(true);
      }
    }

    public bool HelpVisible
    {
      get
      {
        return this.helpElement.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (value)
        {
          this.helpElement.Visibility = ElementVisibility.Visible;
          this.sizeGripElement.Visibility = ElementVisibility.Visible;
          if ((double) this.cachedHelpElementHeight != -1.0)
            this.helpElement.HelpElementHeight = this.cachedHelpElementHeight;
        }
        else
        {
          this.cachedHelpElementHeight = this.helpElement.HelpElementHeight;
          this.helpElement.Visibility = ElementVisibility.Collapsed;
          this.sizeGripElement.Visibility = ElementVisibility.Collapsed;
        }
        this.InvalidateMeasure();
        this.OnNotifyPropertyChanged(nameof (HelpVisible));
      }
    }

    private bool IsInResizeLocation(Point location)
    {
      if (this.ElementTree == null)
        return false;
      switch (this.ElementTree.GetElementAtPoint(new Point(location.X, location.Y), (Predicate<RadElement>) (x => true)))
      {
        case PropertyGridSizeGripElement _:
          return true;
        case null:
          return false;
        default:
          return false;
      }
    }

    public virtual void BeginResize(int offset)
    {
      this.Capture = true;
      this.isResizing = true;
    }

    public virtual void ShowHelp()
    {
      this.helpElement.HelpElementHeight = this.cachedHelpElementHeight;
      this.isHelpHidden = false;
      this.InvalidateMeasure(true);
    }

    public virtual void HideHelp()
    {
      this.cachedHelpElementHeight = this.helpElement.HelpElementHeight;
      this.helpElement.HelpElementHeight = 0.0f;
      this.isHelpHidden = true;
      this.InvalidateMeasure(true);
    }

    public virtual void ClearHelpBarText()
    {
      this.HelpElement.HelpTitleElement.Text = string.Empty;
      this.HelpElement.HelpContentElement.Text = string.Empty;
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (!this.IsInResizeLocation(((MouseEventArgs) e).Location))
        return;
      if (this.isHelpHidden)
        this.ShowHelp();
      else
        this.HideHelp();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.IsInResizeLocation(e.Location))
        return;
      this.BeginResize(this.sizeGripElement.ControlBoundingRectangle.Y - e.Y);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.isResizing)
      {
        if (this.isHelpHidden)
          this.isHelpHidden = false;
        float val1 = (float) (this.BoundingRectangle.Bottom - e.Y);
        if (this.HelpElement.MinSize != Size.Empty)
          val1 = Math.Max(val1, (float) this.HelpElement.MinSize.Height);
        if (this.HelpElement.MaxSize != Size.Empty)
          val1 = Math.Min(val1, (float) this.HelpElement.MaxSize.Height);
        if ((double) val1 >= (double) (this.BoundingRectangle.Height - this.propertyTableElement.ItemHeight * 2))
          this.HelpElementHeight = (float) (this.BoundingRectangle.Height - this.propertyTableElement.ItemHeight * 2) / this.DpiScaleFactor.Height;
        else if ((double) val1 > 0.0)
          this.HelpElementHeight = val1 / this.DpiScaleFactor.Height;
        else
          this.HideHelp();
      }
      else if (this.IsInResizeLocation(e.Location))
        this.ElementTree.Control.Cursor = Cursors.HSplit;
      else
        this.ElementTree.Control.Cursor = Cursors.Default;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.isResizing = false;
      this.Capture = false;
    }

    private void propertyGridTableElement_SelectedItemChanged(
      object sender,
      RadPropertyGridEventArgs e)
    {
      if (e.Item != null)
      {
        this.helpElement.TitleText = e.Item.Label;
        this.helpElement.ContentText = e.Item.Description;
      }
      else
      {
        this.helpElement.TitleText = string.Empty;
        this.helpElement.ContentText = string.Empty;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.GetClientRectangle(availableSize);
      SizeF empty = SizeF.Empty;
      this.helpElement.Measure(availableSize);
      availableSize.Height -= this.helpElement.DesiredSize.Height;
      empty.Height += this.helpElement.DesiredSize.Height;
      empty.Width = this.helpElement.DesiredSize.Height;
      this.sizeGripElement.Measure(availableSize);
      availableSize.Height -= this.sizeGripElement.DesiredSize.Height;
      empty.Height += this.sizeGripElement.DesiredSize.Height;
      empty.Width = Math.Max(empty.Width, this.sizeGripElement.DesiredSize.Width);
      this.propertyTableElement.Measure(availableSize);
      empty.Height += this.propertyTableElement.DesiredSize.Height;
      empty.Width = Math.Max(empty.Width, this.propertyTableElement.DesiredSize.Width);
      empty.Width = Math.Min(empty.Width, availableSize.Width);
      empty.Height = Math.Min(empty.Height, availableSize.Height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float y1 = clientRectangle.Y;
      float height = clientRectangle.Height - (this.helpElement.DesiredSize.Height + this.sizeGripElement.DesiredSize.Height);
      this.propertyTableElement.Arrange(new RectangleF(clientRectangle.X, y1, clientRectangle.Width, height));
      float y2 = clientRectangle.Bottom - this.helpElement.DesiredSize.Height - this.sizeGripElement.DesiredSize.Height;
      this.sizeGripElement.Arrange(new RectangleF(clientRectangle.X, y2, clientRectangle.Width, this.sizeGripElement.DesiredSize.Height));
      float y3 = clientRectangle.Bottom - this.helpElement.DesiredSize.Height;
      this.helpElement.Arrange(new RectangleF(clientRectangle.X, y3, clientRectangle.Width, this.helpElement.DesiredSize.Height));
      return finalSize;
    }
  }
}
