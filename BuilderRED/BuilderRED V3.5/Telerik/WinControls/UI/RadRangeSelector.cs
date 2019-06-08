// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRangeSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.UI.RangeSelector.InterfacesAndEnum;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadRangeSelectorDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  public class RadRangeSelector : RadControl, ISupportInitialize
  {
    private RadRangeSelectorElement rangeSelectorElement;
    private RadControl associateControl;

    public RadRangeSelector()
    {
      this.WireEvents();
      this.ShowItemToolTips = false;
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      this.RootElement.StretchVertically = true;
      base.InitializeRootElement(rootElement);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.rangeSelectorElement = this.CreateRangeSelectorElement();
      this.RootElement.Children.Add((RadElement) this.rangeSelectorElement);
      base.CreateChildItems((RadElement) this.rangeSelectorElement);
    }

    protected virtual RadRangeSelectorElement CreateRangeSelectorElement()
    {
      return new RadRangeSelectorElement();
    }

    protected virtual void WireEvents()
    {
      if (this.rangeSelectorElement == null)
        return;
      this.RangeSelectorElement.ThumbLeftValueChanging += new ValueChangingEventHandler(this.rangeSelectorElement_ThumbLeftValueChanging);
      this.RangeSelectorElement.ThumbLeftValueChanged += new EventHandler(this.rangeSelectorElement_ThumbLeftValueChanged);
      this.RangeSelectorElement.ThumbRightValueChanging += new ValueChangingEventHandler(this.rangeSelectorElement_ThumbRightValueChanging);
      this.RangeSelectorElement.ThumbRightValueChanged += new EventHandler(this.rangeSelectorElement_ThumbRightValueChanged);
      this.RangeSelectorElement.SelectionChanging += new RangeSelectorSelectionChangingEventHandler(this.rangeSelectorElement_SelectionChanging);
      this.RangeSelectorElement.SelectionChanged += new EventHandler(this.rangeSelectorElement_SelectionChanged);
      this.RangeSelectorElement.ScaleInitializing += new ScaleInitializingEventHandler(this.rangeSelectorElement_ScaleInitializing);
    }

    protected virtual void UnwireEvents()
    {
      if (this.RangeSelectorElement == null)
        return;
      if (this.associateControl != null)
        this.associateControl.VisibleChanged -= new EventHandler(this.Value_VisibleChanged);
      this.RangeSelectorElement.ThumbLeftValueChanging -= new ValueChangingEventHandler(this.rangeSelectorElement_ThumbLeftValueChanging);
      this.RangeSelectorElement.ThumbLeftValueChanged -= new EventHandler(this.rangeSelectorElement_ThumbLeftValueChanged);
      this.RangeSelectorElement.ThumbRightValueChanging -= new ValueChangingEventHandler(this.rangeSelectorElement_ThumbRightValueChanging);
      this.RangeSelectorElement.ThumbRightValueChanged -= new EventHandler(this.rangeSelectorElement_ThumbRightValueChanged);
      this.RangeSelectorElement.SelectionChanging -= new RangeSelectorSelectionChangingEventHandler(this.rangeSelectorElement_SelectionChanging);
      this.RangeSelectorElement.SelectionChanged -= new EventHandler(this.rangeSelectorElement_SelectionChanged);
      this.RangeSelectorElement.ScaleInitializing -= new ScaleInitializingEventHandler(this.rangeSelectorElement_ScaleInitializing);
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets whether the edit control is auto-sized")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(400, 150));
      }
    }

    public RadRangeSelectorElement RangeSelectorElement
    {
      get
      {
        return this.rangeSelectorElement;
      }
    }

    [Browsable(false)]
    [Category("Behavior")]
    [Description("Gets or Sets the orientation of the TrackBar")]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
      get
      {
        return this.RangeSelectorElement.Orientation;
      }
      set
      {
        if (value == this.RangeSelectorElement.Orientation)
          return;
        this.RangeSelectorElement.SuspendLayout();
        this.RangeSelectorElement.Orientation = value;
        int width = this.Width;
        this.Width = this.Height;
        this.Height = width;
        this.RangeSelectorElement.ResumeLayout(true);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Orientation)));
      }
    }

    [Browsable(true)]
    [DefaultValue(30f)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the start of the selected range.")]
    public float StartRange
    {
      get
      {
        return this.RangeSelectorElement.StartRange;
      }
      set
      {
        this.RangeSelectorElement.StartRange = value;
      }
    }

    [DefaultValue(70f)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the end of the selected range.")]
    public float EndRange
    {
      get
      {
        return this.rangeSelectorElement.EndRange;
      }
      set
      {
        this.rangeSelectorElement.EndRange = value;
      }
    }

    [Description("Gets or sets the range selector view zoom start.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0.0f)]
    [Browsable(true)]
    public float RangeSelectorViewZoomStart
    {
      get
      {
        return this.RangeSelectorElement.RangeSelectorViewZoomStart;
      }
      set
      {
        this.RangeSelectorElement.RangeSelectorViewZoomStart = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DefaultValue(100f)]
    [Description("Gets or sets the range selector view zoom end.")]
    public float RangeSelectorViewZoomEnd
    {
      get
      {
        return this.RangeSelectorElement.RangeSelectorViewZoomEnd;
      }
      set
      {
        this.RangeSelectorElement.RangeSelectorViewZoomEnd = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or Sets whether the RangeSelector's handles should be drawn")]
    [DefaultValue(true)]
    public bool ShowButtons
    {
      get
      {
        return this.RangeSelectorElement.ShowButtons;
      }
      set
      {
        this.RangeSelectorElement.ShowButtons = value;
      }
    }

    [DefaultValue(UpdateMode.Immediate)]
    [Category("Behavior")]
    [Description("Gets or sets how the associated chart will be updated. Immediate, the chart will be updated while moving the thumb or the tracking element. Deferred, the chart will be updated upon releasing the thumb or the tracking element.")]
    [Browsable(true)]
    public UpdateMode UpdateMode
    {
      get
      {
        return this.RangeSelectorElement.UpdateMode;
      }
      set
      {
        this.RangeSelectorElement.UpdateMode = value;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DefaultValue(null)]
    public RadControl AssociatedControl
    {
      get
      {
        return this.associateControl;
      }
      set
      {
        if (value is RadRangeSelector)
          return;
        if (value == null)
        {
          this.associateControl.VisibleChanged -= new EventHandler(this.Value_VisibleChanged);
          this.associateControl = (RadControl) null;
          this.RangeSelectorElement.AssociatedElement = (RadElement) null;
        }
        else
        {
          if (value is IRangeSelectorControl)
          {
            RadElement viewElement = ((IRangeSelectorControl) value).GetViewElement();
            value.VisibleChanged += new EventHandler(this.Value_VisibleChanged);
            this.RangeSelectorElement.AssociatedElement = viewElement;
          }
          else
            this.RangeSelectorElement.AssociatedElement = (RadElement) value.RootElement;
          this.associateControl = value;
        }
      }
    }

    private void Value_VisibleChanged(object sender, EventArgs e)
    {
      this.rangeSelectorElement.InitializeElements();
      this.rangeSelectorElement.ResetLayout(true);
    }

    [DefaultValue(false)]
    public override bool ShowItemToolTips
    {
      get
      {
        return base.ShowItemToolTips;
      }
      set
      {
        base.ShowItemToolTips = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the value of left thumb is changing.")]
    public event ValueChangingEventHandler ThumbLeftValueChanging;

    [Category("Behavior")]
    [Description("Occurs when the value of left thumb is changed.")]
    public event EventHandler ThumbLeftValueChanged;

    [Category("Behavior")]
    [Description("Occurs when the value of right thumb is changing.")]
    public event ValueChangingEventHandler ThumbRightValueChanging;

    [Description("Occurs when the value of right thumb is changed.")]
    [Category("Behavior")]
    public event EventHandler ThumbRightValueChanged;

    [Description("Occurs when the whole selection of the controls is about to change.")]
    [Category("Behavior")]
    public event RangeSelectorSelectionChangingEventHandler SelectionChanging;

    [Description("Occurs when the whole selection of the controls is changed.")]
    [Category("Behavior")]
    public event EventHandler SelectionChanged;

    [Category("Behavior")]
    [Description("Occurs when scale of the controls is Initializing.")]
    public event ScaleInitializingEventHandler ScaleInitializing;

    public virtual void OnThumbLeftValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler leftValueChanging = this.ThumbLeftValueChanging;
      if (leftValueChanging == null)
        return;
      leftValueChanging((object) this, e);
    }

    public virtual void OnThumbLeftValueChanged(EventArgs e)
    {
      EventHandler leftValueChanged = this.ThumbLeftValueChanged;
      if (leftValueChanged != null)
        leftValueChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "RangeChanged");
    }

    public virtual void OnThumbRightValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler rightValueChanging = this.ThumbRightValueChanging;
      if (rightValueChanging == null)
        return;
      rightValueChanging((object) this, e);
    }

    public virtual void OnThumbRightValueChanged(EventArgs e)
    {
      EventHandler rightValueChanged = this.ThumbRightValueChanged;
      if (rightValueChanged != null)
        rightValueChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "RangeChanged");
    }

    public virtual void OnSelectionChanging(RangeSelectorSelectionChangingEventArgs e)
    {
      RangeSelectorSelectionChangingEventHandler selectionChanging = this.SelectionChanging;
      if (selectionChanging == null)
        return;
      selectionChanging((object) this, e);
    }

    public virtual void OnSelectionChanged(EventArgs e)
    {
      EventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged != null)
        selectionChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged");
    }

    public virtual void OnScaleInitializing(ScaleInitializingEventArgs e)
    {
      ScaleInitializingEventHandler scaleInitializing = this.ScaleInitializing;
      if (scaleInitializing == null)
        return;
      scaleInitializing((object) this, e);
    }

    private void rangeSelectorElement_ThumbLeftValueChanging(
      object sender,
      ValueChangingEventArgs e)
    {
      this.OnThumbLeftValueChanging(e);
    }

    private void rangeSelectorElement_ThumbLeftValueChanged(object sender, EventArgs e)
    {
      this.OnThumbLeftValueChanged(e);
    }

    private void rangeSelectorElement_ThumbRightValueChanging(
      object sender,
      ValueChangingEventArgs e)
    {
      this.OnThumbRightValueChanging(e);
    }

    private void rangeSelectorElement_ThumbRightValueChanged(object sender, EventArgs e)
    {
      this.OnThumbRightValueChanged(e);
    }

    private void rangeSelectorElement_SelectionChanging(
      object sender,
      RangeSelectorSelectionChangingEventArgs e)
    {
      this.OnSelectionChanging(e);
    }

    private void rangeSelectorElement_SelectionChanged(object sender, EventArgs e)
    {
      this.OnSelectionChanged(e);
    }

    private void rangeSelectorElement_ScaleInitializing(object sender, ScaleInitializingEventArgs e)
    {
      this.OnScaleInitializing(e);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.ElementTree.ApplyThemeToElementTree();
    }

    void ISupportInitialize.BeginInit()
    {
    }

    void ISupportInitialize.EndInit()
    {
      this.RangeSelectorElement.InitializeElements();
    }
  }
}
