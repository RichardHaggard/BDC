// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.PageHeaderFooter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Export
{
  public class PageHeaderFooter
  {
    public PageHeaderFooter(
      double height,
      Font font,
      string leftText,
      string centerText,
      string rightText,
      bool reverseOnEvenPages)
    {
      this.Height = height;
      this.Font = font;
      this.LeftText = leftText;
      this.CenterText = centerText;
      this.RightText = rightText;
      this.ReverseOnEvenPages = reverseOnEvenPages;
    }

    public double Height { get; set; }

    public Font Font { get; set; }

    public string LeftText { get; set; }

    public string CenterText { get; set; }

    public string RightText { get; set; }

    public bool ReverseOnEvenPages { get; set; }
  }
}
