// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewItemEditedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewItemEditedEventArgs : RadGanttViewEventArgs
  {
    private IInputEditor editor;
    private bool commit;

    public GanttViewItemEditedEventArgs(GanttViewDataItem item, IInputEditor editor, bool commit)
      : base(item)
    {
      this.editor = editor;
      this.commit = commit;
    }

    public bool Commit
    {
      get
      {
        return this.commit;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }
  }
}
