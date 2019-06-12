// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ObjectRelation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class ObjectRelation
  {
    private System.Collections.Generic.List<string> childNames = new System.Collections.Generic.List<string>();
    private string name;
    private object list;
    private ObjectRelation parent;
    private ObjectRelationCollecion childRelations;
    private PropertyDescriptorCollection properties;
    private object tag;

    public static ObjectRelation GetObjectRelation(object list)
    {
      if (list is BindingSource)
        list = (object) ((BindingSource) list).List;
      if (list is DataTable || list is DataView)
        return (ObjectRelation) new DataSetObjectRelation(list);
      return new ObjectRelation(list);
    }

    public static ObjectRelation GetObjectRelation(
      object dataSource,
      string dataMember)
    {
      if (dataSource is BindingSource)
      {
        if (((BindingSource) dataSource).DataSource == null && !string.IsNullOrEmpty(dataMember))
          return (ObjectRelation) null;
        if (((BindingSource) dataSource).DataSource is DataSet && !string.IsNullOrEmpty(dataMember))
          dataSource = ((BindingSource) dataSource).DataSource;
      }
      return ObjectRelation.GetObjectRelation(ListBindingHelper.GetList(dataSource, dataMember));
    }

    internal ObjectRelation(object list)
    {
      this.childRelations = new ObjectRelationCollecion();
      this.list = list;
      this.Initialize();
    }

    internal ObjectRelation(object dataSource, string dataMember)
      : this(ListBindingHelper.GetList(dataSource, dataMember))
    {
    }

    protected virtual void Initialize()
    {
      if (this.list is ITypedList)
      {
        this.properties = ((ITypedList) this.list).GetItemProperties((PropertyDescriptor[]) null);
        this.name = ((ITypedList) this.list).GetListName((PropertyDescriptor[]) null);
      }
      else if (this.list is IEnumerable)
      {
        this.properties = ListBindingHelper.GetListItemProperties(this.list);
        this.name = ListBindingHelper.GetListName(this.list, (PropertyDescriptor[]) null);
      }
      this.BuildChildren(this);
    }

    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    public ObjectRelation Parent
    {
      get
      {
        return this.parent;
      }
      protected set
      {
        this.parent = value;
      }
    }

    public PropertyDescriptorCollection Properties
    {
      get
      {
        return this.properties;
      }
      protected set
      {
        this.properties = value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      protected set
      {
        this.name = value;
      }
    }

    public object List
    {
      get
      {
        return this.list;
      }
    }

    public ObjectRelationCollecion ChildRelations
    {
      get
      {
        return this.childRelations;
      }
    }

    public virtual string[] ParentRelationNames
    {
      get
      {
        return new string[0];
      }
    }

    public virtual string[] ChildRelationNames
    {
      get
      {
        if (this.parent == null)
          return new string[0];
        if (this.childNames.Count == 0)
        {
          for (int index1 = 0; index1 < this.parent.Properties.Count; ++index1)
          {
            for (int index2 = 0; index2 < this.Properties.Count; ++index2)
            {
              if (this.Properties[index2].Name.Equals(this.parent.Properties[index1].Name, StringComparison.InvariantCultureIgnoreCase) && (object) this.Properties[index2].PropertyType == (object) this.parent.Properties[index1].PropertyType)
                this.childNames.Add(this.Properties[index2].Name);
            }
          }
        }
        return this.childNames.ToArray();
      }
    }

    private void BuildChildren(ObjectRelation parent)
    {
      if (this.properties == null)
        return;
      for (int index = 0; index < this.properties.Count; ++index)
      {
        bool flag = typeof (IBindingList).IsAssignableFrom(this.properties[index].PropertyType) || typeof (IList).IsAssignableFrom(this.properties[index].PropertyType) || typeof (ITypedList).IsAssignableFrom(this.properties[index].PropertyType) || typeof (IListSource).IsAssignableFrom(this.properties[index].PropertyType);
        if (!flag && typeof (IEnumerable).IsAssignableFrom(this.properties[index].PropertyType) && this.properties[index].PropertyType.IsGenericType)
          flag = !(this.list is string);
        if (flag)
        {
          IEnumerable list = this.list as IEnumerable;
          if (list != null)
          {
            IEnumerator enumerator = list.GetEnumerator();
            ObjectRelation objectRelation = !enumerator.MoveNext() ? new ObjectRelation((object) this.properties[index].PropertyType) : new ObjectRelation(this.properties[index].GetValue(enumerator.Current));
            objectRelation.childNames.Add(this.properties[index].Name);
            objectRelation.parent = this;
            this.childRelations.Add(objectRelation);
          }
        }
      }
    }
  }
}
