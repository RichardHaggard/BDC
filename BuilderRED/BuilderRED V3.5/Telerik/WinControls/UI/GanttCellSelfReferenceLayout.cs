// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttCellSelfReferenceLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GanttCellSelfReferenceLayout : DisposableObject
  {
    private GanttViewTextItemElement itemElement;
    private GanttViewTextViewItemExpanderItem expander;
    private GanttCellSelfReferenceStackElement stackLayoutElement;
    private List<GanttIndentCellElement> indents;

    public GanttCellSelfReferenceLayout(GanttViewTextItemElement itemElement)
    {
      this.itemElement = itemElement;
      this.indents = new List<GanttIndentCellElement>();
      this.stackLayoutElement = new GanttCellSelfReferenceStackElement();
      this.stackLayoutElement.StretchVertically = true;
      this.stackLayoutElement.StretchHorizontally = true;
      int num = (int) this.stackLayoutElement.SetDefaultValueOverride(RadElement.FitToSizeModeProperty, (object) RadFitToSizeMode.FitToParentBounds);
      this.expander = new GanttViewTextViewItemExpanderItem(itemElement);
      this.expander.StretchVertically = true;
      this.stackLayoutElement.Children.Add((RadElement) this.expander);
      this.BindRowProperties();
    }

    public virtual void CreateCellElements(GanttViewTextViewCellElement cell)
    {
      if (this.Cell != cell)
      {
        if (this.Cell != null)
          this.Cell.Children.Remove((RadElement) this.stackLayoutElement);
        cell.Children.Insert(0, (RadElement) this.stackLayoutElement);
      }
      this.UpdateExpander();
      this.UpdateLinks();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.DisposeIndetns();
      this.UnbindRowProperties();
      if (this.Cell == null || this.stackLayoutElement.IsDisposed)
        return;
      this.Cell.Children.Remove((RadElement) this.stackLayoutElement);
      this.stackLayoutElement.Dispose();
      this.stackLayoutElement = (GanttCellSelfReferenceStackElement) null;
      this.expander = (GanttViewTextViewItemExpanderItem) null;
    }

    protected void DisposeIndetns()
    {
      for (int index = this.indents.Count - 1; index >= 0; --index)
      {
        GanttIndentCellElement indent = this.indents[index];
        if (!indent.IsDisposed)
        {
          this.stackLayoutElement.Children.Remove((RadElement) indent);
          this.indents.Remove(indent);
          indent.Dispose();
        }
      }
    }

    protected GanttViewDataItem DataItem
    {
      get
      {
        return this.itemElement.Data;
      }
    }

    protected GanttViewTextItemElement ItemElement
    {
      get
      {
        return this.itemElement;
      }
    }

    public GanttViewTextViewItemExpanderItem Expander
    {
      get
      {
        return this.expander;
      }
    }

    public GanttCellSelfReferenceStackElement StackLayoutElement
    {
      get
      {
        return this.stackLayoutElement;
      }
    }

    protected GanttViewTextViewCellElement Cell
    {
      get
      {
        return this.stackLayoutElement.Parent as GanttViewTextViewCellElement;
      }
    }

    protected int Indent
    {
      get
      {
        return this.itemElement.TextView.Indent;
      }
    }

    protected int IndentCount
    {
      get
      {
        return this.indents.Count;
      }
    }

    protected List<GanttIndentCellElement> Indents
    {
      get
      {
        return this.indents;
      }
    }

    protected virtual void UpdateExpander()
    {
      int count = this.DataItem.Items.Count;
      this.expander.Expanded = this.DataItem.Expanded;
      if (count == 0)
        this.expander.Visibility = ElementVisibility.Hidden;
      else
        this.expander.Visibility = ElementVisibility.Visible;
    }

    protected virtual void UpdateLinks()
    {
      int num1 = this.DataItem.Level - this.IndentCount - 1;
      int num2 = Math.Abs(num1);
      bool flag = num1 > 0;
      for (; num2 > 0; --num2)
      {
        if (flag)
        {
          GanttIndentCellElement indentCellElement = new GanttIndentCellElement(this.itemElement);
          indentCellElement.Visibility = ElementVisibility.Hidden;
          indentCellElement.StretchVertically = true;
          int num3 = (int) indentCellElement.SetDefaultValueOverride(RadElement.FitToSizeModeProperty, (object) RadFitToSizeMode.FitToParentBounds);
          this.stackLayoutElement.Children.Insert(0, (RadElement) indentCellElement);
          this.indents.Insert(0, indentCellElement);
        }
        else
        {
          GanttIndentCellElement indent = this.indents[0];
          this.stackLayoutElement.Children.Remove((RadElement) indent);
          this.indents.Remove(indent);
          indent.Dispose();
        }
      }
    }

    public virtual void BindRowProperties()
    {
      PropertyBindingOptions options = PropertyBindingOptions.TwoWay | PropertyBindingOptions.PreserveAsLocalValue;
      int num = (int) this.expander.BindProperty(ExpanderItem.ExpandedProperty, (RadObject) this.ItemElement, GanttViewTextItemElement.IsExpandedProperty, options);
    }

    public virtual void UnbindRowProperties()
    {
      int num = (int) this.expander.UnbindProperty(ExpanderItem.ExpandedProperty);
    }
  }
}
