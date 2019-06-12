// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ComponentBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Assistance;
using Telerik.WinControls.Keyboard;

namespace Telerik.WinControls
{
  public class ComponentBehavior : IDisposable
  {
    private static Size onePixel = new Size(1, 1);
    private FillRepository bitmapRepository = new FillRepository();
    protected internal bool ItemCaptureState = true;
    private int toolTipOffsetY = 40;
    private IntPtr focusedBeforeKeyMap = IntPtr.Zero;
    private Stack<RadItem> keyMapItems = new Stack<RadItem>();
    private Stack<Keys> keyMaps = new Stack<Keys>();
    private StringBuilder keyMapBuilder = new StringBuilder();
    private const int DefaultToolTipInitialDelay = 500;
    private IComponentTreeHandler owner;
    private Control ownerControl;
    private bool mouseOver;
    internal RadElement itemCapture;
    internal RadElement selectedElement;
    internal RadElement pressedElement;
    internal RadElement lastFocusedElement;
    internal RadElement currentFocusedElement;
    private RadElement elementToFocus;
    private bool allowShowFocusCues;
    private ToolTip tooltip;
    private bool showItemToolTips;
    private RadElement currentlyActiveTooltipItem;
    private RadElement currentlyActiveScreentipItem;
    private ScreenTipPresenter tipPresenter;
    private int toolTipOffsetX;
    private bool showCommandBindingsHints;
    private Shortcuts shortcuts;
    private bool isKeyMapActive;
    private bool enableKeyTips;
    private Cursor previousCursor;
    private AdornerLayer adornerLayer;
    private bool showScreenTipsBellowControl;

    public ComponentBehavior(IComponentTreeHandler owner)
    {
      this.owner = owner;
      this.ownerControl = owner as Control;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.lastFocusedElement = (RadElement) null;
      this.selectedElement = (RadElement) null;
      this.pressedElement = (RadElement) null;
      this.itemCapture = (RadElement) null;
      if (this.keyMapItems != null)
      {
        this.ClearKeyMapItemsSelection();
        this.keyMapItems.Clear();
      }
      if (this.tooltip != null)
      {
        this.tooltip.RemoveAll();
        this.tooltip.Dispose();
      }
      if (this.tipPresenter != null)
        this.tipPresenter.Dispose();
      if (this.shortcuts != null)
        this.shortcuts.Dispose();
      if (this.bitmapRepository != null)
        this.bitmapRepository.Dispose();
      this.owner = (IComponentTreeHandler) null;
      this.DisposeKeyTips();
    }

    protected internal RadItem GetActivatedItem(Control control, char charCode)
    {
      IComponentTreeHandler componentTreeHandler = control as IComponentTreeHandler;
      if (componentTreeHandler == null)
        return (RadItem) null;
      foreach (RadElement descendant in (IEnumerable<RadElement>) componentTreeHandler.RootElement.GetDescendants((Predicate<RadElement>) (e => true), TreeTraversalMode.BreadthFirst))
      {
        RadItem activatedItem = this.GetActivatedItem(descendant, charCode);
        if (activatedItem != null)
          return activatedItem;
      }
      return (RadItem) null;
    }

    protected internal RadItem GetActivatedItem(RadElement element, char charCode)
    {
      RadItem radItem = element as RadItem;
      if (radItem != null && (!string.IsNullOrEmpty(radItem.Text) && radItem.Enabled && (radItem.Visibility == ElementVisibility.Visible && radItem.ContainsMnemonic) && Control.IsMnemonic(charCode, radItem.Text) || !string.IsNullOrEmpty(radItem.Text) && radItem.Enabled && (radItem.Visibility == ElementVisibility.Visible && radItem.ContainsMnemonic) && TelerikHelper.IsPseudoMnemonic(charCode, radItem.Text)))
        return radItem;
      return (RadItem) null;
    }

