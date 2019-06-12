// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewRelationBinding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class GanttViewRelationBinding : INotifyPropertyChanged, ICloneable
  {
    private string relationName = string.Empty;
    private string taskDataMember = string.Empty;
    private string parentMember = string.Empty;
    private string childMember = string.Empty;
    private string titleMember = string.Empty;
    private string startMember = string.Empty;
    private string endMember = string.Empty;
    private string progressMember = string.Empty;
    private string linkDataMember = string.Empty;
    private string linkStartMember = string.Empty;
    private string linkEndMember = string.Empty;
    private string linkTypeMember = string.Empty;
    private object dataSource;

    public GanttViewRelationBinding()
    {
    }

    public GanttViewRelationBinding(
      string parentMember,
      string childMember,
      string titleMember,
      string startMember,
      string endMember,
      string progressMember,
      string linkStartMember,
      string linkEndMember,
      string linkTypeMember)
    {
      this.parentMember = parentMember;
      this.childMember = childMember;
      this.titleMember = titleMember;
      this.startMember = startMember;
      this.endMember = endMember;
      this.progressMember = progressMember;
      this.linkStartMember = linkStartMember;
      this.linkEndMember = linkEndMember;
      this.linkTypeMember = linkTypeMember;
    }

    [Description("Gets or sets the name of the relation.")]
    [DefaultValue("")]
    [Category("Data")]
    public string RelationName
    {
      get
      {
        return this.relationName;
      }
      set
      {
        if (!(this.relationName != value))
          return;
        this.relationName = value;
        this.OnPropertyChanged(nameof (RelationName));
      }
    }

    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    [Description("Gets or sets the data source that the RelationBinding.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
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
    [Description("Gets or sets the tasks data member. ")]
    [Browsable(true)]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    public string TaskDataMember
    {
      get
      {
        return this.taskDataMember;
      }
      set
      {
        if (!(this.taskDataMember != value))
          return;
        this.taskDataMember = value;
        this.OnPropertyChanged("TasksDataMember");
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [Description("Gets or sets the liks data member. ")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    public string LinkDataMember
    {
      get
      {
        return this.linkDataMember;
      }
      set
      {
        if (!(this.linkDataMember != value))
          return;
        this.linkDataMember = value;
        this.OnPropertyChanged(nameof (LinkDataMember));
      }
    }

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the title member.")]
    public string TitleMember
    {
      get
      {
        return this.titleMember;
      }
      set
      {
        if (!(this.titleMember != value))
          return;
        this.titleMember = value;
        this.OnPropertyChanged(nameof (TitleMember));
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the parent member.")]
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

    [Description("Gets or sets the child member.")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
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
    [Description("Gets or sets the start member.")]
    public string StartMember
    {
      get
      {
        return this.startMember;
      }
      set
      {
        if (!(this.startMember != value))
          return;
        this.startMember = value;
        this.OnPropertyChanged(nameof (StartMember));
      }
    }

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the end member.")]
    public string EndMember
    {
      get
      {
        return this.endMember;
      }
      set
      {
        if (!(this.endMember != value))
          return;
        this.endMember = value;
        this.OnPropertyChanged(nameof (EndMember));
      }
    }

    [DefaultValue("")]
    [Description("Gets or sets the progress member.")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string ProgressMember
    {
      get
      {
        return this.progressMember;
      }
      set
      {
        if (!(this.progressMember != value))
          return;
        this.progressMember = value;
        this.OnPropertyChanged(nameof (ProgressMember));
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the link start member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string LinkStartMember
    {
      get
      {
        return this.linkStartMember;
      }
      set
      {
        if (!(this.linkStartMember != value))
          return;
        this.linkStartMember = value;
        this.OnPropertyChanged(nameof (LinkStartMember));
      }
    }

    [DefaultValue("")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the link end member.")]
    public string LinkEndMember
    {
      get
      {
        return this.linkEndMember;
      }
      set
      {
        if (!(this.linkEndMember != value))
          return;
        this.linkEndMember = value;
        this.OnPropertyChanged(nameof (LinkEndMember));
      }
    }

    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [Description("Gets or sets the link type member.")]
    public string LinkTypeMember
    {
      get
      {
        return this.linkTypeMember;
      }
      set
      {
        if (!(this.linkTypeMember != value))
          return;
        this.linkTypeMember = value;
        this.OnPropertyChanged(nameof (LinkTypeMember));
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
      return (object) new GanttViewRelationBinding() { taskDataMember = this.taskDataMember, dataSource = this.dataSource, childMember = this.childMember, parentMember = this.parentMember, titleMember = this.titleMember, startMember = this.startMember, endMember = this.endMember, progressMember = this.progressMember, linkDataMember = this.linkDataMember, linkStartMember = this.linkStartMember, linkEndMember = this.linkEndMember, linkTypeMember = this.linkTypeMember };
    }

    public override bool Equals(object obj)
    {
      GanttViewRelationBinding viewRelationBinding = obj as GanttViewRelationBinding;
      if (viewRelationBinding != null && viewRelationBinding.taskDataMember == this.taskDataMember && (viewRelationBinding.dataSource == this.dataSource && viewRelationBinding.childMember == this.childMember) && (viewRelationBinding.parentMember == this.parentMember && viewRelationBinding.startMember == this.startMember && (viewRelationBinding.endMember == this.endMember && viewRelationBinding.progressMember == this.progressMember)) && (viewRelationBinding.linkStartMember == this.linkStartMember && viewRelationBinding.linkTypeMember == this.linkTypeMember && (viewRelationBinding.titleMember == this.titleMember && viewRelationBinding.linkEndMember == this.linkEndMember) && viewRelationBinding.linkDataMember == this.linkDataMember))
        return true;
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
