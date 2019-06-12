// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadScreenTip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls
{
  [Designer("Telerik.WinControls.UI.Design.RadControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Designer("Telerik.WinControls.UI.Design.RadScreenTipDocumentDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (IRootDesigner))]
  public abstract class RadScreenTip : RadControl, IScreenTipContent
  {
    static RadScreenTip()
    {
      ThemeResolutionService.RegisterThemeFromStorage(ThemeStorageType.Resource, "Telerik.WinControls.UI.Resources.ScreenTipThemes.Office2007Silver.xml");
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(210, 150));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public abstract RadScreenTipElement ScreenTipElement { get; }

    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Data")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.ScreenTipElement.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadItemReadOnlyCollection TipItems
    {
      get
      {
        return this.ScreenTipElement.TipItems;
      }
    }

    public virtual string Description
    {
      get
      {
        return this.ScreenTipElement.Description;
      }
      set
      {
        this.ScreenTipElement.Description = value;
      }
    }

    public virtual Size TipSize
    {
      get
      {
        return this.ScreenTipElement.TipSize;
      }
    }

    public abstract Type TemplateType { get; set; }
  }
}
