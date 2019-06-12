// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WrapSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class WrapSelector : ElementSelector
  {
    private IElementSelector internalSelector;

    public WrapSelector(IElementSelector internalSelector)
    {
      this.internalSelector = internalSelector;
    }

    public IElementSelector InternalSelector
    {
      get
      {
        return this.internalSelector;
      }
      set
      {
        this.internalSelector = value;
      }
    }

    public override bool IsCompatible(RadObject element)
    {
      IStylableNode stylableNode = element as IStylableNode;
      ClassSelector internalSelector1 = this.internalSelector as ClassSelector;
      if (internalSelector1 != null)
        return internalSelector1.ElementClass == stylableNode.Class;
      TypeSelector internalSelector2 = this.internalSelector as TypeSelector;
      if (internalSelector2 != null)
        return (object) internalSelector2.ElementType == (object) element.GetType();
      return true;
    }

    public override bool IsValid(RadObject testElement, string state)
    {
      RadElement radElement = testElement as RadElement;
      if (this.internalSelector != null && radElement != null)
        return this.internalSelector.CanSelect((RadObject) radElement);
      return false;
    }
  }
}
