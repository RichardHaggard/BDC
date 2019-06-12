// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFilterOperationMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadFilterOperationMenuItem : RadMenuItem
  {
    private FilterOperationContext context;

    public RadFilterOperationMenuItem(FilterOperationContext context)
      : base(context.Name)
    {
      this.context = context;
    }

    public FilterOperator Operator
    {
      get
      {
        return this.context.Operator;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = false;
    }

    protected override void OnClick(EventArgs e)
    {
      this.ElementTree.Control.Hide();
      base.OnClick(e);
    }
  }
}
