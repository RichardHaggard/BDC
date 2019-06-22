﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualEffects.GridExpandAnimationFade
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.VisualEffects
{
  public class GridExpandAnimationFade : GridExpandAnimation
  {
    public GridExpandAnimationFade(GridTableElement tableElement)
      : base(tableElement)
    {
    }

    public override void Expand(GridViewRowInfo rowInfo, float maxOffset, int rowIndex)
    {
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting = new GridExpandAnimationFade.FadeAnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 0.0, 9, 30);
          animatedPropertySetting.RemoveAfterApply = true;
          if (index == visualRows.Count - 1)
          {
            animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.ExpandSetting_AnimationFinished);
            animatedPropertySetting.RowIndex = rowIndex;
          }
          animatedPropertySetting.ApplyValue((RadObject) gridRowElement);
        }
      }
    }

    public override void Collapse(GridViewRowInfo rowInfo, float maxOffset, int rowIndex)
    {
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting = new GridExpandAnimationFade.FadeAnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 0.0, 9, 30);
          animatedPropertySetting.RemoveAfterApply = true;
          if (index == visualRows.Count - 1)
          {
            animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.CollapseSetting_AnimationFinished);
            animatedPropertySetting.RowIndex = rowIndex;
          }
          animatedPropertySetting.ApplyValue((RadObject) gridRowElement);
        }
      }
    }

    private void ExpandSetting_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.OnUpdateViewNeeded(EventArgs.Empty);
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting1 = sender as GridExpandAnimationFade.FadeAnimatedPropertySetting;
      int rowIndex = animatedPropertySetting1.RowIndex;
      animatedPropertySetting1.AnimationFinished -= new AnimationFinishedEventHandler(this.ExpandSetting_AnimationFinished);
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting2 = new GridExpandAnimationFade.FadeAnimatedPropertySetting(VisualElement.OpacityProperty, (object) 0.0, (object) 1.0, 9, 30);
          animatedPropertySetting2.RemoveAfterApply = true;
          animatedPropertySetting2.ApplyValue((RadObject) gridRowElement);
        }
      }
    }

    private void CollapseSetting_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.OnUpdateViewNeeded(EventArgs.Empty);
      IList<GridRowElement> visualRows = this.TableElement.VisualRows;
      GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting1 = sender as GridExpandAnimationFade.FadeAnimatedPropertySetting;
      int rowIndex = animatedPropertySetting1.RowIndex;
      animatedPropertySetting1.AnimationFinished -= new AnimationFinishedEventHandler(this.CollapseSetting_AnimationFinished);
      for (int index = rowIndex; index < visualRows.Count; ++index)
      {
        GridRowElement gridRowElement = visualRows[index];
        if (gridRowElement.Visibility != ElementVisibility.Hidden)
        {
          GridExpandAnimationFade.FadeAnimatedPropertySetting animatedPropertySetting2 = new GridExpandAnimationFade.FadeAnimatedPropertySetting(VisualElement.OpacityProperty, (object) 0.0, (object) 1.0, 9, 30);
          animatedPropertySetting2.RemoveAfterApply = true;
          animatedPropertySetting2.ApplyValue((RadObject) gridRowElement);
        }
      }
    }

    protected class FadeAnimatedPropertySetting : AnimatedPropertySetting
    {
      private int rowIndex;

      public FadeAnimatedPropertySetting(
        RadProperty property,
        object animationStartValue,
        object animationEndValue,
        int numFrames,
        int interval)
        : base(property, animationStartValue, animationEndValue, numFrames, interval)
      {
      }

      public int RowIndex
      {
        get
        {
          return this.rowIndex;
        }
        set
        {
          this.rowIndex = value;
        }
      }
    }
  }
}