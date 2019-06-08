// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextItemElement : GanttViewBaseItemElement
  {
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (GanttViewTextItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private GanttCellSelfReferenceLayout selfReferenceLayout;
    private GanttViewTextViewElement textView;
    private GanttViewTextViewColumnContainer cellContainer;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.DrawFill = true;
      this.DrawText = true;
      this.BackColor = Color.White;
      this.ForeColor = Color.Black;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.cellContainer = this.CreateColumnContainer();
      this.cellContainer.StretchHorizontally = true;
      this.cellContainer.StretchVertically = true;
      this.cellContainer.Orientation = Orientation.Horizontal;
      this.cellContainer.ElementProvider = (IVirtualizedElementProvider<GanttViewTextViewColumn>) this.CreateElementProvider();
      this.Children.Add((RadElement) this.cellContainer);
    }

    public GanttViewTextItemElement(GanttViewTextViewElement textView)
    {
      this.textView = textView;
    }

    public virtual GanttViewTextViewColumnContainer CreateColumnContainer()
    {
      return new GanttViewTextViewColumnContainer();
    }

    public virtual GanttViewTextViewCellElementProvider CreateElementProvider()
    {
      return new GanttViewTextViewCellElementProvider(this);
    }

    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(GanttViewTextItemElement.IsExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewTextItemElement.IsExpandedProperty, (object) value);
      }
    }

    public GanttViewTextViewColumnContainer CellContainer
    {
      get
      {
        return this.cellContainer;
      }
    }

    public virtual GanttCellSelfReferenceLayout SelfReferenceLayout
    {
      get
      {
        if (this.selfReferenceLayout == null)
          this.selfReferenceLayout = new GanttCellSelfReferenceLayout(this);
        return this.selfReferenceLayout;
      }
    }

    public GanttViewTextViewElement TextView
    {
      get
      {
        return this.textView;
      }
    }

    public override void Attach(GanttViewDataItem data, object context)
    {
      base.Attach(data, context);
      this.cellContainer.Owner = data.GanttViewElement.TextViewElement;
      this.cellContainer.DataProvider = (IEnumerable) this.cellContainer.Owner.ColumnScroller;
      this.cellContainer.Owner.ColumnScroller.ScrollerUpdated += new EventHandler(this.ColumnScroller_ScrollerUpdated);
    }

    public override void Detach()
    {
      this.DisposeSelfReferenceLayout();
      this.cellContainer.Owner.ColumnScroller.ScrollerUpdated -= new EventHandler(this.ColumnScroller_ScrollerUpdated);
      base.Detach();
    }

    public override void Synchronize()
    {
      base.Synchronize();
      foreach (GanttViewTextViewCellElement child in this.cellContainer.Children)
        child.Synchronize();
      this.UpdateInfo();
      this.TextView.GanttViewElement.OnTextViewItemFormatting(new GanttViewTextViewItemFormattingEventArgs(this.Data, this));
    }

    private void DisposeSelfReferenceLayout()
    {
      if (this.selfReferenceLayout == null)
        return;
      this.selfReferenceLayout.Dispose();
      this.selfReferenceLayout = (GanttCellSelfReferenceLayout) null;
    }

    protected virtual void SynchronizeProperties()
    {
      foreach (GanttViewTextViewCellElement child in this.cellContainer.Children)
        child.Synchronize();
    }

    public virtual void UpdateInfo()
    {
      foreach (GanttViewTextViewCellElement child in this.cellContainer.Children)
        child.UpdateInfo();
    }

    public GanttViewTextViewCellElement GetCellElement(
      GanttViewTextViewColumn column)
    {
      foreach (GanttViewTextViewCellElement child in this.CellContainer.Children)
      {
        if (child.Data == column)
          return child;
      }
      return (GanttViewTextViewCellElement) null;
    }

    private void ColumnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.cellContainer.InvalidateMeasure();
    }
  }
}
