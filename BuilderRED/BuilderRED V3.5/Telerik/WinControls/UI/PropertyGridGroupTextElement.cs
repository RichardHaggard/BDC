﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupTextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupTextElement : PropertyGridTextElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ButtonsLayout.Visibility = ElementVisibility.Collapsed;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return availableSize;
    }
  }
}
