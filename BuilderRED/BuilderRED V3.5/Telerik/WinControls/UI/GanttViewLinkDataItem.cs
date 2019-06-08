// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewLinkDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewLinkDataItem : IDataItem, INotifyPropertyChanged
  {
    private List<Point> lines = new List<Point>();
    private object dataBoundItem;
    private RadGanttViewElement ganttView;
    private GanttViewDataItem startItem;
    private GanttViewDataItem endItem;
    private TasksLinkType linkType;
    private bool selected;

    [Editor("Telerik.WinControls.UI.Design.LinkStartEndItemEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public GanttViewDataItem StartItem
    {
      get
      {
        return this.startItem;
      }
      set
      {
        if (this.startItem == value)
          return;
        this.startItem = value;
        this.OnNotifyPropertyChanged(nameof (StartItem));
      }
    }

    [Editor("Telerik.WinControls.UI.Design.LinkStartEndItemEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public GanttViewDataItem EndItem
    {
      get
      {
        return this.endItem;
      }
      set
      {
        if (this.endItem == value)
          return;
        this.endItem = value;
        this.OnNotifyPropertyChanged(nameof (EndItem));
      }
    }

    public TasksLinkType LinkType
    {
      get
      {
        return this.linkType;
      }
      set
      {
        if (this.linkType == value)
          return;
        this.linkType = value;
        this.OnNotifyPropertyChanged(nameof (LinkType));
      }
    }

    public object DataBoundItem
    {
      get
      {
        return ((IDataItem) this).DataBoundItem;
      }
    }

    protected internal List<Point> Lines
    {
      get
      {
        return this.lines;
      }
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttView;
      }
      internal set
      {
        this.ganttView = value;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string name)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(name));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    protected internal virtual void SetDataBoundItem(bool dataBinding, object value)
    {
      this.dataBoundItem = value;
      if (this.GanttViewElement != null && this.GanttViewElement.DataSource != null)
        this.GanttViewElement.OnLinkDataBound(new GanttViewLinkDataBoundEventArgs(this));
      this.OnNotifyPropertyChanged("DataBoundItem");
    }

    object IDataItem.DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        this.SetDataBoundItem(false, value);
      }
    }

    int IDataItem.FieldCount
    {
      get
      {
        return 1;
      }
    }

    public bool Selected
    {
      get
      {
        return this.selected;
      }
      set
      {
        if (this.selected == value)
          return;
        this.selected = value;
        this.OnNotifyPropertyChanged(nameof (Selected));
        if (value)
          this.ShowItemsHandles();
        else
          this.HideItemsHandles();
      }
    }

    object IDataItem.this[string name]
    {
      get
      {
        return ((IDataItem) this)[0];
      }
      set
      {
        ((IDataItem) this)[0] = value;
      }
    }

    object IDataItem.this[int index]
    {
      get
      {
        return (object) this.LinkType;
      }
      set
      {
        this.LinkType = (TasksLinkType) value;
      }
    }

    int IDataItem.IndexOf(string name)
    {
      return name == "LinkType" ? 0 : -1;
    }

    public virtual void ShowStartItemHandle()
    {
      this.SetStartItemVisibility(ElementVisibility.Visible);
    }

    public virtual void HideStartItemHandle()
    {
      this.SetStartItemVisibility(ElementVisibility.Hidden);
    }

    private void SetStartItemVisibility(ElementVisibility visibility)
    {
      if (this.GanttViewElement == null)
        return;
      GanttGraphicalViewBaseItemElement element = this.GanttViewElement.GraphicalViewElement.GetElement(this.StartItem) as GanttGraphicalViewBaseItemElement;
      if (element == null)
        return;
      if (this.LinkType == TasksLinkType.StartToFinish || this.LinkType == TasksLinkType.StartToStart)
      {
        element.LeftLinkHandleElement.Visibility = visibility;
        element.RightLinkHandleElement.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        element.RightLinkHandleElement.Visibility = visibility;
        element.LeftLinkHandleElement.Visibility = ElementVisibility.Hidden;
      }
    }

    public virtual void ShowEndItemHandle()
    {
      this.SetEndItemVisibility(ElementVisibility.Visible);
    }

    public virtual void HideEndItemHandle()
    {
      this.SetEndItemVisibility(ElementVisibility.Hidden);
    }

    private void SetEndItemVisibility(ElementVisibility visibility)
    {
      if (this.GanttViewElement == null)
        return;
      GanttGraphicalViewBaseItemElement element = this.GanttViewElement.GraphicalViewElement.GetElement(this.EndItem) as GanttGraphicalViewBaseItemElement;
      if (element == null)
        return;
      if (this.LinkType == TasksLinkType.FinishToStart || this.LinkType == TasksLinkType.StartToStart)
      {
        element.LeftLinkHandleElement.Visibility = visibility;
        element.RightLinkHandleElement.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        element.RightLinkHandleElement.Visibility = visibility;
        element.LeftLinkHandleElement.Visibility = ElementVisibility.Hidden;
      }
    }

    public virtual void ShowItemsHandles()
    {
      if (this.GanttViewElement == null || this.GanttViewElement.ReadOnly)
        return;
      this.ShowStartItemHandle();
      this.ShowEndItemHandle();
    }

    public virtual void HideItemsHandles()
    {
      this.HideStartItemHandle();
      this.HideEndItemHandle();
    }
  }
}
