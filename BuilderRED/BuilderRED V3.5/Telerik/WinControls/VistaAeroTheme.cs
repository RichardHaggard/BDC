// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.VistaAeroTheme
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms.VisualStyles;

namespace Telerik.WinControls
{
  public static class VistaAeroTheme
  {
    public static class ComboBox
    {
      private static readonly string ClassName = "Combobox";

      public static class Border
      {
        private static readonly int Part = 4;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Border.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Border.Part, 2);
        public static readonly VisualStyleElement Focused = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Border.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Border.Part, 4);
      }

      public static class Readonly
      {
        private static readonly int Part = 5;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Readonly.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Readonly.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Readonly.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.Readonly.Part, 4);
      }

      public static class DropDownButton
      {
        private static readonly int Part = 6;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.DropDownButton.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.DropDownButton.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.DropDownButton.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.ComboBox.ClassName, VistaAeroTheme.ComboBox.DropDownButton.Part, 4);
      }
    }

    public static class DatePicker
    {
      private static readonly string ClassName = "Datepicker";

      public static class Border
      {
        private static readonly int Part = 2;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.Border.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.Border.Part, 2);
        public static readonly VisualStyleElement Focused = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.Border.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.Border.Part, 4);
      }

      public static class DropDownButton
      {
        private static readonly int Part = 3;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.DropDownButton.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.DropDownButton.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.DropDownButton.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.DatePicker.ClassName, VistaAeroTheme.DatePicker.DropDownButton.Part, 4);
      }
    }

    public static class TextBox
    {
      private static readonly string ClassName = "Edit";

      public static class Border
      {
        private static readonly int Part = 6;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.TextBox.ClassName, VistaAeroTheme.TextBox.Border.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.TextBox.ClassName, VistaAeroTheme.TextBox.Border.Part, 2);
        public static readonly VisualStyleElement Focused = VisualStyleElement.CreateElement(VistaAeroTheme.TextBox.ClassName, VistaAeroTheme.TextBox.Border.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.TextBox.ClassName, VistaAeroTheme.TextBox.Border.Part, 4);
      }
    }

    public static class Header
    {
      private static readonly string ClassName = nameof (Header);

      public static class Item
      {
        private static readonly int Part = 1;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 3);
        public static readonly VisualStyleElement SortedNormal = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 4);
        public static readonly VisualStyleElement SortedHot = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 5);
        public static readonly VisualStyleElement SortedPressed = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.Item.Part, 6);
      }

      public static class DropDown
      {
        private static readonly int Part = 5;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.DropDown.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.DropDown.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.DropDown.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.Header.ClassName, VistaAeroTheme.Header.DropDown.Part, 4);
      }
    }

    public static class ListBox
    {
      private static readonly string ClassName = "Listbox";

      public static class Border
      {
        private static readonly int Part = 1;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Border.Part, 1);
        public static readonly VisualStyleElement Focused = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Border.Part, 2);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Border.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Border.Part, 4);
      }

      public static class Item
      {
        private static readonly int Part = 5;
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Item.Part, 1);
        public static readonly VisualStyleElement HotSelected = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Item.Part, 2);
        public static readonly VisualStyleElement Selected = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Item.Part, 3);
        public static readonly VisualStyleElement SelectedNoFocus = VisualStyleElement.CreateElement(VistaAeroTheme.ListBox.ClassName, VistaAeroTheme.ListBox.Item.Part, 4);
      }
    }

    public static class ListView
    {
      private static readonly string ClassName = "Listview";

      public static class Item
      {
        private static readonly int Part = 1;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 2);
        public static readonly VisualStyleElement Selected = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 4);
        public static readonly VisualStyleElement SelectedNoFocus = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 5);
        public static readonly VisualStyleElement HotSelected = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.Item.Part, 6);
      }

      public static class GroupHeaderLine
      {
        private static readonly int Part = 7;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupHeaderLine.Part, 1);
      }

      public static class GroupExpandButton
      {
        private static readonly int Part = 8;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupExpandButton.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupExpandButton.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupExpandButton.Part, 3);
      }

      public static class GroupCollapseButton
      {
        private static readonly int Part = 9;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupCollapseButton.Part, 1);
        public static readonly VisualStyleElement Hot = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupCollapseButton.Part, 2);
        public static readonly VisualStyleElement Pressed = VisualStyleElement.CreateElement(VistaAeroTheme.ListView.ClassName, VistaAeroTheme.ListView.GroupCollapseButton.Part, 3);
      }
    }

    public static class FlyOut
    {
      private static readonly string ClassName = "Flyout";

      public static class Header
      {
        private static readonly int Part = 1;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Header.Part, 1);
        public static readonly VisualStyleElement Hover = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Header.Part, 2);
      }

      public static class Body
      {
        private static readonly int Part = 2;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Body.Part, 1);
        public static readonly VisualStyleElement Emphasized = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Body.Part, 2);
      }

      public static class Label
      {
        private static readonly int Part = 3;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Label.Part, 1);
        public static readonly VisualStyleElement Selected = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Label.Part, 2);
        public static readonly VisualStyleElement Emphasized = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Label.Part, 3);
        public static readonly VisualStyleElement Disabled = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Label.Part, 4);
      }

      public static class Link
      {
        private static readonly int Part = 4;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Link.Part, 1);
        public static readonly VisualStyleElement Hover = VisualStyleElement.CreateElement(VistaAeroTheme.FlyOut.ClassName, VistaAeroTheme.FlyOut.Link.Part, 2);
      }
    }

    public static class Menu
    {
      private static readonly string ClassName = nameof (Menu);

      public static class BarBackground
      {
        private static readonly int Part = 7;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.Menu.ClassName, VistaAeroTheme.Menu.BarBackground.Part, 1);
        public static readonly VisualStyleElement Inactive = VisualStyleElement.CreateElement(VistaAeroTheme.Menu.ClassName, VistaAeroTheme.Menu.BarBackground.Part, 1);
      }

      public static class PopupBackground
      {
        private static readonly int Part = 9;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.Menu.ClassName, VistaAeroTheme.Menu.PopupBackground.Part, 1);
      }

      public static class PopupBorder
      {
        private static readonly int Part = 10;
        public static readonly VisualStyleElement Normal = VisualStyleElement.CreateElement(VistaAeroTheme.Menu.ClassName, VistaAeroTheme.Menu.PopupBorder.Part, 1);
      }
    }
  }
}
