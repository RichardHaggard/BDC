// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarVisualElement : LightVisualElement
  {
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (RadCommandBarVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    private string displayName = "";
    private CollectionBase owner;
    protected Orientation cachedOrientation;

    public RadCommandBarVisualElement()
    {
      this.cachedOrientation = (Orientation) this.GetValue(RadCommandBarVisualElement.OrientationProperty);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawText = false;
    }

    protected override Padding GetBorderThickness(bool checkDrawBorder)
    {
      return base.GetBorderThickness(true);
    }

    public virtual Orientation Orientation
    {
      get
      {
        return this.cachedOrientation;
      }
      set
      {
        if (value == this.cachedOrientation)
          return;
        this.FillPrimitiveImpl.InvalidateFillCache(RadCommandBarVisualElement.OrientationProperty);
        this.InvalidateMeasure();
        this.cachedOrientation = value;
        int num = (int) this.SetValue(RadCommandBarVisualElement.OrientationProperty, (object) value);
      }
    }

    [DefaultValue("")]
    [Localizable(true)]
    public string DisplayName
    {
      get
      {
        return this.displayName;
      }
      set
      {
        if (!(value != this.displayName))
          return;
        this.displayName = value;
      }
    }

    protected internal virtual void SetOwnerCommandBarCollection(CollectionBase ownerCollection)
    {
      this.owner = ownerCollection;
    }

    protected override void DisposeManagedResources()
    {
      if (this.owner != null)
      {
        int index = ((IList) this.owner).IndexOf((object) this);
        if (index >= 0)
          this.owner.RemoveAt(index);
        this.owner = (CollectionBase) null;
      }
      base.DisposeManagedResources();
    }
  }
}
