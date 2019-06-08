// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckmark
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCheckmark : RadItem
  {
    public static RadProperty CheckStateProperty = RadProperty.Register(nameof (CheckState), typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (RadCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsImageProperty = RadProperty.Register("IsImageElement", typeof (bool), typeof (RadCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (RadCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (RadCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (RadCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private ImagePrimitive imageElement;
    private CheckPrimitive checkElement;
    private bool enableVisualStates;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.ShouldHandleMouseInput = false;
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "CheckMarkBackGround";
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "CheckMarkBorder";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.CheckElement);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool VsbVisible
    {
      get
      {
        return this.enableVisualStates;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    public ImagePrimitive ImageElement
    {
      get
      {
        return this.imageElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual CheckPrimitive CheckElement
    {
      get
      {
        if (this.checkElement == null)
        {
          this.checkElement = new CheckPrimitive();
          int num = (int) this.checkElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
        }
        return this.checkElement;
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("Image", typeof (RadCheckmark))]
    [Category("Appearance")]
    [Description("Gets or sets the image that is displayed on a button element.")]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Image Image
    {
      get
      {
        return (Image) this.GetValue(RadCheckmark.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCheckmark.ImageProperty, (object) value);
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageIndex", typeof (RadCheckmark))]
    [Category("Appearance")]
    [Description("Gets or sets the image list index value of the image displayed as a checkmark.")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(RadCheckmark.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCheckmark.ImageIndexProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageKey", typeof (RadCheckmark))]
    [Category("Appearance")]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(RadCheckmark.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCheckmark.ImageKeyProperty, (object) value);
      }
    }

    public Telerik.WinControls.Enumerations.ToggleState CheckState
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(RadCheckmark.CheckStateProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCheckmark.CheckStateProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableVisualStates
    {
      get
      {
        return this.enableVisualStates;
      }
      set
      {
        if (this.enableVisualStates == value)
          return;
        this.enableVisualStates = value;
        this.OnEnableVisualStatesChanged(EventArgs.Empty);
      }
    }

    public override bool CanHaveOwnStyle
    {
      get
      {
        return this.enableVisualStates;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.EnsureImageElement();
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      if ((this.imageElement == null || this.checkElement == null) && (changeOperation == ItemsChangeOperation.Inserted && (bool) child.GetValue(RadCheckmark.IsImageProperty)))
      {
        this.imageElement = child as ImagePrimitive;
        this.SetCheckState();
      }
      base.OnChildrenChanged(child, changeOperation);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadCheckmark.CheckStateProperty)
        this.SetCheckState();
      else if (e.Property == RadCheckmark.ImageProperty)
      {
        this.EnsureImageElement();
        this.imageElement.Image = (Image) e.NewValue;
        this.SetCheckState();
      }
      else if (e.Property == RadCheckmark.ImageIndexProperty)
      {
        this.EnsureImageElement();
        this.imageElement.ImageIndex = (int) e.NewValue;
        this.SetCheckState();
      }
      else
      {
        if (e.Property != RadCheckmark.ImageKeyProperty)
          return;
        this.EnsureImageElement();
        this.imageElement.ImageKey = (string) e.NewValue;
        this.SetCheckState();
      }
    }

    private void OnEnableVisualStatesChanged(EventArgs e)
    {
      if (this.enableVisualStates)
      {
        this.ThemeRole = nameof (RadCheckmark);
        this.StateManager = (ItemStateManagerBase) new RadCheckmarkStateManager();
      }
      else
      {
        this.ThemeRole = (string) null;
        this.StateManager = (ItemStateManagerBase) null;
      }
    }

    protected virtual void SetCheckState()
    {
      RadElement checkElement = (RadElement) this.CheckElement;
      this.CheckElement.CheckState = this.CheckState;
      if (this.imageElement != null && this.imageElement.Image != null)
      {
        checkElement.Visibility = ElementVisibility.Hidden;
        if (this.CheckState == Telerik.WinControls.Enumerations.ToggleState.On || this.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
          this.imageElement.Visibility = ElementVisibility.Visible;
        else
          this.imageElement.Visibility = ElementVisibility.Hidden;
      }
      checkElement.Visibility = this.CheckState == Telerik.WinControls.Enumerations.ToggleState.On || this.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate ? ElementVisibility.Visible : ElementVisibility.Hidden;
      this.StartRippleAnimation();
    }

    private void StartRippleAnimation()
    {
      if (this.CheckState != Telerik.WinControls.Enumerations.ToggleState.On && this.CheckState != Telerik.WinControls.Enumerations.ToggleState.Indeterminate && (this.ElementTree == null || this.ElementTree.Control == null || !this.ElementTree.Control.ContainsFocus))
        return;
      Rectangle boundingRectangle = this.ControlBoundingRectangle;
      this.StartRippleAnimation(new MouseEventArgs(MouseButtons.Left, 1, boundingRectangle.X + boundingRectangle.Width / 2, boundingRectangle.Y + boundingRectangle.Height / 2, 0));
    }

    private void EnsureImageElement()
    {
      if (this.imageElement != null)
        return;
      this.imageElement = new ImagePrimitive();
      this.imageElement.Alignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.imageElement);
    }
  }
}
