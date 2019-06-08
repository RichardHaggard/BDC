// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataEntryElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDataEntryElement : RadScrollablePanelElement
  {
    public static RadProperty ErrorIconProperty = RadProperty.Register(nameof (ErrorIcon), typeof (Image), typeof (RadDataEntryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private int columnCount = 1;
    private FlowDirection flowDirection = FlowDirection.TopDown;
    private int itemSpace = 5;
    private Size defaultSize = new Size(200, 22);
    private Dictionary<string, Control> editableProperties = new Dictionary<string, Control>();
    private Dictionary<string, Control> allReadyGeneratedProperties = new Dictionary<string, Control>();
    private Dictionary<int, List<RadLabel>> controlsInEachColumn = new Dictionary<int, List<RadLabel>>();
    private Dictionary<Control, ValidationInfo> validationInfoForEachEditor = new Dictionary<Control, ValidationInfo>();
    private RadDataEntry dataEntryControl;
    private IDesignerHost designerHost;
    private IComponentChangeService changeService;
    private PropertyDescriptorCollection propertyDescriptorCollection;
    private Icon errorIconCache;
    private bool autoSizeLabels;
    private bool fitToParentWidth;
    private bool bindOnBindingContextChange;
    private object dataSource;
    private BindingManagerBase manager;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
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

    public int ItemSpace
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.itemSpace, this.DpiScaleFactor);
      }
      set
      {
        this.itemSpace = value;
      }
    }

    public bool FitToParentWidth
    {
      get
      {
        return this.fitToParentWidth;
      }
      set
      {
        this.fitToParentWidth = value;
      }
    }

    public Size ItemDefaultSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.defaultSize, this.DpiScaleFactor);
      }
      set
      {
        this.defaultSize = value;
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
          if (this.DataEntryControl.BindingContext != null)
            this.manager = this.DataEntryControl.BindingContext[this.DataSource];
          if (this.DataEntryControl.IsInitializing)
            return;
          if (this.DataEntryControl.BindingContext != null)
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

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadScrollablePanelElement);
      }
    }

    internal RadDataEntry DataEntryControl
    {
      get
      {
        if (this.dataEntryControl == null)
          this.dataEntryControl = this.ElementTree.Control as RadDataEntry;
        return this.dataEntryControl;
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the icon of the Error provider.")]
    public Image ErrorIcon
    {
      get
      {
        return (Image) this.GetValue(RadDataEntryElement.ErrorIconProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDataEntryElement.ErrorIconProperty, (object) value);
      }
    }

    public void Bind()
    {
      this.Clear();
      this.InitializeDataEntry();
      this.FindRequiredProperties();
      this.ArrangeControls();
    }

    public void Clear()
    {
      if (this.designerHost != null)
      {
        foreach (IComponent component in new ArrayList((ICollection) this.DataEntryControl.Controls))
          this.designerHost.DestroyComponent(component);
      }
      this.DataEntryControl.PanelContainer.Controls.Clear();
      this.editableProperties.Clear();
      this.allReadyGeneratedProperties.Clear();
      this.controlsInEachColumn.Clear();
      this.validationInfoForEachEditor.Clear();
    }

    protected internal virtual void InitializeDataEntry()
    {
      if (this.DataEntryControl.Site == null)
        return;
      this.designerHost = this.DataEntryControl.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
      this.changeService = this.DataEntryControl.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
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
          string str = !(propertyDescriptor.Name != propertyDescriptor.DisplayName) || propertyDescriptor.DisplayName.Length <= 0 ? propertyDescriptor.Name : propertyDescriptor.DisplayName;
          if (!this.IsThisPropertyAlreadyCreated(str, propertyDescriptor))
          {
            Control control;
            if (propertyDescriptor.PropertyType.IsEnum)
            {
              control = this.CreateEnum(propertyDescriptor);
            }
            else
            {
              switch (propertyDescriptor.PropertyType.Name)
              {
                case "DateTime":
                  control = this.CreateDateTime(propertyDescriptor);
                  break;
                case "Boolean":
                  control = this.CreateBoolean(propertyDescriptor);
                  break;
                case "Color":
                  control = this.CreateColor(propertyDescriptor);
                  break;
                case "Image":
                  control = this.CreateImage(propertyDescriptor);
                  break;
                default:
                  control = this.CreateTextBox(propertyDescriptor);
                  break;
              }
            }
            if (control != null)
            {
              control.Dock = DockStyle.Fill;
              control.Validating += new CancelEventHandler(this.control_Validating);
              control.Validated += new EventHandler(this.control_Validated);
              ErrorProvider errorProvider = new ErrorProvider();
              errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
              errorProvider.SetIconPadding(control, 2);
              errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
              RadRangeAttribute radRangeAttribute = (RadRangeAttribute) null;
              foreach (Attribute attribute in propertyDescriptor.Attributes)
              {
                if (attribute is RadRangeAttribute)
                  radRangeAttribute = attribute as RadRangeAttribute;
              }
              ValidationInfo validationInfo = new ValidationInfo();
              validationInfo.RangeAttribute = radRangeAttribute;
              validationInfo.ErrorProvider = errorProvider;
              if (!this.validationInfoForEachEditor.ContainsKey(control))
                this.validationInfoForEachEditor.Add(control, validationInfo);
              if (!this.editableProperties.ContainsKey(str))
                this.editableProperties.Add(str, control);
            }
          }
        }
      }
    }

    public void SubscribeControl(
      Control control,
      PropertyDescriptor property,
      RadLabel label,
      RadLabel validationLabel)
    {
      RadRangeAttribute radRangeAttribute = (RadRangeAttribute) null;
      foreach (Attribute attribute in property.Attributes)
      {
        if (attribute is RadRangeAttribute)
          radRangeAttribute = attribute as RadRangeAttribute;
      }
      control.Validating -= new CancelEventHandler(this.control_Validating);
      control.Validated -= new EventHandler(this.control_Validated);
      control.Validating += new CancelEventHandler(this.control_Validating);
      control.Validated += new EventHandler(this.control_Validated);
      ErrorProvider errorProvider = new ErrorProvider();
      errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
      errorProvider.SetIconPadding(control, 2);
      errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
      ValidationInfo validationInfo = new ValidationInfo();
      validationInfo.RangeAttribute = radRangeAttribute;
      validationInfo.ErrorProvider = errorProvider;
      validationInfo.Label = label;
      validationInfo.ValidationLabel = validationLabel;
      if (this.validationInfoForEachEditor.ContainsKey(control))
      {
        this.validationInfoForEachEditor[control].ErrorProvider.Dispose();
        this.validationInfoForEachEditor.Remove(control);
      }
      if (this.validationInfoForEachEditor.ContainsKey(control))
        return;
      this.validationInfoForEachEditor.Add(control, validationInfo);
    }

    protected internal virtual void ArrangeControls()
    {
      int currentColumn = 1;
      if (this.flowDirection == FlowDirection.RightToLeft)
        currentColumn = this.columnCount;
      int num = 1;
      if (this.flowDirection == FlowDirection.BottomUp)
      {
        num = this.editableProperties.Count / this.columnCount;
        if (this.editableProperties.Count % this.columnCount != 0)
          ++num;
      }
      foreach (KeyValuePair<string, Control> editableProperty in this.editableProperties)
      {
        if (!this.allReadyGeneratedProperties.ContainsKey(editableProperty.Key) || this.allReadyGeneratedProperties[editableProperty.Key] != editableProperty.Value)
        {
          Size propertyItemControlSize = this.ItemDefaultSize;
          if (this.FitToParentWidth)
            propertyItemControlSize = new Size(Math.Min((this.DataEntryControl.Size.Width - 2 * this.itemSpace) / this.columnCount, this.DataEntryControl.Size.Width - 2 * this.itemSpace), this.ItemDefaultSize.Height);
          int y = this.itemSpace + (num - 1) * (propertyItemControlSize.Height + this.itemSpace);
          Point propertyItemControlLocation = new Point(this.RightToLeft ? propertyItemControlSize.Width * (this.columnCount - currentColumn) + this.itemSpace : propertyItemControlSize.Width * (currentColumn - 1) + this.itemSpace, y);
          if (this.DataEntryControl.Site == null)
          {
            if (!this.GenerateControlsRunTime(currentColumn, editableProperty, propertyItemControlSize, propertyItemControlLocation))
              continue;
          }
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
    }

    protected virtual Control CreateEnum(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        RadDropDownList radDropDownList = new RadDropDownList();
        this.InitializeDropDownEnum((Control) radDropDownList, property);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) radDropDownList, radDropDownList.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (RadDropDownList)) as RadDropDownList);
        this.InitializeDropDownEnum(component, property);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
      }
      if (control is RadDropDownList)
      {
        Binding binding = this.CreateBinding(control, "SelectedValue", property.Name);
        if (binding != null)
          control.DataBindings.Add(binding);
      }
      this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      return control;
    }

    private void InitializeDropDownEnum(Control control, PropertyDescriptor property)
    {
      RadDropDownList radDropDownList = control as RadDropDownList;
      if (radDropDownList == null)
        return;
      foreach (object obj in Enum.GetValues(property.PropertyType))
      {
        RadListDataItem radListDataItem = new RadListDataItem(obj.ToString(), obj);
        radDropDownList.Items.Add(radListDataItem);
      }
      radDropDownList.DropDownStyle = RadDropDownStyle.DropDownList;
    }

    protected virtual Control CreateTextBox(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        RadTextBox radTextBox = new RadTextBox();
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) radTextBox, radTextBox.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, (Control) radTextBox, radTextBox.GetType()));
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (RadTextBox)) as RadTextBox);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      }
      if (!control.IsHandleCreated)
      {
        control.Parent = this.DataEntryControl.Parent;
        control.CreateControl();
      }
      Binding binding = this.CreateBinding(control, "Text", property.Name);
      if (binding != null)
        control.DataBindings.Add(binding);
      return control;
    }

    protected virtual Control CreateImage(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        PictureBox pictureBox = new PictureBox();
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) pictureBox, pictureBox.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, (Control) pictureBox, pictureBox.GetType()));
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (PictureBox)) as PictureBox);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      }
      Binding binding = this.CreateBinding(control, "Image", property.Name);
      if (binding != null)
        control.DataBindings.Add(binding);
      return control;
    }

    protected virtual Control CreateColor(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        RadColorBox radColorBox = new RadColorBox();
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) radColorBox, radColorBox.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, (Control) radColorBox, radColorBox.GetType()));
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (RadColorBox)) as RadColorBox);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      }
      Binding binding = this.CreateBinding(control, "Value", property.Name);
      if (binding != null)
        control.DataBindings.Add(binding);
      return control;
    }

    protected virtual Control CreateBoolean(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        RadCheckBox radCheckBox = new RadCheckBox();
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) radCheckBox, radCheckBox.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, (Control) radCheckBox, radCheckBox.GetType()));
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (RadCheckBox)) as RadCheckBox);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      }
      Binding binding = this.CreateBinding(control, "Checked", property.Name);
      if (binding != null)
        control.DataBindings.Add(binding);
      return control;
    }

    protected virtual Control CreateDateTime(PropertyDescriptor property)
    {
      Control control;
      if (this.DataEntryControl.Site == null)
      {
        RadDateTimePicker radDateTimePicker = new RadDateTimePicker();
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, (Control) radDateTimePicker, radDateTimePicker.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, (Control) radDateTimePicker, radDateTimePicker.GetType()));
      }
      else
      {
        Control component = (Control) (this.designerHost.CreateComponent(typeof (RadDateTimePicker)) as RadDateTimePicker);
        EditorInitializingEventArgs e = new EditorInitializingEventArgs(property, component, component.GetType());
        this.OnEditorInitializing((object) this, e);
        if (e.Cancel)
          return (Control) null;
        control = !object.ReferenceEquals((object) e.Editor, (object) component) ? this.designerHost.CreateComponent(e.EditorType) as Control : e.Editor;
        this.OnEditorInitialized((object) this, new EditorInitializedEventArgs(property, control, control.GetType()));
      }
      Binding binding = this.CreateBinding(control, "Value", property.Name);
      if (binding != null)
        control.DataBindings.Add(binding);
      return control;
    }

    protected virtual bool GenerateControlsRunTime(
      int currentColumn,
      KeyValuePair<string, Control> pair,
      Size propertyItemControlSize,
      Point propertyItemControlLocation)
    {
      RadPanel radPanel = new RadPanel();
      RadLabel labelControl = new RadLabel();
      RadLabel validationControl = new RadLabel();
      Control editorControl = pair.Value;
      string str = this.DataEntryControl.ThemeName;
      if (string.IsNullOrEmpty(str))
        str = ThemeResolutionService.ApplicationThemeName;
      radPanel.ThemeName = str;
      labelControl.ThemeName = str;
      validationControl.ThemeName = str;
      if (editorControl is RadControl)
        (editorControl as RadControl).ThemeName = str;
      this.SetupInnerControls(pair, propertyItemControlSize, propertyItemControlLocation, radPanel, labelControl, validationControl, editorControl);
      radPanel.Controls.Add(editorControl);
      radPanel.Controls.Add((Control) labelControl);
      radPanel.Controls.Add((Control) validationControl);
      ItemInitializingEventArgs e = new ItemInitializingEventArgs(radPanel);
      this.OnItemInitializing((object) this, e);
      if (e.Cancel)
        return false;
      this.DataEntryControl.PanelContainer.Controls.Add((Control) radPanel);
      this.OnItemInitialized((object) this, new ItemInitializedEventArgs(radPanel));
      this.allReadyGeneratedProperties.Add(pair.Key, editorControl);
      if (this.controlsInEachColumn.ContainsKey(currentColumn))
        this.controlsInEachColumn[currentColumn].Add(labelControl);
      else
        this.controlsInEachColumn.Add(currentColumn, new List<RadLabel>()
        {
          labelControl
        });
      this.validationInfoForEachEditor[editorControl].Label = labelControl;
      this.validationInfoForEachEditor[editorControl].ValidationLabel = validationControl;
      return true;
    }

    protected virtual void GenerateControlsDesignTime(
      int currentColumn,
      KeyValuePair<string, Control> pair,
      Size propertyItemControlSize,
      Point propertyItemControlLocation)
    {
      IComponent component1 = this.designerHost.CreateComponent(typeof (RadPanel));
      IComponent component2 = this.designerHost.CreateComponent(typeof (RadLabel));
      IComponent component3 = this.designerHost.CreateComponent(typeof (RadLabel));
      RadPanel radPanel = (RadPanel) component1;
      RadLabel labelControl = (RadLabel) component2;
      RadLabel validationControl = (RadLabel) component3;
      this.SetupInnerControls(pair, propertyItemControlSize, propertyItemControlLocation, radPanel, labelControl, validationControl, pair.Value);
      this.changeService.OnComponentChanging((object) this.DataEntryControl.Site.Component, (MemberDescriptor) null);
      radPanel.SuspendLayout();
      radPanel.Controls.Add(pair.Value);
      radPanel.Controls.Add((Control) labelControl);
      radPanel.Controls.Add((Control) validationControl);
      ItemInitializingEventArgs e = new ItemInitializingEventArgs(radPanel);
      this.OnItemInitializing((object) this, e);
      if (e.Cancel)
        return;
      this.DataEntryControl.PanelContainer.Controls.Add((Control) radPanel);
      this.OnItemInitialized((object) this, new ItemInitializedEventArgs(radPanel));
      this.changeService.OnComponentChanged((object) this.DataEntryControl.Site.Component, (MemberDescriptor) null, (object) null, (object) null);
      this.allReadyGeneratedProperties.Add(pair.Key, pair.Value);
      if (this.controlsInEachColumn.ContainsKey(currentColumn))
        this.controlsInEachColumn[currentColumn].Add(labelControl);
      else
        this.controlsInEachColumn.Add(currentColumn, new List<RadLabel>()
        {
          labelControl
        });
      radPanel.ResumeLayout(true);
      this.validationInfoForEachEditor[pair.Value].Label = labelControl;
      this.validationInfoForEachEditor[pair.Value].ValidationLabel = validationControl;
    }

    protected virtual void SetupInnerControls(
      KeyValuePair<string, Control> pair,
      Size propertyItemControlSize,
      Point propertyItemControlLocation,
      RadPanel propertyItemContainer,
      RadLabel labelControl,
      RadLabel validationControl,
      Control editorControl)
    {
      propertyItemContainer.PanelElement.PanelBorder.Visibility = ElementVisibility.Collapsed;
      if (this.RightToLeft)
      {
        labelControl.Dock = DockStyle.Right;
        validationControl.Dock = DockStyle.Left;
        labelControl.TextAlignment = ContentAlignment.MiddleRight;
      }
      else
      {
        labelControl.Dock = DockStyle.Left;
        validationControl.Dock = DockStyle.Right;
        labelControl.TextAlignment = ContentAlignment.MiddleLeft;
      }
      editorControl.Dock = DockStyle.Fill;
      labelControl.AutoSize = false;
      labelControl.TextWrap = false;
      labelControl.Text = pair.Key;
      validationControl.TextWrap = false;
      validationControl.LabelElement.MinSize = new Size(20, 0);
      if (editorControl is RadControl)
        (editorControl as RadControl).AutoSize = false;
      propertyItemContainer.Location = propertyItemControlLocation;
      propertyItemContainer.Size = propertyItemControlSize;
    }

    protected virtual void ArrangeLabels()
    {
      if (!this.AutoSizeLabels)
      {
        foreach (KeyValuePair<int, List<RadLabel>> keyValuePair in this.controlsInEachColumn)
        {
          float num = 0.0f;
          MeasurementGraphics measurementGraphics = MeasurementGraphics.CreateMeasurementGraphics();
          foreach (RadLabel radLabel in keyValuePair.Value)
          {
            SizeF sizeF = measurementGraphics.Graphics.MeasureString(radLabel.Text, radLabel.Font);
            if ((double) sizeF.Width > (double) num)
              num = (float) ((double) sizeF.Width + (double) this.itemSpace + 2.0);
          }
          foreach (RadLabel radLabel in keyValuePair.Value)
            radLabel.Size = new Size((int) Math.Floor((double) num), radLabel.Size.Height);
        }
      }
      else
      {
        foreach (KeyValuePair<int, List<RadLabel>> keyValuePair in this.controlsInEachColumn)
        {
          foreach (Control control in keyValuePair.Value)
            control.AutoSize = true;
        }
      }
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

    private Icon ConvertImageToIcon(Image image)
    {
      return Icon.FromHandle(new Bitmap(image).GetHicon());
    }

    internal void ClearBorders()
    {
      foreach (Control control in (ArrangedElementCollection) this.DataEntryControl.PanelContainer.Controls)
      {
        if (control is RadPanel)
          (control as RadPanel).PanelElement.PanelBorder.Visibility = ElementVisibility.Collapsed;
      }
    }

    private bool IsThisPropertyAlreadyCreated(string labeltext, PropertyDescriptor property)
    {
      foreach (Control control in (ArrangedElementCollection) this.DataEntryControl.PanelContainer.Controls)
      {
        RadPanel radPanel = control as RadPanel;
        if (radPanel != null && radPanel.Controls[1] is RadLabel && radPanel.Controls[1].Text == labeltext)
        {
          System.Type type;
          if (property.PropertyType.IsEnum)
          {
            type = typeof (RadDropDownList);
          }
          else
          {
            switch (property.PropertyType.Name)
            {
              case "DateTime":
                type = typeof (RadDateTimePicker);
                break;
              case "Boolean":
                type = typeof (RadCheckBox);
                break;
              case "Color":
                type = typeof (RadColorBox);
                break;
              case "Image":
                type = typeof (PictureBox);
                break;
              default:
                type = typeof (RadTextBox);
                break;
            }
          }
          if ((object) type == (object) radPanel.Controls[0].GetType())
            return true;
        }
      }
      return false;
    }

    public event ItemValidatedEventHandler ItemValidated;

    public event ItemValidatingEventHandler ItemValidating;

    public event EditorInitializingEventHandler EditorInitializing;

    public event EditorInitializedEventHandler EditorInitialized;

    public event ItemInitializingEventHandler ItemInitializing;

    public event ItemInitializedEventHandler ItemInitialized;

    public event BindingCreatingEventHandler BindingCreating;

    public event BindingCreatedEventHandler BindingCreated;

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.ClearBorders();
    }

    private void control_Validated(object sender, EventArgs e)
    {
      Control index = sender as Control;
      if (index == null || this.CurrentObject == null)
        return;
      ValidationInfo validationInfo = this.validationInfoForEachEditor[index];
      if (this.ErrorIcon != null)
        validationInfo.ErrorProvider.Icon = this.GetErrorIcon();
      this.OnItemValidated((object) index, new ItemValidatedEventArgs(validationInfo.Label, validationInfo.ValidationLabel, validationInfo.ErrorProvider, validationInfo.RangeAttribute));
    }

    private void control_Validating(object sender, CancelEventArgs e)
    {
      Control index = sender as Control;
      if (index == null || this.CurrentObject == null)
        return;
      ValidationInfo validationInfo = this.validationInfoForEachEditor[index];
      if (this.ErrorIcon != null)
        validationInfo.ErrorProvider.Icon = this.GetErrorIcon();
      ItemValidatingEventArgs e1 = new ItemValidatingEventArgs(validationInfo.Label, validationInfo.ValidationLabel, validationInfo.ErrorProvider, validationInfo.RangeAttribute);
      this.OnItemValidating((object) index, e1);
      e.Cancel = e1.Cancel;
    }

    private Icon GetErrorIcon()
    {
      if (this.errorIconCache == null)
        this.errorIconCache = this.ConvertImageToIcon(this.ErrorIcon);
      return this.errorIconCache;
    }

    protected internal virtual void OnItemValidated(object sender, ItemValidatedEventArgs e)
    {
      ItemValidatedEventHandler itemValidated = this.ItemValidated;
      if (itemValidated == null)
        return;
      itemValidated(sender, e);
    }

    protected internal virtual void OnItemValidating(object sender, ItemValidatingEventArgs e)
    {
      ItemValidatingEventHandler itemValidating = this.ItemValidating;
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

    protected internal virtual void OnItemInitializing(object sender, ItemInitializingEventArgs e)
    {
      ItemInitializingEventHandler itemInitializing = this.ItemInitializing;
      if (itemInitializing == null)
        return;
      itemInitializing((object) this, e);
    }

    protected internal virtual void OnItemInitialized(object sender, ItemInitializedEventArgs e)
    {
      ItemInitializedEventHandler itemInitialized = this.ItemInitialized;
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
      if (this.DataEntryControl.BindingContext == null)
        return;
      if (this.DataSource != null)
        this.manager = this.DataEntryControl.BindingContext[this.DataSource];
      if (!this.bindOnBindingContextChange)
        return;
      this.bindOnBindingContextChange = false;
      this.Bind();
    }
  }
}
