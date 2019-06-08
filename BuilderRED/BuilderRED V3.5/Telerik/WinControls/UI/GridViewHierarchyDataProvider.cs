// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHierarchyDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public abstract class GridViewHierarchyDataProvider : IDisposable
  {
    private GridViewTemplate template;
    private int notification;

    public static GridViewHierarchyDataProvider Create(
      GridViewRelation relation)
    {
      if (relation == null)
        return (GridViewHierarchyDataProvider) null;
      if (relation.IsSelfReference)
        return (GridViewHierarchyDataProvider) new GridViewSelfReferenceDataProvider(relation.ChildTemplate);
      if (relation.IsObjectRelational)
        return (GridViewHierarchyDataProvider) new GridViewObjectRelationalDataProvider(relation.ChildTemplate);
      return (GridViewHierarchyDataProvider) new GridViewRelationDataProvider(relation.ChildTemplate);
    }

    public GridViewHierarchyDataProvider(GridViewTemplate template)
    {
      this.template = template;
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public virtual GridViewRelation Relation
    {
      get
      {
        return (GridViewRelation) null;
      }
    }

    public abstract IList<GridViewRowInfo> GetChildRows(
      GridViewRowInfo parentRow,
      GridViewInfo view);

    public abstract GridViewHierarchyRowInfo GetParent(
      GridViewRowInfo gridViewRowInfo);

    public abstract void Refresh();

    public virtual void Dispose()
    {
      this.template = (GridViewTemplate) null;
    }

    public virtual bool IsVirtual
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsValid
    {
      get
      {
        return true;
      }
    }

    protected internal void SuspendNotifications()
    {
      ++this.notification;
    }

    protected internal void ResumeNotifications()
    {
      --this.notification;
    }

    protected bool IsSuspendedNotifications
    {
      get
      {
        return this.notification > 0;
      }
    }
  }
}
