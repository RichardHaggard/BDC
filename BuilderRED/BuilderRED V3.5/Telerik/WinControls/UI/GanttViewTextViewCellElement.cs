// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewCellElement : GanttViewVisualElement, IVirtualizedElement<GanttViewTextViewColumn>
  {
    public static RadProperty IsFirstCellProperty = RadProperty.Register(nameof (IsFirstCell), typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsLastCellProperty = RadProperty.Register(nameof (IsLastCell), typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentColumnProperty = RadProperty.Register("CurrentColumn", typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentRowProperty = RadProperty.Register("CurrentRow", typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register("Selected", typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register("HotTracking", typeof (bool), typeof (GanttViewTextViewCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private bool updatingInfo;
    private GanttViewTextViewColumn column;
    private GanttViewTextItemElement owner;
    private IInputEditor editor;
    private RadItem editorElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
    }

    static GanttViewTextViewCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GanttViewTextViewCellElementStateManager(), typeof (GanttViewTextViewCellElement));
    }

    public GanttViewTextViewCellElement(
      GanttViewTextItemElement owner,
      GanttViewTextViewColumn column)
    {
      this.column = column;
      this.owner = owner;
      if (owner == null)
        return;
      int num1 = (int) this.BindProperty(GanttViewTextViewCellElement.SelectedProperty, (RadObject) owner, BaseListViewVisualItem.SelectedProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.BindProperty(GanttViewTextViewCellElement.CurrentRowProperty, (RadObject) owner, GanttViewBaseItemElement.CurrentProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.BindProperty(GanttViewTextViewCellElement.HotTrackingProperty, (RadObject) owner, RadElement.IsMouseOverProperty, PropertyBindingOptions.OneWay);
    }

    protected override void DisposeManagedResources()
    {
      int num1 = (int) this.UnbindProperty(GanttViewTextViewCellElement.SelectedProperty);
      int num2 = (int) this.UnbindProperty(GanttViewTextViewCellElement.CurrentRowProperty);
      int num3 = (int) this.UnbindProperty(GanttViewTextViewCellElement.HotTrackingProperty);
      base.DisposeManagedResources();
    }

    public GanttViewTextViewColumn Data
    {
      get
      {
        return this.column;
      }
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public GanttViewTextItemElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public GanttViewDataItem DataItem
    {
      get
      {
        if (this.owner == null)
          return (GanttViewDataItem) null;
        return this.owner.Data;
      }
    }

    public GanttCellSelfReferenceLayout SelfReferenceLayout
    {
      get
      {
        if (this.owner != null && this.IsFirstCell)
          return this.owner.SelfReferenceLayout;
        return (GanttCellSelfReferenceLayout) null;
      }
    }

    public GanttViewTextViewItemExpanderItem Expander
    {
      get
      {
        if (this.SelfReferenceLayout != null)
          return this.SelfReferenceLayout.Expander;
        return (GanttViewTextViewItemExpanderItem) null;
      }
    }

    public virtual bool IsFirstCell
    {
      get
      {
        return (bool) this.GetValue(GanttViewTextViewCellElement.IsFirstCellProperty);
      }
      private set
      {
        int num = (int) this.SetValue(GanttViewTextViewCellElement.IsFirstCellProperty, (object) value);
      }
    }

    public virtual bool IsLastCell
    {
      get
      {
        return (bool) this.GetValue(GanttViewTextViewCellElement.IsLastCellProperty);
      }
      private set
      {
        int num = (int) this.SetValue(GanttViewTextViewCellElement.IsLastCellProperty, (object) value);
      }
    }

    public virtual void Attach(GanttViewTextViewColumn data, object context)
    {
      if (data == null)
        return;
      this.column = data;
      this.Synchronize();
      this.column.PropertyChanged += new PropertyChangedEventHandler(this.column_PropertyChanged);
    }

    public virtual void Detach()
    {
      if (this.column != null)
        this.column.PropertyChanged -= new PropertyChangedEventHandler(this.column_PropertyChanged);
      this.column = (GanttViewTextViewColumn) null;
    }

    public virtual void Synchronize()
    {
      if (this.Data == null || this.DataItem == null)
        return;
      object obj = this.DataItem[this.Data];
      if ((object) this.Data.DataType == (object) typeof (DateTime))
      {
        DateTime dateTime = !(obj is DateTime) ? Convert.ToDateTime(obj) : (DateTime) obj;
        this.Text = string.Format(this.Data.FormatString ?? "{0}", (object) dateTime);
      }
      else if (TelerikHelper.IsNumericType(this.Data.DataType))
      {
        Decimal num = Convert.ToDecimal(obj);
        this.Text = string.Format(this.Data.FormatString ?? "{0}", (object) num);
      }
      else
        this.Text = string.Format(this.Data.FormatString ?? "{0}", obj);
      int num1 = (int) this.SetValue(GanttViewTextViewCellElement.CurrentColumnProperty, (object) this.column.Current);
      this.Owner.TextView.GanttViewElement.OnTextViewCellFormatting(new GanttViewTextViewCellFormattingEventArgs(this.Owner.Data, this, this.Data));
      this.UpdateInfo();
    }

    public virtual bool IsCompatible(GanttViewTextViewColumn data, object context)
    {
      return data != null;
    }

    public virtual void UpdateInfo()
    {
      if (!this.CanUpdateInfo)
        return;
      this.updatingInfo = true;
      this.UpdateCore();
      this.updatingInfo = false;
    }

    protected virtual bool CanUpdateInfo
    {
      get
      {
        if (!this.updatingInfo && this.column != null && this.owner != null)
          return this.owner.Data != null;
        return false;
      }
    }

    protected virtual void UpdateCore()
    {
      this.IsFirstCell = this.Column == this.owner.TextView.FirstVisibleColumn;
      this.IsLastCell = this.Column == this.owner.TextView.LastVisibleColumn;
      this.UpdateSelfReferenceLayout();
    }

    private void column_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronize();
    }

    private void UpdateSelfReferenceLayout()
    {
      if (this.SelfReferenceLayout != null)
      {
        this.SelfReferenceLayout.CreateCellElements(this);
      }
      else
      {
        if (this.IsFirstCell || this.owner == null)
          return;
        GanttCellSelfReferenceLayout selfReferenceLayout = this.owner.SelfReferenceLayout;
        if (selfReferenceLayout == null)
          return;
        GanttCellSelfReferenceStackElement stackLayoutElement = selfReferenceLayout.StackLayoutElement;
        if (stackLayoutElement == null || stackLayoutElement.Parent != this)
          return;
        this.Children.Remove((RadElement) stackLayoutElement);
      }
    }

    protected override SizeF CalculateDesiredSize(
      SizeF availableSize,
      SizeF desiredSize,
      SizeF elementsDesiredSize)
    {
      if (this.SelfReferenceLayout == null)
        return base.CalculateDesiredSize(availableSize, desiredSize, elementsDesiredSize);
      desiredSize.Width += elementsDesiredSize.Width;
      if ((double) elementsDesiredSize.Height > (double) desiredSize.Height)
        desiredSize.Height = elementsDesiredSize.Height;
      desiredSize.Width = Math.Min(desiredSize.Width, availableSize.Width);
      desiredSize.Height = Math.Min(desiredSize.Height, availableSize.Height);
      return desiredSize;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      int num = 0;
      if (this.SelfReferenceLayout != null)
      {
        this.SelfReferenceLayout.StackLayoutElement.Measure(clientRectangle.Size);
        num = (int) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      }
      foreach (RadElement child in this.Children)
      {
        if (this.SelfReferenceLayout == null || this.SelfReferenceLayout.StackLayoutElement != child)
        {
          SizeF availableSize1 = new SizeF(clientRectangle.Width - (float) num, clientRectangle.Height);
          child.Measure(availableSize1);
        }
      }
      return new SizeF((float) this.Data.Width, clientRectangle.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      GanttCellSelfReferenceLayout selfReferenceLayout = this.SelfReferenceLayout;
      if (selfReferenceLayout != null)
      {
        this.ArrangeSelfReferencePanel(finalSize, ref clientRectangle);
        double width = (double) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      }
      this.Layout.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (selfReferenceLayout == null || selfReferenceLayout.StackLayoutElement != child)
        {
          if (this.IsInEditMode && child == this.editorElement)
            child.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height));
          else
            this.ArrangeElement(child, finalSize, clientRectangle);
        }
      }
      return finalSize;
    }

    protected virtual void ArrangeElement(
      RadElement element,
      SizeF finalSize,
      RectangleF clientRect)
    {
      if (element.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        element.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
      else if (element.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
      {
        element.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
      }
      else
      {
        RectangleF finalRect = new RectangleF(clientRect.Left, clientRect.Top, Math.Min(clientRect.Width, element.DesiredSize.Width), Math.Min(clientRect.Height, element.DesiredSize.Height));
        if (element.StretchHorizontally || (double) finalRect.Width == 0.0 && element.Visibility != ElementVisibility.Collapsed)
          finalRect.Width = clientRect.Width;
        if (element.StretchVertically || (double) finalRect.Height == 0.0 && element.Visibility != ElementVisibility.Collapsed)
          finalRect.Height = clientRect.Height;
        element.Arrange(finalRect);
      }
    }

    protected virtual void ArrangeSelfReferencePanel(SizeF finalSize, ref RectangleF clientRect)
    {
      RadElement stackLayoutElement = (RadElement) this.SelfReferenceLayout.StackLayoutElement;
      int width = (int) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      RectangleF rectangleF = clientRect;
      rectangleF.Width = (float) width;
      if (stackLayoutElement.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
      {
        rectangleF.Location = (PointF) (this.RightToLeft ? new Point((int) ((double) clientRect.Right - (double) width), 0) : Point.Empty);
        rectangleF.Height = finalSize.Height;
        stackLayoutElement.Arrange(rectangleF);
      }
      else
      {
        if (this.RightToLeft)
          rectangleF.X = clientRect.Right - (float) width;
        this.ArrangeElement(stackLayoutElement, finalSize, rectangleF);
      }
      if (this.RightToLeft)
      {
        clientRect.Width -= (float) width;
      }
      else
      {
        clientRect.X += (float) width;
        clientRect.Width -= (float) width;
      }
    }

    public bool IsInEditMode
    {
      get
      {
        return this.editor != null;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public virtual void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.editor == editor)
        return;
      this.editor = editor;
      this.editorElement = this.GetEditorElement((IValueEditor) editor);
      if (this.editorElement == null || this.Children.Contains((RadElement) this.editorElement))
        return;
      this.Children.Add((RadElement) this.editorElement);
      this.Synchronize();
      this.UpdateLayout();
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      if (editor == null || this.editor != editor)
        return;
      this.editorElement = this.GetEditorElement((IValueEditor) editor);
      if (this.editorElement != null && this.Children.Contains((RadElement) this.editorElement))
      {
        this.Children.Remove((RadElement) this.editorElement);
        this.editorElement = (RadItem) null;
      }
      this.editor = (IInputEditor) null;
      this.Synchronize();
      this.Invalidate();
    }

    protected RadItem GetEditorElement(IValueEditor editor)
    {
      BaseInputEditor editor1 = this.editor as BaseInputEditor;
      if (editor1 != null)
        return editor1.EditorElement as RadItem;
      return editor as RadItem;
    }
  }
}
