// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadScreenTipPlaceholder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls
{
  internal class RadScreenTipPlaceholder : RadScreenTip
  {
    private Type templateType;
    private RadScreenTipElement content;

    public RadScreenTipPlaceholder()
    {
      this.RootElement.ApplyShapeToControl = true;
    }

    public override Type TemplateType
    {
      get
      {
        return this.templateType;
      }
      set
      {
        this.templateType = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public override RadScreenTipElement ScreenTipElement
    {
      get
      {
        return this.content;
      }
    }

    public void SetScreenTipElement(RadScreenTipElement element)
    {
      if (element == null)
      {
        if (this.content != null && this.RootElement.Children.Contains((RadElement) this.content))
          this.RootElement.Children.Remove((RadElement) this.content);
        this.content = (RadScreenTipElement) null;
      }
      else
      {
        if (this.content == element || this.RootElement.Children.Contains((RadElement) element))
          return;
        if (this.content != null)
          this.RootElement.Children.Remove((RadElement) this.content);
        this.content = element;
        this.RootElement.Children.Add((RadElement) this.content);
      }
    }
  }
}
