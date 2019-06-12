// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public abstract class PropertyGridItemBase : INotifyPropertyChanged
  {
    protected BitVector32 state = new BitVector32();
    private int imageIndex = -1;
    protected const int SuspendNotificationsState = 1;
    protected const int IsExpandedState = 2;
    protected const int IsVisibleState = 4;
    protected const int IsEnableState = 8;
    protected const int IsModifiedState = 16;
    private object tag;
    private int itemHeight;
    protected string text;
    private string description;
    private string toolTipText;
    private string imageKey;
    private Image image;
    private RadContextMenu contextMenu;
    private PropertyGridTableElement propertyGridTableElement;

    public PropertyGridItemBase(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridTableElement = propertyGridElement;
      this.state[4] = true;
      this.state[8] = true;
      this.state[4] = true;
    }

    public virtual PropertyGridTableElement PropertyGridTableElement
    {
      get
      {
        return this.propertyGridTableElement;
      }
    }

    public virtual bool Visible
    {
      get
      {
        return this.state[4];
      }
      set
      {
        this.SetBooleanProperty(nameof (Visible), 4, value);
        this.PropertyGridTableElement.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
      }
    }

    public virtual bool Selected
    {
      get
      {
        if (this.propertyGridTableElement != null)
          return this.propertyGridTableElement.SelectedGridItem == this;
        return false;
      }
      set
      {
        if (value)
        {
          this.propertyGridTableElement.SelectedGridItem = this;
        }
        else
        {
          if (this.propertyGridTableElement.SelectedGridItem != this)
            return;
          this.propertyGridTableElement.SelectedGridItem = (PropertyGridItemBase) null;
        }
      }
    }

    public virtual bool Expanded
    {
      get
      {
        return this.state[2];
      }
      set
      {
        if (this.Expanded == value)
          return;
        if (this.PropertyGridTableElement != null)
        {
          RadPropertyGridCancelEventArgs e = new RadPropertyGridCancelEventArgs(this);
          this.PropertyGridTableElement.OnItemExpandedChanging(e);
          if (e.Cancel)
            return;
        }
        this.SetBooleanProperty(nameof (Expanded), 2, value);
        if (value && this.GridItems != null && this.GridItems.Count > 1)
          this.GridItems.Sort(this.PropertyGridTableElement.CollectionView.Comparer);
        this.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
        if (this.PropertyGridTableElement == null)
          return;
        this.PropertyGridTableElement.OnItemExpandedChanged(new RadPropertyGridEventArgs(this));
      }
    }

    public virtual bool Enabled
    {
      get
      {
        return this.state[8];
      }
      set
      {
        if (this.Enabled == value)
          return;
        this.SetBooleanProperty(nameof (Enabled), 8, value);
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      }
    }

    public int ItemHeight
    {
      get
      {
        return this.itemHeight;
      }
      set
      {
        if (this.itemHeight == value)
          return;
        this.itemHeight = value;
        this.PropertyGridTableElement.Update(PropertyGridTableElement.UpdateActions.Resume);
        this.OnNotifyPropertyChanged(nameof (ItemHeight));
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image Image
    {
      get
      {
        if (this.image != null)
          return this.image;
        int index = this.ImageIndex >= 0 ? this.ImageIndex : this.propertyGridTableElement.ImageIndex;
        if (index >= 0 && string.IsNullOrEmpty(this.ImageKey))
        {
          RadControl control = this.propertyGridTableElement.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && index < imageList.Images.Count)
              return imageList.Images[index];
          }
        }
        string str = (string) null;
        if (this.propertyGridTableElement != null)
          str = this.propertyGridTableElement.ImageKey;
        string key = string.IsNullOrEmpty(this.ImageKey) ? str : this.ImageKey;
        if (!string.IsNullOrEmpty(key))
        {
          RadControl control = this.propertyGridTableElement.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && imageList.Images.Count > 0 && imageList.Images.ContainsKey(key))
              return imageList.Images[key];
          }
        }
        return (Image) null;
      }
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.OnNotifyPropertyChanged(nameof (Image));
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      }
    }

    [RelatedImageList("PropertyGrid.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        if (this.imageIndex == value)
          return;
        this.imageIndex = value;
        this.OnNotifyPropertyChanged(nameof (ImageIndex));
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      }
    }

    [RelatedImageList("PropertyGrid.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public string ImageKey
    {
      get
      {
        return this.imageKey;
      }
      set
      {
        if (!(this.imageKey != value))
          return;
        this.imageKey = value;
        this.imageIndex = -1;
        this.OnNotifyPropertyChanged(nameof (ImageKey));
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      }
    }

    public virtual string Label
    {
      get
      {
        return this.text;
      }
      set
      {
        if (!(this.text != value))
          return;
        this.text = value;
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("Text"));
      }
    }

    public virtual string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        if (!(this.description != value))
          return;
        this.description = value;
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Description)));
      }
    }

    public virtual string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        if (!(this.toolTipText != value))
          return;
        this.toolTipText = value;
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ToolTipText)));
      }
    }

    public virtual RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
        this.OnNotifyPropertyChanged(nameof (ContextMenu));
      }
    }

    public virtual object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    public int Level
    {
      get
      {
        if (this.Parent != null)
          return this.Parent.Level + 1;
        return 0;
      }
    }

    public abstract PropertyGridItemCollection GridItems { get; }

    public abstract bool Expandable { get; }

    public virtual PropertyGridItemBase Parent
    {
      get
      {
        return (PropertyGridItemBase) null;
      }
    }

    public abstract string Name { get; }

    public virtual void Expand()
    {
      this.Expanded = true;
    }

    public virtual void Collapse()
    {
      this.Expanded = false;
    }

    public virtual void EnsureVisible()
    {
      this.propertyGridTableElement.EnsureVisible(this);
    }

    public virtual void Select()
    {
      this.Selected = true;
    }

    protected virtual bool SetBooleanProperty(string propertyName, int propertyKey, bool value)
    {
      if (this.state[propertyKey] == value)
        return false;
      this.state[propertyKey] = value;
      if (!this.state[1])
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      return true;
    }

    protected virtual void Update(
      PropertyGridTableElement.UpdateActions updateAction)
    {
      if (this.state[1] || this.propertyGridTableElement == null)
        return;
      this.propertyGridTableElement.Update(updateAction);
    }

    public void SuspendPropertyNotifications()
    {
      this.state[1] = true;
    }

    public void ResumePropertyNotifications()
    {
      this.state[1] = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnNotifyPropertyChanged(string name)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(name));
    }

    public virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      if (this.state[1])
        return;
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, args);
    }
  }
}
