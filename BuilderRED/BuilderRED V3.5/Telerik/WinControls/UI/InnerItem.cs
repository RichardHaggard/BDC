// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.InnerItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class InnerItem : LightVisualElement
  {
    private FillPrimitive fill;
    private BorderPrimitive border;
    private InnerItemLayoutElement stack;

    static InnerItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new InnerItemStateManagerFactory(), typeof (InnerItem));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.fill = new FillPrimitive();
      this.fill.Class = "InnerItemFill";
      this.fill.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.border.Class = "InnerItemBorder";
      this.border.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.border);
      this.stack = new InnerItemLayoutElement();
      this.stack.Class = "InnerItemsLayout";
      this.Children.Add((RadElement) this.stack);
    }

    public RadQuickAccessToolBar ParentToolBar
    {
      get
      {
        return this.FindAncestor<RadQuickAccessToolBar>();
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    public InnerItemLayoutElement ContentLayout
    {
      get
      {
        return this.stack;
      }
    }

    public void ShowFillAndBorder(bool value)
    {
      if (this.fill != null)
      {
        int num1 = (int) this.fill.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      if (this.border != null)
      {
        int num2 = (int) this.border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      if (value)
        return;
      if (this.fill != null)
      {
        int num3 = (int) this.fill.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
      }
      if (this.border == null)
        return;
      int num4 = (int) this.border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
    }
  }
}
