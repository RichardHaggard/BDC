// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI
{
  public class LayoutControlItem : LayoutControlItemBase
  {
    private float textProportionalSize = 0.5f;
    private int textFixedSize = 100;
    private LayoutItemTextPosition textPosition = LayoutItemTextPosition.Left;
    private RadVerticalAlignment? controlVerticalAlignment = new RadVerticalAlignment?();
    private Control control;
    private string associatedControlName;
    private int textMinSize;
    private int textMaxSize;
    private LayoutItemTextSizeMode textSizeMode;

    [Browsable(true)]
    [Description("Gets or sets the position of the text of the item.")]
    [DefaultValue(LayoutItemTextPosition.Left)]
    public virtual LayoutItemTextPosition TextPosition
    {
      get
      {
        return this.textPosition;
      }
      set
      {
        if (this.textPosition == value)
          return;
        this.textPosition = value;
        this.OnNotifyPropertyChanged(nameof (TextPosition));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the position of the text of the item.")]
    [DefaultValue(RadVerticalAlignment.Stretch)]
    public RadVerticalAlignment ControlVerticalAlignment
    {
      get
      {
        if (this.controlVerticalAlignment.HasValue)
          return this.controlVerticalAlignment.Value;
        return this.AssociatedControl != null && this.AssociatedControl.AutoSize ? RadVerticalAlignment.Center : RadVerticalAlignment.Stretch;
      }
      set
      {
        if (this.controlVerticalAlignment.HasValue && this.controlVerticalAlignment.Value == value)
          return;
        this.controlVerticalAlignment = new RadVerticalAlignment?(value);
        this.UpdateControlBounds();
        this.OnNotifyPropertyChanged(nameof (ControlVerticalAlignment));
      }
    }

    [Description("Gets or sets the proportional size of the text part which will be used when TextSizeMode is set to proportional.")]
    [Browsable(true)]
    [DefaultValue(0.5f)]
    public float TextProportionalSize
    {
      get
      {
        return this.textProportionalSize;
      }
      set
      {
        if ((double) this.textProportionalSize == (double) value)
          return;
        this.textProportionalSize = value;
        this.OnNotifyPropertyChanged(nameof (TextProportionalSize));
      }
    }

    [DefaultValue(100)]
    [Description("Gets or sets the fixed size of the text part which will be used when TextSizeMode is set to fixed.")]
    [Browsable(true)]
    public int TextFixedSize
    {
      get
      {
        return this.textFixedSize;
      }
      set
      {
        if (this.textFixedSize == value)
          return;
        this.textFixedSize = value;
        this.OnNotifyPropertyChanged(nameof (TextFixedSize));
      }
    }

    [Browsable(true)]
    [DefaultValue(0)]
    [Description("Gets or sets the minimum size of the text part. ")]
    public int TextMinSize
    {
      get
      {
        return this.textMinSize;
      }
      set
      {
        if (this.textMinSize == value)
          return;
        this.textMinSize = value;
        this.OnNotifyPropertyChanged(nameof (TextMinSize));
      }
    }

    [Description("Gets or sets the maximum size of the text part. ")]
    [Browsable(true)]
    [DefaultValue(0)]
    public int TextMaxSize
    {
      get
      {
        if (this.textMaxSize != 0 && this.textMinSize != 0 && this.textMaxSize < this.textMinSize)
          return this.textMinSize;
        return this.textMaxSize;
      }
      set
      {
        if (this.textMaxSize == value)
          return;
        this.textMaxSize = value;
        this.OnNotifyPropertyChanged(nameof (TextMaxSize));
      }
    }

    [Browsable(true)]
    [DefaultValue(LayoutItemTextSizeMode.Proportional)]
    [Description("Gets or sets the way in which the text part will be sized - proportionally or fixed-size.")]
    public LayoutItemTextSizeMode TextSizeMode
    {
      get
      {
        return this.textSizeMode;
      }
      set
      {
        if (this.textSizeMode == value)
          return;
        this.textSizeMode = value;
        this.OnNotifyPropertyChanged(nameof (TextSizeMode));
      }
    }

    [Description("Gets or sets the control associated with this item.")]
    public Control AssociatedControl
    {
      get
      {
        return this.control;
      }
      set
      {
        this.SetAssociatedControl(value);
      }
    }

    protected virtual void SetAssociatedControl(Control value)
    {
      bool flag = this.control != value;
      this.control = value;
      if (this.control != null)
        this.control.Visible = !this.IsHidden;
      if (!flag)
        return;
      this.OnNotifyPropertyChanged("AssociatedControl");
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string AssociatedControlName
    {
      get
      {
        if (this.control == null)
          return string.Empty;
        return this.control.Name;
      }
      set
      {
        if (this.ElementTree != null)
        {
          this.AssociatedControl = this.FindControlByName(value);
          this.associatedControlName = (string) null;
        }
        else
          this.associatedControlName = value;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.associatedControlName == null)
        return;
      this.AssociatedControl = this.FindControlByName(this.associatedControlName);
      this.associatedControlName = (string) null;
    }

    private Control FindControlByName(string name)
    {
      if (this.ElementTree == null)
        return (Control) null;
      foreach (Control control in (ArrangedElementCollection) this.ElementTree.Control.Controls)
      {
        if (control.Name == name)
          return control;
      }
      return (Control) null;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Padding = new Padding(3);
      this.DrawFill = false;
      this.DrawBorder = false;
      this.DrawText = false;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.MinSize = new Size(46, 26);
      this.Bounds = new Rectangle(0, 0, 100, 100);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != LightVisualElement.DrawTextProperty || this.Parent == null)
        return;
      this.Parent.InvalidateMeasure();
      this.Parent.UpdateLayout();
      this.UpdateControlBounds();
      this.Parent.Invalidate();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if ((e.PropertyName == "TextPosition" || e.PropertyName == "TextProportionalSize" || (e.PropertyName == "TextFixedSize" || e.PropertyName == "TextMinSize") || (e.PropertyName == "TextMaxSize" || e.PropertyName == "TextSizeMode")) && (this.Parent != null && !this.Parent.IsLayoutSuspended))
      {
        this.Parent.InvalidateMeasure();
        this.Parent.UpdateLayout();
        this.UpdateControlBounds();
        this.Parent.Invalidate();
      }
      base.OnNotifyPropertyChanged(e);
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.AssociatedControl == null || this.ElementTree == null)
        return;
      RadLayoutControl control = this.ElementTree.Control as RadLayoutControl;
      if (control == null || control.Controls.Contains(this.AssociatedControl))
        return;
      ((RadLayoutControlControlCollection) control.Controls).AddInternal(this.AssociatedControl);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (!(propertyName == "IsHidden") || this.AssociatedControl == null)
        return;
      this.AssociatedControl.Visible = !this.IsHidden && this.Visibility == ElementVisibility.Visible;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      if (this.DrawText)
        this.Layout.Measure(this.GetTextRectangle(clientRectangle).Size);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.MeasureOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.DrawText)
        this.Layout.Arrange(this.GetTextRectangle(clientRectangle));
      return sizeF;
    }

    protected virtual RectangleF GetTextRectangle(RectangleF clientRect)
    {
      SizeF size = clientRect.Size;
      if (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Right)
      {
        size.Width = this.TextSizeMode == LayoutItemTextSizeMode.Proportional ? clientRect.Width * this.TextProportionalSize : (float) this.TextFixedSize;
        if (this.TextMinSize != 0)
          size.Width = Math.Max((float) this.TextMinSize, size.Width);
        if (this.TextMaxSize != 0)
          size.Width = Math.Min((float) this.TextMaxSize, size.Width);
      }
      if (this.TextPosition == LayoutItemTextPosition.Top || this.TextPosition == LayoutItemTextPosition.Bottom)
      {
        size.Height = this.TextSizeMode == LayoutItemTextSizeMode.Proportional ? clientRect.Height * this.TextProportionalSize : (float) this.TextFixedSize;
        if (this.TextMinSize != 0)
          size.Height = Math.Max((float) this.TextMinSize, size.Height);
        if (this.TextMaxSize != 0)
          size.Height = Math.Min((float) this.TextMaxSize, size.Height);
      }
      RectangleF rectangleF = RectangleF.Empty;
      if (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Top)
        rectangleF = new RectangleF(clientRect.Location, size);
      else if (this.TextPosition == LayoutItemTextPosition.Right)
        rectangleF = new RectangleF(new PointF(clientRect.Right - size.Width, clientRect.Y), size);
      else if (this.TextPosition == LayoutItemTextPosition.Bottom)
        rectangleF = new RectangleF(new PointF(clientRect.X, clientRect.Bottom - size.Height), size);
      return rectangleF;
    }

    protected virtual Rectangle GetControlRectangle(RectangleF clientRect)
    {
      if (this.DrawText)
      {
        RectangleF textRectangle = this.GetTextRectangle(clientRect);
        if (this.TextPosition == LayoutItemTextPosition.Left)
          clientRect = new RectangleF(textRectangle.Right, clientRect.Y, clientRect.Width - textRectangle.Width, clientRect.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Top)
          clientRect = new RectangleF(clientRect.X, textRectangle.Bottom, clientRect.Width, clientRect.Height - textRectangle.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Right)
          clientRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width - textRectangle.Width, clientRect.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Bottom)
          clientRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height - textRectangle.Height);
      }
      return this.GetAlignedControlRectangle(new Rectangle((int) clientRect.X, (int) clientRect.Y, (int) clientRect.Width, (int) clientRect.Height));
    }

    protected virtual Rectangle GetAlignedControlRectangle(Rectangle controlArrangeRect)
    {
      if (this.ControlVerticalAlignment == RadVerticalAlignment.Stretch)
        return controlArrangeRect;
      if (this.ControlVerticalAlignment == RadVerticalAlignment.Top)
        return new Rectangle(controlArrangeRect.Location, new Size(controlArrangeRect.Width, this.AssociatedControl.Height));
      if (this.ControlVerticalAlignment == RadVerticalAlignment.Bottom)
        return new Rectangle(new Point(controlArrangeRect.X, controlArrangeRect.Bottom - this.AssociatedControl.Height), new Size(controlArrangeRect.Width, this.AssociatedControl.Height));
      if (this.ControlVerticalAlignment == RadVerticalAlignment.Center)
        return new Rectangle(new Point(controlArrangeRect.X, controlArrangeRect.Top + (controlArrangeRect.Height - this.AssociatedControl.Height) / 2), new Size(controlArrangeRect.Width, this.AssociatedControl.Height));
      return controlArrangeRect;
    }

    public void UpdateControlBounds()
    {
      if (this.AssociatedControl == null)
        return;
      RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.ControlBoundingRectangle.Size);
      clientRectangle.Offset((PointF) this.ControlBoundingRectangle.Location);
      Rectangle controlRectangle = this.GetControlRectangle(clientRectangle);
      this.AssociatedControl.SetBounds(controlRectangle.X, controlRectangle.Y, controlRectangle.Width, controlRectangle.Height, BoundsSpecified.All);
    }
  }
}
