// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.XmlTreeNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class XmlTreeNode : IXmlTreeSerializable
  {
    [XmlAttribute("AdditionalTextStartPosition")]
    [DefaultValue(-1)]
    public int AdditionalTextStartPosition = -1;
    [XmlAttribute("AdditionalTextEndPosition")]
    [DefaultValue(-1)]
    public int AdditionalTextEndPosition = -1;
    [XmlAttribute("Name")]
    [DefaultValue("")]
    public string Name = "";
    [XmlElement("Nodes")]
    public List<XmlTreeNode> Nodes = new List<XmlTreeNode>();
    [DefaultValue(true)]
    [XmlAttribute("AllowDrop")]
    public bool AllowDrop = true;
    [XmlIgnore]
    public Color ForeColor = Color.Empty;
    [XmlIgnore]
    public Color ForeColor2 = Color.Empty;
    [DefaultValue(true)]
    [XmlAttribute("ImageInFill")]
    public bool ImageInFill = true;
    [DefaultValue(-1)]
    [XmlAttribute("ImageIndex")]
    public int ImageIndex = -1;
    [DefaultValue(-1)]
    [XmlAttribute("RightImageIndex")]
    public int RightImageIndex = -1;
    [DefaultValue(-1)]
    [XmlAttribute("StateRightImageIndex")]
    public int StateRightImageIndex = -1;
    [DefaultValue(-1)]
    [XmlAttribute("StateImageIndex")]
    public int StateImageIndex = -1;
    [XmlAttribute("SelectedImageIndex")]
    [DefaultValue(-1)]
    public int SelectedImageIndex = -1;
    [XmlAttribute("ShowRightImage")]
    [DefaultValue(true)]
    public bool ShowRightImage = true;
    [DefaultValue(-1)]
    [XmlAttribute("SelectedRightImageIndex")]
    public int SelectedRightImageIndex = -1;
    [XmlAttribute("Visible")]
    [DefaultValue(true)]
    public bool Visible = true;
    [DefaultValue(-1)]
    [XmlAttribute("ItemHeight")]
    public int ItemHeight = -1;
    [XmlIgnore]
    public Color BackColor = Color.Empty;
    [XmlIgnore]
    public Color BackColor2 = Color.Empty;
    [XmlIgnore]
    public Color BackColor3 = Color.Empty;
    [XmlIgnore]
    public Color BackColor4 = Color.Empty;
    [XmlIgnore]
    public Color BorderColor = Color.Empty;
    [XmlAttribute("GradientAngle")]
    [DefaultValue(90f)]
    public float GradientAngle = 90f;
    [DefaultValue(0.5f)]
    [XmlAttribute("GradientPercentage")]
    public float GradientPercentage = 0.5f;
    [XmlAttribute("GradientPercentage2")]
    [DefaultValue(0.5f)]
    public float GradientPercentage2 = 0.5f;
    [DefaultValue(GradientStyles.Linear)]
    [XmlAttribute("GradientStyle")]
    public GradientStyles GradientStyle = GradientStyles.Linear;
    [DefaultValue(4)]
    [XmlAttribute("NumberOfColors")]
    public int NumberOfColors = 4;
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [XmlAttribute("TextAlignment")]
    public ContentAlignment TextAlignment = ContentAlignment.MiddleLeft;
    private static ColorConverter colorConverter;
    private static FontConverter fontConverter;
    [XmlAttribute("ImageKey")]
    [DefaultValue(null)]
    public string ImageKey;
    [XmlAttribute("RightImageKey")]
    [DefaultValue(null)]
    public string RightImageKey;
    [DefaultValue(null)]
    [XmlAttribute("SelectedImageKey")]
    public string SelectedImageKey;
    [XmlAttribute("SelectedRightImageKey")]
    [DefaultValue(null)]
    public string SelectedRightImageKey;
    [DefaultValue(null)]
    [XmlAttribute("StateImageKey")]
    public string StateImageKey;
    [XmlAttribute("StateRightImageKey")]
    [DefaultValue(null)]
    public string StateRightImageKey;
    [DefaultValue(false)]
    [XmlAttribute("Expanded")]
    public bool Expanded;
    [DefaultValue(null)]
    [XmlAttribute("Label")]
    public string Label;
    [DefaultValue(null)]
    [XmlAttribute("Text")]
    public string Text;
    [DefaultValue(ToggleState.Off)]
    [XmlAttribute("CheckState")]
    public ToggleState CheckState;
    [XmlAttribute("CheckType")]
    [DefaultValue(CheckType.None)]
    public CheckType CheckType;
    [XmlIgnore]
    public Font Font;
    [DefaultValue(null)]
    [XmlElement("Tag")]
    public object Tag;

    public static ColorConverter ColorConverter
    {
      get
      {
        if (XmlTreeNode.colorConverter == null)
          XmlTreeNode.colorConverter = new ColorConverter();
        return XmlTreeNode.colorConverter;
      }
    }

    public static FontConverter FontConverter
    {
      get
      {
        if (XmlTreeNode.fontConverter == null)
          XmlTreeNode.fontConverter = new FontConverter();
        return XmlTreeNode.fontConverter;
      }
    }

    public XmlTreeNode()
    {
    }

    public XmlTreeNode(RadTreeNode node)
    {
      this.Visible = node.Visible;
      this.Expanded = node.Expanded;
      this.Name = node.Name;
      this.ImageKey = node.ImageKey;
      this.ImageIndex = node.ImageIndex;
      this.ItemHeight = node.ItemHeight;
      this.Text = node.Text;
      this.Tag = node.Tag;
      this.CheckState = node.CheckState;
      this.CheckType = node.CheckType;
      this.ForeColor = node.ForeColor;
      this.BackColor = node.BackColor;
      this.BackColor2 = node.BackColor2;
      this.BackColor3 = node.BackColor3;
      this.BackColor4 = node.BackColor4;
      this.BorderColor = node.BorderColor;
      this.GradientAngle = node.GradientAngle;
      this.GradientPercentage = node.GradientPercentage;
      this.GradientPercentage2 = node.GradientPercentage2;
      this.GradientStyle = node.GradientStyle;
      this.NumberOfColors = node.NumberOfColors;
      if (node.HasStyle && node.Font != null)
        this.Font = node.Font;
      this.TextAlignment = node.TextAlignment;
      foreach (RadTreeNode node1 in (Collection<RadTreeNode>) node.Nodes)
        this.Nodes.Add(new XmlTreeNode(node1));
    }

    [DefaultValue("")]
    [XmlAttribute("ForeColor")]
    public string XmlForeColor
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.ForeColor);
      }
      set
      {
        this.ForeColor = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("ForeColor2")]
    [DefaultValue("")]
    public string XmlForeColor2
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.ForeColor2);
      }
      set
      {
        this.ForeColor2 = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("Font")]
    [DefaultValue("(none)")]
    public string XmlFont
    {
      get
      {
        return XmlTreeNode.FontConverter.ConvertToString((object) this.Font);
      }
      set
      {
        this.Font = (Font) XmlTreeNode.FontConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("BackColor")]
    [DefaultValue("")]
    public string XmlBackColor
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.BackColor);
      }
      set
      {
        this.BackColor = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("BackColor2")]
    [DefaultValue("")]
    public string XmlBackColor2
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.BackColor2);
      }
      set
      {
        this.BackColor2 = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("BackColor3")]
    [DefaultValue("")]
    public string XmlBackColor3
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.BackColor3);
      }
      set
      {
        this.BackColor3 = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("BackColor4")]
    [DefaultValue("")]
    public string XmlBackColor4
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.BackColor4);
      }
      set
      {
        this.BackColor4 = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    [XmlAttribute("BorderColor")]
    [DefaultValue("")]
    public string XmlBorderColor
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.BorderColor);
      }
      set
      {
        this.BorderColor = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    public RadTreeNode Deserialize()
    {
      RadTreeNode radTreeNode = new RadTreeNode();
      radTreeNode.Visible = this.Visible;
      radTreeNode.Expanded = this.Expanded;
      radTreeNode.Name = this.Name;
      radTreeNode.ImageKey = this.ImageKey;
      radTreeNode.ImageIndex = this.ImageIndex;
      radTreeNode.ItemHeight = this.ItemHeight;
      radTreeNode.Text = this.Text;
      radTreeNode.Font = this.Font;
      radTreeNode.Tag = this.Tag;
      radTreeNode.CheckType = this.CheckType;
      radTreeNode.CheckState = this.CheckState;
      radTreeNode.Style.ForeColor = this.ForeColor;
      radTreeNode.Style.BackColor = this.BackColor;
      radTreeNode.Style.BackColor2 = this.BackColor2;
      radTreeNode.Style.BackColor3 = this.BackColor3;
      radTreeNode.Style.BackColor4 = this.BackColor4;
      radTreeNode.Style.BorderColor = this.BorderColor;
      radTreeNode.Style.GradientAngle = this.GradientAngle;
      radTreeNode.Style.GradientPercentage = this.GradientPercentage;
      radTreeNode.Style.GradientPercentage2 = this.GradientPercentage2;
      radTreeNode.Style.GradientStyle = this.GradientStyle;
      radTreeNode.Style.NumberOfColors = this.NumberOfColors;
      radTreeNode.Style.TextAlignment = this.TextAlignment;
      foreach (XmlTreeNode node in this.Nodes)
        radTreeNode.Nodes.Add(node.Deserialize());
      return radTreeNode;
    }

    public virtual void ReadUnknownAttribute(XmlAttribute attribute)
    {
      bool result = false;
      switch (attribute.Name)
      {
        case "ShowCheckBox":
          if (!bool.TryParse(attribute.Value, out result))
            break;
          this.CheckType = result ? CheckType.CheckBox : CheckType.None;
          break;
        case "ShowRadioButton":
          if (!bool.TryParse(attribute.Value, out result))
            break;
          this.CheckType = result ? CheckType.RadioButton : CheckType.None;
          break;
      }
    }
  }
}
