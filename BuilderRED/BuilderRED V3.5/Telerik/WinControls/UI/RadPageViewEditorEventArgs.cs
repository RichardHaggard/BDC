// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewEditorEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadPageViewEditorEventArgs : RadPageViewEventArgs
  {
    private IInputEditor activeEditor;

    public RadPageViewEditorEventArgs(RadPageViewPage page, IInputEditor activeEditor)
      : base(page)
    {
      this.activeEditor = activeEditor;
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    public object Value
    {
      get
      {
        return this.activeEditor.Value;
      }
      set
      {
        this.activeEditor.Value = value;
      }
    }
  }
}
