// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeImageElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TreeNodeImageElement : LightVisualElement
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register("IsSelected", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentProperty = RadProperty.Register("IsCurrent", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsRootNodeProperty = RadProperty.Register("IsRootNode", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register("HotTracking", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HasChildrenProperty = RadProperty.Register("HasChildren", typeof (bool), typeof (TreeNodeImageElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.FindAncestor<TreeNodeElement>();
      }
    }

    static TreeNodeImageElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TreeNodeImageElementStateManager(), typeof (TreeNodeImageElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "ImageElement";
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
      int num1 = (int) this.SetDefaultValueOverride(LightVisualElement.DrawFillProperty, (object) false);
      int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) false);
      int num3 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageLayoutProperty, (object) ImageLayout.Center);
    }

    public virtual void Synchronize()
    {
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement == null)
        return;
      int num1 = (int) this.SetValue(TreeNodeImageElement.IsSelectedProperty, (object) nodeElement.IsSelected);
      int num2 = (int) this.SetValue(TreeNodeImageElement.IsCurrentProperty, (object) nodeElement.IsCurrent);
      int num3 = (int) this.SetValue(TreeNodeImageElement.IsExpandedProperty, (object) nodeElement.IsExpanded);
      int num4 = (int) this.SetValue(TreeNodeImageElement.IsRootNodeProperty, (object) nodeElement.IsRootNode);
      int num5 = (int) this.SetValue(TreeNodeImageElement.HotTrackingProperty, (object) nodeElement.HotTracking);
      int num6 = (int) this.SetValue(TreeNodeImageElement.HasChildrenProperty, (object) nodeElement.HasChildren);
      if (nodeElement.Data.Image != null)
      {
        this.Image = nodeElement.Data.Image;
      }
      else
      {
        int num7 = (int) this.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.Local);
      }
    }
  }
}
