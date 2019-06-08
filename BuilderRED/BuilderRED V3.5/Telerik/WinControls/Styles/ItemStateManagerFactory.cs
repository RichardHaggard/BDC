// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.ItemStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Styles
{
  public class ItemStateManagerFactory : ItemStateManagerFactoryBase
  {
    private StateNodeBase rootState;

    public StateNodeBase RootState
    {
      get
      {
        return this.rootState;
      }
    }

    public virtual StateNodeBase CreateRootState()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("all states");
      compositeStateNode.AddState(this.CreateEnabledStates());
      compositeStateNode.AddState(this.CreateSpecificStates());
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
      nodeWithCondition.TrueStateLink = (StateNodeBase) compositeStateNode;
      nodeWithCondition.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
      return (StateNodeBase) nodeWithCondition;
    }

    protected virtual StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Mouse states");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("ContainsMouse", (Condition) new SimpleCondition(RadElement.ContainsMouseProperty, (object) true));
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      return (StateNodeBase) compositeStateNode;
    }

    protected virtual StateNodeBase CreateSpecificStates()
    {
      return (StateNodeBase) new VoidStateNode();
    }

    protected virtual void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Disabled");
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("MouseDown");
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      this.rootState = this.CreateRootState();
      ItemStateManager stateManagerCore = this.CreateStateManagerCore();
      this.AddDefaultVisibleStates(stateManagerCore);
      return (ItemStateManagerBase) stateManagerCore;
    }

    protected virtual ItemStateManager CreateStateManagerCore()
    {
      return new ItemStateManager(this.RootState);
    }
  }
}
