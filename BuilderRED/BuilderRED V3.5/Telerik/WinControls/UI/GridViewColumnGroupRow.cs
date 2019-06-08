// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColumnGroupRow
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewColumnGroupRow : INotifyPropertyChanged, INotifyPropertyChangingEx
  {
    private ColumnNameCollection columnNames;
    private int minHeight;

    public GridViewColumnGroupRow()
    {
      this.columnNames = new ColumnNameCollection();
      this.columnNames.CollectionChanged += new NotifyCollectionChangedEventHandler(this.columnNames_CollectionChanged);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public ColumnNameCollection ColumnNames
    {
      get
      {
        return this.columnNames;
      }
    }

    [DefaultValue(0)]
    public int MinHeight
    {
      set
      {
        if (this.minHeight == value)
          return;
        this.minHeight = value;
        this.OnPropertyChanged(nameof (MinHeight));
      }
      get
      {
        return this.minHeight;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void columnNames_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnPropertyChanged("ColumnNames");
    }

    event PropertyChangingEventHandlerEx INotifyPropertyChangingEx.PropertyChanging
    {
      add
      {
      }
      remove
      {
      }
    }
  }
}
