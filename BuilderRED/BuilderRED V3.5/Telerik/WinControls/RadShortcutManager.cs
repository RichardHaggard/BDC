// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadShortcutManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public sealed class RadShortcutManager : IKeyboardListener
  {
    [ThreadStatic]
    private static RadShortcutManager instance;
    private WeakReferenceList<IShortcutProvider> shortcutProviders;
    private bool hookInstalled;
    private bool enabled;
    private List<Keys> currentKeys;

    private RadShortcutManager()
    {
      this.enabled = true;
      this.shortcutProviders = new WeakReferenceList<IShortcutProvider>(true, false);
      this.currentKeys = new List<Keys>();
    }

    public static RadShortcutManager Instance
    {
      get
      {
        if (RadShortcutManager.instance == null)
          RadShortcutManager.instance = new RadShortcutManager();
        return RadShortcutManager.instance;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        this.enabled = value;
      }
    }

    public int ProvidersCount
    {
      get
      {
        return this.shortcutProviders.Count;
      }
    }

    public void AddShortcutProvider(IShortcutProvider provider)
    {
      if (this.ContainsShortcutProvider(provider))
        return;
      this.shortcutProviders.Add(provider);
      this.UpdateHook();
    }

    public void RemoveShortcutProvider(IShortcutProvider provider)
    {
      if (this.shortcutProviders.IndexOf(provider) < 0)
        return;
      this.shortcutProviders.Remove(provider);
      this.UpdateHook();
    }

    public bool ContainsShortcutProvider(IShortcutProvider provider)
    {
      return this.shortcutProviders.IndexOf(provider) >= 0;
    }

    private void UpdateHook()
    {
      if (this.shortcutProviders.Count > 0)
      {
        if (this.hookInstalled)
          return;
        RadKeyboardFilter.Instance.AddListener((IKeyboardListener) this);
        this.hookInstalled = true;
      }
      else
      {
        if (!this.hookInstalled)
          return;
        RadKeyboardFilter.Instance.RemoveListener((IKeyboardListener) this);
        this.hookInstalled = false;
      }
    }

    private bool IsModifierKey(Keys currKey)
    {
      if (currKey != Keys.ControlKey && currKey != Keys.ShiftKey)
        return currKey == Keys.Menu;
      return true;
    }

    MessagePreviewResult IKeyboardListener.OnPreviewKeyDown(
      Control target,
      KeyEventArgs e)
    {
      if (!this.enabled || this.IsModifierKey(e.KeyCode))
        return MessagePreviewResult.NotProcessed;
      this.currentKeys.Add(e.KeyCode);
      Keys modifiers = e.Modifiers;
      Keys[] array = this.currentKeys.ToArray();
      bool flag = false;
      foreach (IShortcutProvider shortcutProvider in this.shortcutProviders)
      {
        foreach (RadShortcut shortcut in shortcutProvider.Shortcuts)
        {
          if (shortcut.IsShortcutCombination(modifiers, array))
          {
            this.currentKeys.Clear();
            ShortcutEventArgs e1 = new ShortcutEventArgs(target, shortcut);
            shortcutProvider.OnShortcut(e1);
            if (e1.Handled)
              return MessagePreviewResult.ProcessedNoDispatch;
          }
          else if (!flag && shortcut.IsPartialShortcutCombination(modifiers, array))
          {
            PartialShortcutEventArgs e1 = new PartialShortcutEventArgs(target, shortcut, array);
            shortcutProvider.OnPartialShortcut(e1);
            if (e1.Handled)
              flag = true;
          }
        }
      }
      if (flag)
        return MessagePreviewResult.ProcessedNoDispatch;
      this.currentKeys.Clear();
      return MessagePreviewResult.NotProcessed;
    }

    MessagePreviewResult IKeyboardListener.OnPreviewKeyPress(
      Control target,
      KeyPressEventArgs e)
    {
      return MessagePreviewResult.NotProcessed;
    }

    MessagePreviewResult IKeyboardListener.OnPreviewKeyUp(
      Control target,
      KeyEventArgs e)
    {
      if (this.IsModifierKey(e.KeyCode))
        this.currentKeys.Clear();
      return MessagePreviewResult.NotProcessed;
    }
  }
}
