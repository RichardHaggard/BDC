// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemElementBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemElementBase : LightVisualElement, IVirtualizedElement<PropertyGridItemBase>
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (PropertyGridItemElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (PropertyGridItemElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsControlInactiveProperty = RadProperty.Register("IsControlInactive", typeof (bool), typeof (PropertyGridItemElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));

    static PropertyGridItemElementBase()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridItemElementBaseStateManager(), typeof (PropertyGridItemElementBase));
    }

    [Description("Gets a value indicating that the item is selected")]
    [Category("Appearance")]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(PropertyGridItemElementBase.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsSelectedProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets a value indicating that the item is expanded")]
    public virtual bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(PropertyGridItemElementBase.IsExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsExpandedProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets a value indicating whether the control contains the focus.")]
    public virtual bool IsControlFocused
    {
      get
      {
        return !(bool) this.GetValue(PropertyGridItemElementBase.IsControlInactiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsControlInactiveProperty, (object) value);
      }
    }

    public PropertyGridTableElement PropertyTableElement
    {
      get
      {
        return this.FindAncestor<PropertyGridTableElement>();
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.PropertyTableElement == null)
        return;
      this.PropertyTableElement.OnItemMouseDown(new PropertyGridMouseEventArgs(this.Data, e));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.PropertyTableElement == null)
        return;
      this.PropertyTableElement.OnItemMouseMove(new PropertyGridMouseEventArgs(this.Data, e));
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this.PropertyTableElement == null)
        return;
      this.PropertyTableElement.OnItemMouseClick(new RadPropertyGridEventArgs(this.Data));
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.PropertyTableElement == null)
        return;
      this.PropertyTableElement.OnItemMouseDoubleClick(new RadPropertyGridEventArgs(this.Data));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsMouseOverElementProperty || !this.IsInValidState(true))
        return;
      this.Synchronize();
    }

    protected virtual void OnItemPropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Selected")
      {
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsSelectedProperty, (object) this.Data.Selected);
        this.Synchronize();
      }
      else if (e.PropertyName == "Expanded")
      {
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsExpandedProperty, (object) this.Data.Expanded);
        this.Synchronize();
      }
      else
      {
        if (!(e.PropertyName == "ControlFocused"))
          return;
        int num = (int) this.SetValue(PropertyGridItemElementBase.IsControlInactiveProperty, (object) this.IsFocused);
        this.Synchronize();
      }
    }

    protected virtual void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!this.IsInValidState(true))
        return;
      this.OnItemPropertyChanged(e);
    }

    protected virtual void OnFormatting()
    {
      this.PropertyTableElement.OnItemFormatting(new PropertyGridItemFormattingEventArgs(this));
    }

    public virtual PropertyGridItemBase Data
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public virtual void Attach(PropertyGridItemBase data, object context)
    {
      throw new NotImplementedException();
    }

    public virtual void Detach()
    {
      throw new NotImplementedException();
    }

    public virtual void Synchronize()
    {
      this.PropertyTableElement.OnItemFormatting(new PropertyGridItemFormattingEventArgs(this));
    }

    public virtual bool IsCompatible(PropertyGridItemBase data, object context)
    {
      throw new NotImplementedException();
    }
  }
}
