// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.LayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Layouts
{
  public abstract class LayoutPanel : RadElement
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    protected const long LayoutPanelLastStateKey = 68719476736;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool VsbVisible
    {
      get
      {
        return false;
      }
    }
  }
}
