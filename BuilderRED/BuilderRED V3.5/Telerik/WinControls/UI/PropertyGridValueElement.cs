// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridValueElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridValueElement : PropertyGridContentElement
  {
    public static RadProperty ContainsErrorProperty = RadProperty.Register("ContainsError", typeof (bool), typeof (PropertyGridValueElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsModifiedProperty = RadProperty.Register("IsModified", typeof (bool), typeof (PropertyGridValueElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static PropertyGridValueElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridValueElementStateManager(), typeof (PropertyGridValueElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
      this.NotifyParentOnMouseInput = true;
      this.ClipDrawing = true;
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.DrawBorder = false;
      this.DrawFill = false;
    }

    public virtual void Synchronize()
    {
      this.Text = ((PropertyGridItem) this.VisualItem.Data).FormattedValue;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float valueColumnWidth = (float) this.PropertyGridTableElement.ValueColumnWidth;
      availableSize.Width = Math.Min(availableSize.Width, valueColumnWidth);
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width = Math.Min(valueColumnWidth, availableSize.Width);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      PropertyGridItemElement visualItem = this.VisualItem as PropertyGridItemElement;
      BaseInputEditor baseInputEditor = (BaseInputEditor) null;
      if (visualItem != null && visualItem.Editor != null)
        baseInputEditor = visualItem.Editor as BaseInputEditor;
      this.Layout.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (baseInputEditor != null && baseInputEditor.EditorElement == child)
        {
          float height1 = child.DesiredSize.Height;
          if (child.StretchVertically)
            height1 = clientRectangle.Height;
          float height2 = Math.Min(height1, clientRectangle.Height);
          RectangleF finalRect = new RectangleF(clientRectangle.X, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) height2) / 2.0), clientRectangle.Width, height2);
          child.Arrange(finalRect);
        }
        else
          child.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height));
      }
      return finalSize;
    }
  }
}
