// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHyperlinkCellContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GridHyperlinkCellContentElement : LightVisualElement
  {
    public static RadProperty VisitedProperty = RadProperty.Register(nameof (Visited), typeof (bool), typeof (GridHyperlinkCellContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static GridHyperlinkCellContentElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridHyperlinkCellContentElementStateManager(), typeof (GridHyperlinkCellContentElement));
    }

    public GridHyperlinkCellContentElement()
    {
      this.MouseLeave += new EventHandler(this.GridHyperlinkCellContentElement_MouseLeave);
    }

    protected override void DisposeManagedResources()
    {
      this.MouseLeave -= new EventHandler(this.GridHyperlinkCellContentElement_MouseLeave);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      int num1 = (int) this.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleLeft);
      int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ContentAlignment.MiddleCenter);
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating if the link has been opened by the user.")]
    public virtual bool Visited
    {
      get
      {
        return (bool) this.GetValue(GridHyperlinkCellContentElement.VisitedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridHyperlinkCellContentElement.VisitedProperty, (object) value);
      }
    }

    private void GridHyperlinkCellContentElement_MouseLeave(object sender, EventArgs e)
    {
      this.IsMouseDown = false;
    }
  }
}
