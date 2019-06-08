// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupHeaderList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class GridGroupHeaderList : LightVisualElement
  {
    public static RadProperty GroupingLinesColorProperty = RadProperty.Register(nameof (GroupingLinesColor), typeof (Color), typeof (GridGroupHeaderList), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDarkDark), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemsDistanceProperty = RadProperty.Register("ItemsDistance", typeof (Size), typeof (GridGroupHeaderList), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(15, 15), ElementPropertyOptions.AffectsMeasure));
    private GridGroupPanel groupPanel;
    private GridGroupHeaderListsCollection childList;

    public GridGroupHeaderList(GridGroupPanel groupPanel)
    {
      this.groupPanel = groupPanel;
      this.childList = new GridGroupHeaderListsCollection((RadElement) this);
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color GroupingLinesColor
    {
      get
      {
        return (Color) this.GetValue(GridGroupHeaderList.GroupingLinesColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridGroupHeaderList.GroupingLinesColorProperty, (object) value);
      }
    }

    public GridGroupHeaderListsCollection ChildLists
    {
      get
      {
        return this.childList;
      }
    }

    public bool Update(GridViewTemplate template)
    {
      return true;
    }
  }
}
