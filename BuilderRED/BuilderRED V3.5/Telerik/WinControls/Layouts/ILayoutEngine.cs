// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.ILayoutEngine
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Layouts
{
  public interface ILayoutEngine
  {
    Size AvailableSize { get; }

    Padding GetParentPadding();

    Size CheckSize(Size size);

    void SetCoercedSize(Size newCoercedSize);

    Rectangle GetFaceRectangle();

    void PerformLayout(RadElement affectedElement, bool performExplicit);

    void PerformLayoutCore(RadElement affectedElement);

    void PerformParentLayout();

    Size GetPreferredSize(Size proposedSize);

    Size GetPreferredSizeCore(Size proposedSize);

    void InvalidateLayout();

    void LayoutPropertyChanged(RadPropertyChangedEventArgs e);

    bool IsValidWrapElement();

    void SetLayoutInvalidated(bool layoutInvalidated);

    void PerformRegisteredSuspendedLayouts();

    void RegisterChildSuspendedLayout(RadElement element, PerformLayoutType performLayoutType);

    void RegisterLayoutRunning();

    void UnregisterLayoutRunning();

    Point GetTransformationPoint();

    Point TransformByAlignment(Size size, Rectangle withinBounds);

    Size GetBorderOffset();

    Size GetBorderSize();

    Size GetChildBorderSize();

    void InvalidateCachedBorder();
  }
}
