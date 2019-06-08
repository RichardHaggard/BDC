// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.ItemStateManagerFactoryBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Styles
{
  public abstract class ItemStateManagerFactoryBase
  {
    private ItemStateManagerBase stateManagerInstance;

    public ItemStateManagerBase StateManagerInstance
    {
      get
      {
        if (this.stateManagerInstance == null)
          this.stateManagerInstance = this.CreateStateManager();
        return this.stateManagerInstance;
      }
    }

    protected abstract ItemStateManagerBase CreateStateManager();
  }
}
