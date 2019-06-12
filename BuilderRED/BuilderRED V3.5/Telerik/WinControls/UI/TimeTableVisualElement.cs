// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeTableVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TimeTableVisualElement : LightVisualElement
  {
    public static RadProperty ActiveProperty = RadProperty.Register(nameof (Active), typeof (bool), typeof (TimeTableVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (TimeTableVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private DateTime time;

    static TimeTableVisualElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TimeTableVisualElementStateManager(), typeof (TimeTableVisualElement));
    }

    public TimeTableVisualElement()
    {
      this.TextAlignment = ContentAlignment.MiddleRight;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    public bool Selected
    {
      get
      {
        return (bool) this.GetValue(TimeTableVisualElement.SelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(TimeTableVisualElement.SelectedProperty, (object) value);
      }
    }

    public bool Active
    {
      get
      {
        return (bool) this.GetValue(TimeTableVisualElement.ActiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(TimeTableVisualElement.ActiveProperty, (object) value);
      }
    }

    public DateTime Time
    {
      get
      {
        return this.time;
      }
      set
      {
        this.time = value;
      }
    }
  }
}
