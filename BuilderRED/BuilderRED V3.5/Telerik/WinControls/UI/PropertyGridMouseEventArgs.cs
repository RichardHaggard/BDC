// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridMouseEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridMouseEventArgs : RadPropertyGridEventArgs
  {
    private MouseEventArgs originalEventArgs;

    public MouseEventArgs OriginalEventArgs
    {
      get
      {
        return this.originalEventArgs;
      }
    }

    public PropertyGridMouseEventArgs(PropertyGridItemBase item)
      : base(item)
    {
    }

    public PropertyGridMouseEventArgs(PropertyGridItemBase item, MouseEventArgs originalArgs)
      : base(item)
    {
      this.originalEventArgs = originalArgs;
    }
  }
}
