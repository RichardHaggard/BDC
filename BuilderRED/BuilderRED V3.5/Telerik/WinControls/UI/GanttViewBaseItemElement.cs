// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewBaseItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GanttViewBaseItemElement : GanttViewVisualElement, IVirtualizedElement<GanttViewDataItem>
  {
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (GanttViewBaseItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentProperty = RadProperty.Register(nameof (Current), typeof (bool), typeof (GanttViewBaseItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private GanttViewDataItem data;

    static GanttViewBaseItemElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GanttViewBaseItemElementStateManager(), typeof (GanttViewBaseItemElement));
    }

    public GanttViewDataItem Data
    {
      get
      {
        return this.data;
      }
    }

    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(GanttViewBaseItemElement.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewBaseItemElement.SelectedProperty, (object) value);
      }
    }

    public bool Current
    {
      get
      {
        return (bool) this.GetValue(GanttViewBaseItemElement.CurrentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewBaseItemElement.CurrentProperty, (object) value);
      }
    }

    public virtual void Attach(GanttViewDataItem data, object context)
    {
      if (data != null)
      {
        this.data = data;
        this.data.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
        int num1 = (int) this.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.Data, GanttViewDataItem.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
        int num2 = (int) this.BindProperty(GanttViewBaseItemElement.SelectedProperty, (RadObject) this.Data, GanttViewDataItem.SelectedProperty, PropertyBindingOptions.TwoWay);
        int num3 = (int) this.BindProperty(GanttViewBaseItemElement.CurrentProperty, (RadObject) this.Data, GanttViewDataItem.CurrentProperty, PropertyBindingOptions.TwoWay);
        int num4 = (int) this.BindProperty(RadElement.EnabledProperty, (RadObject) this.Data, GanttViewDataItem.EnabledProperty, PropertyBindingOptions.TwoWay);
      }
      this.Synchronize();
    }

    public virtual void Detach()
    {
      this.Data.SuspendPropertyNotifications();
      this.Data.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      int num1 = (int) this.UnbindProperty(RadElement.IsMouseOverProperty);
      int num2 = (int) this.UnbindProperty(GanttViewBaseItemElement.SelectedProperty);
      int num3 = (int) this.UnbindProperty(GanttViewBaseItemElement.CurrentProperty);
      int num4 = (int) this.UnbindProperty(RadElement.EnabledProperty);
      this.Data.ResumePropertyNotifications();
      this.data = (GanttViewDataItem) null;
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronize();
    }

    public virtual void Synchronize()
    {
    }

    public virtual bool IsCompatible(GanttViewDataItem data, object context)
    {
      return data != null;
    }
  }
}
