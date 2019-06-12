// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadNewItemAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RadNewItemAttribute : Attribute
  {
    private readonly bool paintGlyph = true;
    private readonly bool addMenuVerb = true;
    private readonly string newItemName;
    private readonly bool allowEditItemText;

    public RadNewItemAttribute(string newItemName, bool allowEditItemText)
    {
      this.newItemName = newItemName;
      this.allowEditItemText = allowEditItemText;
    }

    public RadNewItemAttribute(string newItemName, bool allowEditItemText, bool paintGlyph)
    {
      this.newItemName = newItemName;
      this.allowEditItemText = allowEditItemText;
      this.paintGlyph = paintGlyph;
    }

    public RadNewItemAttribute(
      string newItemName,
      bool allowEditItemText,
      bool paintGlyph,
      bool addMenuVerb)
    {
      this.newItemName = newItemName;
      this.allowEditItemText = allowEditItemText;
      this.paintGlyph = paintGlyph;
      this.addMenuVerb = addMenuVerb;
    }

    public string NewItemName
    {
      get
      {
        return this.newItemName;
      }
    }

    public bool AllowEditItemText
    {
      get
      {
        return this.allowEditItemText;
      }
    }

    public bool PaintGlyph
    {
      get
      {
        return this.paintGlyph;
      }
    }

    public bool AddMenuVerb
    {
      get
      {
        return this.addMenuVerb;
      }
    }
  }
}
