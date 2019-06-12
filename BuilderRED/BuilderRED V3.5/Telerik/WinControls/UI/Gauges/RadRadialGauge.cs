// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadRadialGauge
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.Licensing;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI.Gauges
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadRadialGaugeDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("The RadRadialGauge control is designed to display a simple value within a definite range")]
  public class RadRadialGauge : RadControl
  {
    private const double DefaultZeroValue = 0.0;
    private const float DefaultValue = 90f;
    private const double DefaultRangeEnd = 100.0;
    private const double DefaultSweepAngle = 300.0;
    private const double DefaultStartAngle = 120.0;
    private RadRadialGaugeElement gaugeElement;
    private ComponentXmlSerializationInfo xmlSerializationInfo;

    public RadRadialGauge()
    {
      this.ForeColor = Color.Black;
    }

    [Description("The ValueChanged event fires when the value is modified.")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.GaugeElement.ValueChanged += value;
      }
      remove
      {
        this.GaugeElement.ValueChanged -= value;
      }
    }

    [System.ComponentModel.DefaultValue(0.0)]
    [Description("Controls the RadRadialGauge's offset in vertical direction.")]
    public double CenterOffsetY
    {
      get
      {
        return this.gaugeElement.CenterOffsetY;
      }
      set
      {
        this.gaugeElement.CenterOffsetY = value;
        this.OnNotifyPropertyChanged(nameof (CenterOffsetY));
      }
    }

    [Description("Controls the RadRadialGauge's offset in horizontal  direction.")]
    [System.ComponentModel.DefaultValue(0.0)]
    public double CenterOffsetX
    {
      get
      {
        return this.gaugeElement.CenterOffsetX;
      }
      set
      {
        this.gaugeElement.CenterOffsetX = value;
        this.OnNotifyPropertyChanged(nameof (CenterOffsetX));
      }
    }

    [RadEditItemsAction]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.gaugeElement.Items;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRadialGaugeElement GaugeElement
    {
      get
      {
        return this.gaugeElement;
      }
      set
      {
        this.gaugeElement = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(200, 200);
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.gaugeElement = this.CreateGaugeElement();
      parent.Children.Add((RadElement) this.gaugeElement);
    }

    protected virtual RadRadialGaugeElement CreateGaugeElement()
    {
      return new RadRadialGaugeElement();
    }

    [System.ComponentModel.DefaultValue(90f)]
    [Description("Specifies the gauge's value")]
    [Bindable(true)]
    public float Value
    {
      get
      {
        return this.gaugeElement.Value;
      }
      set
      {
        if ((double) this.gaugeElement.Value == (double) value)
          return;
        this.gaugeElement.Value = value;
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [Description("Specifies the gauge's end.")]
    [System.ComponentModel.DefaultValue(100.0)]
    public double RangeEnd
    {
      get
      {
        return this.gaugeElement.RangeEnd;
      }
      set
      {
        this.gaugeElement.RangeEnd = value;
      }
    }

    [System.ComponentModel.DefaultValue(0.0)]
    [Description("Specifies the gauge's start.")]
    public double RangeStart
    {
      get
      {
        return this.gaugeElement.RangeStart;
      }
      set
      {
        this.gaugeElement.RangeStart = value;
      }
    }

    [System.ComponentModel.DefaultValue(300.0)]
    [Description("Determines the angle value starting from the StartAngle to draw an arc in clockwise direction.")]
    public double SweepAngle
    {
      get
      {
        return this.gaugeElement.SweepAngle;
      }
      set
      {
        this.gaugeElement.SweepAngle = value;
      }
    }

    [System.ComponentModel.DefaultValue(120.0)]
    [Description("Determines the angle value starting from the StartAngle to draw an arc in clockwise direction.")]
    public double StartAngle
    {
      get
      {
        return this.gaugeElement.StartAngle;
      }
      set
      {
        this.gaugeElement.StartAngle = value;
      }
    }

    [Description("Gets or sets the ForeColor of the control. This is actually the ForeColor property of the root element.")]
    [System.ComponentModel.DefaultValue(typeof (Color), "Black")]
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

    public virtual ComponentXmlSerializationInfo GetDefaultXmlSerializationInfo()
    {
      return new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection() { { typeof (RadRadialGauge), "Name", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadRadialGauge), "Visible", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "ThemeName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadRadialGauge), "Item", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Style", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Shape", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Tag", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "RootElement", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "AccessibleDescription", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Text", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "AccessibleName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Location", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Dock", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Anchor", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } } });
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ComponentXmlSerializationInfo XmlSerializationInfo
    {
      get
      {
        if (this.xmlSerializationInfo == null)
          this.xmlSerializationInfo = this.GetDefaultXmlSerializationInfo();
        return this.xmlSerializationInfo;
      }
      set
      {
        this.xmlSerializationInfo = value;
      }
    }

    public virtual void SaveLayout(XmlWriter xmlWriter)
    {
      ComponentXmlSerializer componentXmlSerializer = new ComponentXmlSerializer(this.XmlSerializationInfo);
      xmlWriter.WriteStartElement(nameof (RadRadialGauge));
      componentXmlSerializer.WriteObjectElement(xmlWriter, (object) this);
      xmlWriter.WriteEndElement();
    }

    public virtual void SaveLayout(Stream stream)
    {
      ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
      StreamWriter streamWriter = new StreamWriter(stream);
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) streamWriter);
      xmlTextWriter.WriteStartElement(nameof (RadRadialGauge));
      controlXmlSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      xmlTextWriter.WriteEndElement();
      streamWriter.Flush();
    }

    public virtual void SaveLayout(string fileName)
    {
      ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement(nameof (RadRadialGauge));
        controlXmlSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      }
    }

    public virtual void LoadLayout(XmlReader xmlReader)
    {
      ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
      xmlReader.Read();
      controlXmlSerializer.ReadObjectElement(xmlReader, (object) this);
    }

    public virtual void LoadLayout(XmlReader xmlReader, InstanceFactory factory)
    {
      this.ResetFields();
      ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
      controlXmlSerializer.InstanceFactory = factory;
      xmlReader.Read();
      this.CleanupComponents(factory);
      controlXmlSerializer.ReadObjectElement(xmlReader, (object) this);
    }

    private void CleanupComponents(InstanceFactory factory)
    {
      DesignTimeInstanceFactory timeInstanceFactory = factory as DesignTimeInstanceFactory;
      if (timeInstanceFactory != null)
      {
        using (DesignerTransaction transaction = timeInstanceFactory.DesignerHost.CreateTransaction(nameof (CleanupComponents)))
        {
          while (this.Items.Count > 0)
            timeInstanceFactory.DesignerHost.DestroyComponent((IComponent) this.Items[this.Items.Count - 1]);
          transaction.Commit();
        }
      }
      this.Items.Clear();
    }

    protected virtual void ResetFields()
    {
      this.StartAngle = 120.0;
      this.SweepAngle = 300.0;
      this.Value = 90f;
      this.RangeEnd = 100.0;
      this.RangeStart = 0.0;
    }

    public virtual void LoadLayout(string fileName)
    {
      if (!File.Exists(fileName))
      {
        int num = (int) MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(fileName))
        {
          ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
          using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) streamReader))
            this.LoadLayout((XmlReader) xmlTextReader);
        }
      }
    }

    public virtual void LoadLayout(string fileName, DesignTimeInstanceFactory factory)
    {
      if (!File.Exists(fileName))
      {
        int num = (int) MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(fileName))
        {
          ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
          using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) streamReader))
            this.LoadLayout((XmlReader) xmlTextReader, (InstanceFactory) factory);
        }
      }
    }
  }
}
