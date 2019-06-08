// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadListVisualItem : LightVisualElement, IVirtualizedElement<RadListDataItem>
  {
    public static RadProperty ActiveProperty = RadProperty.Register(nameof (Active), typeof (bool), typeof (RadListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (RadListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOddProperty = RadProperty.Register(nameof (IsOdd), typeof (bool), typeof (RadListVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    internal RadListDataItem item;
    private static List<RadProperty> synchronizationProperties;
    private bool isAlternatingItemColorSet;

    static RadListVisualItem()
    {
      ElementPropertyOptions options = ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay;
      System.Type forType = typeof (RadListVisualItem);
      LightVisualElement.TextImageRelationProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.ImageBeforeText, options));
      LightVisualElement.ImageAlignmentProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, options));
      LightVisualElement.TextAlignmentProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, options));
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadListVisualItemStateManager(), typeof (RadListVisualItem));
      RadListVisualItem.InitializeSynchronizationProperties();
    }

    private static void InitializeSynchronizationProperties()
    {
      RadListVisualItem.SynchronizationProperties.Add(LightVisualElement.TextImageRelationProperty);
      RadListVisualItem.SynchronizationProperties.Add(LightVisualElement.ImageAlignmentProperty);
      RadListVisualItem.SynchronizationProperties.Add(LightVisualElement.TextAlignmentProperty);
      RadListVisualItem.SynchronizationProperties.Add(LightVisualElement.ImageProperty);
      RadListVisualItem.SynchronizationProperties.Add(LightVisualElement.TextWrapProperty);
      RadListVisualItem.SynchronizationProperties.Add(RadItem.TextOrientationProperty);
      RadListVisualItem.SynchronizationProperties.Add(VisualElement.FontProperty);
      RadListVisualItem.SynchronizationProperties.Add(VisualElement.ForeColorProperty);
      RadListVisualItem.SynchronizationProperties.Add(RadElement.EnabledProperty);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawBorder = true;
      this.BypassLayoutPolicies = true;
      this.NotifyParentOnMouseInput = true;
      this.AutoEllipsis = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.AllowDrag = true;
      this.AllowDrop = true;
    }

    protected static List<RadProperty> SynchronizationProperties
    {
      get
      {
        if (RadListVisualItem.synchronizationProperties == null)
          RadListVisualItem.synchronizationProperties = new List<RadProperty>();
        return RadListVisualItem.synchronizationProperties;
      }
    }

    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(RadListVisualItem.SelectedProperty);
      }
    }

    public bool Active
    {
      get
      {
        return (bool) this.GetValue(RadListVisualItem.ActiveProperty);
      }
    }

    [Category("Appearance")]
    public virtual bool IsOdd
    {
      get
      {
        return (bool) this.GetValue(RadListVisualItem.IsOddProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListVisualItem.IsOddProperty, (object) value);
      }
    }

    protected virtual bool CanApplyAlternatingColor
    {
      get
      {
        return true;
      }
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
    }

    public RadListDataItem Data
    {
      get
      {
        return this.item;
      }
      protected set
      {
        this.item = value;
      }
    }

    public virtual void Attach(RadListDataItem data, object context)
    {
      this.item = data;
      this.Text = this.Data.Text;
      int num1 = (int) this.BindProperty(RadListVisualItem.ActiveProperty, (RadObject) data, RadListDataItem.ActiveProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.BindProperty(RadListVisualItem.SelectedProperty, (RadObject) data, RadListDataItem.SelectedProperty, PropertyBindingOptions.OneWay);
      this.item.RadPropertyChanged += new RadPropertyChangedEventHandler(this.DataPropertyChanged);
      this.Synchronize();
    }

    public virtual void Detach()
    {
      int num1 = (int) this.UnbindProperty(RadListVisualItem.ActiveProperty);
      int num2 = (int) this.UnbindProperty(RadListVisualItem.SelectedProperty);
      this.item.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.DataPropertyChanged);
      this.item = (RadListDataItem) null;
    }

    public virtual void Synchronize()
    {
      if (this.IsDesignMode || this.item.Owner == null)
        return;
      this.SynchronizeProperties();
      this.UpdateAlternatingItemColor();
      this.item.Owner.OnVisualItemFormatting(this);
    }

    protected virtual void SynchronizeProperties()
    {
      this.Text = this.Data.Text;
      foreach (RadProperty synchronizationProperty in RadListVisualItem.SynchronizationProperties)
      {
        this.PropertySynchronizing(synchronizationProperty);
        if (this.Data.GetValueSource(synchronizationProperty) < ValueSource.Local)
        {
          int num1 = (int) this.ResetValue(synchronizationProperty, ValueResetFlags.Local);
        }
        else
        {
          int num2 = (int) this.SetValue(synchronizationProperty, this.Data.GetValue(synchronizationProperty));
        }
        this.PropertySynchronized(synchronizationProperty);
      }
    }

    protected virtual void PropertySynchronizing(RadProperty property)
    {
    }

    protected virtual void PropertySynchronized(RadProperty property)
    {
    }

    protected virtual void DataPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (this.IsDesignMode || this.item.Owner == null)
        return;
      this.SynchronizeProperties();
      this.item.Owner.OnVisualItemFormatting(this);
    }

    public virtual bool IsCompatible(RadListDataItem data, object context)
    {
      return !(data is DescriptionTextListDataItem);
    }

    protected virtual void UpdateAlternatingItemColor()
    {
      bool flag = false;
      if (this.Data.Owner.EnableAlternatingItemColor)
      {
        this.IsOdd = this.Data.Index % 2 == 1;
        if (this.BackColor != this.Data.Owner.AlternatingItemColor)
          this.isAlternatingItemColorSet = false;
        if (this.CanApplyAlternatingColor && this.IsOdd && !this.Data.Selected)
          flag = true;
      }
      else
        this.IsOdd = false;
      if (this.isAlternatingItemColorSet == flag)
        return;
      this.isAlternatingItemColorSet = flag;
      this.NotifyAlternatingItemChange();
    }

    protected void ResetAlternatingItemColor()
    {
      this.isAlternatingItemColorSet = false;
      this.ResetAlternatingItemProperties();
    }

    private void NotifyAlternatingItemChange()
    {
      if (this.isAlternatingItemColorSet && !this.ContainsMouse)
      {
        this.DrawFill = true;
        this.GradientStyle = GradientStyles.Solid;
        this.BackColor = this.Data.Owner.AlternatingItemColor;
      }
      else
        this.ResetAlternatingItemProperties();
    }

    private void ResetAlternatingItemProperties()
    {
      int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
    }
  }
}
