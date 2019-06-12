// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageViewPage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.BackstageViewPageDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class BackstageViewPage : Panel
  {
    private BackstageTabItem item;

    public BackstageViewPage()
    {
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
      this.BackColor = Color.Transparent;
      this.Visible = false;
    }

    public BackstageTabItem Item
    {
      get
      {
        return this.item;
      }
      internal set
      {
        this.item = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = DockStyle.None;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AnchorStyles Anchor
    {
      get
      {
        return base.Anchor;
      }
      set
      {
        base.Anchor = value;
      }
    }
  }
}
