// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSplitButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [Description("Provides a menu-like interface within a button")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadSplitButton : RadDropDownButton
  {
    protected override RadDropDownButtonElement CreateButtonElement()
    {
      RadSplitButtonElement splitButtonElement = new RadSplitButtonElement();
      splitButtonElement.DefaultItemChanged += new EventHandler(this.SplitButtonElement_DefaultItemChanged);
      return (RadDropDownButtonElement) splitButtonElement;
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(110, 24));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadSplitButtonElement DropDownButtonElement
    {
      get
      {
        return (RadSplitButtonElement) base.DropDownButtonElement;
      }
    }

    [DefaultValue(null)]
    [Browsable(false)]
    public RadItem DefaultItem
    {
      get
      {
        return this.DropDownButtonElement.DefaultItem;
      }
      set
      {
        this.DropDownButtonElement.DefaultItem = value;
      }
    }

    [Description("Occurs when the default item is changed.")]
    [Browsable(true)]
    [Category("Action")]
    public event EventHandler DefaultItemChanged;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDefaultItemChanged(EventArgs e)
    {
      EventHandler defaultItemChanged = this.DefaultItemChanged;
      if (defaultItemChanged == null)
        return;
      defaultItemChanged((object) this, e);
    }

    private void SplitButtonElement_DefaultItemChanged(object sender, EventArgs e)
    {
      this.OnDefaultItemChanged(e);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      Type type = element.GetType();
      return (object) type == (object) typeof (RadSplitButtonElement) || (object) type == (object) typeof (RadButtonElement);
    }
  }
}
