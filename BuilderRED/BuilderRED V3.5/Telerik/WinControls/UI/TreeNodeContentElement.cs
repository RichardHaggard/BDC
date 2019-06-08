// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class TreeNodeContentElement : TreeViewVisual
  {
    public static RadProperty IsRootNodeProperty = RadProperty.Register("IsRootNode", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsControlInactiveProperty = RadProperty.Register("IsControlInactive", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FullRowSelectProperty = RadProperty.Register("FullRowSelect", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSelectedProperty = RadProperty.Register("IsSelected", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCurrentProperty = RadProperty.Register("IsCurrent", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HotTrackingProperty = RadProperty.Register("HotTracking", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HasChildrenProperty = RadProperty.Register("HasChildren", typeof (bool), typeof (TreeNodeContentElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    private SizeF fullDesiredSize;

    static TreeNodeContentElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TreeNodeContentElementStateManager(), typeof (TreeNodeContentElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.AutoEllipsis = true;
    }

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.FindAncestor<TreeNodeElement>();
      }
    }

    public SizeF FullDesiredSize
    {
      get
      {
        return this.fullDesiredSize;
      }
      set
      {
        this.fullDesiredSize = value;
      }
    }

    public virtual void Synchronize()
    {
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement == null || nodeElement.Data == null)
        return;
      this.Text = nodeElement.Data.Text;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      if (this.fullDesiredSize != SizeF.Empty && (double) this.fullDesiredSize.Width > (double) availableSize.Width)
        sizeF.Width = availableSize.Width;
      return sizeF;
    }
  }
}
