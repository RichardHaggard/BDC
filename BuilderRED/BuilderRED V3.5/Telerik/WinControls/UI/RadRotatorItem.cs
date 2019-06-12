// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRotatorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadRotatorItem : RadItem
  {
    private int currentIndex = -2;
    private int animationFrames = 10;
    private bool opacityAnimation = true;
    private SizeF locationAnimation = new SizeF(0.0f, -1f);
    private SizeF scaleAnimation = new SizeF(0.5f, 0.5f);
    private int removedIndex = -2;
    private bool shouldStopOnMouseOver = true;
    private static SizeF fullScale = new SizeF(1f, 1f);
    private static RadProperty AnimationSetting = RadProperty.Register(nameof (AnimationSetting), typeof (List<AnimatedPropertySetting>), typeof (RadRotatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static RadProperty fadeInProperty = RadProperty.Register("Class", typeof (List<AnimatedPropertySetting>), typeof (RadRotatorItem), new RadPropertyMetadata((object) string.Empty));
    private static readonly object ItemClickedEventKey = new object();
    private static readonly object StartRotationEventKey = new object();
    private static readonly object StopRotationEventKey = new object();
    private static readonly object BeginRotateEventKey = new object();
    private static readonly object EndRotateEventKey = new object();
    private RadItemOwnerCollection items;
    private RadItem defaultItem;
    private Timer timer;
    private int suspendRotate;
    private int animationsRunning;
    private bool shouldStop;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[12]
      {
        typeof (RadItemsContainer),
        typeof (RadRotatorElement),
        typeof (RadArrowButtonElement),
        typeof (RadDropDownListElement),
        typeof (RadButtonElement),
        typeof (RadWebBrowserElement),
        typeof (RadTextBoxElement),
        typeof (RadImageButtonElement),
        typeof (RadImageItem),
        typeof (RadCheckBoxElement),
        typeof (RadMaskedEditBoxElement),
        typeof (RadLabelElement)
      };
      this.items.ItemsChanged += new ItemChangedDelegate(this.ItemsChanged);
      this.items.Owner = (RadElement) this;
      this.timer = new Timer();
      this.timer.Tick += new EventHandler(this.Animate);
      this.timer.Interval = 2000;
    }

    private void ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          if (changed.IndexOf(target) <= this.currentIndex)
            ++this.currentIndex;
          target.Visibility = ElementVisibility.Hidden;
          target.NotifyParentOnMouseInput = true;
          break;
        case ItemsChangeOperation.Removing:
          this.removedIndex = changed.IndexOf(target);
          break;
        case ItemsChangeOperation.Removed:
          if (this.removedIndex != this.currentIndex)
          {
            this.removedIndex = -2;
            break;
          }
          this.removedIndex = -2;
          this.RemoveAnimations(target);
          if (this.items.Count <= 0)
            break;
          int index1 = (this.currentIndex + 1) % this.Items.Count;
          this.currentIndex = -2;
          this.Goto(index1);
          break;
        case ItemsChangeOperation.Set:
          bool flag = changed.IndexOf(target) == this.currentIndex;
          target.Visibility = flag ? ElementVisibility.Visible : ElementVisibility.Hidden;
          target.NotifyParentOnMouseInput = true;
          break;
        case ItemsChangeOperation.Clearing:
          for (int index2 = 0; index2 < this.Items.Count; ++index2)
            this.RemoveAnimations(this.Items[index2]);
          if (this.defaultItem != null)
          {
            this.currentIndex = -1;
            this.defaultItem.Visibility = ElementVisibility.Visible;
            break;
          }
          this.currentIndex = -2;
          break;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public RadItem DefaultItem
    {
      get
      {
        return this.defaultItem;
      }
      set
      {
        if (this.defaultItem == value)
          return;
        int index = this.Children.IndexOf((RadElement) this.defaultItem);
        if (index != -1)
          this.Children.RemoveAt(index);
        this.defaultItem = value;
        this.Children.Add((RadElement) this.defaultItem);
        this.OnNotifyPropertyChanged(nameof (DefaultItem));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(2000)]
    public int Interval
    {
      get
      {
        return this.timer.Interval;
      }
      set
      {
        this.timer.Interval = value;
      }
    }

    [DefaultValue(true)]
    public bool ShouldStopOnMouseOver
    {
      get
      {
        return this.shouldStopOnMouseOver;
      }
      set
      {
        this.shouldStopOnMouseOver = value;
      }
    }

    [DefaultValue(10)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        if (value <= 0)
          return;
        this.animationFrames = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool OpacityAnimation
    {
      get
      {
        return this.opacityAnimation;
      }
      set
      {
        if (this.opacityAnimation == value)
          return;
        this.opacityAnimation = value;
        this.OnNotifyPropertyChanged(nameof (OpacityAnimation));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SizeF LocationAnimation
    {
      get
      {
        return this.locationAnimation;
      }
      set
      {
        if (!(this.locationAnimation != value))
          return;
        this.locationAnimation = value;
        this.OnNotifyPropertyChanged(nameof (LocationAnimation));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentIndex
    {
      get
      {
        return this.currentIndex;
      }
      set
      {
        this.Goto(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadItem CurrentItem
    {
      get
      {
        if (this.currentIndex < -1 || this.currentIndex >= this.items.Count)
          return (RadItem) null;
        if (this.currentIndex != -1)
          return this.items[this.currentIndex];
        return this.defaultItem;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Running
    {
      get
      {
        return this.timer.Enabled;
      }
      set
      {
        if (this.timer.Enabled == value)
          return;
        if (value)
          this.Start(false);
        else
          this.Stop();
      }
    }

    private void ResetOpacity(RadItem target, double opacity)
    {
      target.Opacity = opacity;
    }

    private void Continue()
    {
      if (this.suspendRotate == 2)
      {
        this.AdvanceFrame();
        this.timer.Start();
      }
      this.suspendRotate = 0;
    }

    private void Pause()
    {
      if (this.suspendRotate != 0)
        return;
      this.suspendRotate = 1;
    }

    private void Animate(object sender, EventArgs e)
    {
      this.AdvanceFrame();
    }

    private void AdvanceFrame()
    {
      if (this.suspendRotate == 1)
      {
        this.suspendRotate = 2;
        this.timer.Stop();
      }
      else
      {
        if (this.Next())
          return;
        this.Stop();
      }
    }

    private bool SwapItems(int to)
    {
      if (this.currentIndex == to)
      {
        this.timer.Stop();
        return false;
      }
      BeginRotateEventArgs e = new BeginRotateEventArgs(this.currentIndex, to);
      this.OnBeginRotate((object) this, e);
      if (e.Cancel)
        return false;
      if (e.To < -1 || e.To >= this.items.Count)
        throw new ArgumentOutOfRangeException("ea.TO", "Should be a valid index in the Items collection, or -1 for default item");
      this.SwapItems(this.GetItem(this.currentIndex), this.GetItem(e.To));
      this.currentIndex = e.To;
      return true;
    }

    public List<AnimatedPropertySetting> fadeIn
    {
      get
      {
        return (List<AnimatedPropertySetting>) this.GetValue(RadRotatorItem.fadeInProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRotatorItem.fadeInProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      RadProperty property = e.Property;
      RadProperty fadeInProperty = RadRotatorItem.fadeInProperty;
    }

    private void SwapItems(RadItem itemToHide, RadItem itemToShow)
    {
      List<AnimatedPropertySetting> animatedPropertySettingList = itemToHide != null ? this.CreateAnimations(itemToHide, false) : (List<AnimatedPropertySetting>) null;
      this.fadeIn = itemToShow != null ? this.CreateAnimations(itemToShow, true) : (List<AnimatedPropertySetting>) null;
      if (animatedPropertySettingList != null)
      {
        for (int index = 0; index < animatedPropertySettingList.Count; ++index)
        {
          if (!animatedPropertySettingList[index].IsAnimating((RadObject) itemToHide))
          {
            ++this.animationsRunning;
            animatedPropertySettingList[index].ApplyValue((RadObject) itemToHide);
          }
        }
      }
      if (this.fadeIn == null)
        return;
      for (int index = 0; index < this.fadeIn.Count; ++index)
      {
        if (!this.fadeIn[index].IsAnimating((RadObject) itemToShow))
        {
          ++this.animationsRunning;
          this.fadeIn[index].ApplyValue((RadObject) itemToShow);
        }
      }
    }

    private List<AnimatedPropertySetting> CreateAnimations(
      RadItem item,
      bool show)
    {
      List<AnimatedPropertySetting> animations = this.GetAnimations(item);
      if (animations == null)
      {
        animations = new List<AnimatedPropertySetting>();
        this.SetAnimations(item, animations);
      }
      this.AddLocationAnimation(item, animations, show);
      if (this.opacityAnimation)
        this.AddOpacityAnimation(item, animations, show);
      else
        this.ResetOpacity(item, 1.0);
      this.AddScaleAnimation(item, animations, show);
      item.Visibility = ElementVisibility.Visible;
      return animations;
    }

    private void AddOpacityAnimation(
      RadItem item,
      List<AnimatedPropertySetting> animations,
      bool show)
    {
      item.Visibility = show ? ElementVisibility.Hidden : ElementVisibility.Visible;
      double num1 = show ? 1.0 : 0.0;
      double num2 = 1.0 - num1;
      AnimatedPropertySetting animation = new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) num2, (object) num1, this.animationFrames, 40);
      animation.ApplyEasingType = RadEasingType.InOutQuad;
      if (show)
      {
        animation.AnimationStarted += (AnimationStartedEventHandler) ((param0, param1) => item.Visibility = ElementVisibility.Visible);
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      }
      else
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          if (animations.Count == 0)
            item.Visibility = ElementVisibility.Hidden;
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      animations.Add(animation);
    }

    private void AddLocationAnimation(
      RadItem item,
      List<AnimatedPropertySetting> animations,
      bool show)
    {
      Rectangle rectangle1;
      Rectangle rectangle2;
      if (show)
      {
        item.Visibility = ElementVisibility.Hidden;
        rectangle1 = new Rectangle((int) ((double) item.Bounds.Width * (double) this.locationAnimation.Width), (int) ((double) item.Bounds.Height * (double) this.locationAnimation.Height), item.Bounds.Width, item.Bounds.Height);
        rectangle2 = new Rectangle(0, 0, item.Bounds.Width, item.Bounds.Height);
      }
      else
      {
        item.Visibility = ElementVisibility.Visible;
        rectangle1 = item.Bounds;
        rectangle2 = new Rectangle(-(int) ((double) this.Bounds.Width * (double) this.locationAnimation.Width), -(int) ((double) this.Bounds.Height * (double) this.locationAnimation.Height), item.Bounds.Width, item.Bounds.Height);
      }
      AnimatedPropertySetting animation = new AnimatedPropertySetting(RadElement.BoundsProperty, (object) rectangle1, (object) rectangle2, this.animationFrames, 40);
      animation.ApplyEasingType = RadEasingType.InOutQuad;
      if (show)
      {
        animation.AnimationStarted += (AnimationStartedEventHandler) ((param0, param1) => item.Visibility = ElementVisibility.Visible);
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      }
      else
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          if (animations.Count == 0)
            item.Visibility = ElementVisibility.Hidden;
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      animations.Add(animation);
    }

    private void AddScaleAnimation(
      RadItem item,
      List<AnimatedPropertySetting> animations,
      bool show)
    {
      SizeF sizeF1;
      SizeF sizeF2;
      if (show)
      {
        item.Visibility = ElementVisibility.Hidden;
        sizeF1 = this.scaleAnimation;
        sizeF2 = RadRotatorItem.fullScale;
      }
      else
      {
        item.Visibility = ElementVisibility.Visible;
        sizeF1 = RadRotatorItem.fullScale;
        sizeF2 = this.scaleAnimation;
      }
      AnimatedPropertySetting animation = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) sizeF1, (object) sizeF2, this.animationFrames, 40);
      animation.ApplyEasingType = RadEasingType.InOutQuad;
      if (show)
      {
        animation.AnimationStarted += (AnimationStartedEventHandler) ((param0, param1) => item.Visibility = ElementVisibility.Visible);
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      }
      else
        animation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
        {
          animations.Remove(animation);
          if (animations.Count == 0)
            item.Visibility = ElementVisibility.Hidden;
          --this.animationsRunning;
          if (this.animationsRunning != 0)
            return;
          this.OnEndRotate((object) this, EventArgs.Empty);
        });
      animations.Add(animation);
    }

    private List<AnimatedPropertySetting> GetAnimations(RadItem item)
    {
      return (List<AnimatedPropertySetting>) item.GetValue(RadRotatorItem.AnimationSetting) ?? (List<AnimatedPropertySetting>) null;
    }

    private void SetAnimations(RadItem item, List<AnimatedPropertySetting> animations)
    {
      int num = (int) item.SetValue(RadRotatorItem.AnimationSetting, (object) animations);
    }

    private void RemoveAnimations(RadItem item)
    {
      List<AnimatedPropertySetting> animations = this.GetAnimations(item);
      if (animations == null)
        return;
      for (int index = animations.Count - 1; index >= 0; --index)
        animations[index].Stop((RadObject) item);
      animations.Clear();
      this.SetAnimations(item, (List<AnimatedPropertySetting>) null);
    }

    private RadItem GetItem(int index)
    {
      if (index == -1)
        return this.defaultItem;
      if (index >= 0 && index < this.Items.Count)
        return this.items[index];
      return (RadItem) null;
    }

    protected override void DisposeManagedResources()
    {
      this.timer.Dispose();
      base.DisposeManagedResources();
    }

    public bool Start(bool startImmediately)
    {
      if (this.items.Count < 1)
        return false;
      if (this.timer.Enabled)
        return true;
      CancelEventArgs e = new CancelEventArgs();
      this.OnStartRotation((object) this, e);
      if (e.Cancel)
        return false;
      if (startImmediately)
        this.AdvanceFrame();
      this.timer.Start();
      return true;
    }

    public void Stop()
    {
      this.timer.Stop();
      if (this.animationsRunning > 0)
        this.shouldStop = true;
      else
        this.OnStopRotation((object) this, new EventArgs());
    }

    public bool Goto(int index)
    {
      if (index < -2 || index >= this.Items.Count)
        return false;
      if (this.currentIndex == index)
        return true;
      BeginRotateEventArgs e = new BeginRotateEventArgs(this.currentIndex, index);
      this.OnBeginRotate((object) this, e);
      if (e.Cancel)
        return true;
      RadItem itemToHide = this.GetItem(this.currentIndex);
      RadItem itemToShow = this.GetItem(e.To);
      this.currentIndex = index;
      this.SwapItems(itemToHide, itemToShow);
      return true;
    }

    public bool GotoDefault()
    {
      return this.Goto(-1);
    }

    public bool Next()
    {
      if (this.items.Count < 0)
        return false;
      return this.SwapItems(this.currentIndex < 0 ? 0 : (this.currentIndex + 1) % this.items.Count);
    }

    public bool Previous()
    {
      return this.SwapItems(this.currentIndex < 1 ? this.items.Count - 1 : this.currentIndex - 1);
    }

    public event EventHandler ItemClicked
    {
      add
      {
        this.Events.AddHandler(RadRotatorItem.ItemClickedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRotatorItem.ItemClickedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler StartRotation
    {
      add
      {
        this.Events.AddHandler(RadRotatorItem.StartRotationEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRotatorItem.StartRotationEventKey, (Delegate) value);
      }
    }

    public event EventHandler StopRotation
    {
      add
      {
        this.Events.AddHandler(RadRotatorItem.StopRotationEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRotatorItem.StopRotationEventKey, (Delegate) value);
      }
    }

    public event BeginRotateEventHandler BeginRotate
    {
      add
      {
        this.Events.AddHandler(RadRotatorItem.BeginRotateEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRotatorItem.BeginRotateEventKey, (Delegate) value);
      }
    }

    public event EventHandler EndRotate
    {
      add
      {
        this.Events.AddHandler(RadRotatorItem.EndRotateEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadRotatorItem.EndRotateEventKey, (Delegate) value);
      }
    }

    protected virtual void OnItemClicked(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadRotatorItem.ItemClickedEventKey];
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    protected virtual void OnStartRotation(object sender, CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadRotatorItem.StartRotationEventKey];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler(sender, e);
    }

    protected virtual void OnStopRotation(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadRotatorItem.StopRotationEventKey];
      if (eventHandler == null)
        return;
      eventHandler(sender, e);
    }

    protected virtual void OnBeginRotate(object sender, BeginRotateEventArgs e)
    {
      BeginRotateEventHandler rotateEventHandler = (BeginRotateEventHandler) this.Events[RadRotatorItem.BeginRotateEventKey];
      if (rotateEventHandler == null)
        return;
      rotateEventHandler(sender, e);
    }

    protected virtual void OnEndRotate(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadRotatorItem.EndRotateEventKey];
      if (eventHandler != null)
        eventHandler(sender, e);
      if (!this.shouldStop)
        return;
      this.shouldStop = false;
      this.OnStopRotation((object) this, EventArgs.Empty);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (!this.shouldStopOnMouseOver)
        return;
      this.Pause();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.Continue();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      switch (args.RoutedEvent.EventName)
      {
        case "MouseClickedEvent":
          RadItem currentItem = this.CurrentItem;
          if (currentItem == null)
            break;
          this.OnItemClicked((object) currentItem, new EventArgs());
          break;
      }
    }
  }
}
