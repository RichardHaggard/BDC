// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSearchCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridSearchCellElement : GridCellElement
  {
    public static RadProperty DistanceBetweenNextAndPreviousButtonProperty = RadProperty.Register(nameof (DistanceBetweenNextAndPreviousButton), typeof (int), typeof (GridSearchCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.AffectsLayout));
    private List<int> controlCharCodes = new List<int>()
    {
      8192,
      8193,
      8194,
      8195,
      8196,
      8197,
      8198,
      8199,
      8200,
      8201,
      8202,
      8203,
      8204,
      8205,
      8206,
      8207,
      8209,
      8232,
      8233,
      8234,
      8235,
      8236,
      8237,
      8238,
      8239,
      8287,
      8288,
      8289,
      8290,
      8291,
      8292,
      8294,
      8295,
      8296,
      8297,
      8298,
      8299,
      8300,
      8301,
      8302,
      8303
    };
    private int searchBoxWidth = 250;
    private int waitingBarHeight = 2;
    private GridSearchCellTextBoxElement searchTextBox;
    private RadWaitingBarElement waitingBar;
    private GridSearchCellButtonElement findNextButton;
    private GridSearchCellButtonElement findPreviousButton;
    private RadDropDownButtonElement optionsButton;
    private RadToggleButtonElement matchCaseButton;
    private RadMenuItem chooseColumnsMenuItem;
    private RadMenuItem matchCaseMenuItem;
    private RadMenuItem searchFromCurrentPositionMenuItem;
    protected StackLayoutPanel buttonsStack;
    private LightVisualButtonElement closeButton;
    private Timer timer;
    private bool changingMatchCasemenuItemValue;

    public GridSearchCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
      this.timer = new Timer();
      this.timer.Interval = 100;
      this.timer.Tick += new EventHandler(this.Timer_Tick);
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo != null)
      {
        this.timer.Interval = rowInfo.SearchDelay;
        rowInfo.PropertyChanged += new PropertyChangedEventHandler(this.searchRow_PropertyChanged);
        rowInfo.SearchProgressChanged += new SearchProgressChangedEventHandler(this.searchRow_SearchProgressChanged);
        this.MatchCaseMenuItem.IsChecked = rowInfo.CaseSensitive;
        this.changingMatchCasemenuItemValue = true;
        this.matchCaseButton.ToggleState = this.MatchCaseMenuItem.IsChecked ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.changingMatchCasemenuItemValue = false;
        this.SearchFromCurrentPositionMenuItem.IsChecked = rowInfo.SearchFromCurrentPosition;
        this.SearchTextBox.ShowClearButton = rowInfo.ShowClearButton;
        this.CloseButton.Visibility = rowInfo.ShowCloseButton ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      this.searchTextBox.KeyDown += new KeyEventHandler(this.searchTextBox_KeyDown);
      this.searchTextBox.MouseDown += new MouseEventHandler(this.SearchTextBox_MouseDown);
      this.findNextButton.Click += new EventHandler(this.findNextButton_Click);
      this.findPreviousButton.Click += new EventHandler(this.findPreviousButton_Click);
      this.searchTextBox.TextChanged += new EventHandler(this.searchTextBox_TextChanged);
      this.findNextButton.MouseEnter += new EventHandler(this.findNextButton_MouseEnter);
      this.findPreviousButton.MouseEnter += new EventHandler(this.findPreviousButton_MouseEnter);
      this.matchCaseButton.ToggleStateChanged += new StateChangedEventHandler(this.matchCaseButton_ToggleStateChanged);
      this.optionsButton.DropDownOpening += new CancelEventHandler(this.OptionsButton_DropDownOpening);
      this.chooseColumnsMenuItem.DropDownClosing += new RadPopupClosingEventHandler(this.ChooseColumnsMenuItem_DropDownClosing);
      this.matchCaseMenuItem.Click += new EventHandler(this.MatchCaseMenuItem_Click);
      this.searchFromCurrentPositionMenuItem.Click += new EventHandler(this.SearchFromCurrentFromPositionMenuItem_Click);
      LocalizationProvider<RadGridLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadGridLocalizationProvider_CurrentProviderChanged);
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      this.UpdateInfo();
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo == null)
        return;
      this.SyncLabelText();
      this.SyncCriteriaToTextBox(rowInfo);
    }

    protected override void DisposeManagedResources()
    {
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo != null)
      {
        rowInfo.PropertyChanged -= new PropertyChangedEventHandler(this.searchRow_PropertyChanged);
        rowInfo.SearchProgressChanged -= new SearchProgressChangedEventHandler(this.searchRow_SearchProgressChanged);
      }
      if (this.timer != null)
      {
        this.timer.Tick -= new EventHandler(this.Timer_Tick);
        this.timer.Dispose();
        this.timer = (Timer) null;
      }
      this.searchTextBox.KeyUp -= new KeyEventHandler(this.searchTextBox_KeyDown);
      this.searchTextBox.MouseDown -= new MouseEventHandler(this.SearchTextBox_MouseDown);
      this.findNextButton.Click -= new EventHandler(this.findNextButton_Click);
      this.findPreviousButton.Click -= new EventHandler(this.findPreviousButton_Click);
      this.searchTextBox.TextChanged -= new EventHandler(this.searchTextBox_TextChanged);
      this.findNextButton.MouseEnter -= new EventHandler(this.findNextButton_MouseEnter);
      this.findPreviousButton.MouseEnter -= new EventHandler(this.findPreviousButton_MouseEnter);
      this.matchCaseButton.ToggleStateChanged -= new StateChangedEventHandler(this.matchCaseButton_ToggleStateChanged);
      this.optionsButton.DropDownOpening -= new CancelEventHandler(this.OptionsButton_DropDownOpening);
      this.chooseColumnsMenuItem.DropDownClosing -= new RadPopupClosingEventHandler(this.ChooseColumnsMenuItem_DropDownClosing);
      this.matchCaseMenuItem.Click -= new EventHandler(this.MatchCaseMenuItem_Click);
      this.searchFromCurrentPositionMenuItem.Click -= new EventHandler(this.SearchFromCurrentFromPositionMenuItem_Click);
      LocalizationProvider<RadGridLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadGridLocalizationProvider_CurrentProviderChanged);
      this.UnsubscribeChildMenuItems(this.chooseColumnsMenuItem);
      this.closeButton.Click -= new EventHandler(this.CloseButton_Click);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.searchTextBox = this.CreateSearchTextBox();
      this.Children.Add((RadElement) this.searchTextBox);
      this.waitingBar = this.CreateWaitingBarElement();
      this.Children.Add((RadElement) this.waitingBar);
      this.findPreviousButton = this.CreateFindPreviousButton();
      this.Children.Add((RadElement) this.findPreviousButton);
      this.findNextButton = this.CreateFindNextButton();
      this.Children.Add((RadElement) this.findNextButton);
      this.matchCaseButton = this.CreateMatchCaseButton();
      this.chooseColumnsMenuItem = this.CreateChooseColumnsMenuItem();
      this.matchCaseMenuItem = this.CreateMatchCaseMenuItem();
      this.searchFromCurrentPositionMenuItem = this.CreateSearchFromCurrentPositionMenuItem();
      this.optionsButton = this.CreateOptionsButton();
      this.Children.Add((RadElement) this.optionsButton);
      this.buttonsStack = new StackLayoutPanel();
      this.buttonsStack.Margin = new Padding(0, 1, 0, 2);
      this.Children.Add((RadElement) this.buttonsStack);
      this.closeButton = this.CreateCloseButton();
      this.closeButton.Class = "SearchRowCloseButton";
      this.closeButton.Click += new EventHandler(this.CloseButton_Click);
      this.buttonsStack.Children.Add((RadElement) this.closeButton);
    }

    protected virtual GridSearchCellTextBoxElement CreateSearchTextBox()
    {
      GridSearchCellTextBoxElement cellTextBoxElement = new GridSearchCellTextBoxElement();
      cellTextBoxElement.Margin = new Padding(2, 1, 2, 1);
      cellTextBoxElement.TextBoxItem.NullText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowTextBoxNullText");
      return cellTextBoxElement;
    }

    protected virtual RadWaitingBarElement CreateWaitingBarElement()
    {
      RadWaitingBarElement waitingBarElement = new RadWaitingBarElement();
      waitingBarElement.MaxSize = new Size(0, this.WaitingBarHeight);
      waitingBarElement.Margin = new Padding(2, -1, 2, 0);
      waitingBarElement.ContentElement.DrawBorder = false;
      waitingBarElement.ContentElement.BorderWidth = 0.0f;
      waitingBarElement.ContentElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
      waitingBarElement.BorderWidth = 0.0f;
      waitingBarElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
      waitingBarElement.Visibility = ElementVisibility.Hidden;
      return waitingBarElement;
    }

    protected virtual GridSearchCellButtonElement CreateFindPreviousButton()
    {
      GridSearchCellButtonElement cellButtonElement = new GridSearchCellButtonElement();
      cellButtonElement.Arrow.Direction = Telerik.WinControls.ArrowDirection.Up;
      cellButtonElement.DisplayStyle = DisplayStyle.Image;
      cellButtonElement.Margin = new Padding(0, 1, 0, 2);
      return cellButtonElement;
    }

    protected virtual GridSearchCellButtonElement CreateFindNextButton()
    {
      GridSearchCellButtonElement cellButtonElement = new GridSearchCellButtonElement();
      cellButtonElement.Arrow.Direction = Telerik.WinControls.ArrowDirection.Down;
      cellButtonElement.DisplayStyle = DisplayStyle.Image;
      cellButtonElement.Margin = new Padding(0, 1, 0, 2);
      return cellButtonElement;
    }

    protected virtual RadDropDownButtonElement CreateOptionsButton()
    {
      RadDropDownButtonElement downButtonElement = new RadDropDownButtonElement();
      downButtonElement.DisplayStyle = DisplayStyle.Image;
      downButtonElement.ImageAlignment = ContentAlignment.MiddleCenter;
      downButtonElement.Margin = new Padding(8, 1, 8, 2);
      downButtonElement.ActionButton.Padding = new Padding(2, 0, 2, 0);
      downButtonElement.Items.AddRange((RadItem) this.chooseColumnsMenuItem, (RadItem) new RadMenuSeparatorItem(), (RadItem) this.matchCaseMenuItem, (RadItem) this.searchFromCurrentPositionMenuItem);
      return downButtonElement;
    }

    [Obsolete("This method is obsolete. Use CreateOptionsButton and/or CreateMatchCaseMenuItem methods instead.")]
    protected virtual RadToggleButtonElement CreateMatchCaseButton()
    {
      RadToggleButtonElement toggleButtonElement = new RadToggleButtonElement();
      toggleButtonElement.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMatchCase");
      return toggleButtonElement;
    }

    protected virtual RadMenuItem CreateChooseColumnsMenuItem()
    {
      RadMenuItem radMenuItem = new RadMenuItem();
      radMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowChooseColumns");
      return radMenuItem;
    }

    protected virtual RadMenuItem CreateMatchCaseMenuItem()
    {
      RadMenuItem radMenuItem = new RadMenuItem();
      radMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMatchCase");
      return radMenuItem;
    }

    protected virtual RadMenuItem CreateSearchFromCurrentPositionMenuItem()
    {
      RadMenuItem radMenuItem = new RadMenuItem();
      radMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowSearchFromCurrentPosition");
      return radMenuItem;
    }

    protected virtual LightVisualButtonElement CreateCloseButton()
    {
      LightVisualButtonElement visualButtonElement = new LightVisualButtonElement();
      visualButtonElement.Margin = new Padding(3, 0, 3, 0);
      visualButtonElement.Padding = new Padding(5, 0, 5, 0);
      return visualButtonElement;
    }

    public GridSearchCellTextBoxElement SearchTextBox
    {
      get
      {
        return this.searchTextBox;
      }
    }

    public RadWaitingBarElement WaitingBar
    {
      get
      {
        return this.waitingBar;
      }
    }

    public GridSearchCellButtonElement FindNextButton
    {
      get
      {
        return this.findNextButton;
      }
    }

    public GridSearchCellButtonElement FindPreviousButton
    {
      get
      {
        return this.findPreviousButton;
      }
    }

    public RadDropDownButtonElement OptionsButton
    {
      get
      {
        return this.optionsButton;
      }
    }

    public LightVisualButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    [Obsolete("Obsolete. Use MatchCaseMenuItem and/or OptionButton instead.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadToggleButtonElement MatchCaseButton
    {
      get
      {
        return this.matchCaseButton;
      }
    }

    public RadMenuItem ChooseColumnsMenuItem
    {
      get
      {
        return this.chooseColumnsMenuItem;
      }
    }

    public RadMenuItem MatchCaseMenuItem
    {
      get
      {
        return this.matchCaseMenuItem;
      }
    }

    public RadMenuItem SearchFromCurrentPositionMenuItem
    {
      get
      {
        return this.searchFromCurrentPositionMenuItem;
      }
    }

    public int DistanceBetweenNextAndPreviousButton
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(GridSearchCellElement.DistanceBetweenNextAndPreviousButtonProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(GridSearchCellElement.DistanceBetweenNextAndPreviousButtonProperty, (object) value);
      }
    }

    public int SearchBoxWidth
    {
      get
      {
        return (int) ((double) this.searchBoxWidth * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        this.searchBoxWidth = value;
        this.InvalidateMeasure(true);
        this.UpdateLayout();
      }
    }

    public int WaitingBarHeight
    {
      get
      {
        return (int) ((double) this.waitingBarHeight * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        this.waitingBarHeight = value;
      }
    }

    public int TextInputDelay
    {
      get
      {
        return this.timer.Interval;
      }
      set
      {
        this.timer.Interval = value;
      }
    }

    protected virtual void SyncLabelText()
    {
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo == null)
        return;
      if (string.IsNullOrEmpty(rowInfo.SearchCriteria))
        this.SearchTextBox.SearchInfoLabel.Text = string.Empty;
      else
        this.SearchTextBox.SearchInfoLabel.Text = string.Format("{0} {1} {2}", (object) (rowInfo.CurrentResultIndex + 1), (object) LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowResultsOfLabel"), (object) rowInfo.CurrentSearchResultsCount);
    }

    protected virtual void Search()
    {
      this.Search(true);
    }

    protected virtual void Search(bool syncCriteria)
    {
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo == null)
        return;
      string criteria = this.RemoveUnicodeControlCharacters(this.SearchTextBox.Text);
      if (!string.IsNullOrEmpty(criteria))
      {
        this.waitingBar.Visibility = ElementVisibility.Visible;
        this.waitingBar.ResetWaiting();
        this.waitingBar.StartWaiting();
      }
      if (syncCriteria)
        this.SyncCriteriaToTextBox(rowInfo);
      rowInfo.Search(criteria);
    }

    protected virtual void SyncCriteriaToTextBox(GridViewSearchRowInfo searchRow)
    {
      this.SearchTextBox.TextChanged -= new EventHandler(this.searchTextBox_TextChanged);
      if (string.IsNullOrEmpty(searchRow.SearchCriteria))
      {
        this.SearchTextBox.Text = string.Empty;
      }
      else
      {
        this.SearchTextBox.Text = searchRow.SearchCriteria;
        this.SearchTextBox.TextBoxItem.SelectionStart = searchRow.SearchCriteria.Length;
      }
      this.SearchTextBox.TextChanged += new EventHandler(this.searchTextBox_TextChanged);
    }

    protected override bool CanUpdateInfo
    {
      get
      {
        return !this.UpdatingInfo;
      }
    }

    protected override void UpdateInfoCore()
    {
    }

    protected virtual string RemoveUnicodeControlCharacters(string input)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < input.Length; ++index)
      {
        if (!char.IsControl(input[index]) && !this.controlCharCodes.Contains((int) input[index]))
          stringBuilder.Append(input[index]);
      }
      return stringBuilder.ToString();
    }

    private void searchRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "CaseSensitive")
      {
        GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
        if (this.matchCaseMenuItem.IsChecked == rowInfo.CaseSensitive)
          return;
        this.matchCaseMenuItem.IsChecked = rowInfo.CaseSensitive;
      }
      else if (e.PropertyName == "SearchFromCurrentPosition")
      {
        GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
        if (this.searchFromCurrentPositionMenuItem.IsChecked == rowInfo.SearchFromCurrentPosition)
          return;
        this.searchFromCurrentPositionMenuItem.IsChecked = rowInfo.SearchFromCurrentPosition;
      }
      else if (e.PropertyName == "ShowClearButton")
        this.searchTextBox.ShowClearButton = (this.RowInfo as GridViewSearchRowInfo).ShowClearButton;
      else if (e.PropertyName == "ShowCloseButton")
      {
        GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
        if (this.closeButton == null)
          return;
        this.closeButton.Visibility = rowInfo.ShowCloseButton ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      else
      {
        if (!(e.PropertyName == "SearchDelay") || this.timer == null)
          return;
        this.timer.Interval = (this.RowInfo as GridViewSearchRowInfo).SearchDelay;
      }
    }

    protected virtual void searchRow_SearchProgressChanged(
      object sender,
      SearchProgressChangedEventArgs e)
    {
      if (e.SearchFinished)
      {
        this.waitingBar.StopWaiting();
        this.waitingBar.Visibility = ElementVisibility.Hidden;
      }
      if (this.GridControl != null && this.GridControl.InvokeRequired)
        this.GridControl.Invoke((Delegate) new MethodInvoker(this.SyncLabelText));
      else
        this.SyncLabelText();
      this.GridViewElement.Invalidate();
    }

    private void findPreviousButton_Click(object sender, EventArgs e)
    {
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo == null)
        return;
      rowInfo.SelectPreviousSearchResult();
      this.SyncLabelText();
    }

    private void findNextButton_Click(object sender, EventArgs e)
    {
      GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
      if (rowInfo == null)
        return;
      rowInfo.SelectNextSearchResult();
      this.SyncLabelText();
    }

    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {
      if (this.timer.Enabled)
        this.timer.Stop();
      this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.timer.Stop();
      this.Search(false);
    }

    private void matchCaseButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (this.changingMatchCasemenuItemValue)
        return;
      this.MatchCaseMenuItem_Click((object) this.matchCaseMenuItem, EventArgs.Empty);
    }

    private void OptionsButton_DropDownOpening(object sender, CancelEventArgs e)
    {
      this.UnsubscribeChildMenuItems(this.chooseColumnsMenuItem);
      this.chooseColumnsMenuItem.Items.Clear();
      this.AddColumnMenuItems(this.chooseColumnsMenuItem, (GridViewTemplate) this.GridControl.MasterTemplate, LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMenuItemMasterTemplate"));
      ContextMenuOpeningEventArgs args = new ContextMenuOpeningEventArgs((IContextMenuProvider) this, this.OptionsButton.DropDownMenu);
      this.GridViewElement.Template.EventDispatcher.RaiseEvent<ContextMenuOpeningEventArgs>(EventDispatcher.ContextMenuOpening, (object) this, args);
      e.Cancel = args.Cancel;
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      (this.RowInfo as GridViewSearchRowInfo).IsVisible = false;
    }

    private void AddColumnMenuItems(
      RadMenuItem parentItem,
      GridViewTemplate template,
      string headerItemName)
    {
      List<RadMenuItemBase> radMenuItemBaseList = new List<RadMenuItemBase>();
      radMenuItemBaseList.Add((RadMenuItemBase) new RadMenuHeaderItem(headerItemName));
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMenuItemAllColumns"), (object) template);
      radMenuItem1.Click += new EventHandler(this.ColumnItem_Click);
      radMenuItem1.IsChecked = true;
      radMenuItemBaseList.Add((RadMenuItemBase) radMenuItem1);
      radMenuItemBaseList.Add((RadMenuItemBase) new RadMenuSeparatorItem());
      foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) template.Columns)
      {
        if (column.IsVisible)
        {
          RadMenuItem radMenuItem2 = new RadMenuItem(column.HeaderText, (object) column);
          radMenuItem2.Click += new EventHandler(this.ColumnItem_Click);
          radMenuItem2.IsChecked = column.AllowSearching;
          radMenuItemBaseList.Add((RadMenuItemBase) radMenuItem2);
          if (!radMenuItem2.IsChecked)
            radMenuItem1.IsChecked = false;
        }
      }
      if (template.Templates.Count > 0)
      {
        RadMenuHeaderItem radMenuHeaderItem = new RadMenuHeaderItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMenuItemChildTemplates"));
        radMenuHeaderItem.Margin = new Padding(0, 5, 0, 0);
        radMenuItemBaseList.Add((RadMenuItemBase) radMenuHeaderItem);
        foreach (GridViewTemplate template1 in (Collection<GridViewTemplate>) template.Templates)
        {
          string text = template1.Caption;
          if (string.IsNullOrEmpty(text))
            text = "Child Template " + (object) (template.Templates.IndexOf(template1) + 1);
          RadMenuItem parentItem1 = new RadMenuItem(text);
          parentItem1.DropDownClosing += new RadPopupClosingEventHandler(this.ChooseColumnsMenuItem_DropDownClosing);
          this.AddColumnMenuItems(parentItem1, template1, template1.Caption);
          radMenuItemBaseList.Add((RadMenuItemBase) parentItem1);
        }
      }
      parentItem.Items.AddRange((RadItem[]) radMenuItemBaseList.ToArray());
    }

    private void UnsubscribeChildMenuItems(RadMenuItem parentItem)
    {
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) parentItem.Items)
      {
        RadMenuItem parentItem1 = radMenuItemBase as RadMenuItem;
        if (parentItem1 != null)
        {
          parentItem1.Tag = (object) null;
          parentItem1.Click -= new EventHandler(this.ColumnItem_Click);
          if (parentItem1.Items.Count > 0)
          {
            parentItem1.DropDownClosing -= new RadPopupClosingEventHandler(this.ChooseColumnsMenuItem_DropDownClosing);
            this.UnsubscribeChildMenuItems(parentItem1);
          }
        }
      }
    }

    private void ChooseColumnsMenuItem_DropDownClosing(object sender, RadPopupClosingEventArgs args)
    {
      RadMenuItem radMenuItem = sender as RadMenuItem;
      if (radMenuItem == null)
        return;
      Point client = radMenuItem.DropDown.PointToClient(Control.MousePosition);
      bool flag = false;
      foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) radMenuItem.Items)
      {
        if (radMenuItemBase is RadMenuItem && radMenuItemBase.BoundingRectangle.Contains(client.X, client.Y))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      args.Cancel = true;
    }

    private void ColumnItem_Click(object sender, EventArgs e)
    {
      RadMenuItem radMenuItem1 = sender as RadMenuItem;
      RadMenuItem radMenuItem2 = radMenuItem1;
      radMenuItem2.IsChecked = !radMenuItem2.IsChecked;
      if (radMenuItem1.Tag is GridViewTemplate)
      {
        foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) radMenuItem1.HierarchyParent.Items)
        {
          RadMenuItem radMenuItem3 = radMenuItemBase as RadMenuItem;
          if (radMenuItem3 != null && radMenuItem3.Tag != null && radMenuItem3 != radMenuItem1)
          {
            radMenuItem3.IsChecked = radMenuItem1.IsChecked;
            (radMenuItem3.Tag as GridViewDataColumn).AllowSearching = radMenuItem3.IsChecked;
          }
        }
      }
      else
      {
        (radMenuItem1.Tag as GridViewDataColumn).AllowSearching = radMenuItem1.IsChecked;
        bool flag = true;
        if (radMenuItem1.IsChecked)
        {
          foreach (RadMenuItemBase radMenuItemBase in (RadItemCollection) radMenuItem1.HierarchyParent.Items)
          {
            RadMenuItem radMenuItem3 = radMenuItemBase as RadMenuItem;
            if (radMenuItem3 != null && radMenuItem3.Tag != null && (!radMenuItem3.IsChecked && !(radMenuItem3.Tag is GridViewTemplate)))
            {
              flag = false;
              break;
            }
          }
        }
        else
          flag = false;
        (radMenuItem1.HierarchyParent.Items[1] as RadMenuItem).IsChecked = flag;
      }
      this.Search();
    }

    private void MatchCaseMenuItem_Click(object sender, EventArgs e)
    {
      RadMenuItem matchCaseMenuItem = this.matchCaseMenuItem;
      matchCaseMenuItem.IsChecked = !matchCaseMenuItem.IsChecked;
      (this.RowInfo as GridViewSearchRowInfo).CaseSensitive = this.matchCaseMenuItem.IsChecked;
      this.changingMatchCasemenuItemValue = true;
      this.matchCaseButton.ToggleState = this.matchCaseMenuItem.IsChecked ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.changingMatchCasemenuItemValue = false;
      this.Search();
    }

    private void SearchFromCurrentFromPositionMenuItem_Click(object sender, EventArgs e)
    {
      RadMenuItem positionMenuItem = this.searchFromCurrentPositionMenuItem;
      positionMenuItem.IsChecked = !positionMenuItem.IsChecked;
      (this.RowInfo as GridViewSearchRowInfo).SearchFromCurrentPosition = this.searchFromCurrentPositionMenuItem.IsChecked;
      this.Search();
    }

    private void RadGridLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.SyncLabelText();
      this.chooseColumnsMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowChooseColumns");
      this.matchCaseButton.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMatchCase");
      this.matchCaseMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowMatchCase");
      this.searchFromCurrentPositionMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowSearchFromCurrentPosition");
      this.searchTextBox.TextBoxItem.NullText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SearchRowTextBoxNullText");
    }

    private void findPreviousButton_MouseEnter(object sender, EventArgs e)
    {
      this.findPreviousButton.ZIndex = 2;
      this.findNextButton.ZIndex = 1;
    }

    private void findNextButton_MouseEnter(object sender, EventArgs e)
    {
      this.findNextButton.ZIndex = 2;
      this.findPreviousButton.ZIndex = 1;
    }

    private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
      {
        GridViewSearchRowInfo rowInfo = this.RowInfo as GridViewSearchRowInfo;
        if (e.Shift)
          rowInfo.SelectPreviousSearchResult();
        else
          rowInfo.SelectNextSearchResult();
        this.SyncLabelText();
      }
      else
      {
        if (e.KeyCode != Keys.Escape)
          return;
        if (this.searchTextBox.Text.Length > 0)
        {
          this.searchTextBox.Text = string.Empty;
        }
        else
        {
          if (!(this.RowInfo as GridViewSearchRowInfo).CloseOnEscape)
            return;
          this.RowInfo.IsVisible = false;
        }
      }
    }

    private void SearchTextBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.GridViewElement == null || !this.GridViewElement.EditorManager.IsInEditMode)
        return;
      this.GridViewElement.EditorManager.EndEdit();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      if (float.IsPositiveInfinity(availableSize.Height))
        clientRectangle = this.GetClientRectangle(new SizeF(availableSize.Width, sizeF.Height));
      this.searchTextBox.Measure(new SizeF((float) this.SearchBoxWidth, clientRectangle.Height - (float) this.WaitingBarHeight - (float) this.SearchTextBox.Margin.Vertical));
      this.waitingBar.Measure(new SizeF((float) this.SearchBoxWidth, (float) this.WaitingBarHeight));
      float num1 = (float) ((double) (this.SearchBoxWidth + this.SearchTextBox.Margin.Horizontal) + ((double) clientRectangle.Height - (double) this.findPreviousButton.Margin.Vertical) + ((double) clientRectangle.Height - (double) this.findNextButton.Margin.Vertical)) + this.optionsButton.DesiredSize.Width + (float) this.optionsButton.Margin.Horizontal + this.buttonsStack.DesiredSize.Width + (float) this.buttonsStack.Margin.Horizontal;
      if ((double) num1 > (double) clientRectangle.Width)
      {
        float num2 = num1 - (this.buttonsStack.DesiredSize.Width + (float) this.buttonsStack.Margin.Horizontal);
        this.buttonsStack.Measure(SizeF.Empty);
        if ((double) num2 > (double) clientRectangle.Width)
        {
          float num3 = num2 - (this.optionsButton.DesiredSize.Width + (float) this.optionsButton.Margin.Horizontal);
          this.optionsButton.Measure(SizeF.Empty);
          if ((double) num3 > (double) clientRectangle.Width)
          {
            float num4 = num3 - (clientRectangle.Height - (float) this.findNextButton.Margin.Vertical);
            this.findNextButton.Measure(SizeF.Empty);
            if ((double) num4 > (double) clientRectangle.Width)
            {
              float num5 = num4 - (clientRectangle.Height - (float) this.findPreviousButton.Margin.Vertical);
              this.findPreviousButton.Measure(SizeF.Empty);
              if ((double) num5 > (double) clientRectangle.Width)
              {
                this.searchTextBox.Measure(new SizeF(clientRectangle.Width, clientRectangle.Height - (float) this.WaitingBarHeight - (float) this.SearchTextBox.Margin.Vertical));
                this.waitingBar.Measure(new SizeF(clientRectangle.Width, (float) this.WaitingBarHeight));
              }
            }
          }
        }
      }
      if (this.optionsButton.DesiredSize == SizeF.Empty)
      {
        int num6 = (int) this.optionsButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
      }
      else if (this.optionsButton.Visibility == ElementVisibility.Hidden)
      {
        int num7 = (int) this.optionsButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      if (float.IsPositiveInfinity(availableSize.Height))
      {
        availableSize.Height = this.SearchTextBox.DesiredSize.Height + (float) this.SearchTextBox.Margin.Vertical + this.WaitingBar.DesiredSize.Height + (float) this.WaitingBar.Margin.Vertical;
        availableSize.Height = Math.Max(availableSize.Height, this.FindPreviousButton.DesiredSize.Height + (float) this.FindPreviousButton.Margin.Vertical);
        availableSize.Height = Math.Max(availableSize.Height, this.FindNextButton.DesiredSize.Height + (float) this.FindNextButton.Margin.Vertical);
        availableSize.Height = Math.Max(availableSize.Height, this.optionsButton.DesiredSize.Height + (float) this.optionsButton.Margin.Vertical);
        availableSize.Height = Math.Max(availableSize.Height, this.buttonsStack.DesiredSize.Height + (float) this.buttonsStack.Margin.Vertical);
      }
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x1 = this.RightToLeft ? clientRectangle.Right - (float) this.SearchBoxWidth - (float) this.searchTextBox.Margin.Right : clientRectangle.X + (float) this.searchTextBox.Margin.Left;
      float y1 = clientRectangle.Y + (float) this.searchTextBox.Margin.Top;
      float searchBoxWidth1 = (float) this.SearchBoxWidth;
      float height1 = clientRectangle.Height - (float) this.WaitingBarHeight - (float) this.SearchTextBox.Margin.Vertical;
      RectangleF finalRect1 = new RectangleF(x1, y1, searchBoxWidth1, height1);
      float x2 = this.RightToLeft ? clientRectangle.Right - (float) this.SearchBoxWidth - (float) this.waitingBar.Margin.Right : clientRectangle.X + (float) this.waitingBar.Margin.Left;
      float y2 = y1 + height1 + (float) this.searchTextBox.Margin.Bottom + (float) this.waitingBar.Margin.Top;
      float searchBoxWidth2 = (float) this.SearchBoxWidth;
      float waitingBarHeight = (float) this.WaitingBarHeight;
      RectangleF finalRect2 = new RectangleF(x2, y2, searchBoxWidth2, waitingBarHeight);
      float x3 = this.RightToLeft ? x2 - (clientRectangle.Height - (float) this.findPreviousButton.Margin.Vertical) - (float) this.searchTextBox.Margin.Left - (float) this.findPreviousButton.Margin.Right : x2 + searchBoxWidth2 + (float) this.searchTextBox.Margin.Right + (float) this.findPreviousButton.Margin.Left;
      float y3 = clientRectangle.Y + (float) this.findPreviousButton.Margin.Top;
      float height2 = clientRectangle.Height - (float) this.findPreviousButton.Margin.Vertical;
      float width1 = height2 + (float) this.findPreviousButton.Margin.Horizontal;
      RectangleF finalRect3 = new RectangleF(x3, y3, width1, height2);
      float x4 = this.RightToLeft ? (float) ((double) x3 + (double) this.findNextButton.Margin.Horizontal - ((double) clientRectangle.Height - (double) this.findNextButton.Margin.Vertical)) - (float) this.findPreviousButton.Margin.Left - (float) this.findNextButton.Margin.Right - (float) this.DistanceBetweenNextAndPreviousButton : x3 + width1 + (float) this.findPreviousButton.Margin.Right + (float) this.findNextButton.Margin.Left + (float) this.DistanceBetweenNextAndPreviousButton;
      float y4 = clientRectangle.Y + (float) this.findNextButton.Margin.Top;
      float height3 = clientRectangle.Height - (float) this.findNextButton.Margin.Vertical;
      float width2 = height3 + (float) this.findNextButton.Margin.Horizontal;
      RectangleF finalRect4 = new RectangleF(x4, y4, width2, height3);
      RectangleF finalRect5 = new RectangleF(this.RightToLeft ? x4 - this.optionsButton.DesiredSize.Width - (float) this.findNextButton.Margin.Left - (float) this.optionsButton.Margin.Right : x4 + width2 + (float) this.findNextButton.Margin.Right + (float) this.optionsButton.Margin.Left, clientRectangle.Y + (float) this.optionsButton.Margin.Top, this.optionsButton.DesiredSize.Width, clientRectangle.Height - (float) this.optionsButton.Margin.Vertical);
      RectangleF finalRect6 = new RectangleF(this.RightToLeft ? clientRectangle.X + (float) this.buttonsStack.Margin.Left : clientRectangle.Right - this.buttonsStack.DesiredSize.Width - (float) this.buttonsStack.Margin.Right, clientRectangle.Y + (float) this.buttonsStack.Margin.Top, this.buttonsStack.DesiredSize.Width, clientRectangle.Height - (float) this.buttonsStack.Margin.Vertical);
      if (this.RightToLeft && (double) finalRect6.Right > (double) finalRect5.X || !this.RightToLeft && (double) finalRect6.X < (double) finalRect5.Right)
        finalRect6 = RectangleF.Empty;
      if (this.RightToLeft && (double) finalRect5.X < (double) clientRectangle.X || (double) finalRect5.Right > (double) clientRectangle.Right)
        finalRect5 = RectangleF.Empty;
      if (this.RightToLeft && (double) finalRect4.X < (double) clientRectangle.X || (double) finalRect4.Right > (double) clientRectangle.Right)
        finalRect4 = RectangleF.Empty;
      if (this.RightToLeft && (double) finalRect3.X < (double) clientRectangle.X || (double) finalRect3.Right > (double) clientRectangle.Right)
        finalRect3 = RectangleF.Empty;
      if (this.RightToLeft && (double) finalRect1.X < (double) clientRectangle.X || (double) finalRect1.Right > (double) clientRectangle.Right)
      {
        finalRect1.Width = clientRectangle.Width - (float) this.searchTextBox.Margin.Horizontal;
        finalRect2.Width = clientRectangle.Width - (float) this.waitingBar.Margin.Horizontal;
        if (this.RightToLeft)
        {
          finalRect1.X = clientRectangle.X + (float) this.searchTextBox.Margin.Left;
          finalRect2.X = clientRectangle.X + (float) this.waitingBar.Margin.Left;
        }
      }
      this.searchTextBox.Arrange(finalRect1);
      this.waitingBar.Arrange(finalRect2);
      this.findPreviousButton.Arrange(finalRect3);
      this.findNextButton.Arrange(finalRect4);
      this.optionsButton.Arrange(finalRect5);
      this.buttonsStack.Arrange(finalRect6);
      return finalSize;
    }
  }
}
