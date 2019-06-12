// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataEntry
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
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Description("A data entry control that generates controls to edit the properties of databound object")]
  [DefaultProperty("DataSource")]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Editors")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadDataEntry : RadScrollablePanel
  {
    private ValidationPanel validationPanel;
    private bool showValidationPanel;

    public RadDataEntry()
    {
      this.ThemeClassName = typeof (RadScrollablePanel).FullName;
      this.WireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override RadScrollablePanelElement CreatePanelElement()
    {
      return (RadScrollablePanelElement) new RadDataEntryElement();
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new DataEntryPanelControlCollection(this);
    }

    private void WireEvents()
    {
      if (this.ValidationPanel != null)
      {
        this.ValidationPanel.PanelContainer.ControlAdded += new ControlEventHandler(this.ValidationPanel_ControlAdded);
        this.ValidationPanel.PanelContainer.ControlRemoved += new ControlEventHandler(this.ValidationPanel_ControlRemoved);
      }
      if (this.DataEntryElement == null)
        return;
      this.DataEntryElement.ItemValidating += new ItemValidatingEventHandler(this.DataEntryElement_ItemValidating);
      this.DataEntryElement.ItemValidated += new ItemValidatedEventHandler(this.DataEntryElement_ItemValidated);
      this.DataEntryElement.ItemInitializing += new ItemInitializingEventHandler(this.DataEntryElement_ItemInitializing);
      this.DataEntryElement.ItemInitialized += new ItemInitializedEventHandler(this.DataEntryElement_ItemInitialized);
      this.DataEntryElement.EditorInitializing += new EditorInitializingEventHandler(this.DataEntryElement_EditorInitializing);
      this.DataEntryElement.EditorInitialized += new EditorInitializedEventHandler(this.DataEntryElement_EditorInitialized);
      this.DataEntryElement.BindingCreating += new BindingCreatingEventHandler(this.DataEntryElement_BindingCreating);
      this.DataEntryElement.BindingCreated += new BindingCreatedEventHandler(this.DataEntryElement_BindingCreated);
    }

    private void UnwireEvents()
    {
      if (this.ValidationPanel != null)
      {
        this.ValidationPanel.ControlAdded -= new ControlEventHandler(this.ValidationPanel_ControlAdded);
        this.ValidationPanel.ControlRemoved -= new ControlEventHandler(this.ValidationPanel_ControlRemoved);
      }
      if (this.DataEntryElement == null)
        return;
      this.DataEntryElement.ItemValidating -= new ItemValidatingEventHandler(this.DataEntryElement_ItemValidating);
      this.DataEntryElement.ItemValidated -= new ItemValidatedEventHandler(this.DataEntryElement_ItemValidated);
      this.DataEntryElement.ItemInitializing -= new ItemInitializingEventHandler(this.DataEntryElement_ItemInitializing);
      this.DataEntryElement.ItemInitialized -= new ItemInitializedEventHandler(this.DataEntryElement_ItemInitialized);
      this.DataEntryElement.EditorInitializing -= new EditorInitializingEventHandler(this.DataEntryElement_EditorInitializing);
      this.DataEntryElement.EditorInitialized -= new EditorInitializedEventHandler(this.DataEntryElement_EditorInitialized);
      this.DataEntryElement.BindingCreating -= new BindingCreatingEventHandler(this.DataEntryElement_BindingCreating);
      this.DataEntryElement.BindingCreated -= new BindingCreatedEventHandler(this.DataEntryElement_BindingCreated);
    }

    public override void EndInit()
    {
      base.EndInit();
      if (this.Site != null || this.DataSource == null)
        return;
      ISupportInitializeNotification dataSource = this.DataSource as ISupportInitializeNotification;
      if (dataSource != null && !dataSource.IsInitialized)
        dataSource.Initialized += new EventHandler(this.supportInitializeDataSource_Initialized);
      else
        this.TrySubscribeControls();
    }

    private void supportInitializeDataSource_Initialized(object sender, EventArgs e)
    {
      ISupportInitializeNotification initializeNotification = sender as ISupportInitializeNotification;
      if (initializeNotification != null)
        initializeNotification.Initialized -= new EventHandler(this.supportInitializeDataSource_Initialized);
      this.TrySubscribeControls();
    }

    private void TrySubscribeControls()
    {
      foreach (Control control1 in (ArrangedElementCollection) this.PanelContainer.Controls)
      {
        RadPanel radPanel = control1 as RadPanel;
        RadLabel label = (RadLabel) null;
        RadLabel validationLabel = (RadLabel) null;
        if (radPanel != null)
        {
          label = radPanel.Controls.Count > 0 ? radPanel.Controls[1] as RadLabel : (RadLabel) null;
          validationLabel = radPanel.Controls.Count > 2 ? radPanel.Controls[2] as RadLabel : (RadLabel) null;
        }
        List<Control> controlList = new List<Control>(ControlHelper.EnumChildControls(control1, true));
        controlList.Insert(0, control1);
        foreach (Control control2 in controlList)
        {
          if (control2.DataBindings.Count > 0 && control2.DataBindings[0].DataSource == this.DataSource)
          {
            PropertyDescriptor property = (PropertyDescriptor) null;
            BindingManagerBase bindingManagerBase = control2.DataBindings[0].BindingManagerBase;
            if (bindingManagerBase != null)
            {
              foreach (PropertyDescriptor itemProperty in bindingManagerBase.GetItemProperties())
              {
                if (itemProperty.Name == control2.DataBindings[0].BindingMemberInfo.BindingMember)
                  property = itemProperty;
              }
              if (property != null)
                this.DataEntryElement.SubscribeControl(control1, property, label, validationLabel);
            }
          }
        }
      }
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      this.DataEntryElement.OnBindingContextChanged(e);
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      SizeF dpiScaleFactor = this.PanelElement.DpiScaleFactor;
      this.ItemDefaultSize = new Size(Math.Max(this.ItemDefaultSize.Width, (int) (300.0 / (double) dpiScaleFactor.Width)), Math.Max(this.ItemDefaultSize.Height, (int) (36.0 / (double) dpiScaleFactor.Height)));
      this.DataEntryElement.Clear();
      this.DataEntryElement.InitializeDataEntry();
      this.DataEntryElement.CreateEditors();
      this.DataEntryElement.ArrangeControls();
      foreach (Control control1 in (ArrangedElementCollection) this.PanelContainer.Controls)
      {
        RadPanel radPanel = control1 as RadPanel;
        if (radPanel != null)
        {
          RadTextBox control2 = radPanel.Controls[0] as RadTextBox;
          if (control2 != null)
            control2.TextBoxItem.StretchVertically = true;
        }
      }
    }

    protected override void InitializeInternalControls()
    {
      this.validationPanel = new ValidationPanel();
      this.validationPanel.Dock = DockStyle.Bottom;
      this.validationPanel.MinimumSize = new Size(50, 50);
      this.validationPanel.Visible = false;
      base.InitializeInternalControls();
    }

    protected override void InsertInternalControls()
    {
      base.InsertInternalControls();
      this.SuspendLayout();
      (this.Controls as DataEntryPanelControlCollection).AddControlInternal((Control) this.ValidationPanel);
      this.ResumeLayout(true);
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    protected override Size DefaultSize
    {
      get
      {
        return new Size(300, 150);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadDataEntryElement DataEntryElement
    {
      get
      {
        return (RadDataEntryElement) this.PanelElement;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [Description("Gets or sets the data source.")]
    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.DataEntryElement.DataSource;
      }
      set
      {
        this.DataEntryElement.DataSource = value;
      }
    }

    [Description("Gets or sets a value indicating whether the amount of columns that RadDataEntry will use to arrange generated controls.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(1)]
    public int ColumnCount
    {
      get
      {
        return this.DataEntryElement.ColumnCount;
      }
      set
      {
        this.DataEntryElement.ColumnCount = value;
      }
    }

    [Description("Gets or sets a value indicating whether the generated editors should fit their width to width of the RadDataEntry.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool FitToParentWidth
    {
      get
      {
        return this.DataEntryElement.FitToParentWidth;
      }
      set
      {
        this.DataEntryElement.FitToParentWidth = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets the validation panel.")]
    [Browsable(true)]
    public ValidationPanel ValidationPanel
    {
      get
      {
        return this.validationPanel;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the validation panel should be visible.")]
    [Browsable(false)]
    public bool ShowValidationPanel
    {
      get
      {
        return this.showValidationPanel;
      }
      set
      {
        if (this.showValidationPanel == value)
          return;
        this.showValidationPanel = value;
        this.UpdateValidationPanelVisibility();
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(FlowDirection.TopDown)]
    [Description("Gets or sets a value indicating whether generating flow of editors when the ColumnCount property has value bigger than 1.")]
    public FlowDirection FlowDirection
    {
      get
      {
        return this.DataEntryElement.FlowDirection;
      }
      set
      {
        this.DataEntryElement.FlowDirection = value;
      }
    }

    [DefaultValue(5)]
    [Description("Gets or sets the between the generated items.")]
    [Browsable(true)]
    [Category("Behavior")]
    public int ItemSpace
    {
      get
      {
        return this.DataEntryElement.ItemSpace;
      }
      set
      {
        this.DataEntryElement.ItemSpace = value;
      }
    }

    [DefaultValue(typeof (Size), "200, 22")]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("The ItemDefaultSize property sets the size that generated items should have if FitToParentWidth property has value false. When property the FitToParentWidth has value true the width of items are calculated according the width of the RadDataEntry.")]
    public Size ItemDefaultSize
    {
      get
      {
        return this.DataEntryElement.ItemDefaultSize;
      }
      set
      {
        this.DataEntryElement.ItemDefaultSize = value;
      }
    }

    [Description("Gets the BindingManagerBase manager that is used for current DataSource.")]
    [Browsable(false)]
    [Category("Behavior")]
    public BindingManagerBase Manager
    {
      get
      {
        return this.DataEntryElement.Manager;
      }
    }

    [Category("Behavior")]
    [Description("In RadDataEntry control there is logic that arranges the labels of the editors in one column according to the longest text. This logic can be controlled by the AutoSizeLabels property.")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool AutoSizeLabels
    {
      get
      {
        return this.DataEntryElement.AutoSizeLabels;
      }
      set
      {
        this.DataEntryElement.AutoSizeLabels = value;
      }
    }

    [Description("Gets the current object.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(null)]
    public object CurrentObject
    {
      get
      {
        return this.DataEntryElement.CurrentObject;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [Description("Occurs when the value of editor is changed.")]
    [Category("Behavior")]
    public event ItemValidatedEventHandler ItemValidated;

    [Category("Behavior")]
    [Description("Occurs when the value of editor is about to change.")]
    public event ItemValidatingEventHandler ItemValidating;

    [Description("Occurs when editor is being initialized. This event is cancelable.")]
    [Category("Behavior")]
    public event EditorInitializingEventHandler EditorInitializing;

    [Description("Occurs when the editor is Initialized.")]
    [Category("Behavior")]
    public event EditorInitializedEventHandler EditorInitialized;

    [Category("Behavior")]
    [Description("This event is firing when the panel that contains the label, editor and validation label is about to be Initialized. This event is cancelable.")]
    public event ItemInitializingEventHandler ItemInitializing;

    [Category("Behavior")]
    [Description("Occurs the item is already Initialized.")]
    public event ItemInitializedEventHandler ItemInitialized;

    [Description("Occurs when a binding object for an editor is about to be created. This event is cancelable.")]
    [Category("Behavior")]
    public event BindingCreatingEventHandler BindingCreating;

    [Category("Behavior")]
    [Description("Occurs when binding object is created.")]
    public event BindingCreatedEventHandler BindingCreated;

    private void ValidationPanel_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.UpdateValidationPanelVisibility();
    }

    private void ValidationPanel_ControlAdded(object sender, ControlEventArgs e)
    {
      this.UpdateValidationPanelVisibility();
    }

    private void DataEntryElement_ItemValidated(object sender, ItemValidatedEventArgs e)
    {
      this.OnItemValidated(sender, e);
    }

    private void DataEntryElement_ItemValidating(object sender, ItemValidatingEventArgs e)
    {
      this.OnItemValidating(sender, e);
    }

    private void DataEntryElement_BindingCreated(object sender, BindingCreatedEventArgs e)
    {
      this.OnBindingCreated((object) this, e);
    }

    private void DataEntryElement_BindingCreating(object sender, BindingCreatingEventArgs e)
    {
      this.OnBindingCreating((object) this, e);
    }

    private void DataEntryElement_EditorInitialized(object sender, EditorInitializedEventArgs e)
    {
      this.OnEditorInitialized((object) this, e);
    }

    private void DataEntryElement_EditorInitializing(object sender, EditorInitializingEventArgs e)
    {
      this.OnEditorInitializing((object) this, e);
    }

    private void DataEntryElement_ItemInitializing(object sender, ItemInitializingEventArgs e)
    {
      this.OnItemInitializing((object) this, e);
    }

    private void DataEntryElement_ItemInitialized(object sender, ItemInitializedEventArgs e)
    {
      this.OnItemInitialized((object) this, e);
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

    public virtual void UpdateValidationPanelVisibility()
    {
      if (this.ShowValidationPanel)
      {
        if (this.ValidationPanel.PanelContainer.Controls.Count == 0)
          this.ValidationPanel.Visible = false;
        else
          this.ValidationPanel.Visible = true;
      }
      else
        this.ValidationPanel.Visible = false;
    }

    protected override RadScrollablePanelContainer CreateScrollablePanelContainer()
    {
      return (RadScrollablePanelContainer) new DataEntryScrollablePanelContainer((RadScrollablePanel) this);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "", "RadScrollablePanelFill");
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      this.PanelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.PanelElement.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.PanelElement.ElementTree.ApplyThemeToElementTree();
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.PanelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.PanelElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.PanelElement.ElementTree.ApplyThemeToElementTree();
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
    }
  }
}
