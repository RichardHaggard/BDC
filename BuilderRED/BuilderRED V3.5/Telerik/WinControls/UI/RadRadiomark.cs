// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRadiomark
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadRadiomark : LightVisualElement
  {
    public static RadProperty CheckStateProperty = RadProperty.Register(nameof (CheckState), typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (RadRadiomark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsImageProperty = RadProperty.Register("IsImageElement", typeof (bool), typeof (RadRadiomark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsCheckmarkProperty = RadProperty.Register("IsCheckElement", typeof (bool), typeof (RadRadiomark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private ImagePrimitive imageElement;
    private RadioPrimitive checkElement;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool VsbVisible
    {
      get
      {
        return false;
      }
    }

    protected internal RadioPrimitive CheckElement
    {
      get
      {
        return this.checkElement;
      }
    }

    protected internal ImagePrimitive ImageElement
    {
      get
      {
        return this.imageElement;
      }
    }

    public Telerik.WinControls.Enumerations.ToggleState CheckState
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(RadRadiomark.CheckStateProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRadiomark.CheckStateProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Shape = (ElementShape) new EllipseShape();
    }

    private void AsureImageAndCheckElements()
    {
      if (this.imageElement == null || this.checkElement == null)
      {
        foreach (RadElement child in this.GetChildren(ChildrenListOptions.Normal))
        {
          if ((bool) child.GetValue(RadRadiomark.IsImageProperty))
            this.imageElement = child as ImagePrimitive;
          else if ((bool) child.GetValue(RadRadiomark.IsCheckmarkProperty))
            this.checkElement = child as RadioPrimitive;
        }
      }
      this.SetCheckState();
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      this.AsureImageAndCheckElements();
      base.OnChildrenChanged(child, changeOperation);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadRadiomark.CheckStateProperty)
        this.SetCheckState();
      if (e.Property == LightVisualElement.ImageProperty)
      {
        this.imageElement.Image = (Image) e.NewValue;
        this.SetCheckState();
      }
      if (e.Property == LightVisualElement.ImageIndexProperty)
      {
        this.imageElement.ImageIndex = (int) e.NewValue;
        this.SetCheckState();
      }
      if (e.Property != LightVisualElement.ImageKeyProperty)
        return;
      this.imageElement.ImageKey = (string) e.NewValue;
      this.SetCheckState();
    }

    protected virtual void SetCheckState()
    {
      if (this.CheckState == Telerik.WinControls.Enumerations.ToggleState.On || this.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
      {
        if (this.imageElement != null && !this.imageElement.IsEmpty)
          this.imageElement.Visibility = ElementVisibility.Visible;
        else if (this.checkElement != null && (this.imageElement == null || this.imageElement.IsEmpty))
          this.checkElement.Visibility = ElementVisibility.Visible;
        this.StartRippleAnimation();
      }
      else
      {
        if (this.imageElement != null && !this.imageElement.IsEmpty)
          this.imageElement.Visibility = ElementVisibility.Hidden;
        if (this.checkElement == null)
          return;
        this.checkElement.Visibility = ElementVisibility.Hidden;
      }
    }

    private void StartRippleAnimation()
    {
      if (this.ElementTree == null || this.ElementTree.Control == null || !this.ElementTree.Control.ContainsFocus)
        return;
      Rectangle boundingRectangle = this.ControlBoundingRectangle;
      this.StartRippleAnimation(new MouseEventArgs(MouseButtons.Left, 1, boundingRectangle.X + boundingRectangle.Width / 2, boundingRectangle.Y + boundingRectangle.Height / 2, 0));
    }
  }
}
