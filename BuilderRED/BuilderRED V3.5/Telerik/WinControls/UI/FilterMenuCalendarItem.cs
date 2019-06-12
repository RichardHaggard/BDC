// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuCalendarItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class FilterMenuCalendarItem : RadMenuItemBase
  {
    private RadCheckBoxElement checkBoxElement;
    private FilterMenuCalendarElement calendarElement;

    public bool IsChecked
    {
      get
      {
        return this.checkBoxElement.IsChecked;
      }
      set
      {
        this.checkBoxElement.IsChecked = value;
      }
    }

    public FilterMenuCalendarElement CalendarElement
    {
      get
      {
        return this.calendarElement;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(5, 5, 5, 0);
      this.MinSize = new Size(140, 160);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBoxElement = new RadCheckBoxElement();
      this.checkBoxElement.IsChecked = false;
      this.checkBoxElement.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("Filter by specific dates:");
      this.checkBoxElement.ToggleStateChanged += new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.checkBoxElement.Margin = new Padding(0, 10, 0, 5);
      this.Children.Add((RadElement) this.checkBoxElement);
      this.calendarElement = new FilterMenuCalendarElement();
      this.calendarElement.Enabled = false;
      this.Children.Add((RadElement) this.calendarElement);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout ancestor = this.FindAncestor<RadDropDownMenuLayout>();
      if (ancestor != null)
      {
        clientRectangle.X += this.RightToLeft ? 0.0f : ancestor.LeftColumnWidth;
        clientRectangle.Width -= ancestor.LeftColumnWidth;
      }
      foreach (RadElement child in this.Children)
      {
        if (child == this.checkBoxElement)
          child.Arrange(new RectangleF(clientRectangle.X, 0.0f, clientRectangle.Width, this.checkBoxElement.DesiredSize.Height));
        else if (child == this.calendarElement)
        {
          this.calendarElement.Calendar.Size = new Size((int) clientRectangle.Width, (int) ((double) clientRectangle.Height - (double) this.checkBoxElement.DesiredSize.Height));
          this.calendarElement.Arrange(new RectangleF(clientRectangle.X, this.checkBoxElement.DesiredSize.Height, clientRectangle.Width, clientRectangle.Height - this.checkBoxElement.DesiredSize.Height));
        }
        else
          child.Arrange(clientRectangle);
      }
      return finalSize;
    }

    private void checkBoxElement_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.calendarElement.Enabled = this.checkBoxElement.IsChecked;
    }

    protected override void DisposeManagedResources()
    {
      this.checkBoxElement.ToggleStateChanged -= new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      base.DisposeManagedResources();
    }
  }
}
