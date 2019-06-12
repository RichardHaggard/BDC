// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IItemsControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public interface IItemsControl
  {
    event ItemSelectedEventHandler ItemSelected;

    event ItemSelectedEventHandler ItemDeselected;

    RadItem GetSelectedItem();

    void SelectItem(RadItem item);

    RadItem GetNextItem(RadItem item, bool forward);

    RadItem SelectNextItem(RadItem item, bool forward);

    RadItem GetFirstVisibleItem();

    RadItem GetLastVisibleItem();

    RadItem SelectFirstVisibleItem();

    RadItem SelectLastVisibleItem();

    RadItemOwnerCollection ActiveItems { get; }

    RadItemOwnerCollection Items { get; }

    bool RollOverItemSelection { get; set; }

    bool ProcessKeyboard { get; set; }

    bool CanNavigate(Keys keyData);

    bool CanProcessMnemonic(char keyData);
  }
}
