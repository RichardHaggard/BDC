// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRadioButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [Description("Enables the user to select a single option from a group of choices when paired with other RadioButtons")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadRadioButton : RadToggleButton
  {
    internal bool firstFocus = true;

    public RadRadioButton()
    {
      this.TabStop = true;
      this.AutoSize = true;
    }

    protected override RadButtonElement CreateButtonElement()
    {
      return (RadButtonElement) new RadRadioButtonElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.ButtonElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.ButtonElement_RadPropertyChanged);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadRadioButtonAccessibleObject(this);
    }

    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [Browsable(true)]
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(110, 18));
      }
    }

    [DefaultValue(true)]
    public new bool TabStop
    {
      get
      {
        return base.TabStop;
      }
      set
      {
        base.TabStop = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRadioButtonElement ButtonElement
    {
      get
      {
        return (RadRadioButtonElement) base.ButtonElement;
      }
    }

    [RadPropertyDefaultValue("RadioCheckAlignment", typeof (RadRadioButtonElement))]
    [RadDescription("RadioCheckAlignment", typeof (RadRadioButtonElement))]
    public ContentAlignment RadioCheckAlignment
    {
      get
      {
        return this.ButtonElement.RadioCheckAlignment;
      }
      set
      {
        this.ButtonElement.RadioCheckAlignment = value;
      }
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    protected override void OnEnter(EventArgs e)
    {
      if (Control.MouseButtons == MouseButtons.None)
      {
        if (Telerik.WinControls.NativeMethods.GetKeyState(9) >= (short) 0)
        {
          this.ButtonElement.CallDoClick(EventArgs.Empty);
        }
        else
        {
          this.SetTabStops(true);
          this.TabStop = true;
        }
      }
      base.OnEnter(e);
    }

    private void ButtonElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      this.Invalidate();
    }

    private void ButtonElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Invalidate();
      if (!(e.PropertyName == "IsChecked"))
        return;
      this.OnNotifyPropertyChanged("IsChecked");
    }

    protected override void ButtonElement_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      this.OnToggleStateChanging(args);
    }

    protected override void ButtonElement_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.SetTabStops(false);
      this.OnToggleStateChanged((EventArgs) args);
      this.OnNotifyPropertyChanged("IsChecked");
    }

    protected override void res_CheckStateChanged(object sender, EventArgs e)
    {
      this.OnCheckStateChanged(e);
    }

    protected override void res_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
    {
      this.OnCheckStateChanging(args);
    }

    protected virtual void SetTabStops(bool tabPressed)
    {
      if (this.firstFocus && this.Parent != null)
      {
        int count = this.Parent.Controls.Count;
        for (int index = 0; index < count; ++index)
        {
          RadRadioButton control = this.Parent.Controls[index] as RadRadioButton;
          if (control != null)
          {
            if (!tabPressed)
              control.firstFocus = false;
            control.TabStop = false;
          }
        }
      }
      this.TabStop = this.IsChecked;
    }
  }
}
