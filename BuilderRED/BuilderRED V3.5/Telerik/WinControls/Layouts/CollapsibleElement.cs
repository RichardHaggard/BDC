// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.CollapsibleElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Layouts
{
  public abstract class CollapsibleElement : RadItem
  {
    private int collapseStep = 1;
    private bool allowCollapsed = true;
    protected bool invalidateCollapsableChildrenCollection = true;
    public IList<CollapsibleElement> collapsableChildren = (IList<CollapsibleElement>) new List<CollapsibleElement>();
    private SizeF sizeAfterCollapsing;
    private SizeF sizeBeforeCollapsing;

    protected void ResetStateSizes()
    {
      for (int index = 0; index < this.CollapseMaxSteps; ++index)
      {
        if (this.CanCollapseToStep(index + 1))
          this.CollapseElementToStep(index + 1);
      }
      for (int collapseStep = this.CollapseMaxSteps - 1; collapseStep > 0; --collapseStep)
      {
        this.ExpandElementToStep(collapseStep);
        this.CollapseStep = collapseStep;
      }
    }

    public abstract bool ExpandElementToStep(int collapseStep);

    public abstract bool CollapseElementToStep(int collapseStep);

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract int CollapseMaxSteps { get; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(1)]
    public int CollapseStep
    {
      get
      {
        return this.collapseStep;
      }
      set
      {
        if (value < 1 || value > this.CollapseMaxSteps)
          return;
        this.collapseStep = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool AllowCollapsed
    {
      get
      {
        return this.allowCollapsed;
      }
      set
      {
        this.allowCollapsed = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public SizeF SizeAfterCollapsing
    {
      get
      {
        return this.sizeAfterCollapsing;
      }
      set
      {
        this.sizeAfterCollapsing = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public SizeF SizeBeforeCollapsing
    {
      get
      {
        return this.sizeBeforeCollapsing;
      }
      set
      {
        this.sizeBeforeCollapsing = value;
      }
    }

    protected void FillCollapsableChildrenCollection(RadElement baseItem)
    {
      foreach (RadElement child in baseItem.Children)
      {
        CollapsibleElement adapter = CollapsibleAdapterFactory.CreateAdapter(child);
        if (adapter != null)
          this.collapsableChildren.Add(adapter);
        else
          this.FillCollapsableChildrenCollection(child);
      }
    }

    protected virtual bool CollapseCollection(int nextStep)
    {
      bool flag = false;
      foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
      {
        bool step = collapsableChild.CollapseElementToStep(nextStep);
        flag |= step;
      }
      this.CollapseStep = nextStep;
      return flag;
    }

    protected virtual bool ExpandCollection(int nextStep)
    {
      bool flag = false;
      foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
      {
        bool step = collapsableChild.ExpandElementToStep(nextStep);
        flag |= step;
      }
      this.CollapseStep = nextStep;
      return flag;
    }

    public abstract bool CanCollapseToStep(int nextStep);

    public abstract bool CanExpandToStep(int nextStep);

    protected virtual void InvalidateIfNeeded()
    {
      if (!this.invalidateCollapsableChildrenCollection)
        return;
      this.invalidateCollapsableChildrenCollection = false;
      this.collapsableChildren.Clear();
      this.FillCollapsableChildrenCollection((RadElement) this);
    }
  }
}
