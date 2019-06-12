// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewComboBoxColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewComboBoxColumn : GridViewDataColumn, IBindableColumn
  {
    public static RadProperty DropDownStyleProperty = RadProperty.Register(nameof (DropDownStyle), typeof (RadDropDownStyle), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDropDownStyle.DropDownList));
    public static RadProperty AutoCompleteModeProperty = RadProperty.Register(nameof (AutoCompleteMode), typeof (AutoCompleteMode), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) AutoCompleteMode.None));
    private SortedDictionary<object, object> items = new SortedDictionary<object, object>();
    private bool syncSelectionWithText = true;
    private object dataSource;
    private string displayMember;
    private string valueMember;
    private GridViewFilteringMode filteringMode;
    private PropertyDescriptor valueDescriptor;
    private PropertyDescriptor displayDescriptor;
    private CurrencyManager currencyManager;
    private bool displayMemberSort;
    private object nullBoundItem;
    private bool isDisplayValueSubProperty;
    private bool isValueSubProperty;

    public GridViewComboBoxColumn()
    {
    }

    public GridViewComboBoxColumn(string fieldName)
      : base(fieldName)
    {
    }

    public GridViewComboBoxColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    protected override void DisposeManagedResources()
    {
      if (this.currencyManager != null)
        this.currencyManager.ListChanged -= new ListChangedEventHandler(this.currencyManager_ListChanged);
      base.DisposeManagedResources();
    }

    [DefaultValue(true)]
    public bool SyncSelectionWithText
    {
      get
      {
        return this.syncSelectionWithText;
      }
      set
      {
        this.syncSelectionWithText = value;
        this.OnNotifyPropertyChanged(nameof (SyncSelectionWithText));
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    public bool DisplayMemberSort
    {
      get
      {
        return this.displayMemberSort;
      }
      set
      {
        if (this.displayMemberSort == value)
          return;
        this.displayMemberSort = value;
        this.OnNotifyPropertyChanged(nameof (DisplayMemberSort));
      }
    }

    [Description("Gets or sets the data source that populates the items for the RadDropDownListEditor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Data")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    [Browsable(true)]
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
        this.nullBoundItem = (object) null;
        if (this.currencyManager != null)
          this.currencyManager.ListChanged -= new ListChangedEventHandler(this.currencyManager_ListChanged);
        this.currencyManager = (CurrencyManager) null;
        this.dataSource = value;
        this.EnsureDescriptors();
        this.DispatchEvent(KnownEvents.ColumnDataSourceInitializing, GridEventType.UI, GridEventDispatchMode.Send, (object) null, (object[]) null);
      }
    }

    [Category("Data")]
    [DefaultValue(null)]
    [Description("Gets or sets a string that specifies the property or database column from which to get values that correspond to the items in the RadDropDownListEditor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    public string ValueMember
    {
      get
      {
        return this.valueMember;
      }
      set
      {
        if (!(this.valueMember != value))
          return;
        this.valueMember = value;
        this.EnsureDescriptors();
        this.DispatchEvent(KnownEvents.ColumnDataSourceInitializing, GridEventType.UI, GridEventDispatchMode.Send, (object) null, (object[]) null);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [Description("Gets or sets a string that specifies the property or database column from which to retrieve strings for display in the RadDropDownListEditor items.")]
    [Browsable(true)]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public string DisplayMember
    {
      get
      {
        return this.displayMember;
      }
      set
      {
        if (!(this.displayMember != value))
          return;
        this.displayMember = value;
        this.EnsureDescriptors();
        this.DispatchEvent(KnownEvents.ColumnDataSourceInitializing, GridEventType.UI, GridEventDispatchMode.Send, (object) null, (object[]) null);
      }
    }

    [DefaultValue(AutoCompleteMode.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Specifies the mode for the automatic completion feature used in the RadDropDownListEditor.")]
    [Browsable(true)]
    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return (AutoCompleteMode) this.GetValue(GridViewComboBoxColumn.AutoCompleteModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewComboBoxColumn.AutoCompleteModeProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(RadDropDownStyle.DropDownList)]
    [Description("Gets or sets a value specifying the style of the RadDropDownListEditor.")]
    [Browsable(true)]
    [Category("Behavior")]
    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return (RadDropDownStyle) this.GetValue(GridViewComboBoxColumn.DropDownStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewComboBoxColumn.DropDownStyleProperty, (object) value);
      }
    }

    [Description("Gets or sets a value specifying the filtering mode.")]
    [DefaultValue(GridViewFilteringMode.ValueMember)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Behavior")]
    public GridViewFilteringMode FilteringMode
    {
      get
      {
        return this.filteringMode;
      }
      set
      {
        if (this.filteringMode == value)
          return;
        this.filteringMode = value;
        this.OnNotifyPropertyChanged(nameof (FilteringMode));
        this.FilterDescriptor = (FilterDescriptor) null;
      }
    }

    public bool HasLookupValue
    {
      get
      {
        if (this.DataSource != null && !string.IsNullOrEmpty(this.DisplayMember))
          return !string.IsNullOrEmpty(this.ValueMember);
        return false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected internal virtual System.Type DisplayMemberDataType
    {
      get
      {
        if (this.displayDescriptor != null)
          return this.displayDescriptor.PropertyType;
        return this.DataType;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected internal virtual System.Type FilteringMemberDataType
    {
      get
      {
        if (this.FilteringMode == GridViewFilteringMode.DisplayMember)
          return this.DisplayMemberDataType;
        return this.DataType;
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadDropDownListEditor();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      RadDropDownListEditor dropDownListEditor = editor as RadDropDownListEditor;
      if (dropDownListEditor == null)
        return;
      RadDropDownListElement editorElement = (RadDropDownListElement) dropDownListEditor.EditorElement;
      if (this.OwnerTemplate != null)
        editorElement.ListElement.BindingContext = this.OwnerTemplate.BindingContext;
      editorElement.DataSource = (object) null;
      editorElement.FilterExpression = string.Empty;
      editorElement.SyncSelectionWithText = this.SyncSelectionWithText;
      if (editorElement.Parent is GridFilterCellElement && this.FilteringMode == GridViewFilteringMode.DisplayMember)
      {
        string str = this.DisplayMember != null ? this.DisplayMember : this.ValueMember;
        editorElement.ValueMember = str;
        editorElement.DisplayMember = str;
      }
      else
      {
        editorElement.ValueMember = this.ValueMember != null ? this.ValueMember : this.DisplayMember;
        editorElement.DisplayMember = this.DisplayMember != null ? this.DisplayMember : this.ValueMember;
      }
      editorElement.DataSource = this.DataSource;
      editorElement.SelectedIndex = -1;
      editorElement.AutoCompleteMode = this.AutoCompleteMode;
      editorElement.DropDownStyle = this.DropDownStyle;
    }

    public override System.Type GetDefaultEditorType()
    {
      return typeof (RadDropDownListEditor);
    }

    public virtual object GetLookupValue(object cellValue)
    {
      if (cellValue == null || cellValue == DBNull.Value || cellValue is string && cellValue.ToString() == string.Empty)
      {
        if (this.nullBoundItem != null)
          return this.displayDescriptor.GetValue(this.nullBoundItem);
        return (object) null;
      }
      object obj = (object) null;
      if (this.items.Count > 0)
      {
        if (this.valueDescriptor != null)
          cellValue = RadDataConverter.Instance.Format(cellValue, this.valueDescriptor.PropertyType, (IDataConversionInfoProvider) this);
        try
        {
          if (this.items.TryGetValue(cellValue, out obj))
          {
            if (this.displayDescriptor != null)
            {
              if (!this.isDisplayValueSubProperty)
                return this.displayDescriptor.GetValue(obj);
              PropertyDescriptor innerDescriptor;
              object innerObject;
              this.GetSubPropertyByPath(this.DisplayMember, obj, out innerDescriptor, out innerObject);
              return innerObject;
            }
          }
        }
        catch (Exception ex)
        {
        }
        return (object) null;
      }
      if (cellValue != null && cellValue != Convert.DBNull && (this.EnsureDescriptors() && this.valueDescriptor != null) && this.displayDescriptor != null)
      {
        for (int index = 0; index < this.currencyManager.List.Count; ++index)
        {
          object objB = this.valueDescriptor.GetValue(this.currencyManager.List[index]);
          if (object.Equals(cellValue, objB))
            return this.displayDescriptor.GetValue(this.currencyManager.List[index]);
        }
      }
      return (object) null;
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      PropertyDescriptorCollection itemProperties = this.currencyManager.GetItemProperties();
      innerDescriptor = itemProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties().Find(strArray[index], true);
        innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    public override System.Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridComboBoxCellElement);
      if (row is GridViewFilteringRowInfo)
        return typeof (GridFilterComboBoxCellElement);
      return base.GetCellType(row);
    }

    protected internal override void Initialize()
    {
      base.Initialize();
      this.EnsureDescriptors();
    }

    internal override object GetValue(GridViewRowInfo row, GridViewDataOperation operation)
    {
      object cellValue = base.GetValue(row, operation);
      if (operation == GridViewDataOperation.Sorting && this.displayMemberSort || operation == GridViewDataOperation.Filtering && this.filteringMode == GridViewFilteringMode.DisplayMember)
        return this.GetLookupValue(cellValue);
      return cellValue;
    }

    private bool EnsureDescriptors()
    {
      if (this.dataSource == null || this.OwnerTemplate == null || this.OwnerTemplate.BindingContext == null)
        return false;
      if (this.currencyManager == null)
      {
        this.currencyManager = this.OwnerTemplate.BindingContext[this.dataSource] as CurrencyManager;
        this.BindToEnumerable();
        this.currencyManager.ListChanged += new ListChangedEventHandler(this.currencyManager_ListChanged);
      }
      if (this.currencyManager != null && !this.currencyManager.IsBindingSuspended)
      {
        PropertyDescriptorCollection itemProperties = this.currencyManager.GetItemProperties();
        if (!string.IsNullOrEmpty(this.valueMember))
        {
          this.valueDescriptor = itemProperties.Find(this.valueMember, true);
          if (this.valueDescriptor == null && this.valueMember.Contains("."))
          {
            this.isValueSubProperty = true;
            string[] strArray = this.valueMember.Split('.');
            this.valueDescriptor = itemProperties.Find(strArray[0], true);
            for (int index = 1; index < strArray.Length && this.valueDescriptor != null; ++index)
              this.valueDescriptor = this.valueDescriptor.GetChildProperties().Find(strArray[index], true);
          }
        }
        if (!string.IsNullOrEmpty(this.displayMember))
        {
          this.displayDescriptor = itemProperties.Find(this.displayMember, true);
          if (this.displayDescriptor == null && this.displayMember.Contains("."))
          {
            this.isDisplayValueSubProperty = true;
            string[] strArray = this.displayMember.Split('.');
            this.displayDescriptor = itemProperties.Find(strArray[0], true);
            for (int index = 1; index < strArray.Length && this.displayDescriptor != null; ++index)
              this.displayDescriptor = this.displayDescriptor.GetChildProperties().Find(strArray[index], true);
          }
          if (string.IsNullOrEmpty(this.valueMember))
            this.valueDescriptor = this.displayDescriptor;
        }
        else
          this.displayDescriptor = this.valueDescriptor;
        if (this.valueDescriptor != null)
        {
          this.items.Clear();
          for (int i = 0; i < this.currencyManager.Count; ++i)
            this.AddItem(i);
          this.DataType = this.valueDescriptor.PropertyType;
        }
      }
      if (this.valueDescriptor != null && this.displayDescriptor != null)
        return this.currencyManager != null;
      return false;
    }

    private void BindToEnumerable()
    {
      if (this.currencyManager != null || !(this.dataSource is IEnumerable))
        return;
      List<object> objectList = new List<object>((int) byte.MaxValue);
      foreach (object obj in this.dataSource as IEnumerable)
        objectList.Add(obj);
      this.currencyManager = this.OwnerTemplate.BindingContext[(object) new ReadOnlyCollection<object>((IList<object>) objectList), (string) null] as CurrencyManager;
    }

    private void currencyManager_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.Reset)
      {
        this.items.Clear();
        if (this.valueDescriptor != null)
        {
          for (int i = 0; i < this.currencyManager.Count; ++i)
            this.AddItem(i);
        }
        this.DispatchEvent(KnownEvents.ColumnDataSourceInitializing, GridEventType.UI, GridEventDispatchMode.Send, (object) null, (object[]) null);
      }
      else
      {
        if ((e.ListChangedType != ListChangedType.ItemAdded || this.valueDescriptor == null) && e.ListChangedType != ListChangedType.ItemChanged)
          return;
        this.AddItem(e.NewIndex);
      }
    }

    private void AddItem(int i)
    {
      object obj = this.currencyManager.List[i];
      if (this.valueDescriptor == null)
        this.EnsureDescriptors();
      object innerObject;
      if (this.isValueSubProperty)
      {
        PropertyDescriptor innerDescriptor;
        this.GetSubPropertyByPath(this.ValueMember, obj, out innerDescriptor, out innerObject);
      }
      else
        innerObject = this.valueDescriptor.GetValue(obj);
      if (this.valueDescriptor.PropertyType.IsEnum)
        innerObject = Enum.ToObject(this.valueDescriptor.PropertyType, innerObject);
      if (innerObject == null || innerObject == DBNull.Value)
        this.nullBoundItem = obj;
      else
        this.items[innerObject] = obj;
    }
  }
}
