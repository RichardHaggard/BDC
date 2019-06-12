// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewEditorRequiredEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewEditorRequiredEventArgs : RadGanttViewEventArgs
  {
    private GanttViewTextViewColumn column;
    private IInputEditor editor;
    private Type editorType;

    public GanttViewEditorRequiredEventArgs(
      GanttViewDataItem item,
      GanttViewTextViewColumn column,
      Type editorType)
      : base(item)
    {
      this.column = column;
      this.editorType = editorType;
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
      set
      {
        this.editor = value;
      }
    }

    public Type EditorType
    {
      get
      {
        return this.editorType;
      }
      set
      {
        this.editorType = value;
      }
    }
  }
}
