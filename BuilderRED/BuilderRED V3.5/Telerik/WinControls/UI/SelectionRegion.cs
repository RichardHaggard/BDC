// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectionRegion
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public struct SelectionRegion
  {
    public static readonly SelectionRegion Empty = new SelectionRegion(-1, -1, -1, -1, (VirtualGridViewInfo) null);

    public int Top { get; private set; }

    public int Left { get; private set; }

    public int Bottom { get; private set; }

    public int Right { get; private set; }

    public VirtualGridViewInfo ViewInfo { get; private set; }

    public SelectionRegion(
      int top,
      int left,
      int bottom,
      int right,
      VirtualGridViewInfo viewInfo)
    {
      this = new SelectionRegion();
      this.Top = top;
      this.Left = left;
      this.Bottom = bottom;
      this.Right = right;
      this.ViewInfo = viewInfo;
    }

    public bool Contains(int row, int column)
    {
      if (this.ContainsRow(row))
        return this.ContainsColumn(column);
      return false;
    }

    public bool ContainsRow(int row)
    {
      if (this.Top <= row)
        return row <= this.Bottom;
      return false;
    }

    public bool ContainsColumn(int column)
    {
      if (this.Left <= column)
        return column <= this.Right;
      return false;
    }

    public static bool operator ==(SelectionRegion A, SelectionRegion B)
    {
      if (A.Top == B.Top && A.Left == B.Left && (A.Bottom == B.Bottom && A.Right == B.Right))
        return A.ViewInfo == B.ViewInfo;
      return false;
    }

    public static bool operator !=(SelectionRegion A, SelectionRegion B)
    {
      return !(A == B);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is SelectionRegion))
        return false;
      return this == (SelectionRegion) obj;
    }

    public override int GetHashCode()
    {
      return this.Top.GetHashCode() ^ this.Bottom.GetHashCode() ^ this.Left.GetHashCode() ^ this.Right.GetHashCode() ^ this.ViewInfo.GetHashCode();
    }
  }
}
