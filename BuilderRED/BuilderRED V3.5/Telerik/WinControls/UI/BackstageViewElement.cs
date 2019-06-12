// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageViewElement
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
  public class BackstageViewElement : BackstageVisualElement
  {
    public static RadProperty IsFullScreenProperty = RadProperty.Register(nameof (IsFullScreen), typeof (bool), typeof (BackstageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout));
    private int currentIndex = -1;
    private RadTitleBarElement titleBarElement;
    private BackstageItemsPanelElement itemsElement;
    private BackstageContentPanelElement contentElement;
    private BackstageVisualElement headerItem;
    private BackstageTabItem selectedItem;
    private RadItem currentItem;

    protected override void CreateChildElements()
    {
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.GradientStyle = GradientStyles.Solid;
      this.headerItem = new BackstageVisualElement();
      this.headerItem.DrawText = false;
      this.headerItem.DrawFill = true;
      this.headerItem.MinSize = new Size(0, 3);
      this.headerItem.Class = "BackstageViewHeader";
      this.Children.Add((RadElement) this.headerItem);
      this.titleBarElement = new RadTitleBarElement();
      this.titleBarElement.Class = "TitleBar";
      this.titleBarElement.TitlePrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.titleBarElement.Close += new TitleBarSystemEventHandler(this.titleBarElement_Close);
      this.titleBarElement.Minimize += new TitleBarSystemEventHandler(this.titleBarElement_Minimize);
      this.titleBarElement.MaximizeRestore += new TitleBarSystemEventHandler(this.titleBarElement_MaximizeRestore);
      this.Children.Add((RadElement) this.titleBarElement);
      this.itemsElement = new BackstageItemsPanelElement(this);
      this.itemsElement.DrawFill = true;
      this.Children.Add((RadElement) this.itemsElement);
      this.contentElement = new BackstageContentPanelElement();
      this.contentElement.DrawFill = true;
      this.Children.Add((RadElement) this.contentElement);
      base.CreateChildElements();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.titleBarElement.Close -= new TitleBarSystemEventHandler(this.titleBarElement_Close);
      this.titleBarElement.Minimize -= new TitleBarSystemEventHandler(this.titleBarElement_Minimize);
      this.titleBarElement.MaximizeRestore -= new TitleBarSystemEventHandler(this.titleBarElement_MaximizeRestore);
    }

    public BackstageItemsPanelElement ItemsPanelElement
    {
      get
      {
        return this.itemsElement;
      }
    }

    public BackstageContentPanelElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    public RadTitleBarElement TitleBarElement
    {
      get
      {
        return this.titleBarElement;
      }
    }

    [Description("Gets or sets the selected tab.")]
    public BackstageTabItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        this.SelectItemCore(value);
      }
    }

    [Category("Data")]
    [Description("Gets a collection representing the items contained in this backstage view.")]
    [Editor("Telerik.WinControls.UI.Design.RadRibbonBarBackstageItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.itemsElement.Items;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether this backstage view should be opened is full screen.")]
    public bool IsFullScreen
    {
      get
      {
        return (bool) this.GetValue(BackstageViewElement.IsFullScreenProperty);
      }
      set
      {
        int num = (int) this.SetValue(BackstageViewElement.IsFullScreenProperty, (object) value);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float height1 = this.headerItem.DesiredSize.Height;
      this.headerItem.Arrange(new RectangleF(clientRectangle.Location, new SizeF(clientRectangle.Width, height1)));
      float height2 = this.titleBarElement.DesiredSize.Height;
      this.titleBarElement.Arrange(new RectangleF(clientRectangle.Location, new SizeF(clientRectangle.Width, height2)));
      float width = this.itemsElement.DesiredSize.Width;
      float x1 = this.RightToLeft ? clientRectangle.Right - width : clientRectangle.Left;
      float num1 = this.IsFullScreenTheme() ? 0.0f : height1;
      this.itemsElement.Arrange(new RectangleF(x1, clientRectangle.Top + num1, width, clientRectangle.Height - height1));
      float x2 = this.RightToLeft ? clientRectangle.Left : clientRectangle.Left + width;
      float num2 = this.IsFullScreenTheme() ? height2 : height1;
      this.contentElement.Arrange(new RectangleF(x2, clientRectangle.Top + num2, clientRectangle.Width - width, clientRectangle.Height - num2));
      if (this.selectedItem != null)
      {
        this.selectedItem.Page.Bounds = this.contentElement.ControlBoundingRectangle;
        this.selectedItem.Page.Visible = true;
      }
      return finalSize;
    }

    internal void ProcessKeyboardSelection(Keys keyCode)
    {
      if (this.currentItem != null && (keyCode == Keys.Space || keyCode == Keys.Return))
        this.currentItem.CallDoClick(EventArgs.Empty);
      else if (keyCode == Keys.Right && !this.RightToLeft || keyCode == Keys.Left && this.RightToLeft)
      {
        (this.currentItem as BackstageTabItem)?.Page.Focus();
      }
      else
      {
        int selectionDirection = this.GetSelectionDirection(keyCode);
        if (selectionDirection == 0)
          return;
        this.HandleKeyboardNavigation(selectionDirection);
      }
    }

    protected virtual void SelectItemCore(BackstageTabItem backstageTabItem)
    {
      if (this.OnSelectedItemChanging(backstageTabItem))
        return;
      BackstageTabItem selectedItem = this.selectedItem;
      int count = this.Items.Count;
      this.SuspendLayout(true);
      for (int index = 0; index < count; ++index)
      {
        if (this.Items[index] != backstageTabItem)
        {
          BackstageTabItem backstageTabItem1 = this.Items[index] as BackstageTabItem;
          if (backstageTabItem1 != null)
          {
            backstageTabItem1.Selected = false;
            backstageTabItem1.Page.Visible = false;
          }
        }
      }
      this.selectedItem = backstageTabItem;
      if (!this.ElementTree.Control.Controls.Contains((Control) this.selectedItem.Page))
        this.ElementTree.Control.Controls.Add((Control) this.selectedItem.Page);
      this.selectedItem.Selected = true;
      this.InvalidateMeasure(true);
      this.ResumeLayout(true, true);
      this.OnSelectedItemChanged(this.selectedItem, selectedItem);
    }

    protected internal virtual bool IsFullScreenTheme()
    {
      foreach (PropertySettingGroup propertySettingGroup in ThemeRepository.FindTheme(!string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName) ? ThemeResolutionService.ApplicationThemeName : ((RadControl) this.ElementTree.Control).ThemeName).FindStyleGroup("Telerik.WinControls.UI.RadRibbonBarBackstageView").PropertySettingGroups)
      {
        if (propertySettingGroup.Selector.Value == nameof (BackstageViewElement))
        {
          foreach (PropertySetting propertySetting in propertySettingGroup.PropertySettings)
          {
            if (propertySetting.Name == "IsFullScreen")
              return (bool) propertySetting.Value;
          }
        }
      }
      return false;
    }

    private void HandleKeyboardNavigation(int dir)
    {
      if (this.currentIndex == -1 || this.currentItem == null)
      {
        this.currentIndex = 0;
        this.currentItem = this.Items.Count > 0 ? this.Items[0] : (RadItem) null;
      }
      else
      {
        this.currentIndex += dir;
        if (this.currentIndex < 0)
          this.currentIndex = this.Items.Count - 1;
        if (this.currentIndex >= this.Items.Count)
          this.currentIndex = 0;
        this.currentItem = this.currentIndex < 0 || this.currentIndex >= this.Items.Count ? (RadItem) null : this.Items[this.currentIndex];
      }
      this.ResetIsCurrentProperties();
      BackstageTabItem currentItem = this.currentItem as BackstageTabItem;
      if (currentItem != null)
      {
        this.SelectedItem = currentItem;
      }
      else
      {
        if (this.currentItem == null)
          return;
        int num = (int) this.currentItem.SetValue(BackstageButtonItem.IsCurrentProperty, (object) true);
      }
    }

    private void ResetIsCurrentProperties()
    {
      foreach (RadObject radObject in (RadItemCollection) this.Items)
      {
        int num = (int) radObject.SetValue(BackstageButtonItem.IsCurrentProperty, (object) false);
      }
    }

    private int GetSelectionDirection(Keys keyCode)
    {
      if (keyCode == Keys.Up)
        return -1;
      return keyCode == Keys.Down ? 1 : 0;
    }

    private void titleBarElement_MaximizeRestore(object sender, EventArgs args)
    {
      Form form = this.ElementTree.Control.FindForm();
      if (form == null)
        return;
      form.WindowState = form.WindowState != FormWindowState.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
      int num = (int) this.titleBarElement.MaximizeButton.SetValue(RadFormElement.FormWindowStateProperty, (object) form.WindowState);
    }

    private void titleBarElement_Close(object sender, EventArgs args)
    {
      this.ElementTree.Control.FindForm()?.Close();
    }

    private void titleBarElement_Minimize(object sender, EventArgs args)
    {
      Form form = this.ElementTree.Control.FindForm();
      if (form == null)
        return;
      form.WindowState = FormWindowState.Minimized;
    }

    public event EventHandler<BackstageItemEventArgs> ItemClicked;

    protected internal virtual void OnItemClicked(BackstageVisualElement backstageItem)
    {
      this.currentItem = (RadItem) backstageItem;
      this.currentIndex = this.Items.IndexOf((RadItem) backstageItem);
      this.ResetIsCurrentProperties();
      if (this.ItemClicked == null)
        return;
      this.ItemClicked((object) this, new BackstageItemEventArgs(backstageItem));
    }

    public event EventHandler<BackstageItemChangingEventArgs> SelectedItemChanging;

    protected virtual bool OnSelectedItemChanging(BackstageTabItem backstageTabItem)
    {
      if (this.SelectedItemChanging == null)
        return false;
      BackstageItemChangingEventArgs e = new BackstageItemChangingEventArgs(backstageTabItem, this.selectedItem);
      this.SelectedItemChanging((object) this, e);
      return e.Cancel;
    }

    public event EventHandler<BackstageItemChangeEventArgs> SelectedItemChanged;

    protected virtual void OnSelectedItemChanged(BackstageTabItem newItem, BackstageTabItem oldItem)
    {
      if (this.SelectedItemChanged == null)
        return;
      this.SelectedItemChanged((object) this, new BackstageItemChangeEventArgs(newItem, oldItem));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.Property.Name == "IsFullScreen"))
        return;
      if ((bool) e.NewValue)
      {
        this.headerItem.Visibility = ElementVisibility.Collapsed;
        this.titleBarElement.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.headerItem.Visibility = ElementVisibility.Visible;
        this.titleBarElement.Visibility = ElementVisibility.Collapsed;
      }
      this.Parent.InvalidateMeasure(true);
      this.Parent.UpdateLayout();
    }
  }
}
