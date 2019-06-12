// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupedItemsEditorUI
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
  internal class GroupedItemsEditorUI : Form
  {
    private RadGalleryElement defaultElement;
    private RadGalleryGroupItem defaultGroup;
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
    private System.Windows.Forms.ComboBox FilterTypeBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListBox AvailableItemsBox;
    private System.Windows.Forms.Label lbItemsInCollection;
    private System.Windows.Forms.ListBox AssignedItemsBox;
    private System.Windows.Forms.Label label2;

    public GroupedItemsEditorUI()
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
      RadGalleryGroupItem group,
      RadGalleryElement owner)
    {
      this.host = host;
      this.edSvc = edSvc;
      this.typeSvc = typeSvc;
      this.currentValue = collection;
      this.originalValue = collection;
      this.defaultGroup = group;
      this.defaultElement = owner;
    }

    public void End()
    {
      this.host = (IDesignerHost) null;
      this.edSvc = (IWindowsFormsEditorService) null;
      this.typeSvc = (ITypeDiscoveryService) null;
      this.defaultGroup = (RadGalleryGroupItem) null;
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

    private static void RemoveItemsContainedInGroup(
      ArrayList items,
      RadItemOwnerCollection groupItems)
    {
      for (int index1 = 0; index1 < groupItems.Count; ++index1)
      {
        RadGalleryItem groupItem = groupItems[index1] as RadGalleryItem;
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
      if (this.FilterTypeBox.SelectedIndex == 0)
      {
        foreach (RadGalleryItem radGalleryItem in (RadItemCollection) this.defaultElement.Items)
        {
          if (!(bool) radGalleryItem.GetValue(RadItem.IsAddNewItemProperty))
            items.Add((object) radGalleryItem);
        }
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.defaultElement.Groups)
          GroupedItemsEditorUI.RemoveItemsContainedInGroup(items, group.Items);
        for (int index = 0; index < items.Count; ++index)
          items[index] = (object) new GroupedItemsEditorUI.InstanceItem(items[index] as RadGalleryItem, this.defaultElement);
      }
      else if (this.FilterTypeBox.SelectedIndex == 1)
      {
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.defaultElement.Groups)
        {
          if (group != this.defaultGroup)
          {
            foreach (RadGalleryItem instance in (RadItemCollection) group.Items)
              items.Add((object) new GroupedItemsEditorUI.InstanceItem(instance, this.defaultElement));
          }
        }
      }
      this.AvailableItemsBox.BeginUpdate();
      this.AvailableItemsBox.Items.Clear();
      this.AvailableItemsBox.Items.AddRange(items.ToArray());
      this.AvailableItemsBox.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void PopulateAssignedItems()
    {
      this.AssignedItemsBox.BeginUpdate();
      this.AssignedItemsBox.Items.Clear();
      foreach (RadGalleryItem instance in (RadItemCollection) this.defaultGroup.Items)
        this.AssignedItemsBox.Items.Add((object) new GroupedItemsEditorUI.InstanceItem(instance, this.defaultElement));
      this.AssignedItemsBox.EndUpdate();
      this.SetButtonsEnabledState();
    }

    private void SetButtonsEnabledState()
    {
      this.AddBtn.Enabled = this.AvailableItemsBox.SelectedItems.Count > 0;
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
      foreach (GroupedItemsEditorUI.InstanceItem selectedItem in this.AvailableItemsBox.SelectedItems)
      {
        if (this.FilterTypeBox.SelectedIndex == 1)
        {
          foreach (RadGalleryGroupItem group in (RadItemCollection) this.defaultElement.Groups)
          {
            if (group != this.defaultGroup)
            {
              foreach (RadGalleryItem radGalleryItem in (RadItemCollection) group.Items)
              {
                if (selectedItem.Instance == radGalleryItem)
                {
                  group.Items.Remove((RadItem) radGalleryItem);
                  break;
                }
              }
            }
          }
        }
        this.defaultGroup.Items.Add((RadItem) selectedItem.Instance);
      }
      int selectedIndex = this.AvailableItemsBox.SelectedIndices[0];
      this.PopulateAvailableItems();
      this.PopulateAssignedItems();
      if (this.AvailableItemsBox.Items.Count > 0)
        this.AvailableItemsBox.SelectedIndex = Math.Min(this.AvailableItemsBox.Items.Count - 1, selectedIndex);
      this.SetButtonsEnabledState();
    }

    private void RemoveAction(object sender, EventArgs e)
    {
      foreach (GroupedItemsEditorUI.InstanceItem selectedItem in this.AssignedItemsBox.SelectedItems)
        this.defaultGroup.Items.Remove((RadItem) selectedItem.Instance);
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
        RadGalleryItem instance = ((GroupedItemsEditorUI.InstanceItem) this.AssignedItemsBox.SelectedItems[index1]).Instance;
        int index2 = this.defaultGroup.Items.IndexOf((RadItem) instance);
        numArray[index1] = index2 + 1;
        this.defaultGroup.Items.RemoveAt(index2);
        this.defaultGroup.Items.Insert(index2 + 1, (RadItem) instance);
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
        RadGalleryItem instance = ((GroupedItemsEditorUI.InstanceItem) this.AssignedItemsBox.SelectedItems[index1]).Instance;
        int index2 = this.defaultGroup.Items.IndexOf((RadItem) instance);
        numArray[index1] = index2 - 1;
        this.defaultGroup.Items.RemoveAt(index2);
        this.defaultGroup.Items.Insert(index2 - 1, (RadItem) instance);
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
      this.FilterTypeBox.SelectedIndex = 0;
      this.PopulateAssignedItems();
      if (this.AvailableItemsBox.Items.Count <= 0)
        return;
      this.AvailableItemsBox.SelectedIndex = 0;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GroupedItemsEditorUI));
      this.FilterTypeBox = new System.Windows.Forms.ComboBox();
      this.AvailableItemsBox = new System.Windows.Forms.ListBox();
      this.AssignedItemsBox = new System.Windows.Forms.ListBox();
      this.AddBtn = new Button();
      this.RemoveBtn = new Button();
      this.OkBtn = new Button();
      this.CancelBtn = new Button();
      this.lbItemsInCollection = new System.Windows.Forms.Label();
      this.MoveUpBtn = new Button();
      this.MoveDownBtn = new Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      this.FilterTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FilterTypeBox.Items.AddRange(new object[2]
      {
        (object) "Unassigned Items",
        (object) "Items assigned to different groups"
      });
      this.FilterTypeBox.Location = new Point(6, 37);
      this.FilterTypeBox.Name = "FilterTypeBox";
      this.FilterTypeBox.Size = new Size(248, 21);
      this.FilterTypeBox.TabIndex = 0;
      this.FilterTypeBox.SelectedIndexChanged += new EventHandler(this.FilterBoxSelectedIndexChanged);
      this.AvailableItemsBox.IntegralHeight = false;
      this.AvailableItemsBox.Location = new Point(6, 96);
      this.AvailableItemsBox.Name = "AvailableItemsBox";
      this.AvailableItemsBox.SelectionMode = SelectionMode.MultiExtended;
      this.AvailableItemsBox.Size = new Size(248, 272);
      this.AvailableItemsBox.TabIndex = 1;
      this.AvailableItemsBox.SelectedIndexChanged += new EventHandler(this.AvailableItemsBoxSelectedIndexChanged);
      this.AssignedItemsBox.IntegralHeight = false;
      this.AssignedItemsBox.Location = new Point(350, 37);
      this.AssignedItemsBox.Name = "AssignedItemsBox";
      this.AssignedItemsBox.SelectionMode = SelectionMode.MultiExtended;
      this.AssignedItemsBox.Size = new Size(256, 331);
      this.AssignedItemsBox.TabIndex = 2;
      this.AssignedItemsBox.SelectedIndexChanged += new EventHandler(this.AssignedItemsBoxSelectedIndexChanged);
      this.AddBtn.Enabled = false;
      this.AddBtn.Location = new Point(262, 177);
      this.AddBtn.Name = "AddBtn";
      this.AddBtn.Size = new Size(75, 23);
      this.AddBtn.TabIndex = 3;
      this.AddBtn.Text = "Add >>";
      this.AddBtn.Click += new EventHandler(this.AddAction);
      this.RemoveBtn.Enabled = false;
      this.RemoveBtn.Location = new Point(262, 217);
      this.RemoveBtn.Name = "RemoveBtn";
      this.RemoveBtn.Size = new Size(75, 23);
      this.RemoveBtn.TabIndex = 4;
      this.RemoveBtn.Text = "<< Remove";
      this.RemoveBtn.Click += new EventHandler(this.RemoveAction);
      this.OkBtn.DialogResult = DialogResult.OK;
      this.OkBtn.Location = new Point(446, 377);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new Size(75, 23);
      this.OkBtn.TabIndex = 5;
      this.OkBtn.Text = "OK";
      this.CancelBtn.DialogResult = DialogResult.Cancel;
      this.CancelBtn.Location = new Point(531, 377);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new Size(75, 23);
      this.CancelBtn.TabIndex = 6;
      this.CancelBtn.Text = "Cancel";
      this.lbItemsInCollection.Location = new Point(347, 18);
      this.lbItemsInCollection.Name = "lbItemsInCollection";
      this.lbItemsInCollection.Size = new Size(97, 17);
      this.lbItemsInCollection.TabIndex = 7;
      this.lbItemsInCollection.Text = "Assigned to group:";
      this.MoveUpBtn.BackColor = Color.White;
      this.MoveUpBtn.Enabled = false;
      this.MoveUpBtn.Image = (Image) componentResourceManager.GetObject("MoveUpBtn.Image");
      this.MoveUpBtn.Location = new Point(320, 38);
      this.MoveUpBtn.Name = "MoveUpBtn";
      this.MoveUpBtn.Size = new Size(24, 23);
      this.MoveUpBtn.TabIndex = 8;
      this.MoveUpBtn.UseVisualStyleBackColor = false;
      this.MoveUpBtn.Click += new EventHandler(this.MoveUpAction);
      this.MoveDownBtn.BackColor = Color.White;
      this.MoveDownBtn.Enabled = false;
      this.MoveDownBtn.Image = (Image) componentResourceManager.GetObject("MoveDownBtn.Image");
      this.MoveDownBtn.Location = new Point(320, 70);
      this.MoveDownBtn.Name = "MoveDownBtn";
      this.MoveDownBtn.Size = new Size(24, 23);
      this.MoveDownBtn.TabIndex = 8;
      this.MoveDownBtn.UseVisualStyleBackColor = false;
      this.MoveDownBtn.Click += new EventHandler(this.MoveDownAction);
      this.label1.Location = new Point(3, 78);
      this.label1.Name = "label1";
      this.label1.Size = new Size(86, 15);
      this.label1.TabIndex = 7;
      this.label1.Text = "Available Items:";
      this.label2.Location = new Point(3, 18);
      this.label2.Name = "label2";
      this.label2.Size = new Size(48, 16);
      this.label2.TabIndex = 9;
      this.label2.Text = "Filter by:";
      this.AcceptButton = (IButtonControl) this.OkBtn;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.CancelBtn;
      this.ClientSize = new Size(618, 404);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.MoveUpBtn);
      this.Controls.Add((Control) this.lbItemsInCollection);
      this.Controls.Add((Control) this.CancelBtn);
      this.Controls.Add((Control) this.OkBtn);
      this.Controls.Add((Control) this.RemoveBtn);
      this.Controls.Add((Control) this.AddBtn);
      this.Controls.Add((Control) this.AssignedItemsBox);
      this.Controls.Add((Control) this.AvailableItemsBox);
      this.Controls.Add((Control) this.FilterTypeBox);
      this.Controls.Add((Control) this.MoveDownBtn);
      this.Controls.Add((Control) this.label1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (GroupedItemsEditorUI);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Items Collection Editor";
      this.Load += new EventHandler(this.EditorForm_Load);
      this.ResumeLayout(false);
    }

    protected class InstanceItem
    {
      private RadGalleryItem instance;
      private RadGalleryElement owner;

      public InstanceItem()
      {
      }

      public InstanceItem(RadGalleryItem instance, RadGalleryElement owner)
      {
        this.instance = instance;
        this.owner = owner;
      }

      public RadGalleryItem Instance
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
        string str = string.Empty;
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.owner.Groups)
        {
          foreach (RadGalleryItem radGalleryItem in (RadItemCollection) group.Items)
          {
            if (this.instance == radGalleryItem)
              str = " (" + group.ToString() + ")";
          }
        }
        return this.instance.ToString() + str;
      }
    }
  }
}
