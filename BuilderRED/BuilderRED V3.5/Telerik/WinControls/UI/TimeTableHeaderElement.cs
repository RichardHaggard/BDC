// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeTableHeaderElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TimeTableHeaderElement : StackLayoutElement
  {
    public static RadProperty ClockBeforeTablesProperty1 = RadProperty.Register(nameof (ClockBeforeTables1), typeof (bool), typeof (TimeTableHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));
    public static RadProperty SpinModeProperty = RadProperty.Register("SpinMode", typeof (bool), typeof (TimeTableHeaderElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay));
    private LightVisualElement leftArrow;
    private LightVisualElement headerElement;
    private LightVisualElement rightArrow;
    private bool spinMode;

    static TimeTableHeaderElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TimePickerVerticalStateManager(TimeTableHeaderElement.ClockBeforeTablesProperty1), typeof (TimeTableHeaderElement));
    }

    public TimeTableHeaderElement()
    {
      this.rightArrow.Visibility = ElementVisibility.Hidden;
      this.leftArrow.Visibility = ElementVisibility.Hidden;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.Orientation = Orientation.Horizontal;
      this.leftArrow = (LightVisualElement) new TimeHeaderLeftArrow();
      this.leftArrow.DrawText = false;
      this.leftArrow.Class = "TimeTableHeaderElementLeftArrow";
      this.leftArrow.TextAlignment = ContentAlignment.MiddleLeft;
      this.leftArrow.StretchHorizontally = false;
      this.headerElement = (LightVisualElement) new TimeHeaderArrow();
      this.headerElement.Text = DateTime.Now.ToString("t");
      this.headerElement.StretchHorizontally = true;
      this.headerElement.StretchVertically = true;
      this.headerElement.TextAlignment = ContentAlignment.MiddleCenter;
      this.headerElement.Class = "TimeTableHeaderElementheaderElement";
      this.rightArrow = (LightVisualElement) new TimeHeaderRightArrow();
      this.rightArrow.TextAlignment = ContentAlignment.MiddleRight;
      this.rightArrow.StretchHorizontally = false;
      this.rightArrow.DrawText = false;
      this.rightArrow.Class = "TimeTableHeaderElementRightArrow";
      this.Children.Add((RadElement) this.leftArrow);
      this.Children.Add((RadElement) this.headerElement);
      this.Children.Add((RadElement) this.rightArrow);
      this.BorderTopWidth = 1f;
      this.BorderLeftWidth = 1f;
      this.MinSize = new Size(0, 19);
      this.SetArrowButtonsVisibility();
    }

    public bool ClockBeforeTables1
    {
      get
      {
        return (bool) this.GetValue(TimeTableHeaderElement.ClockBeforeTablesProperty1);
      }
      set
      {
        int num = (int) this.SetValue(TimeTableHeaderElement.ClockBeforeTablesProperty1, (object) value);
      }
    }

    public LightVisualElement HeaderElement
    {
      get
      {
        return this.headerElement;
      }
    }

    public LightVisualElement RightArrow
    {
      get
      {
        return this.rightArrow;
      }
    }

    public LightVisualElement LeftArrow
    {
      get
      {
        return this.leftArrow;
      }
    }

    public virtual bool AmPmMode
    {
      get
      {
        return this.spinMode;
      }
      set
      {
        this.spinMode = value;
        int num = (int) this.SetValue(TimeTableHeaderElement.SpinModeProperty, (object) value);
        this.SetArrowButtonsVisibility();
      }
    }

    public virtual void SwitchAmToPm(CultureInfo culture)
    {
      this.headerElement.Text = !(this.headerElement.Text == culture.DateTimeFormat.AMDesignator) ? culture.DateTimeFormat.AMDesignator : culture.DateTimeFormat.PMDesignator;
    }

    public virtual void SetHeaderElementText(DateTime time, CultureInfo culture)
    {
      string amDesignator = culture.DateTimeFormat.AMDesignator;
      string pmDesignator = culture.DateTimeFormat.PMDesignator;
      if (time.Hour >= 12)
        this.headerElement.Text = pmDesignator;
      else
        this.headerElement.Text = amDesignator;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != TimeTableHeaderElement.SpinModeProperty)
        return;
      this.SetArrowButtonsVisibility();
    }

    protected virtual void SetArrowButtonsVisibility()
    {
      if (this.AmPmMode)
        this.leftArrow.Visibility = this.rightArrow.Visibility = ElementVisibility.Visible;
      else
        this.leftArrow.Visibility = this.rightArrow.Visibility = ElementVisibility.Collapsed;
    }
  }
}