    protected internal bool ProcessMnemonic(char charCode)
    {
      if (!TelerikHelper.CanProcessMnemonic(this.ownerControl) || (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
        return false;
      List<Control> mnemonicList = new List<Control>();
      this.GetThemedChildControlsList(this.ownerControl, mnemonicList);
      if (mnemonicList.Count > 0 && this.GetValidChildControlByMnemonic(mnemonicList, charCode) == null)
      {
        for (int index = 0; index < mnemonicList.Count; ++index)
        {
          RadItem activatedItem = this.GetActivatedItem(mnemonicList[index], charCode);
          if (activatedItem != null)
          {
            activatedItem.Focus();
            return activatedItem.ProcessMnemonic(charCode);
          }
        }
      }
      return false;
    }

    protected virtual void GetThemedChildControlsList(Control control, List<Control> mnemonicList)
    {
      if (control is IComponentTreeHandler && TelerikHelper.CanProcessMnemonicNoRecursive(control))
        mnemonicList.Add(control);
      foreach (Control control1 in (ArrangedElementCollection) control.Controls)
      {
        if (control1 != null && control1 is IComponentTreeHandler)
          this.GetThemedChildControlsList(control1, mnemonicList);
      }
    }

    protected virtual Control GetValidChildControlByMnemonic(
      List<Control> mnemonicList,
      char charCode)
    {
      if (mnemonicList.Count > 0)
        return (Control) null;
      for (int index = 0; index < mnemonicList.Count; ++index)
      {
        char mnemonic = WindowsFormsUtils.GetMnemonic(mnemonicList[index].Text, true);
        if (mnemonic != char.MinValue && charCode.Equals(mnemonic))
          return mnemonicList[index];
      }
      return (Control) null;
    }

    internal virtual void ProcessItemMnemonic(RadItem item, char charCode)
    {
      if (!TelerikHelper.CanProcessMnemonic(this.ownerControl) || item == null)
        return;
      item.Focus();
    }

    protected virtual string GetMnemonicText(string text)
    {
      char mnemonic = WindowsFormsUtils.GetMnemonic(text, false);
      if (mnemonic != char.MinValue)
        return "Alt+" + (object) mnemonic;
      return (string) null;
    }

    protected virtual bool ProcessMnemonicChar(char charCode)
    {
      return true;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement FocusedElement
    {
      get
      {
        return this.currentFocusedElement;
      }
      set
      {
        if (value != null)
        {
          value.Focus();
        }
        else
        {
          if (this.currentFocusedElement == null)
            return;
          this.currentFocusedElement.SetElementFocused(false);
          this.currentFocusedElement = (RadElement) null;
        }
      }
    }

    public virtual bool OnGotFocus(EventArgs e)
    {
      if (this.lastFocusedElement != null)
      {
        if (!this.lastFocusedElement.IsDisposed)
        {
          this.currentFocusedElement = this.lastFocusedElement;
          this.currentFocusedElement.SetElementFocused(true);
        }
        this.lastFocusedElement = (RadElement) null;
      }
      return false;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadElement LastFocusedElement
    {
      get
      {
        return this.lastFocusedElement;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public RadElement CurrentFocusedElement
    {
      get
      {
        return this.currentFocusedElement;
      }
    }

    public virtual bool OnLostFocus(EventArgs e)
    {
      if (this.currentFocusedElement != null)
      {
        this.currentFocusedElement.SetElementFocused(false);
        this.lastFocusedElement = this.currentFocusedElement;
      }
      if (this.isKeyMapActive)
        this.ResetKeyMapInternal();
      return false;
    }

    public bool IsFocusWithin(RadElement element)
    {
      RadElement radElement = this.elementToFocus != null ? this.elementToFocus : this.currentFocusedElement;
      if (radElement == null)
        return false;
      if (radElement == element)
        return true;
      foreach (RadElement descendant in element.GetDescendants((Predicate<RadElement>) (x => true), TreeTraversalMode.BreadthFirst))
      {
        if (descendant == radElement)
          return true;
      }
      return false;
    }

    internal void SettingElementFocused(RadElement toCangeFocus, bool focusValue)
    {
      this.elementToFocus = toCangeFocus;
      if (focusValue)
      {
        if (this.currentFocusedElement != null)
          this.currentFocusedElement.SetElementFocused(false);
        this.currentFocusedElement = toCangeFocus;
        this.lastFocusedElement = toCangeFocus;
      }
      else if (this.currentFocusedElement == toCangeFocus)
      {
        this.currentFocusedElement.SetElementFocused(false);
        this.currentFocusedElement = (RadElement) null;
      }
      this.elementToFocus = (RadElement) null;
    }

    [Category("Accessibility")]
    [DefaultValue(false)]
    public virtual bool AllowShowFocusCues
    {
      get
      {
        return this.allowShowFocusCues;
      }
      set
      {
        if (this.allowShowFocusCues == value)
          return;
        this.allowShowFocusCues = value;
        this.owner.InvalidateIfNotSuspended();
      }
    }

    internal bool ShouldShowFocusCues
    {
      get
      {
        if (this.AllowShowFocusCues)
          return this.owner.GetShowFocusCues();
        return false;
      }
    }

    public void SetElementValue(RadElement element, RadProperty dependencyProperty, object value)
    {
      if (this.ownerControl.InvokeRequired)
      {
        this.ownerControl.BeginInvoke((Delegate) new SetValueDelegate(((RadObject) element).SetValue), (object) dependencyProperty, value);
      }
      else
      {
        int num = (int) element.SetValue(dependencyProperty, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Form FindFormInternal(Control control)
    {
      Control control1 = control;
      while (control1.Parent != null)
        control1 = control1.Parent;
      return control1 as Form;
    }

    internal ToolTipTextNeededEventArgs OnToolTipTextNeeded(
      RadElement sender)
    {
      Size empty = Size.Empty;
      ToolTipTextNeededEventArgs e = new ToolTipTextNeededEventArgs(this.ToolTip, sender.ToolTipText, empty);
      this.owner.CallOnToolTipTextNeeded((object) sender, e);
      return e;
    }

    internal ScreenTipNeededEventArgs CallOnScreenTipNeeded(RadElement item)
    {
      Size empty = Size.Empty;
      if (this.ShowScreenTipsBellowControl)
      {
        empty.Height = this.ownerControl.ClientRectangle.Height;
        empty.Width = item.ControlBoundingRectangle.X - this.ownerControl.ClientRectangle.X;
      }
      else
        empty.Height = Math.Max(item.Size.Height, 15);
      ScreenTipNeededEventArgs e = new ScreenTipNeededEventArgs(item, empty);
      this.owner.CallOnScreenTipNeeded((object) this, e);
      return e;
    }

    public ToolTip ToolTip
    {
      get
      {
        if (this.tooltip == null)
          this.tooltip = this.Owner != null || this.Owner.ElementTree != null || this.Owner.ElementTree.RootElement != null ? (ToolTip) new RadToolTip(this.Owner.ElementTree.RootElement) : (ToolTip) new RadToolTip();
        return this.tooltip;
      }
    }

    [DefaultValue(40)]
    public int ToolTipOffsetY
    {
      get
      {
        return this.toolTipOffsetY;
      }
      set
      {
        this.toolTipOffsetY = value;
      }
    }

    [DefaultValue(0)]
    public int ToolTipOffsetX
    {
      get
      {
        return this.toolTipOffsetX;
      }
      set
      {
        this.toolTipOffsetX = value;
      }
    }

    internal ScreenTipPresenter ScreenPresenter
    {
      get
      {
        if (this.tipPresenter == null || this.tipPresenter.IsDisposed)
        {
          this.tipPresenter = new ScreenTipPresenter(this.OwnerControl);
          TelerikHelper.SetDropShadow(this.tipPresenter.Handle);
        }
        return this.tipPresenter;
      }
    }

    protected virtual bool DefaultShowItemToolTips
    {
      get
      {
        return true;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether ToolTips are shown for the RadItem objects contained in the RadControl.")]
    [DefaultValue(true)]
    public bool ShowItemToolTips
    {
      get
      {
        return this.showItemToolTips;
      }
      set
      {
        if (this.showItemToolTips == value)
          return;
        this.showItemToolTips = value;
        if (this.showItemToolTips)
          return;
        this.UpdateToolTip((RadElement) null);
      }
    }

    internal void UpdateScreenTip(ScreenTipNeededEventArgs args)
    {
      RadElement radElement = (RadElement) null;
      if (args != null)
        radElement = args.Item;
      if (!this.ShowItemToolTips || radElement == this.currentlyActiveScreentipItem)
        return;
      this.HideScreenTip();
      this.currentlyActiveScreentipItem = radElement;
      if (this.currentlyActiveScreentipItem == null)
        return;
      this.ShowScreenTip(args);
    }

    public void HideScreenTip()
    {
      this.ScreenPresenter.Hide();
    }

    internal void ShowScreenTip(ScreenTipNeededEventArgs args)
    {
      RadElement radElement = args.Item;
      Size offset = args.Offset;
      int delay = args.Delay;
      NativeMethods.SetWindowLong(new HandleRef((object) this, this.ScreenPresenter.Handle), -8, new HandleRef((object) this, NativeMethods.GetActiveWindow()));
      if (!(Cursor.Current != (Cursor) null))
        return;
      Point empty = Point.Empty;
      Point pivotPoint = !this.ShowScreenTipsBellowControl ? radElement.PointToScreen(radElement.Location) : this.ownerControl.PointToScreen(this.ownerControl.Location);
      pivotPoint.X += offset.Width;
      pivotPoint.Y += offset.Height;
      this.ScreenPresenter.Show((IScreenTipContent) radElement.ScreenTip, pivotPoint, delay);
    }

    public bool ShowScreenTipsBellowControl
    {
      get
      {
        return this.showScreenTipsBellowControl;
      }
      set
      {
        this.showScreenTipsBellowControl = value;
      }
    }

    internal void UpdateToolTip(RadElement item)
    {
      this.UpdateToolTip(item, Size.Empty);
    }

    internal void UpdateToolTip(RadElement item, Size offset)
    {
      if (this.ShowItemToolTips && item != this.currentlyActiveTooltipItem && this.ToolTip != null)
      {
        if (item == null)
          ((RadToolTip) this.ToolTip).Hide();
        this.ToolTip.Active = false;
        this.currentlyActiveTooltipItem = item;
        Cursor current = Cursor.Current;
        if (this.currentlyActiveTooltipItem != null && current != (Cursor) null)
        {
          this.ToolTip.Active = true;
          Point position = Cursor.Position;
          string toolTipText = this.currentlyActiveTooltipItem.ToolTipText;
          position.X += offset.Width;
          position.Y += offset.Height;
          ((RadToolTip) this.ToolTip).Show(toolTipText, position, this.ToolTip.AutoPopDelay);
        }
      }
      this.ToolTip.InitialDelay = 500;
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public InputBindingsCollection CommandBindings
    {
      get
      {
        return this.Shortcuts.InputBindings;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Shortcuts Shortcuts
    {
      get
      {
        if (this.shortcuts == null)
          this.shortcuts = new Shortcuts(this.ownerControl);
        return this.shortcuts;
      }
    }

    private bool ShowCommandBindingsHints
    {
      get
      {
        return this.showCommandBindingsHints;
      }
      set
      {
        if (this.showCommandBindingsHints == value)
          return;
        this.showCommandBindingsHints = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadItem ActiveKeyMapItem
    {
      get
      {
        if (this.keyMapItems.Count > 0)
          return this.keyMapItems.Peek();
        return (RadItem) null;
      }
      set
      {
        this.keyMapItems.Push(value);
        value.IsMouseOver = true;
        this.SettingElementFocused((RadElement) value, true);
      }
    }

    private void ClearKeyMapItemsSelection()
    {
      foreach (RadItem keyMapItem in this.keyMapItems)
        keyMapItem.IsMouseOver = keyMapItem.ContainsMouse;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsKeyMapActive
    {
      get
      {
        return this.isKeyMapActive;
      }
    }

    protected internal bool IsParentFormActive
    {
      get
      {
        Form formInternal = this.FindFormInternal(this.ownerControl);
        if (formInternal == null)
          return false;
        if (formInternal.IsMdiChild)
        {
          if (formInternal.MdiParent == null || formInternal.MdiParent.ActiveMdiChild != formInternal)
            return false;
        }
        else if (formInternal != Form.ActiveForm)
          return false;
        return true;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    public bool EnableKeyMap
    {
      get
      {
        return this.EnableKeyTips;
      }
      set
      {
        this.EnableKeyTips = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool EnableKeyTips
    {
      get
      {
        return this.enableKeyTips;
      }
      set
      {
        if (this.enableKeyTips == value)
          return;
        this.enableKeyTips = value;
        if (value)
          ChordMessageFilter.RegisterKeyTipsConsumer(this.owner);
        else
          ChordMessageFilter.UnregisterKeyTipsConsumer(this.owner);
        this.OnEnableKeyTipsChanged();
      }
    }

    protected virtual void DisposeKeyTips()
    {
      this.DisposeAdornerLayer();
      ChordMessageFilter.UnregisterKeyTipsConsumer(this.owner);
      this.focusedBeforeKeyMap = IntPtr.Zero;
    }

    public virtual bool SetKeyMap()
    {
      if (this.isKeyMapActive)
        this.ResetKeyMap();
      else
        this.InitializeKeyMap();
      return true;
    }

    protected virtual void InitializeKeyMap()
    {
      this.previousCursor = Cursor.Current;
      Cursor.Current = Cursors.Default;
      this.GetKeyMapFocus();
      this.isKeyMapActive = true;
      this.SetInternalKeyMapFocus();
      this.ClearKeyMapItemsSelection();
      this.keyMapItems.Clear();
      this.keyMaps.Clear();
      this.ownerControl.Invalidate();
    }

    protected virtual void ResetKeyMap()
    {
      Cursor.Current = this.previousCursor;
      this.previousCursor = (Cursor) null;
      this.ClearKeyMapItemsSelection();
      this.keyMapItems.Clear();
      this.keyMaps.Clear();
      this.isKeyMapActive = false;
      this.ownerControl.Capture = false;
      this.ReturnKeyMapFocus();
      this.ownerControl.Invalidate();
    }

    protected virtual void ResetKeyMapInternal()
    {
      Cursor.Current = this.previousCursor;
      this.previousCursor = (Cursor) null;
      this.ClearKeyMapItemsSelection();
      this.keyMapItems.Clear();
      this.keyMaps.Clear();
      this.isKeyMapActive = false;
      this.ownerControl.Capture = false;
      this.ownerControl.Invalidate();
    }

    protected internal bool GetKeyMapFocus()
    {
      this.focusedBeforeKeyMap = NativeMethods.GetFocus();
      return true;
    }

    protected virtual bool SetInternalKeyMapFocus()
    {
      return true;
    }

    protected internal bool ReturnKeyMapFocus()
    {
      if (this.focusedBeforeKeyMap != IntPtr.Zero)
        NativeMethods.SetFocus(new HandleRef((object) null, this.focusedBeforeKeyMap));
      return true;
    }

    protected virtual void OnEnableKeyTipsChanged()
    {
      int num = this.EnableKeyTips ? 1 : 0;
    }

    protected virtual void InitializeAdornerLayer()
    {
      if (this.adornerLayer != null)
      {
        this.adornerLayer.BringToFront();
      }
      else
      {
        this.adornerLayer = new AdornerLayer(this.ownerControl);
        this.adornerLayer.Bounds = new Rectangle(0, 0, this.ownerControl.Width, this.ownerControl.Height);
        this.adornerLayer.Visible = true;
        this.ownerControl.Controls.Add((Control) this.adornerLayer);
        this.adornerLayer.BringToFront();
      }
    }

    protected virtual void DisposeAdornerLayer()
    {
      if (this.adornerLayer == null)
        return;
      this.adornerLayer.Visible = false;
      this.ownerControl.Controls.Remove((Control) this.adornerLayer);
      this.adornerLayer.Dispose();
      this.adornerLayer = (AdornerLayer) null;
    }

    protected virtual List<RadItem> GetRootItems()
    {
      return (List<RadItem>) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ProccessKeyMap(Keys input)
    {
      if (input == Keys.Return || input == Keys.Space)
        return false;
      string stringRepresentation = this.GetKeyStringRepresentation(input);
      if (input == Keys.Escape)
      {
        if (this.keyMapItems.Count > 0)
        {
          RadItem radItem = this.keyMapItems.Pop();
          radItem.IsMouseOver = radItem.ContainsMouse;
        }
        else
          this.ResetKeyMap();
        this.ownerControl.Invalidate();
        return true;
      }
      if (char.IsLetterOrDigit(input.ToString(), 0))
        this.ActivateKeyTipItem(input, stringRepresentation);
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ActivateKeyTipItem(Keys input, string inputString)
    {
      if (!this.IsKeyMapActive || !this.IsPartOfKeyTip(input, inputString))
        return;
      this.ActivateSelectedItem(this.ActiveKeyMapItem);
      if (this.GetKeyFocusChildren(this.ActiveKeyMapItem).Count != 0)
        return;
      this.ResetKeyMap();
    }

    protected virtual bool ActivateSelectedItem(RadItem currentKeyMapItem)
    {
      return true;
    }

    protected virtual int ProcessUnmappedItems(List<RadItem> childrenToBeMapped)
    {
      int num1 = 0;
      List<RadItem> radItemList = new List<RadItem>();
      for (int index = 0; index < childrenToBeMapped.Count; ++index)
      {
        if (childrenToBeMapped[index].AutoNumberKeyTip > num1)
          num1 = childrenToBeMapped[index].AutoNumberKeyTip;
        if (string.IsNullOrEmpty(childrenToBeMapped[index].KeyTip) && childrenToBeMapped[index].AutoNumberKeyTip == 0)
          radItemList.Add(childrenToBeMapped[index]);
      }
      int num2 = num1 + 1;
      for (int index = 0; index < radItemList.Count; ++index)
      {
        radItemList[index].AutoNumberKeyTip = num2;
        ++num2;
      }
      return num2;
    }

    protected virtual int IsIndexOfItemKeyTip(RadItem item, string keyTipFragment)
    {
      string keyTip = item.KeyTip;
      if (item.KeyTip == string.Empty && item.AutoNumberKeyTip > 0)
        keyTip = item.AutoNumberKeyTip.ToString();
      if (keyTipFragment.Length > keyTip.Length)
        return -1;
      return keyTip.LastIndexOf(keyTipFragment, StringComparison.InvariantCulture);
    }

    protected virtual bool IsExactMatch(RadItem item, string keyTipFragment)
    {
      string keyTip = item.KeyTip;
      if (item.KeyTip == string.Empty && item.AutoNumberKeyTip > 0)
        keyTip = item.AutoNumberKeyTip.ToString();
      return keyTip.Equals(keyTipFragment, StringComparison.InvariantCulture);
    }

    protected virtual bool IsPartOfKeyTip(Keys input, string representation)
    {
      representation = representation.Replace("NUMPAD", "");
      bool flag = false;
      string keyTipFragment = this.keyMapBuilder.ToString() + representation;
      List<RadItem> currentKeyMap = this.GetCurrentKeyMap(this.ActiveKeyMapItem);
      this.ProcessUnmappedItems(currentKeyMap);
      for (int index = 0; index < currentKeyMap.Count; ++index)
      {
        if (this.IsIndexOfItemKeyTip(currentKeyMap[index], keyTipFragment) == 0)
        {
          flag = true;
          if (this.IsExactMatch(currentKeyMap[index], keyTipFragment))
          {
            this.ActiveKeyMapItem = currentKeyMap[index];
            this.keyMapBuilder.Length = 0;
            this.keyMaps.Clear();
            this.ownerControl.Invalidate();
            return true;
          }
        }
      }
      if (!flag)
        return false;
      if (representation != string.Empty)
        this.keyMapBuilder.Append(representation);
      else
        this.keyMapBuilder.Append(input.ToString());
      return true;
    }

    public virtual List<RadItem> GetCurrentKeyMap(RadItem currentKeyMapItem)
    {
      return this.GetKeyFocusChildren(currentKeyMapItem);
    }

    protected virtual List<RadItem> GetKeyFocusChildren(RadItem currentKeyMapItem)
    {
      return (List<RadItem>) null;
    }

    protected virtual string GetKeyStringRepresentation(Keys input)
    {
      return Convert.ToChar(NativeMethods.MapVirtualKey((uint) input, 2U)).ToString().ToUpper();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseOver
    {
      get
      {
        return this.mouseOver;
      }
      internal set
      {
        this.mouseOver = value;
      }
    }

    protected IComponentTreeHandler Owner
    {
      get
      {
        return this.owner;
      }
    }

    [Description("Gets the Control instance that hosts the TPF graph.")]
    [Browsable(false)]
    protected Control OwnerControl
    {
      get
      {
        return this.ownerControl;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadElement ItemCapture
    {
      get
      {
        return this.itemCapture;
      }
      set
      {
        this.itemCapture = value;
        this.ItemCaptureState = true;
      }
    }

    [Browsable(false)]
    public RadElement SelectedElement
    {
      get
      {
        return this.selectedElement;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public FillRepository BitmapRepository
    {
      get
      {
        return this.bitmapRepository;
      }
    }
  }
}
