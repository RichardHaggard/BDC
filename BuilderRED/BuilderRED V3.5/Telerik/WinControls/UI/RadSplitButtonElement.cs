// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSplitButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadSplitButtonElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadSplitButtonElement : RadDropDownButtonElement
  {
    private RadItem defaultItem;
    private LightVisualElement separator;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.separator = new LightVisualElement();
      int num1 = (int) this.separator.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(2, 2));
      this.separator.DrawText = false;
      int num2 = (int) this.separator.SetDefaultValueOverride(LightVisualElement.DrawFillProperty, (object) false);
      int num3 = (int) this.separator.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      int num4 = (int) this.separator.SetValue(DropDownEditorLayoutPanel.IsButtonSeparatorProperty, (object) true);
      this.layoutPanel.Children.Add((RadElement) this.separator);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ThemeRole = "SplitButton";
    }

    [Browsable(false)]
    public RadItem DefaultItem
    {
      get
      {
        return this.defaultItem;
      }
      set
      {
        if (value == this.defaultItem)
          return;
        this.defaultItem = value;
        this.OnDefaultItemChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public LightVisualElement ButtonSeparator
    {
      get
      {
        return this.separator;
      }
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs when the default item is changed.")]
    public event EventHandler DefaultItemChanged;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDefaultItemChanged(EventArgs e)
    {
      EventHandler defaultItemChanged = this.DefaultItemChanged;
      if (defaultItemChanged == null)
        return;
      defaultItemChanged((object) this, e);
    }

    internal override void DoOnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadElement.MouseUpEvent)
        this.IsMouseDown = false;
      if (args.RoutedEvent == RadElement.MouseDownEvent && (sender == this.ArrowButton || sender == this.ActionButton && this.defaultItem == null))
      {
        if (this.menu == null)
          return;
        if (this.Items.Count > 0 && !this.menu.IsVisible)
          this.ShowDropDown();
        else
          this.menu.ClosePopup(RadPopupCloseReason.Mouse);
      }
      if (args.RoutedEvent != RadElement.MouseClickedEvent || sender != this.ActionButton || this.defaultItem == null)
        return;
      this.defaultItem.PerformClick();
      this.DropDownMenu.ClosePopup(RadPopupCloseReason.Mouse);
      args.Canceled = true;
    }
  }
}
