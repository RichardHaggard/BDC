// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorRequiredEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class EditorRequiredEventArgs : EventArgs
  {
    private Type editorType;
    private IValueEditor editor;

    public EditorRequiredEventArgs()
      : this((Type) null)
    {
    }

    public EditorRequiredEventArgs(Type editorType)
    {
      this.EditorType = editorType;
    }

    internal EditorRequiredEventArgs(Type editorType, IInputEditor defaultEditor)
    {
      this.EditorType = editorType;
      this.Editor = (IValueEditor) defaultEditor;
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

    public IValueEditor Editor
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
  }
}
