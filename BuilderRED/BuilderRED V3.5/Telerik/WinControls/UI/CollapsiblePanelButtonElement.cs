// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelButtonElement : LightVisualElement
  {
    private SizeF defaultButtonSize = new SizeF(21f, 21f);
    public static readonly RadProperty ExpandDirectionProperty = RadProperty.Register("ExpandDirection", typeof (RadDirection), typeof (CollapsiblePanelButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Down, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (CollapsiblePanelButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));

    static CollapsiblePanelButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new CollapsiblePanelMouseEventsStateManagerFactory(CollapsiblePanelButtonElement.ExpandDirectionProperty, CollapsiblePanelButtonElement.IsExpandedProperty), typeof (CollapsiblePanelButtonElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Shape = (ElementShape) new RoundRectShape(16);
      this.TextImageRelation = TextImageRelation.ImageAboveText;
      this.ImageAlignment = ContentAlignment.MiddleCenter;
    }

    public SizeF DefaultButtonSize
    {
      get
      {
        return this.defaultButtonSize;
      }
      set
      {
        this.defaultButtonSize = value;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.IsMouseDown = false;
    }

    protected override SizeF MeasureOverride(SizeF finalSize)
    {
      base.MeasureOverride(finalSize);
      return this.defaultButtonSize;
    }
  }
}
