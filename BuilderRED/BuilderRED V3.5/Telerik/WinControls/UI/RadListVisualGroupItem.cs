// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListVisualGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  internal class RadListVisualGroupItem : RadListVisualItem
  {
    public static readonly RadProperty CollapsibleProperty = RadProperty.Register(nameof (Collapsible), typeof (bool), typeof (RadListVisualGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static readonly RadProperty CollapsedProperty = RadProperty.Register("Collapsed", typeof (bool), typeof (RadListVisualGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    private const int endOffset = 4;
    private LinePrimitive linePrimitive;

    static RadListVisualGroupItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadListVisualGroupItem.RadListVisualGroupItemStateManagerFactory(), typeof (RadListVisualGroupItem));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.linePrimitive = new LinePrimitive();
      this.linePrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.linePrimitive.BackColor = Color.Black;
      this.Children.Add((RadElement) this.linePrimitive);
    }

    public override bool IsCompatible(RadListDataItem data, object context)
    {
      return data is RadListDataGroupItem;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
    }

    public bool Collapsible
    {
      get
      {
        return this.Data.Collapsible;
      }
      set
      {
        this.Data.Collapsible = value;
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (!this.Collapsible)
        return;
      RadListDataGroupItem data = this.Data;
      data.Collapsed = !data.Collapsed;
    }

    public RadListDataGroupItem Data
    {
      get
      {
        return base.Data as RadListDataGroupItem;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.Layout.Arrange(clientRectangle);
      float width = this.Layout.DesiredSize.Width;
      this.linePrimitive.Arrange(new RectangleF(this.RightToLeft ? clientRectangle.Left + 4f : clientRectangle.Left + width, clientRectangle.Top + (float) (((double) clientRectangle.Height - (double) this.linePrimitive.LineWidth) / 2.0), (float) ((double) clientRectangle.Width - (double) width - 4.0), clientRectangle.Height));
      return finalSize;
    }

    public override void Attach(RadListDataItem data, object context)
    {
      int num1 = (int) this.BindProperty(RadListVisualGroupItem.CollapsibleProperty, (RadObject) data, RadListDataGroupItem.CollapsibleProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.BindProperty(RadListVisualGroupItem.CollapsedProperty, (RadObject) data, RadListDataGroupItem.CollapsedProperty, PropertyBindingOptions.OneWay);
      base.Attach(data, context);
    }

    public override void Detach()
    {
      int num1 = (int) this.UnbindProperty(RadListVisualGroupItem.CollapsibleProperty);
      int num2 = (int) this.UnbindProperty(RadListVisualGroupItem.CollapsedProperty);
      base.Detach();
    }

    private class RadListVisualGroupItemStateManagerFactory : ItemStateManagerFactory
    {
      protected StateNodeBase CreateCollapseStates()
      {
        StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Collapsible", (Condition) new SimpleCondition(RadListVisualGroupItem.CollapsibleProperty, (object) true));
        StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("Collapsed", (Condition) new SimpleCondition(RadListVisualGroupItem.CollapsedProperty, (object) true));
        CompositeStateNode compositeStateNode = new CompositeStateNode("list group header item states");
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
        compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
        return (StateNodeBase) compositeStateNode;
      }

      public override StateNodeBase CreateRootState()
      {
        CompositeStateNode compositeStateNode = new CompositeStateNode("all states");
        compositeStateNode.AddState(this.CreateEnabledStates());
        compositeStateNode.AddState(this.CreateCollapseStates());
        compositeStateNode.AddState(this.CreateSpecificStates());
        StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
        nodeWithCondition.TrueStateLink = (StateNodeBase) compositeStateNode;
        nodeWithCondition.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
        return (StateNodeBase) nodeWithCondition;
      }

      protected override ItemStateManagerBase CreateStateManager()
      {
        ItemStateManagerBase stateManager = base.CreateStateManager();
        stateManager.AddDefaultVisibleState("Collapsible");
        stateManager.AddDefaultVisibleState("Collapsible.Collapsed");
        stateManager.AddDefaultVisibleState("MouseOver.Collapsible");
        stateManager.AddDefaultVisibleState("MouseDown.Collapsible");
        return stateManager;
      }
    }
  }
}
