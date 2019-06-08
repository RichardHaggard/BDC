// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuTextBoxItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class FilterMenuTextBoxItem : RadMenuHostItem
  {
    public FilterMenuTextBoxItem()
      : base((Control) new RadTextBox())
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = false;
      this.Padding = new Padding(5, 5, 5, 0);
    }

    public RadTextBox TextBox
    {
      get
      {
        return (RadTextBox) this.HostedControl;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.TextBox.NullText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuSearchBoxText");
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width = 0.0f;
      sizeF.Height += (float) this.Padding.Vertical;
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      foreach (RadElement child in this.Children)
      {
        RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
        if (child is RadHostItem)
        {
          finalRect = this.GetClientRectangle(finalSize);
          RadDropDownMenuLayout ancestor = this.FindAncestor<RadDropDownMenuLayout>();
          if (ancestor != null)
          {
            finalRect.X += this.RightToLeft ? 0.0f : ancestor.LeftColumnWidth;
            finalRect.Width -= ancestor.LeftColumnWidth;
          }
          this.TextBox.Size = finalRect.Size.ToSize();
        }
        child.Arrange(finalRect);
      }
      return finalSize;
    }
  }
}
