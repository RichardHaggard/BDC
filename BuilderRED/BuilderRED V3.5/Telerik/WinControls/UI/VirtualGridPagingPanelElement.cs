// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridPagingPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class VirtualGridPagingPanelElement : LightVisualElement
  {
    public static RadProperty NumericButtonsCountProperty = RadProperty.Register(nameof (NumericButtonsCount), typeof (int), typeof (VirtualGridPagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.None));
    public static RadProperty FirstPageButtonImageProperty = RadProperty.Register(nameof (FirstPageButtonImage), typeof (Image), typeof (VirtualGridPagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty PreviousPageButtonImageProperty = RadProperty.Register(nameof (PreviousPageButtonImage), typeof (Image), typeof (VirtualGridPagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NextPageButtonImageProperty = RadProperty.Register(nameof (NextPageButtonImage), typeof (Image), typeof (VirtualGridPagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LastPageButtonImageProperty = RadProperty.Register(nameof (LastPageButtonImage), typeof (Image), typeof (VirtualGridPagingPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private bool showButtonsStripElement = true;
    private bool showTextBoxStripElement = true;
    private bool showFirstButton = true;
    private bool showPreviousButton = true;
    private bool showFastBackButton = true;
    private bool showNumericalButtons = true;
    private bool showFastForwardButton = true;
    private bool showNextButton = true;
    private bool showLastButton = true;
    private VirtualGridTableElement gridViewElement;
    private VirtualGridViewInfo viewInfo;
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

    public VirtualGridPagingPanelElement(
      VirtualGridTableElement gridViewElement,
      VirtualGridViewInfo viewInfo)
    {
      this.gridViewElement = gridViewElement;
      this.viewInfo = viewInfo;
      this.UpdateVisibility();
    }

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
      this.pageLabel.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelPagesLabel");
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.pageLabel);
      this.pageNumberTextBox = new CommandBarTextBox();
      this.pageNumberTextBox.TextBoxElement.MinSize = new Size(30, 20);
      this.textBoxStripElement.Items.Add((RadCommandBarBaseItem) this.pageNumberTextBox);
      this.ofPagesLabel = new CommandBarLabel();
      this.ofPagesLabel.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelOfPagesLabel");
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
      base.DisposeManagedResources();
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (PagingPanelElement);
      }
    }

    [Description("Gets the VirtualGridTableElement which owns this view.")]
    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    [Description("Gets the GridViewInfo that this view represents.")]
    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    [Description("Gets or sets the number of buttons with numbers in the paging panel.")]
    public int NumericButtonsCount
    {
      get
      {
        return (int) this.GetValue(VirtualGridPagingPanelElement.NumericButtonsCountProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridPagingPanelElement.NumericButtonsCountProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates to the first page.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image FirstPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridPagingPanelElement.FirstPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridPagingPanelElement.FirstPageButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates to the previous page.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image PreviousPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridPagingPanelElement.PreviousPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridPagingPanelElement.PreviousPageButtonImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the button that navigates next page.")]
    public Image NextPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridPagingPanelElement.NextPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridPagingPanelElement.NextPageButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates to the last page")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image LastPageButtonImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridPagingPanelElement.LastPageButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridPagingPanelElement.LastPageButtonImageProperty, (object) value);
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

    [Description("Gets or sets whether the button that navigates fast backward is visible.")]
    [DefaultValue(true)]
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

    [Description("Gets or sets whether the button that navigates fast forward is visible.")]
    [DefaultValue(true)]
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

    [Description("Gets or sets whether the button that navigates to the next page is visible.")]
    [DefaultValue(true)]
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

    [DefaultValue(true)]
    [Description("Gets or sets whether the button that navigates to the last page is visible.")]
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

    [DefaultValue(true)]
    [Description("Gets or sets whether the strip element holding the page navigation buttons is visible.")]
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
      if (this.ViewInfo.EnablePaging)
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
        int num1 = this.ViewInfo.PageIndex - this.NumericButtonsCount / 2;
        int num2 = num1 + this.NumericButtonsCount;
        if (this.ViewInfo.PageIndex - this.NumericButtonsCount / 2 < 0)
        {
          num1 = 0;
          num2 = num1 + this.NumericButtonsCount;
          if (num2 > this.ViewInfo.TotalPages)
            num2 = this.ViewInfo.TotalPages;
        }
        else if (this.ViewInfo.PageIndex + this.NumericButtonsCount / 2 >= this.ViewInfo.TotalPages)
        {
          num2 = this.ViewInfo.TotalPages;
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
          if (index == this.ViewInfo.PageIndex)
            button.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          button.Click += (EventHandler) ((sender, e) =>
          {
            this.ViewInfo.MoveToPage(int.Parse(button.Text) - 1);
            this.TableElement.InvalidateMeasure(true);
            this.UpdateView();
          });
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
      this.PageLabel.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelPagesLabel");
      this.OfPagesLabel.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PagingPanelOfPagesLabel");
      if (this.ViewInfo.TotalPages > 0)
      {
        this.PageNumberTextBox.Text = (this.ViewInfo.PageIndex + 1).ToString();
        this.NumberOfPagesLabel.Text = this.ViewInfo.TotalPages.ToString();
      }
      else
      {
        this.PageNumberTextBox.Text = "0";
        this.NumberOfPagesLabel.Text = "0";
      }
    }

    public void UpdateView()
    {
      if (!this.ViewInfo.EnablePaging)
        return;
      if (this.ShowButtonsStripElement)
        this.UpdateButtonsStripElement();
      if (this.ShowTextBoxStripElement)
        this.UpdateTextBoxStripElement();
      this.InvalidateMeasure(true);
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
      this.ViewInfo.MoveToFirstPage();
      this.TableElement.InvalidateMeasure(true);
    }

    private void PreviousButton_Click(object sender, EventArgs e)
    {
      this.ViewInfo.MoveToPreviousPage();
      this.TableElement.InvalidateMeasure(true);
    }

    private void FastBackButton_Click(object sender, EventArgs e)
    {
      int pageIndex = this.ViewInfo.PageIndex - this.NumericButtonsCount;
      if (pageIndex < 0)
        pageIndex = 0;
      this.ViewInfo.MoveToPage(pageIndex);
      this.TableElement.InvalidateMeasure(true);
    }

    private void FastForwardButton_Click(object sender, EventArgs e)
    {
      int pageIndex = this.ViewInfo.PageIndex + this.NumericButtonsCount;
      if (pageIndex >= this.ViewInfo.TotalPages)
        pageIndex = this.ViewInfo.TotalPages - 1;
      this.ViewInfo.MoveToPage(pageIndex);
      this.TableElement.InvalidateMeasure(true);
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
      this.ViewInfo.MoveToNextPage();
      this.TableElement.InvalidateMeasure(true);
    }

    private void LastButton_Click(object sender, EventArgs e)
    {
      this.ViewInfo.MoveToLastPage();
      this.TableElement.InvalidateMeasure(true);
    }

    private void PageNumberTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      int result = -1;
      if (int.TryParse(this.PageNumberTextBox.Text, out result) && result > 0 && result <= this.ViewInfo.TotalPages)
      {
        this.ViewInfo.MoveToPage(result - 1);
        this.pageNumberTextBox.SelectAll();
        this.TableElement.InvalidateMeasure(true);
      }
      else
      {
        this.PageNumberTextBox.Text = (this.ViewInfo.PageIndex + 1).ToString();
        this.pageNumberTextBox.SelectAll();
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(new SizeF(availableSize.Width, float.PositiveInfinity));
    }
  }
}
