// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilteredItemsEditorUI
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  internal class FilteredItemsEditorUI : Form
  {
    private RadGalleryElement defaultElement;
    private RadGalleryGroupFilter defaultFilter;
    private RadItemOwnerCollection originalValue;
    private RadItemOwnerCollection currentValue;
    private IWindowsFormsEditorService edSvc;
    private IDesignerHost host;
    private ITypeDiscoveryService typeSvc;
    private IContainer components;
    private Button AddBtn;
    private Button CancelBtn;
    private Button MoveDownBtn;
    private Button MoveUpBtn;
    private Button OkBtn;
    private Button RemoveBtn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lbItemsInCollection;
    private System.Windows.Forms.ListBox AssignedItemsBox;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private System.Windows.Forms.ListBox AvailableItemsBox;
    private TabPage tabPage2;
    private System.Windows.Forms.ListBox AvailableItemsBox2;

    public FilteredItemsEditorUI()
    {
      this.InitializeComponent();
    }

    public RadItemOwnerCollection Value
    {
      get
      {
        return this.currentValue;
      }
    }

    public void Start(
      IWindowsFormsEditorService edSvc,
      ITypeDiscoveryService typeSvc,
      IDesignerHost host,
      RadItemOwnerCollection collection,
      RadGalleryGroupFilter filter,
      RadGalleryElement owner)
    {
      this.host = host;
      this.edSvc = edSvc;
      this.typeSvc = typeSvc;
      this.currentValue = collection;
      this.originalValue = collection;
      this.defaultFilter = filter;
      this.defaultElement = owner;
    }

    public void End()
    {
      this.host = (IDesignerHost) null;
      this.edSvc = (IWindowsFormsEditorService) null;
      this.typeSvc = (ITypeDiscoveryService) null;
      this.defaultFilter = (RadGalleryGroupFilter) null;
      this.defaultElement = (RadGalleryElement) null;
      this.originalValue = (RadItemOwnerCollection) null;
      this.Reset();
    }

    public void Reset()
    {
      this.currentValue = (RadItemOwnerCollection) null;
    }

    private void AvailableItemsBoxSelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetButtonsEnabledState();
    }

    private void AssignedItemsBoxSelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetButtonsEnabledState();
    }

    private static void RemoveItemsContainedInFilter(
      ArrayList items,
      RadItemOwnerCollection groupItems)
    {
      for (int index1 = 0; index1 < groupItems.Count; ++index1)
      {
        RadGalleryGroupItem groupItem = groupItems[index1] as RadGalleryGroupItem;
        int index2 = items.IndexOf((object) groupItem);
        if (index2 >= 0)
        {
          items.RemoveAt(index2);
          --index1;
        }
      }
    }

    private void PopulateAvailableItems()
    {
      ArrayList items = new ArrayList();
      foreach (RadGalleryGroupItem group in (RadItemCollection) this.defaultElement.Groups)
        items.Add((object) group);
      foreach (RadGalleryGroupFilter filter in (RadItemCollection) this.defaultElement.Filters)
        FilteredItemsEditorUI.RemoveItemsContainedInFilter(items, filter.Items);
      for (int index = 0; index < items.Count; ++index)
        items[index] = (object) new FilteredItemsEditorUI.InstanceItem(items[index] as RadGalleryGroupItem, (RadGalleryGroupFilter) null, this.defaultElement);
      ArrayList arrayList = new ArrayList();
      foreach (RadGalleryGroupFilter filter in (RadItemCollection) this.defaultElement.Filters)
      {
        if (filter != this.defaultFilter)
        {
          foreach (RadGalleryGroupItem instance in (RadItemCollection) filter.Items)
            arrayList.Add((object) new FilteredItemsEditorUI.InstanceItem(instance, filter, this.defaultElement));
        }
      }
      this.AvailableItemsBox.BeginUpdate();
      this.AvailableItemsBox.Items.Clear();
      this.AvailableItemsBox.Items.AddRange(items.ToArray());
      this.AvailableItemsBox.EndUpdate();
      this.AvailableItemsBox2.BeginUpdate();
      this.AvailableItemsBox2.Items.Clear();
      this.AvailableItemsBox2.Items.AddRange(arrayList.ToArray());
      this.AvailableItemsBox2.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void PopulateAssignedItems()
    {
      this.AssignedItemsBox.BeginUpdate();
      this.AssignedItemsBox.Items.Clear();
      foreach (RadGalleryGroupItem instance in (RadItemCollection) this.defaultFilter.Items)
        this.AssignedItemsBox.Items.Add((object) new FilteredItemsEditorUI.InstanceItem(instance, this.defaultFilter, this.defaultElement));
      this.AssignedItemsBox.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void SetButtonsEnabledState()
    {
      if (this.tabControl1.SelectedIndex == 0)
        this.AddBtn.Enabled = this.AvailableItemsBox.SelectedItems.Count > 0;
      else
        this.AddBtn.Enabled = this.AvailableItemsBox2.SelectedItems.Count > 0;
      if (this.AssignedItemsBox.SelectedItems.Count > 0)
      {
        this.RemoveBtn.Enabled = true;
        this.MoveUpBtn.Enabled = this.AssignedItemsBox.SelectedIndices[0] != 0;
        this.MoveDownBtn.Enabled = this.AssignedItemsBox.SelectedIndices[this.AssignedItemsBox.SelectedIndices.Count - 1] != this.AssignedItemsBox.Items.Count - 1;
      }
      else
        this.RemoveBtn.Enabled = this.MoveDownBtn.Enabled = this.MoveUpBtn.Enabled = false;
    }

    private void AddAction(object sender, EventArgs e)
    {
      int selectedIndex;
      if (this.tabControl1.SelectedIndex == 0)
      {
        foreach (FilteredItemsEditorUI.InstanceItem selectedItem in this.AvailableItemsBox.SelectedItems)
        {
          selectedItem.Filter = this.defaultFilter;
          this.defaultFilter.Items.Add((RadItem) selectedItem.Instance);
        }
        selectedIndex = this.AvailableItemsBox.SelectedIndices[0];
      }
      else
      {
        foreach (FilteredItemsEditorUI.InstanceItem selectedItem in this.AvailableItemsBox2.SelectedItems)
        {
          selectedItem.Filter = this.defaultFilter;
          foreach (FilteredItemsEditorUI.InstanceItem instanceItem in this.AssignedItemsBox.Items)
          {
            if (selectedItem.Instance.Equals((object) instanceItem.Instance))
            {
              int num = (int) MessageBox.Show("This item is already added");
              return;
            }
          }
          this.defaultFilter.Items.Add((RadItem) selectedItem.Instance);
        }
        selectedIndex = this.AvailableItemsBox2.SelectedIndices[0];
      }
      this.PopulateAvailableItems();
      this.PopulateAssignedItems();
      if (this.tabControl1.SelectedIndex == 0)
      {
        if (this.AvailableItemsBox.Items.Count > 0)
          this.AvailableItemsBox.SelectedIndex = Math.Min(this.AvailableItemsBox.Items.Count - 1, selectedIndex);
      }
      else if (this.AvailableItemsBox.Items.Count > 0)
        this.AvailableItemsBox2.SelectedIndex = Math.Min(this.AvailableItemsBox2.Items.Count - 1, selectedIndex);
      this.SetButtonsEnabledState();
    }

    private void RemoveAction(object sender, EventArgs e)
    {
      foreach (FilteredItemsEditorUI.InstanceItem selectedItem in this.AssignedItemsBox.SelectedItems)
      {
        selectedItem.Filter = (RadGalleryGroupFilter) null;
        this.defaultFilter.Items.Remove((RadItem) selectedItem.Instance);
      }
      int selectedIndex = this.AssignedItemsBox.SelectedIndices[0];
      this.PopulateAvailableItems();
      this.PopulateAssignedItems();
      if (this.AssignedItemsBox.Items.Count > 0)
        this.AssignedItemsBox.SelectedIndex = Math.Min(this.AssignedItemsBox.Items.Count - 1, selectedIndex);
      this.SetButtonsEnabledState();
    }

    private void MoveDownAction(object sender, EventArgs e)
    {
      int[] numArray = new int[this.AssignedItemsBox.SelectedItems.Count];
      for (int index1 = this.AssignedItemsBox.SelectedItems.Count - 1; index1 >= 0; --index1)
      {
        RadGalleryGroupItem instance = ((FilteredItemsEditorUI.InstanceItem) this.AssignedItemsBox.SelectedItems[index1]).Instance;
        int index2 = this.defaultFilter.Items.IndexOf((RadItem) instance);
        numArray[index1] = index2 + 1;
        this.defaultFilter.Items.RemoveAt(index2);
        this.defaultFilter.Items.Insert(index2 + 1, (RadItem) instance);
      }
      this.AssignedItemsBox.BeginUpdate();
      this.PopulateAssignedItems();
      foreach (int index in numArray)
        this.AssignedItemsBox.SetSelected(index, true);
      this.AssignedItemsBox.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void MoveUpAction(object sender, EventArgs e)
    {
      int[] numArray = new int[this.AssignedItemsBox.SelectedItems.Count];
      for (int index1 = 0; index1 < this.AssignedItemsBox.SelectedItems.Count; ++index1)
      {
        RadGalleryGroupItem instance = ((FilteredItemsEditorUI.InstanceItem) this.AssignedItemsBox.SelectedItems[index1]).Instance;
        int index2 = this.defaultFilter.Items.IndexOf((RadItem) instance);
        numArray[index1] = index2 - 1;
        this.defaultFilter.Items.RemoveAt(index2);
        this.defaultFilter.Items.Insert(index2 - 1, (RadItem) instance);
      }
      this.AssignedItemsBox.BeginUpdate();
      this.PopulateAssignedItems();
      foreach (int index in numArray)
        this.AssignedItemsBox.SetSelected(index, true);
      this.AssignedItemsBox.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void FilterBoxSelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateAvailableItems();
      this.SetButtonsEnabledState();
    }

    private void EditorForm_Load(object sender, EventArgs e)
    {
      this.PopulateAvailableItems();
      this.PopulateAssignedItems();
      if (this.AvailableItemsBox.Items.Count <= 0)
        return;
      this.AvailableItemsBox.SelectedIndex = 0;
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetButtonsEnabledState();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilteredItemsEditorUI));
      this.AssignedItemsBox = new System.Windows.Forms.ListBox();
      this.AddBtn = new Button();
      this.RemoveBtn = new Button();
      this.OkBtn = new Button();
      this.CancelBtn = new Button();
      this.lbItemsInCollection = new System.Windows.Forms.Label();
      this.MoveUpBtn = new Button();
      this.MoveDownBtn = new Button();
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.AvailableItemsBox = new System.Windows.Forms.ListBox();
      this.tabPage2 = new TabPage();
      this.AvailableItemsBox2 = new System.Windows.Forms.ListBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      this.AssignedItemsBox.IntegralHeight = false;
      this.AssignedItemsBox.Location = new Point(364, 33);
      this.AssignedItemsBox.Name = "AssignedItemsBox";
      this.AssignedItemsBox.SelectionMode = SelectionMode.MultiExtended;
      this.AssignedItemsBox.Size = new Size(256, 331);
      this.AssignedItemsBox.TabIndex = 2;
      this.AssignedItemsBox.SelectedIndexChanged += new EventHandler(this.AssignedItemsBoxSelectedIndexChanged);
      this.AddBtn.Enabled = false;
      this.AddBtn.Location = new Point(283, 177);
      this.AddBtn.Name = "AddBtn";
      this.AddBtn.Size = new Size(75, 23);
      this.AddBtn.TabIndex = 3;
      this.AddBtn.Text = "Add >>";
      this.AddBtn.Click += new EventHandler(this.AddAction);
      this.RemoveBtn.Enabled = false;
      this.RemoveBtn.Location = new Point(283, 217);
      this.RemoveBtn.Name = "RemoveBtn";
      this.RemoveBtn.Size = new Size(75, 23);
      this.RemoveBtn.TabIndex = 4;
      this.RemoveBtn.Text = "<< Remove";
      this.RemoveBtn.Click += new EventHandler(this.RemoveAction);
      this.OkBtn.DialogResult = DialogResult.OK;
      this.OkBtn.Location = new Point(464, 374);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new Size(75, 23);
      this.OkBtn.TabIndex = 5;
      this.OkBtn.Text = "OK";
      this.CancelBtn.DialogResult = DialogResult.Cancel;
      this.CancelBtn.Location = new Point(545, 374);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new Size(75, 23);
      this.CancelBtn.TabIndex = 6;
      this.CancelBtn.Text = "Cancel";
      this.lbItemsInCollection.Location = new Point(361, 17);
      this.lbItemsInCollection.Name = "lbItemsInCollection";
      this.lbItemsInCollection.Size = new Size(97, 17);
      this.lbItemsInCollection.TabIndex = 7;
      this.lbItemsInCollection.Text = "Assigned to group:";
      this.MoveUpBtn.BackColor = Color.White;
      this.MoveUpBtn.Enabled = false;
      this.MoveUpBtn.Image = (Image) componentResourceManager.GetObject("MoveUpBtn.Image");
      this.MoveUpBtn.Location = new Point(334, 35);
      this.MoveUpBtn.Name = "MoveUpBtn";
      this.MoveUpBtn.Size = new Size(24, 23);
      this.MoveUpBtn.TabIndex = 8;
      this.MoveUpBtn.UseVisualStyleBackColor = false;
      this.MoveUpBtn.Click += new EventHandler(this.MoveUpAction);
      this.MoveDownBtn.BackColor = Color.White;
      this.MoveDownBtn.Enabled = false;
      this.MoveDownBtn.Image = (Image) componentResourceManager.GetObject("MoveDownBtn.Image");
      this.MoveDownBtn.Location = new Point(334, 70);
      this.MoveDownBtn.Name = "MoveDownBtn";
      this.MoveDownBtn.Size = new Size(24, 23);
      this.MoveDownBtn.TabIndex = 8;
      this.MoveDownBtn.UseVisualStyleBackColor = false;
      this.MoveDownBtn.Click += new EventHandler(this.MoveDownAction);
      this.label1.Location = new Point(3, 17);
      this.label1.Name = "label1";
      this.label1.Size = new Size(86, 15);
      this.label1.TabIndex = 7;
      this.label1.Text = "Available Items:";
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.ItemSize = new Size(134, 18);
      this.tabControl1.Location = new Point(5, 35);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(272, 333);
      this.tabControl1.SizeMode = TabSizeMode.Fixed;
      this.tabControl1.TabIndex = 0;
      this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
      this.tabPage1.Controls.Add((Control) this.AvailableItemsBox);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(264, 307);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Unassingned";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.AvailableItemsBox.Dock = DockStyle.Fill;
      this.AvailableItemsBox.IntegralHeight = false;
      this.AvailableItemsBox.Location = new Point(3, 3);
      this.AvailableItemsBox.Name = "AvailableItemsBox";
      this.AvailableItemsBox.SelectionMode = SelectionMode.MultiExtended;
      this.AvailableItemsBox.Size = new Size(258, 301);
      this.AvailableItemsBox.TabIndex = 2;
      this.AvailableItemsBox.SelectedIndexChanged += new EventHandler(this.AvailableItemsBoxSelectedIndexChanged);
      this.tabPage2.Controls.Add((Control) this.AvailableItemsBox2);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(264, 307);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Assinged to other groups";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.AvailableItemsBox2.Dock = DockStyle.Fill;
      this.AvailableItemsBox2.IntegralHeight = false;
      this.AvailableItemsBox2.Location = new Point(3, 3);
      this.AvailableItemsBox2.Name = "AvailableItemsBox2";
      this.AvailableItemsBox2.SelectionMode = SelectionMode.MultiExtended;
      this.AvailableItemsBox2.Size = new Size(258, 301);
      this.AvailableItemsBox2.TabIndex = 11;
      this.AvailableItemsBox2.SelectedIndexChanged += new EventHandler(this.AvailableItemsBoxSelectedIndexChanged);
      this.AcceptButton = (IButtonControl) this.OkBtn;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.CancelBtn;
      this.ClientSize = new Size(628, 404);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.MoveUpBtn);
      this.Controls.Add((Control) this.lbItemsInCollection);
      this.Controls.Add((Control) this.CancelBtn);
      this.Controls.Add((Control) this.OkBtn);
      this.Controls.Add((Control) this.RemoveBtn);
      this.Controls.Add((Control) this.AddBtn);
      this.Controls.Add((Control) this.AssignedItemsBox);
      this.Controls.Add((Control) this.MoveDownBtn);
      this.Controls.Add((Control) this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FilteredItemsEditorUI);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Items Collection Editor";
      this.Load += new EventHandler(this.EditorForm_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    protected class InstanceItem
    {
      private RadGalleryGroupItem instance;
      private RadGalleryGroupFilter filter;
      private RadGalleryElement owner;

      public InstanceItem()
      {
      }

      public InstanceItem(
        RadGalleryGroupItem instance,
        RadGalleryGroupFilter filter,
        RadGalleryElement owner)
      {
        this.instance = instance;
        this.filter = filter;
        this.owner = owner;
      }

      public RadGalleryGroupItem Instance
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

      public RadGalleryGroupFilter Filter
      {
        get
        {
          return this.filter;
        }
        set
        {
          this.filter = value;
        }
      }

      public RadGalleryElement Owner
      {
        get
        {
          return this.owner;
        }
        set
        {
          this.owner = value;
        }
      }

      public override string ToString()
      {
        if (this.filter != null)
          return this.instance.ToString() + " (" + this.filter.ToString() + ")";
        return this.instance.ToString();
      }
    }
  }
}
