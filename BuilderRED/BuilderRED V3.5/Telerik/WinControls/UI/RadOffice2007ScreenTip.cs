// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadOffice2007ScreenTip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadOffice2007ScreenTip : RadScreenTip
  {
    private RadOffice2007ScreenTipElement tipElement;

    public override Type TemplateType
    {
      get
      {
        return typeof (RadOffice2007ScreenTipElement);
      }
      set
      {
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public override RadScreenTipElement ScreenTipElement
    {
      get
      {
        return (RadScreenTipElement) this.tipElement;
      }
    }

    public bool CaptionVisible
    {
      get
      {
        return this.tipElement.CaptionVisible;
      }
      set
      {
        this.tipElement.CaptionVisible = value;
      }
    }

    public bool FooterVisible
    {
      get
      {
        return this.tipElement.FooterVisible;
      }
      set
      {
        this.tipElement.FooterVisible = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.tipElement = new RadOffice2007ScreenTipElement();
      this.RootElement.Children.Add((RadElement) this.tipElement);
    }
  }
}
