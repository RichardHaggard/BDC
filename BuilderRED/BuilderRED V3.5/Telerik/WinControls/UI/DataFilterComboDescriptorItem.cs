// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterComboDescriptorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterComboDescriptorItem : DataFilterDescriptorItem
  {
    private RadDropDownStyle dropDownStyle;
    private AutoCompleteMode autoCompleteMode;
    private object dataSource;
    private string displayMember;
    private string valueMember;
    private RadListElement listElement;

    public DataFilterComboDescriptorItem()
    {
    }

    public DataFilterComboDescriptorItem(string propertyName, System.Type propertyType)
      : base(propertyName, propertyType)
    {
    }

    public DataFilterComboDescriptorItem(
      string propertyName,
      System.Type propertyType,
      object dataSource,
      string displayMember,
      string valueMember)
      : this(propertyName, propertyType)
    {
      this.dataSource = dataSource;
      this.displayMember = displayMember;
      this.valueMember = valueMember;
    }

    public DataFilterComboDescriptorItem(
      string propertyName,
      System.Type propertyType,
      object dataSource,
      string displayMember,
      string valueMember,
      RadDropDownStyle dropDownStyle,
      AutoCompleteMode autoCompleteMode)
      : this(propertyName, propertyType, dataSource, displayMember, valueMember)
    {
      this.dropDownStyle = dropDownStyle;
      this.autoCompleteMode = autoCompleteMode;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.dropDownStyle = RadDropDownStyle.DropDownList;
      this.autoCompleteMode = AutoCompleteMode.None;
    }

    [Description("Gets or sets the data source that populates the items for the TreeViewDropDownListEditor.")]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.Repaint)]
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
        this.dataSource = value;
      }
    }

    [Category("Data")]
    [Description("Gets or sets a string that specifies the property or database column from which to get values that correspond to the items in the TreeViewDropDownListEditor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    [DefaultValue(null)]
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
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Browsable(true)]
    [DefaultValue(null)]
    [Description("Gets or sets a string that specifies the property or database column from which to retrieve strings for display in the TreeViewDropDownListEditor items.")]
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
      }
    }

    [Category("Behavior")]
    [Description("Specifies the mode for the automatic completion feature used in the TreeViewDropDownListEditor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(AutoCompleteMode.None)]
    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.autoCompleteMode;
      }
      set
      {
        this.autoCompleteMode = value;
      }
    }

    [DefaultValue(RadDropDownStyle.DropDownList)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Description("Gets or sets a value specifying the style of the TreeViewDropDownListEditor.")]
    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.dropDownStyle;
      }
      set
      {
        this.dropDownStyle = value;
      }
    }

    private RadListElement ListElement
    {
      get
      {
        if (this.listElement == null)
          this.listElement = new RadListElement();
        return this.listElement;
      }
    }

    private void InitializeList()
    {
      this.ListElement.BindingContext = this.BindingContext;
      this.ListElement.DisplayMember = this.DisplayMember;
      this.ListElement.ValueMember = this.ValueMember;
      this.ListElement.DataSource = this.DataSource;
    }

    protected override object GetDefaultDescriptorValue()
    {
      if (this.DataSource == null || TelerikHelper.StringIsNullOrWhiteSpace(this.DisplayMember) || TelerikHelper.StringIsNullOrWhiteSpace(this.ValueMember))
        return (object) null;
      this.InitializeList();
      if (this.ListElement.Items.Count > 0)
        return this.ListElement.Items[0].Value;
      return (object) null;
    }

    protected internal virtual object GetDisplayMember(object value)
    {
      if (this.DataSource == null || TelerikHelper.StringIsNullOrWhiteSpace(this.DisplayMember) || TelerikHelper.StringIsNullOrWhiteSpace(this.ValueMember))
        return (object) null;
      this.InitializeList();
      if (this.ListElement.Items.Count == 0)
        return (object) string.Empty;
      this.ListElement.SelectedValue = value;
      if (this.ListElement.SelectedValue == null)
        return (object) this.ListElement.Items[0].Text;
      return (object) this.ListElement.SelectedItem.Text;
    }
  }
}
