// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExpandableStackLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ExpandableStackLayout : StackLayoutPanel
  {
    public static RadProperty CollapsingEnabledProperty = RadProperty.Register(nameof (CollapsingEnabled), typeof (bool), typeof (ExpandableStackLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    private List<CollapsibleElement> collapsibleChildren;
    private int collapsedElementCount;

    [Description("Gets or sets a boolean value determining whether the layout panel will collapse its content according to its size.")]
    [Category("Behavior")]
    public bool CollapsingEnabled
    {
      get
      {
        return (bool) this.GetValue(ExpandableStackLayout.CollapsingEnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpandableStackLayout.CollapsingEnabledProperty, (object) value);
      }
    }

    protected List<CollapsibleElement> CollapsibleChildren1
    {
      get
      {
        return this.collapsibleChildren;
      }
      set
      {
        this.collapsibleChildren = value;
      }
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      this.collapsibleChildren = (List<CollapsibleElement>) null;
      base.OnChildrenChanged(child, changeOperation);
    }

    private List<CollapsibleElement> CollapsibleChildren
    {
      get
      {
        if (this.collapsibleChildren == null)
        {
          this.collapsibleChildren = new List<CollapsibleElement>();
          RadElementCollection children = this.Children;
          int count = children.Count;
          bool flag = false;
          for (int index = 0; index < count; ++index)
          {
            CollapsibleElement collapsibleElement = children[index] as CollapsibleElement;
            if (collapsibleElement != null)
            {
              this.collapsibleChildren.Add(collapsibleElement);
              RadRibbonBarGroup radRibbonBarGroup = collapsibleElement as RadRibbonBarGroup;
              if (radRibbonBarGroup != null && radRibbonBarGroup.CollapsingPriority > 0)
                flag = true;
            }
          }
          if (flag)
            this.collapsibleChildren.Sort((IComparer<CollapsibleElement>) new ExpandableStackLayout.CollapsableOrderSorter());
        }
        return this.collapsibleChildren;
      }
    }

    private SizeF GetAllElementsSize(SizeF constraint)
    {
      SizeF empty = SizeF.Empty;
      foreach (RadElement child in this.Children)
      {
        child.Measure(constraint);
        empty.Width += child.DesiredSize.Width;
        empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
      }
      return empty;
    }

    public static void InvalidateAll(RadElement baseElement)
    {
      baseElement.InvalidateMeasure();
      baseElement.InvalidateArrange();
      foreach (RadElement child in baseElement.Children)
        ExpandableStackLayout.InvalidateAll(child);
    }

    public static void SetCollapsingEnabled(RadElement rootElement, bool enabled)
    {
      foreach (RadElement enumDescendant in rootElement.EnumDescendants(TreeTraversalMode.BreadthFirst))
      {
        if (enumDescendant is ExpandableStackLayout)
        {
          int num = (int) enumDescendant.SetValue(ExpandableStackLayout.CollapsingEnabledProperty, (object) enabled);
        }
      }
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      if (!this.CollapsingEnabled || this.IsDesignMode || constraint == SizeF.Empty)
        return base.MeasureOverride(constraint);
      ExpandableStackLayout.InvalidateAll((RadElement) this);
      SizeF allElementsSize = this.GetAllElementsSize(constraint);
      int count = this.Children.Count;
      if ((double) allElementsSize.Width >= (double) constraint.Width)
      {
        do
        {
          int possibleCollapseStep;
          CollapsibleElement elementToCollapse;
          do
          {
            possibleCollapseStep = -1;
            elementToCollapse = this.GetElementToCollapse(out possibleCollapseStep);
            if (elementToCollapse != null)
            {
              elementToCollapse.InvalidateMeasure();
              elementToCollapse.Measure(constraint);
              SizeF desiredSize = elementToCollapse.DesiredSize;
              elementToCollapse.SizeBeforeCollapsing = desiredSize;
            }
            else
              goto label_11;
          }
          while (!elementToCollapse.CollapseElementToStep(possibleCollapseStep));
          ExpandableStackLayout.InvalidateAll((RadElement) elementToCollapse);
          elementToCollapse.Measure(constraint);
          SizeF desiredSize1 = elementToCollapse.DesiredSize;
          elementToCollapse.SizeAfterCollapsing = desiredSize1;
          ++this.collapsedElementCount;
        }
        while ((double) this.GetAllElementsSize(constraint).Width > (double) constraint.Width);
      }
      else
      {
        for (int index = 0; index < count; ++index)
        {
          int possibleExpandStep = -1;
          CollapsibleElement elementToExpand = this.GetElementToExpand(allElementsSize.Width, constraint.Width, out possibleExpandStep);
          if (elementToExpand != null && ((double) allElementsSize.Width - (double) elementToExpand.DesiredSize.Width + (double) elementToExpand.SizeBeforeCollapsing.Width <= (double) constraint.Width && elementToExpand.ExpandElementToStep(possibleExpandStep)))
            --this.collapsedElementCount;
        }
      }
label_11:
      ExpandableStackLayout.InvalidateAll((RadElement) this);
      return this.GetAllElementsSize(constraint);
    }

    protected CollapsibleElement GetElementToCollapse(out int possibleCollapseStep)
    {
      possibleCollapseStep = -1;
      IList<CollapsibleElement> collapsibleChildren = (IList<CollapsibleElement>) this.CollapsibleChildren;
      int count = collapsibleChildren.Count;
      if (count == 0)
        return (CollapsibleElement) null;
      int val2 = collapsibleChildren[count - 1].CollapseStep + 1;
      for (int index = 0; index < collapsibleChildren.Count; ++index)
        val2 = Math.Min(collapsibleChildren[index].CollapseStep, val2);
      int nextStep = val2 + 1;
      for (int collapseMaxSteps = collapsibleChildren[0].CollapseMaxSteps; nextStep < collapseMaxSteps; ++nextStep)
      {
        for (int index = count - 1; index >= 0; --index)
        {
          CollapsibleElement collapsibleElement = collapsibleChildren[index];
          if (collapsibleElement.CanCollapseToStep(nextStep))
          {
            possibleCollapseStep = nextStep;
            return collapsibleElement;
          }
        }
      }
      for (int index = count - 1; index >= 0; --index)
      {
        CollapsibleElement collapsibleElement = collapsibleChildren[index];
        if (collapsibleElement.CanCollapseToStep(collapsibleElement.CollapseMaxSteps))
        {
          possibleCollapseStep = collapsibleElement.CollapseMaxSteps;
          return collapsibleElement;
        }
      }
      return (CollapsibleElement) null;
    }

    protected CollapsibleElement GetElementToExpand(
      float sumAllElementsWidth,
      float availableWidth,
      out int possibleExpandStep)
    {
      possibleExpandStep = -1;
      CollapsibleElement collapsibleElement1 = (CollapsibleElement) null;
      IList<CollapsibleElement> collapsibleChildren = (IList<CollapsibleElement>) this.CollapsibleChildren;
      int count = collapsibleChildren.Count;
      if (count == 0)
        return (CollapsibleElement) null;
      for (int nextStep = 3; nextStep >= 0; --nextStep)
      {
        for (int index = 0; index < count; ++index)
        {
          CollapsibleElement collapsibleElement2 = collapsibleChildren[index];
          if (collapsibleElement2.CanExpandToStep(nextStep))
          {
            possibleExpandStep = nextStep;
            return collapsibleElement2;
          }
        }
      }
      return collapsibleElement1;
    }

    public class CollapsableOrderSorter : IComparer<CollapsibleElement>
    {
      public int Compare(CollapsibleElement x, CollapsibleElement y)
      {
        RadRibbonBarGroup radRibbonBarGroup1 = x as RadRibbonBarGroup;
        RadRibbonBarGroup radRibbonBarGroup2 = y as RadRibbonBarGroup;
        if (radRibbonBarGroup1 == null || radRibbonBarGroup2 == null)
          return 0;
        return radRibbonBarGroup1.CollapsingPriority.CompareTo(radRibbonBarGroup2.CollapsingPriority);
      }
    }
  }
}
