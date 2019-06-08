// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsableButtonAdapter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CollapsableButtonAdapter : CollapsibleElement
  {
    public RadButtonElement button;

    public CollapsableButtonAdapter(RadButtonElement button)
    {
      this.button = button;
    }

    public override bool ExpandElementToStep(int nextStep)
    {
      bool flag = false;
      switch (nextStep)
      {
        case 1:
          flag = this.ChangeVisibility(true, false) | this.ChangeVisibility(true, true);
          break;
        case 2:
          flag = this.ChangeVisibility(true, true);
          break;
        case 3:
          flag = this.ChangeVisibility(false, true);
          break;
      }
      this.CollapseStep = nextStep;
      return flag;
    }

    public override bool CollapseElementToStep(int nextStep)
    {
      bool flag = false;
      switch (nextStep)
      {
        case 2:
          flag = this.ChangeVisibility(false, false);
          break;
        case 3:
          flag = this.ChangeVisibility(false, true);
          break;
      }
      this.CollapseStep = nextStep;
      return flag;
    }

    private bool ChangeVisibility(bool display, bool changeImages)
    {
      return this.ChangeItemVisibility(display, changeImages);
    }

    private bool ChangeItemVisibility(bool display, bool changeImages)
    {
      bool flag = false;
      if (this.button == null || !changeImages && this.button.DisplayStyle != DisplayStyle.ImageAndText || this.button.Children.Count < 2)
        return flag;
      ImageAndTextLayoutPanel child = this.button.Children[1] as ImageAndTextLayoutPanel;
      if (child == null || child.Children.Count < 2)
        return flag;
      if (!changeImages && this.ChangeTextVisibility(child, display))
        flag = true;
      if (changeImages && this.ChangeImages(display))
        flag = true;
      return flag;
    }

    private bool ChangeTextVisibility(ImageAndTextLayoutPanel imageAndTextPanel, bool display)
    {
      if (imageAndTextPanel.DisplayStyle != DisplayStyle.ImageAndText || this.button.Image == null && this.button.ImageIndex == -1 && this.button.ImageKey == string.Empty)
        return false;
      RadElement child = imageAndTextPanel.Children[1];
      if (display && child.Visibility == ElementVisibility.Collapsed)
      {
        child.Visibility = ElementVisibility.Visible;
        child.InvalidateMeasure();
        return true;
      }
      if (display || child.Visibility != ElementVisibility.Visible)
        return false;
      child.Visibility = ElementVisibility.Collapsed;
      child.InvalidateMeasure();
      return true;
    }

    private bool HasNoButtonSmallImages()
    {
      if (this.button.SmallImage == null && this.button.SmallImageIndex == -1)
        return this.button.SmallImageKey == string.Empty;
      return false;
    }

    private bool ChangeImages(bool display)
    {
      if (this.HasNoButtonSmallImages())
        return false;
      if (!display)
      {
        this.button.UseSmallImageList = true;
        this.PreserveOldImage(this.button);
        if (this.button.SmallImage != null)
          this.button.Image = this.button.SmallImage;
        if (this.button.SmallImageIndex != -1)
          this.button.ImageIndex = this.button.SmallImageIndex;
        if (this.button.SmallImageKey != string.Empty)
          this.button.ImageKey = this.button.SmallImageKey;
      }
      else
      {
        this.button.UseSmallImageList = false;
        this.RestoreOldImage(this.button);
      }
      this.button.InvalidateMeasure();
      this.button.ImagePrimitive.InvalidateMeasure();
      this.button.UpdateLayout();
      return true;
    }

    private void PreserveOldImage(RadButtonElement button)
    {
      if (button.Image != null && button.SmallImage != button.Image)
      {
        int num1 = (int) button.SetValue(RadButtonElement.LargeImageProperty, (object) button.Image);
      }
      if (button.ImageIndex != -1)
      {
        int num2 = (int) button.SetValue(RadButtonElement.LargeImageIndexProperty, (object) button.ImageIndex);
      }
      if (!(button.ImageKey != string.Empty))
        return;
      int num3 = (int) button.SetValue(RadButtonElement.LargeImageKeyProperty, (object) button.ImageKey);
    }

    private void RestoreOldImage(RadButtonElement button)
    {
      if (button.LargeImage != null)
      {
        button.Image = button.LargeImage;
        button.ImagePrimitive.InvalidateMeasure();
        int num = (int) button.SetValue(RadButtonElement.LargeImageProperty, (object) null);
      }
      if (button.LargeImageIndex != -1)
      {
        button.ImageIndex = button.LargeImageIndex;
        button.ImagePrimitive.InvalidateMeasure();
        int num = (int) button.SetValue(RadButtonElement.LargeImageIndexProperty, (object) -1);
      }
      if (!(button.LargeImageKey != string.Empty))
        return;
      button.ImageKey = button.LargeImageKey;
      button.ImagePrimitive.InvalidateMeasure();
      int num1 = (int) button.SetValue(RadButtonElement.LargeImageKeyProperty, (object) string.Empty);
    }

    public override bool CanExpandToStep(int nextStep)
    {
      if (nextStep == this.CollapseStep && nextStep != this.CollapseMaxSteps)
        return false;
      switch (nextStep)
      {
        case 1:
          return true;
        case 2:
          if (this.button.Children.Count < 2)
            return false;
          ImageAndTextLayoutPanel child = this.button.Children[1] as ImageAndTextLayoutPanel;
          return child != null && child.Children.Count >= 2 && (this.button.DisplayStyle == DisplayStyle.ImageAndText && this.button.TextImageRelation != TextImageRelation.ImageAboveText) && this.button.TextImageRelation != TextImageRelation.TextAboveImage;
        case 3:
          if (this.button.SmallImage == null && this.button.SmallImageIndex == -1)
            return !(this.button.SmallImageKey == string.Empty);
          return true;
        default:
          return false;
      }
    }

    public override bool CanCollapseToStep(int nextStep)
    {
      if (nextStep < this.CollapseStep)
        return false;
      switch (nextStep)
      {
        case 1:
          return true;
        case 2:
          if (this.button.Children.Count < 2)
            return false;
          ImageAndTextLayoutPanel child = this.button.Children[1] as ImageAndTextLayoutPanel;
          return child != null && child.Children.Count >= 2 && this.button.DisplayStyle == DisplayStyle.ImageAndText;
        case 3:
          return this.button.SmallImage != null || this.button.SmallImageIndex != -1 || !(this.button.SmallImageKey == string.Empty);
        default:
          return false;
      }
    }

    public override int CollapseMaxSteps
    {
      get
      {
        return 3;
      }
    }

    public SizeF CurrentSize
    {
      get
      {
        return this.button.DesiredSize;
      }
    }
  }
}
