// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListViewAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadListViewAccessibleObject : Control.ControlAccessibleObject
  {
    private RadListView listView;

    public RadListViewAccessibleObject(RadListView control)
      : base((Control) control)
    {
      this.listView = control;
    }

    public RadListView ListView
    {
      get
      {
        return this.listView;
      }
      set
      {
        this.listView = value;
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        if (this.ListView.ViewType == ListViewType.DetailsView)
          return this.ListView.Items.Count.ToString() + " Rows, " + this.ListView.Columns.Count.ToString() + " Columns.";
        return base.Description;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.List;
      }
    }

    public override string Value
    {
      get
      {
        if (this.ListView.SelectedItem != null)
          return this.ListView.SelectedItem.Text;
        return (string) null;
      }
      set
      {
        if (this.ListView.SelectedItem.Text == null)
          return;
        this.ListView.SelectedItem.Text = value;
      }
    }

    public override AccessibleObject GetChild(int index)
    {
      ListViewTraverser enumerator = this.ListView.ListViewElement.ViewElement.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Reset();
      int num = -1;
      if (this.ListView.ViewType == ListViewType.DetailsView && this.ListView.ShowColumnHeaders)
      {
        if (index == 0)
          return (AccessibleObject) new ListViewHeaderRowAccessibleObject((this.listView.ListViewElement.ViewElement as DetailListViewElement).ColumnContainer, this.ListView.ListViewElement);
        num = 0;
      }
      while (enumerator.MoveNext())
      {
        ++num;
        if (num == index)
        {
          if (enumerator.Current is ListViewDataItemGroup)
            return (AccessibleObject) new ListViewDataGroupAccessibleObject(enumerator.Current as ListViewDataItemGroup);
          if (this.ListView.ViewType == ListViewType.DetailsView)
            return (AccessibleObject) new ListViewRowAccessibleObject(enumerator.Current);
          return (AccessibleObject) new ListViewDataItemAccessibleObject(enumerator.Current);
        }
      }
      return (AccessibleObject) null;
    }

    public override AccessibleObject HitTest(int x, int y)
    {
      BaseListViewVisualItem elementAtPoint = this.ListView.ElementTree.GetElementAtPoint(this.ListView.PointToClient(new Point(x, y))) as BaseListViewVisualItem;
      if (elementAtPoint != null)
        return (AccessibleObject) new ListViewDataItemAccessibleObject(elementAtPoint.Data);
      return (AccessibleObject) null;
    }

    public override int GetChildCount()
    {
      if (this.ListView.IsDisposed)
        return 0;
      ListViewTraverser enumerator = this.ListView.ListViewElement.ViewElement.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Reset();
      int num = 0;
      while (enumerator.MoveNext())
        ++num;
      return num;
    }

    public override AccessibleObject Navigate(AccessibleNavigation direction)
    {
      return (AccessibleObject) null;
    }
  }
}
