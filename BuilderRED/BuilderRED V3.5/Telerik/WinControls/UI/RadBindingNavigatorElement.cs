// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBindingNavigatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadBindingNavigatorElement : RadCommandBarElement
  {
    public static RadProperty MoveFirstItemButtonImageProperty = RadProperty.Register(nameof (MoveFirstItemButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MovePreviousItemButtonImageProperty = RadProperty.Register(nameof (MovePreviousItemButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MoveNextItemButtonImageProperty = RadProperty.Register(nameof (MoveNextItemButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MoveLastItemButtonImageProperty = RadProperty.Register(nameof (MoveLastItemButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AddNewButtonImageProperty = RadProperty.Register(nameof (AddNewButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DeleteButtonImageProperty = RadProperty.Register(nameof (DeleteButtonImage), typeof (Image), typeof (RadBindingNavigatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private string countItemFormat = "of {0}";
    private bool autoHandleAddNew = true;
    private CommandBarRowElement commandBarRowElement;
    private CommandBarStripElement firstTopStripElement;
    private CommandBarButton firstButton;
    private CommandBarButton previousButton;
    private CommandBarTextBox currentNumberTextBox;
    private CommandBarLabel pageLabel;
    private CommandBarButton nextButton;
    private CommandBarButton lastButton;
    private CommandBarStripElement secondBottomStripElement;
    private CommandBarButton addNewButton;
    private CommandBarButton deleteButton;
    private BindingSource bindingSource;
    private IDesignerHost designerHost;
    private IComponentChangeService changeService;
    private RadBindingNavigator navigator;
    private bool attachedEvents;

    protected override void LoadCore()
    {
      base.LoadCore();
      this.MapControls();
      if (!this.attachedEvents)
        this.AttachEvents();
      if (this.secondBottomStripElement == null)
        return;
      this.secondBottomStripElement.DesiredLocation = new PointF(10000f, 0.0f);
    }

    protected virtual void MapControls()
    {
      foreach (CommandBarRowElement row in this.Rows)
      {
        if (row.Name == this.Navigator.Name + "RowElement" && this.commandBarRowElement == null)
          this.commandBarRowElement = row;
        foreach (CommandBarStripElement strip in row.Strips)
        {
          if (strip.DisplayName == this.Navigator.Name + "FirstStrip" && this.firstTopStripElement == null)
            this.firstTopStripElement = strip;
          else if (strip.DisplayName == this.Navigator.Name + "SecondStrip" && this.secondBottomStripElement == null)
            this.secondBottomStripElement = strip;
          foreach (RadCommandBarBaseItem commandBarBaseItem in strip.Items)
          {
            string name = commandBarBaseItem.Name;
            if (commandBarBaseItem.Site != null)
              name = commandBarBaseItem.Site.Name;
            if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "MoveFirstItem" && this.firstButton == null)
              this.firstButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "MovePreviousItem" && this.previousButton == null)
              this.previousButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "MoveNextItem" && this.nextButton == null)
              this.nextButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "MoveLastItem" && this.lastButton == null)
              this.lastButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "AddNewItem" && this.addNewButton == null)
              this.addNewButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarButton && name == this.Navigator.Name + "DeleteItem" && this.deleteButton == null)
              this.deleteButton = commandBarBaseItem as CommandBarButton;
            else if (commandBarBaseItem is CommandBarTextBox && name == this.Navigator.Name + "PositionItem" && this.currentNumberTextBox == null)
              this.currentNumberTextBox = commandBarBaseItem as CommandBarTextBox;
            else if (commandBarBaseItem is CommandBarLabel && name == this.Navigator.Name + "CountItem" && this.pageLabel == null)
              this.pageLabel = commandBarBaseItem as CommandBarLabel;
          }
        }
      }
    }

    public virtual void AddStandardItems()
    {
      if (this.Navigator.Site == null)
      {
        this.commandBarRowElement = new CommandBarRowElement();
        this.Rows.Add(this.commandBarRowElement);
      }
      else
      {
        this.designerHost = this.Navigator.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
        this.changeService = this.Navigator.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
        this.commandBarRowElement = (CommandBarRowElement) this.designerHost.CreateComponent(typeof (CommandBarRowElement), this.Navigator.Name + "RowElement");
        this.changeService.OnComponentChanging((object) this.Navigator.Site.Component, (MemberDescriptor) null);
        this.Rows.Add(this.commandBarRowElement);
        this.changeService.OnComponentChanged((object) this.Navigator.Site.Component, (MemberDescriptor) null, (object) null, (object) null);
      }
      this.CreateFirstTopStripElementChildElements();
      this.CreateSecondBottomStripElementChildElements();
      if (!this.attachedEvents)
        this.AttachEvents();
      this.ElementTree.ApplyThemeToElementTree();
    }

    protected override void DisposeManagedResources()
    {
      this.DetachEvents();
      base.DisposeManagedResources();
    }

    protected virtual void CreateFirstTopStripElementChildElements()
    {
      if (this.Navigator.Site == null)
      {
        this.firstTopStripElement = new CommandBarStripElement();
        this.commandBarRowElement.Strips.Add(this.firstTopStripElement);
        this.firstButton = new CommandBarButton();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.firstButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.previousButton = new CommandBarButton();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.previousButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.currentNumberTextBox = new CommandBarTextBox();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.currentNumberTextBox);
        this.pageLabel = new CommandBarLabel();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.pageLabel);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.nextButton = new CommandBarButton();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.nextButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.lastButton = new CommandBarButton();
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.lastButton);
      }
      else
      {
        this.firstTopStripElement = (CommandBarStripElement) this.designerHost.CreateComponent(typeof (CommandBarStripElement), this.Navigator.Name + "FirstStrip");
        this.firstButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "MoveFirstItem");
        this.previousButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "MovePreviousItem");
        this.currentNumberTextBox = (CommandBarTextBox) this.designerHost.CreateComponent(typeof (CommandBarTextBox), this.Navigator.Name + "PositionItem");
        this.pageLabel = (CommandBarLabel) this.designerHost.CreateComponent(typeof (CommandBarLabel), this.Navigator.Name + "CountItem");
        this.nextButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "MoveNextItem");
        this.lastButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "MoveLastItem");
        this.changeService.OnComponentChanging((object) this.Navigator.Site.Component, (MemberDescriptor) null);
        this.commandBarRowElement.Strips.Add(this.firstTopStripElement);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.firstButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.designerHost.CreateComponent(typeof (CommandBarSeparator)));
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.previousButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.designerHost.CreateComponent(typeof (CommandBarSeparator)));
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.currentNumberTextBox);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.pageLabel);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.designerHost.CreateComponent(typeof (CommandBarSeparator)));
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.nextButton);
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.designerHost.CreateComponent(typeof (CommandBarSeparator)));
        this.firstTopStripElement.Items.Add((RadCommandBarBaseItem) this.lastButton);
        this.changeService.OnComponentChanged((object) this.Navigator.Site.Component, (MemberDescriptor) null, (object) null, (object) null);
      }
      this.firstTopStripElement.MinSize = Size.Empty;
      this.firstTopStripElement.EnableDragging = false;
      this.firstTopStripElement.EnableFloating = false;
      this.firstTopStripElement.Grip.Visibility = ElementVisibility.Collapsed;
      this.firstTopStripElement.OverflowButton.Visibility = ElementVisibility.Collapsed;
      this.firstButton.Margin = new Padding(3, 0, 0, 0);
      this.pageLabel.Text = "of {0}";
    }

    protected virtual void CreateSecondBottomStripElementChildElements()
    {
      if (this.Navigator.Site == null)
      {
        this.secondBottomStripElement = new CommandBarStripElement();
        this.commandBarRowElement.Strips.Add(this.secondBottomStripElement);
        this.addNewButton = new CommandBarButton();
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) this.addNewButton);
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) new CommandBarSeparator());
        this.deleteButton = new CommandBarButton();
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) this.deleteButton);
      }
      else
      {
        this.secondBottomStripElement = (CommandBarStripElement) this.designerHost.CreateComponent(typeof (CommandBarStripElement), this.Navigator.Name + "SecondStrip");
        this.addNewButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "AddNewItem");
        this.deleteButton = (CommandBarButton) this.designerHost.CreateComponent(typeof (CommandBarButton), this.Navigator.Name + "DeleteItem");
        this.commandBarRowElement.Strips.Add(this.secondBottomStripElement);
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) this.addNewButton);
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) this.designerHost.CreateComponent(typeof (CommandBarSeparator)));
        this.secondBottomStripElement.Items.Add((RadCommandBarBaseItem) this.deleteButton);
      }
      this.secondBottomStripElement.MinSize = Size.Empty;
      this.secondBottomStripElement.EnableDragging = false;
      this.secondBottomStripElement.EnableFloating = false;
      this.secondBottomStripElement.DesiredLocation = new PointF(10000f, 0.0f);
      this.addNewButton.Margin = new Padding(3, 0, 0, 0);
      this.secondBottomStripElement.Grip.Visibility = ElementVisibility.Collapsed;
      this.secondBottomStripElement.OverflowButton.Visibility = ElementVisibility.Collapsed;
    }

    protected virtual void AttachEvents()
    {
      if (this.FirstButton != null)
        this.FirstButton.Click += new EventHandler(this.FirstButton_Click);
      if (this.PreviousButton != null)
        this.PreviousButton.Click += new EventHandler(this.PreviousButton_Click);
      if (this.NextButton != null)
        this.NextButton.Click += new EventHandler(this.NextButton_Click);
      if (this.LastButton != null)
        this.LastButton.Click += new EventHandler(this.LastButton_Click);
      if (this.CurrentNumberTextBox != null)
        this.CurrentNumberTextBox.KeyDown += new KeyEventHandler(this.currentNumberTextBox_KeyDown);
      if (this.AddNewButton != null)
        this.AddNewButton.Click += new EventHandler(this.AddNewButton_Click);
      if (this.DeleteButton != null)
        this.DeleteButton.Click += new EventHandler(this.DeleteButton_Click);
      this.attachedEvents = true;
    }

    protected virtual void DetachEvents()
    {
      if (this.FirstButton != null)
        this.FirstButton.Click -= new EventHandler(this.FirstButton_Click);
      if (this.PreviousButton != null)
        this.PreviousButton.Click -= new EventHandler(this.PreviousButton_Click);
      if (this.NextButton != null)
        this.NextButton.Click -= new EventHandler(this.NextButton_Click);
      if (this.LastButton != null)
        this.LastButton.Click -= new EventHandler(this.LastButton_Click);
      if (this.CurrentNumberTextBox != null)
        this.CurrentNumberTextBox.KeyDown -= new KeyEventHandler(this.currentNumberTextBox_KeyDown);
      if (this.AddNewButton != null)
        this.AddNewButton.Click -= new EventHandler(this.AddNewButton_Click);
      if (this.DeleteButton == null)
        return;
      this.DeleteButton.Click -= new EventHandler(this.DeleteButton_Click);
    }

    public CommandBarRowElement CommandBarRowElement
    {
      get
      {
        return this.commandBarRowElement;
      }
      set
      {
        this.commandBarRowElement = value;
      }
    }

    public CommandBarStripElement FirstTopStripElement
    {
      get
      {
        return this.firstTopStripElement;
      }
      set
      {
        this.firstTopStripElement = value;
      }
    }

    public CommandBarButton FirstButton
    {
      get
      {
        return this.firstButton;
      }
      set
      {
        this.firstButton = value;
      }
    }

    public CommandBarButton PreviousButton
    {
      get
      {
        return this.previousButton;
      }
      set
      {
        this.previousButton = value;
      }
    }

    public CommandBarTextBox CurrentNumberTextBox
    {
      get
      {
        return this.currentNumberTextBox;
      }
      set
      {
        this.currentNumberTextBox = value;
      }
    }

    public CommandBarLabel PageLabel
    {
      get
      {
        return this.pageLabel;
      }
      set
      {
        this.pageLabel = value;
      }
    }

    public CommandBarButton NextButton
    {
      get
      {
        return this.nextButton;
      }
      set
      {
        this.nextButton = value;
      }
    }

    public CommandBarButton LastButton
    {
      get
      {
        return this.lastButton;
      }
      set
      {
        this.lastButton = value;
      }
    }

    public CommandBarStripElement SecondBottomStripElement
    {
      get
      {
        return this.secondBottomStripElement;
      }
      set
      {
        this.secondBottomStripElement = value;
      }
    }

    public CommandBarButton AddNewButton
    {
      get
      {
        return this.addNewButton;
      }
      set
      {
        this.addNewButton = value;
      }
    }

    public CommandBarButton DeleteButton
    {
      get
      {
        return this.deleteButton;
      }
      set
      {
        this.deleteButton = value;
      }
    }

    public BindingSource BindingSource
    {
      get
      {
        return this.bindingSource;
      }
      set
      {
        if (this.bindingSource == value)
          return;
        if (value == null && this.bindingSource != null)
        {
          this.bindingSource.ListChanged -= new ListChangedEventHandler(this.bindingSource_ListChanged);
          this.bindingSource.PositionChanged -= new EventHandler(this.bindingSource_PositionChanged);
          this.DetachEvents();
          this.attachedEvents = false;
        }
        this.bindingSource = value;
        if (this.bindingSource != null)
        {
          this.bindingSource.ListChanged += new ListChangedEventHandler(this.bindingSource_ListChanged);
          this.bindingSource.PositionChanged += new EventHandler(this.bindingSource_PositionChanged);
        }
        if (this.Navigator.IsInitializing)
          return;
        this.MapControls();
        this.Bind();
        if (this.attachedEvents)
          return;
        this.AttachEvents();
      }
    }

    public RadBindingNavigator Navigator
    {
      get
      {
        if (this.navigator == null)
          this.navigator = this.ElementTree.Control as RadBindingNavigator;
        return this.navigator;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadCommandBarElement);
      }
    }

    [Description("Gets or sets the image of the buton that navigates to the first item.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image MoveFirstItemButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.MoveFirstItemButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.MoveFirstItemButtonImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the button that navigates to the previous item.")]
    public Image MovePreviousItemButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.MovePreviousItemButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.MovePreviousItemButtonImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the button that navigates next item.")]
    public Image MoveNextItemButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.MoveNextItemButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.MoveNextItemButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that navigates to the last item")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image MoveLastItemButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.MoveLastItemButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.MoveLastItemButtonImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image of the button that adds new item")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image AddNewButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.AddNewButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.AddNewButtonImageProperty, (object) value);
      }
    }

    [Description(" Gets or sets the image of the button that deletes the current item.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image DeleteButtonImage
    {
      get
      {
        return (Image) this.GetValue(RadBindingNavigatorElement.DeleteButtonImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadBindingNavigatorElement.DeleteButtonImageProperty, (object) value);
      }
    }

    public string CountItemFormat
    {
      get
      {
        return this.countItemFormat;
      }
      set
      {
        if (!(this.countItemFormat != value))
          return;
        this.countItemFormat = value;
        this.UpdateLabelText();
      }
    }

    [Description("Gets or sets a value indicating whether the control will handle internally the creation of new items.")]
    public bool AutoHandleAddNew
    {
      get
      {
        return this.autoHandleAddNew;
      }
      set
      {
        this.autoHandleAddNew = value;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property.Name == "RightToLeft")
        this.ArrangeButtons();
      if (e.Property.Name == "MoveFirstItemButtonImage" && this.FirstButton != null)
        this.FirstButton.Image = this.MoveFirstItemButtonImage;
      else if (e.Property.Name == "MovePreviousItemButtonImage" && this.PreviousButton != null)
        this.PreviousButton.Image = this.MovePreviousItemButtonImage;
      else if (e.Property.Name == "MoveNextItemButtonImage" && this.NextButton != null)
        this.NextButton.Image = this.MoveNextItemButtonImage;
      else if (e.Property.Name == "MoveLastItemButtonImage" && this.LastButton != null)
        this.LastButton.Image = this.MoveLastItemButtonImage;
      else if (e.Property.Name == "AddNewButtonImage" && this.AddNewButton != null)
      {
        this.AddNewButton.Image = this.AddNewButtonImage;
      }
      else
      {
        if (!(e.Property.Name == "DeleteButtonImage") || this.DeleteButton == null)
          return;
        this.DeleteButton.Image = this.DeleteButtonImage;
      }
    }

    private void ArrangeButtons()
    {
      if (this.RightToLeft)
      {
        this.previousButton.AngleTransform = 180f;
        this.firstButton.AngleTransform = 180f;
        this.nextButton.AngleTransform = 180f;
        this.lastButton.AngleTransform = 180f;
        this.lastButton.Margin = new Padding(0, 0, 0, 0);
        this.firstButton.Margin = new Padding(0, 0, 3, 0);
        this.deleteButton.Margin = new Padding(0, 0, 0, 0);
        this.addNewButton.Margin = new Padding(0, 0, 3, 0);
      }
      else
      {
        this.previousButton.AngleTransform = 0.0f;
        this.firstButton.AngleTransform = 0.0f;
        this.nextButton.AngleTransform = 0.0f;
        this.lastButton.AngleTransform = 0.0f;
        this.firstButton.Margin = new Padding(3, 0, 0, 0);
        this.lastButton.Margin = new Padding(0, 0, 0, 0);
        this.addNewButton.Margin = new Padding(3, 0, 0, 0);
        this.deleteButton.Margin = new Padding(0, 0, 0, 0);
      }
    }

    protected virtual void FirstButton_Click(object sender, EventArgs e)
    {
      if (this.BindingSource == null)
        return;
      this.BindingSource.MoveFirst();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "GridNavigatorMoveToPage", (object) 0);
      this.UpdateVisibility();
    }

    protected virtual void PreviousButton_Click(object sender, EventArgs e)
    {
      if (this.BindingSource == null)
        return;
      this.BindingSource.MovePrevious();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "GridNavigatorMoveToPage", (object) (this.BindingSource.Position + 1));
      this.UpdateVisibility();
    }

    protected virtual void NextButton_Click(object sender, EventArgs e)
    {
      if (this.BindingSource == null)
        return;
      this.BindingSource.MoveNext();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "GridNavigatorMoveToPage", (object) (this.BindingSource.Position + 1));
      this.UpdateVisibility();
    }

    protected virtual void LastButton_Click(object sender, EventArgs e)
    {
      if (this.BindingSource == null)
        return;
      this.BindingSource.MoveLast();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "GridNavigatorMoveToPage", (object) ++this.BindingSource.Position);
      this.UpdateVisibility();
    }

    protected virtual void currentNumberTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      int result = -1;
      if (int.TryParse(this.CurrentNumberTextBox.Text, out result) && this.BindingSource != null && (result > 0 && result <= this.BindingSource.Count))
      {
        this.BindingSource.Position = result - 1;
        this.CurrentNumberTextBox.SelectAll();
      }
      this.CurrentNumberTextBox.Text = (this.BindingSource.Position + 1).ToString();
      this.CurrentNumberTextBox.SelectAll();
    }

    protected virtual void DeleteButton_Click(object sender, EventArgs e)
    {
      if (this.BindingSource == null || !this.BindingSource.AllowRemove)
        return;
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Deleted");
      this.BindingSource.RemoveCurrent();
      this.UpdateVisibility();
    }

    protected virtual void AddNewButton_Click(object sender, EventArgs e)
    {
      if (!this.AutoHandleAddNew || this.BindingSource == null || !this.BindingSource.AllowNew)
        return;
      this.BindingSource.AddNew();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "AddNew");
      this.UpdateVisibility();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.UpdateVisibility();
    }

    protected virtual void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      this.UpdateVisibility();
    }

    protected virtual void bindingSource_PositionChanged(object sender, EventArgs e)
    {
      this.UpdateVisibility();
    }

    private void Bind()
    {
      this.UpdateVisibility();
    }

    public virtual void UpdateVisibility()
    {
      if ((this.BindingSource == null || this.BindingSource.Count == 0) && this.BindingSource == null)
        return;
      this.UpdateNavigationButtons();
      this.UpdateTextBox();
      this.UpdateLabelText();
      this.UpdateAddNewButtonVisibility();
      this.UpdateDeleteButtonVisibility();
    }

    protected virtual void UpdateNavigationButtons()
    {
      if (this.BindingSource.Position <= 0)
      {
        if (this.FirstButton != null)
          this.FirstButton.Enabled = false;
        if (this.PreviousButton != null)
          this.PreviousButton.Enabled = false;
      }
      else
      {
        if (this.FirstButton != null)
          this.FirstButton.Enabled = true;
        if (this.PreviousButton != null)
          this.PreviousButton.Enabled = true;
      }
      if (this.BindingSource.Position == this.BindingSource.Count - 1)
      {
        if (this.LastButton != null)
          this.LastButton.Enabled = false;
        if (this.NextButton == null)
          return;
        this.NextButton.Enabled = false;
      }
      else
      {
        if (this.LastButton != null)
          this.LastButton.Enabled = true;
        if (this.NextButton == null)
          return;
        this.NextButton.Enabled = true;
      }
    }

    protected virtual void UpdateTextBox()
    {
      if (this.CurrentNumberTextBox == null)
        return;
      this.CurrentNumberTextBox.Text = (this.BindingSource.Position + 1).ToString();
    }

    protected virtual void UpdateLabelText()
    {
      if (this.PageLabel == null)
        return;
      if (this.BindingSource != null)
        this.PageLabel.Text = string.Format(this.CountItemFormat, (object) this.BindingSource.Count);
      else
        this.PageLabel.Text = this.CountItemFormat;
    }

    protected virtual void UpdateAddNewButtonVisibility()
    {
      System.Type[] genericArguments = this.BindingSource.List.GetType().GetGenericArguments();
      if (genericArguments.Length > 0 && this.AddNewButton != null)
      {
        if ((object) genericArguments[0].GetConstructor(System.Type.EmptyTypes) == null)
          this.AddNewButton.Enabled = false;
        else
          this.AddNewButton.Enabled = true;
      }
      if (this.AddNewButton == null)
        return;
      if (this.BindingSource.AllowNew)
        this.AddNewButton.Enabled = true;
      else
        this.AddNewButton.Enabled = false;
    }

    protected virtual void UpdateDeleteButtonVisibility()
    {
      if (this.DeleteButton == null)
        return;
      if (this.BindingSource.Count == 0)
        this.DeleteButton.Enabled = false;
      else
        this.DeleteButton.Enabled = true;
    }
  }
}
