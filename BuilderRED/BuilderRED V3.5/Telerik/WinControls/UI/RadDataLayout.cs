// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadDataLayoutDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [TelerikToolboxCategory("Editors")]
  [Docking(DockingBehavior.Ask)]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [DefaultProperty("DataSource")]
  public class RadDataLayout : RadNCEnabledControl
  {
    private ValidationPanel validationPanel;
    private RadLayoutControl layoutControl;
    private RadDataLayoutElement dataLayoutElement;
    private bool showValidationPanel;

    public RadDataLayout()
    {
      this.InitializeInternalControls();
      this.WireEvents();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.dataLayoutElement = new RadDataLayoutElement();
      parent.Children.Add((RadElement) this.dataLayoutElement);
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    private void WireEvents()
    {
      if (this.ValidationPanel != null)
      {
        this.ValidationPanel.PanelContainer.ControlAdded += new ControlEventHandler(this.ValidationPanel_ControlAdded);
        this.ValidationPanel.PanelContainer.ControlRemoved += new ControlEventHandler(this.ValidationPanel_ControlRemoved);
      }
      if (this.DataLayoutElement == null)
        return;
      this.DataLayoutElement.ItemValidating += new DataLayoutItemValidatingEventHandler(this.DataEntryElement_ItemValidating);
      this.DataLayoutElement.ItemValidated += new DataLayoutItemValidatedEventHandler(this.DataEntryElement_ItemValidated);
      this.DataLayoutElement.ItemInitializing += new DataLayoutItemInitializingEventHandler(this.DataEntryElement_ItemInitializing);
      this.DataLayoutElement.ItemInitialized += new DataLayoutItemInitializedEventHandler(this.DataEntryElement_ItemInitialized);
      this.DataLayoutElement.EditorInitializing += new EditorInitializingEventHandler(this.DataEntryElement_EditorInitializing);
      this.DataLayoutElement.EditorInitialized += new EditorInitializedEventHandler(this.DataEntryElement_EditorInitialized);
      this.DataLayoutElement.BindingCreating += new BindingCreatingEventHandler(this.DataEntryElement_BindingCreating);
      this.DataLayoutElement.BindingCreated += new BindingCreatedEventHandler(this.DataEntryElement_BindingCreated);
    }

    private void UnwireEvents()
    {
      if (this.ValidationPanel != null)
      {
        this.ValidationPanel.ControlAdded -= new ControlEventHandler(this.ValidationPanel_ControlAdded);
        this.ValidationPanel.ControlRemoved -= new ControlEventHandler(this.ValidationPanel_ControlRemoved);
      }
      if (this.DataLayoutElement == null)
        return;
      this.DataLayoutElement.ItemValidating -= new DataLayoutItemValidatingEventHandler(this.DataEntryElement_ItemValidating);
      this.DataLayoutElement.ItemValidated -= new DataLayoutItemValidatedEventHandler(this.DataEntryElement_ItemValidated);
      this.DataLayoutElement.ItemInitializing -= new DataLayoutItemInitializingEventHandler(this.DataEntryElement_ItemInitializing);
      this.DataLayoutElement.ItemInitialized -= new DataLayoutItemInitializedEventHandler(this.DataEntryElement_ItemInitialized);
      this.DataLayoutElement.EditorInitializing -= new EditorInitializingEventHandler(this.DataEntryElement_EditorInitializing);
      this.DataLayoutElement.EditorInitialized -= new EditorInitializedEventHandler(this.DataEntryElement_EditorInitialized);
      this.DataLayoutElement.BindingCreating -= new BindingCreatingEventHandler(this.DataEntryElement_BindingCreating);
      this.DataLayoutElement.BindingCreated -= new BindingCreatedEventHandler(this.DataEntryElement_BindingCreated);
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
      foreach (LayoutControlItemBase allItem in this.LayoutControl.GetAllItems(true))
      {
        DataLayoutControlItem layoutControlItem = allItem as DataLayoutControlItem;
        if (layoutControlItem != null && layoutControlItem.AssociatedControl != null && (layoutControlItem.AssociatedControl.DataBindings.Count > 0 && layoutControlItem.AssociatedControl.DataBindings[0].DataSource == this.DataSource))
        {
          PropertyDescriptor property = (PropertyDescriptor) null;
          foreach (PropertyDescriptor itemProperty in layoutControlItem.AssociatedControl.DataBindings[0].BindingManagerBase.GetItemProperties())
          {
            if (itemProperty.Name == layoutControlItem.AssociatedControl.DataBindings[0].BindingMemberInfo.BindingMember)
              property = itemProperty;
          }
          if (property != null)
            this.DataLayoutElement.SubscribeControl(layoutControlItem, property);
        }
      }
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      this.DataLayoutElement.OnBindingContextChanged(e);
    }

    protected virtual void InitializeInternalControls()
    {
      this.validationPanel = new ValidationPanel();
      this.validationPanel.Dock = DockStyle.Bottom;
      this.validationPanel.MinimumSize = new Size(50, 50);
      this.validationPanel.Visible = false;
      this.layoutControl = new RadLayoutControl();
      this.layoutControl.DrawBorder = false;
      this.layoutControl.Dock = DockStyle.Fill;
      this.SuspendLayout();
      this.Controls.Add((Control) this.LayoutControl);
      this.Controls.Add((Control) this.ValidationPanel);
      this.ResumeLayout(true);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return false;
      }
      set
      {
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
    public RadDataLayoutElement DataLayoutElement
    {
      get
      {
        return this.dataLayoutElement;
      }
    }

    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    [Category("Data")]
    [Browsable(true)]
    [Description("Gets or sets the DataSource. Setting the DataSource will auto-generate editors for the fields in it.")]
    public object DataSource
    {
      get
      {
        return this.DataLayoutElement.DataSource;
      }
      set
      {
        this.DataLayoutElement.DataSource = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(1)]
    [Description("Gets or sets the number of columns which will be used to arrange generated controls.")]
    public int ColumnCount
    {
      get
      {
        return this.DataLayoutElement.ColumnCount;
      }
      set
      {
        this.DataLayoutElement.ColumnCount = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets the validation panel.")]
    public ValidationPanel ValidationPanel
    {
      get
      {
        return this.validationPanel;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets the inner RadLayoutControl.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadLayoutControl LayoutControl
    {
      get
      {
        return this.layoutControl;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the validation panel should appear.")]
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

    [DefaultValue(FlowDirection.TopDown)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating the flow direction of generated editors when the ColumnCount property has value bigger than 1.")]
    public FlowDirection FlowDirection
    {
      get
      {
        return this.DataLayoutElement.FlowDirection;
      }
      set
      {
        this.DataLayoutElement.FlowDirection = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(26)]
    [Category("Behavior")]
    [Description("The ItemDefaultHeight property sets the height that generated items should have.")]
    public int ItemDefaultHeight
    {
      get
      {
        return this.DataLayoutElement.ItemDefaultHeight;
      }
      set
      {
        this.DataLayoutElement.ItemDefaultHeight = value;
      }
    }

    [Browsable(false)]
    [Description("Gets the BindingManagerBase manager that is used to manage the current DataSource.")]
    [Category("Behavior")]
    public BindingManagerBase Manager
    {
      get
      {
        return this.DataLayoutElement.Manager;
      }
    }

    [Description("If [true], the labels will have a fixed size, best-fitted to the largest text in the column. If [false], the labels will have their default proportional size.")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool AutoSizeLabels
    {
      get
      {
        return this.DataLayoutElement.AutoSizeLabels;
      }
      set
      {
        this.DataLayoutElement.AutoSizeLabels = value;
      }
    }

    [Browsable(false)]
    [Description("Gets the current object.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object CurrentObject
    {
      get
      {
        return this.DataLayoutElement.CurrentObject;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    public event DataLayoutItemValidatedEventHandler ItemValidated;

    [Description("Occurs when the value of editor is about to change.")]
    [Category("Behavior")]
    public event DataLayoutItemValidatingEventHandler ItemValidating;

    [Description("Occurs when editor is being initialized. This event is cancelable.")]
    [Category("Behavior")]
    public event EditorInitializingEventHandler EditorInitializing;

    [Description("Occurs when the editor is Initialized.")]
    [Category("Behavior")]
    public event EditorInitializedEventHandler EditorInitialized;

    [Description("This event is firing when the panel that contains the label, editor and validation label is about to be Initialized. This event is cancelable.")]
    [Category("Behavior")]
    public event DataLayoutItemInitializingEventHandler ItemInitializing;

    [Description("Occurs the item is already Initialized.")]
    [Category("Behavior")]
    public event DataLayoutItemInitializedEventHandler ItemInitialized;

    [Category("Behavior")]
    [Description("Occurs when a binding object for an editor is about to be created. This event is cancelable.")]
    public event BindingCreatingEventHandler BindingCreating;

    [Description("Occurs when binding object is created.")]
    [Category("Behavior")]
    public event BindingCreatedEventHandler BindingCreated;

    private void ValidationPanel_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.UpdateValidationPanelVisibility();
    }

    private void ValidationPanel_ControlAdded(object sender, ControlEventArgs e)
    {
      this.UpdateValidationPanelVisibility();
    }

    private void DataEntryElement_ItemValidated(object sender, DataLayoutItemValidatedEventArgs e)
    {
      this.OnItemValidated(sender, e);
    }

    private void DataEntryElement_ItemValidating(object sender, DataLayoutItemValidatingEventArgs e)
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

    private void DataEntryElement_ItemInitializing(
      object sender,
      DataLayoutItemInitializingEventArgs e)
    {
      this.OnItemInitializing((object) this, e);
    }

    private void DataEntryElement_ItemInitialized(
      object sender,
      DataLayoutItemInitializedEventArgs e)
    {
      this.OnItemInitialized((object) this, e);
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

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.ValidationPanel.ThemeName = this.ThemeName;
      this.LayoutControl.ThemeName = this.ThemeName;
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.ItemDefaultHeight = Math.Max(this.ItemDefaultHeight, 36);
        this.DataLayoutElement.Clear();
        this.DataLayoutElement.InitializeDataEntry();
        this.DataLayoutElement.CreateEditors();
        this.DataLayoutElement.ArrangeControls();
        this.LayoutControl.UpdateControlsLayout();
        this.TrySubscribeControls();
      }
      this.InvalidateNCArea();
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

    protected override void SetBackColorThemeOverrides()
    {
      this.LayoutControl.BackColor = this.BackColor;
      this.LayoutControl.ContainerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.LayoutControl.ResetBackColor();
      this.LayoutControl.ContainerElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.LayoutControl.ContainerElement.ElementTree.ApplyThemeToElementTree();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.LayoutControl.ForeColor = this.ForeColor;
      this.LayoutControl.ContainerElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.LayoutControl.ForeColor = Color.Empty;
      this.LayoutControl.ContainerElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.LayoutControl.ContainerElement.ElementTree.ApplyThemeToElementTree();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      this.InvalidateNCArea();
      base.OnSizeChanged(e);
    }

    private void InvalidateNCArea()
    {
      if (!this.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 547);
    }

    protected override void OnNCPaint(Graphics g)
    {
      Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) this.DataLayoutElement, true);
      if (!(borderThickness != Padding.Empty))
        return;
      this.DataLayoutElement.BorderPrimitiveImpl.PaintBorder((IGraphics) new RadGdiGraphics(g), 0.0f, new SizeF(1f, 1f), new RectangleF(PointF.Empty, new SizeF((float) (this.Width - borderThickness.Right), (float) (this.Height - borderThickness.Bottom))));
    }

    protected override Padding GetNCMetrics()
    {
      return LightVisualElement.GetBorderThickness((LightVisualElement) this.DataLayoutElement, true);
    }

    public Padding ClientMargin
    {
      get
      {
        return this.GetNCMetrics();
      }
    }

    protected override bool EnableNCPainting
    {
      get
      {
        return true;
      }
    }

    protected override bool EnableNCModification
    {
      get
      {
        return true;
      }
    }
  }
}
