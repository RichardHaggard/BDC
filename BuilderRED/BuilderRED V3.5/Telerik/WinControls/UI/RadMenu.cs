// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Menus & Toolbars")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Builds attractive navigation systems")]
  [DefaultBindingProperty("Items")]
  [DefaultProperty("Items")]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadMenuDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadMenu : RadItemsControl, IMessageListener
  {
    private RadMenu.RadMenuState menuState = RadMenu.RadMenuState.NotActive;
    private List<RadMenuItemBase> itemsToBeRestoredAfterUnmerge = new List<RadMenuItemBase>();
    internal List<RadMenuItemBase> persistedItemsUponMenuMerge = new List<RadMenuItemBase>();
    private ApplicationMdiState applicationMdiState = new ApplicationMdiState();
    private bool systemKeyHighlight = true;
    private bool isMainMenu = true;
    private RadMenuElement menuElement;
    internal bool menuMergeApplied;
    private RadMenu sourceMenuUponMerge;
    private Form parentForm;
    private bool showKeyboardCues;
    private bool activated;
    private bool highLightCycleCompleted;
    internal bool ForceMessageListener;
    private bool shouldShowChildren;
    private bool prevItemWithChildren;

    [RadDescription("OrientationChanged", typeof (RadMenuElement))]
    [Category("Property Changed")]
    public event EventHandler OrientationChanged;

    [Category("Property Changed")]
    [RadDescription("AllItemsEqualHeightChanged", typeof (RadMenuElement))]
    public event EventHandler AllItemsEqualHeightChanged;

    [Category("Property Changed")]
    public event EventHandler TextOrientationChanged;

    public RadMenu()
    {
      this.AutoSize = true;
      this.SetStyle(ControlStyles.Selectable, false);
      this.Items.ItemsChanged += new ItemChangedDelegate(this.OnRadMenu_ItemsChanged);
      this.Initialized += new EventHandler(this.RadMenu_Initialized);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      base.InitializeRootElement(rootElement);
      int num1 = (int) rootElement.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
      int num2 = (int) rootElement.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      this.Dock = DockStyle.Top;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.menuElement = new RadMenuElement();
      this.menuElement.OrientationChanged += (EventHandler) ((sender, args) => this.OnOrientationChanged(args));
      this.menuElement.AllItemsEqualHeightChanged += (EventHandler) ((sender, args) => this.OnAllItemsEqualHeightChanged(args));
      this.menuElement.TextOrientationChanged += (EventHandler) ((sender, args) => this.OnTextOrientationChanged(args));
      parent.Children.Add((RadElement) this.menuElement);
    }

    public void SetMenuState(RadMenu.RadMenuState state)
    {
      RadMenu.RadMenuState menuState = this.menuState;
      this.menuState = state;
      this.OnMenuStateChanged(menuState, state);
    }

    private void ToggleKeyboardCues(RadMenuItem currentItem, bool value)
    {
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) currentItem.Items)
      {
        RadMenuItem currentItem1 = radMenuItemBase as RadMenuItem;
        if (currentItem1 != null)
          this.ToggleKeyboardCues(currentItem1, value);
      }
      if (currentItem == null)
        return;
      currentItem.ShowKeyboardCue = value;
    }

    private bool CheckMouseOverChildDropDown(IPopupControl popup)
    {
      if (popup.Bounds.Contains(Control.MousePosition))
        return true;
      using (List<IPopupControl>.Enumerator enumerator = popup.Children.GetEnumerator())
      {
        if (enumerator.MoveNext())
          return this.CheckMouseOverChildDropDown(enumerator.Current);
      }
      return false;
    }

    private void PerformItemClick(RadMenuItemBase item)
    {
      item.ShowChildItems();
      item.DropDown.SelectFirstVisibleItem();
      item.PerformClick();
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Defines whether RadMenu handles the MDI menu functionality.")]
    public bool IsMainMenu
    {
      get
      {
        return this.isMainMenu;
      }
      set
      {
        this.isMainMenu = value;
      }
    }

    private void PrepareMdiMenuStrip(Form parentForm)
    {
      if (parentForm.MainMenuStrip != null || !this.isMainMenu)
        return;
      parentForm.MainMenuStrip = (MenuStrip) new RadRibbonFormMainMenuStrip();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      this.RollOverItemSelection = true;
      Form form = this.FindForm();
      if (form != null)
      {
        this.parentForm = form;
        if (form.IsMdiContainer)
        {
          this.PrepareMdiMenuStrip(form);
          foreach (Control control1 in (ArrangedElementCollection) form.Controls)
          {
            if (control1 is MdiClient)
            {
              MdiClient mdiClient = control1 as MdiClient;
              foreach (Control control2 in (ArrangedElementCollection) mdiClient.Controls)
              {
                ControlEventArgs e1 = new ControlEventArgs(control2);
                this.MdiClient_ControlAdded((object) mdiClient, e1);
              }
              mdiClient.ControlAdded += new ControlEventHandler(this.MdiClient_ControlAdded);
              mdiClient.ControlRemoved += new ControlEventHandler(this.client_ControlRemoved);
              break;
            }
          }
          this.menuElement.MinimizeButton.Click += new EventHandler(this.MinimizeButton_Click);
          this.menuElement.MaximizeButton.Click += new EventHandler(this.MaximizeButton_Click);
          this.menuElement.CloseButton.Click += new EventHandler(this.CloseButton_Click);
          this.applicationMdiState.MdiParentForm = form;
        }
        if (form.IsMdiChild && form.WindowState == FormWindowState.Maximized)
          this.applicationMdiState.MaximizedChildForm = form;
      }
      base.OnHandleCreated(e);
    }

    private void client_ControlRemoved(object sender, ControlEventArgs e)
    {
      e.Control.SizeChanged -= new EventHandler(this.MdiChild_SizeChanged);
      e.Control.TextChanged -= new EventHandler(this.MdiChildFormToActivate_TextChanged);
      Form control = e.Control as Form;
      if (control != null)
        control.Shown -= new EventHandler(this.castedForm_Shown);
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase.MdiList)
        {
          radMenuItemBase.Click -= new EventHandler(this.item_Click);
          for (int index = radMenuItemBase.Items.Count - 1; index >= 0; --index)
          {
            if (radMenuItemBase.Items[index] is RadMenuItem && ((RadMenuItem) radMenuItemBase.Items[index]).MdiChildFormToActivate == e.Control)
            {
              radMenuItemBase.Items.RemoveAt(index);
              break;
            }
          }
        }
      }
      if (this.parentForm.MdiChildren.Length != 0)
        return;
      if (this.menuMergeApplied)
      {
        if (!this.applicationMdiState.MaximizedChildFormMenu.IsDisposed)
          this.UnmergeMenu(this.applicationMdiState.MaximizedChildFormMenu);
        this.menuMergeApplied = false;
      }
      this.applicationMdiState.MaximizedChildForm = (Form) null;
      this.applicationMdiState.MaximizedChildFormMenu = (RadMenu) null;
      this.menuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
    }

    private void MdiClient_ControlAdded(object sender, ControlEventArgs e)
    {
      e.Control.SizeChanged -= new EventHandler(this.MdiChild_SizeChanged);
      e.Control.SizeChanged += new EventHandler(this.MdiChild_SizeChanged);
      Form control = e.Control as Form;
      if (control != null)
      {
        control.Shown -= new EventHandler(this.castedForm_Shown);
        control.Shown += new EventHandler(this.castedForm_Shown);
      }
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase.MdiList)
        {
          bool flag = false;
          foreach (RadItem radItem in (RadItemCollection) radMenuItemBase.Items)
          {
            if (radItem.Text == e.Control.Text)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            RadMenuItem radMenuItem = new RadMenuItem(e.Control.Text);
            radMenuItemBase.IsMdiListItem = true;
            radMenuItem.MdiChildFormToActivate = (Form) e.Control;
            radMenuItem.Click -= new EventHandler(this.formItem_Click);
            radMenuItem.Click += new EventHandler(this.formItem_Click);
            radMenuItemBase.Items.Add((RadItem) radMenuItem);
            radMenuItemBase.Click -= new EventHandler(this.item_Click);
            radMenuItemBase.Click += new EventHandler(this.item_Click);
            radMenuItemBase.DropDownOpened -= new EventHandler(this.item_Click);
            radMenuItemBase.DropDownOpened += new EventHandler(this.item_Click);
            radMenuItem.MdiChildFormToActivate.TextChanged -= new EventHandler(this.MdiChildFormToActivate_TextChanged);
            radMenuItem.MdiChildFormToActivate.TextChanged += new EventHandler(this.MdiChildFormToActivate_TextChanged);
          }
        }
      }
    }

    private void castedForm_Shown(object sender, EventArgs e)
    {
      if (!(sender is Form) || ((Form) sender).WindowState != FormWindowState.Maximized)
        return;
      this.MdiChild_SizeChanged(sender, new EventArgs());
    }

    private void MdiChildFormToActivate_TextChanged(object sender, EventArgs e)
    {
      Form form = sender as Form;
      foreach (RadMenuItemBase radMenuItemBase1 in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase1.MdiList)
        {
          foreach (RadMenuItemBase radMenuItemBase2 in (RadItemCollection) radMenuItemBase1.Items)
          {
            RadMenuItem radMenuItem = radMenuItemBase2 as RadMenuItem;
            if (radMenuItem != null && radMenuItem.MdiChildFormToActivate != null && radMenuItem.MdiChildFormToActivate == form)
              radMenuItem.Text = form.Text;
          }
        }
      }
    }

    private void item_Click(object sender, EventArgs e)
    {
      RadMenuItem radMenuItem1 = sender as RadMenuItem;
      if (radMenuItem1 == null)
        return;
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) radMenuItem1.Items)
      {
        RadMenuItem radMenuItem2 = radMenuItemBase as RadMenuItem;
        if (radMenuItem2 != null && radMenuItem2.MdiChildFormToActivate != null)
          radMenuItem2.IsChecked = radMenuItem2.MdiChildFormToActivate == this.applicationMdiState.MdiParentForm.ActiveMdiChild;
      }
    }

    private void MdiChild_SizeChanged(object sender, EventArgs e)
    {
      Form form = sender as Form;
      if (form != null && form.WindowState == FormWindowState.Maximized && this.GetFirstRadMenuInForm(form) == null)
      {
        if (this.menuMergeApplied)
        {
          this.UnmergeMenu(this.applicationMdiState.MaximizedChildFormMenu);
          this.menuMergeApplied = false;
        }
        this.applicationMdiState.MaximizedChildForm = form;
        this.applicationMdiState.MaximizedChildFormMenu = (RadMenu) null;
        if (!this.isMainMenu)
          return;
        this.menuElement.SystemButtons.Visibility = ElementVisibility.Visible;
      }
      else if (form != null && form.WindowState != FormWindowState.Maximized && (!this.menuMergeApplied && this.applicationMdiState.MaximizedChildForm == null))
      {
        this.menuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (form != null && form.WindowState != FormWindowState.Maximized && (!this.menuMergeApplied && this.applicationMdiState.MaximizedChildForm != null))
        {
          bool flag = true;
          foreach (Form mdiChild in this.applicationMdiState.MdiParentForm.MdiChildren)
          {
            if (mdiChild.WindowState == FormWindowState.Maximized)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            this.menuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
            if (this.applicationMdiState.MaximizedChildFormMenu != null)
              this.applicationMdiState.MaximizedChildFormMenu.Visible = true;
            this.applicationMdiState.MaximizedChildForm = (Form) null;
            this.applicationMdiState.MaximizedChildFormMenu = (RadMenu) null;
            return;
          }
        }
        if (form != null && form.WindowState != FormWindowState.Maximized && form == this.applicationMdiState.MaximizedChildForm)
        {
          if (this.menuMergeApplied)
          {
            this.UnmergeMenu(this.applicationMdiState.MaximizedChildFormMenu);
            this.menuMergeApplied = false;
          }
          if (this.applicationMdiState.MaximizedChildFormMenu != null)
            this.applicationMdiState.MaximizedChildFormMenu.Visible = true;
          this.applicationMdiState.MaximizedChildForm = (Form) null;
          this.applicationMdiState.MaximizedChildFormMenu = (RadMenu) null;
          this.MenuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
        }
        else if (form != null && form.WindowState == FormWindowState.Maximized && this.applicationMdiState.MaximizedChildForm == null)
        {
          this.applicationMdiState.MaximizedChildForm = form;
          if (this.menuElement.SystemButtons.Visibility != ElementVisibility.Visible && this.isMainMenu)
            this.menuElement.SystemButtons.Visibility = ElementVisibility.Visible;
          RadMenu firstRadMenuInForm = this.GetFirstRadMenuInForm(this.applicationMdiState.MaximizedChildForm);
          this.applicationMdiState.MaximizedChildFormMenu = firstRadMenuInForm;
          if (firstRadMenuInForm == null || this.menuMergeApplied || (!this.AllowMerge || !firstRadMenuInForm.AllowMerge))
            return;
          this.sourceMenuUponMerge = firstRadMenuInForm;
          this.MergeMenu(firstRadMenuInForm);
          firstRadMenuInForm.Visible = false;
          this.menuMergeApplied = true;
        }
        else
        {
          if (form == null || form.WindowState != FormWindowState.Maximized || (this.applicationMdiState.MaximizedChildForm == null || this.applicationMdiState.MaximizedChildForm == form))
            return;
          if (this.menuMergeApplied)
          {
            this.menuMergeApplied = false;
            if (this.applicationMdiState.MaximizedChildFormMenu != null && !this.applicationMdiState.MaximizedChildFormMenu.IsDisposed)
            {
              this.UnmergeMenu(this.applicationMdiState.MaximizedChildFormMenu);
              this.applicationMdiState.MaximizedChildFormMenu.Visible = true;
            }
          }
          RadMenu firstRadMenuInForm = this.GetFirstRadMenuInForm(form);
          if (firstRadMenuInForm != null)
          {
            if (this.AllowMerge && firstRadMenuInForm.AllowMerge)
            {
              this.sourceMenuUponMerge = firstRadMenuInForm;
              this.MergeMenu(firstRadMenuInForm);
              this.menuMergeApplied = true;
              firstRadMenuInForm.Visible = false;
            }
            this.applicationMdiState.MaximizedChildForm = form;
            this.applicationMdiState.MaximizedChildFormMenu = firstRadMenuInForm;
          }
          if (!this.isMainMenu)
            return;
          this.MenuElement.SystemButtons.Visibility = ElementVisibility.Visible;
        }
      }
    }

    private RadMenu GetFirstRadMenuInForm(Form form)
    {
      RadMenu radMenu = (RadMenu) null;
      foreach (Control control in (ArrangedElementCollection) form.Controls)
      {
        if (control is RadMenu)
        {
          radMenu = (RadMenu) control;
          break;
        }
      }
      return radMenu;
    }

    private void OnParentForm_Deactivate(object sender, EventArgs e)
    {
      this.SetMenuState(RadMenu.RadMenuState.NotActive);
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      if (this.applicationMdiState.MaximizedChildForm == null && this.applicationMdiState.MdiParentForm != null && (this.applicationMdiState.MdiParentForm.ActiveMdiChild != null && this.applicationMdiState.MdiParentForm.ActiveMdiChild.WindowState == FormWindowState.Maximized))
        this.applicationMdiState.MaximizedChildForm = this.applicationMdiState.MdiParentForm.ActiveMdiChild;
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase.MdiList)
        {
          for (int index = 0; index < radMenuItemBase.Items.Count; ++index)
          {
            RadMenuItem radMenuItem = radMenuItemBase.Items[index] as RadMenuItem;
            if (radMenuItem != null && radMenuItem.MdiChildFormToActivate == this.applicationMdiState.MaximizedChildForm)
            {
              radMenuItemBase.Items.Remove((RadItem) radMenuItem);
              break;
            }
          }
        }
      }
      this.applicationMdiState.MaximizedChildForm.Close();
      ElementVisibility elementVisibility = ElementVisibility.Collapsed;
      foreach (Form mdiChild in this.applicationMdiState.MdiParentForm.MdiChildren)
      {
        if (mdiChild.Visible && mdiChild.WindowState == FormWindowState.Maximized)
        {
          elementVisibility = ElementVisibility.Visible;
          if (this.applicationMdiState.MaximizedChildForm == null)
            this.applicationMdiState.MaximizedChildForm = mdiChild;
        }
      }
      this.menuElement.SystemButtons.Visibility = elementVisibility;
    }

    private void MaximizeButton_Click(object sender, EventArgs e)
    {
      this.applicationMdiState.MaximizedChildForm.WindowState = FormWindowState.Normal;
      this.menuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
    }

    private void MinimizeButton_Click(object sender, EventArgs e)
    {
      this.applicationMdiState.MaximizedChildForm.WindowState = FormWindowState.Minimized;
      this.menuElement.SystemButtons.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase.MdiList)
        {
          for (int index = radMenuItemBase.Items.Count - 1; index >= 0; --index)
          {
            if (((RadMenuItemBase) radMenuItemBase.Items[index]).IsMdiListItem)
            {
              radMenuItemBase.Items[index].Click -= new EventHandler(this.formItem_Click);
              radMenuItemBase.Items.RemoveAt(index);
            }
          }
        }
      }
      if (this.parentForm != null && this.parentForm.IsMdiContainer)
      {
        this.menuElement.MinimizeButton.Click -= new EventHandler(this.MinimizeButton_Click);
        this.menuElement.MaximizeButton.Click -= new EventHandler(this.MaximizeButton_Click);
        this.menuElement.CloseButton.Click -= new EventHandler(this.CloseButton_Click);
        foreach (Control control in (ArrangedElementCollection) this.parentForm.Controls)
        {
          if (control is MdiClient)
          {
            MdiClient mdiClient = control as MdiClient;
            mdiClient.ControlAdded -= new ControlEventHandler(this.MdiClient_ControlAdded);
            mdiClient.ControlRemoved -= new ControlEventHandler(this.client_ControlRemoved);
            break;
          }
        }
      }
      base.OnHandleDestroyed(e);
    }

    private void formItem_Click(object sender, EventArgs e)
    {
      RadMenuItem radMenuItem = (RadMenuItem) sender;
      if (radMenuItem.MdiChildFormToActivate == null || radMenuItem.MdiChildFormToActivate.MdiParent == null)
        return;
      radMenuItem.MdiChildFormToActivate.Activate();
    }

    public virtual void MergeMenu(RadMenu sourceMenu)
    {
      if (this == sourceMenu)
        throw new ArgumentException("Menu self merging");
      this.menuMergeApplied = true;
      int count = this.Items.Count;
      int[] numArray = new int[count];
      for (int index = 0; index < count; ++index)
        numArray[index] = ((RadMenuItemBase) this.Items[index]).Items.Count;
      this.PersistMenuItemsInChildMenuUponMenuMerge(sourceMenu);
      foreach (RadMenuItemBase radMenuItemBase1 in sourceMenu.persistedItemsUponMenuMerge)
      {
        radMenuItemBase1.IsParticipatingInMerge = true;
        switch (radMenuItemBase1.MergeType)
        {
          case MenuMerge.Add:
            int index1 = count;
            for (int index2 = count; index2 < this.Items.Count && ((RadMenuItemBase) this.Items[index2]).MergeOrder <= radMenuItemBase1.MergeOrder; ++index2)
              ++index1;
            this.Items.Insert(index1, (RadItem) radMenuItemBase1);
            continue;
          case MenuMerge.Replace:
            int index3 = sourceMenu.persistedItemsUponMenuMerge.IndexOf(radMenuItemBase1);
            if (index3 < this.Items.Count)
            {
              RadMenuItemBase radMenuItemBase2 = (RadMenuItemBase) this.Items[index3];
              if (radMenuItemBase2.MergeType != MenuMerge.Add)
              {
                radMenuItemBase2.PositionToBeRestoredAfterMerge = index3;
                this.itemsToBeRestoredAfterUnmerge.Add(radMenuItemBase2);
                this.Items.RemoveAt(index3);
                this.Items.Insert(index3, (RadItem) radMenuItemBase1);
                continue;
              }
              this.Items.Add((RadItem) radMenuItemBase1);
              continue;
            }
            this.Items.Add((RadItem) radMenuItemBase1);
            continue;
          case MenuMerge.MergeItems:
            int index4 = sourceMenu.persistedItemsUponMenuMerge.IndexOf(radMenuItemBase1);
            if (index4 < this.Items.Count)
            {
              RadMenuItemBase radMenuItemBase2 = (RadMenuItemBase) this.Items[index4];
              if (radMenuItemBase2.MergeType != MenuMerge.Add)
              {
                using (RadItemCollection.RadItemEnumerator enumerator = radMenuItemBase1.Items.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    RadMenuItemBase current = (RadMenuItemBase) enumerator.Current;
                    int index2 = numArray[index4];
                    for (int index5 = index2; index5 < radMenuItemBase2.Items.Count && ((RadMenuItemBase) radMenuItemBase2.Items[index5]).MergeOrder <= current.MergeOrder; ++index5)
                      ++index2;
                    current.IsParticipatingInMerge = true;
                    radMenuItemBase2.Items.Insert(index2, (RadItem) current);
                  }
                  continue;
                }
              }
              else
              {
                this.Items.Add((RadItem) radMenuItemBase1);
                continue;
              }
            }
            else
            {
              this.Items.Add((RadItem) radMenuItemBase1);
              continue;
            }
          case MenuMerge.Remove:
            radMenuItemBase1.IsParticipatingInMerge = false;
            continue;
          default:
            continue;
        }
      }
      this.menuMergeApplied = true;
      this.sourceMenuUponMerge = sourceMenu;
    }

    public void UnmergeMenu(RadMenu src)
    {
      if (src == null)
        return;
      this.menuMergeApplied = false;
      for (int index1 = this.Items.Count - 1; index1 >= 0; --index1)
      {
        RadMenuItemBase radMenuItemBase1 = this.Items[index1] as RadMenuItemBase;
        for (int index2 = radMenuItemBase1.Items.Count - 1; index2 >= 0; --index2)
        {
          RadMenuItemBase radMenuItemBase2 = radMenuItemBase1.Items[index2] as RadMenuItemBase;
          if (radMenuItemBase2.IsParticipatingInMerge)
          {
            radMenuItemBase2.IsParticipatingInMerge = false;
            radMenuItemBase1.Items.RemoveAt(index2);
          }
        }
        if (radMenuItemBase1.IsParticipatingInMerge)
        {
          radMenuItemBase1.IsParticipatingInMerge = false;
          this.Items.RemoveAt(index1);
        }
      }
      foreach (RadMenuItemBase radMenuItemBase in this.itemsToBeRestoredAfterUnmerge)
      {
        radMenuItemBase.IsParticipatingInMerge = false;
        this.Items.Insert(radMenuItemBase.PositionToBeRestoredAfterMerge, (RadItem) radMenuItemBase);
      }
      this.itemsToBeRestoredAfterUnmerge.Clear();
      this.RestoreMenuItemsInChildMenuUponMenuUnmerge(src);
      this.sourceMenuUponMerge = (RadMenu) null;
      this.menuMergeApplied = false;
    }

    internal void PersistMenuItemsInChildMenuUponMenuMerge(RadMenu src)
    {
      src.persistedItemsUponMenuMerge.Clear();
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) src.Items)
        src.persistedItemsUponMenuMerge.Add(radMenuItemBase);
      for (int index = src.Items.Count - 1; index >= 0; --index)
        src.Items.RemoveAt(index);
    }

    internal void RestoreMenuItemsInChildMenuUponMenuUnmerge(RadMenu src)
    {
      for (int index = src.Items.Count - 1; index >= 0; --index)
        src.Items.RemoveAt(index);
      foreach (RadMenuItemBase menuItem in src.persistedItemsUponMenuMerge)
      {
        src.Items.Add((RadItem) menuItem);
        this.UpdateItemsOwner(menuItem);
      }
    }

    private void UpdateItemsOwner(RadMenuItemBase menuItem)
    {
      RadElement owner = menuItem.Items.Owner;
      menuItem.Items.Owner = (RadElement) null;
      menuItem.Items.Owner = owner;
      foreach (RadMenuItemBase menuItem1 in (RadItemCollection) menuItem.Items)
        this.UpdateItemsOwner(menuItem1);
    }

    InstalledHook IMessageListener.DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage;
      }
    }

    private bool CanProcessMessage(ref Message msg)
    {
      if (Control.FromHandle(msg.HWnd) == null)
        return false;
      Form form = this.FindForm();
      return this.ForceMessageListener || form == null || object.ReferenceEquals((object) form, (object) Form.ActiveForm);
    }

    MessagePreviewResult IMessageListener.PreviewMessage(
      ref Message msg)
    {
      switch (msg.Msg)
      {
        case 162:
        case 165:
        case 168:
        case 514:
        case 517:
        case 520:
          this.PreprocessMouseEvent(ref msg);
          return MessagePreviewResult.Processed;
        case 256:
          if (!this.CanProcessMessage(ref msg))
            return MessagePreviewResult.Processed;
          Keys keyData = (Keys) (long) msg.WParam | Control.ModifierKeys;
          bool flag = this.menuState == RadMenu.RadMenuState.KeyboardActive || this.menuState == RadMenu.RadMenuState.CuesVisibleKeyboardActive;
          if (flag && this.ProcessCmdKey(ref msg, keyData))
            return MessagePreviewResult.All;
          if (flag && this.IsInputKey(keyData))
            return MessagePreviewResult.ProcessedNoDispatch;
          if (flag && this.ProcessDialogKey(keyData))
            return MessagePreviewResult.All;
          return flag && this.OnHandleKeyDown(msg) ? MessagePreviewResult.ProcessedNoDispatch : MessagePreviewResult.Processed;
        case 258:
          if (!this.CanProcessMessage(ref msg))
            return MessagePreviewResult.Processed;
          uint wparam1 = (uint) (int) msg.WParam;
          return wparam1 < 0U || wparam1 > (uint) ushort.MaxValue || !this.ProcessMnemonic((char) wparam1) ? MessagePreviewResult.Processed : MessagePreviewResult.All;
        case 260:
          if (!this.CanProcessMessage(ref msg))
            return MessagePreviewResult.Processed;
          Keys wparam2 = (Keys) (int) msg.WParam;
          char result;
          char.TryParse(wparam2.ToString(), out result);
          if ((this.menuState == RadMenu.RadMenuState.CuesVisible || this.menuState == RadMenu.RadMenuState.CuesVisibleKeyboardActive) && this.CanProcessMnemonic(result))
          {
            this.SetMenuState(RadMenu.RadMenuState.CuesVisibleKeyboardActive);
            this.ProcessMnemonic(result);
            return MessagePreviewResult.All;
          }
          return (wparam2 == Keys.Menu || wparam2 == Keys.Alt) && this.ProcessFirstStageMnemonicActivation(ref msg, wparam2) ? MessagePreviewResult.All : MessagePreviewResult.Processed;
        case 261:
          if (!this.CanProcessMessage(ref msg))
            return MessagePreviewResult.Processed;
          Keys wparam3 = (Keys) (int) msg.WParam;
          int num;
          switch (wparam3)
          {
            case Keys.Menu:
            case Keys.F10:
              num = 1;
              break;
            default:
              num = wparam3 == Keys.Alt ? 1 : 0;
              break;
          }
          return num != 0 && this.ProcessSecondStageMnemonicActivation(ref msg, (Keys) (int) msg.WParam) ? MessagePreviewResult.All : MessagePreviewResult.Processed;
        default:
          return MessagePreviewResult.Processed;
      }
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
    }

    protected virtual void OnMenuStateChanged(
      RadMenu.RadMenuState oldState,
      RadMenu.RadMenuState newState)
    {
      switch (newState)
      {
        case RadMenu.RadMenuState.CuesVisible:
          this.ProcessKeyboard = false;
          this.ShowMenuKeyboardCues = true;
          this.highLightCycleCompleted = false;
          break;
        case RadMenu.RadMenuState.CuesVisibleKeyboardActive:
          this.ProcessKeyboard = true;
          this.highLightCycleCompleted = true;
          this.ShowMenuKeyboardCues = true;
          break;
        case RadMenu.RadMenuState.KeyboardActive:
          this.ProcessKeyboard = true;
          this.ShowMenuKeyboardCues = false;
          this.highLightCycleCompleted = false;
          break;
        case RadMenu.RadMenuState.NotActive:
          this.ShowMenuKeyboardCues = false;
          this.shouldShowChildren = false;
          this.highLightCycleCompleted = false;
          this.ProcessKeyboard = false;
          (this.GetSelectedItem() as RadMenuItemBase)?.Deselect();
          break;
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      Form form = this.FindForm();
      if (form != null)
        form.Deactivate += new EventHandler(this.OnParentForm_Deactivate);
      if (this.IsDesignMode && !this.ForceMessageListener)
        return;
      RadMessageFilter.Instance.AddListener((IMessageListener) this);
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      return false;
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.SetMenuState(RadMenu.RadMenuState.NotActive);
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (radMenuItemBase.HasChildren && radMenuItemBase.IsPopupShown)
          radMenuItemBase.HideChildItems();
      }
    }

    protected virtual void PreprocessMouseEvent(ref Message msg)
    {
      Point pt = new Point(msg.LParam.ToInt32());
      RadMenuItemBase currentProcessedItem = this.GetCurrentProcessedItem((IItemsControl) this);
      if (currentProcessedItem == null)
        return;
      if (this.Bounds.Contains(pt) && this.menuState == RadMenu.RadMenuState.CuesVisible)
      {
        if (currentProcessedItem.IsPopupShown && !currentProcessedItem.ControlBoundingRectangle.Contains(pt))
        {
          this.SetMenuState(RadMenu.RadMenuState.NotActive);
          return;
        }
        if (!currentProcessedItem.ControlBoundingRectangle.Contains(pt))
        {
          this.SetMenuState(RadMenu.RadMenuState.NotActive);
          currentProcessedItem.Deselect();
          return;
        }
      }
      if (currentProcessedItem.HasChildren && currentProcessedItem.IsOnDropDown)
        return;
      this.SetMenuState(RadMenu.RadMenuState.NotActive);
    }

    protected override bool ProcessUpDownArrowKey(bool down)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem == null || selectedItem.IsPopupShown)
        return false;
      if (!selectedItem.HasChildren || !selectedItem.Enabled)
        return base.ProcessUpDownArrowKey(down);
      selectedItem.ShowChildItems();
      selectedItem.DropDown.SelectFirstVisibleItem();
      return true;
    }

    protected virtual bool CanProcessItem(RadMenuItemBase menuItem)
    {
      if (menuItem != null)
        return menuItem.Enabled;
      return false;
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData != Keys.Return)
        return base.ProcessDialogKey(keyData);
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (!this.CanProcessItem(selectedItem))
        return false;
      if (!selectedItem.IsPopupShown)
      {
        this.BeginInvoke((Delegate) new RadDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) selectedItem);
        return true;
      }
      RadMenuItemBase currentProcessedItem = this.GetCurrentProcessedItem((IItemsControl) this);
      if (!this.CanProcessItem(currentProcessedItem) || currentProcessedItem.HasChildren)
        return false;
      selectedItem.Deselect();
      this.SetMenuState(RadMenu.RadMenuState.NotActive);
      return false;
    }

    protected override bool CallBaseProcessDialogKey(Keys keyData)
    {
      return false;
    }

    protected virtual RadMenuItemBase GetCurrentProcessedItem(
      IItemsControl itemsControl)
    {
      foreach (RadItem radItem in (RadItemCollection) itemsControl.Items)
      {
        RadMenuItemBase radMenuItemBase = radItem as RadMenuItemBase;
        if (radMenuItemBase != null && radMenuItemBase.HasChildren && radMenuItemBase.IsPopupShown)
          return this.GetCurrentProcessedItem((IItemsControl) radMenuItemBase.DropDown);
      }
      return itemsControl.GetSelectedItem() as RadMenuItemBase;
    }

    protected override bool ProcessArrowKey(Keys keyCode)
    {
      bool flag1 = false;
      switch (keyCode)
      {
        case Keys.Left:
        case Keys.Right:
          bool flag2;
          if (this.Orientation == Orientation.Horizontal)
          {
            flag2 = this.ProcessLeftRightArrowKey(keyCode == Keys.Right ^ this.RightToLeft == RightToLeft.Yes);
          }
          else
          {
            RadMenuItemBase currentProcessedItem = this.GetCurrentProcessedItem((IItemsControl) this);
            RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
            if (selectedItem == null || currentProcessedItem == null)
              return false;
            if (selectedItem.IsPopupShown && (object.ReferenceEquals((object) currentProcessedItem.HierarchyParent, (object) selectedItem) || !currentProcessedItem.HasChildren))
              return this.ProcessLeftRightArrowKey(keyCode == Keys.Right);
            flag2 = this.ProcessUpDownArrowKey(keyCode == Keys.Left);
          }
          return flag2;
        case Keys.Up:
        case Keys.Down:
          bool flag3;
          if (this.Orientation == Orientation.Horizontal)
          {
            flag3 = this.ProcessUpDownArrowKey(keyCode == Keys.Down);
          }
          else
          {
            RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
            flag3 = (selectedItem == null || !selectedItem.IsPopupShown) && this.ProcessLeftRightArrowKey(keyCode == Keys.Down);
          }
          return flag3;
        default:
          return flag1;
      }
    }

    protected override bool ProcessLeftRightArrowKey(bool right)
    {
      RadMenuItemBase selectedItem1 = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem1 == null)
        return base.ProcessLeftRightArrowKey(right);
      RadMenuItemBase currentProcessedItem = this.GetCurrentProcessedItem((IItemsControl) this);
      if (currentProcessedItem != null && currentProcessedItem.ElementTree.Control is RadDropDownMenu && (currentProcessedItem.ElementTree.Control as RadDropDownMenu).CanNavigate(right ? Keys.Right : Keys.Left))
        return false;
      bool flag = base.ProcessLeftRightArrowKey(right);
      selectedItem1.HideChildItems();
      RadMenuItemBase selectedItem2 = this.GetSelectedItem() as RadMenuItemBase;
      if (this.CanProcessItem(selectedItem2) && this.shouldShowChildren && selectedItem2.HasChildren)
      {
        selectedItem2.ShowChildItems();
        selectedItem2.DropDown.SelectFirstVisibleItem();
      }
      return flag;
    }

    protected virtual void PerformMouseDown(RadMenuItemBase menuItem)
    {
      if (this.menuState == RadMenu.RadMenuState.NotActive || this.menuState == RadMenu.RadMenuState.CuesVisible)
      {
        if (this.menuState == RadMenu.RadMenuState.CuesVisible)
          this.SetMenuState(RadMenu.RadMenuState.CuesVisibleKeyboardActive);
        else
          this.SetMenuState(RadMenu.RadMenuState.KeyboardActive);
      }
      if (!this.CanProcessItem(menuItem) || menuItem == this.GetSelectedItem())
        return;
      menuItem.Select();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.PerformMouseDown(this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase);
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      RadMenuItemBase elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase;
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (!this.CanProcessItem(elementAtPoint))
      {
        base.OnMouseMove(e);
      }
      else
      {
        if (selectedItem != null && !object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
        {
          selectedItem.Deselect();
          selectedItem.HideChildItems();
        }
        if (this.shouldShowChildren && !object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
          elementAtPoint.ShowChildItems();
        base.OnMouseMove(e);
      }
    }

    private void OnRadMenu_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation != ItemsChangeOperation.Inserted)
        return;
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase == null || this.IsDesignMode)
        return;
      int num = (int) radMenuItemBase.SetDefaultValueOverride(RadMenuItemBase.PopupDirectionProperty, (object) RadDirection.Down);
      if (!(radMenuItemBase is RadMenuItem))
        return;
      (radMenuItemBase as RadMenuItem).ShowKeyboardCue = false;
    }

    protected override void OnItemSelected(ItemSelectedEventArgs args)
    {
      base.OnItemSelected(args);
      if (!(args.Item is RadMenuItemBase))
        return;
      RadMenuItemBase radMenuItemBase = args.Item as RadMenuItemBase;
      radMenuItemBase.Selected = true;
      this.AccessibilityNotifyClients(AccessibleEvents.Focus, this.Items.IndexOf((RadItem) radMenuItemBase));
    }

    protected override void OnItemDeselected(ItemSelectedEventArgs args)
    {
      base.OnItemDeselected(args);
      if (!(args.Item is RadMenuItemBase))
        return;
      RadMenuItemBase radMenuItemBase = args.Item as RadMenuItemBase;
      radMenuItemBase.Selected = false;
      this.shouldShowChildren = radMenuItemBase.IsPopupShown || (!radMenuItemBase.HasChildren || !radMenuItemBase.Enabled) && this.prevItemWithChildren;
      if (!radMenuItemBase.HasChildren || !radMenuItemBase.Enabled)
        return;
      this.prevItemWithChildren = radMenuItemBase.IsPopupShown;
    }

    protected virtual void OnOrientationChanged(EventArgs args)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, args);
    }

    protected virtual void OnAllItemsEqualHeightChanged(EventArgs args)
    {
      if (this.AllItemsEqualHeightChanged == null)
        return;
      this.AllItemsEqualHeightChanged((object) this, args);
    }

    protected virtual void OnTextOrientationChanged(EventArgs args)
    {
      if (this.TextOrientationChanged == null)
        return;
      this.TextOrientationChanged((object) this, args);
    }

    private void RadMenu_Initialized(object sender, EventArgs e)
    {
      if (this.IsDesignMode)
        return;
      this.SetMenuState(RadMenu.RadMenuState.NotActive);
    }

    internal bool Activated
    {
      get
      {
        return this.activated;
      }
      set
      {
        if (this.activated == value)
          return;
        this.activated = value;
      }
    }

    private bool FindAndSelectItem(char keyData)
    {
      RadMenuItemBase selectedItem1 = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem1 != null && selectedItem1.IsPopupShown)
      {
        if (selectedItem1.DropDown.CanProcessMnemonic(keyData))
          this.SetMenuState(RadMenu.RadMenuState.NotActive);
        return false;
      }
      List<RadItem> radItemList = new List<RadItem>();
      int num = -1;
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        char charCode = keyData;
        if (radMenuItemBase.Enabled && Control.IsMnemonic(charCode, radMenuItemBase.Text) && radMenuItemBase.Visibility == ElementVisibility.Visible)
        {
          radItemList.Add((RadItem) radMenuItemBase);
          if (selectedItem1 == radMenuItemBase)
            num = radItemList.Count - 1;
        }
      }
      if (radItemList.Count == 1)
      {
        RadMenuItemBase selectedItem2 = this.GetSelectedItem() as RadMenuItemBase;
        if (selectedItem2 != null && selectedItem2.IsPopupShown)
          selectedItem2.HideChildItems();
        radItemList[0].Select();
        RadMenuItemBase selectedItem3 = this.GetSelectedItem() as RadMenuItemBase;
        if (selectedItem3 != null)
          this.BeginInvoke((Delegate) new RadDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) selectedItem3);
        return true;
      }
      if (radItemList.Count <= 0)
        return false;
      if (selectedItem1 != null && selectedItem1.IsPopupShown)
        selectedItem1.HideChildItems();
      int index = (num + 1) % radItemList.Count;
      radItemList[index].Focus();
      radItemList[index].Select();
      return true;
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Indicates whether the menu items should be stretched to fill the available space.")]
    [Browsable(true)]
    public bool StretchItems
    {
      get
      {
        if (this.menuElement != null && this.menuElement.ItemsLayout != null)
          return this.menuElement.ItemsLayout.StretchItems;
        return false;
      }
      set
      {
        if (this.menuElement == null || this.menuElement.ItemsLayout == null)
          return;
        this.menuElement.ItemsLayout.StretchItems = value;
      }
    }

    internal bool ShowMenuKeyboardCues
    {
      get
      {
        return this.showKeyboardCues;
      }
      set
      {
        if (this.IsDesignMode || value == this.showKeyboardCues)
          return;
        this.showKeyboardCues = value;
        foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
        {
          RadMenuItem currentItem = radMenuItemBase as RadMenuItem;
          if (currentItem != null)
            this.ToggleKeyboardCues(currentItem, value);
        }
      }
    }

    internal bool HighLightCycleCompleted
    {
      get
      {
        return this.highLightCycleCompleted;
      }
      set
      {
        this.highLightCycleCompleted = value;
      }
    }

    [Description("Gets or sets whether the Alt or F10 can be used to highlight the menu.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool SystemKeyHighlight
    {
      get
      {
        return this.systemKeyHighlight;
      }
      set
      {
        if (this.systemKeyHighlight == value)
          return;
        this.systemKeyHighlight = value;
        this.OnNotifyPropertyChanged(nameof (SystemKeyHighlight));
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 24));
      }
    }

    [DefaultValue(true)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadMenuElement MenuElement
    {
      get
      {
        return this.menuElement;
      }
    }

    [RadEditItemsAction]
    [RadNewItem("Type here", true, true, false)]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override RadItemOwnerCollection Items
    {
      get
      {
        return this.menuElement.Items;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("ToolStripAllowMergeDescr")]
    public bool AllowMerge
    {
      get
      {
        return this.menuElement.AllowMerge;
      }
      set
      {
        this.menuElement.AllowMerge = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [RadDescription("Orientation", typeof (RadMenuElement))]
    [RadDefaultValue("Orientation", typeof (RadMenuElement))]
    public Orientation Orientation
    {
      get
      {
        return this.menuElement.Orientation;
      }
      set
      {
        if (value == this.menuElement.Orientation)
          return;
        this.menuElement.Orientation = value;
        if (value == Orientation.Horizontal)
        {
          if (this.RootElement != null)
          {
            int num1 = (int) this.RootElement.ResetValue(RadElement.StretchVerticallyProperty, ValueResetFlags.Local);
            int num2 = (int) this.RootElement.ResetValue(RadElement.StretchHorizontallyProperty, ValueResetFlags.Local);
          }
          if (this.Dock == DockStyle.Left)
            this.Dock = DockStyle.Top;
          if (this.Dock != DockStyle.Right)
            return;
          this.Dock = DockStyle.Bottom;
        }
        else
        {
          if (this.RootElement != null)
          {
            this.RootElement.StretchHorizontally = false;
            this.RootElement.StretchVertically = true;
          }
          if (this.Dock == DockStyle.Top)
            this.Dock = DockStyle.Left;
          if (this.Dock != DockStyle.Bottom)
            return;
          this.Dock = DockStyle.Right;
        }
      }
    }

    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
      }
    }

    [RadDescription("AllItemsEqualHeight", typeof (RadMenuElement))]
    [Category("Behavior")]
    [Browsable(true)]
    [RadDefaultValue("AllItemsEqualHeight", typeof (RadMenuElement))]
    public bool AllItemsEqualHeight
    {
      get
      {
        return this.menuElement.AllItemsEqualHeight;
      }
      set
      {
        this.menuElement.AllItemsEqualHeight = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [RadDefaultValue("DropDownAnimationEnabled", typeof (RadMenuElement))]
    public bool DropDownAnimationEnabled
    {
      get
      {
        return this.menuElement.DropDownAnimationEnabled;
      }
      set
      {
        this.menuElement.DropDownAnimationEnabled = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [RadDefaultValue("DropDownAnimationEasing", typeof (RadMenuElement))]
    public RadEasingType DropDownAnimationEasing
    {
      get
      {
        return this.menuElement.DropDownAnimationEasing;
      }
      set
      {
        this.menuElement.DropDownAnimationEasing = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [RadDefaultValue("DropDownAnimationFrames", typeof (RadMenuElement))]
    public int DropDownAnimationFrames
    {
      get
      {
        return this.menuElement.DropDownAnimationFrames;
      }
      set
      {
        this.menuElement.DropDownAnimationFrames = value;
      }
    }

    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    protected virtual RadMenuItemBase GetSysCharItem(
      IItemsControl itemsControl,
      char searchKey)
    {
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) itemsControl.Items)
      {
        if (radMenuItemBase.Enabled && Control.IsMnemonic(searchKey, radMenuItemBase.Text))
          return radMenuItemBase;
        if (radMenuItemBase.HasChildren && radMenuItemBase.DropDown != null)
        {
          RadMenuItemBase sysCharItem = this.GetSysCharItem((IItemsControl) radMenuItemBase.DropDown, searchKey);
          if (sysCharItem != null)
            return sysCharItem;
        }
      }
      return (RadMenuItemBase) null;
    }

    protected override bool ProcessCmdKey(ref Message m, Keys keyData)
    {
      if (keyData == Keys.Escape && PopupManager.Default.PopupCount == 0)
      {
        this.SetMenuState(RadMenu.RadMenuState.NotActive);
        (this.GetSelectedItem() as RadMenuItemBase)?.Deselect();
        return true;
      }
      if (m.WParam == (IntPtr) 13)
        return false;
      return base.ProcessCmdKey(ref m, keyData);
    }

    protected virtual bool ProcessFirstStageMnemonicActivation(ref Message m, Keys keyData)
    {
      if (Convert.ToBoolean((int) m.LParam & 1073741824) || !this.Visible || !this.systemKeyHighlight)
        return false;
      if (this.menuState == RadMenu.RadMenuState.CuesVisibleKeyboardActive)
      {
        RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
        if (selectedItem != null)
        {
          selectedItem.HideChildItems();
          selectedItem.Deselect();
        }
        this.SetMenuState(RadMenu.RadMenuState.NotActive);
      }
      else
        this.SetMenuState(RadMenu.RadMenuState.CuesVisible);
      return true;
    }

    protected virtual bool ProcessSecondStageMnemonicActivation(ref Message m, Keys keyData)
    {
      if (!this.Visible || !this.systemKeyHighlight)
        return false;
      if (keyData == Keys.F10)
      {
        if (this.menuState == RadMenu.RadMenuState.NotActive)
        {
          this.SetMenuState(RadMenu.RadMenuState.CuesVisibleKeyboardActive);
          this.SelectFirstVisibleItem();
          return true;
        }
        (this.GetSelectedItem() as RadMenuItemBase)?.HideChildItems();
        this.SetMenuState(RadMenu.RadMenuState.NotActive);
        return true;
      }
      if (this.menuState != RadMenu.RadMenuState.CuesVisible)
        return false;
      this.SetMenuState(RadMenu.RadMenuState.CuesVisibleKeyboardActive);
      this.SelectFirstVisibleItem();
      return true;
    }

    public override bool CanProcessMnemonic(char keyData)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem != null && selectedItem.IsPopupShown)
        return false;
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) this.Items)
      {
        if (Control.IsMnemonic(keyData, radMenuItemBase.Text))
          return true;
      }
      return false;
    }

    protected override bool ProcessMnemonic(char charCode)
    {
      return (this.menuState == RadMenu.RadMenuState.CuesVisibleKeyboardActive || this.menuState == RadMenu.RadMenuState.CuesVisible) && this.FindAndSelectItem(charCode);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
        this.Initialized -= new EventHandler(this.RadMenu_Initialized);
        this.Items.ItemsChanged -= new ItemChangedDelegate(this.OnRadMenu_ItemsChanged);
        Form form = this.FindForm();
        if (form != null)
          form.Deactivate -= new EventHandler(this.OnParentForm_Deactivate);
      }
      base.Dispose(disposing);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.MenuElement.SuspendApplyOfThemeSettings();
      this.MenuElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.MenuElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "", "MenuFill");
      this.MenuElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, "", "MenuFill");
      this.MenuElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.MenuElement.SuspendApplyOfThemeSettings();
      this.MenuElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.MenuElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      int num = (int) this.MenuElement.MenuFill.ResetValue(FillPrimitive.GradientStyleProperty, ValueResetFlags.Style);
      this.MenuElement.ElementTree.ApplyThemeToElementTree();
      this.MenuElement.ResumeApplyOfThemeSettings();
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadMenu.RadMenuRootElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadMenuAccessibleObject(this);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue)
      {
        if (request.Message == "ItemsCount")
        {
          request.Data = (object) this.Items.Count;
          return;
        }
        if (request.Message == "HasChildNodes")
        {
          request.Data = (object) (this.Items.Count > 0);
          return;
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Data != null)
      {
        this.GetChildPropertyValueRecursivly(request, this.AccessibilityObject);
        return;
      }
      base.ProcessCodedUIMessage(ref request);
    }

    private void GetChildPropertyValueRecursivly(IPCMessage request, AccessibleObject root)
    {
      int childCount = root.GetChildCount();
      for (int index = 0; index < childCount; ++index)
      {
        AccessibleObject child = root.GetChild(index);
        if (child.Name == (string) request.Data)
        {
          RadItemAccessibleObject accessibleObject = child as RadItemAccessibleObject;
          if (accessibleObject == null || !(accessibleObject.Name == (string) request.Data))
            break;
          RadMenuItem owner = accessibleObject.Owner as RadMenuItem;
          if (request.Message == "IsTopLevelMenu")
          {
            request.Data = (object) (owner.Owner is RadMenuElement);
            break;
          }
          if (request.Message == "HasChildNodes")
          {
            request.Data = (object) (this.Items.Count > 0);
            break;
          }
          if (request.Message == "Text")
          {
            if (owner != null)
            {
              request.Data = (object) owner.Text;
              break;
            }
            request.Data = (object) "";
            break;
          }
          if (request.Message == "ItemsCount")
          {
            request.Data = (object) owner.Items.Count;
            break;
          }
          if (!(request.Message == "Checked"))
            break;
          request.Data = (object) owner.IsChecked;
          break;
        }
        this.GetChildPropertyValueRecursivly(request, child);
      }
    }

    public enum RadMenuState
    {
      CuesVisible,
      CuesVisibleKeyboardActive,
      KeyboardActive,
      NotActive,
    }

    public class RadMenuRootElement : RootRadElement
    {
      public override bool? ShouldSerializeProperty(PropertyDescriptor property)
      {
        if (property.Name == "StretchHorizontally" || property.Name == "StretchVertically")
          return new bool?(false);
        if (property.Name == "AccessibleName" && (string) property.GetValue((object) this) == this.Text)
          return new bool?(false);
        if (property.Name == "AccessibleDescription" && (string) property.GetValue((object) this) == this.Text)
          return new bool?(false);
        return base.ShouldSerializeProperty(property);
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (RootRadElement);
        }
      }
    }
  }
}
