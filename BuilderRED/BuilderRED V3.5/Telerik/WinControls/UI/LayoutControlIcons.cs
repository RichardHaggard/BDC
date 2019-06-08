// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlIcons
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Telerik.WinControls.UI
{
  public static class LayoutControlIcons
  {
    private static Dictionary<string, Image> images = new Dictionary<string, Image>();
    private static readonly string ResourcesPrefix = "Telerik.WinControls.UI.RadLayoutControl.Icons.";
    private static readonly string ResourcesSuffix = ".png";
    private static readonly string ControlItemName = nameof (ControlItem);
    private static readonly string CustomizeName = nameof (Customize);
    private static readonly string EmptySpaceItemName = nameof (EmptySpaceItem);
    private static readonly string GroupItemName = nameof (GroupItem);
    private static readonly string LabelItemName = nameof (LabelItem);
    private static readonly string LoadLayoutName = nameof (LoadLayout);
    private static readonly string SaveLayoutName = nameof (SaveLayout);
    private static readonly string SeparatorItemName = nameof (SeparatorItem);
    private static readonly string SplitterItemName = nameof (SplitterItem);
    private static readonly string TabbedGroupName = nameof (TabbedGroup);

    public static Image ControlItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.ControlItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.ControlItemName);
        return LayoutControlIcons.images[LayoutControlIcons.ControlItemName];
      }
    }

    public static Image Customize
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.CustomizeName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.CustomizeName);
        return LayoutControlIcons.images[LayoutControlIcons.CustomizeName];
      }
    }

    public static Image EmptySpaceItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.EmptySpaceItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.EmptySpaceItemName);
        return LayoutControlIcons.images[LayoutControlIcons.EmptySpaceItemName];
      }
    }

    public static Image GroupItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.GroupItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.GroupItemName);
        return LayoutControlIcons.images[LayoutControlIcons.GroupItemName];
      }
    }

    public static Image LabelItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.LabelItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.LabelItemName);
        return LayoutControlIcons.images[LayoutControlIcons.LabelItemName];
      }
    }

    public static Image LoadLayout
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.LoadLayoutName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.LoadLayoutName);
        return LayoutControlIcons.images[LayoutControlIcons.LoadLayoutName];
      }
    }

    public static Image SaveLayout
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.SaveLayoutName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.SaveLayoutName);
        return LayoutControlIcons.images[LayoutControlIcons.SaveLayoutName];
      }
    }

    public static Image SeparatorItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.SeparatorItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.SeparatorItemName);
        return LayoutControlIcons.images[LayoutControlIcons.SeparatorItemName];
      }
    }

    public static Image SplitterItem
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.SplitterItemName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.SplitterItemName);
        return LayoutControlIcons.images[LayoutControlIcons.SplitterItemName];
      }
    }

    public static Image TabbedGroup
    {
      get
      {
        if (!LayoutControlIcons.images.ContainsKey(LayoutControlIcons.TabbedGroupName))
          LayoutControlIcons.LoadImage(LayoutControlIcons.TabbedGroupName);
        return LayoutControlIcons.images[LayoutControlIcons.TabbedGroupName];
      }
    }

    private static void LoadImage(string name)
    {
      Stream manifestResourceStream = typeof (LayoutControlIcons).Assembly.GetManifestResourceStream(string.Format("{0}{1}{2}", (object) LayoutControlIcons.ResourcesPrefix, (object) name, (object) LayoutControlIcons.ResourcesSuffix));
      if (manifestResourceStream == null)
        return;
      LayoutControlIcons.images[name] = Image.FromStream(manifestResourceStream);
      manifestResourceStream.Dispose();
    }
  }
}
