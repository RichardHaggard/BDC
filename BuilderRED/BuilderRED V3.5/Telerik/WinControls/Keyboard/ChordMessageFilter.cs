// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordMessageFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.Keyboard
{
  public class ChordMessageFilter : IMessageFilter
  {
    private Stack<Keys> stack = new Stack<Keys>();
    private Stack<Keys> tipsStack = new Stack<Keys>();
    private ObservableCollection<Shortcuts> chordsConsumers = new ObservableCollection<Shortcuts>();
    private List<IComponentTreeHandler> keyTipsConsumers = new List<IComponentTreeHandler>();
    private static readonly Dictionary<int, Keys> mappings = new Dictionary<int, Keys>(1);
    private static int previousKeyCode = 0;
    protected static List<ChordMessageFilter> registeredFilters = (List<ChordMessageFilter>) null;
    public static readonly int MaxChordSymbols = 5;
    public static int filterCount = 0;
    private static List<int> constants = new List<int>(6);
    private ChordModifier chordModifier;
    private static Keys[] validKeys;
    private static List<Keys> testKeys;
    private static Chord testChord;
    private bool keyMapActivated;
    private IComponentTreeHandler keyMapActivatedInstance;
    private static Dictionary<int, ChordMessageFilter> filterInstances;
    private bool isAltKeyDown;

    static ChordMessageFilter()
    {
      ChordMessageFilter.constants.Add(131072);
      ChordMessageFilter.constants.Add(17);
      ChordMessageFilter.constants.Add(18);
      ChordMessageFilter.constants.Add(262144);
      ChordMessageFilter.constants.Add(65536);
      ChordMessageFilter.constants.Add(16);
      ChordMessageFilter.validKeys = new Keys[94]
      {
        Keys.A,
        Keys.B,
        Keys.C,
        Keys.D,
        Keys.D0,
        Keys.D1,
        Keys.D2,
        Keys.D3,
        Keys.D4,
        Keys.D5,
        Keys.D6,
        Keys.D7,
        Keys.D8,
        Keys.D9,
        Keys.Delete,
        Keys.Down,
        Keys.E,
        Keys.End,
        Keys.F,
        Keys.F1,
        Keys.F10,
        Keys.F11,
        Keys.F12,
        Keys.F13,
        Keys.F14,
        Keys.F15,
        Keys.F16,
        Keys.F17,
        Keys.F18,
        Keys.F19,
        Keys.F2,
        Keys.F20,
        Keys.F21,
        Keys.F22,
        Keys.F23,
        Keys.F24,
        Keys.F3,
        Keys.F4,
        Keys.F5,
        Keys.F6,
        Keys.F7,
        Keys.F8,
        Keys.F9,
        Keys.G,
        Keys.H,
        Keys.I,
        Keys.Insert,
        Keys.J,
        Keys.K,
        Keys.L,
        Keys.Left,
        Keys.M,
        Keys.N,
        Keys.NumLock,
        Keys.NumPad0,
        Keys.NumPad1,
        Keys.NumPad2,
        Keys.NumPad3,
        Keys.NumPad4,
        Keys.NumPad5,
        Keys.NumPad6,
        Keys.NumPad7,
        Keys.NumPad8,
        Keys.NumPad9,
        Keys.O,
        Keys.OemBackslash,
        Keys.OemClear,
        Keys.OemCloseBrackets,
        Keys.Oemcomma,
        Keys.OemMinus,
        Keys.OemOpenBrackets,
        Keys.OemPeriod,
        Keys.OemPipe,
        Keys.Oemplus,
        Keys.OemQuestion,
        Keys.OemQuotes,
        Keys.OemSemicolon,
        Keys.Oemtilde,
        Keys.P,
        Keys.Pause,
        Keys.Q,
        Keys.R,
        Keys.Right,
        Keys.S,
        Keys.Space,
        Keys.T,
        Keys.Tab,
        Keys.U,
        Keys.Up,
        Keys.V,
        Keys.W,
        Keys.X,
        Keys.Y,
        Keys.Z
      };
      ChordMessageFilter.testKeys = new List<Keys>();
      ChordMessageFilter.testKeys.Add(Keys.D1);
      ChordMessageFilter.testKeys.Add(Keys.D2);
      ChordModifier chordModifier = new ChordModifier(false, true, true);
      ChordMessageFilter.testChord = new Chord(ChordMessageFilter.testKeys, chordModifier);
      ChordMessageFilter.registeredFilters = new List<ChordMessageFilter>();
    }

    internal ChordMessageFilter()
    {
    }

    public int RegistrationCount
    {
      get
      {
        return ChordMessageFilter.registeredFilters.Count;
      }
    }

    public static ChordMessageFilter Current
    {
      get
      {
        return ChordMessageFilter.CreateInstance();
      }
    }

    public static ChordMessageFilter CreateInstance()
    {
      if (ChordMessageFilter.filterInstances == null)
        ChordMessageFilter.filterInstances = new Dictionary<int, ChordMessageFilter>();
      if (ChordMessageFilter.filterInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId))
        return ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId];
      ChordMessageFilter chordMessageFilter = new ChordMessageFilter();
      ChordMessageFilter.filterInstances.Add(Thread.CurrentThread.ManagedThreadId, chordMessageFilter);
      return chordMessageFilter;
    }

    public static bool RegisterInstance()
    {
      if (ChordMessageFilter.filterInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId))
        return ChordMessageFilter.RegisterInstance(ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId]);
      return false;
    }

    internal static bool RegisterInstance(ChordMessageFilter filterInstance)
    {
      try
      {
        if (ChordMessageFilter.registeredFilters.Contains(filterInstance))
          return false;
        ChordMessageFilter.registeredFilters.Add(filterInstance);
        Application.AddMessageFilter((IMessageFilter) filterInstance);
        return true;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error while registering ChordMessageFilter instance", ex);
      }
    }

    public static bool UnregisterInstance()
    {
      if (ChordMessageFilter.filterInstances.ContainsKey(Thread.CurrentThread.ManagedThreadId))
        return ChordMessageFilter.UnregisterInstance(ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId]);
      return false;
    }

    public static bool UnregisterInstance(ChordMessageFilter filterInstance)
    {
      try
      {
        if (!ChordMessageFilter.registeredFilters.Contains(filterInstance))
          return false;
        ChordMessageFilter.registeredFilters.Remove(filterInstance);
        Application.RemoveMessageFilter((IMessageFilter) filterInstance);
        return true;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error while unregistering ChordMessageFilter instance", ex);
      }
    }

    public static bool UnregisterInstance(Shortcuts consumer)
    {
      try
      {
        if (!ChordMessageFilter.registeredFilters.Contains(ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId]))
          return false;
        ChordMessageFilter.registeredFilters.Remove(ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId]);
        Application.RemoveMessageFilter((IMessageFilter) ChordMessageFilter.filterInstances[Thread.CurrentThread.ManagedThreadId]);
        return true;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error while unregistering ChordMessageFilter instance", ex);
      }
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    bool IMessageFilter.PreFilterMessage(ref Message msg)
    {
      if (msg.Msg < 256 || msg.Msg > 264)
        return false;
      int wparam = (int) msg.WParam;
      this.SetModifiers(wparam, true);
      if (msg.Msg == 256 || msg.Msg == 260)
      {
        if ((msg.LParam.ToInt32() >> 16 & (int) ushort.MaxValue & 16384) <= 0 && !ChordMessageFilter.constants.Contains(wparam))
        {
          Keys keys = ChordMessageFilter.IsValidKey((Keys) wparam);
          this.stack.Push(keys);
          if (this.keyMapActivated)
            this.tipsStack.Push(keys);
        }
        if (this.IsRuntimeChord())
        {
          if (this.ProccessChord())
          {
            this.keyMapActivatedInstance = (IComponentTreeHandler) null;
            this.stack.Clear();
            ChordMessageFilter.previousKeyCode = wparam;
            return true;
          }
        }
        else if (wparam == 18 || wparam == 121)
          this.isAltKeyDown = true;
      }
      if (msg.Msg == 257 || msg.Msg == 261)
      {
        if ((wparam == 18 || wparam == 121) && (this.stack.Count == 0 && this.isAltKeyDown))
          this.InvokeKeyMaps();
        this.isAltKeyDown = false;
        this.SetModifiers(wparam, false);
        if (!this.chordModifier.ShiftModifier && !this.chordModifier.ControlModifier && !this.chordModifier.AltModifier)
        {
          this.chordModifier = (ChordModifier) null;
          this.stack.Clear();
          if ((wparam == 18 || wparam == 121) && ChordMessageFilter.previousKeyCode != 17)
          {
            ChordMessageFilter.previousKeyCode = wparam;
            return this.ProccessKeyMaps();
          }
        }
      }
      ChordMessageFilter.previousKeyCode = wparam;
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RegisterKeyTipsConsumer(IComponentTreeHandler consumer)
    {
      for (System.Type type = consumer.GetType(); (object) type != (object) typeof (object); type = type.BaseType)
      {
        if (type.Name == "RadRibbonBar" || type.Name == "RadRibbonBarBackstageView")
        {
          ChordMessageFilter.Current.keyTipsConsumers.Add(consumer);
          break;
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void UnregisterKeyTipsConsumer(IComponentTreeHandler consumer)
    {
      if (ChordMessageFilter.Current.keyTipsConsumers.Contains(consumer))
        ChordMessageFilter.Current.keyTipsConsumers.Remove(consumer);
      if (ChordMessageFilter.Current.keyMapActivatedInstance != consumer)
        return;
      ChordMessageFilter.Current.keyMapActivatedInstance = (IComponentTreeHandler) null;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void UnregisterKeyTipsConsumer(int index)
    {
      if (0 > index || index >= ChordMessageFilter.Current.keyTipsConsumers.Count)
        return;
      if (ChordMessageFilter.Current.keyMapActivatedInstance == ChordMessageFilter.Current.keyTipsConsumers[index])
        ChordMessageFilter.Current.keyMapActivatedInstance = (IComponentTreeHandler) null;
      ChordMessageFilter.Current.keyTipsConsumers.RemoveAt(index);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void ClearKeyTipsConsumers()
    {
      ChordMessageFilter.Current.keyTipsConsumers.Clear();
    }

    public bool ProccessKeyMaps()
    {
      RadControl activatedInstance1 = this.keyMapActivatedInstance as RadControl;
      if (this.keyMapActivatedInstance == null || activatedInstance1 == null || !activatedInstance1.EnableKeyMap)
        return false;
      IComponentTreeHandler activatedInstance2 = this.keyMapActivatedInstance;
      this.keyMapActivatedInstance = (IComponentTreeHandler) null;
      return activatedInstance2.Behavior.SetKeyMap();
    }

    public void InvokeKeyMaps()
    {
      if (this.keyMapActivatedInstance != null)
        return;
      for (int index = 0; index < this.keyTipsConsumers.Count; ++index)
      {
        if (this.keyTipsConsumers[index].Behavior.IsParentFormActive)
        {
          this.keyMapActivatedInstance = this.keyTipsConsumers[index];
          break;
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RegisterChordsConsumer(Shortcuts consumer)
    {
      if (ChordMessageFilter.Current.chordsConsumers.Count == 0)
        ChordMessageFilter.RegisterInstance();
      if (ChordMessageFilter.Current.chordsConsumers.Contains(consumer))
        return;
      ChordMessageFilter.Current.chordsConsumers.Add(consumer);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void UnregisterChordsConsumer(Shortcuts consumer)
    {
      if (ChordMessageFilter.Current.chordsConsumers.Contains(consumer))
        ChordMessageFilter.Current.chordsConsumers.Remove(consumer);
      if (ChordMessageFilter.Current.chordsConsumers.Count != 0)
        return;
      ChordMessageFilter.UnregisterInstance();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void UnregisterChordsConsumer(int index)
    {
      if (0 > index || index >= ChordMessageFilter.Current.chordsConsumers.Count)
        return;
      ChordMessageFilter.UnregisterChordsConsumer(ChordMessageFilter.Current.chordsConsumers[index]);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void ClearChordsConsumers()
    {
      ChordMessageFilter.Current.chordsConsumers.Clear();
      ChordMessageFilter.UnregisterInstance();
    }

    protected virtual InputBinding FindChordPattern()
    {
      if (this.chordsConsumers.Count == 0)
        return (InputBinding) null;
      return this.FindChordPattern((IList<Shortcuts>) this.chordsConsumers);
    }

    protected virtual InputBinding FindChordPattern(IList<Shortcuts> list)
    {
      Form activeForm = Form.ActiveForm;
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (list[index] != null)
        {
          Form form = (Form) null;
          if (list[index].OwnerForm != null)
            form = list[index].OwnerForm;
          if (form != null && !form.Disposing && (form == activeForm && list[index].InputBindings.Count != 0))
          {
            InputBinding chordPattern = this.FindChordPattern(list[index].InputBindings);
            if (chordPattern != null)
              return chordPattern;
          }
        }
      }
      return (InputBinding) null;
    }

    protected virtual InputBinding FindChordPattern(InputBindingsCollection list)
    {
      Chord runtimeChord = this.CreateRuntimeChord();
      for (int index = 0; index < list.Count; ++index)
      {
        if (runtimeChord.CompareTo((object) list[index].Chord) == 0 && list[index].CommandContext != null && (typeof (Control).IsAssignableFrom(list[index].CommandContext.GetType()) && !(list[index].CommandContext as Control).Disposing || typeof (RadItem).IsAssignableFrom(list[index].CommandContext.GetType())))
          return list[index];
      }
      return (InputBinding) null;
    }

    protected virtual Chord FindChordPattern(Chord[] list)
    {
      Chord runtimeChord = this.CreateRuntimeChord();
      for (int index = 0; index < list.Length; ++index)
      {
        if (runtimeChord.CompareTo((object) list[index]) == 0)
          return list[index];
      }
      return (Chord) null;
    }

    protected virtual bool ProccessChord()
    {
      InputBinding chordPattern = this.FindChordPattern();
      if (chordPattern == null || chordPattern.Chord == null)
        return false;
      if (chordPattern.Command != null)
        chordPattern.Command.Execute(chordPattern.CommandContext, null);
      return true;
    }

    protected virtual Chord CreateRuntimeChord()
    {
      List<Keys> keys = new List<Keys>(1);
      keys.AddRange((IEnumerable<Keys>) this.stack);
      return new Chord(keys, this.chordModifier);
    }

    protected void SetModifiers(int pressedKey, bool set)
    {
      Keys modifierKeys = Control.ModifierKeys;
      if (this.chordModifier == null)
        this.chordModifier = new ChordModifier();
      this.chordModifier.ShiftModifier = (modifierKeys & Keys.Shift) == Keys.Shift;
      this.chordModifier.ControlModifier = (modifierKeys & Keys.Control) == Keys.Control;
      if ((modifierKeys & Keys.Alt) == Keys.Alt)
        this.chordModifier.AltModifier = true;
      else
        this.chordModifier.AltModifier = false;
    }

    protected virtual bool IsRuntimeChord()
    {
      return this.stack.Count > 0;
    }

    public static byte CharCodeFromKeys(Keys k)
    {
      byte num = 0;
      if (k.ToString().Length == 1 || k.ToString().Length > 2 && k.ToString()[1] == ',')
        num = (byte) k.ToString()[0];
      else if (k.ToString().Length > 3 && k.ToString()[0] == 'D' && k.ToString()[2] == ',')
        num = (byte) k.ToString()[1];
      return num;
    }

    public static Keys KeysFromInt(int keys)
    {
      if (ChordMessageFilter.mappings.ContainsKey(keys))
        return ChordMessageFilter.mappings[keys];
      return Keys.None;
    }

    private static Keys IsValidKey(Keys keyCode)
    {
      foreach (Keys validKey in ChordMessageFilter.validKeys)
      {
        if (validKey == keyCode)
          return validKey;
      }
      return Keys.None;
    }
  }
}
