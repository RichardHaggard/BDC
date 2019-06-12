// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxControlDefaultContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class TextBoxControlDefaultContextMenu : RadContextMenu
  {
    private readonly RadTextBoxControlElement textBox;
    private readonly RadMenuItem cutMenuItem;
    private readonly RadMenuItem copyMenuItem;
    private readonly RadMenuItem pasteMenuItem;
    private readonly RadMenuItem deleteMenuItem;
    private readonly RadMenuItem selectAllMenuItem;

    public TextBoxControlDefaultContextMenu(RadTextBoxControlElement textBox)
    {
      this.textBox = textBox;
      this.cutMenuItem = this.AddMenuItem("ContextMenuCut");
      this.copyMenuItem = this.AddMenuItem("ContextMenuCopy");
      this.pasteMenuItem = this.AddMenuItem("ContextMenuPaste");
      this.deleteMenuItem = this.AddMenuItem("ContextMenuDelete");
      this.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.selectAllMenuItem = this.AddMenuItem("ContextMenuSelectAll");
    }

    protected virtual RadMenuItem AddMenuItem(string stringId)
    {
      RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<TextBoxControlLocalizationProvider>.CurrentProvider.GetLocalizedString(stringId));
      this.Items.Add((RadItem) radMenuItem);
      radMenuItem.Click += new EventHandler(this.OnMenuItemClick);
      return radMenuItem;
    }

    protected RadTextBoxControlElement TextBox
    {
      get
      {
        return this.textBox;
      }
    }

    protected override void OnDropDownOpening(CancelEventArgs args)
    {
      this.ThemeName = this.textBox.ElementTree.ComponentTreeHandler.ThemeName;
      base.OnDropDownOpening(args);
      TextBoxControlLocalizationProvider currentProvider = LocalizationProvider<TextBoxControlLocalizationProvider>.CurrentProvider;
      bool flag = this.textBox.SelectionLength > 0;
      this.cutMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuCut");
      this.cutMenuItem.Enabled = flag && !this.TextBox.IsReadOnly;
      this.copyMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuCopy");
      this.copyMenuItem.Enabled = flag;
      this.pasteMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuPaste");
      this.pasteMenuItem.Enabled = Clipboard.ContainsText() && !this.TextBox.IsReadOnly;
      this.deleteMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuDelete");
      this.deleteMenuItem.Enabled = flag && !this.TextBox.IsReadOnly;
      this.selectAllMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuSelectAll");
      this.selectAllMenuItem.Enabled = this.textBox.SelectionStart != 0 || this.textBox.SelectionLength != this.textBox.TextLength;
    }

    private void OnMenuItemClick(object sender, EventArgs e)
    {
      if (sender == this.cutMenuItem)
        this.textBox.Cut();
      else if (sender == this.copyMenuItem)
        this.textBox.Copy();
      else if (sender == this.pasteMenuItem)
        this.textBox.Paste();
      else if (sender == this.deleteMenuItem)
      {
        this.textBox.Delete();
      }
      else
      {
        if (sender != this.selectAllMenuItem)
          return;
        this.textBox.SelectAll();
      }
    }
  }
}
