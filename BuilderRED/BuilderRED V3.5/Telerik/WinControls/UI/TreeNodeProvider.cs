// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public abstract class TreeNodeProvider : IDisposable
  {
    private bool reflectInnerObjectRelationChanges = true;
    private RadTreeViewElement treeView;
    private int update;

    public TreeNodeProvider(RadTreeViewElement treeView)
    {
      this.treeView = treeView;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(true)]
    public bool ReflectInnerObjectRelationChanges
    {
      get
      {
        return this.reflectInnerObjectRelationChanges;
      }
      set
      {
        this.reflectInnerObjectRelationChanges = value;
      }
    }

    public abstract IList<RadTreeNode> GetNodes(RadTreeNode parent);

    public virtual void SetCurrent(RadTreeNode node)
    {
    }

    public RadTreeViewElement TreeView
    {
      get
      {
        return this.treeView;
      }
    }

    public virtual void Reset()
    {
    }

    public virtual void Dispose()
    {
    }

    public virtual void SuspendUpdate()
    {
      ++this.update;
    }

    public virtual void ResumeUpdate()
    {
      --this.update;
    }

    public bool IsSuspended
    {
      get
      {
        return this.update > 0;
      }
    }
  }
}
