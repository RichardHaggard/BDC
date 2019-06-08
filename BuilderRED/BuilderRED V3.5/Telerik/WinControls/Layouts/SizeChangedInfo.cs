// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.SizeChangedInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Layouts
{
  public class SizeChangedInfo
  {
    private RadElement _element;
    private bool _heightChanged;
    private Size _previousSize;
    private bool _widthChanged;
    internal SizeChangedInfo Next;

    public SizeChangedInfo(
      RadElement element,
      Size previousSize,
      bool widthChanged,
      bool heightChanged)
    {
      this._element = element;
      this._previousSize = previousSize;
      this._widthChanged = widthChanged;
      this._heightChanged = heightChanged;
    }

    internal void Update(bool widthChanged, bool heightChanged)
    {
      this._widthChanged |= widthChanged;
      this._heightChanged |= heightChanged;
    }

    internal RadElement Element
    {
      get
      {
        return this._element;
      }
    }

    public bool HeightChanged
    {
      get
      {
        return this._heightChanged;
      }
    }

    public Size PreviousSize
    {
      get
      {
        return this._previousSize;
      }
    }

    public bool WidthChanged
    {
      get
      {
        return this._widthChanged;
      }
    }
  }
}
