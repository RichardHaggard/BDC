// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelLayoutElement : RadItem
  {
    public static readonly RadProperty ExpandDirectionProperty = RadProperty.Register("ExpandDirection", typeof (RadDirection), typeof (CollapsiblePanelLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Down, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (CollapsiblePanelLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    private ImagePrimitive imagePrimitive;

    public Image Image
    {
      get
      {
        return this.imagePrimitive.Image;
      }
      set
      {
        this.imagePrimitive.Image = value;
      }
    }

    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
    }

    static CollapsiblePanelLayoutElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadCollapsiblePanelElementStateManagerFactory(CollapsiblePanelLayoutElement.ExpandDirectionProperty, CollapsiblePanelLayoutElement.IsExpandedProperty), typeof (CollapsiblePanelLayoutElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldPaint = true;
      this.Visibility = ElementVisibility.Visible;
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      this.imagePrimitive = new ImagePrimitive();
      this.imagePrimitive.ImageLayout = ImageLayout.Zoom;
      this.Children.Add((RadElement) this.imagePrimitive);
      this.imagePrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.imagePrimitive.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
    }
  }
}
