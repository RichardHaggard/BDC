// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryDropDown
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadGalleryDropDown : RadDropDownMenu
  {
    private RadElement lastFocused;

    public RadGalleryDropDown(RadGalleryElement ownerElement)
      : base((RadElement) ownerElement)
    {
      this.FadeAnimationType = FadeAnimationType.FadeOut;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadGalleryDropDown).Namespace + ".RadGalleryMenu";
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    public void ClickFocusedItem()
    {
      RadGalleryPopupElement popupElement = this.PopupElement as RadGalleryPopupElement;
      if (popupElement == null)
        return;
      List<RadElement> descendants = popupElement.GetDescendants(new Predicate<RadElement>(this.IsGaleryItem), TreeTraversalMode.BreadthFirst);
      descendants.Sort(new Comparison<RadElement>(this.GalleryItemsComparison));
      for (int index = 0; index < descendants.Count; ++index)
      {
        if (descendants[index].IsMouseOver)
        {
          descendants[index].PerformClick();
          break;
        }
      }
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (this.PopupElement is RadGalleryPopupElement && this.Bounds.Contains(Control.MousePosition))
        return false;
      return base.CanClosePopup(reason);
    }

    public override StyleGroup ResolveStyleGroupForElement(
      StyleGroup styleGroup,
      RadObject element)
    {
      if ((element is RadSplitButtonElement || element is RadDropDownButtonElement) && this.ElementTree.Theme != null)
        return this.ElementTree.Theme.FindStyleGroup(element as IStylableNode);
      return base.ResolveStyleGroupForElement(styleGroup, element);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.lastFocused != null && this.ElementTree.GetElementAtPoint(e.Location) is RadGalleryItem)
      {
        this.lastFocused.IsMouseOver = false;
        this.lastFocused = (RadElement) null;
      }
      base.OnMouseMove(e);
    }

    protected override bool ProcessUpDownNavigationKey(bool isUp)
    {
      RadGalleryPopupElement popupElement = this.PopupElement as RadGalleryPopupElement;
      if (popupElement == null)
        return false;
      List<RadElement> descendants = popupElement.GetDescendants(new Predicate<RadElement>(this.IsGaleryItem), TreeTraversalMode.BreadthFirst);
      descendants.Sort(new Comparison<RadElement>(this.GalleryItemsComparison));
      for (int currentIndex = 0; currentIndex < descendants.Count; ++currentIndex)
      {
        if (descendants[currentIndex].IsMouseOver)
        {
          if (isUp)
            this.FocusItem(descendants[currentIndex], this.FindUpperItem(currentIndex, descendants));
          else
            this.FocusItem(descendants[currentIndex], this.FindLowerItem(currentIndex, descendants));
          return true;
        }
      }
      if (descendants.Count == 0)
        this.FocusItem((RadElement) null, (RadElement) null);
      else
        this.FocusItem((RadElement) null, descendants[0]);
      return true;
    }

    protected override bool ProcessLeftRightNavigationKey(bool isLeft)
    {
      RadGalleryPopupElement popupElement = this.PopupElement as RadGalleryPopupElement;
      if (popupElement == null)
        return false;
      List<RadElement> descendants = popupElement.GetDescendants(new Predicate<RadElement>(this.IsGaleryItem), TreeTraversalMode.BreadthFirst);
      descendants.Sort(new Comparison<RadElement>(this.GalleryItemsComparison));
      for (int index = 0; index < descendants.Count; ++index)
      {
        if (descendants[index].IsMouseOver)
        {
          if (isLeft)
            this.FocusItem(descendants[index], index > 0 ? descendants[index - 1] : (RadElement) null);
          else
            this.FocusItem(descendants[index], index + 1 < descendants.Count ? descendants[index + 1] : (RadElement) null);
          return true;
        }
      }
      if (descendants.Count == 0)
        this.FocusItem((RadElement) null, (RadElement) null);
      else
        this.FocusItem((RadElement) null, isLeft ? descendants[descendants.Count] : descendants[0]);
      return true;
    }

    private void FocusItem(RadElement oldElement, RadElement newElement)
    {
      if (newElement == null)
        return;
      if (oldElement != null)
        oldElement.IsMouseOver = false;
      this.lastFocused = newElement;
      newElement.IsMouseOver = true;
      newElement.Focus();
    }

    private bool IsGaleryItem(RadElement element)
    {
      return element is RadGalleryItem;
    }

    private int GalleryItemsComparison(RadElement e1, RadElement e2)
    {
      if (e1 == e2)
        return 0;
      if (e1 == null)
        return -1;
      if (e2 == null)
        return 1;
      if (e1.ControlBoundingRectangle.Y < e2.ControlBoundingRectangle.Bottom && e1.ControlBoundingRectangle.Bottom > e2.ControlBoundingRectangle.Y)
        return e1.ControlBoundingRectangle.X.CompareTo(e2.ControlBoundingRectangle.X);
      return e1.ControlBoundingRectangle.Y.CompareTo(e2.ControlBoundingRectangle.Y);
    }

    private RadElement FindLowerItem(int currentIndex, List<RadElement> items)
    {
      RadElement radElement1 = items[currentIndex];
      RadElement radElement2 = (RadElement) null;
      double num = double.PositiveInfinity;
      for (int index = currentIndex + 1; index < items.Count; ++index)
      {
        RadElement radElement3 = items[index];
        if (radElement3.ControlBoundingRectangle.Top >= radElement1.ControlBoundingRectangle.Bottom)
        {
          double distance = LayoutUtils.GetDistance(new Point(radElement1.ControlBoundingRectangle.X + radElement1.ControlBoundingRectangle.Width / 2, radElement1.ControlBoundingRectangle.Bottom), new Point(radElement3.ControlBoundingRectangle.X + radElement3.ControlBoundingRectangle.Width / 2, radElement3.ControlBoundingRectangle.Top));
          if (distance < num)
          {
            radElement2 = radElement3;
            num = distance;
          }
        }
      }
      return radElement2;
    }

    private RadElement FindUpperItem(int currentIndex, List<RadElement> items)
    {
      RadElement radElement1 = items[currentIndex];
      RadElement radElement2 = (RadElement) null;
      double num = double.PositiveInfinity;
      for (int index = currentIndex - 1; index >= 0; --index)
      {
        RadElement radElement3 = items[index];
        if (radElement3.ControlBoundingRectangle.Bottom <= radElement1.ControlBoundingRectangle.Top)
        {
          double distance = LayoutUtils.GetDistance(new Point(radElement1.ControlBoundingRectangle.X + radElement1.ControlBoundingRectangle.Width / 2, radElement1.ControlBoundingRectangle.Top), new Point(radElement3.ControlBoundingRectangle.X + radElement3.ControlBoundingRectangle.Width / 2, radElement3.ControlBoundingRectangle.Bottom));
          if (distance < num)
          {
            radElement2 = radElement3;
            num = distance;
          }
        }
      }
      return radElement2;
    }
  }
}
