// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Commands
{
  public class CommandBase : ICommand, ICommandPresentation, IImageListProvider
  {
    private string name = string.Empty;
    private string text = string.Empty;
    private string type = string.Empty;
    private ImageList imageList;
    private System.Type owner;
    private System.Type context;

    public event CommandEventHandler HandleExecute;

    public event CommandEventHandler Executed;

    public CommandBase()
    {
    }

    public CommandBase(string name)
      : this(name, string.Empty, string.Empty)
    {
    }

    public CommandBase(string name, string text)
      : this(name, string.Empty, text)
    {
    }

    public CommandBase(string name, string text, string type)
    {
      this.name = name;
      this.text = text;
      this.type = type;
    }

    public override string ToString()
    {
      if (!string.IsNullOrEmpty(this.Name))
        return this.Name;
      return base.ToString();
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
      return this.Execute(new object[2]);
    }

    public virtual object Execute(params object[] settings)
    {
      if (settings.Length > 0)
      {
        this.RaiseHandleExecute(settings);
        this.RaiseExecuted(settings);
      }
      return (object) null;
    }

    protected virtual bool RaiseHandleExecute(params object[] settings)
    {
      if (this.HandleExecute == null)
        return false;
      CommandEventArgs e = new CommandEventArgs((object) this, settings);
      this.HandleExecute((object) this, e);
      return e.Cancel;
    }

    protected virtual void RaiseExecuted(params object[] settings)
    {
      if (this.Executed == null)
        return;
      this.Executed((object) this, new CommandEventArgs((object) this, settings));
    }

    public virtual bool CanExecute(object parameter)
    {
      return typeof (RadControl).IsAssignableFrom(parameter.GetType()) || typeof (RadItem).IsAssignableFrom(parameter.GetType());
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

    public System.Type OwnerType
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
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

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      EventHandler canExecuteChanged = this.CanExecuteChanged;
      if (canExecuteChanged == null)
        return;
      canExecuteChanged((object) this, EventArgs.Empty);
    }
  }
}
