// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Assistance;
using Telerik.WinControls.Commands;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls
{
  public class RadElement : RadObject, ISupportSystemSkin, IStylableNode
  {
    private Rectangle cachedBounds = LayoutUtils.InvalidBounds;
    private Rectangle cachedBoundingRectangle = Rectangle.Empty;
    private Rectangle cachedControlBoundingRectangle = Rectangle.Empty;
    internal HybridDictionary IsStyleSelectorValueSet = new HybridDictionary();
    private List<EventHandler> layoutEvents = new List<EventHandler>();
    private RoutedEventBehaviorCollection routedEventBehaviors = new RoutedEventBehaviorCollection();
    private ArrayList processedEvents = new ArrayList();
    private PropertyChangeBehaviorCollection behaviors = new PropertyChangeBehaviorCollection();
    internal Size cachedSize = LayoutUtils.InvalidSize;
    internal Size cachedBorderSize = LayoutUtils.InvalidSize;
    internal Size cachedBorderOffset = LayoutUtils.InvalidSize;
    private Size coercedSize = LayoutUtils.InvalidSize;
    internal PerformLayoutType PerformLayoutAfterFinishLayout = PerformLayoutType.None;
    private SizeF dpiScaleFactor = new SizeF(1f, 1f);
    public static RadProperty BoundsProperty = RadProperty.Register(nameof (Bounds), typeof (Rectangle), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Rectangle.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty BorderThicknessProperty = RadProperty.Register(nameof (BorderThickness), typeof (Padding), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty PaddingProperty = RadProperty.Register(nameof (Padding), typeof (Padding), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty MarginProperty = RadProperty.Register(nameof (Margin), typeof (Padding), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure));
    public static RadProperty AlignmentProperty = RadProperty.Register(nameof (Alignment), typeof (System.Drawing.ContentAlignment), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) System.Drawing.ContentAlignment.TopLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty AutoSizeModeProperty = RadProperty.Register(nameof (AutoSizeMode), typeof (RadAutoSizeMode), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadAutoSizeMode.WrapAroundChildren, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty FitToSizeModeProperty = RadProperty.Register(nameof (FitToSizeMode), typeof (RadFitToSizeMode), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadFitToSizeMode.FitToParentContent, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsParentMeasure));
    public static RadProperty MinSizeProperty = RadProperty.Register(nameof (MinSize), typeof (Size), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Size.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty EnabledProperty = RadProperty.Register(nameof (Enabled), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CanFocusProperty = RadProperty.Register(nameof (CanFocus), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsItemFocusedProperty = RadProperty.Register("IsItemFocused", typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsFocusedProperty = RadProperty.Register(nameof (IsFocused), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsMouseOverProperty = RadProperty.Register(nameof (IsMouseOver), typeof (bool), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty IsMouseOverElementProperty = RadProperty.Register(nameof (IsMouseOverElement), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty IsMouseDownProperty = RadProperty.Register(nameof (IsMouseDown), typeof (bool), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty ShouldPaintProperty = RadProperty.Register(nameof (ShouldPaint), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty VisibilityProperty = RadProperty.Register(nameof (Visibility), typeof (ElementVisibility), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ElementVisibility.Visible, ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.Cancelable));
    public static RadProperty NameProperty = RadProperty.Register(nameof (Name), typeof (string), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty ClassProperty = RadProperty.Register(nameof (Class), typeof (string), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsTheme));
    public static RadProperty ClipDrawingProperty = RadProperty.Register(nameof (ClipDrawing), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShapeProperty = RadProperty.Register(nameof (Shape), typeof (ElementShape), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RoutedEvent MouseClickedEvent = RadElement.RegisterRoutedEvent(nameof (MouseClickedEvent), typeof (RadElement));
    public static RoutedEvent MouseDoubleClickedEvent = RadElement.RegisterRoutedEvent(nameof (MouseDoubleClickedEvent), typeof (RadElement));
    public static RoutedEvent MouseDownEvent = RadElement.RegisterRoutedEvent(nameof (MouseDownEvent), typeof (RadElement));
    public static RoutedEvent MouseUpEvent = RadElement.RegisterRoutedEvent(nameof (MouseUpEvent), typeof (RadElement));
    public static RoutedEvent MouseWheelEvent = RadElement.RegisterRoutedEvent(nameof (MouseWheelEvent), typeof (RadElement));
    public static RoutedEvent ChildElementAddedEvent = RadElement.RegisterRoutedEvent(nameof (ChildElementAddedEvent), typeof (RadElement));
    public static RoutedEvent ParentChangedEvent = RadElement.RegisterRoutedEvent(nameof (ParentChangedEvent), typeof (RadElement));
    public static RoutedEvent BoundsChangedEvent = RadElement.RegisterRoutedEvent(nameof (BoundsChangedEvent), typeof (RadElement));
    public static RoutedEvent VisibilityChangingEvent = RadElement.RegisterRoutedEvent(nameof (VisibilityChangingEvent), typeof (RadElement));
    public static RoutedEvent EnabledChangedEvent = RadElement.RegisterRoutedEvent(nameof (EnabledChangedEvent), typeof (RadElement));
    public static RoutedEvent ControlChangedEvent = RadElement.RegisterRoutedEvent(nameof (ControlChangedEvent), typeof (RadElement));
    public static RadProperty BackgroundShapeProperty = RadProperty.Register(nameof (BackgroundShape), typeof (RadImageShape), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsFocusProperty = RadProperty.Register(nameof (ContainsFocus), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty ContainsMouseProperty = RadProperty.Register(nameof (ContainsMouse), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static RadProperty IsEditedInSpyProperty = RadProperty.Register("IsEditedInSpy", typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty MaxSizeProperty = RadProperty.Register(nameof (MaxSize), typeof (Size), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Size.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty RightToLeftProperty = RadProperty.Register(nameof (RightToLeft), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty AutoSizeProperty = RadProperty.Register(nameof (AutoSize), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ZIndexProperty = RadProperty.Register(nameof (ZIndex), typeof (int), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AngleTransformProperty = RadProperty.Register(nameof (AngleTransform), typeof (float), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ScaleTransformProperty = RadProperty.Register(nameof (ScaleTransform), typeof (SizeF), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new SizeF(1f, 1f), ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty PositionOffsetProperty = RadProperty.Register(nameof (PositionOffset), typeof (SizeF), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SizeF.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty TagProperty = RadProperty.Register(nameof (Tag), typeof (object), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static readonly RadProperty StretchHorizontallyProperty = RadProperty.Register(nameof (StretchHorizontally), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata(BooleanBoxes.TrueBox, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static readonly RadProperty StretchVerticallyProperty = RadProperty.Register(nameof (StretchVertically), typeof (bool), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata(BooleanBoxes.TrueBox, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static RadProperty UseCompatibleTextRenderingProperty = RadProperty.Register(nameof (UseCompatibleTextRendering), typeof (bool?), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));
    public static RadProperty ClickModeProperty = RadProperty.Register(nameof (ClickMode), typeof (ClickMode), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ClickMode.Release, ElementPropertyOptions.None));
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static bool TraceInvalidation = false;
    private static int animationMaxFramerate = 25;
    public static RadProperty StyleProperty = RadProperty.Register(nameof (Style), typeof (StyleSheet), typeof (RadElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    private static readonly object MouseMoveEventKey = new object();
    private static readonly object MouseDownEventKey = new object();
    private static readonly object MouseUpEventKey = new object();
    private static readonly object MouseEnterEventKey = new object();
    private static readonly object MouseLeaveEventKey = new object();
    private static readonly object EnabledChangedEventKey = new object();
    private static readonly object MouseHoverEventKey = new object();
    private static readonly object MouseWheelEventKey = new object();
    private static readonly object MouseClickEventKey = new object();
    private static readonly object MouseDoubleClickEventKey = new object();
    private static readonly object LostMouseCaptureKey = new object();
    private static readonly object ChildrenChangedKey = new object();
    public static readonly SetPropertyValueCommand SetPropertyValueCommand = new SetPropertyValueCommand();
    internal const long UseIdentityMatrixStateKey = 8;
    internal const long InvalidateMeasureOnRemoveStateKey = 16;
    internal const long NotifyParentOnMouseInputStateKey = 32;
    internal const long ShouldHandleMouseInputStateKey = 64;
    internal const long ProtectFocusedPropertyStateKey = 128;
    internal const long BypassLayoutPoliciesStateKey = 256;
    internal const long InvalidateOnArrangeStateKey = 512;
    internal const long ArrangeDirtyStateKey = 1024;
    internal const long ArrangeInProgressStateKey = 2048;
    internal const long NeverArrangedStateKey = 4096;
    internal const long MeasureDirtyStateKey = 8192;
    internal const long MeasureDuringArrangeStateKey = 16384;
    internal const long MeasureInProgressStateKey = 32768;
    internal const long NeverMeasuredStateKey = 65536;
    internal const long OverridesDefaultLayoutStateKey = 131072;
    internal const long IsDelayedSizeStateKey = 262144;
    internal const long IsLayoutPendingStateKey = 524288;
    internal const long IsLayoutInvalidatedStateKey = 1048576;
    internal const long InvalidateChildrenOnChildChangedStateKey = 2097152;
    internal const long IsPendingInvalidateStateKey = 4194304;
    internal const long IsPendingLayoutInvalidatedStateKey = 8388608;
    internal const long IsPerformLayoutRunningStateKey = 16777216;
    internal const long DisableThemesStateKey = 33554432;
    internal const long UseCenteredRotationStateKey = 67108864;
    internal const long IsThemeAppliedStateKey = 134217728;
    internal const long SerializePropertiesStateKey = 268435456;
    internal const long SerializeChildrenStateKey = 536870912;
    internal const long SerializeElementStateKey = 1073741824;
    internal const long HideFromElementHierarchyEditorStateKey = 2147483648;
    internal const long ShouldPaintChildrenStateKey = 4294967296;
    internal const long IsFocusableStateKey = 8589934592;
    internal const long DoubleClickInitiatedStateKey = 17179869184;
    internal const long EnableDoubleClickStateKey = 34359738368;
    internal const long AutoToolTipStateKey = 68719476736;
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected const long RadElementLastStateKey = 68719476736;
    private RadElement.LayoutTransformData layoutTransformDataField;
    protected internal int styleVersion;
    private ComponentThemableElementTree elementTree;
    private ILayoutManager layoutManager;
    private RadElement parent;
    private ElementState state;
    private bool captureOnMouseDown;
    private RadElementCollection children;
    private RadElementZOrderCollection zOrderedChildren;
    private RadMatrix transform;
    private RadMatrix totalTransform;
    private byte suspendReferenceUpdate;
    private byte treeLevel;
    private SizeF _previousAvailableSize;
    private RectangleF _finalRect;
    private RadElement.SizeBox UnclippedDesiredSize;
    private SizeF _desiredSize;
    private PointF layoutOffset;
    internal SizeChangedInfo sizeChangedInfo;
    internal ContextLayoutManager.LayoutQueue.Request ArrangeRequest;
    internal ContextLayoutManager.LayoutQueue.Request MeasureRequest;
    internal byte layoutsRunning;
    private byte layoutSuspendCount;
    private byte boundsLocked;
    private byte suspendThemeRefresh;
    private byte suspendRecursiveEnabledUpdates;
    private UseSystemSkinMode useSystemSkin;
    protected internal bool? paintSystemSkin;
    private static HybridDictionary registeredRoutedEvents;
    public static readonly GetPropertyValueCommand GetPropertyValueCommand;
    private RadScreenTipElement screenTip;
    private int autoNumberKeyTip;

    static RadElement()
    {
      RadElement.SetPropertyValueCommand.Name = nameof (SetPropertyValueCommand);
      RadElement.SetPropertyValueCommand.Text = "This command sets a provided value to a property of an object. No type verification is done.";
      RadElement.SetPropertyValueCommand.OwnerType = typeof (RadElement);
      RadElement.GetPropertyValueCommand = new GetPropertyValueCommand();
      RadElement.GetPropertyValueCommand.Name = nameof (GetPropertyValueCommand);
      RadElement.GetPropertyValueCommand.Text = "This command gets the value of a property. No type verification is done.";
      RadElement.GetPropertyValueCommand.OwnerType = typeof (RadElement);
    }

    public RadElement()
    {
      this.state = ElementState.Constructing;
      this.InitializeFields();
      this.Construct();
    }

    private void Construct()
    {
      this.SuspendThemeRefresh();
      this.SuspendLayout(false);
      this.CallCreateChildElements();
      this.ResumeThemeRefresh();
      this.state = ElementState.Constructed;
    }

    protected virtual void CallCreateChildElements()
    {
      this.CreateChildElements();
      this.SetChildrenLocalValuesAsDefault(true);
    }

    public void SuspendReferenceUpdate()
    {
      ++this.suspendReferenceUpdate;
    }

    public void ResumeReferenceUpdate()
    {
      if (this.suspendReferenceUpdate <= (byte) 0)
        return;
      --this.suspendReferenceUpdate;
    }

    protected virtual void InitializeFields()
    {
      this.zOrderedChildren = new RadElementZOrderCollection(this);
      this.children = new RadElementCollection(this);
      this.useSystemSkin = UseSystemSkinMode.Inherit;
      this.paintSystemSkin = new bool?();
      RadBitVector64 bitState = this.BitState;
      bitState[16L] = true;
      bitState[128L] = true;
      bitState[4096L] = true;
      bitState[536870912L] = true;
      bitState[1073741824L] = true;
      bitState[4294967296L] = true;
      bitState[1048576L] = true;
      bitState[34359738368L] = true;
    }

    protected virtual void CreateChildElements()
    {
    }

    protected internal void OnLoad(bool recursive)
    {
      if (this.layoutManager == null)
        return;
      this.state = ElementState.Loading;
      this.layoutManager.LayoutEvents.AddRange((IEnumerable<EventHandler>) this.layoutEvents);
      this.layoutEvents.Clear();
      this.ResetLayoutCore();
      this.LoadCore();
      if (recursive)
      {
        foreach (RadElement child in this.children)
          child.OnLoad(recursive);
      }
      this.state = ElementState.Loaded;
      this.OnLoaded();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnLoad(bool recursive)
    {
      this.OnLoad(recursive);
    }

    protected virtual void LoadCore()
    {
    }

    protected virtual void OnLoaded()
    {
    }

    protected internal void OnUnload(ComponentThemableElementTree oldTree, bool recursive)
    {
      this.UnloadCore(oldTree);
      if (recursive)
      {
        foreach (RadElement child in this.children)
          child.OnUnload(oldTree, recursive);
      }
      this.state = ElementState.Unloaded;
      this.OnUnloaded(oldTree);
    }

    protected virtual void UnloadCore(ComponentThemableElementTree oldTree)
    {
      this.DetachFromElementTree(oldTree);
    }

    protected virtual void OnUnloaded(ComponentThemableElementTree oldTree)
    {
    }

    protected virtual void OnElementTreeChanged(ComponentThemableElementTree previousTree)
    {
    }

    protected virtual void OnBeginDispose()
    {
      this.state = ElementState.Disposing;
      ++this.suspendThemeRefresh;
      ++this.layoutSuspendCount;
      ++this.suspendReferenceUpdate;
    }

    [Browsable(false)]
    public ElementState ElementState
    {
      get
      {
        return this.state;
      }
    }

    protected void SetParent(RadElement parent)
    {
      if (this.state == ElementState.Disposed)
        throw new InvalidOperationException("Setting parent to an already disposed element");
      if (this.state == ElementState.Disposing)
      {
        this.parent = (RadElement) null;
      }
      else
      {
        if (this.parent == parent)
          return;
        RadElement parent1 = this.parent;
        this.parent = parent;
        if (parent != null && parent.DpiScaleFactor != this.DpiScaleFactor)
          this.DpiScaleChanged(parent.DpiScaleFactor);
        this.OnParentChanged(parent1);
      }
    }

    protected virtual void OnParentChanged(RadElement previousParent)
    {
      ComponentThemableElementTree elementTree = this.ElementTree;
      if (this.parent != null)
        this.UpdateReferences(this.parent.elementTree, true, true);
      else
        this.UpdateReferences((ComponentThemableElementTree) null, true, true);
      if (this.ShouldApplyTheme && this.elementTree != null && this.styleVersion != this.elementTree.StyleVersion)
        this.elementTree.ApplyThemeToElement((RadObject) this);
      this.InvalidateMeasure(false);
      this.InvalidateArrange(false);
      if (this.parent != null || this.IsThemeRefreshSuspended)
        return;
      this.BitState[134217728L] = false;
    }

    protected virtual void UpdateReferences(
      ComponentThemableElementTree tree,
      bool updateInheritance,
      bool recursive)
    {
      if (this.suspendReferenceUpdate > (byte) 0)
      {
        this.UpdateState(tree);
      }
      else
      {
        ComponentThemableElementTree elementTree = this.elementTree;
        if (this.layoutManager != null)
          this.layoutManager.RemoveElementFromQueues(this);
        this.elementTree = tree;
        this.layoutManager = tree == null ? (ILayoutManager) null : tree.ComponentTreeHandler.LayoutManager;
        bool load;
        bool unload;
        this.UpdateFromParent(out load, out unload);
        if (unload)
          this.OnUnload(elementTree, false);
        ++this.suspendRecursiveEnabledUpdates;
        this.UpdateEnabledFromParent();
        --this.suspendRecursiveEnabledUpdates;
        if (recursive)
        {
          foreach (RadElement child in this.children)
            child.UpdateReferences(tree, updateInheritance, recursive);
        }
        if (elementTree != tree)
          this.OnElementTreeChanged(elementTree);
        if (updateInheritance)
          this.UpdateInheritanceChain(!recursive);
        if (!load)
          return;
        this.OnLoad(true);
      }
    }

    private void UpdateFromParent(out bool load, out bool unload)
    {
      load = false;
      unload = false;
      if (this.parent != null)
      {
        this.IsDesignMode = this.parent.IsDesignMode;
        this.treeLevel = (byte) ((uint) this.parent.treeLevel + 1U);
        if (!this.IsCurrentStateInheritable() || !this.parent.IsCurrentStateInheritable() || this.state == this.parent.state)
          return;
        load = this.parent.state == ElementState.Loaded;
        unload = this.parent.state == ElementState.Unloaded && this.state == ElementState.Loaded;
        if (load || unload)
          return;
        this.state = this.parent.state;
      }
      else
      {
        this.treeLevel = (byte) 0;
        this.IsDesignMode = false;
        if (!this.IsInValidState(false))
          return;
        unload = this.state == ElementState.Loaded;
        if (unload)
          return;
        this.state = ElementState.Constructed;
      }
    }

    private void UpdateEnabledFromParent()
    {
      if (this.parent != null)
      {
        if (!this.parent.Enabled)
        {
          int num1 = (int) this.BindProperty(RadElement.EnabledProperty, (RadObject) this.parent, RadElement.EnabledProperty, PropertyBindingOptions.OneWay);
        }
        else
        {
          int num2 = (int) this.ResetValue(RadElement.EnabledProperty, ValueResetFlags.Binding);
        }
      }
      else
      {
        int num3 = (int) this.ResetValue(RadElement.EnabledProperty, ValueResetFlags.Binding);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetIsDesignMode(bool value, bool recursive)
    {
      if (this.suspendReferenceUpdate > (byte) 0)
        return;
      this.IsDesignMode = value;
      if (!recursive)
        return;
      foreach (RadElement child in this.children)
        child.SetIsDesignMode(value, recursive);
    }

    private void UpdateState(ComponentThemableElementTree tree)
    {
      if (this.layoutManager != null)
      {
        this.layoutManager.MeasureQueue.Remove(this);
        this.layoutManager.ArrangeQueue.Remove(this);
      }
      if (tree != null)
      {
        if (this.state != ElementState.Unloaded)
          return;
        this.state = ElementState.Loaded;
      }
      else
      {
        if (this.state != ElementState.Loaded)
          return;
        this.state = ElementState.Unloaded;
      }
    }

    private bool IsCurrentStateInheritable()
    {
      if (this.state != ElementState.Constructed && this.state != ElementState.Loaded)
        return this.state == ElementState.Unloaded;
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ChangeCollection(RadElement child, ItemsChangeOperation changeOperation)
    {
      if (this.state == ElementState.Disposed)
        throw new InvalidOperationException("Changing Children collection of an already disposed element");
      if (this.state == ElementState.Disposing)
        return;
      this.UpdateZOrderedCollection(child, changeOperation);
      switch (changeOperation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          child.SetParent(this);
          break;
        case ItemsChangeOperation.Removed:
          child.SetParent((RadElement) null);
          break;
        case ItemsChangeOperation.Clearing:
          int count1 = this.children.Count;
          for (int index = 0; index < count1; ++index)
            this.children[index].SetParent((RadElement) null);
          break;
        case ItemsChangeOperation.BatchInsert:
          int count2 = this.children.Count;
          for (int index = 0; index < count2; ++index)
            this.children[index].SetParent(this);
          break;
      }
      if (this.state == ElementState.Loaded && this.GetBitState(16L))
        this.InvalidateMeasure();
      this.OnChildrenChanged(child, changeOperation);
    }

    internal void ResetZOrderCache()
    {
      this.zOrderedChildren.ResetCachedIndices();
    }

    public void ResetLayout(bool recursive)
    {
      this.ResetLayoutCore();
      if (!recursive)
        return;
      foreach (RadElement child in this.children)
        child.ResetLayout(recursive);
    }

    public bool HasInvisibleAncestor()
    {
      for (RadElement parent = this.parent; parent != null; parent = parent.parent)
      {
        if (parent.Visibility != ElementVisibility.Visible)
          return true;
      }
      return false;
    }

    protected virtual void ResetLayoutCore()
    {
      this._previousAvailableSize = SizeF.Empty;
      this.layoutTransformDataField = (RadElement.LayoutTransformData) null;
      this._finalRect = RectangleF.Empty;
      this.UnclippedDesiredSize = (RadElement.SizeBox) null;
      this._desiredSize = SizeF.Empty;
      this.layoutOffset = (PointF) Point.Empty;
      RadBitVector64 bitState = this.BitState;
      bitState[1024L] = false;
      bitState[2048L] = false;
      bitState[4096L] = true;
      bitState[8192L] = false;
      bitState[16384L] = false;
      bitState[32768L] = false;
      bitState[65536L] = true;
      bitState[1048576L] = true;
      if (this.ArrangeRequest != null && this.layoutManager != null)
        this.layoutManager.ArrangeQueue.Remove(this);
      this.ArrangeRequest = (ContextLayoutManager.LayoutQueue.Request) null;
      if (this.MeasureRequest != null && this.layoutManager != null)
        this.layoutManager.MeasureQueue.Remove(this);
      this.MeasureRequest = (ContextLayoutManager.LayoutQueue.Request) null;
      this.SuspendPropertyNotifications();
      if (this.AutoSize)
      {
        this.cachedBounds = LayoutUtils.InvalidBounds;
        int num = (int) this.ResetValue(RadElement.BoundsProperty, ValueResetFlags.Local);
      }
      this.ResetTransformations();
      this.ResumePropertyNotifications();
      this.ResumeLayout(false, false);
    }

    private void ResetTransformations()
    {
      this.transform = RadMatrix.Empty;
      this.totalTransform = RadMatrix.Empty;
      this.cachedControlBoundingRectangle = Rectangle.Empty;
      this.cachedBoundingRectangle = Rectangle.Empty;
    }

    private void DetachFromElementTree(ComponentThemableElementTree oldTree)
    {
      if (oldTree == null)
        return;
      ComponentInputBehavior behavior = oldTree.ComponentTreeHandler.Behavior;
      if (behavior.currentFocusedElement == this)
        behavior.currentFocusedElement = (RadElement) null;
      if (behavior.selectedElement == this)
        behavior.selectedElement = (RadElement) null;
      if (behavior.pressedElement == this)
        behavior.pressedElement = (RadElement) null;
      if (behavior.itemCapture == this)
        behavior.itemCapture = (RadElement) null;
      if (this.IsFocused)
      {
        this.SuspendPropertyNotifications();
        int num = (int) this.SetValue(RadElement.IsFocusedProperty, (object) false);
        if (behavior.FocusedElement == this)
          behavior.FocusedElement = (RadElement) null;
        this.ResumePropertyNotifications();
      }
      oldTree.ComponentTreeHandler.LayoutManager.RemoveElementFromQueues(this);
      this.MeasureRequest = (ContextLayoutManager.LayoutQueue.Request) null;
      this.ArrangeRequest = (ContextLayoutManager.LayoutQueue.Request) null;
    }

    internal void SetChildrenLocalValuesAsDefault(bool recursive)
    {
      int count = this.children.Count;
      for (int index = 0; index < count; ++index)
      {
        this.children[index].SetAllLocalValuesAsDefault(false);
        if (recursive)
          this.children[index].SetChildrenLocalValuesAsDefault(recursive);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetAllLocalValuesAsDefault(bool recursive)
    {
      this.PropertyValues.SetLocalValuesAsDefault();
      if (!recursive)
        return;
      int count = this.children.Count;
      for (int index = 0; index < count; ++index)
        this.children[index].SetAllLocalValuesAsDefault(recursive);
    }

    public void InvalidateArrange()
    {
      this.InvalidateArrange(false);
    }

    public void InvalidateArrange(bool recursive)
    {
      if (!this.IsInValidState(true))
      {
        this.SetBitState(1024L, true);
      }
      else
      {
        if (!this.GetBitState(1024L) && !this.GetBitState(2048L))
        {
          if (!this.GetBitState(4096L) && this.layoutSuspendCount == (byte) 0)
            this.layoutManager.ArrangeQueue.Add(this);
          this.BitState[1024L] = true;
        }
        if (!recursive)
          return;
        foreach (RadElement child in this.children)
          child.InvalidateArrange(recursive);
      }
    }

    public void InvalidateMeasure()
    {
      this.InvalidateMeasure(false);
    }

    public void InvalidateMeasure(bool recursive)
    {
      if (!this.IsInValidState(true))
      {
        this.BitState[8192L] = true;
      }
      else
      {
        if (!this.GetBitState(8192L) && !this.GetBitState(32768L))
        {
          if (!this.GetBitState(65536L) && this.layoutSuspendCount == (byte) 0)
            this.layoutManager.MeasureQueue.Add(this);
          this.BitState[8192L] = true;
        }
        if (!recursive)
          return;
        foreach (RadElement child in this.children)
          child.InvalidateMeasure(true);
      }
    }

    public void UpdateLayout()
    {
      if (!this.CanExecuteLayoutOperation())
        return;
      this.layoutManager.UpdateLayout();
    }

    public void SuspendLayout()
    {
      this.SuspendLayout(false);
    }

    public virtual void SuspendLayout(bool recursive)
    {
      ++this.layoutSuspendCount;
      if (!recursive)
        return;
      foreach (RadElement child in this.children)
        child.SuspendLayout(recursive);
    }

    public void ResumeLayout(bool performLayout)
    {
      this.ResumeLayout(false, performLayout);
    }

    public virtual void ResumeLayout(bool recursive, bool performLayout)
    {
      if (this.layoutSuspendCount > (byte) 0)
        --this.layoutSuspendCount;
      if (this.state == ElementState.Loaded && this.layoutSuspendCount == (byte) 0)
      {
        this.InvalidateTransformations();
        if (performLayout)
        {
          this.BitState[8192L] = false;
          this.InvalidateMeasure(false);
          this.BitState[1024L] = false;
          this.InvalidateArrange(false);
        }
      }
      if (!recursive)
        return;
      foreach (RadElement child in this.children)
        child.ResumeLayout(recursive, performLayout);
    }

    public void Arrange(RectangleF finalRect)
    {
      if (!this.CanExecuteLayoutOperation())
      {
        this.UpdateLayoutRequest(true);
      }
      else
      {
        try
        {
          this.layoutManager.IncrementLayoutCalls();
          if (float.IsPositiveInfinity(finalRect.Width) || float.IsPositiveInfinity(finalRect.Height) || (float.IsNaN(finalRect.Width) || float.IsNaN(finalRect.Height)))
          {
            RadElement parent = this.Parent;
            throw new InvalidOperationException(string.Format("Arrange with infinity or NaN size (parent: {0}; this: {1})", parent == null ? (object) "" : (object) parent.GetType().FullName, (object) this.GetType().FullName));
          }
          if (this.Visibility == ElementVisibility.Collapsed || this.IsLayoutSuspended)
          {
            if (this.ArrangeRequest != null)
              this.layoutManager.ArrangeQueue.Remove(this);
            this._finalRect = finalRect;
          }
          else
          {
            if (!this.GetBitState(8192L))
            {
              if (!this.GetBitState(65536L))
                goto label_15;
            }
            try
            {
              this.BitState[16384L] = true;
              if (this.GetBitState(65536L))
                this.Measure(finalRect.Size);
              else
                this.Measure(this._previousAvailableSize);
            }
            finally
            {
              this.BitState[16384L] = false;
            }
label_15:
            if (this.IsArrangeValid && !this.GetBitState(4096L) && (DoubleUtil.AreClose(finalRect, this._finalRect) && this.AutoSize))
              return;
            this.BitState[4096L] = false;
            this.BitState[2048L] = true;
            ILayoutManager layoutManager = this.layoutManager;
            Size size = this.Size;
            try
            {
              layoutManager.EnterArrange();
              this.ArrangeCore(finalRect);
              this.MarkForSizeChangedIfNeeded(size, this.Size);
            }
            finally
            {
              this.BitState[2048L] = false;
              layoutManager.ExitArrange();
            }
            this._finalRect = finalRect;
            this.BitState[1024L] = false;
            if (this.ArrangeRequest == null)
              return;
            this.layoutManager.ArrangeQueue.Remove(this);
          }
        }
        finally
        {
          this.layoutManager.DecrementLayoutCalls();
        }
      }
    }

    public void Measure(SizeF availableSize)
    {
      if (!this.CanExecuteLayoutOperation())
      {
        this.UpdateLayoutRequest(false);
      }
      else
      {
        try
        {
          this.layoutManager.IncrementLayoutCalls();
          if (float.IsNaN(availableSize.Width) || float.IsNaN(availableSize.Height))
            throw new InvalidOperationException("Measure with NaN available size");
          if (this.Visibility == ElementVisibility.Collapsed || this.IsLayoutSuspended)
          {
            if (this.MeasureRequest != null)
              this.layoutManager.MeasureQueue.Remove(this);
            if (DoubleUtil.AreClose(availableSize, this._previousAvailableSize))
              return;
            this.BitState[8192L] = true;
            this._previousAvailableSize = availableSize;
          }
          else if (this.IsMeasureValid && !this.GetBitState(65536L) && DoubleUtil.AreClose(availableSize, this._previousAvailableSize))
          {
            if (this.MeasureRequest == null)
              return;
            this.layoutManager.MeasureQueue.Remove(this);
          }
          else
          {
            this.BitState[65536L] = false;
            SizeF desiredSize = this._desiredSize;
            this.InvalidateArrange();
            this.BitState[32768L] = true;
            SizeF size2 = new SizeF(0.0f, 0.0f);
            ILayoutManager layoutManager = this.layoutManager;
            try
            {
              layoutManager.EnterMeasure();
              size2 = this.MeasureCore(availableSize);
            }
            finally
            {
              this.BitState[32768L] = false;
              this._previousAvailableSize = availableSize;
              layoutManager.ExitMeasure();
            }
            if (float.IsPositiveInfinity(size2.Width) || float.IsPositiveInfinity(size2.Height))
              throw new InvalidOperationException(string.Format("MeasureOverride returned positive infinity: {0}", (object) this.GetType().FullName));
            if (float.IsNaN(size2.Width) || float.IsNaN(size2.Height))
              throw new InvalidOperationException(string.Format("MeasureOverride returned NaN: {0}", (object) this.GetType().FullName));
            this.BitState[8192L] = false;
            if (this.MeasureRequest != null)
              this.layoutManager.MeasureQueue.Remove(this);
            this._desiredSize = size2;
            if (this.GetBitState(16384L) || DoubleUtil.AreClose(desiredSize, size2))
              return;
            this.BitState[512L] = true;
            RadElement parent = this.Parent;
            if (parent == null || parent.GetBitState(32768L))
              return;
            parent.OnChildDesiredSizeChanged(this);
          }
        }
        finally
        {
          if (this.layoutManager != null)
            this.layoutManager.DecrementLayoutCalls();
        }
      }
    }

    public void SetBounds(Rectangle bounds)
    {
      this.SetBoundsCore(bounds);
    }

    public void SetBounds(int x, int y, int width, int height)
    {
      this.SetBounds(new Rectangle(x, y, width, height));
    }

    public Rectangle GetBoundingRectangle(Size size)
    {
      return this.GetBoundingRectangle(new Rectangle(Point.Empty, size));
    }

    public Rectangle GetBoundingRectangle(Rectangle bounds)
    {
      if ((double) this.AngleTransform == 0.0 && (double) this.ScaleTransform.Width == 1.0 && (double) this.ScaleTransform.Height == 1.0)
        return bounds;
      Rectangle boundingRect = TelerikHelper.GetBoundingRect(new Rectangle(Point.Empty, bounds.Size), this.Transform);
      return new Rectangle(bounds.Location, boundingRect.Size);
    }

    public SizeF GetDesiredSize(bool checkCollapsed)
    {
      if (checkCollapsed && this.Visibility == ElementVisibility.Collapsed)
        return SizeF.Empty;
      return this._desiredSize;
    }

    public Point PointToScreen(Point point)
    {
      if (!this.IsInValidState(true))
        return Point.Empty;
      Control control1 = this.elementTree.Control;
      Point control2 = this.PointToControl(point);
      return control1 != null ? control1.PointToScreen(control2) : control2;
    }

    public Point PointFromScreen(Point point)
    {
      if (!this.IsInValidState(true))
        return Point.Empty;
      Point screen = this.PointToScreen(new Point(0, 0));
      screen.X = point.X - screen.X;
      screen.Y = point.Y - screen.Y;
      return screen;
    }

    public Point PointToControl(Point point)
    {
      if (this.state != ElementState.Loaded)
        return point;
      Point[] points = new Point[1]
      {
        new Point(point.X, point.Y)
      };
      TelerikHelper.TransformPoints(points, this.TotalTransform.Elements);
      return points[0];
    }

    public Point PointFromControl(Point point)
    {
      if (this.state != ElementState.Loaded)
        return point;
      Point[] points = new Point[1]{ new Point(0, 0) };
      TelerikHelper.TransformPoints(points, this.TotalTransform.Elements);
      points[0].X = point.X - points[0].X;
      points[0].Y = point.Y - points[0].Y;
      float angleTransform = this.AngleTransform;
      for (RadElement parent = this.Parent; parent != null; parent = parent.Parent)
        angleTransform += parent.AngleTransform;
      if ((double) angleTransform != 0.0)
      {
        using (Matrix matrix = new Matrix())
        {
          matrix.Rotate(-angleTransform);
          TelerikHelper.TransformPoints(points, matrix.Elements);
        }
      }
      return points[0];
    }

    public Point LocationToControl()
    {
      return this.ControlBoundingRectangle.Location;
    }

    public Rectangle RectangleToScreen(Rectangle rect)
    {
      if (this.state != ElementState.Loaded)
        return Rectangle.Empty;
      rect = TelerikHelper.GetBoundingRect(rect, this.TotalTransform);
      return this.elementTree.Control.RectangleToScreen(rect);
    }

    public virtual bool HitTest(Point point)
    {
      if (this.Size.Width == 0 || this.Size.Height == 0)
        return false;
      if (this.Shape != null)
      {
        using (GraphicsPath path = this.Shape.CreatePath(new Rectangle(Point.Empty, this.Size)))
        {
          bool flag = false;
          using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
          {
            path.Transform(gdiMatrix);
            flag = path.IsVisible(point);
          }
          return flag;
        }
      }
      else
      {
        if ((double) this.TotalTransform.M12 == 0.0)
          return this.ControlBoundingRectangle.Contains(point);
        Point[] points = new Point[4]
        {
          new Point(0, 0),
          new Point(this.Bounds.Width, 0),
          new Point(this.Bounds.Width, this.Bounds.Height),
          new Point(0, this.Bounds.Height)
        };
        TelerikHelper.TransformPoints(points, this.TotalTransform.Elements);
        using (GraphicsPath graphicsPath = new GraphicsPath())
        {
          graphicsPath.AddPolygon(points);
          return graphicsPath.IsVisible(point);
        }
      }
    }

    [Browsable(false)]
    public event EventHandler LayoutUpdated
    {
      add
      {
        if (this.layoutManager != null)
          this.layoutManager.LayoutEvents.Add(value);
        else
          this.layoutEvents.Add(value);
      }
      remove
      {
        if (this.layoutManager != null)
          this.layoutManager.LayoutEvents.Remove(value);
        else
          this.layoutEvents.Remove(value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnRenderSizeChanged(SizeChangedInfo info)
    {
      this.OnRenderSizeChanged(info);
    }

    protected virtual void OnRenderSizeChanged(SizeChangedInfo info)
    {
    }

    protected virtual void OnChildDesiredSizeChanged(RadElement child)
    {
      if (!this.IsMeasureValid || !this.AutoSize)
        return;
      this.InvalidateMeasure();
    }

    protected virtual void OnLayoutPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (this.state != ElementState.Loaded)
        return;
      if (!this.IsLayoutSuspended && (e.Property == RadElement.AngleTransformProperty || e.Property == RadElement.ScaleTransformProperty))
        this.InvalidateTransformations(true);
      RadElementPropertyMetadata metadata = e.Metadata as RadElementPropertyMetadata;
      if (metadata == null)
        return;
      if (e.Property == RadElement.BoundsProperty)
      {
        Rectangle oldValue = (Rectangle) e.OldValue;
        Rectangle newValue = (Rectangle) e.NewValue;
        this.cachedBounds = newValue;
        this.InvalidateBoundingRectangle();
        bool flag1 = newValue.Location != oldValue.Location;
        bool flag2 = newValue.Size != oldValue.Size;
        if (!this.layoutManager.IsUpdating)
        {
          if (this.parent != null)
            this.parent.InvalidateArrange();
          if (flag2)
          {
            this.InvalidateMeasure();
            this.InvalidateArrange();
          }
        }
        this.OnBoundsChanged(e);
        if (!flag1)
          return;
        this.OnLocationChanged(e);
      }
      else
      {
        RadElement radElement = this.parent != null ? this.parent : this;
        if (metadata.AffectsParentMeasure)
          radElement.InvalidateMeasure();
        if (metadata.AffectsParentArrange)
          radElement.InvalidateArrange();
        if (metadata.AffectsMeasure)
          this.InvalidateMeasure();
        if (!metadata.AffectsArrange && !metadata.AffectsLayout)
          return;
        this.InvalidateArrange();
      }
    }

    protected virtual void ArrangeCore(RectangleF finalRect)
    {
      if (!this.AutoSize)
      {
        Size size = this.Size;
        this.ArrangeOverride((SizeF) size);
        this.SetLayoutParams(this.CalcLayoutOffset(finalRect.Location), (SizeF) size);
      }
      else if (this.BypassLayoutPolicies)
      {
        SizeF newSize = this.ArrangeOverride(finalRect.Size);
        this.SetLayoutParams(this.CalcLayoutOffset(finalRect.Location), newSize);
      }
      else
      {
        SizeF sizeF1 = finalRect.Size;
        Padding margin = this.Margin;
        int num1 = margin.Left + margin.Right;
        int num2 = margin.Top + margin.Bottom;
        sizeF1.Width = Math.Max(0.0f, sizeF1.Width - (float) num1);
        sizeF1.Height = Math.Max(0.0f, sizeF1.Height - (float) num2);
        SizeF sizeF2 = SizeF.Empty;
        RadElement.SizeBox unclippedDesiredSize = this.UnclippedDesiredSize;
        sizeF2 = unclippedDesiredSize != null ? new SizeF(unclippedDesiredSize.Width, unclippedDesiredSize.Height) : new SizeF(this.DesiredSize.Width - (float) num1, this.DesiredSize.Height - (float) num2);
        if (DoubleUtil.LessThan(sizeF1.Width, sizeF2.Width))
          sizeF1.Width = sizeF2.Width;
        if (DoubleUtil.LessThan(sizeF1.Height, sizeF2.Height))
          sizeF1.Height = sizeF2.Height;
        if (!this.StretchHorizontally)
          sizeF1.Width = sizeF2.Width;
        if (!this.StretchVertically)
          sizeF1.Height = sizeF2.Height;
        RadElement.LayoutTransformData transformDataField = this.layoutTransformDataField;
        if (transformDataField != null)
        {
          SizeF areaLocalSpaceRect = this.FindMaximalAreaLocalSpaceRect(transformDataField.transform, sizeF1);
          sizeF1 = areaLocalSpaceRect;
          sizeF2 = transformDataField.UntransformedDS;
          if (!DoubleUtil.IsZero(areaLocalSpaceRect.Width) && !DoubleUtil.IsZero(areaLocalSpaceRect.Height) && (DoubleUtil.LessThan(areaLocalSpaceRect.Width, sizeF2.Width) || DoubleUtil.LessThan(areaLocalSpaceRect.Height, sizeF2.Height)))
            sizeF1 = sizeF2;
          if (DoubleUtil.LessThan(sizeF1.Width, sizeF2.Width))
            sizeF1.Width = sizeF2.Width;
          if (DoubleUtil.LessThan(sizeF1.Height, sizeF2.Height))
            sizeF1.Height = sizeF2.Height;
        }
        RadElement.MinMax minMax = new RadElement.MinMax(this);
        float num3 = Math.Max(sizeF2.Width, minMax.maxWidth);
        if (DoubleUtil.LessThan(num3, sizeF1.Width))
          sizeF1.Width = num3;
        float num4 = Math.Max(sizeF2.Height, minMax.maxHeight);
        if (DoubleUtil.LessThan(num4, sizeF1.Height))
          sizeF1.Height = num4;
        SizeF newSize = this.ArrangeOverride(sizeF1);
        SizeF sizeF3 = newSize;
        if ((double) minMax.maxWidth > 0.0)
          sizeF3.Width = Math.Min(newSize.Width, minMax.maxWidth);
        if ((double) minMax.maxHeight > 0.0)
          sizeF3.Height = Math.Min(newSize.Height, minMax.maxHeight);
        if (transformDataField != null)
          sizeF3 = TelerikHelper.GetBoundingRect(new RectangleF((PointF) Point.Empty, sizeF3), transformDataField.transform).Size;
        SizeF size = new SizeF(Math.Max(0.0f, finalRect.Width - (float) num1), Math.Max(0.0f, finalRect.Height - (float) num2));
        System.Drawing.ContentAlignment align = this.RightToLeft ? TelerikAlignHelper.RtlTranslateContent(this.Alignment) : this.Alignment;
        this.SetLayoutParams(this.CalcLayoutOffset(LayoutUtils.Align(sizeF3, new RectangleF(finalRect.Location, size), align).Location), newSize);
      }
    }

    protected virtual SizeF MeasureCore(SizeF availableSize)
    {
      if (!this.AutoSize)
      {
        Size size = this.Size;
        this.MeasureOverride((SizeF) size);
        if (this.BypassLayoutPolicies)
          size = Size.Empty;
        return (SizeF) size;
      }
      if (this.BypassLayoutPolicies)
        return this.MeasureOverride(availableSize);
      Padding margin = this.Margin;
      int num1 = margin.Left + margin.Right;
      int num2 = margin.Top + margin.Bottom;
      SizeF sizeF = new SizeF(Math.Max(availableSize.Width - (float) num1, 0.0f), Math.Max(availableSize.Height - (float) num2, 0.0f));
      RadElement.MinMax minMax = new RadElement.MinMax(this);
      RadElement.LayoutTransformData layoutTransformData = this.layoutTransformDataField;
      if (!this.Transform.IsIdentity)
      {
        if (layoutTransformData == null)
        {
          layoutTransformData = new RadElement.LayoutTransformData();
          this.layoutTransformDataField = layoutTransformData;
        }
        layoutTransformData.CreateTransformSnapshot(this.Transform);
        layoutTransformData.UntransformedDS = new SizeF();
      }
      else if (layoutTransformData != null)
      {
        layoutTransformData = (RadElement.LayoutTransformData) null;
        this.layoutTransformDataField = (RadElement.LayoutTransformData) null;
      }
      if (layoutTransformData != null)
        sizeF = this.FindMaximalAreaLocalSpaceRect(layoutTransformData.transform, sizeF);
      sizeF.Width = Math.Max(minMax.minWidth, Math.Min(sizeF.Width, minMax.maxWidth));
      sizeF.Height = Math.Max(minMax.minHeight, Math.Min(sizeF.Height, minMax.maxHeight));
      SizeF size1 = this.MeasureOverride(sizeF);
      size1 = new SizeF(Math.Max(size1.Width, minMax.minWidth), Math.Max(size1.Height, minMax.minHeight));
      SizeF size2 = size1;
      if (layoutTransformData != null)
      {
        layoutTransformData.UntransformedDS = size2;
        size2 = TelerikHelper.GetBoundingRect(new RectangleF(PointF.Empty, size2), layoutTransformData.transform).Size;
      }
      bool flag = false;
      if ((double) size1.Width > (double) minMax.maxWidth && (double) minMax.maxWidth > 0.0)
      {
        size1.Width = minMax.maxWidth;
        flag = true;
      }
      if ((double) size1.Height > (double) minMax.maxHeight && (double) minMax.maxHeight > 0.0)
      {
        size1.Height = minMax.maxHeight;
        flag = true;
      }
      if (layoutTransformData != null)
        size1 = TelerikHelper.GetBoundingRect(new RectangleF((PointF) Point.Empty, size1), layoutTransformData.transform).Size;
      float val2_1 = size1.Width + (float) num1;
      float val2_2 = size1.Height + (float) num2;
      if ((double) val2_1 > (double) availableSize.Width)
      {
        val2_1 = availableSize.Width;
        flag = true;
      }
      if ((double) val2_2 > (double) availableSize.Height)
      {
        val2_2 = availableSize.Height;
        flag = true;
      }
      RadElement.SizeBox unclippedDesiredSize = this.UnclippedDesiredSize;
      if (flag || (double) val2_1 < 0.0 || (double) val2_2 < 0.0)
      {
        if (unclippedDesiredSize == null)
        {
          this.UnclippedDesiredSize = new RadElement.SizeBox(size2);
        }
        else
        {
          unclippedDesiredSize.Width = size2.Width;
          unclippedDesiredSize.Height = size2.Height;
        }
      }
      else if (unclippedDesiredSize != null)
        this.UnclippedDesiredSize = (RadElement.SizeBox) null;
      return new SizeF(Math.Max(0.0f, val2_1), Math.Max(0.0f, val2_2));
    }

    protected virtual bool ShouldArrangeChild(RadElement child)
    {
      return true;
    }

    protected virtual SizeF ArrangeOverride(SizeF finalSize)
    {
      for (int index = 0; index < this.children.Count; ++index)
      {
        RadElement child = this.children[index];
        if (this.ShouldArrangeChild(child))
        {
          RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
          if (!this.BypassLayoutPolicies)
          {
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
            {
              Padding borderThickness = this.BorderThickness;
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) borderThickness.Left, (float) borderThickness.Top));
              finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) borderThickness.Size);
            }
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
            {
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) this.Padding.Left, (float) this.Padding.Top));
              finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) this.Padding.Size);
            }
          }
          child.Arrange(finalRect);
        }
      }
      return finalSize;
    }

    protected virtual SizeF MeasureOverride(SizeF availableSize)
    {
      return this.MeasureChildren(availableSize);
    }

    protected virtual bool ShouldMeasureChild(RadElement child)
    {
      return true;
    }

    protected SizeF MeasureChildren(SizeF availableSize)
    {
      SizeF sizeF = SizeF.Empty;
      if (this.AutoSize)
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          if (this.ShouldMeasureChild(child))
          {
            child.Measure(availableSize);
            SizeF sz1 = child.DesiredSize;
            if (!this.BypassLayoutPolicies)
            {
              if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
                sz1 = SizeF.Add(sz1, (SizeF) this.Padding.Size);
              if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
                sz1 = SizeF.Add(sz1, (SizeF) this.BorderThickness.Size);
            }
            if ((double) sizeF.Width < (double) sz1.Width)
              sizeF.Width = sz1.Width;
            if ((double) sizeF.Height < (double) sz1.Height)
              sizeF.Height = sz1.Height;
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Children.Count; ++index)
          this.Children[index].Measure(availableSize);
        sizeF = (SizeF) this.Size;
      }
      return sizeF;
    }

    protected internal virtual RectangleF GetArrangeRect(RectangleF proposed)
    {
      return proposed;
    }

    protected virtual bool CanExecuteLayoutOperation()
    {
      if (this.state == ElementState.Loaded)
        return this.layoutSuspendCount == (byte) 0;
      return false;
    }

    protected virtual PointF CalcLayoutOffset(PointF startPoint)
    {
      startPoint.X = startPoint.X + (float) this.Margin.Left + (float) this.Location.X;
      startPoint.Y = startPoint.Y + (float) this.Margin.Top + (float) this.Location.Y;
      return startPoint;
    }

    protected virtual void LockBounds()
    {
      ++this.boundsLocked;
    }

    protected virtual void UnlockBounds()
    {
      if (this.boundsLocked <= (byte) 0)
        return;
      --this.boundsLocked;
    }

    protected virtual void SetBoundsCore(Rectangle bounds)
    {
      if (this.boundsLocked != (byte) 0)
        throw new InvalidOperationException("Bounds cannot be changed while locked.");
      bounds.Width = Math.Max(0, bounds.Width);
      bounds.Height = Math.Max(0, bounds.Height);
      this.cachedBounds = bounds;
      this.InvalidateBoundingRectangle();
      int num = (int) this.SetValue(RadElement.BoundsProperty, (object) this.cachedBounds);
    }

    public bool IsInValidState(bool checkElementTree)
    {
      if (this.elementTree == null && checkElementTree)
        return false;
      if (this.state != ElementState.Constructed && this.state != ElementState.Loaded)
        return this.state == ElementState.Unloaded;
      return true;
    }

    private void SetLayoutParams(PointF newOffset, SizeF newSize)
    {
      bool flag = !DoubleUtil.AreClose(newOffset, this.layoutOffset) || !DoubleUtil.AreClose(newSize, (SizeF) this.Size);
      if (!this.GetBitState(512L) && !flag)
        return;
      if (flag)
      {
        int num = RadElement.TraceInvalidation ? 1 : 0;
        this.Invalidate();
        this.layoutOffset = (PointF) Point.Round(newOffset);
        this.Size = Size.Round(newSize);
        this.InvalidateTransformations(false);
        this.CallOnTransformationInvalidatedRecursively();
      }
      int num1 = RadElement.TraceInvalidation ? 1 : 0;
      this.Invalidate();
      this.BitState[512L] = false;
    }

    private bool MarkForSizeChangedIfNeeded(Size oldSize, Size newSize)
    {
      if (!this.IsInValidState(true))
        return false;
      bool widthChanged = oldSize.Width != newSize.Width;
      bool heightChanged = oldSize.Height != newSize.Height;
      SizeChangedInfo sizeChangedInfo = this.sizeChangedInfo;
      if (sizeChangedInfo != null)
      {
        sizeChangedInfo.Update(widthChanged, heightChanged);
        return true;
      }
      if (!widthChanged && !heightChanged)
        return false;
      SizeChangedInfo info = new SizeChangedInfo(this, oldSize, widthChanged, heightChanged);
      this.sizeChangedInfo = info;
      this.layoutManager.AddToSizeChangedChain(info);
      return true;
    }

    private SizeF FindMaximalAreaLocalSpaceRect(
      RadMatrix layoutTransform,
      SizeF transformSpaceBounds)
    {
      float f1 = transformSpaceBounds.Width;
      float f2 = transformSpaceBounds.Height;
      if (DoubleUtil.IsZero(f1) || DoubleUtil.IsZero(f2))
        return (SizeF) new Size(0, 0);
      bool flag1 = float.IsInfinity(f1);
      bool flag2 = float.IsInfinity(f2);
      if (flag1 && flag2)
        return new SizeF(float.PositiveInfinity, float.PositiveInfinity);
      if (flag1)
        f1 = f2;
      else if (flag2)
        f2 = f1;
      if (!layoutTransform.IsInvertible)
        return (SizeF) new Size(0, 0);
      float m11 = layoutTransform.M11;
      float m12 = layoutTransform.M12;
      float m21 = layoutTransform.M21;
      float m22 = layoutTransform.M22;
      float height;
      float width;
      if (DoubleUtil.IsZero(m12) || DoubleUtil.IsZero(m21))
      {
        float val2_1 = flag2 ? float.PositiveInfinity : Math.Abs(f2 / m22);
        float val2_2 = flag1 ? float.PositiveInfinity : Math.Abs(f1 / m11);
        if (DoubleUtil.IsZero(m12))
        {
          if (DoubleUtil.IsZero(m21))
          {
            height = val2_1;
            width = val2_2;
          }
          else
          {
            height = Math.Min(0.5f * Math.Abs(f1 / m21), val2_1);
            width = val2_2 - m21 * height / m11;
          }
        }
        else
        {
          width = Math.Min(0.5f * Math.Abs(f2 / m12), val2_2);
          height = val2_1 - m12 * width / m22;
        }
      }
      else if (DoubleUtil.IsZero(m11) || DoubleUtil.IsZero(m22))
      {
        float val2_1 = Math.Abs(f2 / m12);
        float val2_2 = Math.Abs(f1 / m21);
        if (DoubleUtil.IsZero(m11))
        {
          if (DoubleUtil.IsZero(m22))
          {
            height = val2_2;
            width = val2_1;
          }
          else
          {
            height = Math.Min(0.5f * Math.Abs(f2 / m22), val2_2);
            width = val2_1 - m22 * height / m12;
          }
        }
        else
        {
          width = Math.Min(0.5f * Math.Abs(f1 / m11), val2_1);
          height = val2_2 - m11 * width / m21;
        }
      }
      else
      {
        float val2_1 = Math.Abs(f1 / m11);
        float val1_1 = Math.Abs(f1 / m21);
        float val1_2 = Math.Abs(f2 / m12);
        float val2_2 = Math.Abs(f2 / m22);
        width = Math.Min(val1_2, val2_1) * 0.5f;
        height = Math.Min(val1_1, val2_2) * 0.5f;
        if (DoubleUtil.GreaterThanOrClose(val2_1, val1_2) && DoubleUtil.LessThanOrClose(val1_1, val2_2) || DoubleUtil.LessThanOrClose(val2_1, val1_2) && DoubleUtil.GreaterThanOrClose(val1_1, val2_2))
        {
          RectangleF boundingRect = TelerikHelper.GetBoundingRect(new RectangleF(0.0f, 0.0f, width, height), layoutTransform);
          float f3 = Math.Min(f1 / boundingRect.Width, f2 / boundingRect.Height);
          if (!float.IsNaN(f3) && !float.IsInfinity(f3))
          {
            width *= f3;
            height *= f3;
          }
        }
      }
      return new SizeF(width, height);
    }

    private void UpdateLayoutRequest(bool isArrange)
    {
      if (this.layoutManager == null)
        return;
      ContextLayoutManager.LayoutQueue.Request request;
      ILayoutQueue layoutQueue;
      if (isArrange)
      {
        request = this.ArrangeRequest;
        layoutQueue = this.layoutManager.ArrangeQueue;
      }
      else
      {
        request = this.MeasureRequest;
        layoutQueue = this.layoutManager.MeasureQueue;
      }
      if (request == null || this.layoutSuspendCount <= (byte) 0 && !this.layoutManager.IsUpdating)
        return;
      layoutQueue.Remove(this);
    }

    private void InvalidateBoundingRectangle()
    {
      this.cachedBoundingRectangle = Rectangle.Empty;
      this.cachedControlBoundingRectangle = Rectangle.Empty;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Point GetScrollingOffset()
    {
      Size sz = Size.Round(this.PositionOffset);
      if (this.RightToLeft)
        sz.Width = -sz.Width;
      return new Point(sz);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ILayoutManager LayoutManager
    {
      get
      {
        return this.layoutManager;
      }
    }

    [Category("Behavior")]
    public SizeF DesiredSize
    {
      get
      {
        if (this.Visibility == ElementVisibility.Collapsed)
          return SizeF.Empty;
        return this._desiredSize;
      }
    }

    [Category("Behavior")]
    public bool IsLayoutSuspended
    {
      get
      {
        return this.layoutSuspendCount > (byte) 0;
      }
    }

    [Category("Behavior")]
    public virtual Rectangle BoundingRectangle
    {
      get
      {
        if (this.cachedBoundingRectangle == Rectangle.Empty)
          this.cachedBoundingRectangle = this.GetBoundingRectangle(new Rectangle(Point.Round(new PointF(this.Transform.DX, this.Transform.DY)), this.Size));
        return this.cachedBoundingRectangle;
      }
    }

    [Category("Behavior")]
    public virtual Rectangle ControlBoundingRectangle
    {
      get
      {
        if (this.cachedControlBoundingRectangle == Rectangle.Empty)
          this.cachedControlBoundingRectangle = TelerikHelper.GetBoundingRect(new Rectangle(Point.Empty, this.Size), this.TotalTransform);
        return this.cachedControlBoundingRectangle;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool BypassLayoutPolicies
    {
      get
      {
        return this.GetBitState(256L);
      }
      set
      {
        this.SetBitState(256L, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsArrangeValid
    {
      get
      {
        return !this.GetBitState(1024L);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsMeasureValid
    {
      get
      {
        return !this.GetBitState(8192L);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RectangleF PreviousArrangeRect
    {
      get
      {
        return this._finalRect;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public SizeF PreviousConstraint
    {
      get
      {
        return this._previousAvailableSize;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public byte TreeLevel
    {
      get
      {
        return this.treeLevel;
      }
    }

    public void Invalidate()
    {
      if (this.state != ElementState.Loaded || this.elementTree == null)
        return;
      this.elementTree.ComponentTreeHandler.InvalidateElement(this);
    }

    public void Invalidate(bool checkSuspended)
    {
      if (checkSuspended && this.IsLayoutSuspended)
        this.BitState[4194304L] = true;
      else
        this.PerformInvalidate();
    }

    private void PerformInvalidate()
    {
      this.PerformInvalidate(Rectangle.Empty);
    }

    private void PerformInvalidate(Rectangle bounds)
    {
      if (this.state != ElementState.Loaded || this.elementTree == null)
        return;
      if (bounds != Rectangle.Empty)
        this.elementTree.ComponentTreeHandler.InvalidateElement(this, bounds);
      else
        this.elementTree.ComponentTreeHandler.InvalidateElement(this);
    }

    public virtual Rectangle GetInvalidateBounds()
    {
      return this.ControlBoundingRectangle;
    }

    protected virtual void NotifyInvalidate(RadElement invalidatedChild)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public RadMatrix Transform
    {
      get
      {
        if (this.transform.IsEmpty)
        {
          this.transform = RadMatrix.Identity;
          this.PerformTransformation(ref this.transform);
        }
        return this.transform;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [Browsable(false)]
    public RadMatrix TotalTransform
    {
      get
      {
        if (this.totalTransform.IsEmpty)
        {
          this.totalTransform = this.Transform;
          if (this.parent != null)
            this.totalTransform.Multiply(this.parent.TotalTransform, MatrixOrder.Append);
        }
        return this.totalTransform;
      }
    }

    private void InvalidateTotalTransformationReqursively()
    {
      this.InvalidateTotalTransformation();
      RadElementCollection children = this.Children;
      for (int index = 0; index < children.Count; ++index)
        children[index].InvalidateTotalTransformationReqursively();
    }

    private void InvalidateTotalTransformationOnly(bool saveOldMatrix)
    {
      this.totalTransform = RadMatrix.Empty;
      this.InvalidateBoundingRectangle();
    }

    private void InvalidateTotalTransformation()
    {
      this.InvalidateTotalTransformationOnly(true);
      this.totalTransform = RadMatrix.Empty;
    }

    protected virtual void OnTransformationInvalidated()
    {
    }

    private void CallOnTransformationInvalidatedRecursively()
    {
      this.OnTransformationInvalidated();
      for (int index = 0; index < this.Children.Count; ++index)
        this.Children[index].CallOnTransformationInvalidatedRecursively();
    }

    internal void InvalidateLayoutTransforms()
    {
      this.InvalidateOwnTransformation();
      this.InvalidateTotalTransformation();
    }

    private void InvalidateChildTransformations()
    {
      for (int index = 0; index < this.children.Count; ++index)
      {
        RadElement child = this.children[index];
        child.InvalidateOwnTransformation();
        child.InvalidateTotalTransformationOnly(false);
      }
    }

    private void InvalidateOwnTransformation()
    {
      this.InvalidateBoundingRectangle();
      this.transform = RadMatrix.Empty;
    }

    private void InvalidateTransformations(bool invalidateElement)
    {
      if (!this.IsInValidState(true))
        return;
      if (invalidateElement)
      {
        if (RadElement.TraceInvalidation)
          Console.WriteLine("InvalidateTransformations: {0}; ElementBounds:{1}", (object) this.GetType().Name, (object) this.Bounds);
        this.Invalidate();
      }
      this.InvalidateOwnTransformation();
      this.InvalidateTotalTransformationReqursively();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void InvalidateTransformations()
    {
      this.InvalidateTransformations(true);
    }

    protected virtual bool PerformLayoutTransformation(ref RadMatrix matrix)
    {
      if (this.state != ElementState.Loaded)
        return false;
      if (matrix.IsEmpty)
        matrix = RadMatrix.Identity;
      PointF layoutOffset = this.layoutOffset;
      matrix.Translate(layoutOffset.X, layoutOffset.Y, MatrixOrder.Append);
      return !layoutOffset.IsEmpty;
    }

    protected virtual bool PerformPaintTransformation(ref RadMatrix matrix)
    {
      bool flag = false;
      float angleTransform = this.AngleTransform;
      if ((double) angleTransform != 0.0)
      {
        flag = true;
        RectangleF bounds = new RectangleF(PointF.Empty, (SizeF) this.Bounds.Size);
        if (this.GetBitState(67108864L))
          TelerikHelper.PerformCenteredRotation(ref matrix, bounds, angleTransform);
        else
          TelerikHelper.PerformTopLeftRotation(ref matrix, bounds, angleTransform);
      }
      SizeF scaleTransform = this.ScaleTransform;
      if ((double) scaleTransform.Width > 0.0 && (double) scaleTransform.Height > 0.0 && ((double) scaleTransform.Width != 1.0 || (double) scaleTransform.Height != 1.0))
      {
        flag = true;
        matrix.Scale(scaleTransform.Width, scaleTransform.Height, MatrixOrder.Append);
      }
      SizeF positionOffset = this.PositionOffset;
      if (positionOffset != SizeF.Empty)
      {
        flag = true;
        matrix.Translate(this.RightToLeft ? -positionOffset.Width : positionOffset.Width, positionOffset.Height, MatrixOrder.Append);
      }
      return flag;
    }

    private bool PerformTransformation(ref RadMatrix matrix)
    {
      bool flag = this.PerformPaintTransformation(ref matrix);
      if (!this.PerformLayoutTransformation(ref matrix))
        return flag;
      return true;
    }

    protected bool IsInGetAsBitmap()
    {
      for (RadElement radElement = this; radElement != null; radElement = radElement.Parent)
      {
        if (radElement.GetBitState(8L))
          return true;
      }
      return false;
    }

    internal bool IsThisTheTopDisabledItem
    {
      get
      {
        if (this.Enabled)
          return false;
        if (this.Parent != null)
          return this.Parent.Enabled;
        return true;
      }
    }

    protected virtual void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
    }

    protected virtual void PaintElementSkin(IGraphics graphics)
    {
      SystemSkinManager.Instance.PaintCurrentElement((Graphics) graphics.UnderlayGraphics, this.GetSystemSkinPaintBounds());
    }

    protected virtual void PaintOverride(
      IGraphics graphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      scale.Width *= this.ScaleTransform.Width;
      scale.Height *= this.ScaleTransform.Height;
      this.Paint(graphics, clipRectangle, angle + this.AngleTransform, scale, useRelativeTransformation);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Paint(
      IGraphics graphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.Visibility != ElementVisibility.Visible || !this.IsInVisibleClipBounds(clipRectangle))
        return;
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      this.LockBounds();
      object state = graphics.SaveState();
      this.TranformGraphics(underlayGraphics, useRelativeTransformation);
      this.DoOwnPaint(graphics, angle, scale);
      if (this.ClipDrawing)
        this.SetClipping(underlayGraphics);
      this.PaintChildren(graphics, clipRectangle, angle, scale, useRelativeTransformation);
      this.PostPaintChildren(graphics, clipRectangle, angle, scale);
      if (state != null)
        graphics.RestoreState(state);
      this.UnlockBounds();
      if (this.ElementPainted == null)
        return;
      this.ElementPainted((object) this, new PaintEventArgs(underlayGraphics, graphics.ClipRectangle));
    }

    private void DoOwnPaint(IGraphics graphics, float angle, SizeF scale)
    {
      if (!this.ShouldPaint)
        return;
      bool flag = false;
      if (!this.paintSystemSkin.HasValue)
      {
        this.paintSystemSkin = new bool?(this.ShouldPaintSystemSkin());
        flag = true;
      }
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0 && this.PrepareSystemSkin())
      {
        if (flag)
          this.InitializeSystemSkinPaint();
        this.PaintElementSkin(graphics);
      }
      else
      {
        this.PrePaintElement(graphics);
        this.PaintElement(graphics, angle, scale);
        this.PostPaintElement(graphics);
      }
    }

    protected virtual void PostPaintElement(IGraphics graphics)
    {
    }

    protected virtual void PrePaintElement(IGraphics graphics)
    {
      this.BackgroundShape?.Paint(graphics.UnderlayGraphics as Graphics, new RectangleF(PointF.Empty, (SizeF) this.Size), RadControl.EnableDpiScaling ? this.DpiScaleFactor : new SizeF(1f, 1f));
    }

    protected virtual void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      if (!this.IsFocused || this.elementTree == null || !this.elementTree.ComponentTreeHandler.Behavior.ShouldShowFocusCues)
        return;
      this.PaintFocusCues(graphics, clipRectange);
    }

    protected virtual void PaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (!this.GetBitState(4294967296L))
        return;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.ZOrdered))
      {
        if (this.ShouldPaintChild(child))
          this.PaintChild(child, graphics, clipRectange, angle, scale, useRelativeTransformation);
      }
    }

    protected virtual void PaintChild(
      RadElement child,
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      this.PaintShadow(child, graphics);
      child.PaintOverride(graphics, clipRectange, angle, scale, useRelativeTransformation);
    }

    protected virtual void PaintShadow(RadElement child, IGraphics graphics)
    {
      RadItem child1 = child as RadItem;
      if (child1 == null || !child1.EnableElementShadow)
        return;
      this.PaintShadowCore(graphics.UnderlayGraphics as Graphics, child.BoundingRectangle, child1);
    }

    protected virtual void PaintShadowCore(Graphics graphics, Rectangle bounds, RadItem child)
    {
      if (child == null || child.Visibility != ElementVisibility.Visible || child.ShadowDepth == 0)
        return;
      double shadowColorOpacity = this.GetShadowColorOpacity(child);
      Rectangle shadowRect = this.GetShadowRect(ref bounds, child.ShadowDepth);
      Color shadowColor = child.ShadowColor;
      ElementShape elementShape = child.Shape;
      int num = (shadowRect.Height - bounds.Height) / 2;
      if (elementShape == null)
        elementShape = (ElementShape) new RoundRectShape(child.ShadowDepth * 3, true, true, true, true);
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int alpha = (int) (shadowColorOpacity * (double) shadowColor.A / (double) num);
      for (int index = 0; index < num; ++index)
      {
        if (index > 0)
          bounds.Inflate(1, 1);
        using (GraphicsPath path = elementShape.CreatePath(bounds))
        {
          using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, shadowColor)))
            graphics.FillPath((Brush) solidBrush, path);
        }
      }
      graphics.SmoothingMode = smoothingMode;
    }

    private double GetShadowColorOpacity(RadItem item)
    {
      double num;
      switch (item.ShadowDepth)
      {
        case 2:
          num = 0.35;
          break;
        case 3:
          num = 0.4;
          break;
        case 4:
          num = 0.5;
          break;
        case 5:
          num = 0.5;
          break;
        default:
          num = 0.3;
          break;
      }
      return num;
    }

    protected virtual Rectangle GetShadowRect(ref Rectangle elementRect, int shadowDepth)
    {
      int num1;
      int num2;
      int num3;
      switch (shadowDepth)
      {
        case 2:
          num1 = 4;
          num2 = 5;
          num3 = 6;
          break;
        case 3:
          num1 = 4;
          num2 = 6;
          num3 = 10;
          break;
        case 4:
          num1 = 5;
          num2 = 8;
          num3 = 14;
          break;
        case 5:
          num1 = 5;
          num2 = 12;
          num3 = 20;
          break;
        default:
          num1 = 3;
          num2 = 3;
          num3 = 4;
          break;
      }
      Rectangle rectangle = new Rectangle();
      rectangle.X = elementRect.X - num2;
      rectangle.Width = elementRect.Width + 2 * num2;
      rectangle.Y = elementRect.Y - num1;
      rectangle.Height = elementRect.Height + num1 + num3;
      elementRect.Y += (rectangle.Height - elementRect.Height) / 2 - num1;
      return rectangle;
    }

    protected virtual void PaintFocusCues(IGraphics graphics, Rectangle clipRectange)
    {
      Rectangle focusRect = this.GetFocusRect();
      if (focusRect.Width <= 0 || focusRect.Height <= 0)
        return;
      ControlPaint.DrawFocusRectangle((Graphics) graphics.UnderlayGraphics, focusRect);
    }

    public Bitmap GetAsBitmap(
      IGraphics screenRadGraphics,
      Brush backgroundBrush,
      float totalAngle,
      SizeF totalScale)
    {
      if (this.state != ElementState.Loaded)
        return (Bitmap) null;
      Size size = this.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Graphics underlayGraphics = (Graphics) screenRadGraphics.UnderlayGraphics;
      Bitmap destinationImage = new Bitmap(size.Width, size.Height);
      this.BitState[8L] = true;
      this.Paint(screenRadGraphics, new Rectangle(Point.Empty, size)
      {
        Size = this.elementTree.Control.Size
      }, totalAngle, totalScale, true);
      this.BitState[8L] = false;
      TelerikPaintHelper.CopyImageFromGraphics(underlayGraphics, destinationImage);
      return destinationImage;
    }

    public Bitmap GetAsTransformedBitmap(
      IGraphics screenRadGraphics,
      Brush backgroundBrush,
      float totalAngle,
      SizeF totalScale)
    {
      if (this.state != ElementState.Loaded)
        return (Bitmap) null;
      Size size = this.ElementTree.Control.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Graphics underlayGraphics = (Graphics) screenRadGraphics.UnderlayGraphics;
      Bitmap destinationImage = new Bitmap(size.Width, size.Height);
      Rectangle clipRectangle = new Rectangle(Point.Empty, this.ElementTree.Control.Size);
      this.Paint(screenRadGraphics, clipRectangle, totalAngle, totalScale, false);
      TelerikPaintHelper.CopyImageFromGraphics(underlayGraphics, destinationImage);
      return destinationImage;
    }

    public Bitmap GetAsTransformedBitmap(
      Brush backgroundBrush,
      float totalAngle,
      SizeF totalScale)
    {
      return this.GetAsTransformedBitmap(new Rectangle(Point.Empty, this.ElementTree.Control.Size), backgroundBrush, totalAngle, totalScale);
    }

    public Bitmap GetAsTransformedBitmap(
      Rectangle clippingRectangle,
      Brush backgroundBrush,
      float totalAngle,
      SizeF totalScale)
    {
      if (this.state != ElementState.Loaded)
        return (Bitmap) null;
      Rectangle boundingRectangle = this.ControlBoundingRectangle;
      Rectangle rectangle1 = boundingRectangle;
      rectangle1.Intersect(clippingRectangle);
      Size size = rectangle1.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
      Rectangle rectangle2 = this.Parent == null ? this.ControlBoundingRectangle : this.Parent.ControlBoundingRectangle;
      int offsetX = rectangle2.Left - boundingRectangle.Left;
      int offsetY = rectangle2.Top - boundingRectangle.Top;
      if (boundingRectangle.Left < rectangle1.Left)
        offsetX -= rectangle1.Left - boundingRectangle.Left;
      if (boundingRectangle.Top < rectangle1.Top)
        offsetY -= rectangle1.Top - boundingRectangle.Top;
      radGdiGraphics.TranslateTransform(offsetX, offsetY);
      SolidBrush solidBrush = backgroundBrush as SolidBrush;
      if (solidBrush != null)
        graphics.Clear(solidBrush.Color);
      else
        graphics.FillRectangle(backgroundBrush, new Rectangle(Point.Empty, boundingRectangle.Size));
      this.Paint((IGraphics) radGdiGraphics, clippingRectangle, totalAngle, totalScale, true);
      radGdiGraphics.Dispose();
      return bitmap;
    }

    public virtual Bitmap GetAsBitmapEx(Color backColor, float totalAngle, SizeF totalScale)
    {
      if (this.state != ElementState.Loaded)
        return (Bitmap) null;
      Size size = this.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.Clear(backColor);
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
      this.BitState[8L] = true;
      this.Paint((IGraphics) radGdiGraphics, new Rectangle(Point.Empty, size)
      {
        Location = this.LocationToControl()
      }, totalAngle, totalScale, true);
      this.BitState[8L] = false;
      radGdiGraphics.Dispose();
      return bitmap;
    }

    public Bitmap GetAsBitmapEx(Brush backgroundBrush, float totalAngle, SizeF totalScale)
    {
      return this.GetAsBitmapEx(Color.Empty, totalAngle, totalScale);
    }

    public Bitmap GetAsBitmap(Brush backgroundBrush, float totalAngle, SizeF totalScale)
    {
      if (this.state != ElementState.Loaded)
        return (Bitmap) null;
      Size size = this.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
      graphics.FillRectangle(backgroundBrush, new Rectangle(Point.Empty, size));
      this.BitState[8L] = true;
      this.Paint((IGraphics) radGdiGraphics, new Rectangle(Point.Empty, size)
      {
        Size = this.elementTree.Control.Size
      }, totalAngle, totalScale, true);
      this.BitState[8L] = false;
      radGdiGraphics.Dispose();
      return bitmap;
    }

    protected virtual bool IsInVisibleClipBounds(Rectangle clipRectangle)
    {
      return this.GetBitState(8L) || this.ControlBoundingRectangle.IntersectsWith(clipRectangle);
    }

    private Matrix TranformGraphics(Graphics rawGraphics, bool relativeTransform)
    {
      Matrix currentTransform = rawGraphics.Transform;
      RadMatrix paintTransform = this.GetPaintTransform(currentTransform, relativeTransform);
      if (relativeTransform)
      {
        if (this.GetBitState(8L))
          rawGraphics.ResetTransform();
        else
          rawGraphics.Transform = paintTransform.ToGdiMatrix();
      }
      else
      {
        currentTransform = paintTransform.ToGdiMatrix();
        rawGraphics.Transform = currentTransform;
      }
      return currentTransform;
    }

    internal virtual RadMatrix GetPaintTransform(Matrix currentTransform, bool relative)
    {
      RadMatrix radMatrix;
      if (relative)
      {
        radMatrix = new RadMatrix(currentTransform);
        radMatrix.Multiply(this.Transform, MatrixOrder.Prepend);
      }
      else
        radMatrix = this.TotalTransform;
      return radMatrix;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event PaintEventHandler ElementPainted;

    protected virtual void SetClipping(Graphics rawGraphics)
    {
      RectangleF clipRect = this.GetClipRect();
      rawGraphics.SetClip(clipRect, CombineMode.Intersect);
    }

    protected virtual RectangleF GetClipRect()
    {
      return (RectangleF) this.Bounds;
    }

    protected virtual bool ShouldPaintChild(RadElement element)
    {
      return true;
    }

    protected virtual Rectangle GetFocusRect()
    {
      Size size = this.BoundingRectangle.Size;
      if (size.Width < 4 || size.Height < 4)
        return Rectangle.Empty;
      return new Rectangle(new Point(2, 2), new Size(size.Width - 4, size.Height - 4));
    }

    protected ElementShape GetCurrentShape()
    {
      ElementShape shape = this.Shape;
      if (shape == null && this.ShouldPaintUsingParentShape)
      {
        RadElement parent = this.Parent;
        if (parent != null)
          shape = parent.Shape;
      }
      return shape;
    }

    protected virtual bool ShouldPaintUsingParentShape
    {
      get
      {
        return false;
      }
    }

    protected RectangleF GetPaintRectangle(float borderWidth, float angle, SizeF scale)
    {
      Size size = this.Size;
      float num = 0.0f;
      if ((double) borderWidth != 0.0)
      {
        num = (float) Math.Floor((double) borderWidth / 2.0);
        borderWidth = (float) (2.0 * (double) num + 1.0);
      }
      return this.GetPatchedRect(new RectangleF(num, num, (float) size.Width - borderWidth, (float) size.Height - borderWidth), angle, scale);
    }

    protected RectangleF GetPatchedRect(RectangleF rect, float angle, SizeF scale)
    {
      SizeF size = rect.Size;
      RectangleF rectangleF = new RectangleF(new PointF(0.0f, 0.0f), size);
      int num1 = (int) ((double) angle / 360.0);
      angle -= (float) num1 * 360f;
      int num2 = (int) ((double) angle / 90.0);
      if ((double) scale.Width < 2.0 && (double) scale.Height < 2.0)
      {
        if ((double) Math.Abs(angle - (float) num2 * 90f) > 0.01)
          return new RectangleF(rect.Left + 1f, rect.Top + 1f, size.Width - 1f, size.Height - 1f);
        if ((double) angle < -180.0)
          angle += 360f;
        if ((double) angle > 180.0)
          angle -= 360f;
        if ((double) angle > -181.0 && (double) angle < -135.0)
          rectangleF.Location = (PointF) new Point(1, 1);
        else if ((double) angle < -45.0)
          rectangleF.Location = (PointF) new Point(1, 0);
        else if ((double) angle < 45.0)
          rectangleF.Location = (PointF) new Point(0, 0);
        else if ((double) angle < 135.0)
          rectangleF.Location = (PointF) new Point(0, 1);
        else if ((double) angle < 181.0)
          rectangleF.Location = (PointF) new Point(1, 1);
      }
      else
        rectangleF.Size = rect.Size;
      return new RectangleF(rect.Left + rectangleF.Left, rect.Top + rectangleF.Top, size.Width, size.Height);
    }

    public virtual VisualStyleElement GetXPVisualStyle()
    {
      return (VisualStyleElement) null;
    }

    public virtual VisualStyleElement GetVistaVisualStyle()
    {
      return (VisualStyleElement) null;
    }

    [DefaultValue(UseSystemSkinMode.Inherit)]
    [Description("Gets or sets the mode that describes the usage of system skinning (if available).WARNING: This feature is not yet implemented and it will not work as expected.")]
    public virtual UseSystemSkinMode UseSystemSkin
    {
      get
      {
        return this.useSystemSkin;
      }
      set
      {
        if (this.useSystemSkin == value)
          return;
        this.useSystemSkin = value;
        this.OnUseSystemSkinChanged(EventArgs.Empty);
      }
    }

    protected virtual void InitializeSystemSkinPaint()
    {
    }

    protected virtual void UnitializeSystemSkinPaint()
    {
    }

    protected virtual Rectangle GetSystemSkinPaintBounds()
    {
      return this.cachedBounds;
    }

    protected virtual void OnUseSystemSkinChanged(EventArgs e)
    {
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0)
        this.UnitializeSystemSkinPaint();
      this.paintSystemSkin = new bool?();
      foreach (RadElement child in this.children)
        child.OnUseSystemSkinChanged(e);
      this.Invalidate(true);
    }

    protected virtual bool ShouldPaintSystemSkin()
    {
      return false;
    }

    protected virtual bool ComposeShouldPaintSystemSkin()
    {
      return false;
    }

    private bool PrepareSystemSkin()
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual RadProperty MapStyleProperty(
      RadProperty propertyToMap,
      string settingType)
    {
      return (RadProperty) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public virtual bool VsbVisible
    {
      get
      {
        return true;
      }
    }

    public virtual Filter GetStylablePropertiesFilter()
    {
      return (Filter) PropertyFilter.ExcludeFilter;
    }

    public void ResetStyleSettings(bool recursive)
    {
      this.ResetStyleSettings(recursive, (RadProperty) null);
    }

    public virtual void ResetStyleSettings(bool recursive, RadProperty property)
    {
      if (!this.IsInValidState(false))
        return;
      if (property != null)
      {
        int num1 = (int) this.ResetValue(property, ValueResetFlags.Style);
      }
      else
        this.PropertyValues.ResetStyleProperties();
      if (!recursive)
        return;
      foreach (IStylableNode stylableNode in ((IStylableNode) this).ChildrenHierarchy)
      {
        RadObject radObject = stylableNode as RadObject;
        if (property != null)
        {
          int num2 = (int) radObject.ResetValue(property, ValueResetFlags.Style);
        }
        else
          radObject.PropertyValues.ResetStyleProperties();
      }
    }

    protected virtual void ProcessBehaviors(RadPropertyChangedEventArgs e)
    {
      for (int index = 0; index < this.behaviors.Count; ++index)
      {
        PropertyChangeBehavior behavior = this.behaviors[index];
        if (behavior.Property == e.Property)
        {
          if (this.IsDesignMode && e.Property == RadElement.IsMouseDownProperty)
            break;
          behavior.OnPropertyChange(this, e);
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void AddBehavior(PropertyChangeBehavior behavior)
    {
      if (this.behaviors.Contains(behavior))
        return;
      this.behaviors.Add(behavior);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public PropertyChangeBehaviorCollection GetBehaviors()
    {
      return this.behaviors;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void RemoveBehavior(PropertyChangeBehavior behavior)
    {
      this.behaviors.Remove(behavior);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void ClearBehaviors()
    {
      this.behaviors.Clear();
    }

    public void AddRangeBehavior(PropertyChangeBehaviorCollection behaviors)
    {
      this.behaviors.AddRange((IEnumerable<PropertyChangeBehavior>) behaviors);
    }

    protected internal void SetThemeApplied(bool newValue)
    {
      this.BitState[134217728L] = newValue;
    }

    protected virtual void OnStyleChanged(RadPropertyChangedEventArgs e)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SuspendThemeRefresh()
    {
      ++this.suspendThemeRefresh;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResumeThemeRefresh()
    {
      --this.suspendThemeRefresh;
    }

    internal bool IsThemeRefreshSuspended
    {
      get
      {
        return this.suspendThemeRefresh > (byte) 0 || this.state != ElementState.Constructed && this.state != ElementState.Loaded;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual bool CanHaveOwnStyle
    {
      get
      {
        return false;
      }
    }

    protected virtual void UnapplyStyle()
    {
      this.UnapplyParentStyle(this.parent);
    }

    private void UnapplyParentStyle(RadElement parent)
    {
      if (parent == null)
        return;
      this.UnapplyParentStyle(parent.parent);
    }

    public virtual void RemoveRangeBehaviors(
      PropertyChangeBehaviorCollection propertyChangeBehaviorCollection)
    {
      foreach (PropertyChangeBehavior propertyChangeBehavior in (List<PropertyChangeBehavior>) propertyChangeBehaviorCollection)
      {
        for (int index = 0; index < this.behaviors.Count; ++index)
        {
          if (this.behaviors[index] == propertyChangeBehavior)
          {
            this.behaviors.RemoveAt(index);
            --index;
          }
        }
      }
    }

    public virtual void RemoveBehaviors(PropertyChangeBehavior behavior)
    {
      for (int index = 0; index < this.behaviors.Count; ++index)
      {
        if (this.behaviors[index] == behavior)
        {
          this.behaviors.RemoveAt(index);
          --index;
        }
      }
    }

    public virtual void RemoveRangeRoutedEventBehaviors(
      RoutedEventBehaviorCollection routedEventBehaviorCollection)
    {
      foreach (RoutedEventBehavior routedEventBehavior in (List<RoutedEventBehavior>) routedEventBehaviorCollection)
      {
        for (int index = 0; index < this.routedEventBehaviors.Count; ++index)
        {
          if (this.routedEventBehaviors[index].RaisedRoutedEvent.IsSameEvent(routedEventBehavior.RaisedRoutedEvent))
          {
            this.routedEventBehaviors.RemoveAt(index);
            --index;
          }
        }
      }
    }

    [Browsable(false)]
    public virtual bool PropagateStyleToChildren
    {
      get
      {
        return true;
      }
    }

    public System.Type GetThemeEffectiveType()
    {
      return this.ThemeEffectiveType;
    }

    protected virtual System.Type ThemeEffectiveType
    {
      get
      {
        return this.GetType();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual ComponentThemableElementTree ElementTree
    {
      get
      {
        return this.elementTree;
      }
      internal set
      {
        this.elementTree = value;
        if (value == null)
          return;
        this.layoutManager = this.elementTree.ComponentTreeHandler.LayoutManager;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the collection of elements that are child elements in the element tree.")]
    [Browsable(false)]
    [Category("Data")]
    public virtual RadElementCollection Children
    {
      get
      {
        return this.children;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IEnumerable<RadElement> ChildrenHierarchy
    {
      get
      {
        foreach (RadElement child in this.children)
        {
          yield return child;
          foreach (RadElement radElement in child.ChildrenHierarchy)
            yield return radElement;
        }
      }
    }

    public List<RadElement> GetChildrenByType(System.Type type)
    {
      List<RadElement> radElementList = new List<RadElement>(1);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if ((object) this.Children[index].GetType() == (object) type)
          radElementList.Add(this.Children[index]);
      }
      return radElementList;
    }

    public List<RadElement> GetChildrenByBaseType(System.Type type)
    {
      List<RadElement> radElementList = new List<RadElement>(1);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (type.IsAssignableFrom(child.GetType()))
          radElementList.Add(this.Children[index]);
      }
      return radElementList;
    }

    public RadElement FindAncestorByThemeEffectiveType(System.Type themeEffectiveType)
    {
      for (RadElement parent = this.parent; parent != null; parent = parent.parent)
      {
        if (parent.ThemeEffectiveType.Equals(themeEffectiveType))
          return parent;
      }
      return (RadElement) null;
    }

    public T FindAncestor<T>() where T : RadElement
    {
      for (RadElement parent = this.parent; parent != null; parent = parent.parent)
      {
        if (parent is T)
          return (T) parent;
      }
      return default (T);
    }

    public bool IsAncestorOf(RadElement element)
    {
      for (RadElement parent = element.parent; parent != null; parent = parent.parent)
      {
        if (parent == this)
          return true;
      }
      return false;
    }

    public T FindDescendant<T>() where T : RadElement
    {
      Queue<RadElement> radElementQueue = new Queue<RadElement>();
      radElementQueue.Enqueue(this);
      while (radElementQueue.Count > 0)
      {
        foreach (RadElement child in radElementQueue.Dequeue().children)
        {
          if (child is T)
            return (T) child;
          radElementQueue.Enqueue(child);
        }
      }
      return default (T);
    }

    public RadElement FindDescendant(Predicate<RadElement> criteria)
    {
      if (criteria == null)
        return this;
      Queue<RadElement> radElementQueue = new Queue<RadElement>();
      radElementQueue.Enqueue(this);
      while (radElementQueue.Count > 0)
      {
        RadElement radElement = radElementQueue.Dequeue();
        if (criteria(radElement))
          return radElement;
        foreach (RadElement child in radElement.children)
          radElementQueue.Enqueue(child);
      }
      return (RadElement) null;
    }

    public RadElement FindDescendant(System.Type descendantType)
    {
      if ((object) descendantType == null)
        return this;
      Queue<RadElement> radElementQueue = new Queue<RadElement>();
      radElementQueue.Enqueue(this);
      while (radElementQueue.Count > 0)
      {
        RadElement radElement = radElementQueue.Dequeue();
        if ((object) radElement.GetType() == (object) descendantType)
          return radElement;
        foreach (RadElement child in radElement.children)
          radElementQueue.Enqueue(child);
      }
      return (RadElement) null;
    }

    public IEnumerable<RadElement> EnumDescendants(
      TreeTraversalMode traverseMode)
    {
      return this.EnumDescendants((Filter) null, traverseMode);
    }

    public IEnumerable<RadElement> EnumDescendants(
      Predicate<RadElement> predicate,
      TreeTraversalMode traverseMode)
    {
      if (traverseMode == TreeTraversalMode.BreadthFirst)
      {
        Queue<RadElement> children = new Queue<RadElement>();
        children.Enqueue(this);
        while (children.Count > 0)
        {
          RadElement child = children.Dequeue();
          foreach (RadElement child1 in child.children)
          {
            if (predicate == null)
              yield return child1;
            else if (predicate(child1))
              yield return child1;
            children.Enqueue(child1);
          }
        }
      }
      else
      {
        foreach (RadElement child in this.children)
        {
          if (predicate == null)
            yield return child;
          else if (predicate(child))
            yield return child;
          child.EnumDescendants(predicate, traverseMode);
        }
      }
    }

    public IEnumerable<RadElement> EnumDescendants(
      Filter filter,
      TreeTraversalMode traverseMode)
    {
      if (traverseMode == TreeTraversalMode.BreadthFirst)
      {
        Queue<RadElement> children = new Queue<RadElement>();
        children.Enqueue(this);
        while (children.Count > 0)
        {
          RadElement child = children.Dequeue();
          foreach (RadElement child1 in child.children)
          {
            if (filter == null)
              yield return child1;
            else if (filter.Match((object) child1))
              yield return child1;
            children.Enqueue(child1);
          }
        }
      }
      else
      {
        foreach (RadElement child in this.children)
        {
          if (filter == null)
            yield return child;
          else if (filter.Match((object) child))
            yield return child;
          child.EnumDescendants(filter, traverseMode);
        }
      }
    }

    public List<RadElement> GetDescendants(
      Predicate<RadElement> predicate,
      TreeTraversalMode traverseMode)
    {
      return new List<RadElement>(this.EnumDescendants(predicate, traverseMode));
    }

    public List<RadElement> GetDescendants(
      Filter filter,
      TreeTraversalMode traverseMode)
    {
      return new List<RadElement>(this.EnumDescendants(filter, traverseMode));
    }

    public IEnumerable<RadElement> GetAncestors(Filter filter)
    {
      for (RadElement parent = this.parent; parent != null; parent = parent.parent)
      {
        if (filter == null)
          yield return parent;
        if (filter.Match((object) parent))
          yield return parent;
      }
    }

    public IEnumerable<RadElement> GetAncestors(Predicate<RadElement> predicate)
    {
      for (RadElement parent = this.parent; parent != null; parent = parent.parent)
      {
        if (predicate == null)
          yield return parent;
        if (predicate(parent))
          yield return parent;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement Parent
    {
      get
      {
        return this.parent;
      }
    }

    public virtual void RaiseRoutedEvent(RadElement sender, RoutedEventArgs args)
    {
      this.RaiseTunnelEvent(sender, args);
      if (args.Canceled)
        return;
      this.RaiseBubbleEvent(sender, args);
    }

    public virtual void RaiseTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      this.OnTunnelEvent(sender, args);
      args.Direction = RoutingDirection.Tunnel;
      if (args.Canceled)
        return;
      this.SetEventProcessed(sender, args);
      this.PocessRootedEventBehaviors(sender, args);
      this.SetEventNotProcessed(sender, args);
      for (int index = 0; index < this.children.Count; ++index)
      {
        this.children[index].RaiseTunnelEvent(sender, args);
        if (args.Canceled)
          break;
      }
    }

    public virtual void RaiseBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      bool isItemHovered = this.IsItemHovered;
      this.OnBubbleEvent(sender, args);
      args.Direction = RoutingDirection.Bubble;
      if (!args.Canceled)
      {
        this.SetEventProcessed(sender, args);
        this.PocessRootedEventBehaviors(sender, args);
        this.SetEventNotProcessed(sender, args);
        if (this.Parent != null)
          this.Parent.RaiseBubbleEvent(sender, args);
      }
      if (args.Canceled || sender != this || (args.RoutedEvent != RadElement.MouseDownEvent || this.ClickMode != ClickMode.Press) && (args.RoutedEvent != RadElement.MouseUpEvent || this.ClickMode != ClickMode.Release || !isItemHovered || this.ElementTree != null && this.ElementTree.ComponentTreeHandler != null && (this.ElementTree.ComponentTreeHandler.Behavior != null && this.ElementTree.ComponentTreeHandler.Behavior.pressedElement != this)))
        return;
      MouseEventArgs originalEventArgs = (MouseEventArgs) args.OriginalEventArgs;
      if (originalEventArgs.Button == MouseButtons.None)
        return;
      RoutedEventArgs args1 = !this.GetBitState(17179869184L) || !this.DoubleClickEnabled ? new RoutedEventArgs((EventArgs) originalEventArgs, RadElement.MouseClickedEvent) : new RoutedEventArgs((EventArgs) originalEventArgs, RadElement.MouseDoubleClickedEvent);
      this.RaiseRoutedEvent(this, args1);
      if (args1.Canceled)
        return;
      if (args1.RoutedEvent == RadElement.MouseDoubleClickedEvent)
      {
        this.DoDoubleClick(args1.OriginalEventArgs);
      }
      else
      {
        if (args1.RoutedEvent != RadElement.MouseClickedEvent)
          return;
        this.DoClick(args1.OriginalEventArgs);
      }
    }

    private void SetEventProcessed(RadElement sender, RoutedEventArgs args)
    {
      this.processedEvents.Add((object) new RadElement.ProcessedEvent(sender, args));
    }

    private void SetEventNotProcessed(RadElement sender, RoutedEventArgs args)
    {
      for (int index = 0; index < this.processedEvents.Count; ++index)
      {
        RadElement.ProcessedEvent processedEvent = (RadElement.ProcessedEvent) this.processedEvents[index];
        if (processedEvent.Sender == sender && processedEvent.Args == args)
        {
          this.processedEvents.RemoveAt(index);
          --index;
        }
      }
    }

    protected virtual void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
    }

    protected virtual void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      RadElement radElement = (RadElement) null;
      if (sender.NotifyParentOnMouseInput && (args.RoutedEvent == RadElement.MouseDownEvent || args.RoutedEvent == RadElement.MouseUpEvent || (args.RoutedEvent == RadElement.MouseClickedEvent || args.RoutedEvent == RadElement.MouseDoubleClickedEvent) || args.RoutedEvent == RadElement.MouseWheelEvent))
      {
        for (RadElement parent = sender.Parent; parent != null; parent = parent.Parent)
        {
          if (parent.ShouldHandleMouseInput)
          {
            radElement = parent;
            break;
          }
        }
      }
      if (sender != this && radElement != this)
        return;
      this.OnCLREventsRise(args);
    }

    [Browsable(false)]
    public RoutedEventBehaviorCollection RoutedEventBehaviors
    {
      get
      {
        return this.routedEventBehaviors;
      }
    }

    private void PocessRootedEventBehaviors(RadElement sender, RoutedEventArgs args)
    {
      foreach (RoutedEventBehavior routedEventBehavior in (List<RoutedEventBehavior>) this.routedEventBehaviors)
      {
        if (routedEventBehavior.RaisedRoutedEvent.IsSameEvent(sender, args))
          routedEventBehavior.OnEventOccured(sender, this, args);
      }
    }

    public bool IsEventInProcess(RaisedRoutedEvent raisedEvent)
    {
      for (int index = 0; index < this.processedEvents.Count; ++index)
      {
        RadElement.ProcessedEvent processedEvent = (RadElement.ProcessedEvent) this.processedEvents[index];
        if (raisedEvent.IsSameEvent(processedEvent.Sender, processedEvent.Args))
          return true;
      }
      return false;
    }

    public static RoutedEvent RegisterRoutedEvent(string eventName, System.Type ownerType)
    {
      RadElement.EnsureRegisteredRoutedEvents();
      RoutedEvent routedEvent = new RoutedEvent(eventName, ownerType);
      routedEvent.EventName = eventName;
      routedEvent.OwnerType = ownerType;
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(eventName, ownerType);
      RadElement.registeredRoutedEvents[(object) fromNameKey] = (object) routedEvent;
      return routedEvent;
    }

    private static void EnsureRegisteredRoutedEvents()
    {
      if (RadElement.registeredRoutedEvents != null)
        return;
      RadElement.registeredRoutedEvents = new HybridDictionary();
    }

    public static RoutedEvent GetRegisterRoutedEvent(string eventName, System.Type ownerType)
    {
      RadElement.EnsureRegisteredRoutedEvents();
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(eventName, ownerType);
      return (RoutedEvent) RadElement.registeredRoutedEvents[(object) fromNameKey];
    }

    public static RoutedEvent GetRegisterRoutedEvent(string eventName, string className)
    {
      RadElement.EnsureRegisteredRoutedEvents();
      System.Type typeByName = RadTypeResolver.Instance.GetTypeByName(className);
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(eventName, typeByName);
      return (RoutedEvent) RadElement.registeredRoutedEvents[(object) fromNameKey];
    }

    public RoutedEvent GetRegisterRoutedEvent(string eventName)
    {
      RadElement.EnsureRegisteredRoutedEvents();
      RadProperty.FromNameKey fromNameKey = new RadProperty.FromNameKey(eventName, this.GetType());
      return (RoutedEvent) RadElement.registeredRoutedEvents[(object) fromNameKey];
    }

    private void UpdateZOrderedCollection(RadElement child, ItemsChangeOperation change)
    {
      this.zOrderedChildren.ResetCachedIndices();
      switch (change)
      {
        case ItemsChangeOperation.Inserted:
          this.zOrderedChildren.Add(child);
          break;
        case ItemsChangeOperation.Removed:
          this.zOrderedChildren.Remove(child);
          break;
        case ItemsChangeOperation.Set:
          this.zOrderedChildren.OnElementSet(child);
          break;
        case ItemsChangeOperation.Cleared:
          this.zOrderedChildren.Clear();
          break;
        case ItemsChangeOperation.BatchInsert:
          this.zOrderedChildren.Reset();
          break;
      }
    }

    protected virtual void OnChildrenChanged(RadElement child, ItemsChangeOperation changeOperation)
    {
      ChildrenChangedEventHandler changedEventHandler = (ChildrenChangedEventHandler) this.Events[RadElement.ChildrenChangedKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new ChildrenChangedEventArgs(child, changeOperation));
    }

    public IEnumerable<RadElement> GetChildren(ChildrenListOptions options)
    {
      bool includeCollapsed = (options & ChildrenListOptions.IncludeCollapsed) == ChildrenListOptions.IncludeCollapsed;
      bool onlyVisible = (options & ChildrenListOptions.IncludeOnlyVisible) == ChildrenListOptions.IncludeOnlyVisible;
      bool reversedOrder = (options & ChildrenListOptions.ReverseOrder) == ChildrenListOptions.ReverseOrder;
      if ((options & ChildrenListOptions.ZOrdered) == ChildrenListOptions.ZOrdered)
      {
        List<RadElement> elements = this.zOrderedChildren.Elements;
        int count = elements.Count;
        RadElement element;
        if (reversedOrder)
        {
          for (int i = count - 1; i >= 0; --i)
          {
            element = elements[i];
            ElementVisibility visibility = element.Visibility;
            if ((visibility != ElementVisibility.Collapsed || includeCollapsed) && (visibility != ElementVisibility.Hidden || !onlyVisible))
              yield return element;
          }
        }
        else
        {
          for (int i = 0; i < count; ++i)
          {
            element = elements[i];
            ElementVisibility visibility = element.Visibility;
            if ((visibility != ElementVisibility.Collapsed || includeCollapsed) && (visibility != ElementVisibility.Hidden || !onlyVisible))
              yield return element;
          }
        }
      }
      else
      {
        RadElementCollection list = this.Children;
        int count = list.Count;
        RadElement child;
        ElementVisibility visibility;
        if (reversedOrder)
        {
          for (int i = count - 1; i >= 0; --i)
          {
            child = list[i];
            visibility = child.Visibility;
            if ((visibility != ElementVisibility.Collapsed || includeCollapsed) && (visibility != ElementVisibility.Hidden || !onlyVisible))
              yield return child;
          }
        }
        else
        {
          for (int i = 0; i < count; ++i)
          {
            child = list[i];
            visibility = child.Visibility;
            if ((visibility != ElementVisibility.Collapsed || includeCollapsed) && (visibility != ElementVisibility.Hidden || !onlyVisible))
              yield return child;
          }
        }
      }
    }

    [Browsable(false)]
    public int LayoutableChildrenCount
    {
      get
      {
        return this.zOrderedChildren.LayoutableCount;
      }
    }

    public void SendToBack()
    {
      if (this.parent == null)
        return;
      this.parent.zOrderedChildren.SendToBack(this);
    }

    public void BringToFront()
    {
      if (this.parent == null)
        return;
      this.parent.zOrderedChildren.BringToFront(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual RadElement GetChildAt(int index)
    {
      return this.Children[index];
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool IsChildOf(RadElement parent)
    {
      if (parent.elementTree != this.elementTree)
        return false;
      for (RadElement parent1 = this.Parent; parent1 != null; parent1 = parent1.Parent)
      {
        if (parent1 == parent)
          return true;
      }
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public event EventHandler MouseHover
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseHoverEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseHoverEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event MouseEventHandler MouseMove
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseMoveEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseMoveEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    public event MouseEventHandler MouseDown
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseDownEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseDownEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public event MouseEventHandler MouseUp
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseUpEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseUpEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the element is clicked.")]
    [Browsable(true)]
    [Category("Action")]
    public event EventHandler Click
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseClickEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseClickEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the element is double-clicked.")]
    [Category("Action")]
    [Browsable(true)]
    public virtual event EventHandler DoubleClick
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseDoubleClickEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseDoubleClickEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public event EventHandler MouseEnter
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseEnterEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseEnterEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the RadItem has focus and the user presses a key down")]
    [Category("Action")]
    public event MouseEventHandler MouseWheel
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseWheelEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseWheelEventKey, (Delegate) value);
      }
    }

    public event EventHandler EnabledChanged
    {
      add
      {
        this.Events.AddHandler(RadElement.EnabledChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.EnabledChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler MouseLeave
    {
      add
      {
        this.Events.AddHandler(RadElement.MouseLeaveEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.MouseLeaveEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event ChildrenChangedEventHandler ChildrenChanged
    {
      add
      {
        this.Events.AddHandler(RadElement.ChildrenChangedKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.ChildrenChangedKey, (Delegate) value);
      }
    }

    public event MouseEventHandler LostMouseCapture
    {
      add
      {
        this.Events.AddHandler(RadElement.LostMouseCaptureKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadElement.LostMouseCaptureKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ContainsFocus
    {
      get
      {
        return (bool) this.GetValue(RadElement.ContainsFocusProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(RadElement.ContainsFocusProperty, (object) value);
      }
    }

    public virtual bool Focus()
    {
      return this.Focus(true);
    }

    protected bool Focus(bool setParentControlFocus)
    {
      bool flag = false;
      if (!this.CanFocus || this.elementTree == null)
        return flag;
      if (setParentControlFocus && this.ElementTree.ComponentTreeHandler.OnFocusRequested(this))
      {
        this.SetFocusPropertySafe(true);
        flag = true;
      }
      else if (!this.IsFocused)
      {
        this.SetFocusPropertySafe(true);
        flag = true;
      }
      return flag;
    }

    protected void SetFocusPropertySafe(bool isFocused)
    {
      if (this.elementTree != null)
        this.ElementTree.ComponentTreeHandler.Behavior.SettingElementFocused(this, isFocused);
      this.SetElementFocused(isFocused);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetElementFocused(bool isFocused)
    {
      this.BitState[128L] = false;
      int num = (int) this.SetValue(RadElement.IsFocusedProperty, (object) isFocused);
      this.BitState[128L] = true;
    }

    protected virtual void KillFocus()
    {
      this.SetFocusPropertySafe(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void SetFocus()
    {
      if (!this.GetBitState(8589934592L))
        return;
      this.Focus();
    }

    protected virtual void OnCLREventsRise(RoutedEventArgs args)
    {
      if (this.state != ElementState.Loaded)
        return;
      MouseEventArgs originalEventArgs = args.OriginalEventArgs as MouseEventArgs;
      if (args.RoutedEvent == RadElement.MouseDownEvent)
        this.OnMouseDown(originalEventArgs);
      else if (args.RoutedEvent == RadElement.MouseUpEvent)
      {
        this.OnMouseUp(originalEventArgs);
      }
      else
      {
        if (args.RoutedEvent != RadElement.MouseWheelEvent)
          return;
        this.DoMouseWheel(originalEventArgs);
      }
    }

    protected virtual void OnMouseMove(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[RadElement.MouseMoveEventKey];
      if (mouseEventHandler != null)
        mouseEventHandler((object) this, e);
      if (e.Button == MouseButtons.None || this.ClickMode != ClickMode.Hover)
        return;
      if (e.Clicks > 1)
        this.OnDoubleClick((EventArgs) e);
      else
        this.OnClick((EventArgs) e);
    }

    protected virtual void OnMouseHover(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.MouseHoverEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnEnabledChanged(RadPropertyChangedEventArgs e)
    {
      bool newValue = (bool) e.NewValue;
      if (this.Visibility == ElementVisibility.Visible && newValue && this.state == ElementState.Loaded)
      {
        this.IsMouseOver = this.ControlBoundingRectangle.Contains(this.elementTree.Control.PointToClient(Control.MousePosition));
      }
      else
      {
        this.IsMouseDown = false;
        this.IsMouseOver = false;
        this.IsMouseOverElement = false;
        this.SetElementFocused(false);
        this.ContainsFocus = false;
        this.ContainsMouse = false;
      }
      if (this.suspendRecursiveEnabledUpdates == (byte) 0)
      {
        foreach (RadElement child in this.Children)
          child.OnParentEnabledChanged(e);
      }
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.EnabledChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, (EventArgs) e);
    }

    protected virtual void OnParentEnabledChanged(RadPropertyChangedEventArgs e)
    {
      this.UpdateEnabledFromParent();
    }

    protected virtual void OnMouseDown(MouseEventArgs e)
    {
      if (this.captureOnMouseDown)
        this.Capture = true;
      this.SetFocus();
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[RadElement.MouseDownEventKey];
      if (mouseEventHandler != null)
        mouseEventHandler((object) this, e);
      this.BitState[17179869184L] = e.Clicks > 1;
    }

    protected virtual void OnMouseUp(MouseEventArgs e)
    {
      if (this.captureOnMouseDown)
        this.Capture = false;
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[RadElement.MouseUpEventKey];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler((object) this, e);
    }

    protected virtual void OnLostMouseCapture(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[RadElement.LostMouseCaptureKey];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.MouseClickEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDoubleClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.MouseDoubleClickEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnMouseEnter(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.MouseEnterEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnMouseLeave(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadElement.MouseLeaveEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    public virtual void PerformClick()
    {
      this.DoClick(EventArgs.Empty);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseWheel(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[RadElement.MouseWheelEventKey];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler((object) this, e);
    }

    protected virtual void DoMouseWheel(MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    protected virtual void DoClick(EventArgs e)
    {
      this.OnClick(e);
      if (this.ElementState != ElementState.Loaded)
        return;
      this.ElementTree.Control.Invalidate();
    }

    protected virtual void DoDoubleClick(EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseWheel(MouseEventArgs e)
    {
      this.DoMouseWheel(e);
    }

    protected internal virtual bool IsInputKey(InputKeyEventArgs e)
    {
      return false;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool ShouldHandleMouseInput
    {
      get
      {
        return this.GetBitState(64L);
      }
      set
      {
        this.SetBitState(64L, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool NotifyParentOnMouseInput
    {
      get
      {
        return this.GetBitState(32L);
      }
      set
      {
        this.SetBitState(32L, value);
      }
    }

    protected internal bool IsAbleToRespondToMouseEvents
    {
      get
      {
        if (this.state != ElementState.Loaded)
          return false;
        return this.Enabled;
      }
    }

    protected virtual MouseEventArgs MouseEventArgsFromControl(MouseEventArgs e)
    {
      Point point = this.PointFromControl(e.Location);
      return new MouseEventArgs(e.Button, e.Clicks, point.X, point.Y, e.Delta);
    }

    protected virtual void DoMouseDown(MouseEventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.IsMouseDown = true;
      this.RaiseRoutedEvent(this, new RoutedEventArgs((EventArgs) e, RadElement.MouseDownEvent));
    }

    protected virtual void DoMouseUp(MouseEventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.IsMouseDown = false;
      this.RaiseRoutedEvent(this, new RoutedEventArgs((EventArgs) e, RadElement.MouseUpEvent));
    }

    protected virtual void DoMouseMove(MouseEventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.OnMouseMove(e);
      if (this.ShouldHandleMouseInput && this.NotifyParentOnMouseInput && this.Parent != null)
        this.Parent.DoMouseMove(e);
      foreach (RadElement child in this.Children)
      {
        if (!child.ShouldHandleMouseInput)
          child.DoMouseMove(e);
      }
    }

    protected virtual void DoMouseHover(EventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.OnMouseHover(e);
      if (this.ElementTree == null)
        return;
      ComponentInputBehavior behavior = this.ElementTree.ComponentTreeHandler.Behavior;
      ScreenTipNeededEventArgs args = behavior.CallOnScreenTipNeeded(this);
      if (this.ScreenTip != null)
      {
        behavior.UpdateScreenTip(args);
      }
      else
      {
        ToolTipTextNeededEventArgs textNeededEventArgs = behavior.OnToolTipTextNeeded(this);
        string toolTipText = textNeededEventArgs.ToolTipText;
        if (this.ToolTipText != toolTipText)
          this.ToolTipText = toolTipText;
        if (string.IsNullOrEmpty(this.ToolTipText))
        {
          for (RadElement parent = this.parent; parent != null; parent = parent.Parent)
          {
            if (!string.IsNullOrEmpty(parent.ToolTipText))
            {
              this.ToolTipText = parent.ToolTipText;
              break;
            }
          }
        }
        if (string.IsNullOrEmpty(this.ToolTipText))
          return;
        behavior.UpdateToolTip(this, textNeededEventArgs.Offset);
      }
    }

    internal void CallRaiseMouseWheel(MouseEventArgs e)
    {
      this.RaiseMouseWheel(e);
    }

    internal void CallOnLostMouseCapture(MouseEventArgs e)
    {
      this.OnLostMouseCapture(e);
    }

    protected virtual void RaiseClick(EventArgs e)
    {
      this.RaiseRoutedEvent(this, new RoutedEventArgs(e, RadElement.MouseClickedEvent));
    }

    protected virtual void RaiseDoubleClick(EventArgs e)
    {
      this.RaiseRoutedEvent(this, new RoutedEventArgs(e, RadElement.MouseDoubleClickedEvent));
    }

    protected virtual void RaiseMouseWheel(MouseEventArgs e)
    {
      this.RaiseRoutedEvent(this, new RoutedEventArgs((EventArgs) e, RadElement.MouseWheelEvent));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void UpdateContainsMouse()
    {
      if (this.state != ElementState.Loaded)
        this.ContainsMouse = false;
      else if (this.RectangleToScreen(this.Bounds).Contains(Control.MousePosition))
      {
        Control controlUnderMouse = ControlHelper.GetControlUnderMouse();
        this.ContainsMouse = controlUnderMouse != null && (controlUnderMouse == this.elementTree.Control || ControlHelper.IsDescendant(this.elementTree.Control, controlUnderMouse));
      }
      else
        this.ContainsMouse = false;
      if (this.parent == null)
        return;
      this.parent.UpdateContainsMouse();
    }

    protected virtual void UpdateContainsFocus(bool isFocused)
    {
      this.ContainsFocus = isFocused;
      if (this.parent == null)
        return;
      this.parent.UpdateContainsFocus(isFocused);
    }

    protected virtual void DoMouseEnter(EventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.IsMouseOver = true;
      this.OnMouseEnter(e);
    }

    protected virtual void DoMouseLeave(EventArgs e)
    {
      if (!this.IsAbleToRespondToMouseEvents)
        return;
      this.IsMouseOver = false;
      this.OnMouseLeave(e);
      if (this.ElementTree == null)
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.UpdateToolTip((RadElement) null);
      this.ElementTree.ComponentTreeHandler.Behavior.UpdateScreenTip((ScreenTipNeededEventArgs) null);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseDown(MouseEventArgs e)
    {
      this.DoMouseDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseUp(MouseEventArgs e)
    {
      this.DoMouseUp(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseMove(MouseEventArgs e)
    {
      this.DoMouseMove(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseHover(EventArgs e)
    {
      this.DoMouseHover(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseEnter(EventArgs e)
    {
      this.DoMouseEnter(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoMouseLeave(EventArgs e)
    {
      this.DoMouseLeave(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoClick(EventArgs e)
    {
      this.DoClick(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallDoDoubleClick(EventArgs e)
    {
      this.DoDoubleClick(e);
    }

    public override object GetValue(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, true);
      if (this.elementTree != null && entry.StyleVersion != this.elementTree.StyleVersion)
      {
        switch (this.GetValueSource(property))
        {
          case ValueSource.Style:
            int num1 = (int) this.ResetValue(property, ValueResetFlags.Style);
            break;
          case ValueSource.Animation:
            if (entry.AnimationSetting != null && entry.AnimationSetting.IsStyleSetting)
            {
              int num2 = (int) this.ResetValue(property, ValueResetFlags.Animation);
              break;
            }
            break;
        }
      }
      return entry.GetCurrentValue(true);
    }

    protected internal override ValueUpdateResult AddStylePropertySetting(
      IPropertySetting setting)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(setting.Property, true);
      if (this.elementTree != null)
        entry.StyleVersion = this.elementTree.StyleVersion;
      if (((PropertySetting) setting).EndValue == null)
        return this.SetValueCore(entry, (object) setting, (object) null, ValueSource.Style);
      return ValueUpdateResult.NotUpdated;
    }

    protected override ValueUpdateResult SetValueCore(
      RadPropertyValue propVal,
      object propModifier,
      object newValue,
      ValueSource source)
    {
      if (source == ValueSource.Local && this.state == ElementState.Constructing)
        source = ValueSource.DefaultValueOverride;
      return base.SetValueCore(propVal, propModifier, newValue, source);
    }

    private void InvalidateCachedValues(RadProperty property)
    {
      if (this.state != ElementState.Loaded)
        return;
      if (property == RadElement.BorderThicknessProperty)
        this.InvalidateChildTransformations();
      else if (property == RadElement.PaddingProperty)
        this.InvalidateChildTransformations();
      else if (property == RadElement.RightToLeftProperty)
        this.InvalidateTransformations(false);
      else if (property == RadElement.AngleTransformProperty)
        this.InvalidateBoundingRectangle();
      else if (property == RadElement.PositionOffsetProperty)
      {
        this.InvalidateTransformations();
        this.CallOnTransformationInvalidatedRecursively();
      }
      else
      {
        if (property != RadElement.ScaleTransformProperty)
          return;
        this.InvalidateBoundingRectangle();
      }
    }

    private bool CheckBoxProperty(string propertyName)
    {
      return propertyName == "Width" || propertyName == "LeftWidth" || (propertyName == "TopWidth" || propertyName == "RightWidth") || propertyName == "BottomWidth";
    }

    protected virtual void NotifyChildren(RadPropertyChangedEventArgs e)
    {
      RadElementCollection children = this.Children;
      for (int index = 0; index < children.Count; ++index)
        children[index].OnParentPropertyChanged(e);
    }

    private void SetControlPropertyValue(
      RadElement parentElement,
      RadProperty property,
      object value)
    {
      RadElementCollection children = parentElement.Children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
      {
        RadElement parentElement1 = children[index];
        if (!parentElement1.ShouldHandleMouseInput)
        {
          int num = (int) parentElement1.SetValue(property, value);
          this.SetControlPropertyValue(parentElement1, property, value);
        }
      }
    }

    protected virtual void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
    }

    protected virtual void OnLocationChanged(RadPropertyChangedEventArgs e)
    {
    }

    protected virtual void OnDisplayPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (this.state != ElementState.Loaded)
        return;
      this.PerformInvalidate();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      RadElementPropertyMetadata metadata = e.Metadata as RadElementPropertyMetadata;
      if (metadata == null)
        return;
      if (metadata.IsInherited)
        this.NotifyChildren(e);
      this.ProcessBehaviors(e);
      this.InvalidateCachedValues(e.Property);
      if (e.Property == RadElement.IsMouseOverProperty)
        this.UpdateContainsMouse();
      else if (e.Property == RadElement.VisibilityProperty)
      {
        if (this.parent != null)
          this.parent.zOrderedChildren.OnElementVisibilityChanged(this);
        this.RaiseTunnelEvent(this, new RoutedEventArgs(EventArgs.Empty, RadElement.VisibilityChangingEvent));
        if (this.state == ElementState.Loaded)
        {
          if (ElementVisibility.Collapsed == (ElementVisibility) e.OldValue || ElementVisibility.Collapsed == (ElementVisibility) e.NewValue)
          {
            this.Parent?.OnChildDesiredSizeChanged(this);
            this.Invalidate();
            this.OnLayoutPropertyChanged(e);
          }
          else
            this.OnDisplayPropertyChanged(e);
        }
      }
      else if (this.state == ElementState.Loaded)
      {
        if (metadata.AffectsLayout || metadata.AffectsMeasure || metadata.AffectsArrange)
          this.OnLayoutPropertyChanged(e);
        if (metadata.AffectsDisplay)
          this.OnDisplayPropertyChanged(e);
      }
      if (e.Property == RadElement.IsMouseOverProperty || e.Property == RadElement.IsMouseDownProperty)
      {
        if (!this.ShouldHandleMouseInput)
          return;
        this.SetControlPropertyValue(this, e.Property, e.NewValue);
      }
      else if (e.Property == RadElement.IsFocusedProperty)
      {
        if (this.GetBitState(128L))
          throw new InvalidOperationException("The property IsFocused can be set only by the method Focus()");
        this.UpdateContainsFocus((bool) e.NewValue);
      }
      else if (e.Property == RadElement.ZIndexProperty)
      {
        if (this.parent == null)
          return;
        this.parent.zOrderedChildren.OnElementZIndexChanged(this);
      }
      else if (e.Property == RadElement.EnabledProperty)
        this.OnEnabledChanged(e);
      else if (e.Property == RadElement.StyleProperty)
      {
        this.PropertyValues.ResetStyleProperties();
        (e.NewValue as StyleSheet)?.Apply((RadObject) this, true);
      }
      else
      {
        if (e.Property != RadElement.ClassProperty || this is RadItem || this.elementTree == null)
          return;
        this.elementTree.ApplyThemeToElement((RadObject) this);
      }
    }

    protected override bool CanRaisePropertyChangeNotifications(RadPropertyValue propVal)
    {
      if (!base.CanRaisePropertyChangeNotifications(propVal))
        return false;
      if (this.state != ElementState.Constructed && this.state != ElementState.Loaded)
        return this.state == ElementState.Unloaded;
      return true;
    }

    protected override bool IsPropertyCancelable(RadPropertyMetadata metadata)
    {
      RadElementPropertyMetadata propertyMetadata = metadata as RadElementPropertyMetadata;
      if (propertyMetadata != null)
        return propertyMetadata.Cancelable;
      return base.IsPropertyCancelable(metadata);
    }

    internal void UpdateInheritanceChain(bool recursive)
    {
      this.PropertyValues.ResetInheritableProperties();
      this.paintSystemSkin = new bool?();
      if (!recursive)
        return;
      int count = this.children.Count;
      for (int index = 0; index < count; ++index)
        this.children[index].UpdateInheritanceChain(recursive);
    }

    protected internal virtual void OnParentPropertyChanged(RadPropertyChangedEventArgs args)
    {
      if (this.parent == null || !args.Metadata.IsInherited)
        return;
      RadPropertyValue entry = this.PropertyValues.GetEntry(args.Property, false);
      if (entry == null)
      {
        RadElementPropertyMetadata metadata = args.Metadata as RadElementPropertyMetadata;
        if (metadata != null && (metadata.AffectsLayout || metadata.AffectsMeasure || (metadata.AffectsArrange || metadata.InvalidatesLayout)))
          this.OnLayoutPropertyChanged(args);
        this.NotifyChildren(args);
      }
      else if (entry.ValueSource == ValueSource.Inherited || entry.ValueSource == ValueSource.DefaultValue)
      {
        int num = (int) this.ResetValueCore(entry, ValueResetFlags.Inherited);
      }
      else
        entry.InvalidateInheritedValue();
    }

    internal override RadObject InheritanceParent
    {
      get
      {
        if (this.parent != null && !this.parent.IsDisposing && !this.parent.IsDisposed)
          return (RadObject) this.parent;
        return (RadObject) null;
      }
    }

    internal override PropertyDescriptorCollection ReplaceDefaultDescriptors(
      PropertyDescriptorCollection props)
    {
      PropertyDescriptorCollection descriptorCollection = base.ReplaceDefaultDescriptors(props);
      if ((bool) this.GetValue(RadElement.IsEditedInSpyProperty))
      {
        PropertyDescriptor property = TypeDescriptor.CreateProperty(this.GetType(), "ElementTree", typeof (ComponentThemableElementTree), (Attribute) new BrowsableAttribute(true));
        descriptorCollection.Add(property);
      }
      return descriptorCollection;
    }

    public override string ToString()
    {
      if (string.IsNullOrEmpty(this.Name))
        return this.GetType().Name;
      return this.Name + "[" + this.GetType().Name + "]";
    }

    [Category("Layout")]
    [RadPropertyDefaultValue("AutoSize", typeof (RadElement))]
    [Description("Indicates whether the element would automatically calculate its bounds depending on the value of AutoSizeMode and its children.")]
    public virtual bool AutoSize
    {
      get
      {
        return (bool) this.GetValue(RadElement.AutoSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.AutoSizeProperty, BooleanBoxes.Box(value));
      }
    }

    [RadPropertyDefaultValue("Bounds", typeof (RadElement))]
    [Description("Represents the element bounding rectangle")]
    [Category("Layout")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [NotifyParentProperty(true)]
    public virtual Rectangle Bounds
    {
      get
      {
        if (this.cachedBounds == LayoutUtils.InvalidBounds)
        {
          this.cachedBounds = (Rectangle) this.GetValue(RadElement.BoundsProperty);
          this.InvalidateBoundingRectangle();
        }
        return this.cachedBounds;
      }
      set
      {
        this.SetBounds(value);
      }
    }

    [Description("Gets or sets the location of the element based on the element parent rectangle.")]
    [Category("Layout")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [NotifyParentProperty(true)]
    public virtual Point Location
    {
      get
      {
        return this.Bounds.Location;
      }
      set
      {
        this.SetBounds(new Rectangle(value, this.Size));
      }
    }

    [Category("Layout")]
    [Browsable(true)]
    [Description("Size of the element, based on its bounds.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [NotifyParentProperty(true)]
    public virtual Size Size
    {
      get
      {
        return this.Bounds.Size;
      }
      set
      {
        this.SetBounds(new Rectangle(this.Location, value));
      }
    }

    [Description("Represents the thickness of the element's border.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadPropertyDefaultValue("BorderThickness", typeof (RadElement))]
    [Category("Layout")]
    public virtual Padding BorderThickness
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(RadElement.BorderThicknessProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.BorderThicknessProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Padding", typeof (RadElement))]
    [Description("Represents the padding sizes of the element. Paddings do not calculate into element bounds")]
    [Category("Layout")]
    [Localizable(true)]
    public virtual Padding Padding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(RadElement.PaddingProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.PaddingProperty, (object) value);
      }
    }

    [Description("Represents the margins of the element. Margins do not calculate into element bounds.")]
    [RadPropertyDefaultValue("Margin", typeof (RadElement))]
    [Category("Layout")]
    [Localizable(true)]
    public virtual Padding Margin
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(RadElement.MarginProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.MarginProperty, (object) value);
      }
    }

    [Category("Layout")]
    [Description("Represents the preferred location of the element if its size is less than its parent size.")]
    [Localizable(true)]
    [RadPropertyDefaultValue("Alignment", typeof (RadElement))]
    public virtual System.Drawing.ContentAlignment Alignment
    {
      get
      {
        return (System.Drawing.ContentAlignment) this.GetValue(RadElement.AlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.AlignmentProperty, (object) value);
      }
    }

    [Category("Layout")]
    [RadPropertyDefaultValue("AutoSizeMode", typeof (RadElement))]
    [Description("If element AutoSize is set to true, corresponds to the way the element would calculate its size.")]
    public virtual RadAutoSizeMode AutoSizeMode
    {
      get
      {
        return (RadAutoSizeMode) this.GetValue(RadElement.AutoSizeModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.AutoSizeModeProperty, AutoSizeModeBoxes.Box(value));
      }
    }

    [Category("Layout")]
    [RadPropertyDefaultValue("FitToSizeMode", typeof (RadElement))]
    public virtual RadFitToSizeMode FitToSizeMode
    {
      get
      {
        return (RadFitToSizeMode) this.GetValue(RadElement.FitToSizeModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.FitToSizeModeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Enabled", typeof (RadElement))]
    [Category("Behavior")]
    public virtual bool Enabled
    {
      get
      {
        return (bool) this.GetValue(RadElement.EnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.EnabledProperty, BooleanBoxes.Box(value));
      }
    }

    [RadPropertyDefaultValue("CanFocus", typeof (RadElement))]
    [Category("Behavior")]
    [Description("Gets or sets value indicating whether an element can receive input focus.")]
    public virtual bool CanFocus
    {
      get
      {
        return (bool) this.GetValue(RadElement.CanFocusProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.CanFocusProperty, BooleanBoxes.Box(value));
      }
    }

    [Description("Gets a value indicating whether the element has input focus.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsFocused
    {
      get
      {
        return (bool) this.GetValue(RadElement.IsFocusedProperty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsMouseOver
    {
      get
      {
        return (bool) this.GetValue(RadElement.IsMouseOverProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.IsMouseOverProperty, BooleanBoxes.Box(value));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsMouseOverElement
    {
      get
      {
        return (bool) this.GetValue(RadElement.IsMouseOverElementProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.IsMouseOverElementProperty, BooleanBoxes.Box(value));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsMouseDown
    {
      get
      {
        return (bool) this.GetValue(RadElement.IsMouseDownProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.IsMouseDownProperty, BooleanBoxes.Box(value));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool InvalidateChildrenOnChildChanged
    {
      get
      {
        return this.GetBitState(2097152L);
      }
      set
      {
        this.SetBitState(2097152L, value);
      }
    }

    [RadPropertyDefaultValue("ShouldPaint", typeof (RadElement))]
    [Description("Indicates whether the element should be painted. Children visibility would not be affected.")]
    [Category("Appearance")]
    public virtual bool ShouldPaint
    {
      get
      {
        return (bool) this.GetValue(RadElement.ShouldPaintProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ShouldPaintProperty, BooleanBoxes.Box(value));
      }
    }

    [Category("Appearance")]
    [Description("Indicates element visibility, affecting also its children. Collapsed means the element and its children would not be painted and would not be calculated in the layout. This property has no effect in design-time on RadItem objects.")]
    [RadPropertyDefaultValue("Visibility", typeof (RadElement))]
    public virtual ElementVisibility Visibility
    {
      get
      {
        return (ElementVisibility) this.GetValue(RadElement.VisibilityProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.VisibilityProperty, VisibilityBoxes.Box(value));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsElementVisible
    {
      get
      {
        if ((ElementVisibility) this.GetValue(RadElement.VisibilityProperty) != ElementVisibility.Visible)
          return false;
        if (this.Parent != null)
          return this.parent.IsElementVisible;
        return true;
      }
    }

    [Browsable(false)]
    [RadPropertyDefaultValue("Name", typeof (RadElement))]
    [Category("StyleSheet")]
    [StyleBuilderReadOnly]
    public virtual string Name
    {
      get
      {
        return (string) this.GetValue(RadElement.NameProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.NameProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadPropertyDefaultValue("Class", typeof (RadElement))]
    [Category("StyleSheet")]
    [StyleBuilderReadOnly]
    public string Class
    {
      get
      {
        return (string) this.GetValue(RadElement.ClassProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ClassProperty, (object) value);
      }
    }

    [Description("Indicated whether the painting of the element and its children should be restricted to its bounds /through clipping/. Some elements like FillPrimitive apply clipping always, ignoring this property value.")]
    [RadPropertyDefaultValue("ClipDrawing", typeof (RadElement))]
    [Category("Appearance")]
    public bool ClipDrawing
    {
      get
      {
        return (bool) this.GetValue(RadElement.ClipDrawingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ClipDrawingProperty, BooleanBoxes.Box(value));
      }
    }

    [TypeConverter(typeof (ElementShapeConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("Shape", typeof (RadElement))]
    [Description("Represents the shape of the element. Setting shape affects also border element painting. Shape is considered when painting, clipping and hit-testing an element.")]
    public virtual ElementShape Shape
    {
      get
      {
        return (ElementShape) this.GetValue(RadElement.ShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ShapeProperty, (object) value);
      }
    }

    protected override void DisposeManagedResources()
    {
      if (this.elementTree != null && !this.elementTree.Disposing)
      {
        RadControl control = this.elementTree.Control as RadControl;
        if (control != null && control.Behavior.currentFocusedElement == this)
        {
          control.Behavior.currentFocusedElement.SetElementFocused(false);
          control.Behavior.currentFocusedElement = (RadElement) null;
          control.Behavior.lastFocusedElement = (RadElement) null;
        }
      }
      if (this.parent != null)
      {
        int index = this.parent.children.IndexOf(this);
        if (index >= 0)
          this.parent.children.RemoveAt(index);
      }
      this.IsStyleSelectorValueSet.Clear();
      this.IsStyleSelectorValueSet = (HybridDictionary) null;
      this.behaviors.Clear();
      this.layoutEvents.Clear();
      this.DetachFromElementTree(this.elementTree);
      if (this.zOrderedChildren != null)
        this.zOrderedChildren.Clear();
      RadPropertyValue propertyValue = this.GetPropertyValue(RadElement.ShapeProperty);
      if (propertyValue != null && propertyValue.ValueSource == ValueSource.Local)
        (propertyValue.GetCurrentValue(false) as ElementShape)?.Dispose();
      base.DisposeManagedResources();
    }

    protected override void PerformDispose(bool disposing)
    {
      if (this.state != ElementState.Disposing)
      {
        this.OnBeginDispose();
        foreach (RadElement radElement in this.ChildrenHierarchy)
          radElement.OnBeginDispose();
      }
      this.DisposeChildren();
      base.PerformDispose(disposing);
      this.elementTree = (ComponentThemableElementTree) null;
      this.layoutManager = (ILayoutManager) null;
      this.state = ElementState.Disposed;
    }

    public void DisposeChildren()
    {
      int count = this.children.Count;
      List<RadElement> radElementList = new List<RadElement>((IEnumerable<RadElement>) this.children);
      for (int index = count - 1; index >= 0; --index)
        radElementList[index].Dispose();
    }

    [Description("Represents the minimum size of the element")]
    [RadPropertyDefaultValue("MinSize", typeof (RadElement))]
    [Category("Layout")]
    public virtual Size MinSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(RadElement.MinSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.MinSizeProperty, (object) value);
      }
    }

    [Category("Layout")]
    [Description("Represents the maximum size of the element")]
    [RadPropertyDefaultValue("MaxSize", typeof (RadElement))]
    public virtual Size MaxSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(RadElement.MaxSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.MaxSizeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ZIndex", typeof (RadElement))]
    [Description("Specifies the order of painting an element compared to its sibling elements")]
    [Category("Behavior")]
    public virtual int ZIndex
    {
      get
      {
        return (int) this.GetValue(RadElement.ZIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ZIndexProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RightToLeft", typeof (RadElement))]
    [Localizable(true)]
    [Category("Appearance")]
    public virtual bool RightToLeft
    {
      get
      {
        return (bool) this.GetValue(RadElement.RightToLeftProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.RightToLeftProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResetUseCompatibleTextRendering()
    {
      this.UseCompatibleTextRendering = RadControl.UseCompatibleTextRenderingDefaultValue;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeUseCompatibleTextRendering()
    {
      return this.UseCompatibleTextRendering != RadControl.UseCompatibleTextRenderingDefaultValue;
    }

    [Description("Determines whether to use compatible text rendering engine (GDI+) or not (GDI).")]
    [RadPropertyDefaultValue("UseCompatibleTextRendering", typeof (RadElement))]
    [Category("Behavior")]
    public virtual bool UseCompatibleTextRendering
    {
      get
      {
        if (this.GetValue(RadElement.UseCompatibleTextRenderingProperty) == null)
          return RadControl.UseCompatibleTextRenderingDefaultValue;
        return ((bool?) this.GetValue(RadElement.UseCompatibleTextRenderingProperty)).Value;
      }
      set
      {
        int num = (int) this.SetValue(RadElement.UseCompatibleTextRenderingProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [VsbBrowsable(true)]
    public RadImageShape BackgroundShape
    {
      get
      {
        return (RadImageShape) this.GetValue(RadElement.BackgroundShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.BackgroundShapeProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ContainsMouse
    {
      get
      {
        return (bool) this.GetValue(RadElement.ContainsMouseProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ContainsMouseProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Capture
    {
      get
      {
        if (this.ElementTree == null)
          return false;
        bool capture = this.ElementTree.Control.Capture;
        bool flag = object.ReferenceEquals((object) this.ElementTree.ComponentTreeHandler.Behavior.ItemCapture, (object) this);
        if (capture)
          return flag;
        return false;
      }
      set
      {
        if (this.ElementTree == null)
          return;
        if (value)
        {
          this.ElementTree.ComponentTreeHandler.Behavior.ItemCapture = this;
          this.ElementTree.ComponentTreeHandler.OnCaptureChangeRequested(this, true);
        }
        else
        {
          this.ElementTree.ComponentTreeHandler.OnCaptureChangeRequested(this, false);
          this.ElementTree.ComponentTreeHandler.Behavior.ItemCapture = (RadElement) null;
        }
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ScaleTransform", typeof (RadElement))]
    [Description("Scale factors for painting the element and its children.")]
    public virtual SizeF ScaleTransform
    {
      get
      {
        return (SizeF) this.GetValue(RadElement.ScaleTransformProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ScaleTransformProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [RadPropertyDefaultValue("AngleTransform", typeof (RadElement))]
    [Description("Rotation transform angle for painting the element and its children.")]
    public virtual float AngleTransform
    {
      get
      {
        return (float) this.GetValue(RadElement.AngleTransformProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.AngleTransformProperty, (object) value);
      }
    }

    [Description("Offset of the origin of the coordinate system used when painting the element and its children.")]
    [RadPropertyDefaultValue("PositionOffset", typeof (RadElement))]
    [Category("Appearance")]
    public virtual SizeF PositionOffset
    {
      get
      {
        return TelerikDpiHelper.ScaleSizeF((SizeF) this.GetValue(RadElement.PositionOffsetProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.PositionOffsetProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Category("Design")]
    [Description("Gets or sets whether the properties of this element should be serialized via CodeDom")]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual bool SerializeProperties
    {
      get
      {
        return this.GetBitState(268435456L);
      }
      set
      {
        this.SetBitState(268435456L, value);
      }
    }

    [Browsable(false)]
    [Category("Design")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets whether this element should be serialized via CodeDom")]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual bool SerializeElement
    {
      get
      {
        return this.GetBitState(1073741824L);
      }
      set
      {
        this.SetBitState(1073741824L, value);
      }
    }

    [Description("Gets or sets whether the children of this element should be serialized via CodeDom")]
    [Category("Design")]
    [Browsable(true)]
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual bool SerializeChildren
    {
      get
      {
        return this.GetBitState(536870912L);
      }
      set
      {
        this.SetBitState(536870912L, value);
      }
    }

    public static int RenderingMaxFramerate
    {
      get
      {
        if (RadElement.animationMaxFramerate < 1 || RadElement.animationMaxFramerate > 1000)
          return 200;
        return RadElement.animationMaxFramerate;
      }
      set
      {
        RadElement.animationMaxFramerate = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsThemeApplied
    {
      get
      {
        return this.GetBitState(134217728L);
      }
      internal set
      {
        this.SetBitState(134217728L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool ShouldApplyTheme
    {
      get
      {
        return !this.GetBitState(33554432L);
      }
      set
      {
        this.SetBitState(33554432L, !value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool UseCenteredAngleTransform
    {
      get
      {
        return this.GetBitState(67108864L);
      }
      set
      {
        this.SetBitState(67108864L, value);
      }
    }

    [Category("Data")]
    [Bindable(true)]
    [DefaultValue(null)]
    [Description("Tag object that can be used to store user data, corresponding to the element")]
    [Localizable(false)]
    [TypeConverter(typeof (StringConverter))]
    public object Tag
    {
      get
      {
        return this.GetValue(RadElement.TagProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.TagProperty, value);
      }
    }

    [Category("Layout")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [NotifyParentProperty(true)]
    [Description("Allow stretching in horizontal direction")]
    [RadPropertyDefaultValue("StretchHorizontally", typeof (RadElement))]
    public virtual bool StretchHorizontally
    {
      get
      {
        return (bool) this.GetValue(RadElement.StretchHorizontallyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.StretchHorizontallyProperty, (object) value);
      }
    }

    [NotifyParentProperty(true)]
    [Description("Allow stretching in vertical direction")]
    [Category("Layout")]
    [RadPropertyDefaultValue("StretchVertically", typeof (RadElement))]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual bool StretchVertically
    {
      get
      {
        return (bool) this.GetValue(RadElement.StretchVerticallyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.StretchVerticallyProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ClickMode", typeof (RadElement))]
    [SettingsBindable(true)]
    [Bindable(true)]
    [Description("Specifies when the Click event should fire.")]
    [Category("Behavior")]
    public ClickMode ClickMode
    {
      get
      {
        return (ClickMode) base.GetValue(RadElement.ClickModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.ClickModeProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Gets or sets a value indicating whether the DoubleClick event will fire for this item.")]
    [DefaultValue(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool DoubleClickEnabled
    {
      get
      {
        return this.BitState[34359738368L];
      }
      set
      {
        this.SetBitState(34359738368L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool CaptureOnMouseDown
    {
      get
      {
        return this.captureOnMouseDown;
      }
      set
      {
        this.captureOnMouseDown = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsFocusable
    {
      get
      {
        return this.BitState[8589934592L];
      }
      set
      {
        this.SetBitState(8589934592L, value);
      }
    }

    protected virtual bool IsItemHovered
    {
      get
      {
        return this.IsMouseOver;
      }
    }

    IStylableNode IStylableNode.Parent
    {
      get
      {
        return (IStylableNode) this.Parent;
      }
    }

    IEnumerable<RadObject> IStylableNode.ChildrenHierarchy
    {
      get
      {
        return this.GetStylableChildrenHierarchy();
      }
    }

    IEnumerable<RadObject> IStylableNode.Children
    {
      get
      {
        return this.GetStylableChildren();
      }
    }

    string IStylableNode.Class
    {
      get
      {
        return this.Class;
      }
    }

    System.Type IStylableNode.GetThemeEffectiveType()
    {
      return this.GetThemeEffectiveType();
    }

    void IStylableNode.ApplySettings(PropertySettingGroup group)
    {
      group?.Apply((RadObject) this);
    }

    protected virtual IEnumerable<RadObject> GetStylableChildrenHierarchy()
    {
      foreach (IStylableNode child in this.Children)
      {
        yield return child as RadObject;
        foreach (RadObject radObject in child.ChildrenHierarchy)
          yield return radObject;
      }
    }

    protected virtual IEnumerable<RadObject> GetStylableChildren()
    {
      for (int i = 0; i < this.Children.Count; ++i)
      {
        RadObject child = (RadObject) this.Children[i];
        yield return child;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [TypeConverter(typeof (ComponentConverter))]
    [DefaultValue(null)]
    [Browsable(false)]
    [Category("StyleSheet")]
    [Description("Gets or sets the stylesheet associated with the element.")]
    public StyleSheet Style
    {
      get
      {
        return (StyleSheet) this.GetValue(RadElement.StyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.StyleProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected void ResetStyleVersion()
    {
      this.styleVersion = 0;
    }

    [Category("Behavior")]
    [DefaultValue("")]
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [Description("Gets or sets the tooltip text associated with this item.")]
    public virtual string ToolTipText
    {
      get
      {
        string str = (string) this.GetValue(RadItem.ToolTipTextProperty);
        if (!this.AutoToolTip || !string.IsNullOrEmpty(str))
          return str;
        return string.Empty;
      }
      set
      {
        if (!(value != (string) this.GetValue(RadItem.ToolTipTextProperty)))
          return;
        int num = (int) this.SetValue(RadItem.ToolTipTextProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    [Description("ToolStripItemAutoToolTipDescr")]
    [Category("Behavior")]
    public virtual bool AutoToolTip
    {
      get
      {
        return this.GetBitState(68719476736L);
      }
      set
      {
        if (this.GetBitState(68719476736L) == value)
          return;
        this.BitState[68719476736L] = value;
        this.OnNotifyPropertyChanged(nameof (AutoToolTip));
      }
    }

    [TypeConverter(typeof (RadFilteredPropertiesConverter))]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Editor(typeof (ScreenTipEditor), typeof (UITypeEditor))]
    public virtual RadScreenTipElement ScreenTip
    {
      get
      {
        return this.screenTip;
      }
      set
      {
        if (this.screenTip == value)
          return;
        this.screenTip = value;
        this.AutoNumberKeyTip = 0;
      }
    }

    protected internal virtual int AutoNumberKeyTip
    {
      get
      {
        return this.autoNumberKeyTip;
      }
      set
      {
        this.autoNumberKeyTip = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MeasureIsDirty
    {
      get
      {
        return this.BitState[8192L];
      }
      set
      {
        this.SetBitState(8192L, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool ArrangeIsDirty
    {
      get
      {
        return this.BitState[1024L];
      }
      set
      {
        this.SetBitState(1024L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool NeverMeasured
    {
      get
      {
        return this.BitState[65536L];
      }
      set
      {
        this.SetBitState(65536L, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool NeverArranged
    {
      get
      {
        return this.BitState[4096L];
      }
      set
      {
        this.SetBitState(4096L, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ArrangeInProgress
    {
      get
      {
        return this.BitState[2048L];
      }
      set
      {
        this.SetBitState(2048L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool MeasureInProgress
    {
      get
      {
        return this.BitState[32768L];
      }
      set
      {
        this.SetBitState(32768L, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldPaintChildren
    {
      get
      {
        return this.BitState[4294967296L];
      }
      set
      {
        this.SetBitState(4294967296L, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool InvalidateMeasureOnRemove
    {
      get
      {
        return this.BitState[16L];
      }
      set
      {
        this.SetBitState(16L, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool HideFromElementHierarchyEditor
    {
      get
      {
        return this.BitState[2147483648L];
      }
      set
      {
        this.SetBitState(2147483648L, value);
      }
    }

    protected virtual RectangleF GetClientRectangle(SizeF finalSize)
    {
      Padding padding = this.Padding;
      RectangleF rectangleF = new RectangleF((float) padding.Left, (float) padding.Top, finalSize.Width - (float) padding.Horizontal, finalSize.Height - (float) padding.Vertical);
      foreach (RadElement child in this.Children)
      {
        if (child is BorderPrimitive)
        {
          if (child != null)
          {
            if (child.Visibility != ElementVisibility.Collapsed)
            {
              Padding borderThickness = this.GetBorderThickness(child as BorderPrimitive);
              rectangleF.X += (float) borderThickness.Left;
              rectangleF.Y += (float) borderThickness.Top;
              rectangleF.Width -= (float) borderThickness.Horizontal;
              rectangleF.Height -= (float) borderThickness.Vertical;
              break;
            }
            break;
          }
          break;
        }
      }
      rectangleF.Width = Math.Max(0.0f, rectangleF.Width);
      rectangleF.Height = Math.Max(0.0f, rectangleF.Height);
      return rectangleF;
    }

    protected Padding GetBorderThickness(BorderPrimitive element)
    {
      Padding padding = Padding.Empty;
      if (element.BoxStyle == BorderBoxStyle.SingleBorder)
        padding = new Padding((int) Math.Round((double) element.Width, MidpointRounding.AwayFromZero));
      else if (element.BoxStyle == BorderBoxStyle.FourBorders)
        padding = new Padding((int) Math.Round((double) element.LeftWidth, MidpointRounding.AwayFromZero), (int) Math.Round((double) element.TopWidth, MidpointRounding.AwayFromZero), (int) Math.Round((double) element.RightWidth, MidpointRounding.AwayFromZero), (int) Math.Round((double) element.BottomWidth, MidpointRounding.AwayFromZero));
      else if (element.BoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        int all = (int) Math.Round((double) element.Width, MidpointRounding.AwayFromZero);
        if (all == 1)
          all = 2;
        padding = new Padding(all);
      }
      return padding;
    }

    public SizeF DpiScaleFactor
    {
      get
      {
        return this.dpiScaleFactor;
      }
      protected set
      {
        this.dpiScaleFactor = value;
      }
    }

    public virtual void DpiScaleChanged(SizeF scaleFactor)
    {
      this.dpiScaleFactor = scaleFactor;
      foreach (RadElement child in this.Children)
        child.DpiScaleChanged(scaleFactor);
    }

    private class ProcessedEvent
    {
      private RadElement sender;
      private RoutedEventArgs args;

      public RadElement Sender
      {
        get
        {
          return this.sender;
        }
      }

      public RoutedEventArgs Args
      {
        get
        {
          return this.args;
        }
      }

      public ProcessedEvent(RadElement sender, RoutedEventArgs args)
      {
        this.sender = sender;
        this.args = args;
      }
    }

    internal struct MinMax
    {
      internal float minWidth;
      internal float maxWidth;
      internal float minHeight;
      internal float maxHeight;

      internal MinMax(RadElement e)
      {
        Size maxSize = e.MaxSize;
        Size minSize = e.MinSize;
        float val1_1 = maxSize.Height > 0 ? (float) maxSize.Height : float.PositiveInfinity;
        float val1_2 = maxSize.Width > 0 ? (float) maxSize.Width : float.PositiveInfinity;
        float val2_1 = minSize.Height > 0 ? (float) minSize.Height : 0.0f;
        float val2_2 = minSize.Width > 0 ? (float) minSize.Width : 0.0f;
        this.maxHeight = Math.Max(val1_1, val2_1);
        this.minHeight = Math.Min(val1_1, val2_1);
        this.maxWidth = Math.Max(val1_2, val2_2);
        this.minWidth = Math.Min(val1_2, val2_2);
      }
    }

    private class LayoutTransformData
    {
      internal RadMatrix transform;
      internal SizeF UntransformedDS;

      internal void CreateTransformSnapshot(RadMatrix sourceTransform)
      {
        this.transform = sourceTransform;
      }
    }

    private class SizeBox
    {
      private float height;
      private float width;

      internal float Height
      {
        get
        {
          return this.height;
        }
        set
        {
          if ((double) value < 0.0)
            throw new ArgumentException("Width and Height cannot be Negative");
          this.height = value;
        }
      }

      internal float Width
      {
        get
        {
          return this.width;
        }
        set
        {
          if ((double) value < 0.0)
            throw new ArgumentException("Width and Height cannot be Negative");
          this.width = value;
        }
      }

      internal SizeBox(SizeF size)
        : this(size.Width, size.Height)
      {
      }

      internal SizeBox(float width, float height)
      {
        if ((double) width < 0.0 || (double) height < 0.0)
          throw new ArgumentException("Width and Height cannot be Negative");
        this.width = width;
        this.height = height;
      }
    }
  }
}
