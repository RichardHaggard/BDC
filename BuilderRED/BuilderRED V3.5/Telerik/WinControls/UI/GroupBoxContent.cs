// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupBoxContent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GroupBoxContent : GroupBoxVisualElement
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Class = nameof (GroupBoxContent);
    }

    public override string ToString()
    {
      return nameof (GroupBoxContent);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        empty.Width = Math.Max(child.DesiredSize.Width, empty.Width);
        empty.Height = Math.Max(child.DesiredSize.Height, empty.Height);
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      return finalSize;
    }
  }
}
