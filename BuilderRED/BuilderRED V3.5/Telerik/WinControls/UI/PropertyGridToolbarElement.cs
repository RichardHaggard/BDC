// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridToolbarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridToolbarElement : StackLayoutElement
  {
    public static RadProperty ToolbarElementHeightProperty = RadProperty.Register(nameof (ToolbarElementHeight), typeof (float), typeof (PropertyGridToolbarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 25f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private ToolbarTextBoxElement searchTextBoxElement;
    private FilterDescriptor searchCriteria;
    private AlphabeticalToggleButton alphabeticalToggleButton;
    private CategorizedAlphabeticalToggleButton categorizedToggleButton;

    public PropertyGridElement PropertyGridElement
    {
      get
      {
        return this.FindAncestor<PropertyGridElement>();
      }
    }

    public CategorizedAlphabeticalToggleButton CategorizedToggleButton
    {
      get
      {
        return this.categorizedToggleButton;
      }
    }

    public AlphabeticalToggleButton AlphabeticalToggleButton
    {
      get
      {
        return this.alphabeticalToggleButton;
      }
    }

    public ToolbarTextBoxElement SearchTextBoxElement
    {
      get
      {
        return this.searchTextBoxElement;
      }
    }

    [Description("Gets or sets the property name by which the search will be performed.")]
    [Category("Data")]
    [Browsable(true)]
    [DefaultValue("Label")]
    public string FilterPropertyName
    {
      get
      {
        return this.searchCriteria.PropertyName;
      }
      set
      {
        this.searchCriteria.PropertyName = value;
        this.ExecuteSearch();
      }
    }

    [Description("Gets or sets the filter operator which will be used for the search.")]
    [DefaultValue(FilterOperator.Contains)]
    [Category("Data")]
    [Browsable(true)]
    public FilterOperator FilterOperator
    {
      get
      {
        return this.searchCriteria.Operator;
      }
      set
      {
        this.searchCriteria.Operator = value;
        this.ExecuteSearch();
      }
    }

    [Category("Data")]
    [Description("Gets or sets the value by which the search will be performed.")]
    [DefaultValue("")]
    [Browsable(true)]
    public object FilterValue
    {
      get
      {
        return this.searchCriteria.Value;
      }
      set
      {
        this.searchCriteria.Value = value;
        this.ExecuteSearch();
      }
    }

    public float ToolbarElementHeight
    {
      get
      {
        return (float) Math.Round((double) (float) this.GetValue(PropertyGridToolbarElement.ToolbarElementHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridToolbarElement.ToolbarElementHeightProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.Orientation = Orientation.Horizontal;
      this.FitInAvailableSize = true;
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.searchTextBoxElement = new ToolbarTextBoxElement();
      this.alphabeticalToggleButton = new AlphabeticalToggleButton();
      this.categorizedToggleButton = new CategorizedAlphabeticalToggleButton();
      this.alphabeticalToggleButton.MaxSize = new Size(25, 0);
      this.categorizedToggleButton.MaxSize = new Size(25, 0);
      this.alphabeticalToggleButton.MinSize = new Size(25, 0);
      this.categorizedToggleButton.MinSize = new Size(25, 0);
      this.searchTextBoxElement.StretchHorizontally = true;
      this.searchTextBoxElement.StretchVertically = true;
      this.alphabeticalToggleButton.StretchHorizontally = false;
      this.categorizedToggleButton.StretchHorizontally = false;
      this.alphabeticalToggleButton.Text = string.Empty;
      this.categorizedToggleButton.Text = string.Empty;
      this.alphabeticalToggleButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.categorizedToggleButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.alphabeticalToggleButton);
      this.Children.Add((RadElement) this.categorizedToggleButton);
      this.Children.Add((RadElement) this.searchTextBoxElement);
      this.searchCriteria = new FilterDescriptor("Label", FilterOperator.Contains, (object) "");
      this.WireEvents();
    }

    protected virtual void ExecuteSearch()
    {
      this.PropertyGridElement.PropertyTableElement.FilterDescriptors.Clear();
      this.PropertyGridElement.PropertyTableElement.FilterDescriptors.Add(this.searchCriteria);
    }

    protected virtual void WireEvents()
    {
      this.searchTextBoxElement.TextChanged += new EventHandler(this.searchTextBoxElement_TextChanged);
      this.searchTextBoxElement.TextBoxItem.GotFocus += new EventHandler(this.searchTextBoxElement_GotFocus);
      this.alphabeticalToggleButton.ToggleStateChanged += new StateChangedEventHandler(this.alphabeticalToggleButton_ToggleStateChanged);
      this.categorizedToggleButton.ToggleStateChanged += new StateChangedEventHandler(this.categorizedToggleButton_ToggleStateChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.searchTextBoxElement.TextChanged -= new EventHandler(this.searchTextBoxElement_TextChanged);
      this.searchTextBoxElement.TextBoxItem.GotFocus -= new EventHandler(this.searchTextBoxElement_GotFocus);
      this.alphabeticalToggleButton.ToggleStateChanged -= new StateChangedEventHandler(this.alphabeticalToggleButton_ToggleStateChanged);
      this.categorizedToggleButton.ToggleStateChanged -= new StateChangedEventHandler(this.categorizedToggleButton_ToggleStateChanged);
    }

    private void searchTextBoxElement_GotFocus(object sender, EventArgs e)
    {
      if (!this.PropertyGridElement.PropertyTableElement.IsEditing)
        return;
      this.PropertyGridElement.PropertyTableElement.EndEdit();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
    }

    public virtual void SyncronizeToggleButtons()
    {
      if (this.PropertyGridElement == null)
        return;
      PropertySort propertySort = this.PropertyGridElement.PropertyTableElement.PropertySort;
      this.UnwireEvents();
      switch (propertySort)
      {
        case PropertySort.NoSort:
          this.alphabeticalToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
        case PropertySort.Alphabetical:
          this.alphabeticalToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
        case PropertySort.Categorized:
          this.alphabeticalToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
          this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          break;
        case PropertySort.CategorizedAlphabetical:
          this.alphabeticalToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
          break;
      }
      if (this.PropertyGridElement.PropertyTableElement.GroupDescriptors.Count > 0)
        this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      else
        this.categorizedToggleButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.WireEvents();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != PropertyGridToolbarElement.ToolbarElementHeightProperty)
        return;
      this.InvalidateMeasure(true);
      this.PropertyGridElement.PropertyTableElement.UseCachedValues = true;
      this.PropertyGridElement.PropertyTableElement.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
      this.PropertyGridElement.PropertyTableElement.UseCachedValues = false;
    }

    private void searchTextBoxElement_TextChanged(object sender, EventArgs e)
    {
      this.searchCriteria.Value = (object) this.searchTextBoxElement.Text;
      this.ExecuteSearch();
      this.PropertyGridElement.PropertyTableElement.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
    }

    private void categorizedToggleButton_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.OnToggleButtonToggleStateChanged(sender as RadToggleButtonElement, args);
    }

    private void alphabeticalToggleButton_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.OnToggleButtonToggleStateChanged(sender as RadToggleButtonElement, args);
    }

    protected virtual void OnToggleButtonToggleStateChanged(
      RadToggleButtonElement button,
      StateChangedEventArgs args)
    {
      this.PropertyGridElement.PropertyTableElement.EndEdit();
      if (this.alphabeticalToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On && this.categorizedToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        this.PropertyGridElement.PropertyTableElement.PropertySort = PropertySort.CategorizedAlphabetical;
      else if (this.alphabeticalToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On && this.categorizedToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
        this.PropertyGridElement.PropertyTableElement.PropertySort = PropertySort.Alphabetical;
      else if (this.alphabeticalToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off && this.categorizedToggleButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        this.PropertyGridElement.PropertyTableElement.PropertySort = PropertySort.Categorized;
      else
        this.PropertyGridElement.PropertyTableElement.PropertySort = PropertySort.NoSort;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize.Height = Math.Min(availableSize.Height, this.ToolbarElementHeight);
      return base.MeasureOverride(availableSize);
    }
  }
}
