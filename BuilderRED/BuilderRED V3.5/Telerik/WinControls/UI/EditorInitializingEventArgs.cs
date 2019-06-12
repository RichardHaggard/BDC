// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorInitializingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class EditorInitializingEventArgs : CancelEventArgs
  {
    private PropertyDescriptor property;
    private Control editor;
    private System.Type editorType;

    public EditorInitializingEventArgs(
      PropertyDescriptor property,
      Control editor,
      System.Type editorType)
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
      set
      {
        this.editor = value;
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
