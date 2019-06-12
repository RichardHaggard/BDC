// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarOverflowPanelHostContol
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadCommandBarOverflowPanelHostContol : RadControl
  {
    private RadCommandBarOverflowPanelElement element;

    [Description("Represent RadCommandBarOverflowPanelElement")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadCommandBarOverflowPanelElement Element
    {
      get
      {
        return this.element;
      }
      set
      {
        this.element = value;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadCommandBar).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.element = new RadCommandBarOverflowPanelElement();
      this.RootElement.Children.Add((RadElement) this.element);
      this.AutoSize = true;
      this.RootElement.StretchVertically = false;
      this.RootElement.StretchHorizontally = true;
    }

    public LayoutPanel LayoutPanel
    {
      get
      {
        return this.element.Layout;
      }
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (!(element is RadTextBoxElement) && !(element is RadDropDownListArrowButtonElement) && (!(element is RadDropDownListEditableAreaElement) && !(element is RadDropDownListElement)))
        return element is RadArrowButtonElement;
      return true;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
    }
  }
}
