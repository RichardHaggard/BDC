// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.GroupDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace Telerik.WinControls.Data
{
  public class GroupDescriptor : INotifyPropertyChanged
  {
    private SortDescriptorCollection groupNames;
    private GroupDescriptorCollection owner;
    private StringCollection aggregates;
    private string format;

    public GroupDescriptor()
    {
      this.groupNames = new SortDescriptorCollection();
      this.groupNames.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groupNames_CollectionChanged);
      this.format = "{0}: {1}";
      this.aggregates = new StringCollection();
    }

    public GroupDescriptor(string expression)
      : this()
    {
      this.Expression = expression;
    }

    public GroupDescriptor(string expression, string format)
      : this()
    {
      this.Expression = expression;
      if (format == null)
        format = string.Empty;
      this.format = format;
    }

    public GroupDescriptor(params SortDescriptor[] sortDescriptions)
      : this()
    {
      this.groupNames.AddRange(sortDescriptions);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public StringCollection Aggregates
    {
      get
      {
        return this.aggregates;
      }
    }

    [DefaultValue("{0}: {1}")]
    public string Format
    {
      get
      {
        return this.format;
      }
      set
      {
        this.format = value;
        this.OnPropertyChanged(nameof (Format));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    public virtual string Expression
    {
      get
      {
        return this.groupNames.Expression;
      }
      set
      {
        this.groupNames.Expression = value;
        this.OnPropertyChanged(nameof (Expression));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SortDescriptorCollection GroupNames
    {
      get
      {
        return this.groupNames;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GroupDescriptorCollection Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    public override string ToString()
    {
      if (this.GroupNames.Count < 1)
        return base.ToString();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.GroupNames.Count; ++index)
      {
        string str1 = "";
        if (index < this.GroupNames.Count - 1)
          str1 = ",";
        string str2 = "ASC";
        if (this.GroupNames[index].Direction == ListSortDirection.Descending)
          str2 = "DESC";
        stringBuilder.AppendFormat("{0} {1}{2}", (object) this.GroupNames[index].PropertyName, (object) str2, (object) str1);
      }
      return stringBuilder.ToString();
    }

    private void groupNames_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnPropertyChanged("GroupNames");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }
  }
}
