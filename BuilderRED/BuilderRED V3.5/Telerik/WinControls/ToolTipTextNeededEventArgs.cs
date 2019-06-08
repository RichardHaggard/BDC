// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ToolTipTextNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class ToolTipTextNeededEventArgs : EventArgs
  {
    private Size offset = Size.Empty;
    private string toolTipText;
    private ToolTip toolTip;

    public ToolTipTextNeededEventArgs(ToolTip toolTip)
      : this(toolTip, string.Empty)
    {
    }

    public ToolTipTextNeededEventArgs(ToolTip toolTip, string toolTipText)
      : this(toolTip, toolTipText, Size.Empty)
    {
    }

    public ToolTipTextNeededEventArgs(ToolTip toolTip, string toolTipText, Size offset)
    {
      this.offset = offset;
      this.toolTip = toolTip;
      this.toolTipText = toolTipText;
    }

    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        this.toolTipText = value;
      }
    }

    public Size Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
      }
    }

    public ToolTip ToolTip
    {
      get
      {
        return this.toolTip;
      }
    }
  }
}
