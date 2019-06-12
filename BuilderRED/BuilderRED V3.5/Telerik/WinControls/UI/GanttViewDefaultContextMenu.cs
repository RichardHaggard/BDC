// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDefaultContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class GanttViewDefaultContextMenu : RadContextMenu
  {
    private Decimal progressStep = new Decimal(10);
    private bool showProgress = true;
    private RadGanttViewElement ganttViewElement;
    private GanttViewMenuItem addMenuItem;
    private GanttViewMenuItem addChildMenuItem;
    private GanttViewMenuItem addSiblingMenuItem;
    private GanttViewMenuItem deleteMenuItem;
    private GanttViewMenuItem progressMenuItem;

    public GanttViewDefaultContextMenu(RadGanttViewElement ganttViewElement)
    {
      this.ganttViewElement = ganttViewElement;
      this.addMenuItem = new GanttViewMenuItem("Add", "&Add");
      this.Items.Add((RadItem) this.addMenuItem);
      this.addChildMenuItem = new GanttViewMenuItem("AddChild", "Add &Child");
      this.addChildMenuItem.Click += new EventHandler(this.addMenuItem_Click);
      this.addMenuItem.Items.Add((RadItem) this.addChildMenuItem);
      this.addSiblingMenuItem = new GanttViewMenuItem("AddSibling", "Add &Sibling");
      this.addSiblingMenuItem.Click += new EventHandler(this.addMenuItem_Click);
      this.addMenuItem.Items.Add((RadItem) this.addSiblingMenuItem);
      this.deleteMenuItem = new GanttViewMenuItem("Delete", "&Delete");
      this.deleteMenuItem.Click += new EventHandler(this.deleteMenuItem_Click);
      this.Items.Add((RadItem) this.deleteMenuItem);
      this.progressMenuItem = new GanttViewMenuItem("Progress", "&Progress");
      this.Items.Add((RadItem) this.progressMenuItem);
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttViewElement;
      }
    }

    public GanttViewMenuItem AddMenuItem
    {
      get
      {
        return this.addMenuItem;
      }
    }

    public GanttViewMenuItem AddChildMenuItem
    {
      get
      {
        return this.addChildMenuItem;
      }
    }

    public GanttViewMenuItem AddSiblingMenuItem
    {
      get
      {
        return this.addSiblingMenuItem;
      }
    }

    public GanttViewMenuItem DeleteMenuItem
    {
      get
      {
        return this.deleteMenuItem;
      }
    }

    public GanttViewMenuItem ProgressMenuItem
    {
      get
      {
        return this.progressMenuItem;
      }
    }

    public bool ShowProgress
    {
      get
      {
        return this.showProgress;
      }
      set
      {
        this.showProgress = value;
      }
    }

    public Decimal ProgressStep
    {
      get
      {
        return this.progressStep;
      }
      set
      {
        this.progressStep = value;
      }
    }

    protected override void OnDropDownOpening(CancelEventArgs args)
    {
      base.OnDropDownOpening(args);
      GanttViewDataItem selectedItem = this.GanttViewElement.SelectedItem;
      if (this.ShowProgress && this.ProgressStep > new Decimal(0))
      {
        this.ProgressMenuItem.Visibility = ElementVisibility.Visible;
        bool flag = selectedItem.Progress % this.ProgressStep != new Decimal(0);
        Decimal num = new Decimal(0);
        while (num <= new Decimal(100))
        {
          GanttViewMenuItem ganttViewMenuItem1 = new GanttViewMenuItem(num.ToString(), string.Format("{0:P0}", (object) (num / new Decimal(100))));
          this.progressMenuItem.Items.Add((RadItem) ganttViewMenuItem1);
          ganttViewMenuItem1.IsChecked = selectedItem.Progress == num;
          ganttViewMenuItem1.Click += new EventHandler(this.progressMenuItem_Click);
          if (flag && selectedItem.Progress > num && selectedItem.Progress < num + this.ProgressStep)
          {
            GanttViewMenuItem ganttViewMenuItem2 = new GanttViewMenuItem(selectedItem.Progress.ToString(), string.Format("{0:P0}", (object) (selectedItem.Progress / new Decimal(100))));
            this.progressMenuItem.Items.Add((RadItem) ganttViewMenuItem2);
            ganttViewMenuItem2.IsChecked = true;
            ganttViewMenuItem2.Click += new EventHandler(this.progressMenuItem_Click);
          }
          num += this.ProgressStep;
        }
      }
      else
      {
        if (this.ShowProgress)
          return;
        this.ProgressMenuItem.Visibility = ElementVisibility.Collapsed;
      }
    }

    private void addMenuItem_Click(object sender, EventArgs e)
    {
      this.AddMenuItemClicked(sender as GanttViewMenuItem);
    }

    private void deleteMenuItem_Click(object sender, EventArgs e)
    {
      this.DeleteMenuItemClicked(sender as GanttViewMenuItem);
    }

    private void progressMenuItem_Click(object sender, EventArgs e)
    {
      this.ProgressMenuItemClicked(sender as GanttViewMenuItem);
    }

    protected virtual void AddMenuItemClicked(GanttViewMenuItem item)
    {
      if (!(item.Command == "AddChild") && !(item.Command == "AddSibling"))
        return;
      CreateGanttDataItemEventArgs e = new CreateGanttDataItemEventArgs();
      this.GanttViewElement.OnCreateDataItem(e);
      GanttViewDataItem ganttViewDataItem = e.Item ?? this.GanttViewElement.CreateNewTask();
      DateTime dateTime1 = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart.AddSeconds((double) (this.GanttViewElement.GraphicalViewElement.PointFromScreen(this.DropDown.Location).X + this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Value) * this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      DateTime dateTime2 = dateTime1.AddSeconds(this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds * 20.0);
      ganttViewDataItem.Start = dateTime1;
      ganttViewDataItem.End = dateTime2;
      if (item.Command == "AddChild")
      {
        this.GanttViewElement.SelectedItem.Items.Add(ganttViewDataItem);
      }
      else
      {
        if (!(item.Command == "AddSibling"))
          return;
        if (this.GanttViewElement.SelectedItem.Parent == null)
        {
          int index = this.GanttViewElement.Items.IndexOf(this.GanttViewElement.SelectedItem);
          if (index < this.GanttViewElement.Items.Count)
            this.GanttViewElement.Items.Insert(index, ganttViewDataItem);
          else
            this.GanttViewElement.Items.Add(ganttViewDataItem);
        }
        else
        {
          int index = this.GanttViewElement.SelectedItem.Parent.Items.IndexOf(this.GanttViewElement.SelectedItem);
          if (index < this.GanttViewElement.SelectedItem.Parent.Items.Count)
            this.GanttViewElement.SelectedItem.Parent.Items.Insert(index, ganttViewDataItem);
          else
            this.GanttViewElement.SelectedItem.Parent.Items.Add(ganttViewDataItem);
        }
      }
    }

    protected virtual void DeleteMenuItemClicked(GanttViewMenuItem item)
    {
      if (this.GanttViewElement.SelectedItem.Parent == null)
        this.GanttViewElement.Items.Remove(this.GanttViewElement.SelectedItem);
      else
        this.GanttViewElement.SelectedItem.Parent.Items.Remove(this.GanttViewElement.SelectedItem);
    }

    protected virtual void ProgressMenuItemClicked(GanttViewMenuItem item)
    {
      if (item == null)
        return;
      this.GanttViewElement.SelectedItem.Progress = (Decimal) int.Parse(item.Command);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.addChildMenuItem.Click += new EventHandler(this.addMenuItem_Click);
      this.addSiblingMenuItem.Click += new EventHandler(this.addMenuItem_Click);
      this.deleteMenuItem.Click += new EventHandler(this.deleteMenuItem_Click);
      foreach (RadElement radElement in (RadItemCollection) this.progressMenuItem.Items)
        radElement.Click -= new EventHandler(this.progressMenuItem_Click);
    }
  }
}
