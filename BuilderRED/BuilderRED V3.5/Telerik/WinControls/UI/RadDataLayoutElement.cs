// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDataLayoutElement : LightVisualElement
  {
    public static RadProperty ErrorIconProperty = RadProperty.Register(nameof (ErrorIcon), typeof (Image), typeof (RadDataLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private int columnCount = 1;
    private FlowDirection flowDirection = FlowDirection.TopDown;
    private bool autoSizeLabels = true;
    private int defaultHeight = 26;
    private Dictionary<string, Control> editableProperties = new Dictionary<string, Control>();
    private Dictionary<string, Control> alreadyGeneratedProperties = new Dictionary<string, Control>();
    private Dictionary<Control, DataLayoutValidationInfo> validationInfoForEachEditor = new Dictionary<Control, DataLayoutValidationInfo>();
    private RadDataLayout dataEntryControl;
    private IDesignerHost designerHost;
    private IComponentChangeService changeService;
    private PropertyDescriptorCollection propertyDescriptorCollection;
    private Icon errorIconCache;
    private bool bindOnBindingContextChange;
    private object dataSource;
    private BindingManagerBase manager;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.DrawText = false;
      this.DrawFill = false;
      this.DrawBorder = true;
    }

    public int ItemDefaultHeight
    {
      get
      {
        return this.defaultHeight;
      }
      set
      {
        this.defaultHeight = value;
      }
    }

    public int ColumnCount
    {
      get
      {
        return this.columnCount;
      }
      set
      {
        if (value >= 1 && value != this.columnCount)
          this.columnCount = value;
        else if (value < 1)
          throw new ArgumentException("Number Of Columns should be at least one");
      }
    }

    public FlowDirection FlowDirection
    {
      get
      {
        return this.flowDirection;
      }
      set
      {
        this.flowDirection = value;
      }
    }

    public bool AutoSizeLabels
    {
      get
      {
        return this.autoSizeLabels;
      }
      set
      {
        this.autoSizeLabels = value;
      }
    }

    public object DataSource
    {
      get
      {
        return this.dataSource;
      }
      set
      {
        if (this.dataSource == value)
          return;
        if (value != null)
        {
          this.dataSource = value;
          if (this.DataLayoutControl.BindingContext != null)
            this.manager = this.DataLayoutControl.BindingContext[this.DataSource];
          if (this.DataLayoutControl.IsInitializing)
            return;
          if (this.DataLayoutControl.BindingContext != null)
            this.Bind();
          else
            this.bindOnBindingContextChange = true;
        }
        else
        {
          this.dataSource = value;
          this.Clear();
        }
      }
    }

    public object CurrentObject
    {
      get
      {
        if (this.manager.Position == -1)
          return (object) null;
        return this.manager.Current;
      }
    }

    public BindingManagerBase Manager
    {
      get
      {
        return this.manager;
      }
    }

    internal RadDataLayout DataLayoutControl
    {
      get
      {
        if (this.dataEntryControl == null)
          this.dataEntryControl = this.ElementTree.Control as RadDataLayout;
        return this.dataEntryControl;
      }
    }

    [Description("Gets or sets the icon of the Error provider.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image ErrorIcon
    {
      get
      {
        return (Image) this.GetValue(RadDataLayoutElement.ErrorIconProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDataLayoutElement.ErrorIconProperty, (object) value);
      }
    }

    public void Bind()
    {
      this.Clear();
      this.InitializeDataEntry();
      this.FindRequiredProperties();
      this.ArrangeControls();
      this.DataLayoutControl.LayoutControl.UpdateControlsLayout();
    }

    public void Clear()
    {
      if (this.designerHost != null)
      {
        foreach (IComponent component in new List<RadItem>((IEnumerable<RadItem>) this.DataLayoutControl.LayoutControl.Items))
          this.designerHost.DestroyComponent(component);
      }
      RadLayoutControl layoutControl = this.DataLayoutControl.LayoutControl;
      IList<LayoutControlItemBase> layoutControlItemBaseList = (IList<LayoutControlItemBase>) new List<LayoutControlItemBase>(layoutControl.GetAllItems());
      while (layoutControlItemBaseList.Count > 0)
      {
        DataLayoutControlItem layoutControlItem = layoutControlItemBaseList[0] as DataLayoutControlItem;
        layoutControl.RemoveItem(layoutControlItemBaseList[0]);
        layoutControlItemBaseList.RemoveAt(0);
        if (layoutControlItem != null && layoutControlItem.AssociatedControl != null)
        {
          layoutControlItem.AssociatedControl.Validating -= new CancelEventHandler(this.control_Validating);
          layoutControlItem.AssociatedControl.Validated -= new EventHandler(this.control_Validated);
          layoutControl.Controls.Remove(layoutControlItem.AssociatedControl);
        }
      }
      this.editableProperties.Clear();
      this.alreadyGeneratedProperties.Clear();
      this.validationInfoForEachEditor.Clear();
    }

    protected internal virtual void InitializeDataEntry()
    {
      if (this.DataLayoutControl.Site == null)
        return;
      this.designerHost = this.DataLayoutControl.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
      this.changeService = this.DataLayoutControl.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
    }

    protected virtual void FindRequiredProperties()
    {
      this.propertyDescriptorCollection = this.Manager.GetItemProperties();
      this.CreateEditors();
    }

    protected internal virtual void CreateEditors()
    {
      if (this.propertyDescriptorCollection == null)
        return;
      foreach (PropertyDescriptor propertyDescriptor in this.propertyDescriptorCollection)
      {
        if (propertyDescriptor.IsBrowsable && !(propertyDescriptor.GetType().Name == "DataRelationPropertyDescriptor"))
        {
          RadRangeAttribute radRangeAttribute = (RadRangeAttribute) null;
          foreach (Attribute attribute in propertyDescriptor.Attributes)
          {
            if (attribute is RadRangeAttribute)
              radRangeAttribute = attribute as RadRangeAttribute;
          }
          string str = !(propertyDescriptor.Name != propertyDescriptor.DisplayName) || propertyDescriptor.DisplayName.Length <= 0 ? propertyDescriptor.Name : propertyDescriptor.DisplayName;
          if (!this.IsPropertyAlreadyCreated(str, propertyDescriptor))
          {
            System.Type suggestedEditorType = this.GetSuggestedEditorType(propertyDescriptor.PropertyType);
            Control control = this.CreateControl(propertyDescriptor, suggestedEditorType);
            if (control != null)
            {
              control.Validating += new CancelEventHandler(this.control_Validating);
              control.Validated += new EventHandler(this.control_Validated);
              ErrorProvider errorProvider = new ErrorProvider();
              errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
              errorProvider.SetIconPadding(control, 2);
              errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
              DataLayoutValidationInfo layoutValidationInfo = new DataLayoutValidationInfo();
              layoutValidationInfo.RangeAttribute = radRangeAttribute;
              layoutValidationInfo.ErrorProvider = errorProvider;
              if (!this.validationInfoForEachEditor.ContainsKey(control))
                this.validationInfoForEachEditor.Add(control, layoutValidationInfo);
              if (!this.editableProperties.ContainsKey(str))
                this.editableProperties.Add(str, control);
            }
          }
        }
      }
    }

    public void SubscribeControl(DataLayoutControlItem item, PropertyDescriptor property)
    {
      if (item == null || item.AssociatedControl == null)
        return;
      RadRangeAttribute radRangeAttribute = (RadRangeAttribute) null;
      foreach (Attribute attribute in property.Attributes)
      {
        if (attribute is RadRangeAttribute)
          radRangeAttribute = attribute as RadRangeAttribute;
      }
      Control associatedControl = item.AssociatedControl;
      associatedControl.Validating -= new CancelEventHandler(this.control_Validating);
      associatedControl.Validated -= new EventHandler(this.control_Validated);
      associatedControl.Validating += new CancelEventHandler(this.control_Validating);
      associatedControl.Validated += new EventHandler(this.control_Validated);
      ErrorProvider errorProvider = new ErrorProvider();
      errorProvider.SetIconAlignment(associatedControl, ErrorIconAlignment.MiddleRight);
      errorProvider.SetIconPadding(associatedControl, 2);
      errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
      DataLayoutValidationInfo layoutValidationInfo = new DataLayoutValidationInfo();
      layoutValidationInfo.RangeAttribute = radRangeAttribute;
      layoutValidationInfo.ErrorProvider = errorProvider;
      layoutValidationInfo.Item = item;
      if (this.validationInfoForEachEditor.ContainsKey(associatedControl))
      {
        this.validationInfoForEachEditor[associatedControl].ErrorProvider.Dispose();
        this.validationInfoForEachEditor.Remove(associatedControl);
      }
      if (this.validationInfoForEachEditor.ContainsKey(associatedControl))
        return;
      this.validationInfoForEachEditor.Add(associatedControl, layoutValidationInfo);
    }

    protected internal virtual void ArrangeControls()
    {
      this.DataLayoutControl.LayoutControl.BeginInit();
      this.DataLayoutControl.LayoutControl.SuspendLayout();
      int currentColumn = this.flowDirection != FlowDirection.RightToLeft ? 1 : this.columnCount;
      int num;
      if (this.flowDirection == FlowDirection.BottomUp)
      {
        num = this.editableProperties.Count / this.columnCount;
        if (this.editableProperties.Count % this.columnCount != 0)
          ++num;
      }
      else
        num = 1;
      foreach (KeyValuePair<string, Control> editableProperty in this.editableProperties)
      {
        if (!this.alreadyGeneratedProperties.ContainsKey(editableProperty.Key) || this.alreadyGeneratedProperties[editableProperty.Key] != editableProperty.Value)
        {
          Size propertyItemControlSize = new Size(Math.Min((this.DataLayoutControl.Size.Width - 2) / this.columnCount, this.DataLayoutControl.Size.Width - 2), this.ItemDefaultHeight);
          int y = (num - 1) * propertyItemControlSize.Height;
          Point propertyItemControlLocation = new Point(this.RightToLeft ? propertyItemControlSize.Width * (this.columnCount - currentColumn) : propertyItemControlSize.Width * (currentColumn - 1), y);
          if (this.DataLayoutControl.Site == null)
            this.GenerateControlsRunTime(currentColumn, editableProperty, propertyItemControlSize, propertyItemControlLocation);
          else
            this.GenerateControlsDesignTime(currentColumn, editableProperty, propertyItemControlSize, propertyItemControlLocation);
          if (this.flowDirection == FlowDirection.TopDown)
          {
            if (this.editableProperties.Count % this.columnCount == 0)
            {
              if (num % (this.editableProperties.Count / this.columnCount) == 0)
              {
                ++currentColumn;
                num = 1;
              }
              else
                ++num;
            }
            else if (num % (this.editableProperties.Count / this.columnCount + 1) == 0)
            {
              ++currentColumn;
              num = 1;
            }
            else
              ++num;
          }
          else if (this.flowDirection == FlowDirection.LeftToRight)
          {
            if (currentColumn % this.columnCount == 0)
            {
              currentColumn = 1;
              ++num;
            }
            else
              ++currentColumn;
          }
          else if (this.flowDirection == FlowDirection.BottomUp)
          {
            if (this.editableProperties.Count % this.columnCount == 0)
            {
              if (num == 1)
              {
                ++currentColumn;
                num = this.editableProperties.Count / this.columnCount;
              }
              else
                --num;
            }
            else if (num == 1)
            {
              ++currentColumn;
              num = this.editableProperties.Count / this.columnCount + 1;
            }
            else
              --num;
          }
          else if (currentColumn == 1)
          {
            ++num;
            currentColumn = this.columnCount;
          }
          else
            --currentColumn;
        }
      }
      this.ArrangeLabels();
      this.DataLayoutControl.LayoutControl.EndInit();
      this.DataLayoutControl.LayoutControl.ResumeLayout(true);
    }

    protected virtual Control CreateControl(PropertyDescriptor property, System.Type editorType)
    {
      Control control;
      if (this.DataLayoutControl.Site == null)
      {
        Control instance = (Control) Activator.CreateInstance(editorType);
        this.SetupControlSpecificProperties(instance, property);
        instance.Name = editorType.Name + property.DisplayName;
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, instance, instance.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
      }
      else
      {
        Control component = this.designerHost.CreateComponent(editorType) as Control;
        this.SetupControlSpecificProperties(component, property);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
      }
      if (!control.IsHandleCreated)
      {
        control.Parent = this.DataLayoutControl.Parent;
        control.CreateControl();
      }
      this.SetupControlBinding(control, property);
      this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      return control;
    }

    private void SetupControlSpecificProperties(Control control, PropertyDescriptor property)
    {
      RadDropDownList radDropDownList = control as RadDropDownList;
      if (radDropDownList != null && property.PropertyType.IsEnum)
      {
        foreach (object obj in Enum.GetValues(property.PropertyType))
        {
          RadListDataItem radListDataItem = new RadListDataItem(obj.ToString(), obj);
          radDropDownList.Items.Add(radListDataItem);
        }
        radDropDownList.DropDownStyle = RadDropDownStyle.DropDownList;
      }
      RadSpinEditor radSpinEditor = control as RadSpinEditor;
      if (radSpinEditor == null)
        return;
      RadRangeAttribute radRangeAttribute = (RadRangeAttribute) null;
      foreach (Attribute attribute in property.Attributes)
      {
        if (attribute is RadRangeAttribute)
          radRangeAttribute = attribute as RadRangeAttribute;
      }
      if (radRangeAttribute != null)
      {
        radSpinEditor.Minimum = (Decimal) radRangeAttribute.MinValue;
        radSpinEditor.Maximum = (Decimal) radRangeAttribute.MaxValue;
      }
      else
      {
        radSpinEditor.Minimum = new Decimal(int.MinValue);
        radSpinEditor.Maximum = new Decimal(int.MaxValue);
      }
    }

    private void SetupControlBinding(Control control, PropertyDescriptor property)
    {
      if (control is RadDropDownList)
      {
        Binding binding1 = this.CreateBinding(control, "SelectedValue", property.Name);
        if (binding1 == null)
          return;
        if (property.PropertyType.IsEnum)
        {
          binding1.Parse += (ConvertEventHandler) ((sender, e) =>
          {
            Binding binding = sender as Binding;
            if ((object) e.DesiredType != (object) binding.BindingManagerBase.GetItemProperties()[binding.BindingMemberInfo.BindingField].PropertyType)
              return;
            RadDropDownList control1 = binding.Control as RadDropDownList;
            int int32 = Convert.ToInt32(e.Value);
            e.Value = control1.Items[int32].Value;
          });
          binding1.Format += (ConvertEventHandler) ((sender, e) =>
          {
            if ((object) e.DesiredType != (object) typeof (int))
              return;
            Binding binding = sender as Binding;
            Convert.ToInt32(e.Value);
            foreach (RadListDataItem radListDataItem in (binding.Control as RadDropDownList).Items)
            {
              if (radListDataItem.Value.Equals(Enum.Parse(binding.BindingManagerBase.GetItemProperties()[binding.BindingMemberInfo.BindingField].PropertyType, Convert.ToString(e.Value))))
              {
                e.Value = (object) radListDataItem.Index;
                break;
              }
            }
          });
        }
        control.DataBindings.Add(binding1);
      }
      else if (control is RadTextBox)
      {
        Binding binding = this.CreateBinding(control, "Text", property.Name);
        if (binding == null)
          return;
        control.DataBindings.Add(binding);
      }
      else if (control is PictureBox)
      {
        Binding binding = this.CreateBinding(control, "Image", property.Name);
        if (binding == null)
          return;
        control.DataBindings.Add(binding);
      }
      else if (control is RadCheckBox)
      {
        Binding binding = this.CreateBinding(control, "Checked", property.Name);
        if (binding == null)
          return;
        control.DataBindings.Add(binding);
      }
      else
      {
        if (!(control is RadDateTimePicker) && !(control is RadColorBox) && !(control is RadSpinEditor))
          return;
        Binding binding = this.CreateBinding(control, "Value", property.Name);
        if (binding == null)
          return;
        control.DataBindings.Add(binding);
      }
    }

    protected virtual void GenerateControlsRunTime(
      int currentColumn,
      KeyValuePair<string, Control> pair,
      Size propertyItemControlSize,
      Point propertyItemControlLocation)
    {
      DataLayoutControlItem layoutControlItem = new DataLayoutControlItem();
      layoutControlItem.Text = pair.Key;
      Control index = pair.Value;
      if (index is RadControl)
        (index as RadControl).ThemeName = this.DataLayoutControl.ThemeName;
      layoutControlItem.AssociatedControl = index;
      if (layoutControlItem.MinSize.Height != propertyItemControlSize.Height)
        layoutControlItem.MinSize = new Size(layoutControlItem.MinSize.Width, propertyItemControlSize.Height);
      if (layoutControlItem.MinSize.Width > propertyItemControlSize.Width)
        layoutControlItem.MinSize = new Size(propertyItemControlSize.Width, layoutControlItem.MinSize.Height);
      layoutControlItem.Bounds = new Rectangle(propertyItemControlLocation, propertyItemControlSize);
      DataLayoutItemInitializingEventArgs e = new DataLayoutItemInitializingEventArgs((LayoutControlItem) layoutControlItem);
      this.OnItemInitializing((object) this, e);
      if (e.Cancel)
        return;
      this.DataLayoutControl.LayoutControl.Items.Add((RadItem) layoutControlItem);
      this.OnItemInitialized((object) this, new DataLayoutItemInitializedEventArgs((LayoutControlItem) layoutControlItem));
      this.alreadyGeneratedProperties.Add(pair.Key, index);
      this.validationInfoForEachEditor[index].Item = layoutControlItem;
    }

    protected virtual void GenerateControlsDesignTime(
      int currentColumn,
      KeyValuePair<string, Control> pair,
      Size propertyItemControlSize,
      Point propertyItemControlLocation)
    {
      DataLayoutControlItem component = (DataLayoutControlItem) this.designerHost.CreateComponent(typeof (DataLayoutControlItem));
      this.changeService.OnComponentChanging((object) component, (MemberDescriptor) null);
      component.Text = pair.Key;
      if (component.MinSize.Height != propertyItemControlSize.Height)
        component.MinSize = new Size(component.MinSize.Width, propertyItemControlSize.Height);
      this.changeService.OnComponentChanged((object) component, (MemberDescriptor) null, (object) null, (object) null);
      this.changeService.OnComponentChanging((object) this.DataLayoutControl.Site.Component, (MemberDescriptor) null);
      DataLayoutItemInitializingEventArgs e = new DataLayoutItemInitializingEventArgs((LayoutControlItem) component);
      this.OnItemInitializing((object) this, e);
      if (e.Cancel)
        return;
      Control index = pair.Value;
      component.AssociatedControl = index;
      component.Bounds = new Rectangle(propertyItemControlLocation, propertyItemControlSize);
      this.DataLayoutControl.LayoutControl.Items.Add((RadItem) component);
      this.OnItemInitialized((object) this, new DataLayoutItemInitializedEventArgs((LayoutControlItem) component));
      this.changeService.OnComponentChanged((object) this.DataLayoutControl.LayoutControl.Site.Component, (MemberDescriptor) null, (object) null, (object) null);
      this.alreadyGeneratedProperties.Add(pair.Key, pair.Value);
      this.validationInfoForEachEditor[index].Item = component;
    }

    protected virtual Binding CreateBinding(
      Control control,
      string propertyName,
      string dataMember)
    {
      Binding binding1 = (Binding) null;
      object obj = !(this.Manager is CurrencyManager) ? this.manager.Current : (object) (this.Manager as CurrencyManager).List;
      BindingCreatingEventArgs e = new BindingCreatingEventArgs(control, propertyName, obj, dataMember);
      this.OnBindingCreating((object) this, e);
      if (e.Cancel)
        return binding1;
      Binding binding2 = new Binding(e.PropertyName, obj, e.DataMember, e.FormattingEnabled, DataSourceUpdateMode.OnPropertyChanged);
      this.OnBindingCreated((object) this, new BindingCreatedEventArgs(control, e.PropertyName, obj, e.DataMember, binding2));
      return binding2;
    }

    protected virtual void ArrangeLabels()
    {
      if (!this.AutoSizeLabels)
        return;
      Dictionary<int, List<DataLayoutControlItem>> dictionary = new Dictionary<int, List<DataLayoutControlItem>>();
      foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) this.DataLayoutControl.LayoutControl.Items)
      {
        DataLayoutControlItem layoutControlItem = layoutControlItemBase as DataLayoutControlItem;
        if (layoutControlItem != null)
        {
          if (!dictionary.ContainsKey(layoutControlItem.Bounds.X))
            dictionary.Add(layoutControlItem.Bounds.X, new List<DataLayoutControlItem>());
          dictionary[layoutControlItem.Bounds.X].Add(layoutControlItem);
        }
      }
      foreach (KeyValuePair<int, List<DataLayoutControlItem>> keyValuePair in dictionary)
      {
        float num = 0.0f;
        MeasurementGraphics measurementGraphics = MeasurementGraphics.CreateMeasurementGraphics();
        foreach (DataLayoutControlItem layoutControlItem in keyValuePair.Value)
        {
          SizeF sizeF = measurementGraphics.Graphics.MeasureString(layoutControlItem.Text, layoutControlItem.Font);
          if ((double) sizeF.Width > (double) num)
            num = (float) ((double) sizeF.Width + (double) layoutControlItem.Padding.Horizontal + 2.0);
        }
        foreach (DataLayoutControlItem layoutControlItem in keyValuePair.Value)
        {
          if (this.changeService != null)
            this.changeService.OnComponentChanging((object) layoutControlItem, (MemberDescriptor) null);
          layoutControlItem.TextSizeMode = LayoutItemTextSizeMode.Fixed;
          layoutControlItem.TextFixedSize = (int) num;
          if (this.changeService != null)
            this.changeService.OnComponentChanged((object) layoutControlItem, (MemberDescriptor) null, (object) null, (object) null);
        }
      }
    }

    private Icon ConvertImageToIcon(Image image)
    {
      return Icon.FromHandle(new Bitmap(image).GetHicon());
    }

    private bool IsPropertyAlreadyCreated(string labeltext, PropertyDescriptor property)
    {
      foreach (Control control in (ArrangedElementCollection) this.DataLayoutControl.LayoutControl.Controls)
      {
        if (control is RadPanel && (control as RadPanel).Controls[1] is RadLabel && ((control as RadPanel).Controls[1].Text == labeltext && (object) this.GetSuggestedEditorType(property.PropertyType) == (object) (control as RadPanel).Controls[0].GetType()))
          return true;
      }
      return false;
    }

    protected virtual System.Type GetSuggestedEditorType(System.Type propertyType)
    {
      System.Type type1 = typeof (RadTextBox);
      if (propertyType.IsEnum)
        return typeof (RadDropDownList);
      System.Type type2;
      switch (propertyType.Name)
      {
        case "DateTime":
          type2 = typeof (RadDateTimePicker);
          break;
        case "Boolean":
          type2 = typeof (RadCheckBox);
          break;
        case "Color":
          type2 = typeof (RadColorBox);
          break;
        case "Int32":
        case "Int16":
        case "Int64":
        case "Single":
        case "Double":
        case "Decimal":
          type2 = typeof (RadSpinEditor);
          break;
        case "Image":
        case "Bitmap":
          type2 = typeof (PictureBox);
          break;
        default:
          type2 = typeof (RadTextBox);
          break;
      }
      return type2;
    }

    public event DataLayoutItemValidatedEventHandler ItemValidated;

    public event DataLayoutItemValidatingEventHandler ItemValidating;

    public event EditorInitializingEventHandler EditorInitializing;

    public event EditorInitializedEventHandler EditorInitialized;

    public event DataLayoutItemInitializingEventHandler ItemInitializing;

    public event DataLayoutItemInitializedEventHandler ItemInitialized;

    public event BindingCreatingEventHandler BindingCreating;

    public event BindingCreatedEventHandler BindingCreated;

    private void control_Validated(object sender, EventArgs e)
    {
      Control index = sender as Control;
      if (index == null || this.CurrentObject == null)
        return;
      DataLayoutValidationInfo layoutValidationInfo = this.validationInfoForEachEditor[index];
      if (this.ErrorIcon != null)
        layoutValidationInfo.ErrorProvider.Icon = this.GetErrorIcon();
      this.OnItemValidated((object) index, new DataLayoutItemValidatedEventArgs(layoutValidationInfo.Item, layoutValidationInfo.ErrorProvider, layoutValidationInfo.RangeAttribute));
    }

    private void control_Validating(object sender, CancelEventArgs e)
    {
      Control index = sender as Control;
      if (index == null || this.CurrentObject == null)
        return;
      DataLayoutValidationInfo layoutValidationInfo = this.validationInfoForEachEditor[index];
      if (this.ErrorIcon != null)
        layoutValidationInfo.ErrorProvider.Icon = this.GetErrorIcon();
      DataLayoutItemValidatingEventArgs e1 = new DataLayoutItemValidatingEventArgs(layoutValidationInfo.Item, layoutValidationInfo.ErrorProvider, layoutValidationInfo.RangeAttribute);
      this.OnItemValidating((object) index, e1);
      e.Cancel = e1.Cancel;
    }

    private Icon GetErrorIcon()
    {
      if (this.errorIconCache == null)
        this.errorIconCache = this.ConvertImageToIcon(this.ErrorIcon);
      return this.errorIconCache;
    }

    protected internal virtual void OnItemValidated(
      object sender,
      DataLayoutItemValidatedEventArgs e)
    {
      DataLayoutItemValidatedEventHandler itemValidated = this.ItemValidated;
      if (itemValidated == null)
        return;
      itemValidated(sender, e);
    }

    protected internal virtual void OnItemValidating(
      object sender,
      DataLayoutItemValidatingEventArgs e)
    {
      DataLayoutItemValidatingEventHandler itemValidating = this.ItemValidating;
      if (itemValidating == null)
        return;
      itemValidating(sender, e);
    }

    protected internal virtual void OnEditorInitializing(
      object sender,
      EditorInitializingEventArgs e)
    {
      EditorInitializingEventHandler editorInitializing = this.EditorInitializing;
      if (editorInitializing == null)
        return;
      editorInitializing((object) this, e);
    }

    protected internal virtual void OnEditorInitialized(object sender, EditorInitializedEventArgs e)
    {
      EditorInitializedEventHandler editorInitialized = this.EditorInitialized;
      if (editorInitialized == null)
        return;
      editorInitialized((object) this, e);
    }

    protected internal virtual void OnItemInitializing(
      object sender,
      DataLayoutItemInitializingEventArgs e)
    {
      DataLayoutItemInitializingEventHandler itemInitializing = this.ItemInitializing;
      if (itemInitializing == null)
        return;
      itemInitializing((object) this, e);
    }

    protected internal virtual void OnItemInitialized(
      object sender,
      DataLayoutItemInitializedEventArgs e)
    {
      DataLayoutItemInitializedEventHandler itemInitialized = this.ItemInitialized;
      if (itemInitialized == null)
        return;
      itemInitialized((object) this, e);
    }

    protected internal virtual void OnBindingCreating(object sender, BindingCreatingEventArgs e)
    {
      BindingCreatingEventHandler bindingCreating = this.BindingCreating;
      if (bindingCreating == null)
        return;
      bindingCreating((object) this, e);
    }

    protected internal virtual void OnBindingCreated(object sender, BindingCreatedEventArgs e)
    {
      BindingCreatedEventHandler bindingCreated = this.BindingCreated;
      if (bindingCreated == null)
        return;
      bindingCreated((object) this, e);
    }

    protected internal virtual void OnBindingContextChanged(EventArgs e)
    {
      if (this.DataLayoutControl.BindingContext == null)
        return;
      if (this.DataSource != null)
        this.manager = this.DataLayoutControl.BindingContext[this.DataSource];
      if (!this.bindOnBindingContextChange)
        return;
      this.bindOnBindingContextChange = false;
      this.Bind();
    }
  }
}
