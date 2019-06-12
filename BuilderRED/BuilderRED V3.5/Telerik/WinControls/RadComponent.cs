// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadComponent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [DesignTimeVisible(false)]
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadComponent : RadObject, IComponent, IDisposable
  {
    private ISite site;
    private string themeName;

    ~RadComponent()
    {
      this.Dispose(false);
    }

    protected override void DisposeManagedResources()
    {
      if (this.site != null && this.site.Container != null)
        this.site.Container.Remove((IComponent) this);
      base.DisposeManagedResources();
    }

    public override string ToString()
    {
      ISite site = this.site;
      if (site != null)
        return site.Name + " [" + this.GetType().FullName + "]";
      return this.GetType().FullName;
    }

    protected virtual object GetService(Type service)
    {
      return this.site?.GetService(service);
    }

    protected virtual bool CanRaiseEvents
    {
      get
      {
        return true;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IContainer Container
    {
      get
      {
        return this.site?.Container;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected bool DesignMode
    {
      get
      {
        ISite site = this.site;
        if (site != null)
          return site.DesignMode;
        return false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool IsDesignMode
    {
      get
      {
        if (this.site != null)
          return this.site.DesignMode;
        return base.IsDesignMode;
      }
      set
      {
        if (this.site != null)
          value = this.site.DesignMode;
        base.IsDesignMode = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ISite Site
    {
      get
      {
        return this.site;
      }
      set
      {
        this.site = value;
        if (this.site != null)
          base.IsDesignMode = this.site.DesignMode;
        else
          base.IsDesignMode = false;
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the theme name of the component.")]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Appearance")]
    public virtual string ThemeName
    {
      get
      {
        return this.themeName;
      }
      set
      {
        if (!(this.themeName != value))
          return;
        this.themeName = value;
        this.OnNotifyPropertyChanged(nameof (ThemeName));
      }
    }

    public virtual IComponentTreeHandler GetOwnedTreeHandler()
    {
      return (IComponentTreeHandler) null;
    }
  }
}
