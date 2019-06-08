// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MessageBoxDetailsButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class MessageBoxDetailsButton : RadButton
  {
    protected override RadButtonElement CreateButtonElement()
    {
      return (RadButtonElement) new MessageBoxDetailsButtonElement();
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return ((MessageBoxDetailsButtonElement) this.ButtonElement).Arrow;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadButton).FullName;
      }
    }
  }
}
