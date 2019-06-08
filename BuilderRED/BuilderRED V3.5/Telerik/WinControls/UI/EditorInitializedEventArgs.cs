// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorInitializedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class EditorInitializedEventArgs : EventArgs
  {
    private PropertyDescriptor property;
    private Control editor;
    private System.Type editorType;

    public EditorInitializedEventArgs(PropertyDescriptor property, Control editor, System.Type editorType)
    {
      this.property = property;
      this.editor = editor;
      this.editorType = editorType;
    }

    public PropertyDescriptor Property
    {
      get
      {
        return this.property;
      }
    }

    public Control Editor
    {
      get
      {
        return this.editor;
      }
    }

    public System.Type EditorType
    {
      get
      {
        return this.editorType;
      }
    }
  }
}
