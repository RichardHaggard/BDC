﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemEditedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemEditedEventArgs : PropertyGridVisualItemEventArgs
  {
    private IValueEditor editor;
    private bool canceled;

    public PropertyGridItemEditedEventArgs(
      PropertyGridItemElement visualItem,
      IValueEditor editor,
      bool canceled)
      : base((PropertyGridItemElementBase) visualItem)
    {
      this.editor = editor;
      this.canceled = canceled;
    }

    public bool Canceled
    {
      get
      {
        return this.canceled;
      }
    }

    public IValueEditor Editor
    {
      get
      {
        return this.editor;
      }
    }
  }
}
