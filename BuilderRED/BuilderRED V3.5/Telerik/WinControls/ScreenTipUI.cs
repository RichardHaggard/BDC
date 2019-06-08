// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ScreenTipUI
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  internal class ScreenTipUI : UserControl
  {
    private object originalValue;
    private IScreenTipContent currentValue;
    private bool updateCurrentValue;
    private ScreenTipEditor editor;
    private IWindowsFormsEditorService edSvc;
    private IComponentChangeService changeSvc;
    private IDesignerHost host;
    private ITypeDiscoveryService typeSvc;
    private ICollection templates;
    private IContainer components;
    private System.Windows.Forms.ComboBox templateBox;
    private System.Windows.Forms.ListBox stylableElements;
    private PropertyGrid propertyGrid1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private SplitContainer splitContainer1;
    private Button button1;
    private Button button2;

    public ScreenTipUI()
      : this((ScreenTipEditor) null)
    {
    }

    public ScreenTipUI(ScreenTipEditor editor)
    {
      this.editor = editor;
      this.End();
      this.InitializeComponent();
    }

    public IWindowsFormsEditorService EditorService
    {
      get
      {
        return this.edSvc;
      }
    }

    public bool ShouldPersistValue
    {
      get
      {
        if (this.currentValue != null && (this.currentValue as RadItem).SerializeChildren)
        {
          foreach (RadElement tipItem in this.currentValue.TipItems)
          {
            if (tipItem.SerializeProperties)
              return true;
          }
        }
        return false;
      }
    }

    public object Value
    {
      get
      {
        if (this.ShouldPersistValue)
          return (object) this.currentValue;
        return (object) null;
      }
    }

    public void Initialize(
      IWindowsFormsEditorService edSvc,
      ITypeDiscoveryService typeSvc,
      IDesignerHost host,
      IComponentChangeService changeSvc,
      object value)
    {
      this.host = host;
      this.edSvc = edSvc;
      this.typeSvc = typeSvc;
      this.changeSvc = changeSvc;
      this.currentValue = value as IScreenTipContent;
      this.originalValue = value;
      this.updateCurrentValue = false;
      if (this.templates == null)
      {
        this.templates = this.typeSvc.GetTypes(typeof (IScreenTipContent), false);
        if (this.templates == null)
          return;
        foreach (System.Type template in (IEnumerable) this.templates)
        {
          if (!template.IsAbstract && !template.IsInterface && (!template.IsAbstract && template.IsPublic) && (!this.templateBox.Items.Contains((object) template) && template.IsVisible && !typeof (Control).IsAssignableFrom(template)))
            this.templateBox.Items.Add((object) template);
        }
      }
      if (this.currentValue != null)
      {
        System.Type templateType = (value as IScreenTipContent).TemplateType;
        int index = (object) templateType != null ? this.templateBox.Items.IndexOf((object) templateType) : -1;
        if (index > -1)
          this.templateBox.SelectedItem = this.templateBox.Items[index];
        this.PopulateElementsBox(this.currentValue.TipItems);
      }
      this.updateCurrentValue = true;
    }

    public void End()
    {
      this.typeSvc = (ITypeDiscoveryService) null;
      this.host = (IDesignerHost) null;
      this.editor = (ScreenTipEditor) null;
      this.edSvc = (IWindowsFormsEditorService) null;
      this.originalValue = (object) null;
      this.currentValue = (IScreenTipContent) null;
    }

    private void templateBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this.updateCurrentValue || this.currentValue != null && (object) (System.Type) this.templateBox.SelectedItem == (object) this.currentValue.GetType())
        return;
      this.currentValue = (IScreenTipContent) null;
      if (this.templateBox.SelectedIndex == 0)
        return;
      if (this.originalValue != null)
        this.host.DestroyComponent(this.originalValue as IComponent);
      this.currentValue = this.host.CreateComponent(this.templateBox.SelectedItem as System.Type) as IScreenTipContent;
      if (this.currentValue is Control)
      {
        IScreenTipContent screenTipElement = (IScreenTipContent) (this.currentValue as RadScreenTip).ScreenTipElement;
        screenTipElement.TemplateType = (this.currentValue as RadScreenTip).TemplateType;
        this.currentValue = screenTipElement;
      }
      this.updateCurrentValue = true;
      this.PopulateElementsBox(this.currentValue.TipItems);
    }

    private void stylableElements_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.propertyGrid1.SelectedObject = (object) (this.stylableElements.SelectedItem as ScreenTipUI.InstanceItem)?.Instance;
    }

    protected void PopulateElementsBox(RadItemReadOnlyCollection elements)
    {
      this.stylableElements.Items.Clear();
      for (int index = 0; index < elements.Count; ++index)
        this.stylableElements.Items.Add((object) new ScreenTipUI.InstanceItem(elements[index]));
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.EditorService.CloseDropDown();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.currentValue = (IScreenTipContent) null;
      this.EditorService.CloseDropDown();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.templateBox = new System.Windows.Forms.ComboBox();
      this.stylableElements = new System.Windows.Forms.ListBox();
      this.propertyGrid1 = new PropertyGrid();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.splitContainer1 = new SplitContainer();
      this.button1 = new Button();
      this.button2 = new Button();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      this.templateBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.templateBox.FormattingEnabled = true;
      this.templateBox.Items.AddRange(new object[1]
      {
        (object) "Select a template"
      });
      this.templateBox.Location = new Point(10, 26);
      this.templateBox.Name = "templateBox";
      this.templateBox.Size = new Size(299, 21);
      this.templateBox.TabIndex = 0;
      this.templateBox.SelectedIndexChanged += new EventHandler(this.templateBox_SelectedIndexChanged);
      this.stylableElements.FormattingEnabled = true;
      this.stylableElements.Location = new Point(13, 107);
      this.stylableElements.Name = "stylableElements";
      this.stylableElements.Size = new Size(296, 173);
      this.stylableElements.TabIndex = 1;
      this.stylableElements.SelectedIndexChanged += new EventHandler(this.stylableElements_SelectedIndexChanged);
      this.propertyGrid1.Location = new Point(20, 26);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new Size(237, 254);
      this.propertyGrid1.TabIndex = 2;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(10, 10);
      this.label1.Name = "label1";
      this.label1.Size = new Size(150, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Available screen tip templates:";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(10, 91);
      this.label2.Name = "label2";
      this.label2.Size = new Size(141, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Screen tip stylable elements:";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 10);
      this.label3.Name = "label3";
      this.label3.Size = new Size(90, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Object properties:";
      this.splitContainer1.Dock = DockStyle.Top;
      this.splitContainer1.Location = new Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.stylableElements);
      this.splitContainer1.Panel1.Controls.Add((Control) this.label2);
      this.splitContainer1.Panel1.Controls.Add((Control) this.templateBox);
      this.splitContainer1.Panel1.Controls.Add((Control) this.label1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.propertyGrid1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.label3);
      this.splitContainer1.Size = new Size(585, 299);
      this.splitContainer1.SplitterDistance = 312;
      this.splitContainer1.TabIndex = 6;
      this.button1.Location = new Point(417, 306);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 7;
      this.button1.Text = "Ok";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(498, 306);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 8;
      this.button2.Text = "Cancel";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.splitContainer1);
      this.Name = nameof (ScreenTipUI);
      this.Size = new Size(585, 340);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    protected class InstanceItem
    {
      private RadItem instance;

      public InstanceItem()
      {
      }

      public InstanceItem(RadItem instance)
      {
        this.instance = instance;
      }

      public RadItem Instance
      {
        get
        {
          return this.instance;
        }
        set
        {
          this.instance = value;
        }
      }

      public override string ToString()
      {
        return this.instance.Class;
      }
    }
  }
}
