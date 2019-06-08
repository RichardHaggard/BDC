// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class WaitingBarIndicatorElement : BaseWaitingBarIndicatorElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (WaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OffsetProperty = RadProperty.Register(nameof (Offset), typeof (int), typeof (WaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    private WaitingBarSeparatorElement separator;

    static WaitingBarIndicatorElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new WaitingBarIndicatorStateManager(), typeof (WaitingBarIndicatorElement));
    }

    public WaitingBarSeparatorElement SeparatorElement
    {
      get
      {
        return this.separator;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.StretchVertically = true;
      this.StretchHorizontally = false;
      this.ZIndex = -1;
      this.BackColor = Color.Green;
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.separator = this.CreateSeparatorElement();
      this.separator.Class = "WaitingIndicatorSeparator";
      this.Children.Add((RadElement) this.separator);
    }

    protected virtual WaitingBarSeparatorElement CreateSeparatorElement()
    {
      return new WaitingBarSeparatorElement();
    }

    public virtual int Offset
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(WaitingBarIndicatorElement.OffsetProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarIndicatorElement.OffsetProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RadWaitingBarElement ancestor = this.FindAncestor<RadWaitingBarElement>();
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (ancestor != null)
      {
        sizeF.Height = !this.StretchVertically || float.IsInfinity(availableSize.Height) ? (this.Image == null || this.GetValueSource(RadWaitingBarElement.WaitingIndicatorSizeProperty) == ValueSource.Local ? (float) ancestor.WaitingIndicatorSize.Height : (float) this.Image.Height) : availableSize.Height;
        if (this.StretchHorizontally && !float.IsInfinity(availableSize.Width))
          sizeF.Width = availableSize.Width;
        else if (this.Image != null && this.GetValueSource(RadWaitingBarElement.WaitingIndicatorSizeProperty) != ValueSource.Local)
          sizeF.Height = (float) this.Image.Height;
        else
          sizeF.Width = (float) ancestor.WaitingIndicatorSize.Width;
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.SeparatorElement.Arrange(this.SetDashInitialPosition(this.SeparatorElement, this.GetClientRectangle(finalSize)));
      return finalSize;
    }

    protected RectangleF SetDashInitialPosition(
      WaitingBarSeparatorElement element,
      RectangleF clientRect)
    {
      int num = element.StepWidth + element.SeparatorWidth;
      return new RectangleF(new PointF(clientRect.X - (float) num, clientRect.Y - 1f), new SizeF(clientRect.Width + (float) num, clientRect.Height + 1f));
    }

    public override void Animate(int step)
    {
    }

    public override void ResetAnimation()
    {
    }
  }
}
