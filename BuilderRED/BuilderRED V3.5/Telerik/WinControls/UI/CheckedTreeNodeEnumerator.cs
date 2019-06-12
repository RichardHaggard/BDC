// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckedTreeNodeEnumerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class CheckedTreeNodeEnumerator : IEnumerator<RadTreeNode>, IDisposable, IEnumerator, IEnumerable<RadTreeNode>, IEnumerable
  {
    private Queue<RadTreeNode> queue = new Queue<RadTreeNode>();
    private RadTreeNode rootNode;
    private RadTreeNode current;

    public CheckedTreeNodeEnumerator(RadTreeNode rootNode)
    {
      this.rootNode = rootNode;
    }

    public void Dispose()
    {
      this.Reset();
      this.rootNode = (RadTreeNode) null;
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.current;
      }
    }

    public RadTreeNode Current
    {
      get
      {
        return this.current;
      }
    }

    public bool MoveNext()
    {
      this.EnqueueNodes(this.queue, this.current != null ? (IEnumerable<RadTreeNode>) this.current.Nodes : (IEnumerable<RadTreeNode>) this.rootNode.Nodes);
      while (this.queue.Count > 0)
      {
        RadTreeNode radTreeNode = this.queue.Dequeue();
        if (radTreeNode.Checked)
        {
          this.current = radTreeNode;
          return true;
        }
        this.EnqueueNodes(this.queue, (IEnumerable<RadTreeNode>) radTreeNode.Nodes);
      }
      return false;
    }

    private void EnqueueNodes(Queue<RadTreeNode> queue, IEnumerable<RadTreeNode> nodes)
    {
      foreach (RadTreeNode node in nodes)
        queue.Enqueue(node);
    }

    public void Reset()
    {
      this.current = (RadTreeNode) null;
      this.queue.Clear();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new CheckedTreeNodeEnumerator(this.rootNode);
    }

    public IEnumerator<RadTreeNode> GetEnumerator()
    {
      return (IEnumerator<RadTreeNode>) new CheckedTreeNodeEnumerator(this.rootNode);
    }
  }
}
