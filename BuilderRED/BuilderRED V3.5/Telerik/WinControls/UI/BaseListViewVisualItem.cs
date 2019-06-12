// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseListViewVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BaseListViewVisualItem : LightVisualElement, IVirtualizedElement<ListViewDataItem>
  {
    public static RadProperty HotTrackingProperty = RadProperty.Register("HotTracking", typeof (bool), typeof (BaseListViewVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (BaseListViewVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentProperty = RadProperty.Register(nameof (Current), typeof (bool), typeof (BaseListViewVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsControlInactiveProperty = RadProperty.Register("IsControlInactive", typeof (bool), typeof (BaseListViewVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    protected ListViewDataItem dataItem;
    private RadToggleButtonElement toggleElement;
    private IInputEditor editor;

    static BaseListViewVisualItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ListViewVisualItemStateManagerFactory(), typeof (BaseListViewVisualItem));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.toggleElement = this.CreateToggleElement();
      this.toggleElement.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.toggleElement.NotifyParentOnMouseInput = false;
      this.toggleElement.ShouldHandleMouseInput = true;
      this.toggleElement.Alignment = ContentAlignment.MiddleCenter;
      this.toggleElement.StretchHorizontally = this.toggleElement.StretchVertically = false;
      this.toggleElement.MinSize = new Size(16, 16);
      this.toggleElement.ToggleStateChanging += new StateChangingEventHandler(this.toggleButton_ToggleStateChanging);
      this.Children.Add((RadElement) this.toggleElement);
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.ImageLayout = ImageLayout.None;
      this.ImageAlignment = ListViewDataItemStyle.DefaultImageAlignment;
      this.TextAlignment = ListViewDataItemStyle.DefaultImageAlignment;
      this.TextImageRelation = ListViewDataItemStyle.DefaultTextImageRelation;
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.DrawFill = true;
      this.NumberOfColors = 1;
    }

    protected virtual RadToggleButtonElement CreateToggleElement()
    {
      return (RadToggleButtonElement) new ListViewItemCheckbox();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsMouseOverElementProperty || !this.IsInValidState(true) || this.Data == null)
        return;
      if (this.Data.Owner.HotTracking)
      {
        int num1 = (int) this.SetValue(BaseListViewVisualItem.HotTrackingProperty, e.NewValue);
      }
      else
      {
        int num2 = (int) this.SetValue(BaseListViewVisualItem.HotTrackingProperty, (object) false);
      }
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return this.Data.Owner.AllowDragDrop;
    }

    protected override void DisposeManagedResources()
    {
      this.Detach();
      base.DisposeManagedResources();
    }

    private void toggleButton_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      args.Cancel = this.OnToggleButtonStateChanging(args);
    }

    public ListViewDataItem Data
    {
      get
      {
        return this.dataItem;
      }
    }

    public virtual void Attach(ListViewDataItem data, object context)
    {
      this.dataItem = data;
      this.WireItemEvents();
      this.Synchronize();
    }

    public virtual void Detach()
    {
      if (this.dataItem != null)
      {
        if (this.IsInEditMode)
        {
          this.Data.Owner.CancelEdit();
          this.RemoveEditor(this.editor);
        }
        this.UnwireItemEvents();
      }
      this.ResetProperties();
      this.dataItem = (ListViewDataItem) null;
    }

    protected virtual void ResetProperties()
    {
      int num1 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      int num4 = (int) this.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
      int num5 = (int) this.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
      int num6 = (int) this.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
      int num7 = (int) this.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
      int num8 = (int) this.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
      int num9 = (int) this.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
      int num10 = (int) this.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
      int num11 = (int) this.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
      int num12 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
      int num13 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
      int num14 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ListViewDataItemStyle.DefaultTextAlignment);
      int num15 = (int) this.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
      int num16 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) ListViewDataItemStyle.DefaultTextImageRelation);
      int num17 = (int) this.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
      int num18 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) ListViewDataItemStyle.DefaultImageAlignment);
      int num19 = (int) this.ResetValue(RadElement.ContainsMouseProperty);
      int num20 = (int) this.ResetValue(BaseListViewVisualItem.HotTrackingProperty);
    }

    public void Synchronize()
    {
      if (this.dataItem == null || this.dataItem.Owner == null)
        return;
      this.SynchronizeProperties();
      if (this.dataItem == null || this.dataItem.Owner == null)
        return;
      this.dataItem.Owner.OnVisualItemFormatting(this);
    }

    public virtual bool IsCompatible(ListViewDataItem data, object context)
    {
      return !(data is ListViewDataItemGroup) && data.Owner.ViewType == ListViewType.ListView;
    }

    protected virtual void UnwireItemEvents()
    {
      this.dataItem.PropertyChanged -= new PropertyChangedEventHandler(this.DataPropertyChanged);
    }

    protected virtual void WireItemEvents()
    {
      this.dataItem.PropertyChanged += new PropertyChangedEventHandler(this.DataPropertyChanged);
    }

    protected virtual void SynchronizeProperties()
    {
      this.toggleElement.ToggleStateChanging -= new StateChangingEventHandler(this.toggleButton_ToggleStateChanging);
      if (this.Data.Owner != null)
      {
        this.toggleElement.IsThreeState = this.Data.Owner.ThreeStateMode;
        ContentAlignment contentAlignment = ContentAlignment.MiddleCenter;
        if (this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Left || this.Data.Owner.CheckBoxesPosition == CheckBoxesPosition.Right)
        {
          switch (this.Data.Owner.CheckBoxesAlignment)
          {
            case CheckBoxesAlignment.Near:
              contentAlignment = ContentAlignment.TopCenter;
              break;
            case CheckBoxesAlignment.Center:
              contentAlignment = ContentAlignment.MiddleCenter;
              break;
            case CheckBoxesAlignment.Far:
              contentAlignment = ContentAlignment.BottomCenter;
              break;
          }
        }
        else
        {
          switch (this.Data.Owner.CheckBoxesAlignment)
          {
            case CheckBoxesAlignment.Near:
              contentAlignment = ContentAlignment.MiddleLeft;
              break;
            case CheckBoxesAlignment.Center:
              contentAlignment = ContentAlignment.MiddleCenter;
              break;
            case CheckBoxesAlignment.Far:
              contentAlignment = ContentAlignment.MiddleRight;
              break;
          }
        }
        this.toggleElement.Alignment = contentAlignment;
      }
      else
      {
        this.toggleElement.IsThreeState = false;
        this.toggleElement.Alignment = ContentAlignment.MiddleCenter;
      }
      this.toggleElement.ToggleState = !this.toggleElement.IsThreeState ? (this.Data.CheckState == Telerik.WinControls.Enumerations.ToggleState.On ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off) : this.Data.CheckState;
      this.Text = this.Data.Text;
      this.Image = this.Data.Image;
      this.Selected = this.Data.Selected;
      this.Current = this.Data.Current;
      this.Enabled = this.Data.Enabled;
      int num1 = (int) this.SetValue(BaseListViewVisualItem.IsControlInactiveProperty, (object) !this.Data.Owner.ElementTree.Control.Focused);
      if (this.Data.HasStyle)
      {
        if (this.Data.Font != ListViewDataItemStyle.DefaultFont && this.Data.Font != this.Font)
          this.Font = this.Data.Font;
        else if (this.Data.Font == ListViewDataItemStyle.DefaultFont)
        {
          int num2 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
        if (this.Data.ForeColor != ListViewDataItemStyle.DefaultForeColor && this.Data.ForeColor != this.ForeColor)
          this.ForeColor = this.Data.ForeColor;
        else if (this.Data.ForeColor == ListViewDataItemStyle.DefaultForeColor)
        {
          int num3 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        }
        if (this.Data.BackColor != ListViewDataItemStyle.DefaultBackColor && this.Data.BackColor != this.BackColor)
          this.BackColor = this.Data.BackColor;
        else if (this.Data.BackColor == ListViewDataItemStyle.DefaultBackColor)
        {
          int num4 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }
        if (this.Data.BackColor2 != ListViewDataItemStyle.DefaultBackColor2 && this.Data.BackColor2 != this.BackColor2)
          this.BackColor2 = this.Data.BackColor2;
        else if (this.Data.BackColor2 == ListViewDataItemStyle.DefaultBackColor2)
        {
          int num5 = (int) this.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
        }
        if (this.Data.BackColor3 != ListViewDataItemStyle.DefaultBackColor3 && this.Data.BackColor3 != this.BackColor3)
          this.BackColor3 = this.Data.BackColor3;
        else if (this.Data.BackColor3 == ListViewDataItemStyle.DefaultBackColor3)
        {
          int num6 = (int) this.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
        }
        if (this.Data.BackColor4 != ListViewDataItemStyle.DefaultBackColor4 && this.Data.BackColor4 != this.BackColor4)
          this.BackColor4 = this.Data.BackColor4;
        else if (this.Data.BackColor4 == ListViewDataItemStyle.DefaultBackColor4)
        {
          int num7 = (int) this.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
        }
        if (this.Data.BorderColor != ListViewDataItemStyle.DefaultBorderColor && this.Data.BorderColor != this.BorderColor)
          this.BorderColor = this.Data.BorderColor;
        else if (this.Data.BorderColor == ListViewDataItemStyle.DefaultBorderColor)
        {
          int num8 = (int) this.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
        }
        if (this.Data.NumberOfColors != ListViewDataItemStyle.DefaultNumberOfColors && this.Data.NumberOfColors != this.NumberOfColors)
          this.NumberOfColors = this.Data.NumberOfColors;
        else if (this.Data.NumberOfColors == ListViewDataItemStyle.DefaultNumberOfColors)
        {
          int num9 = (int) this.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
        }
        if ((double) this.Data.GradientPercentage != (double) ListViewDataItemStyle.DefaultGradientPercentage && (double) this.Data.GradientPercentage != (double) this.GradientPercentage)
          this.GradientPercentage = this.Data.GradientPercentage;
        else if ((double) this.Data.GradientPercentage == (double) ListViewDataItemStyle.DefaultGradientPercentage)
        {
          int num10 = (int) this.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
        }
        if ((double) this.Data.GradientPercentage2 != (double) ListViewDataItemStyle.DefaultGradientPercentage2 && (double) this.Data.GradientPercentage2 != (double) this.GradientPercentage2)
          this.GradientPercentage2 = this.Data.GradientPercentage2;
        else if ((double) this.Data.GradientPercentage2 == (double) ListViewDataItemStyle.DefaultGradientPercentage2)
        {
          int num11 = (int) this.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
        }
        if ((double) this.Data.GradientAngle != (double) ListViewDataItemStyle.DefaultGradientAngle && (double) this.Data.GradientAngle != (double) this.GradientAngle)
          this.GradientAngle = this.Data.GradientAngle;
        else if ((double) this.Data.GradientAngle == (double) ListViewDataItemStyle.DefaultGradientAngle)
        {
          int num12 = (int) this.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
        }
        if (this.Data.GradientStyle != ListViewDataItemStyle.DefaultGradientStyle && this.Data.GradientStyle != this.GradientStyle)
          this.GradientStyle = this.Data.GradientStyle;
        else if (this.Data.GradientStyle == ListViewDataItemStyle.DefaultGradientStyle)
        {
          int num13 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        }
        if (this.Data.TextAlignment != ListViewDataItemStyle.DefaultTextAlignment && this.Data.TextAlignment != this.TextAlignment)
          this.TextAlignment = this.Data.TextAlignment;
        else if (this.Data.TextAlignment == ListViewDataItemStyle.DefaultTextAlignment)
        {
          int num14 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
          int num15 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ListViewDataItemStyle.DefaultTextAlignment);
        }
        if (this.Data.TextImageRelation != ListViewDataItemStyle.DefaultTextImageRelation && this.Data.TextImageRelation != this.TextImageRelation)
          this.TextImageRelation = this.Data.TextImageRelation;
        else if (this.Data.TextImageRelation == ListViewDataItemStyle.DefaultTextImageRelation)
        {
          int num14 = (int) this.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
          int num15 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) ListViewDataItemStyle.DefaultTextImageRelation);
        }
        if (this.Data.ImageAlignment != ListViewDataItemStyle.DefaultImageAlignment && this.Data.ImageAlignment != this.ImageAlignment)
          this.ImageAlignment = this.Data.ImageAlignment;
        else if (this.Data.ImageAlignment == ListViewDataItemStyle.DefaultImageAlignment)
        {
          int num14 = (int) this.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
          int num15 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) ListViewDataItemStyle.DefaultImageAlignment);
        }
      }
      else
      {
        int num2 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        int num3 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        int num4 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        int num5 = (int) this.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
        int num6 = (int) this.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
        int num7 = (int) this.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
        int num8 = (int) this.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
        int num9 = (int) this.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
        int num10 = (int) this.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
        int num11 = (int) this.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
        int num12 = (int) this.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
        int num13 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        int num14 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
        int num15 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ListViewDataItemStyle.DefaultTextAlignment);
        int num16 = (int) this.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
        int num17 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) ListViewDataItemStyle.DefaultTextImageRelation);
        int num18 = (int) this.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
        int num19 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) ListViewDataItemStyle.DefaultImageAlignment);
      }
      this.toggleElement.ToggleStateChanging += new StateChangingEventHandler(this.toggleButton_ToggleStateChanging);
    }

    protected virtual bool OnToggleButtonStateChanging(StateChangingEventArgs args)
    {
      if (this.IsInValidState(true))
      {
        this.dataItem.CheckState = args.NewValue;
        if (this.dataItem != null)
          args.Cancel = this.dataItem.CheckState != args.NewValue;
      }
      return args.Cancel;
    }

    protected virtual void DataPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.IsDesignMode || e.PropertyName == "ActualSize" || (this.dataItem == null || this.dataItem.Owner == null))
        return;
      this.SuspendLayout();
      this.SynchronizeProperties();
      this.dataItem.Owner.OnVisualItemFormatting(this);
      this.ResumeLayout(true);
    }

    public bool IsControlActive
    {
      get
      {
        return !(bool) this.GetValue(BaseListViewVisualItem.IsControlInactiveProperty);
      }
    }

    public RadToggleButtonElement ToggleElement
    {
      get
      {
        return this.toggleElement;
      }
    }

    [Browsable(false)]
    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(BaseListViewVisualItem.SelectedProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(BaseListViewVisualItem.SelectedProperty, (object) value);
      }
    }

    [Browsable(false)]
    public bool Current
    {
      get
      {
        return (bool) this.GetValue(BaseListViewVisualItem.CurrentProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(BaseListViewVisualItem.CurrentProperty, (object) value);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.ArrangeContentCore(this.GetClientRectangle(finalSize));
      return finalSize;
    }

    protected virtual void ArrangeContentCore(RectangleF clientRect)
    {
      RectangleF toggleRectangle;
      RectangleF layoutManagerRect;
      this.GetArrangeRectangles(clientRect, out toggleRectangle, out layoutManagerRect);
      if (this.RightToLeft)
      {
        toggleRectangle = LayoutUtils.RTLTranslateNonRelative(toggleRectangle, clientRect);
        layoutManagerRect = LayoutUtils.RTLTranslateNonRelative(layoutManagerRect, clientRect);
      }
      if (this.toggleElement.Visibility != ElementVisibility.Collapsed)
        this.toggleElement.Arrange(toggleRectangle);
      this.Layout.Arrange(layoutManagerRect);
      RadItem editorElement = this.GetEditorElement((IValueEditor) this.editor);
      if (!this.IsInEditMode || editorElement == null)
        return;
      editorElement.Arrange(this.GetEditorArrangeRectangle(clientRect));
    }

    protected void GetArrangeRectangles(
      RectangleF clientRect,
      out RectangleF toggleRectangle,
      out RectangleF layoutManagerRect)
    {
      switch (this.Data == null || this.Data.Owner == null ? CheckBoxesPosition.Left : this.Data.Owner.CheckBoxesPosition)
      {
        case CheckBoxesPosition.Left:
          toggleRectangle = new RectangleF(clientRect.Location, new SizeF(this.toggleElement.DesiredSize.Width, clientRect.Height));
          layoutManagerRect = new RectangleF(clientRect.X + this.toggleElement.DesiredSize.Width, clientRect.Y, clientRect.Width - this.toggleElement.DesiredSize.Width, clientRect.Height);
          break;
        case CheckBoxesPosition.Top:
          toggleRectangle = new RectangleF(clientRect.Location, new SizeF(clientRect.Width, this.toggleElement.DesiredSize.Height));
          layoutManagerRect = new RectangleF(clientRect.X, clientRect.Y + this.toggleElement.DesiredSize.Height, clientRect.Width, clientRect.Height - this.toggleElement.DesiredSize.Height);
          break;
        case CheckBoxesPosition.Right:
          toggleRectangle = new RectangleF(new PointF(clientRect.Right - this.toggleElement.DesiredSize.Width, clientRect.Y), new SizeF(this.toggleElement.DesiredSize.Width, clientRect.Height));
          layoutManagerRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width - this.toggleElement.DesiredSize.Width, clientRect.Height);
          break;
        case CheckBoxesPosition.Bottom:
          toggleRectangle = new RectangleF(new PointF(clientRect.Location.X, clientRect.Bottom - this.toggleElement.DesiredSize.Height), new SizeF(clientRect.Width, this.toggleElement.DesiredSize.Height));
          layoutManagerRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height - this.toggleElement.DesiredSize.Height);
          break;
        default:
          toggleRectangle = RectangleF.Empty;
          layoutManagerRect = clientRect;
          break;
      }
    }

    protected virtual RectangleF GetEditorArrangeRectangle(RectangleF clientRect)
    {
      RectangleF bounds = new RectangleF(clientRect.X + this.toggleElement.DesiredSize.Width, clientRect.Y, clientRect.Width - this.toggleElement.DesiredSize.Width, clientRect.Height);
      if ((double) bounds.Width > (double) this.Data.Owner.ViewElement.ViewElement.DesiredSize.Width)
        bounds.Width = this.Data.Owner.ViewElement.ViewElement.DesiredSize.Width - clientRect.X;
      if (this.RightToLeft)
        bounds = LayoutUtils.RTLTranslateNonRelative(bounds, clientRect);
      return bounds;
    }

    protected override SizeF MeasureElements(
      SizeF availableSize,
      SizeF clientSize,
      Padding borderThickness)
    {
      SizeF sizeF1 = SizeF.Empty;
      if (this.AutoSize)
      {
        foreach (RadElement child in this.Children)
        {
          if (child != this.GetEditorElement((IValueEditor) this.editor))
          {
            SizeF sizeF2 = SizeF.Empty;
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds || this.BypassLayoutPolicies)
            {
              child.Measure(availableSize);
              sizeF2 = child.DesiredSize;
            }
            else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
            {
              child.Measure(new SizeF(clientSize.Width - (float) borderThickness.Horizontal, clientSize.Height - (float) borderThickness.Vertical));
              sizeF2.Width = child.DesiredSize.Width + (float) borderThickness.Horizontal;
              sizeF2.Height += (float) borderThickness.Vertical;
            }
            else
            {
              child.Measure(clientSize);
              sizeF2.Width += child.DesiredSize.Width + (float) this.Padding.Horizontal + (float) borderThickness.Horizontal;
              sizeF2.Height += child.DesiredSize.Height + (float) this.Padding.Vertical + (float) borderThickness.Vertical;
            }
            sizeF1.Width = Math.Max(sizeF1.Width, sizeF2.Width);
            sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
          }
        }
      }
      else
      {
        foreach (RadElement child in this.Children)
          child.Measure(availableSize);
        sizeF1 = (SizeF) this.Size;
      }
      return sizeF1;
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
      RadItem editorElement = this.GetEditorElement((IValueEditor) editor);
      if (editorElement == null || this.Children.Contains((RadElement) editorElement))
        return;
      this.Children.Add((RadElement) editorElement);
      this.Data.Owner.ViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      if (editor == null || this.editor != editor)
        return;
      RadItem editorElement = this.GetEditorElement((IValueEditor) editor);
      if (editorElement != null && this.Children.Contains((RadElement) editorElement))
        this.Children.Remove((RadElement) editorElement);
      this.editor = (IInputEditor) null;
      this.Synchronize();
    }

    protected virtual RadItem GetEditorElement(IValueEditor editor)
    {
      BaseInputEditor editor1 = this.editor as BaseInputEditor;
      if (editor1 != null)
        return editor1.EditorElement as RadItem;
      return editor as RadItem;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseDown(new ListViewItemMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseUp(new ListViewItemMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseMove(new ListViewItemMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseHover(EventArgs e)
    {
      base.OnMouseHover(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseHover(new ListViewItemEventArgs(this.Data));
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseEnter(new ListViewItemEventArgs(this.Data));
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseLeave(new ListViewItemEventArgs(this.Data));
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseClick(new ListViewItemEventArgs(this.Data));
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.Data == null || this.Data.Owner == null)
        return;
      this.Data.Owner.OnItemMouseDoubleClick(new ListViewItemEventArgs(this.Data));
    }
  }
}
