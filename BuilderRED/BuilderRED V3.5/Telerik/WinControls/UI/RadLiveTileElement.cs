// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLiveTileElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadLiveTileElement : RadTileElement
  {
    private static readonly object syncObj = new object();
    private ContentTransitionType transitionType = ContentTransitionType.Fade;
    private int animationFrames = 50;
    private int animationInterval = 10;
    private bool enableAnimations = true;
    private RadItemCollection items;
    private RadElement currentItem;
    private RadElement oldItem;
    private int currentIndex;
    private Timer timer;
    private AnimatedPropertySetting currentInAnimation;
    private AnimatedPropertySetting currentOutAnimation;

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the interval at which the content of RadLiveTileElement changes.")]
    [DefaultValue(3000)]
    public int ContentChangeInterval
    {
      get
      {
        if (this.timer == null)
          return 0;
        return this.timer.Interval;
      }
      set
      {
        if (this.timer == null)
          return;
        this.timer.Interval = value;
      }
    }

    [Description("Gets or sets a value indicating whether the animations are enabled.")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool EnableAnimations
    {
      get
      {
        return this.enableAnimations;
      }
      set
      {
        this.enableAnimations = value;
      }
    }

    [DefaultValue(50)]
    [Description("Gets or sets the number of frames of the transition animation.")]
    [Browsable(true)]
    [Category("Behavior")]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        this.animationFrames = value;
      }
    }

    [Description("Gets or sets the interval between each frame of the transition animation.")]
    [Browsable(true)]
    [DefaultValue(10)]
    [Category("Behavior")]
    public int AnimationInterval
    {
      get
      {
        return this.animationInterval;
      }
      set
      {
        this.animationInterval = value;
      }
    }

    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.LiveTileFrameCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection of RadElement objects that represent the content items of the RadLiveTileElement")]
    public virtual RadItemCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the type of the transition animation.")]
    [DefaultValue(ContentTransitionType.Fade)]
    [Category("Behavior")]
    public ContentTransitionType TransitionType
    {
      get
      {
        return this.transitionType;
      }
      set
      {
        this.transitionType = value;
      }
    }

    [Description("Gets or sets the currently displayed item.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement CurrentItem
    {
      get
      {
        return this.currentItem;
      }
      set
      {
        if (this.Site != null || this.currentItem == value)
          return;
        lock (RadLiveTileElement.syncObj)
          this.SetCurrentItemCore(value);
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadTileElement);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.items = new RadItemCollection();
      this.timer = new Timer();
      this.timer.Interval = 3000;
      if (this.Site == null)
      {
        this.timer.Tick += new EventHandler(this.timer_Tick);
        this.timer.Start();
      }
      this.ClipDrawing = true;
    }

    protected override void DisposeManagedResources()
    {
      this.timer.Stop();
      this.timer.Dispose();
      base.DisposeManagedResources();
    }

    protected virtual void ProcessFadeAnimation()
    {
      if (this.oldItem != null && !this.oldItem.IsDisposed)
      {
        this.currentOutAnimation = new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 0.0, this.AnimationFrames, this.AnimationInterval);
        this.currentOutAnimation.RemoveAfterApply = true;
        this.currentOutAnimation.ApplyValue((RadObject) this.oldItem);
        this.currentOutAnimation.AnimationFinished += new AnimationFinishedEventHandler(this.outAnimation_AnimationFinished);
      }
      if (this.currentItem == null || this.currentItem.IsDisposed)
        return;
      this.currentInAnimation = new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 0.0, (object) 1.0, this.AnimationFrames, this.AnimationInterval);
      this.currentInAnimation.RemoveAfterApply = true;
      this.currentInAnimation.ApplyValue((RadObject) this.currentItem);
    }

    protected virtual void ProcessSlideAnimation()
    {
      if (this.oldItem != null && !this.oldItem.IsDisposed)
      {
        this.currentOutAnimation = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) SizeF.Empty, (object) this.GetSlideOutOffset(), this.AnimationFrames, this.AnimationInterval);
        this.currentOutAnimation.RemoveAfterApply = true;
        this.currentOutAnimation.AnimationFinished += new AnimationFinishedEventHandler(this.outAnimation_AnimationFinished);
        this.currentOutAnimation.ApplyValue((RadObject) this.oldItem);
      }
      if (this.currentItem == null || this.currentItem.IsDisposed)
        return;
      this.currentInAnimation = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) this.GetSlideInOffset(), (object) SizeF.Empty, this.AnimationFrames, this.AnimationInterval);
      this.currentInAnimation.RemoveAfterApply = true;
      this.currentInAnimation.ApplyValue((RadObject) this.currentItem);
    }

    private SizeF GetSlideInOffset()
    {
      switch (this.transitionType)
      {
        case ContentTransitionType.SlideLeft:
          return new SizeF((float) this.Bounds.Width, 0.0f);
        case ContentTransitionType.SlideRight:
          return new SizeF((float) -this.Bounds.Width, 0.0f);
        case ContentTransitionType.SlideUp:
          return new SizeF(0.0f, (float) this.Bounds.Height);
        case ContentTransitionType.SlideDown:
          return new SizeF(0.0f, (float) -this.Bounds.Height);
        default:
          return SizeF.Empty;
      }
    }

    private SizeF GetSlideOutOffset()
    {
      switch (this.transitionType)
      {
        case ContentTransitionType.SlideLeft:
          return new SizeF((float) -this.Bounds.Width, 0.0f);
        case ContentTransitionType.SlideRight:
          return new SizeF((float) this.Bounds.Width, 0.0f);
        case ContentTransitionType.SlideUp:
          return new SizeF(0.0f, (float) -this.Bounds.Height);
        case ContentTransitionType.SlideDown:
          return new SizeF(0.0f, (float) this.Bounds.Height);
        default:
          return SizeF.Empty;
      }
    }

    private void SetCurrentItemCore(RadElement value)
    {
      this.CancelAnimations(this.currentItem);
      this.CancelAnimations(value);
      if (value != null && !this.Children.Contains(value))
        this.Children.Add(value);
      this.oldItem = this.currentItem;
      this.currentItem = value;
      if (this.EnableAnimations)
      {
        if (this.transitionType == ContentTransitionType.Fade)
          this.ProcessFadeAnimation();
        else
          this.ProcessSlideAnimation();
      }
      else
      {
        if (this.oldItem == null)
          return;
        this.Children.Remove(this.oldItem);
      }
    }

    private void CancelAnimations(RadElement element)
    {
      if (element == null || element.IsDisposed)
        return;
      if (this.currentInAnimation != null && this.currentInAnimation.IsAnimating((RadObject) element))
        this.currentInAnimation.Cancel((RadObject) element);
      if (this.currentOutAnimation == null || !this.currentOutAnimation.IsAnimating((RadObject) element))
        return;
      this.currentOutAnimation.Cancel((RadObject) element);
    }

    public void CancelAnimations()
    {
      foreach (RadElement element in this.Items)
        this.CancelAnimations(element);
    }

    public void PauseAnimations()
    {
      this.timer.Stop();
      this.CancelAnimations();
    }

    public void ContinueAnimations()
    {
      this.timer.Start();
    }

    public void NextFrame()
    {
      this.ChangeContent();
    }

    protected virtual void ChangeContent()
    {
      if (this.Items.Count == 0)
        return;
      ++this.currentIndex;
      this.currentIndex %= this.Items.Count;
      this.CurrentItem = (RadElement) this.Items[this.currentIndex];
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (this.Parent == null)
        return;
      this.ChangeContent();
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
      {
        this.timer.Stop();
        this.CancelAnimations();
        this.Children.Clear();
        this.currentItem = (RadElement) null;
      }
      else
      {
        this.currentIndex = this.items.Count - 1;
        this.ChangeContent();
        this.timer.Start();
      }
    }

    private void outAnimation_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      (sender as AnimatedPropertySetting).AnimationFinished -= new AnimationFinishedEventHandler(this.outAnimation_AnimationFinished);
      if (e.Element == null || !this.Children.Contains(e.Element))
        return;
      this.Children.Remove(e.Element);
    }
  }
}
