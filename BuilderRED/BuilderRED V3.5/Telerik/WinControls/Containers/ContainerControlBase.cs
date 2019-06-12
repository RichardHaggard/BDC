// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Containers.ContainerControlBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.Collections;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.Containers
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ContainerControlBase : ContainerControl
  {
    private double sizeWeight = 0.5;
    protected TypeRestriction validationShema = TypeRestriction.ValidateForbiddenTypes;
    private static readonly object RequestResizeEventKey = new object();
    private static readonly object BorderStyleChangedEventKey = new object();
    private static readonly object BorderColorPropertyKey = new object();
    private static readonly object BorderColorChangedEventKey = new object();
    private BorderStyle borderStyle;
    protected int borderSize;
    protected Hashtable properties;
    private EventsCollection events;
    protected readonly List<System.Type> forbiddenTypes;
    protected readonly List<System.Type> allowedTypes;

    public ContainerControlBase()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
      this.UpdateStyles();
      this.forbiddenTypes = new List<System.Type>(3);
      this.allowedTypes = new List<System.Type>(3);
    }

    protected void AddEventHandler(object eventKey, Delegate handler)
    {
      if (this.events == null)
        this.events = new EventsCollection();
      this.events[eventKey] = (object) Delegate.Combine((Delegate) this.events[eventKey], handler);
    }

    protected void RemoveEventHandler(object eventKey, Delegate handler)
    {
      if (this.events == null)
        return;
      this.events[eventKey] = (object) Delegate.Remove((Delegate) this.events[eventKey], handler);
    }

    protected virtual void RaiseEvent(object eventKey, EventArgs e)
    {
      if (this.events == null)
        return;
      Delegate @delegate = (Delegate) this.events[eventKey];
      if ((object) @delegate == null)
        return;
      object[] objArray = new object[2]{ (object) this, (object) e };
      @delegate.DynamicInvoke(objArray);
    }

    public event EventHandler BorderStyleChanged
    {
      add
      {
        this.Events.AddHandler(ContainerControlBase.BorderStyleChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(ContainerControlBase.BorderStyleChangedEventKey, (Delegate) value);
      }
    }

    protected virtual void OnBorderStyleChanged(EventArgs e)
    {
      EventHandler eventHandler = this.Events[ContainerControlBase.BorderStyleChangedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style &= -8388609;
        if (this.borderStyle != BorderStyle.None)
        {
          switch (this.borderStyle)
          {
            case BorderStyle.FixedSingle:
              createParams.Style |= 8388608;
              break;
            case BorderStyle.Fixed3D:
              createParams.ExStyle |= 512;
              break;
          }
        }
        return createParams;
      }
    }

    [Description("Specifies the border style for a control.")]
    [DispId(-504)]
    [DefaultValue(0)]
    [Category("Appearance")]
    public virtual BorderStyle BorderStyle
    {
      get
      {
        return this.borderStyle;
      }
      set
      {
        if (this.borderStyle == value)
          return;
        this.borderStyle = value;
        switch (this.borderStyle)
        {
          case BorderStyle.None:
            this.borderSize = 0;
            break;
          case BorderStyle.FixedSingle:
            this.borderSize = 1;
            break;
          case BorderStyle.Fixed3D:
            this.borderSize = 4;
            break;
        }
        this.UpdateStyles();
        this.RecreateHandle();
        this.OnBorderStyleChanged(EventArgs.Empty);
      }
    }

    protected override Padding DefaultMargin
    {
      get
      {
        return new Padding(0, 0, 0, 0);
      }
    }

    protected override Padding DefaultPadding
    {
      get
      {
        return new Padding(0, 0, 0, 0);
      }
    }

    public virtual double SizeWeight
    {
      get
      {
        return this.sizeWeight;
      }
      set
      {
        this.sizeWeight = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new ContainerControlBase.ContainerTypedControlCollection((Control) this, false);
    }

    protected IntPtr SendMessage(int msg, bool wparam, int lparam)
    {
      return Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, this.Handle), msg, wparam, lparam);
    }

    protected static bool IsControlNullOrEmpty(Control control)
    {
      if (control != null)
        return control.Controls.Count == 0;
      return true;
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      this.ContainerLayout(e);
    }

    protected virtual Rectangle CalculateContainerClientArea()
    {
      return Rectangle.Empty;
    }

    protected virtual void LayoutContentCore()
    {
    }

    protected virtual void ContainerLayout(LayoutEventArgs e)
    {
      if (e.AffectedControl == null || string.IsNullOrEmpty(e.AffectedProperty))
        return;
      this.LayoutContentCore();
    }

    protected void SetPropertyValue(object key, object value)
    {
      if (this.properties == null)
        this.properties = new Hashtable();
      this.properties[key] = value;
    }

    protected object GetPropertyValue(object key)
    {
      if (this.properties == null)
        return (object) null;
      return this.properties[key];
    }

    protected void RemovePropertyValue(object key)
    {
      if (this.properties == null)
        return;
      this.properties.Remove(key);
      if (this.properties.Count != 0)
        return;
      this.properties = (Hashtable) null;
    }

    protected bool IsPropertyDefined(object key)
    {
      if (this.properties == null)
        return false;
      return this.properties.Contains(key);
    }

    protected virtual void RegisterForbiddenType(System.Type type)
    {
      this.forbiddenTypes.Add(type);
    }

    protected virtual void RegisterAllowedType(System.Type type)
    {
      this.allowedTypes.Add(type);
    }

    protected virtual List<System.Type> GetForbiddenTypes()
    {
      return this.forbiddenTypes;
    }

    protected virtual List<System.Type> GetAllowedTypes()
    {
      return this.allowedTypes;
    }

    protected virtual List<System.Type> GetValidationTypes(TypeRestriction validationShema)
    {
      switch (validationShema)
      {
        case TypeRestriction.ValidateForbiddenTypes:
          return this.GetForbiddenTypes();
        case TypeRestriction.ValidateAllowedTypes:
          return this.GetAllowedTypes();
        default:
          return (List<System.Type>) null;
      }
    }

    protected virtual List<System.Type> GetValidationTypes()
    {
      return this.GetValidationTypes(this.validationShema);
    }

    public class ContainerTypedControlCollection : Telerik.WinControls.ReadOnlyControlCollection
    {
      private const string readOnlyMessage = "The Controls collection is Read Only. You are not allowed to add controls.";
      private const string nullNotAllowedMessage = "Null refferences not allowed";
      private const string allowedOnlyMessage = "The ContainerTypedControlCollection of the current instance allows types: ";
      private const string forbiddenOnlyMessage = "The ContainerTypedControlCollection of the current instance forbids types: ";
      private ContainerControlBase owner;

      public ContainerTypedControlCollection(Control control, bool isReadOnly)
        : base(control, isReadOnly)
      {
        this.owner = control as ContainerControlBase;
      }

      public override void Add(Control value)
      {
        if (value == null)
          throw new ArgumentNullException("Null refferences not allowed");
        if (this.IsReadOnly)
          throw new NotSupportedException("The Controls collection is Read Only. You are not allowed to add controls.");
        this.ValidateType(value.GetType());
        base.Add(value);
      }

      public override void Remove(Control value)
      {
        this.ValidateType(value.GetType());
        if (this.owner != null && !this.owner.DesignMode && this.IsReadOnly)
          throw new NotSupportedException("The Controls collection is Read Only. You are not allowed to add controls.");
        base.Remove(value);
      }

      public override void SetChildIndex(Control child, int newIndex)
      {
        if (this.owner == null || this.owner.DesignMode)
          return;
        if (this.IsReadOnly)
          throw new NotSupportedException("The Controls collection is Read Only. You are not allowed to add controls.");
        base.SetChildIndex(child, newIndex);
      }

      public void ValidateType(System.Type type)
      {
        if (this.owner == null)
          return;
        List<System.Type> validationTypes = this.owner.GetValidationTypes(this.owner.validationShema);
        for (int index1 = 0; index1 < validationTypes.Count; ++index1)
        {
          if (validationTypes[index1].IsAssignableFrom(type))
          {
            object[] objArray = new object[validationTypes.Count];
            string format = string.Empty;
            for (int index2 = 0; index2 < validationTypes.Count; ++index2)
              objArray[index2] = (object) validationTypes[index2].Name;
            switch (this.owner.validationShema)
            {
              case TypeRestriction.ValidateForbiddenTypes:
                format = "The ContainerTypedControlCollection of the current instance forbids types: ";
                break;
              case TypeRestriction.ValidateAllowedTypes:
                format = "The ContainerTypedControlCollection of the current instance allows types: ";
                break;
            }
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, objArray));
          }
        }
      }
    }
  }
}
