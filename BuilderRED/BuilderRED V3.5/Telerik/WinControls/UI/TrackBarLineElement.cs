// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarLineElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarLineElement : TrackBarElementWithOrientation
  {
    public static RadProperty ShowSlideAreaProperty = RadProperty.Register(nameof (ShowSlideArea), typeof (bool), typeof (TrackBarLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty SlideAreaWidthProperty = RadProperty.Register(nameof (SlideAreaWidth), typeof (int), typeof (TrackBarLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty SlideAreaColor1Property = RadProperty.Register(nameof (SlideAreaColor1), typeof (Color), typeof (TrackBarLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(160, 160, 160), ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty SlideAreaColor2Property = RadProperty.Register(nameof (SlideAreaColor2), typeof (Color), typeof (TrackBarLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(160, 160, 160), ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty SlideAreaGradientAngleProperty = RadProperty.Register(nameof (SlideAreaGradientAngle), typeof (float), typeof (TrackBarLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.InvalidatesLayout));

    static TrackBarLineElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TrackBarStateManager(), typeof (TrackBarLineElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    public bool ShowSlideArea
    {
      get
      {
        return (bool) this.GetValue(TrackBarLineElement.ShowSlideAreaProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarLineElement.ShowSlideAreaProperty, (object) value);
        if (!value)
          this.Visibility = ElementVisibility.Hidden;
        else
          this.Visibility = ElementVisibility.Visible;
      }
    }

    public int SlideAreaWidth
    {
      get
      {
        return (int) this.GetValue(TrackBarLineElement.SlideAreaWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarLineElement.SlideAreaWidthProperty, (object) value);
        this.Size = new Size(this.Size.Width, value);
      }
    }

    public Color SlideAreaColor1
    {
      get
      {
        return (Color) this.GetValue(TrackBarLineElement.SlideAreaColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarLineElement.SlideAreaColor1Property, (object) value);
        this.BackColor = value;
        this.GradientAngle = 0.0f;
        this.GradientStyle = GradientStyles.Linear;
      }
    }

    public Color SlideAreaColor2
    {
      get
      {
        return (Color) this.GetValue(TrackBarLineElement.SlideAreaColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarLineElement.SlideAreaColor2Property, (object) value);
        this.BackColor2 = value;
        this.GradientAngle = 0.0f;
        this.GradientStyle = GradientStyles.Linear;
      }
    }

    public float SlideAreaGradientAngle
    {
      get
      {
        return (float) this.GetValue(TrackBarLineElement.SlideAreaGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarLineElement.SlideAreaGradientAngleProperty, (object) value);
        this.GradientAngle = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      if (this.TrackBarElement.Orientation == Orientation.Horizontal)
      {
        this.StretchHorizontally = true;
        this.StretchVertically = false;
        return new SizeF(0.0f, (float) this.TrackBarElement.SlideAreaWidth);
      }
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      return new SizeF((float) this.TrackBarElement.SlideAreaWidth, 0.0f);
    }
  }
}
