// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RatingBaseVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class RatingBaseVisualElement : RatingVisualItem
  {
    internal abstract Rectangle ClipArea { get; set; }

    internal Rectangle EmptyArea
    {
      get
      {
        return Rectangle.Empty;
      }
    }

    internal Rectangle FullArea
    {
      get
      {
        return new Rectangle(0, 0, this.Size.Width, this.Size.Height);
      }
    }
  }
}
