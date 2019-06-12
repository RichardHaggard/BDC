// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnChooserControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class ColumnChooserControl : RadControl
  {
    private ColumnChooserElement element;

    public ColumnChooserControl(RadGridViewElement rootElement)
    {
      if (this.element == null)
        return;
      this.element.Initialize(rootElement, (GridViewInfo) null);
    }

    public ColumnChooserControl()
      : this((RadGridViewElement) null)
    {
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.element = new ColumnChooserElement();
      this.element.StretchVertically = true;
      this.element.StretchHorizontally = true;
      parent.Children.Add((RadElement) this.element);
    }

    public GridViewTemplate ViewTemplate
    {
      set
      {
        if (value == null || this.element == null)
          return;
        this.element.ViewTemplate = value;
      }
      get
      {
        if (this.element != null)
          return this.element.ViewTemplate;
        return (GridViewTemplate) null;
      }
    }

    public IList<GridViewColumn> Columns
    {
      get
      {
        return this.element.Columns;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadSortOrder SortOrder
    {
      get
      {
        return this.element.SortOrder;
      }
      set
      {
        this.element.SortOrder = value;
      }
    }

    public bool EnableFiltering
    {
      get
      {
        return this.ColumnChooserElement.EnableFiltering;
      }
      set
      {
        this.ColumnChooserElement.EnableFiltering = value;
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        if (this.element != null)
          return this.element.GridViewElement;
        return (RadGridViewElement) null;
      }
      set
      {
        if (this.element == null)
          return;
        this.element.GridViewElement = value;
      }
    }

    public ColumnChooserElement ColumnChooserElement
    {
      get
      {
        return this.element;
      }
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element is RadScrollViewer)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    public RadTextBoxElement FilterTextBox
    {
      get
      {
        return this.ColumnChooserElement.FilterTextBox;
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      if (this.element == null)
        return;
      this.element.UpdateView();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      RadScrollLayoutPanel scrollLayoutPanel = this.ColumnChooserElement.ScrollViewer.ScrollLayoutPanel;
      RadScrollBarElement verticalScrollBar = scrollLayoutPanel.VerticalScrollBar;
      if (verticalScrollBar.Visibility == ElementVisibility.Visible)
      {
        int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
        int num2 = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
        int ypos = verticalScrollBar.Value - num2 * verticalScrollBar.SmallChange;
        if (ypos > verticalScrollBar.Maximum - verticalScrollBar.LargeChange + 1)
          ypos = verticalScrollBar.Maximum - verticalScrollBar.LargeChange + 1;
        if (ypos < verticalScrollBar.Minimum)
          ypos = 0;
        else if (ypos > verticalScrollBar.Maximum)
          ypos = verticalScrollBar.Maximum;
        if (ypos != verticalScrollBar.Value)
        {
          scrollLayoutPanel.ScrollTo(0, ypos);
          HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
          if (handledMouseEventArgs != null)
            handledMouseEventArgs.Handled = true;
        }
      }
      base.OnMouseWheel(e);
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
      this.ColumnChooserElement.InvalidateMeasure(true);
      this.ColumnChooserElement.UpdateLayout();
    }

    public event ColumnChooserItemElementCreatingEventHandler ItemElementCreating
    {
      add
      {
        this.ColumnChooserElement.ItemElementCreating += value;
      }
      remove
      {
        this.ColumnChooserElement.ItemElementCreating -= value;
      }
    }
  }
}
