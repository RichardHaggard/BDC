// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.XmlStyleRepository
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Styles
{
  public class XmlStyleRepository
  {
    private List<XmlRepositoryItem> xmlRepositoryItems = new List<XmlRepositoryItem>();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<XmlRepositoryItem> RepositoryItems
    {
      get
      {
        return this.xmlRepositoryItems;
      }
    }

    public void MergeWith(XmlStyleRepository styleRepository)
    {
      LinkedList<XmlRepositoryItem> linkedList = new LinkedList<XmlRepositoryItem>();
      for (int index1 = 0; index1 < styleRepository.xmlRepositoryItems.Count; ++index1)
      {
        XmlRepositoryItem xmlRepositoryItem = styleRepository.xmlRepositoryItems[index1];
        bool flag = false;
        for (int index2 = 0; index2 < this.xmlRepositoryItems.Count; ++index2)
        {
          if (this.xmlRepositoryItems[index2].Key == xmlRepositoryItem.Key)
          {
            this.xmlRepositoryItems[index2] = styleRepository.xmlRepositoryItems[index1];
            flag = true;
            break;
          }
        }
        if (!flag)
          linkedList.AddLast(xmlRepositoryItem);
      }
      this.xmlRepositoryItems.AddRange((IEnumerable<XmlRepositoryItem>) linkedList);
    }
  }
}
