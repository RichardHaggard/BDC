// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownTextBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadDropDownTextBoxElement : RadTextBoxElement
  {
    private const int WM_PASTE = 770;
    private NativeWindowAdapter adapter;

    public event EventHandler Pasted;

    public RadDropDownTextBoxElement()
    {
      this.TextBoxItem.TextBoxControl.HandleCreated += new EventHandler(this.TextBoxControl_HandleCreated);
    }

    private void TextBoxControl_HandleCreated(object sender, EventArgs e)
    {
      if (this.adapter != null)
        return;
      this.adapter = new NativeWindowAdapter(this.TextBoxItem.TextBoxControl.Handle);
      this.adapter.AddPostWndProcHook(new WndHookDelegate(this.WndProc));
    }

    private IntPtr WndProc(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam,
      ref bool handled,
      ref bool defWndProc)
    {
      if (msg == 770)
        this.OnPasted();
      return IntPtr.Zero;
    }

    private void OnPasted()
    {
      if (this.Pasted == null)
        return;
      this.Pasted((object) this, EventArgs.Empty);
    }

    protected override void DisposeManagedResources()
    {
      if (this.adapter != null)
        this.adapter.Dispose();
      this.TextBoxItem.TextBoxControl.HandleCreated -= new EventHandler(this.TextBoxControl_HandleCreated);
      base.DisposeManagedResources();
    }
  }
}
