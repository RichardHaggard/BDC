// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarFloatingForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CommandBarFloatingForm : RadForm
  {
    private Point dragOffset = Point.Empty;
    protected RadDropDownMenu dropDownMenuElement;
    private bool isWindowMoving;
    private Control parentControl;
    protected RadCommandBarOverflowPanelHostContol hostControl;
    private CommandBarStripElement stripElement;
    private CommandBarStripInfoHolder stripInfoHolder;
    protected RadImageButtonElement customizeMenuButtonElement;
    private IContainer components;

    public CommandBarFloatingForm()
    {
      this.InitializeComponent();
      this.HorizontalScroll.Visible = false;
      this.VerticalScroll.Visible = false;
      this.FormBehavior = (FormControlBehavior) new CommandBarFloatingForm.CommandBarFloatingFormBehavior((IComponentTreeHandler) this);
      this.CreateHostControl();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.StartPosition = FormStartPosition.Manual;
      this.customizeMenuButtonElement = new RadImageButtonElement();
      this.customizeMenuButtonElement.ThemeRole = "CommandBarFloatingOverflowButton";
      this.customizeMenuButtonElement.Class = "CommandBarFloatingOverflowButton";
      this.customizeMenuButtonElement.Click += new EventHandler(this.customizeMenuButtonElement_Click);
      this.customizeMenuButtonElement.Visibility = ElementVisibility.Collapsed;
      this.FormElement.TitleBar.Children[2].Children[0].Children.Insert(2, (RadElement) this.customizeMenuButtonElement);
      this.FormElement.TitleBar.MouseDown += new MouseEventHandler(this.TitleBar_MouseDown);
      this.FormElement.VerticalScrollbar.Visibility = ElementVisibility.Collapsed;
      this.FormElement.HorizontalScrollbar.Visibility = ElementVisibility.Collapsed;
    }

    protected virtual void CreateHostControl()
    {
      this.hostControl = new RadCommandBarOverflowPanelHostContol();
      this.hostControl.Dock = DockStyle.Fill;
      this.hostControl.Element.Padding = new Padding(2, 2, 2, -2);
      this.hostControl.Element.MinSize = new Size(25, 25);
      this.hostControl.SizeChanged += new EventHandler(this.hostControl_SizeChanged);
      this.Controls.Add((Control) this.hostControl);
      this.PerformLayout();
    }

    public RadCommandBarOverflowPanelHostContol ItemsHostControl
    {
      get
      {
        return this.hostControl;
      }
    }

    public Control ParentControl
    {
      get
      {
        return this.parentControl;
      }
      set
      {
        this.parentControl = value;
        if (this.parentControl == null)
          return;
        this.BindingContext = this.parentControl.BindingContext;
      }
    }

    public CommandBarStripInfoHolder StripInfoHolder
    {
      get
      {
        return this.stripInfoHolder;
      }
    }

    public CommandBarStripElement StripElement
    {
      get
      {
        return this.stripElement;
      }
      set
      {
        if (value != null)
        {
          CommandBarRowElement parent = value.Parent as CommandBarRowElement;
          if (parent != null && parent.Owner != null)
          {
            RadControl control = parent.Owner.ElementTree.Control as RadControl;
            if (control != null)
            {
              this.ThemeName = control.ThemeName;
              this.hostControl.ImageList = control.ImageList;
            }
            this.hostControl.ThemeName = this.ThemeName;
            this.stripInfoHolder = parent.Owner.StripInfoHolder;
          }
        }
        else
          this.stripInfoHolder = (CommandBarStripInfoHolder) null;
        this.SetStripElementCore(value);
      }
    }

    private void SetStripElementCore(CommandBarStripElement value)
    {
      if (this.stripElement != null)
        this.stripElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.stripElement_RadPropertyChanged);
      this.stripElement = value;
      if (this.stripElement == null)
        return;
      this.stripElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.stripElement_RadPropertyChanged);
      this.stripElement.Items.Owner = (RadElement) this.hostControl.Element.Layout;
      while (this.stripElement.OverflowButton.ItemsLayout.Children.Count > 0)
      {
        RadElement child = this.stripElement.OverflowButton.ItemsLayout.Children[0];
        this.stripElement.OverflowButton.ItemsLayout.Children.Remove(child);
        this.hostControl.Element.Layout.Children.Add(child);
      }
    }

    private void hostControl_SizeChanged(object sender, EventArgs e)
    {
      this.Size = new Size(this.Width, this.hostControl.Element.Size.Height + this.FormBehavior.ClientMargin.Top + this.FormBehavior.ClientMargin.Bottom);
    }

    private void customizeMenuButtonElement_Click(object sender, EventArgs e)
    {
      this.StripElement.OverflowButton.RightToLeft = this.RightToLeft == RightToLeft.Yes;
      this.StripElement.OverflowButton.PopulateDropDownMenu();
      Point point = Point.Empty;
      if (this.RightToLeft == RightToLeft.Yes)
        point = new Point(this.customizeMenuButtonElement.Size.Width - this.stripElement.OverflowButton.DropDownMenu.PreferredSize.Width, 0);
      this.StripElement.OverflowButton.DropDownMenu.Show((RadItem) this.customizeMenuButtonElement, point, RadDirection.Left);
    }

    private void stripElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != CommandBarStripElement.VisibleInCommandBarProperty)
        return;
      this.Visible = this.stripElement.VisibleInCommandBar;
    }

    private void TitleBar_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.InitializeMove(e.Location);
    }

    internal void InitializeMove(Point offset)
    {
      this.isWindowMoving = true;
      this.Capture = true;
      this.dragOffset = new Point(-offset.X, -offset.Y);
    }

    internal void EndMove()
    {
      this.isWindowMoving = false;
      this.Capture = false;
    }

    public void TryDocking(RadCommandBar commandBar)
    {
      if (commandBar == null || commandBar.CommandBarElement.CallOnFloatingStripDocking((object) this.stripElement))
        return;
      this.Capture = false;
      this.isWindowMoving = false;
      this.stripElement.EnableDragging = true;
      this.stripElement.Capture = false;
      this.stripElement.ForceEndDrag();
      this.stripElement.Items.Owner = (RadElement) this.stripElement.ItemsLayout;
      bool enableFloating = this.stripElement.EnableFloating;
      this.stripElement.EnableFloating = false;
      while (this.hostControl.Element.Layout.Children.Count > 0)
      {
        RadElement child = this.hostControl.Element.Layout.Children[0];
        if (child is RadCommandBarBaseItem || !this.stripElement.Items.Contains(child as RadCommandBarBaseItem))
        {
          this.hostControl.Element.Layout.Children.Remove(child);
          this.stripElement.Items.Add(child as RadCommandBarBaseItem);
        }
      }
      this.stripElement.FloatingForm = (CommandBarFloatingForm) null;
      this.stripInfoHolder.RemoveStripInfo(this.stripElement);
      if (commandBar.Rows.Count == 0)
        commandBar.Rows.Add(new CommandBarRowElement());
      commandBar.Rows[0].Strips.Add(this.stripElement);
      this.stripElement.OverflowButton.HostControlThemeName = commandBar.ThemeName;
      this.stripElement.Capture = false;
      this.stripElement.ForceEndDrag();
      this.stripElement.ElementTree.Control.Capture = false;
      this.stripElement.CallDoMouseUp(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
      Point client = this.parentControl.PointToClient(this.Location);
      if (this.stripElement.RightToLeft)
        client.X = this.parentControl.Size.Width - client.X - this.Size.Width;
      this.stripElement.DesiredLocation = (PointF) client;
      this.stripElement.EnableFloating = enableFloating;
      commandBar.CommandBarElement.CallOnFloatingStripDocked((object) this.stripElement);
      this.StripElement = (CommandBarStripElement) null;
      this.Close();
    }

    public void TryDocking(Point screenLocation)
    {
      if (this.parentControl == null)
        return;
      this.TryDocking(this.ParentControl.GetChildAtPoint(this.parentControl.PointToClient(screenLocation)) as RadCommandBar);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 532)
      {
        Telerik.WinControls.NativeMethods.RECT structure = (Telerik.WinControls.NativeMethods.RECT) Marshal.PtrToStructure(m.LParam, typeof (Telerik.WinControls.NativeMethods.RECT));
        this.hostControl.Element.InvalidateMeasure(true);
        this.hostControl.Element.Measure((SizeF) this.ClientSize);
        int childMaxWidth = this.hostControl.Element.GetChildMaxWidth();
        structure.bottom = structure.top + this.hostControl.Element.Size.Height + this.FormBehavior.ClientMargin.Top + this.FormBehavior.ClientMargin.Bottom;
        if (structure.right - structure.left < childMaxWidth + this.hostControl.Element.Padding.Left + this.hostControl.Element.Padding.Right)
          structure.right = structure.left + childMaxWidth + this.hostControl.Element.Padding.Left + this.hostControl.Element.Padding.Right + this.FormBehavior.ClientMargin.Left + this.FormBehavior.ClientMargin.Right;
        Marshal.StructureToPtr((object) structure, m.LParam, true);
      }
      base.WndProc(ref m);
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 134217856;
        return createParams;
      }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);
      if (this.StripElement == null)
        return;
      e.Cancel = true;
      this.StripElement.VisibleInCommandBar = false;
      this.Visible = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button != MouseButtons.Left)
        return;
      if (this.isWindowMoving)
      {
        Point mousePosition = Control.MousePosition;
        mousePosition.Offset(this.dragOffset);
        this.Location = mousePosition;
        this.TryDocking(Control.MousePosition);
      }
      this.Invalidate(true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.BeginInit();
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(259, 159);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(20, 20);
      this.Name = nameof (CommandBarFloatingForm);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Text = nameof (CommandBarFloatingForm);
      this.TopMost = true;
      this.EndInit();
      this.ResumeLayout(false);
    }

    private class CommandBarFloatingFormBehavior : RadFormBehavior
    {
      public override Padding ClientMargin
      {
        get
        {
          return new Padding(2, this.GetDynamicCaptionHeight(), 2, 2);
        }
      }

      public CommandBarFloatingFormBehavior(IComponentTreeHandler treeHandler)
        : base(treeHandler, true)
      {
      }
    }
  }
}
