// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.Keyboard;
using Telerik.WinControls.UI.Docking;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.SplitPanelDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class SplitPanel : RadControl
  {
    private static readonly object ControlTreeChangedEventKey = new object();
    private SplitPanelElement splitPanelElement;
    private Size desiredSize;
    private bool collapsed;
    private BorderStyle borderStyle;
    private SplitPanelSizeInfo sizeInfo;

    [Browsable(false)]
    public override ComponentThemableElementTree ElementTree
    {
      get
      {
        return base.ElementTree;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    public new bool EnableKeyMap
    {
      get
      {
        return base.EnableKeyMap;
      }
      set
      {
        base.EnableKeyMap = value;
      }
    }

    [Browsable(false)]
    [Category("Behavior")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new InputBindingsCollection CommandBindings
    {
      get
      {
        return base.CommandBindings;
      }
    }

    [Browsable(false)]
    public override string ThemeClassName
    {
      get
      {
        return base.ThemeClassName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    [Browsable(false)]
    public new Size ImageScalingSize
    {
      get
      {
        return base.ImageScalingSize;
      }
      set
      {
        base.ImageScalingSize = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override ImageList SmallImageList
    {
      get
      {
        return base.SmallImageList;
      }
      set
      {
        base.SmallImageList = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    [Browsable(false)]
    public new bool IsDesignMode
    {
      get
      {
        return this.DesignMode;
      }
    }

    public SplitPanel()
    {
      this.sizeInfo = new SplitPanelSizeInfo();
      this.sizeInfo.PropertyChanged += new PropertyChangedEventHandler(this.OnSizeInfo_PropertyChanged);
      this.TabStop = false;
      this.desiredSize = this.DefaultSize;
      base.MinimumSize = new Size(25, 25);
    }

    public event ControlTreeChangedEventHandler ControlTreeChanged
    {
      add
      {
        this.Events.AddHandler(SplitPanel.ControlTreeChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(SplitPanel.ControlTreeChangedEventKey, (Delegate) value);
      }
    }

    private void OnSizeInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.Parent == null)
        return;
      this.Parent.PerformLayout();
    }

    private bool ShouldSerializeLocation()
    {
      return false;
    }

    [DefaultValue(typeof (BorderStyle), "None")]
    public BorderStyle BorderStyle
    {
      get
      {
        return this.borderStyle;
      }
      set
      {
        if (this.borderStyle == value)
          return;
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (BorderStyle));
        this.borderStyle = value;
        this.UpdateStyles();
      }
    }

    [Description("Gets the object that encapsulates sizing information for this panel.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SplitPanelSizeInfo SizeInfo
    {
      get
      {
        return this.sizeInfo;
      }
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 65536;
        createParams.ExStyle &= -513;
        createParams.Style &= -8388609;
        switch (this.borderStyle)
        {
          case BorderStyle.FixedSingle:
            createParams.Style |= 8388608;
            return createParams;
          case BorderStyle.Fixed3D:
            createParams.ExStyle |= 512;
            return createParams;
          default:
            return createParams;
        }
      }
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Size MinimumSize
    {
      get
      {
        if (this.sizeInfo != null)
          return this.sizeInfo.MinimumSize;
        return new Size(25, 25);
      }
      set
      {
        base.MinimumSize = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MaximumSize
    {
      get
      {
        if (this.sizeInfo != null)
          return this.sizeInfo.MaximumSize;
        return base.MaximumSize;
      }
      set
      {
        base.MaximumSize = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 200));
      }
    }

    [Browsable(false)]
    public RadSplitContainer SplitContainer
    {
      get
      {
        return this.Parent as RadSplitContainer;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control Parent
    {
      get
      {
        return base.Parent;
      }
      set
      {
        base.Parent = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SplitPanelElement SplitPanelElement
    {
      get
      {
        return this.splitPanelElement;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      if (this.splitPanelElement == null)
        this.splitPanelElement = new SplitPanelElement();
      this.splitPanelElement.AutoSizeMode = this.AutoSize ? RadAutoSizeMode.WrapAroundChildren : RadAutoSizeMode.FitToAvailableSize;
      this.RootElement.Children.Add((RadElement) this.splitPanelElement);
      base.CreateChildItems(parent);
    }

    [DefaultValue(false)]
    public virtual bool Collapsed
    {
      get
      {
        return this.collapsed;
      }
      set
      {
        if (this.collapsed == value)
          return;
        this.collapsed = value;
        this.Visible = !this.collapsed;
        this.SplitContainer?.OnChildPanelCollapsedChanged(this);
      }
    }

    protected override Padding DefaultMargin
    {
      get
      {
        return new Padding(0, 0, 0, 0);
      }
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      this.OnControlTreeChanged(new ControlTreeChangedEventArgs((Control) this, e.Control, ControlTreeChangeAction.Add));
    }

    protected virtual void OnControlTreeChanged(ControlTreeChangedEventArgs args)
    {
      (this.Parent as SplitPanel)?.OnControlTreeChanged(args);
      ControlTreeChangedEventHandler changedEventHandler = this.Events[SplitPanel.ControlTreeChangedEventKey] as ControlTreeChangedEventHandler;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, args);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      this.OnControlTreeChanged(new ControlTreeChangedEventArgs((Control) this, e.Control, ControlTreeChangeAction.Remove));
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 160:
        case 512:
          if (this.CausesValidation && this.ValidationCancel)
          {
            this.DefWndProc(ref m);
            return;
          }
          break;
      }
      base.WndProc(ref m);
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
      this.SizeInfo.DpiScale = this.RootElement.DpiScaleFactor;
    }
  }
}
