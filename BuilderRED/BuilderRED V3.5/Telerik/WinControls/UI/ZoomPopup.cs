// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ZoomPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  [ToolboxItem(false)]
  public class ZoomPopup : RadControl, IMessageListener
  {
    private bool hasShadow = true;
    private int animationFrames = 4;
    private int animationInterval = 20;
    private bool waitForAnimationFinished = true;
    private bool animationFinished;
    private bool elementAutoSize;
    private SizeF zoomFactor;
    private bool clicked;
    private bool hideMenus;
    private RadElement element;
    private Rectangle initialBounds;
    private Rectangle elementScreenBounds;
    private Point elementLocation;
    private RadElement elementParent;
    private int elementIndex;
    private SizeF elementScaleTransform;
    private Padding elementMargin;
    private bool elementHandleMouseInput;
    public EventHandler Closed;
    public EventHandler Clicked;

    public ZoomPopup(RadElement element, SizeF zoomFactor)
      : this(element, zoomFactor, false)
    {
    }

    public ZoomPopup(RadElement element, SizeF zoomFactor, bool waitForAnimationFinished)
      : this(element, zoomFactor, Rectangle.Empty, waitForAnimationFinished)
    {
    }

    public ZoomPopup(
      RadElement element,
      SizeF zoomFactor,
      Rectangle initialBounds,
      bool waitForAnimationFinished)
    {
      this.Visible = false;
      this.element = element;
      this.zoomFactor = zoomFactor;
      this.waitForAnimationFinished = waitForAnimationFinished;
      this.initialBounds = initialBounds;
      this.CreateChildItems((RadElement) this.RootElement);
      element.InvalidateArrange(true);
      this.LoadElementTree();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      if (this.element == null || this.element.ElementTree == null)
        return;
      this.AutoSize = false;
      this.elementScreenBounds = this.element.ElementTree.Control.RectangleToScreen(this.element.ControlBoundingRectangle);
      if (this.initialBounds == Rectangle.Empty)
        this.initialBounds = this.elementScreenBounds;
      this.elementLocation = this.element.Location;
      this.elementParent = this.element.Parent;
      this.elementIndex = this.elementParent.Children.IndexOf(this.element);
      this.elementScaleTransform = this.element.ScaleTransform;
      this.elementMargin = this.element.Margin;
      this.elementAutoSize = this.element.AutoSize;
      this.elementHandleMouseInput = this.element.ShouldHandleMouseInput;
      Point screen = this.element.PointToScreen(new Point(0, 0));
      Rectangle rectangle = new Rectangle(screen.X + this.Size.Width / 2, screen.Y + this.Size.Height / 2, 1, 1);
      string str = this.element.ElementTree.ComponentTreeHandler.ThemeName;
      if (string.IsNullOrEmpty(str))
        str = "ControlDefault";
      this.ThemeName = str;
      this.ThemeClassName = this.element.ElementTree.ComponentTreeHandler.ThemeClassName;
      this.elementParent.SuspendLayout();
      this.elementParent.Children.RemoveAt(this.elementIndex);
      this.element.Margin = new Padding(0);
      this.element.Location = new Point(0, 0);
      this.element.AutoSize = true;
      this.element.ShouldHandleMouseInput = false;
      this.element.IsMouseOver = false;
      this.Bounds = rectangle;
      parent.Children.Add(this.element);
      this.element.RadPropertyChanged += new RadPropertyChangedEventHandler(this.element_RadPropertyChanged);
      if (!(this.element is RadItem))
        return;
      (this.element as RadItem).Click += new EventHandler(this.ZoomPopup_Click);
    }

    [Browsable(true)]
    [Description("Gets or sets the zoom popup shadow")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool HasShadow
    {
      get
      {
        return this.hasShadow;
      }
      set
      {
        this.hasShadow = value;
      }
    }

    [DefaultValue(4)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the animation frames count")]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        this.animationFrames = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(20)]
    [Category("Appearance")]
    [Description("Gets or sets the animation interval (in miliseconds)")]
    public int AnimationInterval
    {
      get
      {
        return this.animationInterval;
      }
      set
      {
        this.animationInterval = value;
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        if ((Environment.OSVersion.Version.Major > 5 || Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) && this.hasShadow)
          createParams.ClassStyle |= 131072;
        createParams.Style &= -79691777;
        createParams.ExStyle &= -262145;
        createParams.Style |= int.MinValue;
        createParams.ExStyle |= 65544;
        createParams.ClassStyle |= 2048;
        return createParams;
      }
    }

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      if (value || !this.IsLoaded)
        return;
      RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
      RadModalFilter.Instance.MenuHierarchyClosing -= new EventHandler(this.Instance_MenuHierarchyClosing);
      this.ReparentElement();
      if (this.Closed != null)
        this.Closed((object) this, EventArgs.Empty);
      if (this.clicked && this.Clicked != null)
        this.Clicked((object) this, EventArgs.Empty);
      if (!this.hideMenus)
        return;
      RadModalFilter.Instance.UnRegisterAllMenus();
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 28:
          this.Hide();
          break;
        case 33:
          m.Result = (IntPtr) 3;
          return;
        case 513:
        case 514:
        case 515:
        case 516:
        case 517:
        case 518:
        case 519:
        case 520:
        case 521:
          if (!this.animationFinished)
            return;
          break;
      }
      base.WndProc(ref m);
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    protected override bool ProcessCaptureChangeRequested(RadElement element, bool capture)
    {
      return false;
    }

    public new void Show()
    {
      Telerik.WinControls.NativeMethods.ShowWindow(this.Handle, 4);
      ControlHelper.BringToFront(this.Handle, false);
      this.ShowCore();
    }

    private void ShowCore()
    {
      this.hideMenus = false;
      this.animationFinished = false;
      RadMessageFilter.Instance.AddListener((IMessageListener) this);
      RadModalFilter.Instance.MenuHierarchyClosing += new EventHandler(this.Instance_MenuHierarchyClosing);
      SizeF scaleTransform = this.element.ScaleTransform;
      AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) scaleTransform, (object) this.zoomFactor, this.animationFrames, this.animationInterval);
      animatedPropertySetting.ApplyValue((RadObject) this.element);
      animatedPropertySetting.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
      {
        this.animationFinished = true;
        this.element.ShouldHandleMouseInput = this.elementHandleMouseInput;
        this.element.IsMouseOver = true;
        this.ApplyElementShape();
      });
    }

    private void ApplyElementShape()
    {
      if (this.element == null || this.element.Shape == null)
        return;
      using (GraphicsPath path = this.element.Shape.CreatePath(this.ClientRectangle))
        this.Region = new Region(path);
    }

    private void ReparentElement()
    {
      if (this.elementParent == null || !this.elementParent.IsInValidState(false))
        return;
      this.element.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.element_RadPropertyChanged);
      if (this.element is RadItem)
        (this.element as RadItem).Click -= new EventHandler(this.ZoomPopup_Click);
      int num = (int) this.element.ResetValue(RadElement.ScaleTransformProperty, ValueResetFlags.Animation);
      this.element.AutoSize = this.elementAutoSize;
      this.element.Margin = this.elementMargin;
      this.element.Location = this.elementLocation;
      this.element.ScaleTransform = this.elementScaleTransform;
      this.element.IsMouseOver = false;
      this.element.ShouldHandleMouseInput = this.elementHandleMouseInput;
      if (this.element.Parent != this.elementParent)
        this.elementParent.Children.Insert(this.elementIndex, this.element);
      this.elementParent.ResumeLayout(true);
      this.elementParent = (RadElement) null;
    }

    private void UpdateSize(SizeF scale)
    {
      this.element.InvalidateTransformations();
      this.Size = new Size((int) ((double) ((float) this.initialBounds.Width * scale.Width) + 0.5), (int) ((double) ((float) this.initialBounds.Height * scale.Height) + 0.5));
      this.Location = new Point(this.initialBounds.X - (this.Width - this.initialBounds.Size.Width) / 2, this.initialBounds.Y - (this.Height - this.initialBounds.Size.Height) / 2);
      this.ApplyElementShape();
    }

    private void element_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.ScaleTransformProperty || !this.IsHandleCreated)
        return;
      this.RootElement.UpdateLayout();
      this.UpdateSize((SizeF) e.NewValue);
    }

    private void ZoomPopup_Click(object sender, EventArgs e)
    {
      this.clicked = true;
      this.Hide();
    }

    private void Instance_MenuHierarchyClosing(object sender, EventArgs e)
    {
      this.hideMenus = true;
    }

    InstalledHook IMessageListener.DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage;
      }
    }

    MessagePreviewResult IMessageListener.PreviewMessage(
      ref Message msg)
    {
      if (msg.Msg == 512)
      {
        Point mousePosition = Control.MousePosition;
        if (this.animationFinished ? !this.initialBounds.Contains(mousePosition) : !this.elementScreenBounds.Contains(mousePosition))
        {
          this.Hide();
          return MessagePreviewResult.Processed;
        }
      }
      return MessagePreviewResult.NotProcessed;
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
      throw new NotImplementedException();
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
      throw new NotImplementedException();
    }
  }
}
