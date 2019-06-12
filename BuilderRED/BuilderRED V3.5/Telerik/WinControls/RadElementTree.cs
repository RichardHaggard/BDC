// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElementTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class RadElementTree : IDisposable
  {
    private static int treeInstanceCount;
    private RootRadElement rootElement;
    private IComponentTreeHandler component;
    private string treeName;
    private int layoutSuspendCount;
    private bool disposing;
    private bool isDisposed;

    public RadElementTree(IComponentTreeHandler component)
    {
      this.component = component;
      this.treeName = "VisualTree" + (object) RadElementTree.treeInstanceCount;
      ++RadElementTree.treeInstanceCount;
      if (!RadElementTree.AllowShowEvaluation)
        return;
      this.CheckLicense();
    }

    public virtual void InitializeRootElement()
    {
      if (this.rootElement == null)
      {
        this.rootElement = this.ComponentTreeHandler.CreateRootElement();
        ++this.rootElement.layoutsRunning;
        this.ComponentTreeHandler.InitializeRootElement(this.rootElement);
        this.rootElement.ElementTree = this.component.ElementTree;
        this.rootElement.SaveCurrentStretchModeAsDefault();
        this.rootElement.RaiseTunnelEvent((RadElement) this.rootElement, new RoutedEventArgs(EventArgs.Empty, RadElement.ControlChangedEvent));
        --this.rootElement.layoutsRunning;
      }
      this.ComponentTreeHandler.CreateChildItems((RadElement) this.rootElement);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      this.disposing = true;
      if (disposing)
        this.RootElement.Dispose();
      this.component = (IComponentTreeHandler) null;
      this.disposing = false;
      this.isDisposed = true;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public bool Disposing
    {
      get
      {
        return this.disposing;
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [Description("Gets the RootElement of a Control.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public RootRadElement RootElement
    {
      get
      {
        return this.rootElement;
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control Control
    {
      get
      {
        return this.component as Control;
      }
    }

    public IComponentTreeHandler ComponentTreeHandler
    {
      get
      {
        return this.component;
      }
    }

    public string TreeName
    {
      get
      {
        return this.treeName;
      }
    }

    public bool IsLayoutSuspended
    {
      get
      {
        return this.layoutSuspendCount > 0;
      }
    }

    public T GetElementAtPoint<T>(Point point) where T : class
    {
      return this.GetElementAtPoint(point, (Predicate<RadElement>) (element =>
      {
        if (element.ShouldHandleMouseInput)
          return element is T;
        return false;
      })) as T;
    }

    public RadElement GetElementAtPoint(Point point)
    {
      return this.GetElementAtPoint(point, new Predicate<RadElement>(this.CanHandleMouseInput));
    }

    public RadElement GetElementAtPoint(Point point, Predicate<RadElement> predicate)
    {
      return this.GetElementAtPoint((RadElement) this.RootElement, point, (List<RadElement>) null, predicate);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadElement GetElementAtPoint(
      RadElement parent,
      Point point,
      List<RadElement> foundElements)
    {
      return this.GetElementAtPoint(parent, point, foundElements, new Predicate<RadElement>(this.CanHandleMouseInput));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadElement GetElementAtPoint(
      RadElement parent,
      Point point,
      List<RadElement> foundElements,
      Predicate<RadElement> predicate)
    {
      if (parent.Visibility != ElementVisibility.Visible)
        return (RadElement) null;
      ChildrenListOptions options = ChildrenListOptions.ZOrdered | ChildrenListOptions.ReverseOrder | ChildrenListOptions.IncludeOnlyVisible;
      Rectangle clientRectangle = this.Control.ClientRectangle;
      foreach (RadElement child in parent.GetChildren(options))
      {
        if (child.ElementState == ElementState.Loaded && child.HitTest(point))
        {
          if (foundElements != null && !(child is BorderPrimitive))
            foundElements.Add(child);
          RadElement elementAtPoint = this.GetElementAtPoint(child, point, foundElements, predicate);
          if (elementAtPoint != null)
            return elementAtPoint;
          if (predicate == null || predicate(child))
            return child;
        }
      }
      return (RadElement) null;
    }

    public virtual Size GetPreferredSize(Size proposedSize, Size sizeConstraints)
    {
      if (this.IsHorizontallyStretchable())
      {
        if (sizeConstraints.Width > 0)
          proposedSize.Width = sizeConstraints.Width;
        else if (!this.ComponentTreeHandler.IsDesignMode)
        {
          sizeConstraints.Width = 0;
          proposedSize.Width = sizeConstraints.Width;
        }
      }
      if (this.IsVerticallyStretchable())
      {
        if (sizeConstraints.Height > 0)
          proposedSize.Height = sizeConstraints.Height;
        else if (!this.ComponentTreeHandler.IsDesignMode)
        {
          sizeConstraints.Height = 0;
          proposedSize.Height = sizeConstraints.Height;
        }
      }
      if (!this.ComponentTreeHandler.LayoutManager.IsUpdating)
      {
        if (this.RootElement.NeverMeasured || this.RootElement.NeverArranged)
        {
          Rectangle rectangle = new Rectangle(this.Control.Location, proposedSize);
          this.PerformInnerLayout(true, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
        else
        {
          this.RootElement.DisableControlSizeSet = true;
          this.RootElement.Measure((SizeF) proposedSize);
          this.RootElement.UpdateLayout();
          this.RootElement.DisableControlSizeSet = false;
        }
      }
      Size size = Size.Round(this.RootElement.DesiredSize);
      if (this.RootElement.StretchHorizontally)
        size.Width = proposedSize.Width;
      if (this.RootElement.StretchVertically)
        size.Height = proposedSize.Height;
      return size;
    }

    public void PerformLayout()
    {
      Rectangle bounds = this.Control.Bounds;
      this.Control.Size = this.PerformInnerLayout(true, bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }

    public Size PerformInnerLayout(bool performMeasure, int x, int y, int width, int height)
    {
      if (!this.RootElement.DisableControlSizeSet)
      {
        this.RootElement.DisableControlSizeSet = true;
        if (performMeasure)
        {
          SizeF availableSize = new SizeF((float) width, (float) height);
          this.RootElement.InvalidateMeasure();
          this.RootElement.Measure(availableSize);
        }
        SizeF desiredSize = this.RootElement.DesiredSize;
        if (this.RootElement.StretchHorizontally)
          desiredSize.Width = (float) width;
        if (this.RootElement.StretchVertically)
          desiredSize.Height = (float) height;
        this.RootElement.Arrange(new RectangleF((float) x, (float) y, desiredSize.Width, desiredSize.Height));
        this.RootElement.UpdateLayout();
        if (this.Control.AutoSize)
        {
          Size size = Size.Round(desiredSize);
          width = size.Width;
          height = size.Height;
        }
        this.RootElement.DisableControlSizeSet = false;
      }
      return new Size(width, height);
    }

    public virtual void OnAutoSizeChanged(EventArgs e)
    {
      if (this.ComponentTreeHandler.Initializing)
        return;
      if (this.Control.AutoSize && !this.ComponentTreeHandler.LayoutManager.IsUpdating)
      {
        this.RootElement.InvalidateMeasure();
        this.RootElement.UpdateLayout();
      }
      this.RootElement.OnControlAutoSizeChanged(this.Control.AutoSize);
    }

    private void CheckLicense()
    {
    }

    internal bool IsHorizontallyStretchable()
    {
      AnchorStyles anchorStyles = AnchorStyles.Left | AnchorStyles.Right;
      bool flag1 = (this.Control.Anchor & anchorStyles) == anchorStyles;
      bool flag2 = this.Control.Dock == DockStyle.Top || this.Control.Dock == DockStyle.Bottom || this.Control.Dock == DockStyle.Fill;
      if (this.Control.AutoSize && (flag1 || flag2))
        return this.RootElement.StretchHorizontally;
      return false;
    }

    internal bool IsVerticallyStretchable()
    {
      AnchorStyles anchorStyles = AnchorStyles.Top | AnchorStyles.Bottom;
      bool flag1 = (this.Control.Anchor & anchorStyles) == anchorStyles;
      bool flag2 = this.Control.Dock == DockStyle.Left || this.Control.Dock == DockStyle.Right || this.Control.Dock == DockStyle.Fill;
      if (this.Control.AutoSize && (flag1 || flag2))
        return this.RootElement.StretchVertically;
      return false;
    }

    private bool CanHandleMouseInput(RadElement element)
    {
      return element.ShouldHandleMouseInput;
    }

    protected static bool AllowShowEvaluation
    {
      get
      {
        if (!RadControl.IsTrial)
          return false;
        try
        {
          Assembly entryAssembly = Assembly.GetEntryAssembly();
          AssemblyName assemblyName = (object) entryAssembly == null ? (AssemblyName) null : entryAssembly.GetName();
          return assemblyName == null || !(assemblyName.Name.ToLower() == "lc");
        }
        catch
        {
          return false;
        }
      }
    }
  }
}
