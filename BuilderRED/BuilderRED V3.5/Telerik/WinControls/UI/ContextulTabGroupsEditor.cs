// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ContextulTabGroupsEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ContextulTabGroupsEditor : RadForm
  {
    private RadItemCollection collection = new RadItemCollection();
    private RadItemCollection ribbonBarTabs;
    private IContainer components;
    private RadGroupBox radGroupBox1;
    private RadListControl radListBoxAssociatedTabs;
    private RadListControl radListBoxAvaibleTabs;
    private RadButton radButtonOk;
    private RadButton radButtonCancel;
    private RadButton radButtonRemoveOne;
    private RadButton radButtonRemoveAll;
    private RadButton radButtonAddAll;
    private RadButton radButtonAddOne;
    private RadLabel radLabel2;
    private RadLabel radLabel1;

    public RadItemCollection Collection
    {
      get
      {
        return this.collection;
      }
      set
      {
        this.collection = value;
      }
    }

    public ContextulTabGroupsEditor(RadItemCollection collection, RadRibbonBar parentRibbon)
    {
      this.InitializeComponent();
      this.Icon = Telerik.WinControls.ResFinder.ProgressIcon;
      this.InitCollection(collection);
      this.FillListBoxeses(parentRibbon);
    }

    private void InitCollection(RadItemCollection collection)
    {
      this.collection.Clear();
      foreach (RadItem radItem in collection)
        this.collection.Add(radItem);
    }

    private void FillListBoxeses(RadRibbonBar parentRibbon)
    {
      this.FillAssociatedTabs();
      this.FillAvaibleTabs(parentRibbon);
    }

    private void FillAvaibleTabs(RadRibbonBar parentRibbon)
    {
      this.ribbonBarTabs = new RadItemCollection();
      foreach (RadItem radItem in (IEnumerable<RadPageViewItem>) parentRibbon.RibbonBarElement.TabStripElement.Items)
      {
        RibbonTab ribbonTab = radItem as RibbonTab;
        if (ribbonTab != null && ribbonTab.CanBeAddedToContextualGroup)
          this.ribbonBarTabs.Add(radItem);
      }
      for (int index = 0; index < this.ribbonBarTabs.Count; ++index)
      {
        RadPageViewItem ribbonBarTab = (RadPageViewItem) this.ribbonBarTabs[index];
        if (this.ContextualGroupsNotContainsThisTab(parentRibbon, ribbonBarTab) && !(bool) ribbonBarTab.GetValue(RadItem.IsAddNewItemProperty))
          this.radListBoxAvaibleTabs.Items.Add(new RadListDataItem(ribbonBarTab.Text)
          {
            Tag = (object) ribbonBarTab
          });
      }
    }

    private bool ContextualGroupsNotContainsThisTab(
      RadRibbonBar parentRibbon,
      RadPageViewItem tabItem)
    {
      foreach (ContextualTabGroup contextualTabGroup in (RadItemCollection) parentRibbon.ContextualTabGroups)
      {
        if (contextualTabGroup.TabItems.Contains((RadItem) tabItem))
          return false;
      }
      return true;
    }

    private void FillAssociatedTabs()
    {
      for (int index = 0; index < this.collection.Count; ++index)
      {
        RadItem radItem = this.collection[index];
        this.radListBoxAssociatedTabs.Items.Add(new RadListDataItem(radItem.Text)
        {
          Tag = (object) radItem
        });
      }
    }

    private void radButtonOk_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void radButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void radButtonAddOne_Click(object sender, EventArgs e)
    {
      this.MoveItems(this.radListBoxAvaibleTabs, this.ribbonBarTabs, this.radListBoxAssociatedTabs, this.collection, 1);
    }

    private void radButtonAddAll_Click(object sender, EventArgs e)
    {
      this.MoveItems(this.radListBoxAvaibleTabs, this.ribbonBarTabs, this.radListBoxAssociatedTabs, this.collection, this.radListBoxAvaibleTabs.Items.Count);
    }

    private void radButtonRemoveAll_Click(object sender, EventArgs e)
    {
      this.MoveItems(this.radListBoxAssociatedTabs, this.collection, this.radListBoxAvaibleTabs, this.ribbonBarTabs, this.radListBoxAssociatedTabs.Items.Count);
    }

    private void radButtonRemoveOne_Click(object sender, EventArgs e)
    {
      this.MoveItems(this.radListBoxAssociatedTabs, this.collection, this.radListBoxAvaibleTabs, this.ribbonBarTabs, 1);
    }

    private void MoveItems(
      RadListControl left,
      RadItemCollection leftCollection,
      RadListControl right,
      RadItemCollection rigthCollection,
      int countItemsToMove)
    {
      if (left.Items.Count == 0)
        return;
      if (left.SelectedItems.Count == 0)
      {
        for (int index = 0; index < countItemsToMove; ++index)
        {
          RadListDataItem radListDataItem = left.Items[0];
          left.Items.Remove(radListDataItem);
          right.Items.Add(radListDataItem);
          RadItem tag = (RadItem) radListDataItem.Tag;
          leftCollection.Remove(tag);
          if (!rigthCollection.Contains(tag))
            rigthCollection.Add(tag);
        }
      }
      else
      {
        for (int index = 0; index < left.SelectedItems.Count; ++index)
        {
          RadListDataItem selectedItem = left.SelectedItems[index];
          left.Items.Remove(selectedItem);
          right.Items.Add(selectedItem);
          RadItem tag = (RadItem) selectedItem.Tag;
          leftCollection.Remove(tag);
          if (!rigthCollection.Contains(tag))
            rigthCollection.Add(tag);
        }
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
      this.radGroupBox1 = new RadGroupBox();
      this.radLabel2 = new RadLabel();
      this.radLabel1 = new RadLabel();
      this.radButtonRemoveOne = new RadButton();
      this.radButtonRemoveAll = new RadButton();
      this.radButtonAddAll = new RadButton();
      this.radButtonAddOne = new RadButton();
      this.radListBoxAssociatedTabs = new RadListControl();
      this.radListBoxAvaibleTabs = new RadListControl();
      this.radButtonOk = new RadButton();
      this.radButtonCancel = new RadButton();
      this.radGroupBox1.BeginInit();
      this.radGroupBox1.SuspendLayout();
      this.radLabel2.BeginInit();
      this.radLabel1.BeginInit();
      this.radButtonRemoveOne.BeginInit();
      this.radButtonRemoveAll.BeginInit();
      this.radButtonAddAll.BeginInit();
      this.radButtonAddOne.BeginInit();
      this.radListBoxAssociatedTabs.BeginInit();
      this.radListBoxAvaibleTabs.BeginInit();
      this.radButtonOk.BeginInit();
      this.radButtonCancel.BeginInit();
      this.SuspendLayout();
      this.radGroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radGroupBox1.Controls.Add((Control) this.radLabel2);
      this.radGroupBox1.Controls.Add((Control) this.radLabel1);
      this.radGroupBox1.Controls.Add((Control) this.radButtonRemoveOne);
      this.radGroupBox1.Controls.Add((Control) this.radButtonRemoveAll);
      this.radGroupBox1.Controls.Add((Control) this.radButtonAddAll);
      this.radGroupBox1.Controls.Add((Control) this.radButtonAddOne);
      this.radGroupBox1.Controls.Add((Control) this.radListBoxAssociatedTabs);
      this.radGroupBox1.Controls.Add((Control) this.radListBoxAvaibleTabs);
      this.radGroupBox1.FooterImageIndex = -1;
      this.radGroupBox1.FooterImageKey = "";
      this.radGroupBox1.HeaderImageIndex = -1;
      this.radGroupBox1.HeaderImageKey = "";
      this.radGroupBox1.HeaderMargin = new Padding(0);
      this.radGroupBox1.HeaderText = "Tabs";
      this.radGroupBox1.Location = new Point(13, 13);
      this.radGroupBox1.Name = "radGroupBox1";
      this.radGroupBox1.Size = new Size(588, 412);
      this.radGroupBox1.TabIndex = 0;
      this.radGroupBox1.Text = "Tabs";
      this.radLabel2.Location = new Point(325, 33);
      this.radLabel2.Name = "radLabel2";
      this.radLabel2.Size = new Size(88, 14);
      this.radLabel2.TabIndex = 5;
      this.radLabel2.Text = "Associated Tabs";
      this.radLabel1.Location = new Point(13, 33);
      this.radLabel1.Name = "radLabel1";
      this.radLabel1.Size = new Size(79, 14);
      this.radLabel1.TabIndex = 5;
      this.radLabel1.Text = "Available Tabs";
      this.radButtonRemoveOne.AllowShowFocusCues = true;
      this.radButtonRemoveOne.Location = new Point(246, 146);
      this.radButtonRemoveOne.Name = "radButtonRemoveOne";
      this.radButtonRemoveOne.Size = new Size(75, 23);
      this.radButtonRemoveOne.TabIndex = 4;
      this.radButtonRemoveOne.Text = "<";
      this.radButtonRemoveOne.Click += new EventHandler(this.radButtonRemoveOne_Click);
      this.radButtonRemoveAll.AllowShowFocusCues = true;
      this.radButtonRemoveAll.Location = new Point(246, 116);
      this.radButtonRemoveAll.Name = "radButtonRemoveAll";
      this.radButtonRemoveAll.Size = new Size(75, 23);
      this.radButtonRemoveAll.TabIndex = 3;
      this.radButtonRemoveAll.Text = "<<";
      this.radButtonRemoveAll.Click += new EventHandler(this.radButtonRemoveAll_Click);
      this.radButtonAddAll.AllowShowFocusCues = true;
      this.radButtonAddAll.Location = new Point(246, 86);
      this.radButtonAddAll.Name = "radButtonAddAll";
      this.radButtonAddAll.Size = new Size(75, 23);
      this.radButtonAddAll.TabIndex = 2;
      this.radButtonAddAll.Text = ">>";
      this.radButtonAddAll.Click += new EventHandler(this.radButtonAddAll_Click);
      this.radButtonAddOne.AllowShowFocusCues = true;
      this.radButtonAddOne.Location = new Point(246, 57);
      this.radButtonAddOne.Name = "radButtonAddOne";
      this.radButtonAddOne.Size = new Size(75, 23);
      this.radButtonAddOne.TabIndex = 1;
      this.radButtonAddOne.Text = ">";
      this.radButtonAddOne.Click += new EventHandler(this.radButtonAddOne_Click);
      this.radListBoxAssociatedTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radListBoxAssociatedTabs.Location = new Point(325, 55);
      this.radListBoxAssociatedTabs.Name = "radListBoxAssociatedTabs";
      this.radListBoxAssociatedTabs.SelectionMode = SelectionMode.MultiExtended;
      this.radListBoxAssociatedTabs.Size = new Size(248, 340);
      this.radListBoxAssociatedTabs.TabIndex = 0;
      this.radListBoxAvaibleTabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.radListBoxAvaibleTabs.Location = new Point(13, 55);
      this.radListBoxAvaibleTabs.Name = "radListBoxAvaibleTabs";
      this.radListBoxAvaibleTabs.SelectionMode = SelectionMode.MultiExtended;
      this.radListBoxAvaibleTabs.Size = new Size(227, 340);
      this.radListBoxAvaibleTabs.TabIndex = 0;
      this.radButtonOk.AllowShowFocusCues = true;
      this.radButtonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonOk.Location = new Point(422, 430);
      this.radButtonOk.Name = "radButtonOk";
      this.radButtonOk.Size = new Size(75, 23);
      this.radButtonOk.TabIndex = 1;
      this.radButtonOk.Text = "OK";
      this.radButtonOk.Click += new EventHandler(this.radButtonOk_Click);
      this.radButtonCancel.AllowShowFocusCues = true;
      this.radButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonCancel.Location = new Point(524, 430);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(75, 23);
      this.radButtonCancel.TabIndex = 2;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonCancel.Click += new EventHandler(this.radButtonCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(613, 458);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radButtonOk);
      this.Controls.Add((Control) this.radGroupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Name = nameof (ContextulTabGroupsEditor);
      this.ShowInTaskbar = false;
      this.Text = "Contextual Tab Groups Editor";
      this.radGroupBox1.EndInit();
      this.radGroupBox1.ResumeLayout(false);
      this.radGroupBox1.PerformLayout();
      this.radLabel2.EndInit();
      this.radLabel1.EndInit();
      this.radButtonRemoveOne.EndInit();
      this.radButtonRemoveAll.EndInit();
      this.radButtonAddAll.EndInit();
      this.radButtonAddOne.EndInit();
      this.radListBoxAssociatedTabs.EndInit();
      this.radListBoxAvaibleTabs.EndInit();
      this.radButtonOk.EndInit();
      this.radButtonCancel.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
