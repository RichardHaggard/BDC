// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadEditorControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [DesignerSerializer("Telerik.WinControls.UI.Design.RadControlCodeDomSerializer, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ToolboxItem(false)]
  [TypeDescriptionProvider(typeof (ReplaceRadControlProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadControlDesignerLite, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public abstract class RadEditorControl : RadControl
  {
    private bool isInTableLayoutPanel;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ImageList ImageList
    {
      get
      {
        return base.ImageList;
      }
      set
      {
        base.ImageList = value;
      }
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      this.isInTableLayoutPanel = this.Parent is TableLayoutPanel;
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
      Size preferredSize = base.GetPreferredSize(proposedSize);
      if (this.isInTableLayoutPanel && (preferredSize.Width <= 1 || preferredSize.Height <= 1))
        return base.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
      return preferredSize;
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.AutoSize && this.isInTableLayoutPanel && (this.RootElement.StretchHorizontally && width != this.Width))
      {
        Form form = this.FindForm();
        if (form != null && form.WindowState == FormWindowState.Minimized)
          width = this.Width;
      }
      base.SetBoundsCore(x, y, width, height, specified);
    }
  }
}
