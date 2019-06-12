// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelTextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelTextElement : LightVisualElement
  {
    public static readonly RadProperty ExpandDirectionProperty = RadProperty.Register("ExpandDirection", typeof (RadDirection), typeof (CollapsiblePanelTextElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Down, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (CollapsiblePanelTextElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));

    static CollapsiblePanelTextElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new CollapsiblePanelMouseEventsStateManagerFactory(CollapsiblePanelTextElement.ExpandDirectionProperty, CollapsiblePanelTextElement.IsExpandedProperty), typeof (CollapsiblePanelTextElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Margin = new Padding(5, 0, 5, 0);
    }
  }
}
