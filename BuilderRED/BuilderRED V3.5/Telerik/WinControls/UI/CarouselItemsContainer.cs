// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselItemsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CarouselItemsContainer : RadItem, IVirtualViewport
  {
    public static readonly RadProperty CarouselAnimationData = RadProperty.Register(nameof (CarouselAnimationData), typeof (CarouselPathAnimationData), typeof (CarouselItemsContainer), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static readonly RadProperty CarouselLocationProperty = RadProperty.Register(nameof (CarouselLocationProperty), typeof (double), typeof (CarouselItemsContainer), (RadPropertyMetadata) new RadElementPropertyMetadata((object) double.NaN, ElementPropertyOptions.None));
    private RadEasingType easingType = RadEasingType.OutQuad;
    private Animations animationsApplied = Animations.All;
    private int animationFrames = 30;
    private int visibleItemCount = 10;
    private RangeMapper mapper = (RangeMapper) new CircularRangeMapper();
    private Range currentRange = new Range();
    private Range newRange = new Range();
    private List<CarouselItemsContainer.AnimationEntry> currentlyRunningAnimations = new List<CarouselItemsContainer.AnimationEntry>();
    private double minFadeOpacity = 0.33;
    private OpacityChangeConditions opacityChangeCondition = OpacityChangeConditions.ZIndex;
    private int animationDelay = 40;
    private AutoLoopPauseConditions autoLoopPauseCondition = AutoLoopPauseConditions.OnMouseOverCarousel;
    private bool enableAnimationOnFormResize = true;
    internal const long SpecialCaseStateKey = 8796093022208;
    internal const long UpToDateStateKey = 17592186044416;
    internal const long EnableLoopingStateKey = 35184372088832;
    internal const long VirtualModeStateKey = 70368744177664;
    internal const long EnableAutoLoopStateKey = 140737488355328;
    private int update;
    private ICarouselPath carouselPath;
    private RadItemVirtualizationCollection items;
    private int selectedIndex;
    private RadCarouselElement owner;
    private AutoLoopDirections autoLoopDirection;
    private CarouselItemsLocationBehavior carouselItemBehavior;

    public CarouselItemsContainer(RadCarouselElement owner)
    {
      this.owner = owner;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[35184372088832L] = true;
      this.BitState[70368744177664L] = true;
      CarouselEllipsePath carouselEllipsePath = new CarouselEllipsePath();
      carouselEllipsePath.Center = new Point3D(50.0, 50.0, 0.0);
      carouselEllipsePath.FinalAngle = -100.0;
      carouselEllipsePath.InitialAngle = -90.0;
      carouselEllipsePath.U = new Point3D(-20.0, -17.0, -50.0);
      carouselEllipsePath.V = new Point3D(30.0, -25.0, -60.0);
      carouselEllipsePath.ZScale = 500.0;
      this.carouselPath = (ICarouselPath) carouselEllipsePath;
      this.BypassLayoutPolicies = true;
      this.carouselItemBehavior = new CarouselItemsLocationBehavior(this);
      this.items = new RadItemVirtualizationCollection((IVirtualViewport) this);
      this.items.ItemsChanged += new ItemChangedDelegate(this.OnItemsChanged);
      this.items.ItemTypes = new Type[11]
      {
        typeof (RadButtonElement),
        typeof (RadImageItem),
        typeof (RadImageButtonElement),
        typeof (RadLabelElement),
        typeof (RadItemsContainer),
        typeof (RadRotatorElement),
        typeof (RadDropDownListElement),
        typeof (RadWebBrowserElement),
        typeof (RadTextBoxElement),
        typeof (RadCheckBoxElement),
        typeof (RadMaskedEditBoxElement)
      };
    }

    private void OnItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (this.IsDisposing || this.IsDisposed)
        return;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          target.Disposing += new EventHandler(this.OnItem_Disposing);
          break;
        case ItemsChangeOperation.Removed:
          target.Disposing -= new EventHandler(this.OnItem_Disposing);
          break;
        case ItemsChangeOperation.Clearing:
          using (RadItemCollection.RadItemEnumerator enumerator = changed.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.Disposing -= new EventHandler(this.OnItem_Disposing);
            break;
          }
      }
    }

    private void OnItem_Disposing(object sender, EventArgs e)
    {
      if (!(sender is RadItem))
        return;
      this.items.Remove(sender as RadItem);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.SynchronizeCollections();
    }

    public bool IsAnimationRunning
    {
      get
      {
        return this.currentlyRunningAnimations.Count > 0;
      }
    }

    [Description("Enable or disable the re-animation of RadCarousel on form maximize, minimeze or resize")]
    [DefaultValue(true)]
    public virtual bool EnableAnimationOnFormResize
    {
      get
      {
        return this.enableAnimationOnFormResize;
      }
      set
      {
        this.enableAnimationOnFormResize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OpacityChangeConditions OpacityChangeCondition
    {
      get
      {
        return this.opacityChangeCondition;
      }
      set
      {
        if (value == this.opacityChangeCondition)
          return;
        this.opacityChangeCondition = value;
        this.OnNotifyPropertyChangedAndUpdate(nameof (OpacityChangeCondition));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int SelectedIndex
    {
      get
      {
        int num = this.Items.Count > 0 ? this.selectedIndex % this.Items.Count : 0;
        if (num >= 0)
          return num;
        return num + this.Items.Count;
      }
      set
      {
        int closest = this.CalculateClosest(value);
        if (this.selectedIndex == closest)
          return;
        this.selectedIndex = closest;
        this.BitState[17592186044416L] = false;
        this.SyncItemsAndUpdateCarousel(this.selectedIndex);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double MinFadeOpacity
    {
      get
      {
        return this.minFadeOpacity;
      }
      set
      {
        this.minFadeOpacity = value;
        this.OnNotifyPropertyChangedAndUpdate(nameof (MinFadeOpacity));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableLooping
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        this.SetBitState(35184372088832L, value);
      }
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      switch (key)
      {
        case 35184372088832:
          this.mapper = !newValue ? (RangeMapper) new RestrictedRangeMapper() : (RangeMapper) new CircularRangeMapper();
          this.RecalculateItemCount();
          this.OnNotifyPropertyChanged("EnableLooping");
          break;
        case 70368744177664:
          this.OnNotifyPropertyChanged("Virtualized");
          break;
        case 140737488355328:
          this.OnNotifyPropertyChanged("EnableAutoLoop");
          break;
      }
    }

    private void RecalculateItemCount()
    {
      this.mapper.ItemsCount = this.Items.Count;
    }

    public RadCarouselElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public int VisibleItemCount
    {
      get
      {
        return this.visibleItemCount;
      }
      set
      {
        if (this.visibleItemCount == value || value <= 0)
          return;
        this.visibleItemCount = value;
        this.RecalculateItemCount();
        this.OnNotifyPropertyChanged(nameof (VisibleItemCount));
        this.SynchronizeCollections();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ICarouselPath CarouselPath
    {
      get
      {
        return this.carouselPath;
      }
      set
      {
        if (this.carouselPath == value)
          return;
        this.BitState[17592186044416L] = false;
        this.SetPathCalculator(value);
        this.OnNotifyPropertyChanged(nameof (CarouselPath));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public RadEasingType EasingType
    {
      get
      {
        return this.easingType;
      }
      set
      {
        if (this.easingType == value)
          return;
        this.easingType = value;
        this.OnNotifyPropertyChanged(nameof (EasingType));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public RadItemCollection Items
    {
      get
      {
        return (RadItemCollection) this.items;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Animations AnimationsApplied
    {
      get
      {
        return this.animationsApplied;
      }
      set
      {
        if (this.animationsApplied == value)
          return;
        this.animationsApplied = value;
        this.OnNotifyPropertyChanged(nameof (AnimationsApplied));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        if (this.animationFrames == value)
          return;
        this.animationFrames = value;
        this.OnNotifyPropertyChanged(nameof (AnimationFrames));
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int AnimationDelay
    {
      get
      {
        return this.animationDelay;
      }
      set
      {
        if (this.animationDelay == value)
          return;
        this.animationDelay = value;
        this.OnNotifyPropertyChanged(nameof (AnimationDelay));
      }
    }

    private void PathCalculator_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.ResetItemsPositions();
      this.SyncItemsAndUpdateCarousel(this.selectedIndex);
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      bool flag = false;
      switch (e.PropertyName)
      {
        case "CarouselPath":
          this.ResetItemsPositions();
          flag = true;
          break;
        case "Virtualized":
        case "VisibleItemCount":
        case "EnableLooping":
          flag = true;
          break;
        case "EnableAutoLoop":
          this.Owner.ChangedAutoLoop();
          break;
      }
      if (flag)
        this.SyncItemsAndUpdateCarousel(this.selectedIndex);
      this.SynchronizeCollections();
      base.OnNotifyPropertyChanged(e);
    }

    private void OnNotifyPropertyChangedAndUpdate(string propertyName)
    {
      this.OnNotifyPropertyChanged(propertyName);
      this.SyncItemsAndUpdateCarousel(this.selectedIndex);
      this.SynchronizeCollections();
    }

    private void ResetItemsPositions()
    {
      for (int index = 0; index < this.Children.Count; ++index)
        this.GetItemAnimationData(index).From = new double?(double.NegativeInfinity);
    }

    private void AnimateToPositions()
    {
      if (this.Visibility != ElementVisibility.Visible || this.GetBitState(17592186044416L))
        return;
      int num1 = this.currentRange.FromRangeIndexRestricted(this.selectedIndex);
      if (num1 == -1)
        num1 = 0;
      bool flag = !this.CarouselPath.ZindexFromPath();
      double num2 = this.CarouselPath.Step(this.currentRange.Length - 1);
      double val2 = 1.0;
      for (int index = 0; index < this.currentRange.Length; ++index)
      {
        CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(index);
        itemAnimationData.To = new double?(Math.Max(0.0, Math.Min(1.0, val2)));
        if (this.GetBitState(8796093022208L))
          itemAnimationData.UpdateSpecialHandling();
        if (flag)
          this.SetZIndex(this.Children[index], index <= num1 ? index + this.currentRange.Length - num1 : this.currentRange.Length - index + num1);
        val2 -= num2;
      }
      for (int index = 0; index < this.Children.Count; ++index)
        this.Animate((VisualElement) this.Children[index]);
      this.BitState[17592186044416L] = true;
    }

    private void Animate(VisualElement element)
    {
      CarouselPathAnimationData itemAnimationData = CarouselItemsContainer.GetItemAnimationData((RadObject) element);
      if (!CarouselItemsContainer.AnimationRemoveCurrent((RadElement) element, itemAnimationData))
        return;
      this.AnimationCreate(element, itemAnimationData);
      itemAnimationData.Interrupt = !itemAnimationData.IsOutAnimation();
      if (itemAnimationData.CurrentAnimation == null)
        return;
      itemAnimationData.CurrentAnimation.AnimationStarted += new AnimationStartedEventHandler(this.CurrentAnimation_AnimationStarted);
      itemAnimationData.CurrentAnimation.AnimationFinished += new AnimationFinishedEventHandler(this.CurrentAnimation_AnimationFinished);
      itemAnimationData.CurrentAnimation.ApplyValue((RadObject) element);
    }

    public void CancelAniamtion()
    {
      bool flag = this.currentlyRunningAnimations.Count > 0;
      while (this.currentlyRunningAnimations.Count > 0)
      {
        CarouselItemsContainer.AnimationEntry runningAnimation = this.currentlyRunningAnimations[0];
        runningAnimation.PropertySetting.Stop((RadObject) runningAnimation.Element);
      }
      if (!flag)
        return;
      this.Owner.OnAnimationFinished();
    }

    private void CurrentAnimation_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      int index = this.currentlyRunningAnimations.IndexOf(new CarouselItemsContainer.AnimationEntry((AnimatedPropertySetting) sender, e.Element));
      if (index > -1)
      {
        this.currentlyRunningAnimations.RemoveAt(index);
      }
      else
      {
        int count = this.currentlyRunningAnimations.Count;
        if (count > 0)
          this.currentlyRunningAnimations.RemoveAt(count - 1);
      }
      if (this.currentlyRunningAnimations.Count != 0)
        return;
      this.Owner.OnAnimationFinished();
    }

    private void CurrentAnimation_AnimationStarted(object sender, AnimationStatusEventArgs e)
    {
      if (this.currentlyRunningAnimations.Count == 0)
        this.Owner.OnAnimationStarted();
      this.currentlyRunningAnimations.Add(new CarouselItemsContainer.AnimationEntry((AnimatedPropertySetting) sender, e.Element));
    }

    private void AnimationCreate(VisualElement element, CarouselPathAnimationData data)
    {
      this.CarouselPath.CreateAnimation(element, data, this.animationFrames, this.animationDelay);
      data.CurrentAnimation.ApplyEasingType = this.easingType;
      if (data.IsOutAnimation())
        data.CurrentAnimation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          CarouselPathAnimationData itemAnimationData = CarouselItemsContainer.GetItemAnimationData((RadObject) element);
          bool interrupt = itemAnimationData.Interrupt;
          CarouselItemsContainer.AnimationRemoveCurrent((RadElement) element, itemAnimationData);
          if (interrupt)
            return;
          Rectangle boundingRectangle = element.ControlBoundingRectangle;
          int index = this.Children.IndexOf((RadElement) element);
          if (index != -1)
            this.Children.RemoveAt(index);
          this.ElementTree.Control.Invalidate(boundingRectangle, true);
        });
      else
        data.CurrentAnimation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) => CarouselItemsContainer.AnimationRemoveCurrent((RadElement) element, (CarouselPathAnimationData) null));
    }

    public bool Virtualized
    {
      get
      {
        return this.GetBitState(70368744177664L);
      }
      set
      {
        this.SetBitState(70368744177664L, value);
      }
    }

    public void SetVirtualItemsCollection(IVirtualizationCollection virtualItemsCollection)
    {
    }

    public void StartInitialAnimation()
    {
      this.Children.Clear();
      this.BitState[17592186044416L] = false;
      this.SyncItemsAndUpdateCarousel(0);
    }

    public void OnItemDataInserted(int index, object itemData)
    {
      Range maxRange = this.GetMaxRange(true);
      int itemsCount = this.mapper.ItemsCount;
      this.mapper.ItemsCount = this.CalculateVisibleItemsCount();
      bool flag = this.mapper.IsInRange(maxRange, index);
      this.mapper.ItemsCount = itemsCount;
      if (!flag)
        return;
      int childIndex = this.EnsureItemToChildIndex(index);
      this.UpdateAnimationOnItemInsertRemove(childIndex, 1, new double?(double.NaN), new double?());
      this.BitState[17592186044416L] = false;
      this.SyncItemsAndUpdateCarousel(index);
      if (this.update <= 0)
        return;
      this.Owner.CallOnItemEntering(new ItemEnteringEventArgs(childIndex));
    }

    public void OnItemDataRemoved(int index, object itemData)
    {
      this.GetMaxRange(true);
      RadElement radElement = itemData as RadElement;
      if (radElement != null && this.Children.Contains(radElement.Parent))
      {
        this.Children.Remove(radElement.Parent);
        this.UpdateAnimationOnItemInsertRemove(index, -1, new double?(), new double?(double.NaN));
      }
      if (this.currentRange.IsInRange(index))
        this.UpdateAnimationOnItemInsertRemove(index, -1, new double?(), new double?(double.NaN));
      else
        this.BitState[17592186044416L] = false;
      this.SyncItemsAndUpdateCarousel(index);
    }

    public void OnItemDataSet(int index, object oldItemData, object newItemData)
    {
      this.GetMaxRange(true);
      for (int index1 = 0; index1 < this.currentRange.Length; ++index1)
      {
        if (this.mapper.MapFromRangeIndex(this.currentRange.ToRangeIndex(index1)) == index)
        {
          if (index1 < this.Children.Count)
          {
            ((CarouselContentItem) this.Children[index1]).HostedItem = (RadElement) newItemData;
            break;
          }
          break;
        }
      }
      this.BitState[17592186044416L] = false;
      this.SyncItemsAndUpdateCarousel(this.selectedIndex);
    }

    public void OnItemsDataClear()
    {
      this.Reset();
    }

    void IVirtualViewport.OnItemsDataClearComplete()
    {
    }

    void IVirtualViewport.OnItemsDataSort()
    {
    }

    public void OnItemsDataSortComplete()
    {
      this.SyncItemsAndUpdateCarousel(0);
    }

    public void BeginUpdate()
    {
      ++this.update;
    }

    public void EndUpdate()
    {
      if (this.update <= 0 || --this.update != 0)
        return;
      this.SyncItemsAndUpdateCarousel(this.selectedIndex);
    }

    private Range GetMaxRange(bool normalize)
    {
      int visibleItemsCount = this.CalculateVisibleItemsCount();
      this.mapper.ItemsCount = this.Items.Count;
      return normalize ? this.mapper.Normalize(Range.CenterAt(0, visibleItemsCount, this.selectedIndex)) : Range.CenterAt(0, visibleItemsCount, this.selectedIndex);
    }

    private int CalculateVisibleItemsCount()
    {
      if (!this.GetBitState(70368744177664L))
        return this.Items.Count;
      return Math.Max(Math.Min(this.visibleItemCount, this.Items.Count), 1);
    }

    protected int EnsureItemToChildIndex(int itemIndex)
    {
      RadElement virtualData = (RadElement) this.items.GetVirtualData(itemIndex);
      int num = this.Children.IndexOf(virtualData);
      if (num != -1)
        return num;
      CarouselContentItem carouselContentItem = new CarouselContentItem(virtualData);
      carouselContentItem.SetOwner(this.owner);
      carouselContentItem.AddBehavior((PropertyChangeBehavior) this.carouselItemBehavior);
      this.CarouselPath.InitializeItem((VisualElement) carouselContentItem, (object) this.AnimationsApplied);
      this.Children.Add((RadElement) carouselContentItem);
      return this.Children.Count - 1;
    }

    protected int GetChildIndexFromItemIndex(int itemIndex)
    {
      return this.Children.IndexOf((RadElement) this.items.GetVirtualData(itemIndex));
    }

    private void SynchronizeCollections()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      Range range = this.mapper.Normalize(this.newRange);
      this.BitState[8796093022208L] = range.Length != this.Items.Count;
      this.SetUpItemsAnimationParameters();
      bool flag = this.currentRange.Length + this.newRange.Length > 0;
      this.currentRange = range;
      this.selectedIndex = this.mapper.Normalize(this.currentRange, this.selectedIndex);
      for (int index = 0; index < this.currentRange.Length; ++index)
      {
        int indexFromItemIndex = this.GetChildIndexFromItemIndex(this.mapper.MapFromRangeIndex(this.currentRange.ToRangeIndex(index)));
        if (indexFromItemIndex == -1 || this.Children.Count <= indexFromItemIndex)
          return;
        if (indexFromItemIndex != index)
          this.Children.Move(indexFromItemIndex, index);
      }
      if (!flag)
        return;
      this.AnimateToPositions();
    }

    private void SetUpItemsAnimationParameters()
    {
      for (int index = 0; index < this.Children.Count; ++index)
      {
        CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(index);
        itemAnimationData.ResetSpecialHandling();
        itemAnimationData.Interrupt = !itemAnimationData.IsOutAnimation();
      }
      Range left1 = this.mapper.CreateLeft(this.currentRange, this.newRange);
      for (int from = left1.From; from < left1.To; ++from)
      {
        int itemIndex = this.mapper.MapFromRangeIndex(from);
        int indexFromItemIndex = this.GetChildIndexFromItemIndex(itemIndex);
        if (indexFromItemIndex != -1)
        {
          CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(indexFromItemIndex);
          itemAnimationData.Interrupt = !itemAnimationData.IsOutAnimation();
          itemAnimationData.To = new double?(double.PositiveInfinity);
          this.Owner.CallOnItemLeaving(new ItemLeavingEventArgs(itemIndex));
        }
      }
      Range right1 = this.mapper.CreateRight(this.currentRange, this.newRange);
      for (int from = right1.From; from < right1.To; ++from)
      {
        int itemIndex = this.mapper.MapFromRangeIndex(from);
        int indexFromItemIndex = this.GetChildIndexFromItemIndex(itemIndex);
        if (indexFromItemIndex != -1)
        {
          CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(indexFromItemIndex);
          itemAnimationData.Interrupt = !itemAnimationData.IsOutAnimation();
          itemAnimationData.To = new double?(double.NegativeInfinity);
          this.Owner.CallOnItemLeaving(new ItemLeavingEventArgs(itemIndex));
        }
      }
      Range right2 = this.mapper.CreateRight(this.newRange, this.currentRange);
      for (int from = right2.From; from < right2.To; ++from)
      {
        int itemIndex = this.mapper.MapFromRangeIndex(from);
        int childIndex = this.EnsureItemToChildIndex(itemIndex);
        if (childIndex == -1)
          throw new Exception("EnsureItemToChildIndex failed!");
        CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(childIndex);
        itemAnimationData.Interrupt = true;
        itemAnimationData.From = new double?(double.NegativeInfinity);
        itemAnimationData.UpdateSpecialHandling();
        this.Owner.CallOnItemEntering(new ItemEnteringEventArgs(itemIndex));
      }
      Range left2 = this.mapper.CreateLeft(this.newRange, this.currentRange);
      for (int from = left2.From; from < left2.To; ++from)
      {
        int itemIndex = this.mapper.MapFromRangeIndex(from);
        int childIndex = this.EnsureItemToChildIndex(itemIndex);
        if (childIndex == -1)
          throw new Exception("EnsureItemToChildIndex failed!");
        CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(childIndex);
        itemAnimationData.Interrupt = true;
        itemAnimationData.From = new double?(double.PositiveInfinity);
        itemAnimationData.UpdateSpecialHandling();
        this.Owner.CallOnItemEntering(new ItemEnteringEventArgs(itemIndex));
      }
    }

    private CarouselPathAnimationData GetItemAnimationData(int index)
    {
      if (this.ElementState != ElementState.Loaded && this.ElementState != ElementState.Constructed)
        return (CarouselPathAnimationData) null;
      if (index >= this.Children.Count)
        return (CarouselPathAnimationData) null;
      return CarouselItemsContainer.GetItemAnimationData((RadObject) this.Children[index]);
    }

    private static CarouselPathAnimationData GetItemAnimationData(
      RadObject element)
    {
      CarouselPathAnimationData pathAnimationData = (CarouselPathAnimationData) element.GetValue(CarouselItemsContainer.CarouselAnimationData);
      if (pathAnimationData == null)
      {
        pathAnimationData = new CarouselPathAnimationData();
        int num = (int) element.SetValue(CarouselItemsContainer.CarouselAnimationData, (object) pathAnimationData);
      }
      return pathAnimationData;
    }

    private void SetPathCalculator(ICarouselPath value)
    {
      if (this.carouselPath != null)
        this.carouselPath.PropertyChanged -= new PropertyChangedEventHandler(this.PathCalculator_PropertyChanged);
      this.carouselPath = value;
      if (this.carouselPath == null)
        return;
      this.carouselPath.PropertyChanged += new PropertyChangedEventHandler(this.PathCalculator_PropertyChanged);
    }

    private void UpdateAnimationOnItemInsertRemove(
      int index,
      int change,
      double? from,
      double? to)
    {
      switch (index.CompareTo(this.selectedIndex))
      {
        case -1:
          this.currentRange.Extend(change, 0);
          break;
        case 0:
        case 1:
          this.currentRange.Extend(0, change);
          break;
      }
      CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(index);
      if (itemAnimationData == null)
        return;
      itemAnimationData.From = from;
      itemAnimationData.To = to;
    }

    private void SyncItemsAndUpdateCarousel(int index)
    {
      this.newRange = this.GetMaxRange(false);
      RadBitVector64 bitState;
      (bitState = this.BitState)[17592186044416L] = ((bitState[17592186044416L] ? 1 : 0) & (this.mapper.IsInRange(this.currentRange, index) ? 0 : (!this.mapper.IsInRange(this.newRange, index) ? 1 : 0))) != 0;
      if (this.GetBitState(17592186044416L) || this.update != 0)
        return;
      if (this.Items.Count > 0)
        this.SynchronizeCollections();
      else
        this.Reset();
      this.BitState[17592186044416L] = true;
      this.SynchronizeCollections();
    }

    public virtual bool UpdateCarousel()
    {
      this.BitState[17592186044416L] = false;
      if (this.update != 0)
        return false;
      if (this.items.Count > 0)
        this.SynchronizeCollections();
      else
        this.Reset();
      return true;
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (this.EnableAnimationOnFormResize)
        this.ResetItemsPositions();
      this.ForceUpdate();
    }

    public virtual void ForceUpdate()
    {
      PropertySetting propertySetting = new PropertySetting(CarouselItemsContainer.CarouselLocationProperty, (object) 3.25777);
      for (int index = 0; index < this.currentRange.Length; ++index)
      {
        int indexFromItemIndex = this.GetChildIndexFromItemIndex(this.mapper.MapFromRangeIndex(this.currentRange.ToRangeIndex(index)));
        if (indexFromItemIndex == -1)
          return;
        int num = (int) this.Children[indexFromItemIndex].ResetValue(CarouselItemsContainer.CarouselLocationProperty, ValueResetFlags.Animation);
        propertySetting.ApplyValue((RadObject) this.Children[indexFromItemIndex]);
      }
      this.UpdateCarousel();
    }

    private static bool AnimationRemoveCurrent(RadElement element, CarouselPathAnimationData data)
    {
      if (data == null)
        data = (CarouselPathAnimationData) element.GetValue(CarouselItemsContainer.CarouselAnimationData);
      if (data == null || data.CurrentAnimation == null)
        return true;
      bool flag = true;
      if (data.Interrupt)
      {
        data.CurrentAnimation.Stop((RadObject) element);
        data.CurrentAnimation = (AnimatedPropertySetting) null;
        flag = false;
      }
      return !flag;
    }

    private void Reset()
    {
      for (int index = this.Children.Count - 1; index >= 0; --index)
      {
        CarouselPathAnimationData itemAnimationData = this.GetItemAnimationData(index);
        itemAnimationData.Interrupt = true;
        if (itemAnimationData.CurrentAnimation != null)
          itemAnimationData.CurrentAnimation.Stop((RadObject) this.Children[index]);
      }
      this.Children.Clear();
      this.currentRange.SetRange(0, 0);
      this.newRange.SetRange(0, 0);
      this.selectedIndex = 0;
      this.BitState[17592186044416L] = false;
      this.mapper.ItemsCount = 0;
    }

    private int CalculateClosest(int value)
    {
      return this.mapper.Closest(this.selectedIndex, value);
    }

    private void SetZIndex(RadElement element, int zIndex)
    {
      new PropertySetting(RadElement.ZIndexProperty, (object) zIndex).ApplyValue((RadObject) element);
    }

    [Description("Get or set using the relative point coordinate. If set to true each point should be between 0 and 100")]
    public bool EnableRelativePath
    {
      set
      {
        this.CarouselPath.EnableRelativePath = value;
      }
      get
      {
        return this.CarouselPath.EnableRelativePath;
      }
    }

    [Description("Gets or sets a value indicating whether carousel will increnment or decrement item indexes when in auto-loop mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public AutoLoopDirections AutoLoopDirection
    {
      get
      {
        return this.autoLoopDirection;
      }
      set
      {
        this.autoLoopDirection = value;
        this.OnNotifyPropertyChanged(nameof (AutoLoopDirection));
      }
    }

    [Description("Gets or sets a value indicating that the Carousel will loop items automatically.")]
    [DefaultValue(false)]
    public bool EnableAutoLoop
    {
      get
      {
        return this.GetBitState(140737488355328L);
      }
      set
      {
        this.SetBitState(140737488355328L, value);
      }
    }

    [Description("Gets or sets a value indicating when carousel will pause looping if in auto-loop mode.")]
    [Category("AutoLoopBehavior")]
    [DefaultValue(AutoLoopPauseConditions.OnMouseOverCarousel)]
    public AutoLoopPauseConditions AutoLoopPauseCondition
    {
      get
      {
        return this.autoLoopPauseCondition;
      }
      set
      {
        this.autoLoopPauseCondition = value;
        this.OnNotifyPropertyChanged(nameof (AutoLoopPauseCondition));
      }
    }

    protected override void DisposeManagedResources()
    {
      this.items.ItemsChanged -= new ItemChangedDelegate(this.OnItemsChanged);
      base.DisposeManagedResources();
    }

    private class AnimationEntry
    {
      public AnimatedPropertySetting PropertySetting;
      public RadElement Element;

      public AnimationEntry(AnimatedPropertySetting PropertySetting, RadElement Element)
      {
        this.PropertySetting = PropertySetting;
        this.Element = Element;
      }

      public override bool Equals(object obj)
      {
        if (base.Equals(obj))
          return true;
        if (((CarouselItemsContainer.AnimationEntry) obj).PropertySetting == this.PropertySetting)
          return ((CarouselItemsContainer.AnimationEntry) obj).Element == this.Element;
        return false;
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }
    }
  }
}
