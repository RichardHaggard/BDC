// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.VisualStateSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Styles
{
  public class VisualStateSelector : HierarchicalSelector
  {
    private string visualState;

    public VisualStateSelector()
    {
    }

    public VisualStateSelector(string itemVisualState)
    {
      this.visualState = itemVisualState;
    }

    public string VisualState
    {
      get
      {
        return this.visualState;
      }
      set
      {
        this.visualState = value;
      }
    }

    protected override bool CanSelectOverride(RadObject element)
    {
      IStylableElement stylableElement = element as IStylableElement;
      if (stylableElement != null)
        return string.CompareOrdinal(stylableElement.VisualState, this.visualState) == 0;
      return false;
    }

    protected internal override bool CanUseCache
    {
      get
      {
        return false;
      }
    }

    protected override int GetKey()
    {
      if (!string.IsNullOrEmpty(this.visualState))
        return VisualStateSelector.GetSelectorKey(this.visualState);
      return 0;
    }

    public static int GetSelectorKey(string visualState)
    {
      return ("State=" + visualState).GetHashCode();
    }

    public override bool Equals(IElementSelector elementSelector)
    {
      VisualStateSelector visualStateSelector = elementSelector as VisualStateSelector;
      if (visualStateSelector != null)
        return visualStateSelector.visualState == this.visualState;
      return false;
    }

    public override string ToString()
    {
      if (this.visualState == null)
        return "VisualState == NotSpecified";
      return "VisualState == " + this.visualState;
    }
  }
}
