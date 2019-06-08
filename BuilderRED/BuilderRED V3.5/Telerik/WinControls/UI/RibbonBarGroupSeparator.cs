// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBarGroupSeparator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RibbonBarGroupSeparator : RadItem
  {
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (RibbonBarGroupSeparator), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private BorderPrimitive separatorPrimitive;

    static RibbonBarGroupSeparator()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RibbonBarGroupSeparator.RibbonBarGroupSeparatorStateManager(), typeof (RibbonBarGroupSeparator));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Class = "GroupSeparator";
      this.separatorPrimitive = new BorderPrimitive();
      this.separatorPrimitive.Class = "SeparatorPrimitive";
      this.Children.Add((RadElement) this.separatorPrimitive);
    }

    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(RibbonBarGroupSeparator.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RibbonBarGroupSeparator.OrientationProperty, (object) value);
      }
    }

    public BorderPrimitive SeparatorPrimitive
    {
      get
      {
        return this.separatorPrimitive;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      switch (this.Orientation)
      {
        case Orientation.Horizontal:
          sizeF = new SizeF((float) this.BorderThickness.Horizontal, sizeF.Height);
          break;
        case Orientation.Vertical:
          sizeF = new SizeF(sizeF.Width, (float) this.BorderThickness.Vertical);
          break;
      }
      return sizeF;
    }

    public class RibbonBarGroupSeparatorStateManager : ItemStateManagerFactory
    {
      protected override StateNodeBase CreateSpecificStates()
      {
        CompositeStateNode compositeStateNode = new CompositeStateNode("SeparatorStates");
        StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("HorizontalOrientation", (Condition) new SimpleCondition(RibbonBarGroupSeparator.OrientationProperty, (object) Orientation.Horizontal));
        StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("VerticalOrientation", (Condition) new SimpleCondition(RibbonBarGroupSeparator.OrientationProperty, (object) Orientation.Vertical));
        nodeWithCondition1.FalseStateLink = (StateNodeBase) nodeWithCondition2;
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
        return (StateNodeBase) compositeStateNode;
      }
    }
  }
}
