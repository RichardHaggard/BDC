// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuCustomDateItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class FilterMenuCustomDateItem : RadMenuItemBase
  {
    private RadCheckBoxElement checkBoxElement = new RadCheckBoxElement();
    private FilterDescriptor filterDescriptor;

    public override string Text
    {
      get
      {
        return this.checkBoxElement.Text;
      }
      set
      {
        this.checkBoxElement.Text = value;
      }
    }

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

    public FilterDescriptor FilterDescriptor
    {
      get
      {
        return this.filterDescriptor;
      }
      set
      {
        this.filterDescriptor = value;
      }
    }

    public FilterMenuCustomDateItem(string text, FilterDescriptor filterDescriptor)
    {
      this.checkBoxElement.Text = text;
      this.filterDescriptor = filterDescriptor;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(5, 5, 5, 0);
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBoxElement.IsChecked = false;
      this.checkBoxElement.ToggleStateChanged += new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
      this.Children.Add((RadElement) this.checkBoxElement);
    }

    public event StateChangedEventHandler ToggleStateChanged;

    private void OnToggleStateChanged(StateChangedEventArgs args)
    {
      if (this.ToggleStateChanged == null)
        return;
      this.ToggleStateChanged((object) this, args);
    }

    private void checkBoxElement_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.OnToggleStateChanged(args);
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
      this.checkBoxElement.Arrange(new RectangleF(clientRectangle.X, 0.0f, clientRectangle.Width, this.checkBoxElement.DesiredSize.Height));
      return finalSize;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.checkBoxElement.ToggleStateChanged -= new StateChangedEventHandler(this.checkBoxElement_ToggleStateChanged);
    }
  }
}
