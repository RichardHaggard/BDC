// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTimePickerContent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadTimePickerContent : RadControl, IPickerContentElementOwner
  {
    private CultureInfo culture;
    private RadTimePickerContentElement timePickerContentElement;
    private string format;
    private object value;

    public RadTimePickerContent()
    {
      this.RootElement.StretchHorizontally = true;
      this.RootElement.StretchVertically = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.timePickerContentElement = this.CreateTimePickerContentElement();
      this.timePickerContentElement.HideClock();
      this.timePickerContentElement.FooterPanel.Visibility = ElementVisibility.Collapsed;
      this.timePickerContentElement.Margin = new Padding(-1, 0, 0, 0);
      parent.Children.Add((RadElement) this.timePickerContentElement);
    }

    protected virtual RadTimePickerContentElement CreateTimePickerContentElement()
    {
      return new RadTimePickerContentElement((IPickerContentElementOwner) this);
    }

    public CultureInfo Culture
    {
      get
      {
        if (this.culture == null)
          this.culture = CultureInfo.CurrentCulture;
        return this.culture;
      }
      set
      {
        this.culture = value;
      }
    }

    [DefaultValue(true)]
    [Description("Determines whether control's height will be determines automatically, depending on the current Font.")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [Browsable(false)]
    public RadTimePickerContentElement TimePickerElement
    {
      get
      {
        return this.timePickerContentElement;
      }
    }

    public object Value
    {
      get
      {
        DateTime result = DateTime.Now;
        if (this.value == null)
          this.value = (object) result;
        if (DateTime.TryParse(this.value.ToString(), out result))
          return (object) result;
        return (object) result;
      }
      set
      {
        if (value != null && this.value != null && (value is DateTime && this.value is DateTime) && ((DateTime) value).TimeOfDay == ((DateTime) this.value).TimeOfDay)
          return;
        this.value = value;
        if (value != null)
          this.timePickerContentElement.SetClockTime((DateTime) value);
        else
          this.timePickerContentElement.SetClockTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
        this.OnValueChanged((object) this, new EventArgs());
      }
    }

    public string Format
    {
      get
      {
        return this.format;
      }
      set
      {
        this.format = value;
      }
    }

    [Localizable(true)]
    [DefaultValue("Minutes")]
    public virtual string MinutesHeaderText
    {
      get
      {
        return LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString(nameof (MinutesHeaderText));
      }
    }

    [Localizable(true)]
    [DefaultValue("Hours")]
    public string HourHeaderText
    {
      get
      {
        return LocalizationProvider<RadTimePickerLocalizationProvider>.CurrentProvider.GetLocalizedString(nameof (HourHeaderText));
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the contents of the time picker control can be changed.")]
    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.timePickerContentElement.ReadOnly;
      }
      set
      {
        if (this.timePickerContentElement.ReadOnly == value)
          return;
        this.timePickerContentElement.ReadOnly = value;
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    [Category("Action")]
    [Description("Occurs when the editing value has been changed")]
    public event EventHandler ValueChanged;

    [Description(" Occurs when the editing value is changing.")]
    [Category("Action")]
    public event CancelEventHandler ValueChanging;

    public event EventHandler CloseButtonClicked;

    protected virtual void OnValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected virtual void OnValueChanging(object sender, CancelEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnCloseButtonClicked(object sender, EventArgs e)
    {
      if (this.CloseButtonClicked == null)
        return;
      this.CloseButtonClicked((object) this, e);
    }

    public void CloseOwnerPopup()
    {
      this.OnCloseButtonClicked((object) this, new EventArgs());
    }
  }
}
