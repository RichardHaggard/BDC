// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WindowsFormsUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  internal sealed class WindowsFormsUtils
  {
    public static readonly Size UninitializedSize = new Size(-7199369, -5999471);
    public static readonly ContentAlignment AnyRightAlign = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
    public static readonly ContentAlignment AnyLeftAlign = ContentAlignment.TopLeft | ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft;
    public static readonly ContentAlignment AnyTopAlign = ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight;
    public static readonly ContentAlignment AnyBottomAlign = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
    public static readonly ContentAlignment AnyMiddleAlign = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
    public static readonly ContentAlignment AnyCenterAlign = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;

    internal static string AssertControlInformation(bool condition, Control control)
    {
      if (condition)
        return string.Empty;
      return WindowsFormsUtils.GetControlInformation(control.Handle);
    }

    internal static Rectangle ConstrainToBounds(
      Rectangle constrainingBounds,
      Rectangle bounds)
    {
      if (!constrainingBounds.Contains(bounds))
      {
        bounds.Size = new Size(Math.Min(constrainingBounds.Width - 2, bounds.Width), Math.Min(constrainingBounds.Height - 2, bounds.Height));
        if (bounds.Right > constrainingBounds.Right)
          bounds.X = constrainingBounds.Right - bounds.Width;
        else if (bounds.Left < constrainingBounds.Left)
          bounds.X = constrainingBounds.Left;
        if (bounds.Bottom > constrainingBounds.Bottom)
        {
          bounds.Y = constrainingBounds.Bottom - 1 - bounds.Height;
          return bounds;
        }
        if (bounds.Top < constrainingBounds.Top)
          bounds.Y = constrainingBounds.Top;
      }
      return bounds;
    }

    internal static Rectangle ConstrainToScreenBounds(Rectangle bounds)
    {
      return WindowsFormsUtils.ConstrainToBounds(Screen.FromRectangle(bounds).Bounds, bounds);
    }

    internal static Rectangle ConstrainToScreenWorkingAreaBounds(Rectangle bounds)
    {
      return WindowsFormsUtils.ConstrainToBounds(Screen.GetWorkingArea(bounds), bounds);
    }

    public static bool ContainsMnemonic(string text)
    {
      if (text != null)
      {
        int length = text.Length;
        int num = text.IndexOf('&', 0);
        if (num >= 0 && num <= length - 2 && text.IndexOf('&', num + 1) == -1)
          return true;
      }
      return false;
    }

    internal static string EscapeTextWithAmpersands(string text)
    {
      if (text == null)
        return (string) null;
      int length = text.IndexOf('&');
      if (length == -1)
        return text;
      StringBuilder stringBuilder = new StringBuilder(text.Substring(0, length));
      for (; length < text.Length; ++length)
      {
        if (text[length] == '&')
          stringBuilder.Append("&");
        if (length < text.Length)
          stringBuilder.Append(text[length]);
      }
      return stringBuilder.ToString();
    }

    internal static int GetCombinedHashCodes(params int[] args)
    {
      int num = -757577119;
      for (int index = 0; index < args.Length; ++index)
        num = (args[index] ^ num) * -1640531535;
      return num;
    }

    public static string GetComponentName(IComponent component, string defaultNameValue)
    {
      string str = string.Empty;
      if (!string.IsNullOrEmpty(defaultNameValue))
        return defaultNameValue;
      if (component.Site != null)
        str = component.Site.Name;
      if (str == null)
        str = string.Empty;
      return str;
    }

    internal static string GetControlInformation(IntPtr hwnd)
    {
      return hwnd == IntPtr.Zero ? "Handle is IntPtr.Zero" : "";
    }

    public static char GetMnemonic(string text, bool bConvertToUpperCase)
    {
      char ch = char.MinValue;
      if (text != null)
      {
        int length = text.Length;
        for (int index = 0; index < length - 1; ++index)
        {
          if (text[index] == '&')
          {
            if (text[index + 1] == '&')
            {
              ++index;
            }
            else
            {
              ch = !bConvertToUpperCase ? char.ToLower(text[index + 1], CultureInfo.CurrentCulture) : char.ToUpper(text[index + 1], CultureInfo.CurrentCulture);
              break;
            }
          }
        }
      }
      return ch;
    }

    public static int RotateLeft(int value, int nBits)
    {
      nBits %= 32;
      return value << nBits | value >> 32 - nBits;
    }

    public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
    {
      if (string1 == null || string2 == null || string1.Length != string2.Length)
        return false;
      return string.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
    }

    public static string TextWithoutMnemonics(string text)
    {
      if (text == null)
        return (string) null;
      int length = text.IndexOf('&');
      if (length == -1)
        return text;
      StringBuilder stringBuilder = new StringBuilder(text.Substring(0, length));
      for (; length < text.Length; ++length)
      {
        if (text[length] == '&')
          ++length;
        if (length < text.Length)
          stringBuilder.Append(text[length]);
      }
      return stringBuilder.ToString();
    }

    public class ArraySubsetEnumerator : IEnumerator
    {
      private object[] array;
      private int current;
      private int total;

      public ArraySubsetEnumerator(object[] array, int count)
      {
        this.array = array;
        this.total = count;
        this.current = -1;
      }

      public bool MoveNext()
      {
        if (this.current >= this.total - 1)
          return false;
        ++this.current;
        return true;
      }

      public void Reset()
      {
        this.current = -1;
      }

      public object Current
      {
        get
        {
          if (this.current == -1)
            return (object) null;
          return this.array[this.current];
        }
      }
    }

    public static class EnumValidator
    {
      public static bool IsEnumWithinShiftedRange(
        Enum enumValue,
        int numBitsToShift,
        int minValAfterShift,
        int maxValAfterShift)
      {
        int int32 = Convert.ToInt32((object) enumValue, (IFormatProvider) CultureInfo.InvariantCulture);
        int num = int32 >> numBitsToShift;
        if (num << numBitsToShift == int32 && num >= minValAfterShift)
          return num <= maxValAfterShift;
        return false;
      }

      public static bool IsValidArrowDirection(System.Windows.Forms.ArrowDirection direction)
      {
        switch (direction)
        {
          case System.Windows.Forms.ArrowDirection.Left:
          case System.Windows.Forms.ArrowDirection.Up:
          case System.Windows.Forms.ArrowDirection.Right:
          case System.Windows.Forms.ArrowDirection.Down:
            return true;
          default:
            return false;
        }
      }
    }

    internal class WeakRefCollection : IList, ICollection, IEnumerable
    {
      private ArrayList _innerList;

      internal WeakRefCollection()
      {
        this._innerList = new ArrayList(4);
      }

      internal WeakRefCollection(int size)
      {
        this._innerList = new ArrayList(size);
      }

      public int Add(object value)
      {
        return this.InnerList.Add((object) this.CreateWeakRefObject(value));
      }

      public void Clear()
      {
        this.InnerList.Clear();
      }

      public bool Contains(object value)
      {
        return this.InnerList.Contains((object) this.CreateWeakRefObject(value));
      }

      private static void Copy(
        WindowsFormsUtils.WeakRefCollection sourceList,
        int sourceIndex,
        WindowsFormsUtils.WeakRefCollection destinationList,
        int destinationIndex,
        int length)
      {
        if (sourceIndex < destinationIndex)
        {
          sourceIndex += length;
          destinationIndex += length;
          for (; length > 0; --length)
            destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
        }
        else
        {
          for (; length > 0; --length)
            destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
        }
      }

      public void CopyTo(Array array, int index)
      {
        this.InnerList.CopyTo(array, index);
      }

      private WindowsFormsUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(
        object value)
      {
        if (value == null)
          return (WindowsFormsUtils.WeakRefCollection.WeakRefObject) null;
        return new WindowsFormsUtils.WeakRefCollection.WeakRefObject(value);
      }

      public override bool Equals(object obj)
      {
        WindowsFormsUtils.WeakRefCollection weakRefCollection = obj as WindowsFormsUtils.WeakRefCollection;
        if (weakRefCollection == null || this.Count != weakRefCollection.Count)
          return false;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this.InnerList[index] != weakRefCollection.InnerList[index])
            return false;
        }
        return true;
      }

      public IEnumerator GetEnumerator()
      {
        return this.InnerList.GetEnumerator();
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      public int IndexOf(object value)
      {
        return this.InnerList.IndexOf((object) this.CreateWeakRefObject(value));
      }

      public void Insert(int index, object value)
      {
        this.InnerList.Insert(index, (object) this.CreateWeakRefObject(value));
      }

      public void Remove(object value)
      {
        this.InnerList.Remove((object) this.CreateWeakRefObject(value));
      }

      public void RemoveAt(int index)
      {
        this.InnerList.RemoveAt(index);
      }

      public void ScavengeReferences()
      {
        int index1 = 0;
        int count = this.Count;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (this[index1] == null)
            this.InnerList.RemoveAt(index1);
          else
            ++index1;
        }
      }

      public int Count
      {
        get
        {
          return this.InnerList.Count;
        }
      }

      internal ArrayList InnerList
      {
        get
        {
          return this._innerList;
        }
      }

      public bool IsFixedSize
      {
        get
        {
          return this.InnerList.IsFixedSize;
        }
      }

      public bool IsReadOnly
      {
        get
        {
          return this.InnerList.IsReadOnly;
        }
      }

      public object this[int index]
      {
        get
        {
          WindowsFormsUtils.WeakRefCollection.WeakRefObject inner = this.InnerList[index] as WindowsFormsUtils.WeakRefCollection.WeakRefObject;
          if (inner != null && inner.IsAlive)
            return inner.Target;
          return (object) null;
        }
        set
        {
          this.InnerList[index] = (object) this.CreateWeakRefObject(value);
        }
      }

      bool ICollection.IsSynchronized
      {
        get
        {
          return this.InnerList.IsSynchronized;
        }
      }

      object ICollection.SyncRoot
      {
        get
        {
          return this.InnerList.SyncRoot;
        }
      }

      internal class WeakRefObject
      {
        private int hash;
        private object strongHolder;
        private WeakReference weakHolder;

        internal WeakRefObject(object obj)
          : this(obj, true)
        {
        }

        internal WeakRefObject(object obj, bool weakRef)
        {
          if (obj != null)
          {
            this.hash = obj.GetHashCode();
            if (weakRef)
            {
              this.weakHolder = new WeakReference(obj);
              this.strongHolder = (object) null;
            }
            else
            {
              this.strongHolder = obj;
              this.weakHolder = (WeakReference) null;
            }
          }
          else
          {
            this.weakHolder = (WeakReference) null;
            this.strongHolder = (object) null;
            this.hash = 0;
          }
        }

        public override bool Equals(object obj)
        {
          if (obj == this)
            return true;
          WindowsFormsUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as WindowsFormsUtils.WeakRefCollection.WeakRefObject;
          return weakRefObject != null && this.Target == weakRefObject.Target && this.Target != null;
        }

        public override int GetHashCode()
        {
          return this.hash;
        }

        internal bool IsAlive
        {
          get
          {
            if (this.IsWeakReference)
              return this.weakHolder.IsAlive;
            return true;
          }
        }

        internal bool IsWeakReference
        {
          get
          {
            return this.strongHolder == null;
          }
        }

        internal object Target
        {
          get
          {
            if (!this.IsWeakReference)
              return this.strongHolder;
            if (this.weakHolder != null)
              return this.weakHolder.Target;
            return (object) null;
          }
        }
      }
    }
  }
}
