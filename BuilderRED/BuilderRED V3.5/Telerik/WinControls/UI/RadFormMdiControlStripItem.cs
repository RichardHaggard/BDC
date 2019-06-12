// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormMdiControlStripItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadFormMdiControlStripItem : RadItem
  {
    private RadButtonElement closeButton;
    private RadButtonElement minimizeButton;
    private RadButtonElement maximizeButton;
    private ImagePrimitive mdiFormIcon;
    private DockLayoutPanel layout;
    private StackLayoutPanel systemButtons;
    private FillPrimitive fill;
    private Form activeMDIForm;

    internal RadFormMdiControlStripItem()
    {
    }

    public Form ActiveMDIChild
    {
      get
      {
        return this.activeMDIForm;
      }
      set
      {
        this.activeMDIForm = value;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public RadButtonElement MinimizeButton
    {
      get
      {
        return this.minimizeButton;
      }
    }

    public RadButtonElement MaximizeButton
    {
      get
      {
        return this.maximizeButton;
      }
    }

    public RadButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    public ImagePrimitive MaximizedMdiIcon
    {
      get
      {
        return this.mdiFormIcon;
      }
    }

    public StackLayoutPanel SystemButtonsLayout
    {
      get
      {
        return this.systemButtons;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.fill = new FillPrimitive();
      this.fill.Class = "TitleFill";
      this.Children.Add((RadElement) this.fill);
      this.layout = new DockLayoutPanel();
      this.layout.StretchVertically = true;
      this.layout.StretchHorizontally = true;
      this.layout.LastChildFill = false;
      this.Children.Add((RadElement) this.layout);
      this.systemButtons = new StackLayoutPanel();
      this.systemButtons.ZIndex = 10;
      this.systemButtons.Alignment = ContentAlignment.MiddleRight;
      this.minimizeButton = (RadButtonElement) new RadImageButtonElement();
      this.minimizeButton.StretchHorizontally = false;
      this.minimizeButton.StretchVertically = false;
      this.minimizeButton.Class = "MinimizeButton";
      this.minimizeButton.ButtonFillElement.Name = "MinimizeButtonFill";
      this.minimizeButton.CanFocus = false;
      this.minimizeButton.ThemeRole = "MDIStripMinimizeButton";
      this.systemButtons.Children.Add((RadElement) this.minimizeButton);
      this.maximizeButton = (RadButtonElement) new RadImageButtonElement();
      this.maximizeButton.StretchHorizontally = false;
      this.maximizeButton.StretchVertically = false;
      this.maximizeButton.Class = "MaximizeButton";
      this.maximizeButton.ButtonFillElement.Name = "MaximizeButtonFill";
      this.maximizeButton.CanFocus = false;
      this.maximizeButton.ThemeRole = "MDIStripMaximizeButton";
      this.systemButtons.Children.Add((RadElement) this.maximizeButton);
      this.closeButton = (RadButtonElement) new RadImageButtonElement();
      this.closeButton.StretchHorizontally = false;
      this.closeButton.StretchVertically = false;
      this.closeButton.Class = "CloseButton";
      this.closeButton.ButtonFillElement.Name = "CloseButtonFill";
      this.closeButton.CanFocus = false;
      this.closeButton.ThemeRole = "MDIStripCloseButton";
      this.systemButtons.Children.Add((RadElement) this.closeButton);
      this.mdiFormIcon = new ImagePrimitive();
      this.mdiFormIcon.Class = "MdiStripIcon";
      this.mdiFormIcon.StretchHorizontally = false;
      this.mdiFormIcon.StretchVertically = false;
      this.mdiFormIcon.CanFocus = false;
      this.mdiFormIcon.Margin = new Padding(1, 1, 0, 0);
      this.mdiFormIcon.MaxSize = new Size(16, 16);
      this.mdiFormIcon.ImageLayout = ImageLayout.Stretch;
      this.layout.Children.Add((RadElement) this.mdiFormIcon);
      this.layout.Children.Add((RadElement) this.systemButtons);
      DockLayoutPanel.SetDock((RadElement) this.systemButtons, Dock.Right);
      DockLayoutPanel.SetDock((RadElement) this.mdiFormIcon, Dock.Left);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.VisibilityProperty || this.activeMDIForm == null)
        return;
      if (this.activeMDIForm.Icon != null && this.activeMDIForm.ShowIcon)
      {
        if (this.mdiFormIcon.Image != null)
        {
          this.mdiFormIcon.Image.Dispose();
          this.mdiFormIcon.Image = (Image) null;
        }
        this.mdiFormIcon.Image = (Image) new Icon(this.activeMDIForm.Icon, this.mdiFormIcon.ScaleSize).ToBitmap();
      }
      else
        this.mdiFormIcon.Image = (Image) null;
    }
  }
}
