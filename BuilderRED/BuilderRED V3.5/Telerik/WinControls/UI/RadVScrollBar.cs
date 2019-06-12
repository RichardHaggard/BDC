// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadVScrollBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ComVisible(false)]
  [Description("Enables its parent component to scroll content vertically")]
  public class RadVScrollBar : RadScrollBar
  {
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override ScrollType ScrollType
    {
      get
      {
        return ScrollType.Vertical;
      }
      set
      {
        throw new InvalidOperationException("Cannot change ScrollType of RadVScrollBar, use RadScrollBar instead.");
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(RadScrollBarElement.VerticalScrollBarWidth, 80);
      }
    }
  }
}
