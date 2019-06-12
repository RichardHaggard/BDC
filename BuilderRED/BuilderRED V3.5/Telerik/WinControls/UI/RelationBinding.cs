// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RelationBinding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class RelationBinding : INotifyPropertyChanged, ICloneable
  {
    private string relationName = string.Empty;
    private string dataMember = string.Empty;
    private string parentMember = string.Empty;
    private string childMember = string.Empty;
    private string displayMember = string.Empty;
    private string valueMember = string.Empty;
    private string checkedMember = string.Empty;
    private object dataSource;

    public RelationBinding()
    {
    }

    public RelationBinding(
      object dataSource,
      string dataMember,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember,
      string checkedMember)
    {
      this.dataSource = dataSource;
      this.dataMember = dataMember;
      this.displayMember = displayMember;
      this.parentMember = parentMember;
      this.childMember = childMember;
      this.valueMember = valueMember;
      this.checkedMember = checkedMember;
    }

    public RelationBinding(
      object dataSource,
      string dataMember,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember)
      : this(dataSource, dataMember, displayMember, parentMember, childMember, valueMember, "")
    {
    }

    public RelationBinding(
      object dataSource,
      string displayMember,
      string parentMember,
      string childMember,
      string valueMember)
      : this(dataSource, "", displayMember, parentMember, childMember, valueMember, "")
    {
    }

    public RelationBinding(
      object dataSource,
      string displayMember,
      string parentMember,
      string childMember)
      : this(dataSource, "", displayMember, parentMember, childMember, "", "")
    {
    }

    public RelationBinding(object dataSource, string displayMember, string parentChildMember)
      : this(dataSource, "", displayMember, parentChildMember, parentChildMember, "", "")
    {
    }

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the name of the relation.")]
    public string RelationName
    {
      get
      {
        return this.relationName;
      }
      set
      {
        this.relationName = value;
        this.OnPropertyChanged(nameof (RelationName));
      }
    }

    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets the data source that the RelationBinding.")]
    [DefaultValue(null)]
    [Category("Data")]
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
        this.OnPropertyChanged(nameof (DataSource));
      }
    }

    [DefaultValue("")]
    [Browsable(true)]
    [Description("Gets or sets the data member. ")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Category("Data")]
    public string DataMember
    {
      get
      {
        return this.dataMember;
      }
      set
      {
        if (!(this.dataMember != value))
          return;
        this.dataMember = value;
        this.OnPropertyChanged(nameof (DataMember));
      }
    }

    [Description("Gets or sets the display member.")]
    [DefaultValue("")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
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
        this.OnPropertyChanged(nameof (DisplayMember));
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the parent member.")]
    [DefaultValue("")]
    [Category("Data")]
    public string ParentMember
    {
      get
      {
        return this.parentMember;
      }
      set
      {
        if (!(this.parentMember != value))
          return;
        this.parentMember = value;
        this.OnPropertyChanged(nameof (ParentMember));
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the child member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string ChildMember
    {
      get
      {
        return this.childMember;
      }
      set
      {
        if (!(this.childMember != value))
          return;
        this.childMember = value;
        this.OnPropertyChanged(nameof (ChildMember));
      }
    }

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the value member.")]
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
        this.OnPropertyChanged(nameof (ValueMember));
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the checked member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string CheckedMember
    {
      get
      {
        return this.checkedMember;
      }
      set
      {
        if (!(this.checkedMember != value))
          return;
        this.checkedMember = value;
        this.OnPropertyChanged(nameof (CheckedMember));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    object ICloneable.Clone()
    {
      return (object) new RelationBinding() { dataMember = this.dataMember, dataSource = this.dataSource, displayMember = this.displayMember, valueMember = this.valueMember, checkedMember = this.checkedMember, parentMember = this.parentMember };
    }

    public override bool Equals(object obj)
    {
      RelationBinding relationBinding = obj as RelationBinding;
      if (relationBinding != null && relationBinding.dataMember == this.dataMember && (relationBinding.dataSource == this.dataSource && relationBinding.checkedMember == this.checkedMember) && (relationBinding.displayMember == this.displayMember && relationBinding.valueMember == this.valueMember && relationBinding.parentMember == this.parentMember))
        return true;
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
