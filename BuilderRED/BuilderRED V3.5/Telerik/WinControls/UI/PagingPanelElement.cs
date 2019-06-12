// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PagingPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class PagingPanelElement : GridVisualElement, IGridView, IGridViewEventListener
  {
    public static RadProperty NumericButtonsCountProperty = RadProperty.Register(nameof (NumericButtonsCount), typeof (int), typeof (PagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.None));
    public static RadProperty FirstPageButtonImageProperty = RadProperty.Register(nameof (FirstPageButtonImage), typeof (Image), typeof (PagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty PreviousPageButtonImageProperty = RadProperty.Register(nameof (PreviousPageButtonImage), typeof (Image), typeof (PagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NextPageButtonImageProperty = RadProperty.Register(nameof (NextPageButtonImage), typeof (Image), typeof (PagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LastPageButtonImageProperty = RadProperty.Register(nameof (LastPageButtonImage), typeof (Image), typeof (PagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private bool showButtonsStripElement = true;
    private bool showTextBoxStripElement = true;
    private bool showFirstButton = true;
    private bool showPreviousButton = true;
    private bool showFastBackButton = true;
    private bool showNumericalButtons = true;
    private bool showFastForwardButton = true;
    private bool showNextButton = true;
    private bool showLastButton = true;
    private RadGridViewElement gridViewElement;
    private GridViewInfo viewInfo;
    private RadCommandBarElement commandBar;
    private CommandBarRowElement commandBarRowElement;
    private CommandBarStripElement buttonsStripElement;
    private CommandBarButton firstButton;
    private CommandBarButton previousButton;
    private CommandBarButton fastBackButton;
    private CommandBarButton fastForwardButton;
    private CommandBarButton nextButton;
    private CommandBarButton lastButton;
    private CommandBarStripElement textBoxStripElement;
    private CommandBarLabel pageLabel;
    private CommandBarTextBox pageNumberTextBox;
    private CommandBarLabel ofPagesLabel;
    private CommandBarLabel numberOfPagesLabel;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.DrawFill = false;
      this.DrawBorder = false;
      this.showFirstButton = true;
      this.showPreviousButton = true;
      this.showFastBackButton = true;
      this.showNumericalButtons = true;
      this.showFastForwardButton = true;
      this.showNextButton = true;
      this.showLastButton = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.commandBar = new RadCommandBarElement();
      this.commandBar.Margin = new Padding(0, 0, 0, -3);
      this.commandBar.AllowDrag = false;
      this.commandBar.AllowDrop = false;
      this.commandBar.DesignTimeAllowDrag = false;
      this.commandBar.DesignTimeAllowDrop = false;
      this.commandBarRowElement = new CommandBarRowElement();
      this.commandBarRowElement.Margin = new Padding(1, 0, 1, 0);
      this.commandBar.Rows.Add(this.CommandBarRowElement);
      this.CreateButtonsStripElementChildElements();
      this.CreateTextBoxStripElementChildElements();
      this.Children.Add((RadElement) this.commandBar);
      this.WireEvents();
    }

    protected virtual void CreateButtonsStripElementChildElements()
    {
      this.buttonsStripElement = new CommandBarStripElement();
      this.buttonsStripElement.MinSize = Size.Empty;
      this.buttonsStripElement.EnableDragging = false;
      this.buttonsStripElement.EnableFloating = false;
      this.buttonsStripElement.StretchHorizontally = true;
      this.buttonsStripElement.Grip.Visibility = ElementVisibility.Collapsed;
      this.buttonsStripElement.OverflowButton.Visibility = ElementVisibility.Collapsed;
      this.commandBarRowElement.Strips.Add(this.buttonsStripElement);
      this.firstButton = new CommandBarButton();
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.firstButton);
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
      this.previousButton = new CommandBarButton();
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.previousButton);
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
      this.fastBackButton = new CommandBarButton();
      this.fastBackButton.Text = "...";
      this.fastBackButton.Image = (Image) null;
      this.fastBackButton.DrawText = true;
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.fastBackButton);
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
      this.fastForwardButton = new CommandBarButton();
      this.fastForwardButton.Text = "...";
      this.fastForwardButton.Image = (Image) null;
      this.fastForwardButton.DrawText = true;
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.fastForwardButton);
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
      this.nextButton = new CommandBarButton();
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.nextButton);
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
      this.lastButton = new CommandBarButton();
      this.buttonsStripElement.Items.Add((RadCommandBarBaseItem) this.lastButton);
    }

    protected virtual void CreateTextBoxStripElementChildElements()
    {
      this.textBoxStripElement = new CommandBarStripElement();
      this.textBoxStripElement.MinSize = Size.Empty;
      this.textBoxStripElement.EnableDragging = false;
      this.textBoxStripElement.EnableFloating = false;
      this.textBoxStripElement.DesiredLocation = new PointF(10000f, 0.0f);
      this.textBoxStripElement.Grip.Visibility = ElementVisibility.Collapsed;
      this.textBoxStripElement.OverflowButton.Visibility = ElementVisibility.Collapsed;
      this.commandBarRowElement.Strips.Add(this.textBoxStripElement);
      this.pageLabel = new CommandBarLabel();
      this.pageLabel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelPagesLabel");
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.pageLabel);
      this.pageNumberTextBox = new CommandBarTextBox();
      this.pageNumberTextBox.TextBoxElement.MinSize = new Size(30, 20);
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.pageNumberTextBox);
      this.ofPagesLabel = new CommandBarLabel();
      this.ofPagesLabel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelOfPagesLabel");
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.ofPagesLabel);
      this.numberOfPagesLabel = new CommandBarLabel();
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.numberOfPagesLabel);
    }

    public override bool Focus()
    {
      return false;
    }

    protected virtual void WireEvents()
    {
      this.FirstButton.Click += new EventHandler(this.FirstButton_Click);
      this.PreviousButton.Click += new EventHandler(this.PreviousButton_Click);
      this.FastBackButton.Click += new EventHandler(this.FastBackButton_Click);
      this.FastForwardButton.Click += new EventHandler(this.FastForwardButton_Click);
      this.NextButton.Click += new EventHandler(this.NextButton_Click);
      this.LastButton.Click += new EventHandler(this.LastButton_Click);
      this.PageNumberTextBox.KeyDown += new KeyEventHandler(this.PageNumberTextBox_KeyDown);
    }

    protected virtual void UnwireEvents()
    {
      this.FirstButton.Click -= new EventHandler(this.FirstButton_Click);
      this.PreviousButton.Click -= new EventHandler(this.PreviousButton_Click);
      this.FastBackButton.Click -= new EventHandler(this.FastBackButton_Click);
      this.FastForwardButton.Click -= new EventHandler(this.FastForwardButton_Click);
      this.NextButton.Click -= new EventHandler(this.NextButton_Click);
      this.LastButton.Click -= new EventHandler(this.LastButton_Click);
      this.PageNumberTextBox.KeyDown -= new KeyEventHandler(this.PageNumberTextBox_KeyDown);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      this.Detach();
      base.DisposeManagedResources();
    }

    [Description("Gets or sets the number of buttons with numbers in the paging panel.")]
    public int NumericButtonsCount
    {
      get
      {
        return (int) this.GetValue(PagingPanelElement.NumericButtonsCountProperty);
      }
      set
      {
        int num = (int) this.SetValue(PagingPanelElement.NumericButtonsCountProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the button that navigates to the first page.")]
    public Image FirstPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(PagingPanelElement.FirstPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(PagingPanelElement.FirstPageButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates to the previous page.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image PreviousPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(PagingPanelElement.PreviousPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(PagingPanelElement.PreviousPageButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates next page.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image NextPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(PagingPanelElement.NextPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(PagingPanelElement.NextPageButtonImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the button that navigates to the last page")]
    public Image LastPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(PagingPanelElement.LastPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(PagingPanelElement.LastPageButtonImageProperty, (object) value);
      }
    }

    [Description("Gets the command bar element")]
    public RadCommandBarElement CommandBar
    {
      get
      {
        return this.commandBar;
      }
    }

    [Description("Gets the command bar row element.")]
    public CommandBarRowElement CommandBarRowElement
    {
      get
      {
        return this.commandBarRowElement;
      }
    }

    [Description("Gets the buttons strip element.")]
    public CommandBarStripElement ButtonsStripElement
    {
      get
      {
        return this.buttonsStripElement;
      }
    }

    [Description("Gets the button that navigates to the first page.")]
    public CommandBarButton FirstButton
    {
      get
      {
        return this.firstButton;
      }
    }

    [Description("Gets the button that navigates to the previous page.")]
    public CommandBarButton PreviousButton
    {
      get
      {
        return this.previousButton;
      }
    }

    [Description("Gets the button that navigates fast in the backward direction.")]
    public CommandBarButton FastBackButton
    {
      get
      {
        return this.fastBackButton;
      }
    }

    [Description("Gets the button that navigates fast in the forward direction.")]
    public CommandBarButton FastForwardButton
    {
      get
      {
        return this.fastForwardButton;
      }
    }

    [Description("Gets the button that navigates to the next page.")]
    public CommandBarButton NextButton
    {
      get
      {
        return this.nextButton;
      }
    }

    [Description("Gets the button that navigates to the last page.")]
    public CommandBarButton LastButton
    {
      get
      {
        return this.lastButton;
      }
    }

    [Description("Gets the text box strip element.")]
    public CommandBarStripElement TextBoxStripElement
    {
      get
      {
        return this.textBoxStripElement;
      }
    }

    [Description("Gets the label that shows the \"Page\" text.")]
    public CommandBarLabel PageLabel
    {
      get
      {
        return this.pageLabel;
      }
    }

    [Description("Gets the text box that shows the current page.")]
    public CommandBarTextBox PageNumberTextBox
    {
      get
      {
        return this.pageNumberTextBox;
      }
    }

    [Description("Gets the label that shows the \"of\" text.")]
    public CommandBarLabel OfPagesLabel
    {
      get
      {
        return this.ofPagesLabel;
      }
    }

    [Description("Gets the label that shows the total number of pages.")]
    public CommandBarLabel NumberOfPagesLabel
    {
      get
      {
        return this.numberOfPagesLabel;
      }
    }

    [Description("Gets or sets whether the button that navigates to the first page is visible.")]
    [DefaultValue(true)]
    public bool ShowFirstButton
    {
      get
      {
        return this.showFirstButton;
      }
      set
      {
        if (this.showFirstButton == value)
          return;
        this.showFirstButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowFirstButton));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the button that navigates to the previous page is visible.")]
    public bool ShowPreviousButton
    {
      get
      {
        return this.showPreviousButton;
      }
      set
      {
        if (this.showPreviousButton == value)
          return;
        this.showPreviousButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowPreviousButton));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the button that navigates fast backward is visible.")]
    public bool ShowFastBackButton
    {
      get
      {
        return this.showFastBackButton;
      }
      set
      {
        if (this.showFastBackButton == value)
          return;
        this.showFastBackButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowFastBackButton));
      }
    }

    [Description("Gets or sets whether the buttons that navigate to a concrete page are visible.")]
    [DefaultValue(true)]
    public bool ShowNumericalButtons
    {
      get
      {
        return this.showNumericalButtons;
      }
      set
      {
        if (this.showNumericalButtons == value)
          return;
        this.showNumericalButtons = value;
        this.OnNotifyPropertyChanged(nameof (ShowNumericalButtons));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the button that navigates fast forward is visible.")]
    public bool ShowFastForwardButton
    {
      get
      {
        return this.showFastForwardButton;
      }
      set
      {
        if (this.showFastForwardButton == value)
          return;
        this.showFastForwardButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowFastForwardButton));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the button that navigates to the next page is visible.")]
    public bool ShowNextButton
    {
      get
      {
        return this.showNextButton;
      }
      set
      {
        if (this.showNextButton == value)
          return;
        this.showNextButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowNextButton));
      }
    }

    [Description("Gets or sets whether the button that navigates to the last page is visible.")]
    [DefaultValue(true)]
    public bool ShowLastButton
    {
      get
      {
        return this.showLastButton;
      }
      set
      {
        if (this.showLastButton == value)
          return;
        this.showLastButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowLastButton));
      }
    }

    [Description("Gets or sets whether the strip element holding the page navigation buttons is visible.")]
    [DefaultValue(true)]
    public bool ShowButtonsStripElement
    {
      get
      {
        return this.showButtonsStripElement;
      }
      set
      {
        if (this.showButtonsStripElement == value)
          return;
        this.showButtonsStripElement = value;
        this.OnNotifyPropertyChanged(nameof (ShowButtonsStripElement));
      }
    }

    [Description("Gets or sets whether the strip element holding the page navigation text box is visible.")]
    [DefaultValue(true)]
    public bool ShowTextBoxStripElement
    {
      get
      {
        return this.showTextBoxStripElement;
      }
      set
      {
        if (this.showTextBoxStripElement == value)
          return;
        this.showTextBoxStripElement = value;
        this.OnNotifyPropertyChanged(nameof (ShowTextBoxStripElement));
      }
    }

    private void UpdateVisibility()
    {
      if (this.GridViewElement.Template.EnablePaging)
      {
        if (this.Visibility == ElementVisibility.Visible)
          return;
        this.Visibility = ElementVisibility.Visible;
      }
      else
      {
        if (this.Visibility == ElementVisibility.Collapsed)
          return;
        this.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected virtual void UpdateButtonsStripElement()
    {
      this.ButtonsStripElement.SuspendLayout();
      this.ButtonsStripElement.Items.Clear();
      this.ButtonsStripElement.OverflowButton.OverflowPanel.Layout.Children.Clear();
      bool flag = false;
      if (this.showFirstButton)
      {
        flag = true;
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.FirstButton);
      }
      if (this.ShowPreviousButton)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        flag = true;
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.PreviousButton);
      }
      if (this.ShowFastBackButton)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        flag = true;
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.FastBackButton);
      }
      if (this.ShowNumericalButtons)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        flag = true;
        int num1 = this.GridViewElement.Template.PageIndex - this.NumericButtonsCount / 2;
        int num2 = num1 + this.NumericButtonsCount;
        if (this.GridViewElement.Template.PageIndex - this.NumericButtonsCount / 2 < 0)
        {
          num1 = 0;
          num2 = num1 + this.NumericButtonsCount;
          if (num2 > this.GridViewElement.Template.TotalPages)
            num2 = this.GridViewElement.Template.TotalPages;
        }
        else if (this.GridViewElement.Template.PageIndex + this.NumericButtonsCount / 2 >= this.GridViewElement.Template.TotalPages)
        {
          num2 = this.GridViewElement.Template.TotalPages;
          num1 = num2 - this.NumericButtonsCount;
          if (num1 < 0)
            num1 = 0;
        }
        for (int index = num1; index < num2; ++index)
        {
          CommandBarToggleButton button = new CommandBarToggleButton();
          button.Text = (index + 1).ToString();
          button.Image = (Image) null;
          button.DrawText = true;
          button.StretchHorizontally = true;
          if (index == this.GridViewElement.Template.PageIndex)
            button.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          button.Click += (EventHandler) ((sender, e) => this.GridViewElement.Template.MoveToPage(int.Parse(button.Text) - 1));
          button.ToggleStateChanging += (StateChangingEventHandler) ((sender, e) => e.Cancel = true);
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) button);
        }
      }
      if (this.ShowFastForwardButton)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        flag = true;
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.FastForwardButton);
      }
      if (this.ShowNextButton)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        flag = true;
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.NextButton);
      }
      if (this.ShowLastButton)
      {
        if (flag)
          this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.ButtonsStripElement.Items.Add((RadCommandBarBaseItem) this.LastButton);
      }
      this.ButtonsStripElement.ResumeLayout(false);
    }

    protected virtual void UpdateTextBoxStripElement()
    {
      this.PageLabel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelPagesLabel");
      this.PageNumberTextBox.Text = (this.GridViewElement.Template.PageIndex + 1).ToString();
      this.OfPagesLabel.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelOfPagesLabel");
      this.NumberOfPagesLabel.Text = this.GridViewElement.Template.TotalPages > 0 ? this.GridViewElement.Template.TotalPages.ToString() : "1";
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName == "NumericButtonsCount")
        this.UpdateView();
      else if (e.PropertyName == "FirstPageButtonImage")
        this.FirstButton.Image = this.FirstPageButtonImage;
      else if (e.PropertyName == "PreviousPageButtonImage")
        this.PreviousButton.Image = this.PreviousPageButtonImage;
      else if (e.PropertyName == "NextPageButtonImage")
        this.NextButton.Image = this.NextPageButtonImage;
      else if (e.PropertyName == "LastPageButtonImage")
        this.LastButton.Image = this.LastPageButtonImage;
      else if (e.PropertyName == "ShowFirstButton" || e.PropertyName == "ShowPreviousButton" || (e.PropertyName == "ShowFastBackButton" || e.PropertyName == "ShowNumericalButtons") || (e.PropertyName == "ShowFastForwardButton" || e.PropertyName == "ShowNextButton" || e.PropertyName == "ShowLastButton"))
        this.UpdateView();
      else if (e.PropertyName == "ShowButtonsStripElement")
      {
        if (this.ShowButtonsStripElement)
        {
          this.UpdateView();
          this.ButtonsStripElement.Visibility = ElementVisibility.Visible;
        }
        else
          this.ButtonsStripElement.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (!(e.PropertyName == "ShowTextBoxStripElement"))
          return;
        if (this.ShowTextBoxStripElement)
        {
          this.UpdateView();
          this.TextBoxStripElement.Visibility = ElementVisibility.Visible;
        }
        else
          this.TextBoxStripElement.Visibility = ElementVisibility.Collapsed;
      }
    }

    private void FirstButton_Click(object sender, EventArgs e)
    {
      this.GridViewElement.Template.MoveToFirstPage();
    }

    private void PreviousButton_Click(object sender, EventArgs e)
    {
      this.GridViewElement.Template.MoveToPreviousPage();
    }

    private void FastBackButton_Click(object sender, EventArgs e)
    {
      int pageIndex = this.GridViewElement.Template.PageIndex - this.NumericButtonsCount;
      if (pageIndex < 0)
        pageIndex = 0;
      this.GridViewElement.Template.MoveToPage(pageIndex);
    }

    private void FastForwardButton_Click(object sender, EventArgs e)
    {
      int pageIndex = this.GridViewElement.Template.PageIndex + this.NumericButtonsCount;
      if (pageIndex >= this.GridViewElement.Template.TotalPages)
        pageIndex = this.GridViewElement.Template.TotalPages - 1;
      this.GridViewElement.Template.MoveToPage(pageIndex);
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
      this.GridViewElement.Template.MoveToNextPage();
    }

    private void LastButton_Click(object sender, EventArgs e)
    {
      this.GridViewElement.Template.MoveToLastPage();
    }

    private void PageNumberTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      int result = -1;
      if (int.TryParse(this.PageNumberTextBox.Text, out result) && result > 0 && result <= this.GridViewElement.Template.TotalPages)
      {
        this.GridViewElement.Template.MoveToPage(result - 1);
        this.pageNumberTextBox.SelectAll();
      }
      else
      {
        this.PageNumberTextBox.Text = (this.GridViewElement.Template.PageIndex + 1).ToString();
        this.pageNumberTextBox.SelectAll();
      }
    }

    public void Initialize(RadGridViewElement gridViewElement, GridViewInfo viewInfo)
    {
      this.gridViewElement = gridViewElement;
      this.viewInfo = viewInfo;
      this.viewInfo.ViewTemplate.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
      this.UpdateVisibility();
    }

    public void Detach()
    {
      this.viewInfo.ViewTemplate.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
    }

    public void UpdateView()
    {
      if (!this.GridViewElement.Template.EnablePaging)
        return;
      if (this.ShowButtonsStripElement)
        this.UpdateButtonsStripElement();
      if (this.ShowTextBoxStripElement)
        this.UpdateTextBoxStripElement();
      this.InvalidateMeasure(true);
    }

    [Description("Gets or the RadGridViewElement that owns this view.")]
    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    [Description("Gets the GridViewInfo that this view represents.")]
    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    public GridEventType DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    public EventListenerPriority Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    public GridEventProcessMode DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.PostProcess;
      }
    }

    public GridViewEventResult PreProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public GridViewEventResult ProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public GridViewEventResult PostProcessEvent(GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.ViewChanged)
      {
        DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
        if (changedEventArgs.Action == ViewChangedAction.Add || changedEventArgs.Action == ViewChangedAction.Remove || (changedEventArgs.Action == ViewChangedAction.FilteringChanged || changedEventArgs.Action == ViewChangedAction.GroupingChanged) || (changedEventArgs.Action == ViewChangedAction.PagingChanged || changedEventArgs.Action == ViewChangedAction.Reset))
        {
          this.UpdateVisibility();
          this.UpdateView();
        }
      }
      return (GridViewEventResult) null;
    }

    public bool AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(new SizeF(availableSize.Width, float.PositiveInfinity));
    }
  }
}
