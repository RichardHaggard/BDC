// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadLinearGauge
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
  [Description("The RadLinearGauge control is designed to display a simple value within a definite range")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadLinearGaugeDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadLinearGauge : RadControl
  {
    private SizeF currentFactor = new SizeF(1f, 1f);
    private const float DefaultZeroValue = 0.0f;
    private const float DefaultValue = 50f;
    private const float DefaultRangeEnd = 100f;
    private RadLinearGaugeElement gaugeElement;
    private ComponentXmlSerializationInfo xmlSerializationInfo;
    private bool vertical;

    public RadLinearGauge()
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

    [Description("The OrientationChanged event fires when the orientation of the gauges is changed.")]
    public event EventHandler OrientationChanged
    {
      add
      {
        this.GaugeElement.OrientationChanged += value;
      }
      remove
      {
        this.GaugeElement.OrientationChanged -= value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadLinearGaugeElement GaugeElement
    {
      get
      {
        return this.gaugeElement;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.gaugeElement = new RadLinearGaugeElement();
      parent.Children.Add((RadElement) this.gaugeElement);
    }

    protected virtual string RadLinearGaugeName
    {
      get
      {
        return this.GetType().Name;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [RadEditItemsAction]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.gaugeElement.Items;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        if (this.Vertical)
          return new Size(50, 280);
        return new Size(280, 50);
      }
    }

    [Description("Specifies the gauge's end.")]
    [System.ComponentModel.DefaultValue(100f)]
    public float RangeEnd
    {
      get
      {
        return this.gaugeElement.RangeEnd;
      }
      set
      {
        this.gaugeElement.RangeEnd = value;
        this.OnNotifyPropertyChanged(nameof (RangeEnd));
      }
    }

    [Description("Specifies the gauge's start.")]
    [System.ComponentModel.DefaultValue(0.0f)]
    public float RangeStart
    {
      get
      {
        return this.gaugeElement.RangeStart;
      }
      set
      {
        this.gaugeElement.RangeStart = value;
        this.OnNotifyPropertyChanged(nameof (RangeStart));
      }
    }

    [System.ComponentModel.DefaultValue(50f)]
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

    [System.ComponentModel.DefaultValue(false)]
    [Browsable(false)]
    [Description("Set or Get Gauge Orientation")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool Vertical
    {
      get
      {
        if (this.IsLoaded)
          return this.gaugeElement.Vertical;
        return this.vertical;
      }
      set
      {
        this.vertical = value;
        this.gaugeElement.Vertical = value;
        this.OnNotifyPropertyChanged(nameof (Vertical));
      }
    }

    [System.ComponentModel.DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the ForeColor of the control. This is actually the ForeColor property of the root element.")]
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
      return new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection() { { typeof (RadLinearGauge), "Name", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLinearGauge), "Visible", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "ThemeName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLinearGauge), "Item", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Style", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Shape", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Tag", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "RootElement", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "AccessibleDescription", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Text", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "AccessibleName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Location", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Dock", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Anchor", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Padding", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (RadLinearGauge), "Vertical", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } } });
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ComponentXmlSerializationInfo XmlSerializationInfo
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
      xmlWriter.WriteStartElement(this.RadLinearGaugeName);
      componentXmlSerializer.WriteObjectElement(xmlWriter, (object) this);
      xmlWriter.WriteEndElement();
    }

    public virtual void SaveLayout(Stream stream)
    {
      ControlXmlSerializer controlXmlSerializer = new ControlXmlSerializer(this.XmlSerializationInfo);
      StreamWriter streamWriter = new StreamWriter(stream);
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) streamWriter);
      xmlTextWriter.WriteStartElement(this.RadLinearGaugeName);
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
        xmlTextWriter.WriteStartElement(this.RadLinearGaugeName);
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
      this.Value = 50f;
      this.RangeEnd = 100f;
      this.RangeStart = 0.0f;
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

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      this.currentFactor = factor;
      base.ScaleControl(factor, specified);
      this.RootElement.DpiScaleChanged(new SizeF(1f / this.RootElement.DpiScaleFactor.Width, 1f / this.RootElement.DpiScaleFactor.Height));
      if ((double) factor.Width > 1.0 || (double) factor.Height > 1.0)
        this.RootElement.ScaleTransform = factor;
      else
        this.RootElement.ScaleTransform = new SizeF(1f, 1f);
    }

    protected override Size GetRootElementDesiredSize(int x, int y, int width, int height)
    {
      SizeF sizeF = new SizeF(1f, 1f);
      if ((double) this.currentFactor.Width > 1.0 || (double) this.currentFactor.Height > 1.0)
      {
        sizeF.Width /= this.currentFactor.Width;
        sizeF.Height /= this.currentFactor.Height;
      }
      this.isResizing2 = true;
      this.ElementTree.PerformInnerLayout(true, x, y, (int) Math.Round((double) width * (double) sizeF.Width), (int) Math.Round((double) height * (double) sizeF.Height));
      this.isResizing2 = false;
      return new Size(width, height);
    }
  }
}
