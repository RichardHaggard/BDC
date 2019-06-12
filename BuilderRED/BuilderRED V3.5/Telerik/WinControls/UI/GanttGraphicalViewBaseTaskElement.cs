// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttGraphicalViewBaseTaskElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GanttGraphicalViewBaseTaskElement : GanttViewVisualElement
  {
    public static RadProperty SelectedProperty = RadProperty.Register("Selected", typeof (bool), typeof (GanttGraphicalViewBaseTaskElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private bool isMouseOverStartResizeRectangle;
    private bool isMouseOverEndResizeRectangle;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = true;
      this.AllowDrag = true;
    }

    static GanttGraphicalViewBaseTaskElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GanttGraphicalViewBaseTaskElementStateManager(), typeof (GanttGraphicalViewBaseTaskElement));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      Point point = this.PointFromControl(e.Location);
      if (point.X < 5 && this.CanBeResized())
      {
        this.ElementTree.Control.Cursor = Cursors.SizeWE;
        this.IsMouseOverStartResizeRectangle = true;
      }
      else if (point.X > this.Size.Width - 5 && this.CanBeResized())
      {
        this.ElementTree.Control.Cursor = Cursors.SizeWE;
        this.IsMouseOverEndResizeRectangle = true;
      }
      else
      {
        this.ElementTree.Control.Cursor = Cursors.Default;
        this.IsMouseOverStartResizeRectangle = false;
        this.IsMouseOverEndResizeRectangle = false;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
      this.IsMouseOverStartResizeRectangle = false;
      this.IsMouseOverEndResizeRectangle = false;
    }

    public bool IsMouseOverStartResizeRectangle
    {
      get
      {
        return this.isMouseOverStartResizeRectangle;
      }
      set
      {
        this.isMouseOverStartResizeRectangle = value;
      }
    }

    public bool IsMouseOverEndResizeRectangle
    {
      get
      {
        return this.isMouseOverEndResizeRectangle;
      }
      set
      {
        this.isMouseOverEndResizeRectangle = value;
      }
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      if (this.ElementState != ElementState.Loaded)
        return false;
      GanttGraphicalViewBaseItemElement parent = this.Parent as GanttGraphicalViewBaseItemElement;
      if (!parent.Data.ReadOnly)
        return !parent.Data.GanttViewElement.ReadOnly;
      return false;
    }

    public virtual bool CanBeResized()
    {
      GanttGraphicalViewBaseItemElement parent = this.Parent as GanttGraphicalViewBaseItemElement;
      if (!parent.Data.ReadOnly)
        return !parent.Data.GanttViewElement.ReadOnly;
      return false;
    }
  }
}
