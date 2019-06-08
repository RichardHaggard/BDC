// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarDropDownButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class CommandBarDropDownButton : RadCommandBarBaseItem, IItemsOwner
  {
    protected RadCommandBarArrowButton arrowButton;
    private RadDropDownMenu dropDownMenu;

    static CommandBarDropDownButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DropDownButtonStateManagerFatory(), typeof (CommandBarDropDownButton));
    }

    public CommandBarDropDownButton()
    {
      this.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DefaultButton;
    }

    public RadCommandBarArrowButton ArrowPart
    {
      get
      {
        return this.arrowButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadDropDownMenu DropDownMenu
    {
      get
      {
        return this.dropDownMenu;
      }
      set
      {
        if (this.dropDownMenu == value)
          return;
        this.UnwireDropDownEvents();
        this.dropDownMenu = value;
        this.WireDropDownEvents();
      }
    }

    [RadEditItemsAction]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.dropDownMenu.Items;
      }
    }

    private void dropDownMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) false);
    }

    private void dropDownMenu_PopupOpened(object sender, EventArgs args)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) true);
    }

    private void arrowButton_MouseLeave(object sender, EventArgs e)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.None);
    }

    private void arrowButton_MouseEnter(object sender, EventArgs e)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.OverArrowButton);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      int num = (int) this.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.OverActionButton);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num1 = (int) this.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.None);
      int num2 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
      this.ShowDropdown();
    }

    public override bool ProcessMnemonic(char charCode)
    {
      this.ShowDropdown();
      return base.ProcessMnemonic(charCode);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
    }

    public virtual void ShowDropdown()
    {
      if (this.Items.Count == 0)
        return;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control != null)
        this.dropDownMenu.ThemeName = control.ThemeName;
      this.dropDownMenu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.dropDownMenu.Show((RadItem) this, 0, this.Size.Height);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.dropDownMenu = new RadDropDownMenu((RadElement) this);
      this.dropDownMenu.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToEdges;
      this.WireDropDownEvents();
      this.arrowButton = new RadCommandBarArrowButton();
      this.arrowButton.Class = "CommandBarDropDownButtonArrow";
      this.arrowButton.MouseEnter += new EventHandler(this.arrowButton_MouseEnter);
      this.arrowButton.MouseLeave += new EventHandler(this.arrowButton_MouseLeave);
      this.Children.Add((RadElement) this.arrowButton);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.dropDownMenu.Dispose();
    }

    protected virtual void UnwireDropDownEvents()
    {
      this.dropDownMenu.PopupOpened -= new RadPopupOpenedEventHandler(this.dropDownMenu_PopupOpened);
      this.dropDownMenu.PopupClosed -= new RadPopupClosedEventHandler(this.dropDownMenu_PopupClosed);
    }

    protected virtual void WireDropDownEvents()
    {
      this.dropDownMenu.PopupOpened += new RadPopupOpenedEventHandler(this.dropDownMenu_PopupOpened);
      this.dropDownMenu.PopupClosed += new RadPopupClosedEventHandler(this.dropDownMenu_PopupClosed);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width += this.arrowButton.DesiredSize.Width;
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float width = this.arrowButton.DesiredSize.Width;
      this.arrowButton.Arrange(new RectangleF(clientRectangle.Left + (this.RightToLeft ? 0.0f : clientRectangle.Width - width), clientRectangle.Top, width, clientRectangle.Height));
      this.Layout.Arrange(new RectangleF(clientRectangle.Left + (this.RightToLeft ? width : 0.0f), clientRectangle.Top, clientRectangle.Width - width, clientRectangle.Height));
      return finalSize;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.ElementTree == null)
        return;
      this.dropDownMenu.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      RadControl radControl = this.ElementTree != null ? this.ElementTree.Control as RadControl : (RadControl) null;
      if (radControl == null)
        return;
      this.dropDownMenu.ImageList = radControl.ImageList;
    }
  }
}
