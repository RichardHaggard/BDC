// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RatingVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RatingVisualElement : RatingVisualItem
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (RatingVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private ValueRatingVisualElement valueElement;
    private HoverRatingVisualElement hoverElement;
    private SelectedValueRatingVisualElement selectedValueElement;

    public RatingVisualElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadRatingStateManager(), typeof (RatingVisualElement));
      this.Shape = this.GetShape();
      this.ShouldHandleMouseInput = false;
    }

    protected override void InitializeFields()
    {
      this.MinSize = new Size(25, 25);
      base.InitializeFields();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ValueElement = new ValueRatingVisualElement();
      this.ValueElement.Shape = this.GetShape();
      this.Children.Add((RadElement) this.ValueElement);
      this.HoverElement = new HoverRatingVisualElement();
      this.HoverElement.Shape = this.GetShape();
      this.Children.Add((RadElement) this.HoverElement);
      this.SelectedValueElement = new SelectedValueRatingVisualElement();
      this.SelectedValueElement.Shape = this.GetShape();
      this.Children.Add((RadElement) this.SelectedValueElement);
    }

    public HoverRatingVisualElement HoverElement
    {
      get
      {
        return this.hoverElement;
      }
      set
      {
        this.hoverElement = value;
      }
    }

    public ValueRatingVisualElement ValueElement
    {
      get
      {
        return this.valueElement;
      }
      set
      {
        this.valueElement = value;
      }
    }

    public SelectedValueRatingVisualElement SelectedValueElement
    {
      get
      {
        return this.selectedValueElement;
      }
      set
      {
        this.selectedValueElement = value;
      }
    }

    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(RatingVisualElement.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetValue(RatingVisualElement.IsVerticalProperty, (object) value);
      }
    }

    public override bool IsInRadGridView
    {
      get
      {
        return this.hoverElement.IsInRadGridView;
      }
      set
      {
        this.hoverElement.IsInRadGridView = value;
        this.valueElement.IsInRadGridView = value;
        this.selectedValueElement.IsInRadGridView = value;
      }
    }

    protected virtual ElementShape GetShape()
    {
      return (ElementShape) new StarShape();
    }
  }
}
