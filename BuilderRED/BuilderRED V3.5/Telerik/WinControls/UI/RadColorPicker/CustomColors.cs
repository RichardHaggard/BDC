// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.CustomColors
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using Telerik.WinControls.Themes.ColorDialog;

namespace Telerik.WinControls.UI.RadColorPicker
{
  [ToolboxItem(false)]
  public class CustomColors : UserControl
  {
    private int selectedPanelIndex = -1;
    private Color[] customColors = new Color[10];
    private Panel[] colorPanels = new Panel[10];
    private string configFilename = "custom_colors.cfg";
    private bool saveCustomColors = true;
    private string configLocation;
    private IContainer components;
    private Panel panel2;
    private Panel panel1;
    private Panel panel3;
    private Panel panel4;
    private Panel panel5;
    private Panel panel6;
    private Panel panel7;
    private Panel panel8;
    private Panel panel9;
    private Panel panel10;

    public event ColorChangedEventHandler ColorChanged;

    public event CustomColorsEventHandler CustomColorsConfigLocationNeeded;

    public bool SaveCustomColors
    {
      get
      {
        return this.saveCustomColors;
      }
      set
      {
        this.saveCustomColors = value;
      }
    }

    public CustomColors()
    {
      this.InitializeComponent();
      for (int index = 0; index < this.customColors.Length; ++index)
        this.customColors[index] = Color.White;
      this.configLocation = this.GetConfigFileLocation();
      this.LoadXML();
      int index1 = 0;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is Panel)
        {
          control.Click += new EventHandler(this.ctrl_Click);
          control.BackColor = this.customColors[index1];
          control.Tag = (object) index1;
          this.colorPanels[index1] = control as Panel;
          ++index1;
        }
      }
    }

    private string GetConfigFileLocation()
    {
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      if (folderPath == string.Empty)
        folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      return folderPath;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.FindForm() == null)
        return;
      this.FindForm().FormClosing += new FormClosingEventHandler(this.CustomColors_FormClosing);
    }

    private void CustomColors_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SaveXML();
    }

    public void SaveXML()
    {
      if (!this.SaveCustomColors)
        return;
      if (this.CustomColorsConfigLocationNeeded != null)
      {
        CustomColorsEventArgs args = new CustomColorsEventArgs(this.configLocation, this.configFilename);
        this.CustomColorsConfigLocationNeeded((object) this, args);
        this.configLocation = args.ConfigLocation;
        this.configFilename = args.ConfigFilename;
      }
      XmlTextWriter xmlTextWriter = new XmlTextWriter((Stream) new FileStream(this.configLocation + "\\" + this.configFilename, FileMode.Create), Encoding.UTF8);
      xmlTextWriter.Formatting = Formatting.Indented;
      xmlTextWriter.WriteStartDocument();
      xmlTextWriter.WriteStartElement("Colors");
      for (int index = 0; index < this.customColors.Length; ++index)
        xmlTextWriter.WriteElementString("Color", this.customColors[index].ToArgb().ToString());
      xmlTextWriter.WriteEndElement();
      xmlTextWriter.WriteEndDocument();
      xmlTextWriter.Close();
    }

    public void LoadXML()
    {
      if (this.CustomColorsConfigLocationNeeded != null)
      {
        CustomColorsEventArgs args = new CustomColorsEventArgs(this.configLocation, this.configFilename);
        this.CustomColorsConfigLocationNeeded((object) this, args);
        this.configLocation = args.ConfigLocation;
        this.configFilename = args.ConfigFilename;
      }
      string path = this.configLocation + "\\" + this.configFilename;
      if (!File.Exists(path))
        return;
      XmlTextReader xmlTextReader = new XmlTextReader((Stream) new FileStream(path, FileMode.Open));
      int index = 0;
      while (!xmlTextReader.EOF)
      {
        if (xmlTextReader.ReadToFollowing("Color"))
        {
          Color empty = Color.Empty;
          Color color;
          try
          {
            color = Color.FromArgb(Convert.ToInt32(xmlTextReader.ReadString()));
          }
          catch (ArgumentException ex)
          {
            color = Color.White;
          }
          this.customColors[index] = color;
          ++index;
          if (index == this.customColors.Length)
            break;
        }
      }
      xmlTextReader.Close();
    }

    private void ctrl_Click(object sender, EventArgs e)
    {
      this.SelectPanel((int) ((Control) sender).Tag);
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorPanels[this.selectedPanelIndex].BackColor));
    }

    private void SelectPanel(int newPanelSelectedIndex)
    {
      if (this.selectedPanelIndex != -1)
        this.colorPanels[this.selectedPanelIndex].BorderStyle = BorderStyle.Fixed3D;
      this.selectedPanelIndex = newPanelSelectedIndex;
      this.colorPanels[this.selectedPanelIndex].BorderStyle = BorderStyle.FixedSingle;
    }

    public string CustomColorsConfigLocation
    {
      get
      {
        return this.configLocation;
      }
    }

    public int SelectedColorIndex
    {
      get
      {
        return this.selectedPanelIndex;
      }
      set
      {
        this.selectedPanelIndex = value;
        if (this.selectedPanelIndex < this.colorPanels.Length)
          return;
        this.selectedPanelIndex = -1;
      }
    }

    public void SaveColor(Color color)
    {
      if (this.selectedPanelIndex == -1)
        this.selectedPanelIndex = 0;
      this.customColors[this.selectedPanelIndex] = color;
      this.colorPanels[this.selectedPanelIndex].BackColor = color;
      this.colorPanels[this.selectedPanelIndex].Refresh();
      this.SelectPanel((this.selectedPanelIndex + 1) % this.colorPanels.Length);
    }

    public Color SelectedPanelColor
    {
      get
      {
        return this.colorPanels[this.selectedPanelIndex].BackColor;
      }
    }

    public Color[] Colors
    {
      get
      {
        return this.customColors;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel2 = new Panel();
      this.panel1 = new Panel();
      this.panel3 = new Panel();
      this.panel4 = new Panel();
      this.panel5 = new Panel();
      this.panel6 = new Panel();
      this.panel7 = new Panel();
      this.panel8 = new Panel();
      this.panel9 = new Panel();
      this.panel10 = new Panel();
      this.SuspendLayout();
      this.panel2.BackColor = Color.White;
      this.panel2.BorderStyle = BorderStyle.Fixed3D;
      this.panel2.Location = new Point(30, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(20, 20);
      this.panel2.TabIndex = 1;
      this.panel1.BackColor = Color.White;
      this.panel1.BorderStyle = BorderStyle.Fixed3D;
      this.panel1.Location = new Point(0, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(20, 20);
      this.panel1.TabIndex = 2;
      this.panel3.BackColor = Color.White;
      this.panel3.BorderStyle = BorderStyle.Fixed3D;
      this.panel3.Location = new Point(60, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(20, 20);
      this.panel3.TabIndex = 3;
      this.panel4.BackColor = Color.White;
      this.panel4.BorderStyle = BorderStyle.Fixed3D;
      this.panel4.Location = new Point(90, 3);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(20, 20);
      this.panel4.TabIndex = 4;
      this.panel5.BackColor = Color.White;
      this.panel5.BorderStyle = BorderStyle.Fixed3D;
      this.panel5.Location = new Point(120, 3);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(20, 20);
      this.panel5.TabIndex = 5;
      this.panel6.BackColor = Color.White;
      this.panel6.BorderStyle = BorderStyle.Fixed3D;
      this.panel6.Location = new Point(150, 3);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(20, 20);
      this.panel6.TabIndex = 6;
      this.panel7.BackColor = Color.White;
      this.panel7.BorderStyle = BorderStyle.Fixed3D;
      this.panel7.Location = new Point(180, 3);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(20, 20);
      this.panel7.TabIndex = 7;
      this.panel8.BackColor = Color.White;
      this.panel8.BorderStyle = BorderStyle.Fixed3D;
      this.panel8.Location = new Point(210, 3);
      this.panel8.Name = "panel8";
      this.panel8.Size = new Size(20, 20);
      this.panel8.TabIndex = 8;
      this.panel9.BackColor = Color.White;
      this.panel9.BorderStyle = BorderStyle.Fixed3D;
      this.panel9.Location = new Point(240, 3);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(20, 20);
      this.panel9.TabIndex = 2;
      this.panel10.BackColor = Color.White;
      this.panel10.BorderStyle = BorderStyle.Fixed3D;
      this.panel10.Location = new Point(270, 3);
      this.panel10.Name = "panel10";
      this.panel10.Size = new Size(20, 20);
      this.panel10.TabIndex = 2;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel4);
      this.Controls.Add((Control) this.panel5);
      this.Controls.Add((Control) this.panel6);
      this.Controls.Add((Control) this.panel7);
      this.Controls.Add((Control) this.panel8);
      this.Controls.Add((Control) this.panel9);
      this.Controls.Add((Control) this.panel10);
      this.Name = nameof (CustomColors);
      this.Padding = new Padding(5);
      this.Size = new Size(295, 25);
      this.ResumeLayout(false);
    }
  }
}
