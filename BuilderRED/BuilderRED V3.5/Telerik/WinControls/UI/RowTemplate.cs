// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowTemplate
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RowTemplate : NotifyPropertyBase
  {
    private RowDefinitionsCollection rows = new RowDefinitionsCollection();
    private int cellSpacing = -1;
    private int border = 1;
    private int cellPadding;

    public RowTemplate()
    {
      this.rows.Owner = this;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection that contains all the row definitions in the RowTemplate.")]
    [Browsable(true)]
    public RowDefinitionsCollection Rows
    {
      get
      {
        return this.rows;
      }
    }

    [Description("Gets the RowDefinition at the specified index.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RowDefinition this[int index]
    {
      get
      {
        return this.rows[index];
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the CellDefinition at the specified row and column")]
    public CellDefinition this[int row, int column]
    {
      get
      {
        return this.rows[row].Cells[column];
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(-1)]
    [Description("Gets or sets the cell spacing for this RowTemplate.")]
    public int CellSpacing
    {
      get
      {
        return this.cellSpacing;
      }
      set
      {
        this.SetProperty<int>(nameof (CellSpacing), ref this.cellSpacing, value);
      }
    }

    [Browsable(true)]
    [DefaultValue(0)]
    [Category("Appearance")]
    [Description("Gets or sets the cell padding for this RowTemplate.")]
    public int CellPadding
    {
      get
      {
        return this.cellPadding;
      }
      set
      {
        this.SetProperty<int>(nameof (CellPadding), ref this.cellPadding, value);
      }
    }

    [Browsable(true)]
    [DefaultValue(1)]
    [Category("Appearance")]
    [Description("Gets or sets the border width for this RowTemplate.")]
    public int Border
    {
      get
      {
        return this.border;
      }
      set
      {
        this.SetProperty<int>(nameof (Border), ref this.border, value);
      }
    }

    public void ReadXml(string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        this.ReadXml((Stream) fileStream);
    }

    public void ReadXml(Stream stream)
    {
      using (XmlTextReader xmlTextReader = new XmlTextReader(stream))
      {
        this.Rows.Clear();
        RowDefinition rowDefinition1 = (RowDefinition) null;
        while (xmlTextReader.Read())
        {
          if (xmlTextReader.NodeType == XmlNodeType.Element)
          {
            if (xmlTextReader.Name.ToLower() == "table")
            {
              this.CellSpacing = this.GetIntAttribute(xmlTextReader.GetAttribute("cellspacing"), 0);
              this.CellPadding = this.GetIntAttribute(xmlTextReader.GetAttribute("cellpadding"), 0);
              this.Border = this.GetIntAttribute(xmlTextReader.GetAttribute("border"), 1);
            }
            else if (xmlTextReader.Name.ToLower() == "tr")
            {
              Color colorAttribute = this.GetColorAttribute(xmlTextReader.GetAttribute("bgcolor"), Color.Empty);
              RowDefinition rowDefinition2 = new RowDefinition();
              rowDefinition2.BackColor = colorAttribute;
              this.Rows.Add(rowDefinition2);
              rowDefinition1 = rowDefinition2;
            }
            else if (xmlTextReader.Name.ToLower() == "td" && rowDefinition1 != null)
            {
              int intAttribute1 = this.GetIntAttribute(xmlTextReader.GetAttribute("colspan"), 1);
              int intAttribute2 = this.GetIntAttribute(xmlTextReader.GetAttribute("rowspan"), 1);
              int intAttribute3 = this.GetIntAttribute(xmlTextReader.GetAttribute("width"), 50);
              int intAttribute4 = this.GetIntAttribute(xmlTextReader.GetAttribute("height"), 20);
              Color colorAttribute = this.GetColorAttribute(xmlTextReader.GetAttribute("bgcolor"), Color.Empty);
              int intAttribute5 = this.GetIntAttribute(xmlTextReader.GetAttribute("border"), -1);
              xmlTextReader.Read();
              rowDefinition1.Height = Math.Max(rowDefinition1.Height, intAttribute4);
              rowDefinition1.Cells.Add(new CellDefinition(xmlTextReader.Value, intAttribute3, intAttribute1, intAttribute2)
              {
                BackColor = colorAttribute,
                Border = intAttribute5
              });
            }
          }
        }
      }
    }

    public void WriteXml(string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
        this.WriteXml((Stream) fileStream);
    }

    public void WriteXml(Stream stream)
    {
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement("table");
        if (this.CellSpacing != 0)
          xmlTextWriter.WriteAttributeString("cellspacing", this.CellSpacing.ToString());
        if (this.CellPadding != 0)
          xmlTextWriter.WriteAttributeString("cellpadding", this.CellPadding.ToString());
        if (this.CellPadding != 1)
          xmlTextWriter.WriteAttributeString("border", this.Border.ToString());
        foreach (RowDefinition row in (Collection<RowDefinition>) this.rows)
        {
          xmlTextWriter.WriteStartElement("tr");
          if (row.BackColor != Color.Empty)
            xmlTextWriter.WriteAttributeString("bgcolor", "#" + ColorProvider.ColorToHex(row.BackColor));
          bool flag = false;
          foreach (CellDefinition cell in (Collection<CellDefinition>) row.Cells)
          {
            xmlTextWriter.WriteStartElement("td");
            if (cell.ColSpan != 1)
              xmlTextWriter.WriteAttributeString("colspan", cell.ColSpan.ToString());
            if (cell.RowSpan != 1)
              xmlTextWriter.WriteAttributeString("rowspan", cell.RowSpan.ToString());
            if (cell.Border != -1)
              xmlTextWriter.WriteAttributeString("border", cell.Border.ToString());
            xmlTextWriter.WriteAttributeString("width", cell.Width.ToString());
            if (!flag && row.Height != 20)
            {
              flag = true;
              xmlTextWriter.WriteAttributeString("height", row.Height.ToString());
            }
            if (cell.BackColor != Color.Empty)
              xmlTextWriter.WriteAttributeString("bgcolor", "#" + ColorProvider.ColorToHex(cell.BackColor));
            xmlTextWriter.WriteString(cell.UniqueName);
            xmlTextWriter.WriteEndElement();
          }
          xmlTextWriter.WriteEndElement();
        }
        xmlTextWriter.WriteEndElement();
      }
    }

    private int GetIntAttribute(string value, int defaultValue)
    {
      int result = 0;
      if (!string.IsNullOrEmpty(value) && int.TryParse(value, out result))
        return result;
      return defaultValue;
    }

    private Color GetColorAttribute(string value, Color defaultValue)
    {
      Color color = Color.Empty;
      if (!string.IsNullOrEmpty(value))
      {
        color = ColorProvider.HexToColor(value);
        if (color == Color.Empty)
          color = Color.FromName(value);
        if (color == Color.Empty)
          return defaultValue;
      }
      return color;
    }
  }
}
