// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandComponentBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.Commands
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class CommandComponentBase : Component, ICommand, ICommandPresentation, IImageListProvider
  {
    private string name = string.Empty;
    private string type = string.Empty;
    private string text = string.Empty;
    private System.Type context;
    private ImageList imageList;
    private IContainer components;

    public event CommandEventHandler HandleExecute;

    public event CommandEventHandler Executed;

    public CommandComponentBase()
    {
      this.InitializeComponent();
    }

    public CommandComponentBase(IContainer container)
    {
      container.Add((IComponent) this);
      this.InitializeComponent();
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public string Type
    {
      get
      {
        return "";
      }
      set
      {
        this.type = value;
      }
    }

    public virtual object Execute()
    {
      return this.Execute((object) null, (object[]) null);
    }

    public virtual object Execute(params object[] settings)
    {
      return this.Execute((object) null, settings);
    }

    public virtual object Execute(object target, params object[] settings)
    {
      if (this.HandleExecute == null)
        return (object) false;
      this.HandleExecute((object) this, new CommandEventArgs(target, settings));
      if (this.Executed != null)
        this.Executed((object) this, new CommandEventArgs(target, settings));
      return (object) true;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        this.imageList = value;
      }
    }

    public Image GetImageAt(int index)
    {
      if (this.imageList != null && this.imageList.Images.Count > index)
        return this.imageList.Images[index];
      return (Image) null;
    }

    public Image GetImageAt(string index)
    {
      if (this.imageList != null && this.imageList.Images.ContainsKey(index))
        return this.imageList.Images[index];
      return (Image) null;
    }

    System.Type ICommand.OwnerType
    {
      get
      {
        throw new Exception("The method or operation is not implemented.");
      }
      set
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    public System.Type ContextType
    {
      get
      {
        return this.context;
      }
      set
      {
        this.context = value;
      }
    }

    public void Execute(object parameter, object target)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      EventHandler canExecuteChanged = this.CanExecuteChanged;
      if (canExecuteChanged == null)
        return;
      canExecuteChanged((object) this, EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
    }
  }
}
