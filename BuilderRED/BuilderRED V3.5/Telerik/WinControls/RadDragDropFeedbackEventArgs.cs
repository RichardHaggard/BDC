// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadDragDropFeedbackEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadDragDropFeedbackEventArgs : RadDragEventArgs
  {
    private Image previewImage;
    private Cursor cursor;

    public RadDragDropFeedbackEventArgs(ISupportDrag dragInstance)
      : base(dragInstance)
    {
    }

    public Image PreviewImage
    {
      get
      {
        return this.previewImage;
      }
      set
      {
        this.previewImage = value;
      }
    }

    public Cursor Cursor
    {
      get
      {
        return this.cursor;
      }
      set
      {
        this.cursor = value;
      }
    }
  }
}
