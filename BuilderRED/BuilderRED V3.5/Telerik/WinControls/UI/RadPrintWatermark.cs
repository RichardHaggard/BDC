// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPrintWatermark
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPrintWatermark : ICloneable
  {
    private string text = string.Empty;
    private string pages = string.Empty;
    private Font font;
    private Color foreColor;
    private float textAngle;
    private byte textOpacity;
    private int textHOffset;
    private int textVOffset;
    private string imagePath;
    private byte imageOpacity;
    private int imageHOffset;
    private int imageVOffset;
    private bool imageTiling;
    private bool drawInFront;
    private bool allPages;
    private List<int> pageNumbers;
    private bool shouldInvalidatePages;

    public RadPrintWatermark()
    {
      this.foreColor = Color.Black;
      this.textOpacity = (byte) 127;
      this.imageOpacity = (byte) 127;
      this.allPages = true;
      this.drawInFront = true;
      this.font = new Font(Control.DefaultFont.FontFamily, 144f);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<int> PageNumbers
    {
      get
      {
        if (this.pageNumbers == null || this.shouldInvalidatePages)
        {
          this.pageNumbers = this.GetListWithPageNumbers();
          this.shouldInvalidatePages = false;
        }
        return this.pageNumbers;
      }
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool AllPages
    {
      get
      {
        return this.allPages;
      }
      set
      {
        if (this.allPages == value)
          return;
        this.allPages = value;
        this.shouldInvalidatePages = true;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue("")]
    public string Pages
    {
      get
      {
        return this.pages;
      }
      set
      {
        if (!(this.pages != value))
          return;
        this.pages = value;
        this.shouldInvalidatePages = true;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
      }
    }

    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    [DefaultValue(typeof (Font), "Microsoft Sans Serif, 144pt")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool DrawInFront
    {
      get
      {
        return this.drawInFront;
      }
      set
      {
        this.drawInFront = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool DrawText
    {
      get
      {
        return !string.IsNullOrEmpty(this.Text);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0.0f)]
    public float TextAngle
    {
      get
      {
        return this.textAngle;
      }
      set
      {
        this.textAngle = value;
      }
    }

    [DefaultValue(127)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public byte TextOpacity
    {
      get
      {
        return this.textOpacity;
      }
      set
      {
        this.textOpacity = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0)]
    public int TextHOffset
    {
      get
      {
        return this.textHOffset;
      }
      set
      {
        this.textHOffset = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0)]
    [Browsable(true)]
    public int TextVOffset
    {
      get
      {
        return this.textVOffset;
      }
      set
      {
        this.textVOffset = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool DrawImage
    {
      get
      {
        return !string.IsNullOrEmpty(this.imagePath);
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string ImagePath
    {
      get
      {
        return this.imagePath;
      }
      set
      {
        this.imagePath = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(127)]
    public byte ImageOpacity
    {
      get
      {
        return this.imageOpacity;
      }
      set
      {
        this.imageOpacity = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0)]
    [Browsable(true)]
    public int ImageHOffset
    {
      get
      {
        return this.imageHOffset;
      }
      set
      {
        this.imageHOffset = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0)]
    [Browsable(true)]
    public int ImageVOffset
    {
      get
      {
        return this.imageVOffset;
      }
      set
      {
        this.imageVOffset = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ImageTiling
    {
      get
      {
        return this.imageTiling;
      }
      set
      {
        this.imageTiling = value;
      }
    }

    private List<int> GetListWithPageNumbers()
    {
      string[] strArray = this.pages.Replace(" ", string.Empty).Split(',', ';');
      List<int> intList = new List<int>();
      if (string.IsNullOrEmpty(this.pages))
        return intList;
      foreach (string s1 in strArray)
      {
        if (s1.Contains("-"))
        {
          int length = s1.IndexOf('-');
          string s2 = s1.Substring(0, length);
          string s3 = s1.Substring(length + 1, s1.Length - 1 - length);
          int result1;
          int result2;
          if (!int.TryParse(s2, out result1) || !int.TryParse(s3, out result2))
            throw new ArgumentException("The provided pages string is not valid.");
          if (result1 > result2)
          {
            int num = result1 + result2;
            result2 = num - result2;
            result1 = num - result2;
          }
          for (int index = result1; index <= result2; ++index)
            intList.Add(index);
        }
        else
        {
          int result;
          if (!int.TryParse(s1, out result))
            throw new ArgumentException("The provided pages string is not valid.");
          intList.Add(result);
        }
      }
      intList.Sort();
      int index1 = 0;
      while (index1 < intList.Count - 1)
      {
        if (intList[index1] == intList[index1 + 1])
          intList.RemoveAt(index1 + 1);
        else
          ++index1;
      }
      return intList;
    }

    public bool ShouldPrintOnPage(int pageNumber)
    {
      if (this.allPages)
        return true;
      return this.PageNumbers.Contains(pageNumber);
    }

    public object Clone()
    {
      return (object) new RadPrintWatermark() { Text = this.Text, Font = this.Font, ForeColor = this.ForeColor, TextAngle = this.TextAngle, TextOpacity = this.TextOpacity, TextHOffset = this.TextHOffset, TextVOffset = this.TextVOffset, ImagePath = this.ImagePath, ImageOpacity = this.ImageOpacity, ImageHOffset = this.ImageHOffset, ImageVOffset = this.ImageVOffset, ImageTiling = this.ImageTiling, DrawInFront = this.DrawInFront, AllPages = this.AllPages, Pages = this.Pages };
    }
  }
}
