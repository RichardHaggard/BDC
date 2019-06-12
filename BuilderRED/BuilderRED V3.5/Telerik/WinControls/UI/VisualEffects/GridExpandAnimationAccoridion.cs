// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualEffects.GridExpandAnimationAccoridion
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI.VisualEffects
{
  public class GridExpandAnimationAccoridion : GridExpandAnimation
  {
    public GridExpandAnimationAccoridion(GridTableElement tableElement)
      : base(tableElement)
    {
    }

    public override void Expand(GridViewRowInfo rowInfo, float maxOffset, int rowIndex)
    {
      this.IsAnimating = true;
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 0.0, 9, 30)
          {
            ApplyEasingType = RadEasingType.OutQuint,
            RemoveAfterApply = true,
            ApplyDelay = ((visualRows.Count - index) * 25)
          }.ApplyValue((RadObject) gridRowElement);
          AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, 0.0f), (object) new SizeF(0.0f, maxOffset), 9, 30);
          animatedPropertySetting.RemoveAfterApply = true;
          animatedPropertySetting.ApplyEasingType = RadEasingType.OutQuad;
          animatedPropertySetting.ApplyDelay = (visualRows.Count - index) * 25;
          if (index == visualRows.Count - 1)
            animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.ExpandSetting_AnimationFinished);
          animatedPropertySetting.ApplyValue((RadObject) gridRowElement);
        }
      }
    }

    public override void Collapse(GridViewRowInfo rowInfo, float maxOffset, int rowIndex)
    {
      this.IsAnimating = true;
      this.OnUpdateViewNeeded(EventArgs.Empty);
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 0.0, (object) 1.0, 9, 30)
          {
            ApplyEasingType = RadEasingType.OutQuint,
            RemoveAfterApply = true,
            ApplyDelay = ((index - rowIndex) * 25)
          }.ApplyValue((RadObject) gridRowElement);
          AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, maxOffset), (object) new SizeF(0.0f, 0.0f), 9, 30);
          animatedPropertySetting.ApplyEasingType = RadEasingType.OutQuad;
          animatedPropertySetting.RemoveAfterApply = true;
          animatedPropertySetting.ApplyDelay = (index - rowIndex) * 25;
          animatedPropertySetting.ApplyValue((RadObject) gridRowElement);
          if (index == visualRows.Count - 1)
            animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.CollapseSetting_AnimationFinished);
        }
      }
    }

    private void ExpandSetting_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.OnUpdateViewNeeded(EventArgs.Empty);
      (sender as AnimatedPropertySetting).AnimationFinished -= new AnimationFinishedEventHandler(this.ExpandSetting_AnimationFinished);
    }

    private void CollapseSetting_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.TableElement.ViewElement.Invalidate();
      (sender as AnimatedPropertySetting).AnimationFinished -= new AnimationFinishedEventHandler(this.ExpandSetting_AnimationFinished);
    }
  }
}
