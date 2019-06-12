// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarCustomizeDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class CommandBarCustomizeDialog : RadForm
  {
    private IContainer components;
    private RadThemeComponentBase.ThemeContext context;
    public RadPageView radPageView;
    public RadPageViewPage toolstripsPage;
    public RadPageViewPage toolstripItemsPage;
    public RadListControl stripsListControl;
    public RadButton closeButton;
    public RadDropDownList stripsDropDownList;
    public RadListControl stripItemsListControl;
    public RadButton moveUpButton;
    public RadButton moveDownButton;
    public RadLabel chooseToolstripLabel;
    public RadButton resetItemsButton;
    public CommandBarStripInfoHolder stripInfoHolder;

    public CommandBarCustomizeDialog()
    {
      this.InitializeComponent();
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    public CommandBarCustomizeDialog(CommandBarStripInfoHolder stripInfoHolder)
    {
      this.InitializeComponent();
      this.chooseToolstripLabel.LabelElement.LabelFill.Visibility = ElementVisibility.Visible;
      this.SetLocalizedStrings();
      this.stripInfoHolder = stripInfoHolder;
      this.stripsListControl.CreatingVisualListItem += new CreatingVisualListItemEventHandler(this.stripsListControl_CreatingVisualListItem);
      this.stripsListControl.ItemDataBinding += new ListItemDataBindingEventHandler(this.ItemDataBinding);
      this.stripItemsListControl.CreatingVisualListItem += new CreatingVisualListItemEventHandler(this.toolstripItemsListControl_CreatingVisualListItem);
      this.stripItemsListControl.ItemDataBinding += new ListItemDataBindingEventHandler(this.toolstripItemsListControl_ItemDataBinding);
      this.stripsListControl.DataSource = (object) stripInfoHolder.StripInfoList;
      this.stripsDropDownList.DropDownStyle = RadDropDownStyle.DropDownList;
      this.stripsDropDownList.Items.Clear();
      foreach (CommandBarStripElement stripInfo in stripInfoHolder.StripInfoList)
        this.stripsDropDownList.Items.Add(new RadListDataItem()
        {
          Text = stripInfo.DisplayName,
          Value = (object) stripInfo
        });
      if (this.stripsDropDownList.Items.Count > 0)
        this.stripsDropDownList.SelectedIndex = 0;
      this.toolstripsDownList_SelectedValueChanged((object) this, EventArgs.Empty);
      this.moveUpButton.Enabled = false;
      this.WireEvents();
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
          this.components.Dispose();
        this.UnwireEvents();
      }
      base.Dispose(disposing);
    }

    protected virtual void UnwireEvents()
    {
      this.closeButton.Click -= (EventHandler) ((sender, e) => this.Close());
      this.stripsListControl.CreatingVisualListItem -= new CreatingVisualListItemEventHandler(this.stripsListControl_CreatingVisualListItem);
      this.stripsListControl.ItemDataBinding -= new ListItemDataBindingEventHandler(this.ItemDataBinding);
      this.stripsListControl.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.stripsListControl_SelectedIndexChanged);
      this.stripItemsListControl.CreatingVisualListItem -= new CreatingVisualListItemEventHandler(this.toolstripItemsListControl_CreatingVisualListItem);
      this.stripItemsListControl.ItemDataBinding -= new ListItemDataBindingEventHandler(this.toolstripItemsListControl_ItemDataBinding);
      this.stripItemsListControl.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolstripItemsListControl_SelectedIndexChanged);
      LocalizationProvider<CommandBarLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.CommandBarLocalizationProvider_CurrentProviderChanged);
    }

    protected virtual void WireEvents()
    {
      this.closeButton.Click += (EventHandler) ((sender, e) => this.Close());
      this.stripsListControl.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.stripsListControl_SelectedIndexChanged);
      this.stripsDropDownList.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolstripsDropDownList_SelectedIndexChanged);
      this.stripItemsListControl.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.toolstripItemsListControl_SelectedIndexChanged);
      LocalizationProvider<CommandBarLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.CommandBarLocalizationProvider_CurrentProviderChanged);
    }

    private void InitializeComponent()
    {
      this.radPageView = new RadPageView();
      this.toolstripsPage = new RadPageViewPage();
      this.stripsListControl = new RadListControl();
      this.toolstripItemsPage = new RadPageViewPage();
      this.resetItemsButton = new RadButton();
      this.chooseToolstripLabel = new RadLabel();
      this.moveDownButton = new RadButton();
      this.moveUpButton = new RadButton();
      this.stripItemsListControl = new RadListControl();
      this.stripsDropDownList = new RadDropDownList();
      this.closeButton = new RadButton();
      this.radPageView.BeginInit();
      this.radPageView.SuspendLayout();
      this.toolstripsPage.SuspendLayout();
      this.stripsListControl.BeginInit();
      this.toolstripItemsPage.SuspendLayout();
      this.resetItemsButton.BeginInit();
      this.chooseToolstripLabel.BeginInit();
      this.moveDownButton.BeginInit();
      this.moveUpButton.BeginInit();
      this.stripItemsListControl.BeginInit();
      this.stripsDropDownList.BeginInit();
      this.closeButton.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radPageView.Controls.Add((Control) this.toolstripsPage);
      this.radPageView.Controls.Add((Control) this.toolstripItemsPage);
      this.radPageView.Location = new Point(0, 0);
      this.radPageView.Name = "radPageView";
      this.radPageView.SelectedPage = this.toolstripItemsPage;
      this.radPageView.Size = new Size(399, 352);
      this.radPageView.TabIndex = 0;
      this.radPageView.Text = "f";
      this.toolstripsPage.Controls.Add((Control) this.stripsListControl);
      this.toolstripsPage.Location = new Point(10, 37);
      this.toolstripsPage.Name = "toolstripsPage";
      this.toolstripsPage.Size = new Size(378, 304);
      this.toolstripsPage.Text = "Toolstrips";
      this.stripsListControl.Location = new Point(3, 3);
      this.stripsListControl.Name = "stripsListControl";
      this.stripsListControl.Size = new Size(372, 298);
      this.stripsListControl.TabIndex = 0;
      this.stripsListControl.Text = "radListControl1";
      this.toolstripItemsPage.Controls.Add((Control) this.resetItemsButton);
      this.toolstripItemsPage.Controls.Add((Control) this.chooseToolstripLabel);
      this.toolstripItemsPage.Controls.Add((Control) this.moveDownButton);
      this.toolstripItemsPage.Controls.Add((Control) this.moveUpButton);
      this.toolstripItemsPage.Controls.Add((Control) this.stripItemsListControl);
      this.toolstripItemsPage.Controls.Add((Control) this.stripsDropDownList);
      this.toolstripItemsPage.Location = new Point(10, 37);
      this.toolstripItemsPage.Name = "toolstripItemsPage";
      this.toolstripItemsPage.Size = new Size(378, 304);
      this.toolstripItemsPage.Text = "Toolstrip Items";
      this.resetItemsButton.Location = new Point(285, 115);
      this.resetItemsButton.Name = "resetItemsButton";
      this.resetItemsButton.Size = new Size(91, 24);
      this.resetItemsButton.TabIndex = 8;
      this.resetItemsButton.Text = "Reset";
      this.resetItemsButton.Visible = false;
      this.chooseToolstripLabel.BackColor = Color.Transparent;
      this.chooseToolstripLabel.Location = new Point(1, 3);
      this.chooseToolstripLabel.Name = "chooseToolstripLabel";
      this.chooseToolstripLabel.Size = new Size(165, 18);
      this.chooseToolstripLabel.TabIndex = 7;
      this.chooseToolstripLabel.Text = "Choose a toolstrip to rearrange:";
      this.moveDownButton.Location = new Point(285, 85);
      this.moveDownButton.Name = "moveDownButton";
      this.moveDownButton.Size = new Size(90, 24);
      this.moveDownButton.TabIndex = 6;
      this.moveDownButton.Text = "Move Down";
      this.moveDownButton.Click += new EventHandler(this.moveDownButton_Click);
      this.moveUpButton.Location = new Point(286, 55);
      this.moveUpButton.Name = "moveUpButton";
      this.moveUpButton.Size = new Size(89, 24);
      this.moveUpButton.TabIndex = 5;
      this.moveUpButton.Text = "Move Up";
      this.moveUpButton.Click += new EventHandler(this.moveUpButton_Click);
      this.stripItemsListControl.Location = new Point(3, 55);
      this.stripItemsListControl.Name = "stripItemsListControl";
      this.stripItemsListControl.Size = new Size(277, 246);
      this.stripItemsListControl.TabIndex = 1;
      this.stripItemsListControl.Text = "radListControl1";
      this.stripsDropDownList.Location = new Point(3, 27);
      this.stripsDropDownList.Name = "stripsDropDownList";
      this.stripsDropDownList.Size = new Size(372, 20);
      this.stripsDropDownList.TabIndex = 0;
      this.stripsDropDownList.SelectedValueChanged += new EventHandler(this.toolstripsDownList_SelectedValueChanged);
      this.closeButton.Location = new Point(299, 358);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new Size(93, 24);
      this.closeButton.TabIndex = 2;
      this.closeButton.Text = "Close";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(407, 390);
      this.Controls.Add((Control) this.closeButton);
      this.Controls.Add((Control) this.radPageView);
      this.Name = nameof (CommandBarCustomizeDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Customize";
      this.radPageView.EndInit();
      this.radPageView.ResumeLayout(false);
      this.toolstripsPage.ResumeLayout(false);
      this.stripsListControl.EndInit();
      this.toolstripItemsPage.ResumeLayout(false);
      this.toolstripItemsPage.PerformLayout();
      this.resetItemsButton.EndInit();
      this.chooseToolstripLabel.EndInit();
      this.moveDownButton.EndInit();
      this.moveUpButton.EndInit();
      this.stripItemsListControl.EndInit();
      this.stripsDropDownList.EndInit();
      this.closeButton.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.context.CorrectPositions();
        this.stripItemsListControl.Width -= 30;
        this.moveUpButton.Left -= 20;
        this.moveUpButton.Width += 20;
        this.moveDownButton.Left -= 20;
        this.moveDownButton.Width += 20;
        this.Width += 50;
        this.radPageView.Width += 50;
      }
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.context.CorrectPositions();
      this.stripItemsListControl.Width -= 30;
      this.moveUpButton.Left -= 25;
      this.moveUpButton.Width += 25;
      this.moveDownButton.Left -= 25;
      this.moveDownButton.Width += 25;
      this.closeButton.Width += 5;
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        RadControl radControl = control as RadControl;
        if (radControl != null)
          radControl.ThemeName = this.ThemeName;
      }
      if (this.toolstripItemsPage != null)
      {
        foreach (Control control in (ArrangedElementCollection) this.toolstripItemsPage.Controls)
        {
          RadControl radControl = control as RadControl;
          if (radControl != null)
            radControl.ThemeName = this.ThemeName;
        }
      }
      if (this.toolstripsPage == null)
        return;
      foreach (Control control in (ArrangedElementCollection) this.toolstripsPage.Controls)
      {
        RadControl radControl = control as RadControl;
        if (radControl != null)
          radControl.ThemeName = this.ThemeName;
      }
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.closeButton.Location = new Point(this.closeButton.Parent.ClientSize.Width - this.closeButton.Location.X - this.closeButton.Width, this.closeButton.Location.Y);
      this.moveUpButton.Location = new Point(this.moveUpButton.Parent.ClientSize.Width - this.moveUpButton.Location.X - this.moveUpButton.Width, this.moveUpButton.Location.Y);
      this.moveDownButton.Location = new Point(this.moveDownButton.Parent.ClientSize.Width - this.moveDownButton.Location.X - this.moveDownButton.Width, this.moveDownButton.Location.Y);
      this.chooseToolstripLabel.Location = new Point(this.chooseToolstripLabel.Parent.ClientSize.Width - this.chooseToolstripLabel.Location.X - this.chooseToolstripLabel.Width, this.chooseToolstripLabel.Location.Y);
      this.stripItemsListControl.Location = new Point(this.stripItemsListControl.Parent.ClientSize.Width - this.stripItemsListControl.Location.X - this.stripItemsListControl.Width, this.stripItemsListControl.Location.Y);
    }

    private void CommandBarLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.SetLocalizedStrings();
    }

    protected virtual void toolstripItemsListControl_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (this.stripItemsListControl.SelectedIndex <= 0)
        this.moveUpButton.Enabled = false;
      else
        this.moveUpButton.Enabled = true;
      if (this.stripItemsListControl.SelectedIndex >= this.stripItemsListControl.Items.Count - 1)
        this.moveDownButton.Enabled = false;
      else
        this.moveDownButton.Enabled = true;
    }

    protected virtual void toolstripsDropDownList_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.stripsListControl.SelectedIndex = e.Position;
    }

    protected virtual void stripsListControl_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.stripsDropDownList.SelectedIndex = e.Position;
    }

    protected virtual void toolstripItemsListControl_ItemDataBinding(
      object sender,
      ListItemDataBindingEventArgs args)
    {
      args.NewItem = (RadListDataItem) new CommandBarItemsListDataItem();
    }

    protected virtual void toolstripItemsListControl_CreatingVisualListItem(
      object sender,
      CreatingVisualListItemEventArgs args)
    {
      args.VisualItem = (RadListVisualItem) new CommandBarItemsListVisualItem();
    }

    protected virtual void ItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      args.NewItem = (RadListDataItem) new CommandBarStripsListDataItem();
    }

    protected virtual void stripsListControl_CreatingVisualListItem(
      object sender,
      CreatingVisualListItemEventArgs args)
    {
      args.VisualItem = (RadListVisualItem) new CommandBarStripsListVisualItem();
    }

    protected virtual void toolstripsDownList_SelectedValueChanged(object sender, EventArgs e)
    {
      if (this.stripsDropDownList.SelectedItem == null)
        return;
      CommandBarStripElement commandBarStripElement = this.stripsDropDownList.SelectedItem.Value as CommandBarStripElement;
      if (commandBarStripElement == null)
        return;
      List<RadCommandBarBaseItem> commandBarBaseItemList = new List<RadCommandBarBaseItem>();
      foreach (RadCommandBarBaseItem commandBarBaseItem in commandBarStripElement.Items)
        commandBarBaseItemList.Add(commandBarBaseItem);
      foreach (RadCommandBarBaseItem child in commandBarStripElement.OverflowButton.OverflowPanel.Layout.Children)
        commandBarBaseItemList.Add(child);
      this.stripItemsListControl.DataSource = (object) commandBarBaseItemList;
    }

    protected virtual void moveUpButton_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.stripItemsListControl.SelectedIndex;
      int index1 = selectedIndex - 1;
      if (index1 < 0)
        return;
      RadCommandBarBaseItem commandBarBaseItem1 = this.stripItemsListControl.Items[selectedIndex].Value as RadCommandBarBaseItem;
      RadCommandBarBaseItem commandBarBaseItem2 = this.stripItemsListControl.Items[index1].Value as RadCommandBarBaseItem;
      if (commandBarBaseItem1 == null || commandBarBaseItem2 == null)
        return;
      CommandBarStripElement commandBarStripElement = this.stripsDropDownList.SelectedItem.Value as CommandBarStripElement;
      LayoutPanel layout = commandBarStripElement.OverflowButton.OverflowPanel.Layout;
      int index2 = commandBarStripElement.Items.IndexOf(commandBarBaseItem1);
      int index3 = commandBarStripElement.Items.IndexOf(commandBarBaseItem2);
      if (index2 != -1 && index3 != -1)
      {
        RadCommandBarBaseItem commandBarBaseItem3 = commandBarStripElement.Items[index2];
        commandBarStripElement.Items.RemoveAt(index2);
        commandBarStripElement.Items.Insert(index3, commandBarBaseItem3);
      }
      else if (index2 == -1 && index3 != -1)
      {
        int index4 = layout.Children.IndexOf((RadElement) commandBarBaseItem1);
        if (index3 != -1)
        {
          RadCommandBarBaseItem child = layout.Children[index4] as RadCommandBarBaseItem;
          if (child != null)
          {
            layout.Children.RemoveAt(index4);
            commandBarStripElement.Items.Insert(index3, child);
          }
        }
      }
      else
      {
        int index4 = layout.Children.IndexOf((RadElement) commandBarBaseItem1);
        int index5 = layout.Children.IndexOf((RadElement) commandBarBaseItem2);
        if (index4 != -1 && index5 != -1)
        {
          RadElement child = layout.Children[index4];
          layout.Children.RemoveAt(index4);
          layout.Children.Insert(index5, child);
        }
      }
      this.toolstripsDownList_SelectedValueChanged((object) this, EventArgs.Empty);
      this.stripItemsListControl.SelectedIndex = index1;
    }

    protected virtual void moveDownButton_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.stripItemsListControl.SelectedIndex;
      int index1 = selectedIndex + 1;
      if (index1 >= this.stripItemsListControl.Items.Count)
        return;
      RadCommandBarBaseItem commandBarBaseItem1 = this.stripItemsListControl.Items[selectedIndex].Value as RadCommandBarBaseItem;
      RadCommandBarBaseItem commandBarBaseItem2 = this.stripItemsListControl.Items[index1].Value as RadCommandBarBaseItem;
      if (commandBarBaseItem1 == null || commandBarBaseItem2 == null)
        return;
      CommandBarStripElement commandBarStripElement = this.stripsDropDownList.SelectedItem.Value as CommandBarStripElement;
      LayoutPanel layout = commandBarStripElement.OverflowButton.OverflowPanel.Layout;
      int index2 = commandBarStripElement.Items.IndexOf(commandBarBaseItem1);
      int index3 = commandBarStripElement.Items.IndexOf(commandBarBaseItem2);
      if (index2 != -1 && index3 != -1)
      {
        RadCommandBarBaseItem commandBarBaseItem3 = commandBarStripElement.Items[index2];
        commandBarStripElement.Items.RemoveAt(index2);
        commandBarStripElement.Items.Insert(index3, commandBarBaseItem3);
      }
      else if (index2 != -1 && index3 == -1)
      {
        int index4 = layout.Children.IndexOf((RadElement) commandBarBaseItem2);
        if (index4 != -1)
        {
          RadCommandBarBaseItem child = layout.Children[index4] as RadCommandBarBaseItem;
          if (child != null)
          {
            layout.Children.RemoveAt(index4);
            commandBarStripElement.Items.Insert(index2, child);
          }
        }
      }
      else
      {
        int index4 = layout.Children.IndexOf((RadElement) commandBarBaseItem1);
        int index5 = layout.Children.IndexOf((RadElement) commandBarBaseItem2);
        if (index4 != -1 && index5 != -1)
        {
          RadElement child = layout.Children[index5];
          layout.Children.RemoveAt(index5);
          layout.Children.Insert(index4, child);
        }
      }
      this.toolstripsDownList_SelectedValueChanged((object) this, EventArgs.Empty);
      this.stripItemsListControl.SelectedIndex = index1;
    }

    protected virtual void SetLocalizedStrings()
    {
      (this.radPageView.ViewElement as RadPageViewStripElement).ItemContainer.ButtonsPanel.Visibility = ElementVisibility.Collapsed;
      this.Text = this.moveDownButton.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogTitle");
      this.closeButton.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogCloseButtonText");
      this.resetItemsButton.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogResetButtonText");
      this.moveDownButton.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogMoveDownButtonText");
      this.moveUpButton.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogMoveUpButtonText");
      this.chooseToolstripLabel.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogChooseToolstripLabelText");
      this.toolstripsPage.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogToolstripsPageTitle");
      this.toolstripItemsPage.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomizeDialogItemsPageTitle");
    }
  }
}
