// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterGroupNodeContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class DataFilterGroupNodeContentElement : TreeNodeContentElement
  {
    public static RadProperty LogicalOperatorProperty = RadProperty.Register("LogicalOperator", typeof (FilterLogicalOperator), typeof (DataFilterGroupNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) FilterLogicalOperator.And, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));

    static DataFilterGroupNodeContentElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DataFilterGroupElementStateManager(), typeof (DataFilterGroupNodeContentElement));
    }
  }
}
