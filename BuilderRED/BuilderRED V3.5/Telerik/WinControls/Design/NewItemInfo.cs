// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.NewItemInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Design
{
  public class NewItemInfo
  {
    public bool PaintGlyph = true;
    public bool AddMenuVerb = true;
    public bool AllowEdit;
    public string EditText;
    public RadItemCollection TargetCollection;

    public NewItemInfo(RadNewItemAttribute attribute, RadItemCollection targetCollection)
    {
      this.AllowEdit = attribute.AllowEditItemText;
      this.EditText = attribute.NewItemName;
      this.TargetCollection = targetCollection;
      this.PaintGlyph = attribute.PaintGlyph;
      this.AddMenuVerb = attribute.AddMenuVerb;
    }
  }
}
