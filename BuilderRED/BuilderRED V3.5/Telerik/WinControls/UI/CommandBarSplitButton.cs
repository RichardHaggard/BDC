// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarSplitButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CommandBarSplitButton : CommandBarDropDownButton
  {
    private RadItem defaultItem;
    protected BorderPrimitive buttonBorder;
    protected RadCommandBarVisualElement buttonSeparator;

    static CommandBarSplitButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DropDownButtonStateManagerFatory(), typeof (CommandBarSplitButton));
    }

    public event EventHandler DefaultItemChanged;

    public event CancelEventHandler DefaultItemChanging;

    public BorderPrimitive ActionPartBorder
    {
      get
      {
        return this.buttonBorder;
      }
    }

    public LightVisualElement Separator
    {
      get
      {
        return (LightVisualElement) this.buttonSeparator;
      }
    }

    public RadItem DefaultItem
    {
      get
      {
        return this.defaultItem;
      }
      set
      {
        if (this.defaultItem == value || this.OnDefaultItemChanging())
          return;
        this.defaultItem = value;
        this.SetDefaultItemCore();
        this.OnDefaultItemChanged();
      }
    }

    protected virtual bool OnDefaultItemChanging()
    {
      if (this.DefaultItemChanging == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      this.DefaultItemChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnDefaultItemChanged()
    {
      if (this.DefaultItemChanged == null)
        return;
      this.DefaultItemChanged((object) this, EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      RectangleF boundingRectangle = (RectangleF) this.arrowButton.ControlBoundingRectangle;
      if ((double) e.X >= (double) boundingRectangle.X && (double) e.X <= (double) boundingRectangle.Right && ((double) e.Y >= (double) boundingRectangle.Y && (double) e.Y <= (double) boundingRectangle.Bottom) || e.Button != MouseButtons.Left)
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
      if (this.defaultItem == null)
        return;
      this.defaultItem.CallDoClick(EventArgs.Empty);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrowButton.Class = "CommandBarSplitButtonArrow";
      this.buttonSeparator = new RadCommandBarVisualElement();
      this.buttonSeparator.NotifyParentOnMouseInput = true;
      this.buttonSeparator.Class = "CommandBarSplitButtonSeparator";
      this.buttonSeparator.StretchVertically = true;
      this.buttonSeparator.StretchHorizontally = false;
      int num1 = (int) this.buttonSeparator.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(2, 2));
      this.buttonSeparator.DrawText = false;
      int num2 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.DrawFillProperty, (object) false);
      int num3 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      int num4 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.BorderBoxStyleProperty, (object) BorderBoxStyle.FourBorders);
      int num5 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.BorderBottomWidthProperty, (object) 0.0f);
      int num6 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.BorderTopWidthProperty, (object) 0.0f);
      int num7 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.BorderLeftWidthProperty, (object) 1f);
      int num8 = (int) this.buttonSeparator.SetDefaultValueOverride(LightVisualElement.BorderRightWidthProperty, (object) 1f);
      this.Children.Add((RadElement) this.buttonSeparator);
      this.buttonBorder = new BorderPrimitive();
      this.Children.Add((RadElement) this.buttonBorder);
      this.arrowButton.Click += new EventHandler(this.arrowButton_Click);
    }

    private void arrowButton_Click(object sender, EventArgs e)
    {
      this.ShowDropdown();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (!this.VisibleInStrip)
        return SizeF.Empty;
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.buttonSeparator.Measure(availableSize);
      sizeF.Width += this.buttonSeparator.DesiredSize.Width;
      if (this.buttonBorder.Visibility != ElementVisibility.Collapsed)
      {
        Padding borderThickness = this.buttonBorder.GetBorderThickness();
        sizeF.Width += (float) (borderThickness.Left + borderThickness.Right);
        sizeF.Height += (float) (borderThickness.Top + borderThickness.Bottom);
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      Padding borderThickness = this.buttonBorder.GetBorderThickness();
      if (this.RightToLeft)
      {
        float left = clientRectangle.Left;
        this.arrowButton.Arrange(new RectangleF(clientRectangle.Location, new SizeF(this.arrowButton.DesiredSize.Width, clientRectangle.Height)));
        float x1 = left + this.arrowButton.DesiredSize.Width;
        this.buttonSeparator.Arrange(new RectangleF(x1, clientRectangle.Top, this.buttonSeparator.DesiredSize.Width, clientRectangle.Height));
        float x2 = x1 + this.buttonSeparator.DesiredSize.Width;
        this.buttonBorder.Arrange(new RectangleF(x2, clientRectangle.Top, clientRectangle.Right - x2, clientRectangle.Height));
        float x3 = x2 + (float) borderThickness.Left;
        this.Layout.Arrange(new RectangleF(x3, clientRectangle.Top + (float) borderThickness.Top, clientRectangle.Right - x3 - (float) borderThickness.Right, clientRectangle.Height - (float) borderThickness.Top - (float) borderThickness.Bottom));
      }
      else
      {
        float x1 = clientRectangle.Right - this.arrowButton.DesiredSize.Width;
        this.arrowButton.Arrange(new RectangleF(new PointF(x1, clientRectangle.Top), new SizeF(this.arrowButton.DesiredSize.Width, clientRectangle.Height)));
        float x2 = x1 - this.buttonSeparator.DesiredSize.Width;
        this.buttonSeparator.Arrange(new RectangleF(x2, clientRectangle.Top, this.buttonSeparator.DesiredSize.Width, clientRectangle.Height));
        this.buttonBorder.Arrange(new RectangleF(clientRectangle.Location, new SizeF(x2 - clientRectangle.Left, clientRectangle.Height)));
        this.Layout.Arrange(new RectangleF(clientRectangle.Left + (float) borderThickness.Left, clientRectangle.Top + (float) borderThickness.Top, x2 - (float) borderThickness.Right - (float) borderThickness.Left - clientRectangle.Left, clientRectangle.Height - (float) borderThickness.Top - (float) borderThickness.Bottom));
      }
      return finalSize;
    }

    private void SetDefaultItemCore()
    {
      if (this.defaultItem == null)
        return;
      this.Text = this.defaultItem.Text;
      PropertyInfo property = this.defaultItem.GetType().GetProperty("Image");
      if ((object) property == null)
        return;
      this.Image = (Image) property.GetValue((object) this.defaultItem, (object[]) null);
    }
  }
}
