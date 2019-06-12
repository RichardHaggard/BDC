// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridHelpElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridHelpElement : LightVisualElement
  {
    private PropertyGridHelpTitleElement titleElement;
    private PropertyGridHelpContentElement contentElement;
    private float helpElementHeight;

    static PropertyGridHelpElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (PropertyGridHelpElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.DrawFill = true;
      this.DrawBorder = true;
      this.StretchHorizontally = true;
      this.helpElementHeight = 80f;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.titleElement = new PropertyGridHelpTitleElement();
      this.contentElement = new PropertyGridHelpContentElement();
      this.Children.Add((RadElement) this.titleElement);
      this.Children.Add((RadElement) this.contentElement);
    }

    public string TitleText
    {
      get
      {
        return this.titleElement.Text;
      }
      set
      {
        this.titleElement.Text = value;
      }
    }

    public string ContentText
    {
      get
      {
        return this.contentElement.Text;
      }
      set
      {
        this.contentElement.Text = value;
      }
    }

    public PropertyGridHelpTitleElement HelpTitleElement
    {
      get
      {
        return this.titleElement;
      }
    }

    public PropertyGridHelpContentElement HelpContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    public float HelpElementHeight
    {
      get
      {
        return this.helpElementHeight * this.DpiScaleFactor.Height;
      }
      set
      {
        this.helpElementHeight = value;
        this.InvalidateMeasure(true);
        this.SplitElement.PropertyTableElement.UseCachedValues = true;
        this.SplitElement.PropertyTableElement.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
        this.SplitElement.PropertyTableElement.UseCachedValues = false;
      }
    }

    public PropertyGridSplitElement SplitElement
    {
      get
      {
        return this.FindAncestor<PropertyGridSplitElement>();
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float height = availableSize.Height;
      availableSize.Height = Math.Min(this.HelpElementHeight, availableSize.Height);
      Padding borderThickness = this.GetBorderThickness(false);
      availableSize.Width -= (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      availableSize.Height -= (float) (this.Padding.Vertical + borderThickness.Vertical);
      SizeF empty = SizeF.Empty;
      this.titleElement.Measure(availableSize);
      availableSize.Height -= this.titleElement.DesiredSize.Height;
      this.contentElement.Measure(availableSize);
      empty.Width = Math.Max(this.titleElement.DesiredSize.Width, this.contentElement.DesiredSize.Width);
      empty.Height = Math.Min(this.HelpElementHeight, height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.titleElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, this.titleElement.DesiredSize.Height));
      this.contentElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y + this.titleElement.DesiredSize.Height, clientRectangle.Width, clientRectangle.Height - this.titleElement.DesiredSize.Height));
      return finalSize;
    }
  }
}
