// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadComponentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ComVisible(false)]
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public class RadComponentElement : VisualElement, IBindableComponent, IComponent, IDisposable
  {
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected const long ComponentElementLastStateKey = 68719476736;
    private ISite site;
    private ControlBindingsCollection bindingsCollection;

    protected override void DisposeManagedResources()
    {
      if (this.site != null && this.site.DesignMode)
      {
        IDesignerHost service = this.site.GetService(typeof (IDesignerHost)) as IDesignerHost;
        if (service != null)
        {
          try
          {
            service.DestroyComponent((IComponent) this);
          }
          catch (InvalidOperationException ex)
          {
          }
        }
      }
      base.DisposeManagedResources();
    }

    public override string ToString()
    {
      ISite site = this.site;
      if (site != null)
        return site.Name + " [" + this.GetType().FullName + "]";
      return this.GetType().FullName;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadObject.BindingContextProperty)
        return;
      this.UpdateBindings(e);
    }

    protected virtual object GetService(System.Type service)
    {
      return this.site?.GetService(service);
    }

    private void UpdateBindings(RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadObject.BindingContextProperty)
        return;
      for (int index = 0; index < this.DataBindings.Count; ++index)
        BindingContext.UpdateBinding(this.BindingContext, this.DataBindings[index]);
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual ISite Site
    {
      get
      {
        return this.site;
      }
      set
      {
        if (this.site == value)
          return;
        this.site = value;
        if (this.site != null && this.site.DesignMode)
          this.SetIsDesignMode(true, true);
        else
          this.SetIsDesignMode(false, true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [ParenthesizePropertyName(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets the collection of data-binding objects for this IBindableComponent.")]
    public virtual ControlBindingsCollection DataBindings
    {
      get
      {
        if (this.bindingsCollection == null)
          this.bindingsCollection = new ControlBindingsCollection((IBindableComponent) this);
        return this.bindingsCollection;
      }
    }
  }
}
