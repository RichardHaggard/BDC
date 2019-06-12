// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.XmlTreeView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.WinControls.UI
{
  [XmlRoot("TreeView")]
  public class XmlTreeView : IXmlTreeSerializable
  {
    [XmlAttribute("PlusMinusAnimationStep")]
    [DefaultValue(0.025)]
    public double PlusMinusAnimationStep = 0.025;
    [DefaultValue("\\")]
    [XmlAttribute("PathSeparator")]
    public string PathSeparator = "\\";
    [XmlIgnore]
    public Color BackColor = SystemColors.Control;
    [DefaultValue(true)]
    [XmlAttribute("FullRowSelect")]
    public bool FullRowSelect = true;
    [DefaultValue(TreeLineStyle.Dot)]
    [XmlAttribute("LineStyle")]
    public TreeLineStyle LineStyle = TreeLineStyle.Dot;
    [XmlAttribute("ItemHeight")]
    [DefaultValue(20)]
    public int ItemHeight = 20;
    [XmlIgnore]
    public Color LineColor = Color.Gray;
    [XmlAttribute("MouseDownEditDelay")]
    [DefaultValue(2000)]
    public int MouseDownEditDelay = 2000;
    [XmlAttribute("TreeIndent")]
    [DefaultValue(20)]
    public int TreeIndent = 20;
    [DefaultValue(true)]
    [XmlAttribute("ShowPlusMinus")]
    public bool ShowPlusMinus = true;
    [DefaultValue(true)]
    [XmlAttribute("ShowRootLines")]
    public bool ShowRootLines = true;
    [XmlElement("Nodes")]
    public List<XmlTreeNode> Nodes = new List<XmlTreeNode>();
    [XmlAttribute("ShowExpandCollapse")]
    [DefaultValue(true)]
    public bool ShowExpandCollapse = true;
    [DefaultValue(false)]
    [XmlAttribute("AllowIncrementalSearch")]
    public bool AllowIncrementalSearch;
    [DefaultValue(ExpandAnimation.Opacity)]
    [XmlAttribute("ExpandAnimation")]
    public ExpandAnimation ExpandAnimation;
    [DefaultValue(false)]
    [XmlAttribute("AllowArbitaryItemHeight")]
    public bool AllowArbitraryItemHeight;
    [XmlAttribute("MultiSelect")]
    [DefaultValue(false)]
    public bool MultiSelect;
    [XmlAttribute("ShowLines")]
    [DefaultValue(false)]
    public bool ShowLines;
    [XmlAttribute("SpacingBetweenNodes")]
    [DefaultValue(0)]
    public int SpacingBetweenNodes;
    [XmlAttribute("AllowDragDrop")]
    [DefaultValue(false)]
    public bool AllowDragDrop;
    [DefaultValue(false)]
    [XmlAttribute("AllowPlusMinusAnimation")]
    public bool AllowPlusMinusAnimation;
    [DefaultValue(false)]
    [XmlAttribute("TriStateMode")]
    public bool TriStateMode;
    [DefaultValue(false)]
    [XmlAttribute("CheckBoxes")]
    public bool CheckBoxes;
    [DefaultValue(false)]
    [XmlAttribute("LabelEdit")]
    public bool LabelEdit;
    [XmlAttribute("ThemeName")]
    [DefaultValue("")]
    public string ThemeName;
    [XmlAttribute("ThemeClassName")]
    [DefaultValue("Telerik.WinControls.UI.RadTreeView")]
    public string ThemeClassName;
    [XmlAttribute("AllowDrop")]
    [DefaultValue(false)]
    public bool AllowDrop;
    [XmlAttribute("RightToLeft")]
    [DefaultValue(RightToLeft.No)]
    public RightToLeft RightToLeft;

    public XmlTreeView()
    {
    }

    public XmlTreeView(RadTreeView treeView)
    {
      this.AllowDragDrop = treeView.AllowDragDrop;
      this.AllowDrop = treeView.AllowDrop;
      this.BackColor = treeView.BackColor;
      this.CheckBoxes = treeView.CheckBoxes;
      this.MultiSelect = treeView.MultiSelect;
      this.FullRowSelect = treeView.FullRowSelect;
      this.ItemHeight = treeView.ItemHeight;
      this.LineColor = treeView.LineColor;
      this.LineStyle = treeView.LineStyle;
      this.PathSeparator = treeView.PathSeparator;
      this.ShowLines = treeView.ShowLines;
      this.ShowExpandCollapse = treeView.ShowExpandCollapse;
      this.ShowRootLines = treeView.ShowRootLines;
      this.ThemeClassName = treeView.ThemeClassName;
      this.ThemeName = treeView.ThemeName;
      this.TreeIndent = treeView.TreeIndent;
      this.TriStateMode = treeView.TriStateMode;
      this.LabelEdit = treeView.AllowEdit;
      this.ExpandAnimation = treeView.ExpandAnimation;
      this.AllowArbitraryItemHeight = treeView.AllowArbitraryItemHeight;
      this.AllowDragDrop = treeView.AllowDragDrop;
      this.SpacingBetweenNodes = treeView.SpacingBetweenNodes;
      this.RightToLeft = treeView.RightToLeft;
      this.SpacingBetweenNodes = treeView.SpacingBetweenNodes;
      foreach (RadTreeNode node in (Collection<RadTreeNode>) treeView.Nodes)
        this.Nodes.Add(new XmlTreeNode(node));
    }

    [XmlAttribute("BackColor")]
    [DefaultValue("Control")]
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

    [DefaultValue("Gray")]
    [XmlAttribute("LineColor")]
    public string XmlLineColor
    {
      get
      {
        return XmlTreeNode.ColorConverter.ConvertToString((object) this.LineColor);
      }
      set
      {
        this.LineColor = (Color) XmlTreeNode.ColorConverter.ConvertFromString(value);
      }
    }

    public void Deserialize(RadTreeView treeView)
    {
      if (treeView == null)
        return;
      treeView.BeginUpdate();
      treeView.AllowDragDrop = this.AllowDragDrop;
      treeView.AllowDrop = this.AllowDrop;
      treeView.BackColor = this.BackColor;
      treeView.CheckBoxes = this.CheckBoxes;
      treeView.MultiSelect = this.MultiSelect;
      treeView.FullRowSelect = this.FullRowSelect;
      treeView.ItemHeight = this.ItemHeight;
      treeView.AllowEdit = this.LabelEdit;
      treeView.LineColor = this.LineColor;
      treeView.LineStyle = this.LineStyle;
      treeView.PathSeparator = this.PathSeparator;
      treeView.ShowLines = this.ShowLines;
      treeView.ShowExpandCollapse = this.ShowExpandCollapse;
      treeView.ShowRootLines = this.ShowRootLines;
      treeView.ThemeClassName = this.ThemeClassName;
      treeView.ThemeName = this.ThemeName;
      treeView.TreeIndent = this.TreeIndent;
      treeView.TriStateMode = this.TriStateMode;
      treeView.ExpandAnimation = this.ExpandAnimation;
      treeView.AllowArbitraryItemHeight = this.AllowArbitraryItemHeight;
      treeView.SpacingBetweenNodes = this.SpacingBetweenNodes;
      treeView.RightToLeft = this.RightToLeft;
      treeView.Nodes.Clear();
      foreach (XmlTreeNode node in this.Nodes)
        treeView.Nodes.Add(node.Deserialize());
      treeView.EndUpdate();
    }

    public virtual void ReadUnknownAttribute(XmlAttribute attribute)
    {
      bool result = false;
      switch (attribute.Name)
      {
        case "AllowMultiselect":
          if (!bool.TryParse(attribute.Value, out result))
            break;
          this.MultiSelect = result;
          break;
        case "AllowDragDropBetweenTreeViews":
          if (!bool.TryParse(attribute.Value, out result))
            break;
          this.AllowDragDrop = result;
          break;
      }
    }
  }
}
