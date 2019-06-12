// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.Group`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public class Group<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    where T : IDataItem
  {
    private object key;
    private string header;
    private bool isNullHeader;
    private Group<T> parent;

    public Group(object key)
      : this(key, (Group<T>) null)
    {
    }

    public Group(object key, Group<T> parent)
    {
      this.key = key;
      this.parent = parent;
      this.isNullHeader = true;
    }

    public int Level
    {
      get
      {
        int num = 0;
        for (Group<T> parent = this.parent; parent != null; parent = parent.parent)
          ++num;
        return num;
      }
    }

    public virtual string Header
    {
      get
      {
        if (this.isNullHeader && string.IsNullOrEmpty(this.header))
          this.header = this.DefaultHeader;
        return this.header;
      }
      set
      {
        this.header = value;
        this.isNullHeader = false;
      }
    }

    public object Key
    {
      get
      {
        return this.key;
      }
    }

    public virtual int ItemCount
    {
      get
      {
        return this.Items.Count;
      }
    }

    public virtual T this[int index]
    {
      get
      {
        return this.Items[index];
      }
    }

    public virtual Group<T> Parent
    {
      get
      {
        return this.parent;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual GroupCollection<T> Groups
    {
      get
      {
        return (GroupCollection<T>) null;
      }
    }

    public virtual bool Contains(T item)
    {
      return this.Items.Contains(item);
    }

    public virtual int IndexOf(T item)
    {
      return this.Items.IndexOf(item);
    }

    public object Evaluate(string expression)
    {
      ExpressionNode node = ExpressionParser.Parse(expression, false);
      if (this.Items.Count == 0 || ExpressionNode.GetNodes<AggregateNode>(node).Count == 0)
        return (object) null;
      List<NameNode> nodes = ExpressionNode.GetNodes<NameNode>(node);
      StringCollection stringCollection = new StringCollection();
      foreach (NameNode nameNode in nodes)
      {
        if (!stringCollection.Contains(nameNode.Name))
          stringCollection.Add(nameNode.Name);
      }
      ExpressionContext context = ExpressionContext.GetContext(Thread.CurrentThread.ManagedThreadId);
      context.Clear();
      for (int index = 0; index < stringCollection.Count; ++index)
      {
        if (context.ContainsKey(stringCollection[index]))
          context[stringCollection[index]] = this.Items[0][stringCollection[index]];
        else
          context.Add(stringCollection[index], this.Items[0][stringCollection[index]]);
      }
      return node.Eval((object) new AggregateItems<T>((IEnumerable<T>) this.Items), (object) context);
    }

    protected internal virtual IList<T> Items
    {
      get
      {
        return (IList<T>) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public IList<T> GetItems()
    {
      return this.Items;
    }

    private string DefaultHeader
    {
      get
      {
        object[] key = this.key as object[];
        if (key != null)
        {
          StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
          for (int index = 0; index < key.Length; ++index)
          {
            if (key[index] != null)
              stringBuilder.Append(key[index].ToString() + (object) ',');
          }
          if (stringBuilder.Length > 1)
            return stringBuilder.ToString(0, stringBuilder.Length - 1);
        }
        return string.Empty;
      }
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public virtual void CopyTo(T[] array, int index)
    {
      this.Items.CopyTo(array, index);
    }

    int IReadOnlyCollection<T>.Count
    {
      get
      {
        return this.ItemCount;
      }
    }
  }
}
