// Decompiled with JetBrains decompiler
// Type: BuilderRED.CueProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BuilderRED
{
  public sealed class CueProvider
  {
    private const int EM_SETCUEBANNER = 5377;

    private CueProvider()
    {
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    public static void SetCue(TextBox textBox, string cue)
    {
      CueProvider.SendMessage(textBox.Handle, 5377, 0, cue);
    }

    public static void ClearCue(TextBox textBox)
    {
      CueProvider.SendMessage(textBox.Handle, 5377, 0, string.Empty);
    }
  }
}
