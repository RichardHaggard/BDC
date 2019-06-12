// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRating
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadRatingDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadRating : RadControl
  {
    private RadRatingElement ratingElement;

    public RadRating()
    {
      this.SetStyle(ControlStyles.Selectable, true);
      this.WireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected virtual void WireEvents()
    {
      if (this.ratingElement == null)
        return;
      this.ratingElement.ValueChanging += new ValueChangingEventHandler(this.ratingElement_ValueChanging);
      this.ratingElement.ValueChanged += new EventHandler(this.ratingElement_ValueChanged);
    }

    protected virtual void UnwireEvents()
    {
      if (this.ratingElement == null)
        return;
      this.ratingElement.ValueChanging -= new ValueChangingEventHandler(this.ratingElement_ValueChanging);
      this.ratingElement.ValueChanged -= new EventHandler(this.ratingElement_ValueChanged);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.ratingElement = this.GetRatingElement();
      this.RootElement.Children.Add((RadElement) this.ratingElement);
    }

    protected virtual RadRatingElement GetRatingElement()
    {
      return new RadRatingElement();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(249, 56));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(RatingDirection.Standard)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    public virtual RatingDirection Direction
    {
      get
      {
        return this.RatingElement.Direction;
      }
      set
      {
        this.RatingElement.Direction = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(Orientation.Horizontal)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    public Orientation Orientation
    {
      get
      {
        return this.RatingElement.ElementOrientation;
      }
      set
      {
        if (this.RatingElement.ElementOrientation == value)
          return;
        this.RatingElement.ElementOrientation = value;
      }
    }

    [DefaultValue(RatingSelectionMode.Precise)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    public RatingSelectionMode SelectionMode
    {
      get
      {
        return this.RatingElement.SelectionMode;
      }
      set
      {
        this.RatingElement.SelectionMode = value;
      }
    }

    [Bindable(true)]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.RatingElement.Items;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double? Value
    {
      get
      {
        return this.ratingElement.Value;
      }
      set
      {
        this.ratingElement.Value = value;
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0.0)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    public double Minimum
    {
      get
      {
        return this.ratingElement.Minimum;
      }
      set
      {
        this.ratingElement.Minimum = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(100.0)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    public double Maximum
    {
      get
      {
        return this.ratingElement.Maximum;
      }
      set
      {
        this.ratingElement.Maximum = value;
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    public string Caption
    {
      get
      {
        return this.ratingElement.Caption;
      }
      set
      {
        this.ratingElement.Caption = value;
      }
    }

    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    public string Subcaption
    {
      get
      {
        return this.ratingElement.SubCaption;
      }
      set
      {
        this.ratingElement.SubCaption = value;
      }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue("")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Description
    {
      get
      {
        return this.ratingElement.Description;
      }
      set
      {
        this.ratingElement.Description = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRatingElement RatingElement
    {
      get
      {
        return this.ratingElement;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public bool ReadOnly
    {
      get
      {
        return this.ratingElement.ReadOnly;
      }
      set
      {
        this.ratingElement.ReadOnly = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    [System.ComponentModel.Description("Occurs when the value of the rating has been changed.")]
    [Browsable(true)]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Category("Action")]
    [System.ComponentModel.Description("Occurs when the value of the rating is changing.")]
    public event ValueChangingEventHandler ValueChanging;

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    private void ratingElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged(e);
    }

    private void ratingElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if ((object) element.GetType() == (object) typeof (RadRatingElement))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }
  }
}
