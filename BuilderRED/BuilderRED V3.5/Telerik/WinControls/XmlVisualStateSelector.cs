// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlVisualStateSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls
{
  public class XmlVisualStateSelector : XmlSelectorBase
  {
    private string visualState;

    public XmlVisualStateSelector()
    {
    }

    public XmlVisualStateSelector(string visualState)
    {
      this.visualState = visualState;
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

    protected override IElementSelector CreateInstance()
    {
      return (IElementSelector) new VisualStateSelector(this.VisualState);
    }
  }
}
