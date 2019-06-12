// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRelation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class GridViewRelation : INotifyPropertyChanged, ICloneable, IComparable
  {
    private string relationName = string.Empty;
    private GridViewTemplate parentTemplate;
    private GridViewTemplate childTemplate;
    private StringCollection parentColumnNames;
    private StringCollection childColumnNames;

    public GridViewRelation()
    {
    }

    public GridViewRelation(GridViewTemplate parent)
    {
      this.parentTemplate = parent;
    }

    public GridViewRelation(GridViewTemplate parentTemplate, GridViewTemplate childTemplate)
    {
      this.parentTemplate = parentTemplate;
      this.childTemplate = childTemplate;
    }

    public void CopyTo(GridViewRelation relation)
    {
      relation.RelationName = this.relationName;
      relation.parentTemplate = this.parentTemplate;
      relation.childTemplate = this.childTemplate;
      for (int index = 0; index < this.ParentColumnNames.Count; ++index)
        relation.ParentColumnNames.Add(this.parentColumnNames[index]);
      for (int index = 0; index < this.ChildColumnNames.Count; ++index)
        relation.ChildColumnNames.Add(this.childColumnNames[index]);
    }

    [Editor("Telerik.WinControls.UI.Design.TemplateCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter(typeof (ReferenceConverter))]
    [DefaultValue(null)]
    public GridViewTemplate ParentTemplate
    {
      get
      {
        return this.parentTemplate;
      }
      set
      {
        this.parentTemplate = value;
        this.OnPropertyChanged(nameof (ParentTemplate));
      }
    }

    [Editor("Telerik.WinControls.UI.Design.TemplateCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter(typeof (ReferenceConverter))]
    [DefaultValue(null)]
    public GridViewTemplate ChildTemplate
    {
      get
      {
        return this.childTemplate;
      }
      set
      {
        this.childTemplate = value;
        this.OnPropertyChanged(nameof (ChildTemplate));
      }
    }

    [DefaultValue("")]
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

    [Editor("Telerik.WinControls.UI.Design.RelationStringCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public StringCollection ParentColumnNames
    {
      get
      {
        if (this.parentColumnNames == null)
          this.parentColumnNames = new StringCollection();
        return this.parentColumnNames;
      }
      set
      {
        this.parentColumnNames = value;
        this.OnPropertyChanged(nameof (ParentColumnNames));
      }
    }

    [Editor("Telerik.WinControls.UI.Design.RelationStringCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public StringCollection ChildColumnNames
    {
      get
      {
        if (this.childColumnNames == null)
          this.childColumnNames = new StringCollection();
        return this.childColumnNames;
      }
      set
      {
        this.childColumnNames = value;
        this.OnPropertyChanged(nameof (ChildColumnNames));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsSelfReference
    {
      get
      {
        return this.parentTemplate == this.childTemplate;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsObjectRelational
    {
      get
      {
        return this.childTemplate != null && this.parentTemplate != null && (this.childTemplate.DataSource == null && this.ChildColumnNames.Count == 1) && this.ParentColumnNames.Count <= 0;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsValid
    {
      get
      {
        if (this.IsObjectRelational)
          return true;
        if (this.ChildTemplate == null || this.ParentTemplate == null)
          return false;
        for (int index = 0; index < this.childColumnNames.Count; ++index)
        {
          if (!this.ChildTemplate.Columns.Contains(this.childColumnNames[index]))
            return false;
        }
        for (int index = 0; index < this.parentColumnNames.Count; ++index)
        {
          if (!this.ParentTemplate.Columns.Contains(this.parentColumnNames[index]))
            return false;
        }
        return true;
      }
    }

    object ICloneable.Clone()
    {
      GridViewRelation gridViewRelation = new GridViewRelation();
      gridViewRelation.RelationName = this.relationName;
      gridViewRelation.parentTemplate = this.parentTemplate;
      gridViewRelation.childTemplate = this.childTemplate;
      for (int index = 0; index < this.ParentColumnNames.Count; ++index)
        gridViewRelation.ParentColumnNames.Add(this.parentColumnNames[index]);
      for (int index = 0; index < this.ChildColumnNames.Count; ++index)
        gridViewRelation.ChildColumnNames.Add(this.childColumnNames[index]);
      return (object) gridViewRelation;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public int CompareTo(object obj)
    {
      GridViewRelation gridViewRelation = obj as GridViewRelation;
      if (gridViewRelation == null || this.ParentTemplate != gridViewRelation.ParentTemplate || (this.ChildTemplate != gridViewRelation.ChildTemplate || this.ParentColumnNames.Count != gridViewRelation.parentColumnNames.Count) || this.ChildColumnNames.Count != gridViewRelation.ChildColumnNames.Count)
        return -1;
      for (int index = 0; index < this.ParentColumnNames.Count; ++index)
      {
        if (this.ParentColumnNames[index].CompareTo(gridViewRelation.ParentColumnNames[index]) != 0)
          return -1;
      }
      for (int index = 0; index < this.ChildColumnNames.Count; ++index)
      {
        if (this.ChildColumnNames[index].CompareTo(gridViewRelation.ChildColumnNames[index]) != 0)
          return -1;
      }
      return 0;
    }
  }
}
