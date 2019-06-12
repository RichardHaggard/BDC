// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Value")]
  [ToolboxItem(false)]
  [ComVisible(true)]
  [DefaultEvent("Scroll")]
  public class RadScrollBar : RadControl
  {
    private RadScrollBarElement scrollBar;
    private ScrollType scrollType;

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems((RadElement) this.scrollBar);
      this.scrollBar = this.CreateScrollBarElement();
      this.scrollBar.ScrollType = this.ScrollType;
      this.scrollBar.Scroll += new ScrollEventHandler(this.scrollBar_Scroll);
      this.scrollBar.ValueChanged += new EventHandler(this.scrollBar_ValueChanged);
      this.scrollBar.ScrollParameterChanged += new EventHandler(this.scrollBar_ScrollParameterChanged);
      this.RootElement.Children.Add((RadElement) this.scrollBar);
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadScrollbarRootRadElement();
    }

    protected virtual RadScrollBarElement CreateScrollBarElement()
    {
      return new RadScrollBarElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadScrollBarAccessibleObject(this, this.Name);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.scrollBar != null)
      {
        this.scrollBar.Scroll -= new ScrollEventHandler(this.scrollBar_Scroll);
        this.scrollBar.ValueChanged -= new EventHandler(this.scrollBar_ValueChanged);
        this.scrollBar.ScrollParameterChanged -= new EventHandler(this.scrollBar_ScrollParameterChanged);
      }
      base.Dispose(disposing);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(false)]
    [Category("Layout")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadScrollBar).FullName;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadScrollBarElement ScrollBarElement
    {
      get
      {
        return this.scrollBar;
      }
    }

    [Category("Behavior")]
    [RadDescription("ThumbLengthProportion", typeof (RadScrollBarElement))]
    [RadDefaultValue("ThumbLengthProportion", typeof (RadScrollBarElement))]
    public double ThumbLengthProportion
    {
      get
      {
        return this.scrollBar.ThumbLengthProportion;
      }
      set
      {
        this.scrollBar.ThumbLengthProportion = value;
      }
    }

    [RadDefaultValue("MinThumbLength", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    [RadDescription("MinThumbLength", typeof (RadScrollBarElement))]
    public int MinThumbLength
    {
      get
      {
        return this.scrollBar.MinThumbLength;
      }
      set
      {
        this.scrollBar.MinThumbLength = value;
      }
    }

    [Category("Behavior")]
    [RadDescription("ThumbLength", typeof (RadScrollBarElement))]
    public int ThumbLength
    {
      get
      {
        return this.scrollBar.ThumbLength;
      }
    }

    [RadDescription("Minimum", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    [RadDefaultValue("Minimum", typeof (RadScrollBarElement))]
    public int Minimum
    {
      get
      {
        return this.scrollBar.Minimum;
      }
      set
      {
        this.scrollBar.Minimum = value;
      }
    }

    [Category("Behavior")]
    [RadDescription("Maximum", typeof (RadScrollBarElement))]
    [RadDefaultValue("Maximum", typeof (RadScrollBarElement))]
    public int Maximum
    {
      get
      {
        return this.scrollBar.Maximum;
      }
      set
      {
        this.scrollBar.Maximum = value;
      }
    }

    [Category("Behavior")]
    [RadDescription("Value", typeof (RadScrollBarElement))]
    [RadDefaultValue("Value", typeof (RadScrollBarElement))]
    public int Value
    {
      get
      {
        return this.scrollBar.Value;
      }
      set
      {
        this.scrollBar.Value = value;
      }
    }

    [RadDescription("SmallChange", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    [RadDefaultValue("SmallChange", typeof (RadScrollBarElement))]
    public int SmallChange
    {
      get
      {
        return this.scrollBar.SmallChange;
      }
      set
      {
        this.scrollBar.SmallChange = value;
      }
    }

    [RadDefaultValue("LargeChange", typeof (RadScrollBarElement))]
    [RadDescription("LargeChange", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    public int LargeChange
    {
      get
      {
        return this.scrollBar.LargeChange;
      }
      set
      {
        this.scrollBar.LargeChange = value;
      }
    }

    [RadDescription("ScrollType", typeof (RadScrollBarElement))]
    [RadDefaultValue("ScrollType", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    public virtual ScrollType ScrollType
    {
      get
      {
        if (this.scrollBar != null)
          this.scrollType = this.scrollBar.ScrollType;
        return this.scrollType;
      }
      set
      {
        this.scrollType = value;
        if (this.scrollBar == null)
          return;
        this.scrollBar.ScrollType = value;
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

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [RadDescription("ValueChanged", typeof (RadScrollBarElement))]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Category("Property Changed")]
    [RadDescription("ScrollParameterChanged", typeof (RadScrollBarElement))]
    public event EventHandler ScrollParameterChanged;

    public void PerformSmallDecrement(int numSteps)
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformSmallDecrement(numSteps);
    }

    public void PerformSmallIncrement(int numSteps)
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformSmallIncrement(numSteps);
    }

    public void PerformLargeDecrement(int numSteps)
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformLargeDecrement(numSteps);
    }

    public void PerformLargeIncrement(int numSteps)
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformLargeIncrement(numSteps);
    }

    public void PerformFirst()
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformFirst();
    }

    public void PerformLast()
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformLast();
    }

    public void PerformScrollTo(Point position)
    {
      if (this.scrollBar == null)
        return;
      this.scrollBar.PerformScrollTo(position);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      return element is RadScrollBarElement;
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected virtual void OnScrollParameterChanged(EventArgs args)
    {
      if (this.ScrollParameterChanged == null)
        return;
      this.ScrollParameterChanged((object) this, args);
    }

    private void scrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged(e);
    }

    private void scrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      this.OnScroll(e);
    }

    private void scrollBar_ScrollParameterChanged(object sender, EventArgs e)
    {
      this.OnScrollParameterChanged(e);
    }
  }
}
