// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSizablePopupControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadSizablePopupControl : RadPopupControlBase
  {
    private SizeGripElement sizingGrip;
    private DockLayoutPanel sizingGripDockLayout;

    public RadSizablePopupControl(RadItem owner)
      : base((RadElement) owner)
    {
      this.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadSizablePopupControl).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    public SizingMode SizingMode
    {
      get
      {
        return this.sizingGrip.SizingMode;
      }
      set
      {
        this.sizingGrip.SizingMode = value;
      }
    }

    public SizeGripElement SizingGrip
    {
      get
      {
        return this.sizingGrip;
      }
    }

    public DockLayoutPanel SizingGripDockLayout
    {
      get
      {
        return this.sizingGripDockLayout;
      }
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element.GetType().Equals(typeof (SizeGripElement)))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.sizingGrip = new SizeGripElement();
      this.sizingGrip.SizingMode = SizingMode.None;
      this.sizingGrip.MinSize = new Size(0, 12);
      this.sizingGripDockLayout = new DockLayoutPanel();
      this.sizingGripDockLayout.Class = "PopupPanel";
      this.sizingGripDockLayout.Children.Add((RadElement) this.sizingGrip);
      this.sizingGripDockLayout.LastChildFill = true;
      DockLayoutPanel.SetDock((RadElement) this.sizingGrip, Dock.Bottom);
      parent.Children.Add((RadElement) this.sizingGripDockLayout);
    }
  }
}
