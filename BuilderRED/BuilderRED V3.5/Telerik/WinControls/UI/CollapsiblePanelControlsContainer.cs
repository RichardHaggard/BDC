// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelControlsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class CollapsiblePanelControlsContainer : RadScrollablePanel
  {
    public void SynchronizeWithElement(RadElement element)
    {
      this.SynchronizeLocationWithElement(element);
      this.SynchronizeMarginWithElement(element);
      this.SynchronizeSizeWithElement(element);
    }

    public void SynchronizeSizeWithElement(RadElement element)
    {
      this.Size = element.BoundingRectangle.Size;
    }

    public void SynchronizeLocationWithElement(RadElement element)
    {
      this.Location = element.BoundingRectangle.Location;
    }

    public void SynchronizeMarginWithElement(RadElement element)
    {
      this.Margin = element.Margin;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Panel PanelContainer
    {
      get
      {
        return base.PanelContainer;
      }
    }

    public override DockStyle Dock
    {
      get
      {
        return DockStyle.None;
      }
      set
      {
        base.Dock = DockStyle.None;
      }
    }

    public void SuspendChildControlsLayout()
    {
      foreach (Control control in (ArrangedElementCollection) this.PanelContainer.Controls)
        control.SuspendLayout();
    }

    public void SuspendChildControlsLayoutWhereNotDocked()
    {
      foreach (Control control in (ArrangedElementCollection) this.PanelContainer.Controls)
      {
        if (control.Dock == DockStyle.None)
          control.SuspendLayout();
      }
    }

    public void ResumeChildControlsLayout(bool performLayout)
    {
      foreach (Control control in (ArrangedElementCollection) this.PanelContainer.Controls)
        control.ResumeLayout(performLayout);
    }

    public void ResumeChildControlsLayout()
    {
      this.ResumeChildControlsLayout(false);
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadScrollablePanel).FullName;
      }
      set
      {
      }
    }

    public virtual Bitmap ToBitmap()
    {
      Bitmap bitmap = new Bitmap(this.Width, this.Height);
      this.DrawToBitmap(bitmap, new Rectangle(Point.Empty, this.Size));
      return bitmap;
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new CollapsiblePanelControlsContainerControlsCollection((RadScrollablePanel) this);
    }

    protected override RadScrollablePanelContainer CreateScrollablePanelContainer()
    {
      return (RadScrollablePanelContainer) new CollapsiblePanelPanelContainer((RadScrollablePanel) this);
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
    }
  }
}
