// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CSSParser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.WinControls
{
  internal class CSSParser
  {
    public List<CSSGroup> groups = new List<CSSGroup>();
    private Random r = new Random();
    private const string specialOperators = ",;:{}()";
    private string text;
    private int position;
    private int length;
    private string current;
    private CSSParser.tokenTypes currentType;

    public void ReadText(string text)
    {
      this.text = text;
      this.Parse();
    }

    public void Read(string file)
    {
      this.text = new StreamReader(file).ReadToEnd();
      this.Parse();
    }

    public void Read(Stream stream)
    {
      this.text = new StreamReader(stream).ReadToEnd();
      this.Parse();
    }

    private void Reset()
    {
      this.current = string.Empty;
      this.position = 0;
      this.length = this.text.Length;
      this.currentType = CSSParser.tokenTypes.Unknown;
      this.groups.Clear();
    }

    private void Parse()
    {
      this.Reset();
      while (this.MoveNext())
      {
        bool flag = false;
        if (this.currentType == CSSParser.tokenTypes.SpecialOperator && this.current == "#")
        {
          flag = true;
          this.MoveNext();
        }
        if (this.currentType == CSSParser.tokenTypes.Identifier || this.currentType == CSSParser.tokenTypes.String)
        {
          CSSGroup cssGroup = new CSSGroup();
          cssGroup.isRoot = flag;
          cssGroup.name = this.current;
          this.groups.Add(cssGroup);
          this.MoveNext();
          if (this.currentType != CSSParser.tokenTypes.SpecialOperator)
          {
            cssGroup.childName = this.current;
            this.MoveNext();
          }
          if (this.currentType == CSSParser.tokenTypes.SpecialOperator)
          {
            if (this.current == ":")
            {
              do
              {
                this.MoveNext();
                cssGroup.BasedOn.Add(this.current);
                this.MoveNext();
              }
              while (this.current == ",");
            }
            if (this.current == "{")
            {
              this.MoveNext();
              while (this.current != "}")
              {
                CSSItem cssItem1 = new CSSItem();
                cssItem1.name = this.current;
                cssGroup.items.Add(cssItem1);
                this.MoveNext();
                if (this.current == "{")
                {
                  this.MoveNext();
                  while (this.current != "}")
                  {
                    CSSItem cssItem2 = new CSSItem();
                    cssItem2.name = this.current;
                    this.MoveNext();
                    cssItem2.value = this.ReadValue();
                    cssItem1.childItems.Add(cssItem2);
label_14:
                    if (this.MoveNext() && this.current == ";")
                      goto label_14;
                  }
                }
                else
                {
                  cssItem1.value = this.ReadValue();
label_18:
                  if (this.MoveNext() && this.current == ";")
                    goto label_18;
                }
              }
            }
          }
        }
      }
    }

    private string ReadValue()
    {
      this.MoveNext();
      return this.current;
    }

    private bool MoveNext()
    {
      if (this.position >= this.length)
        return false;
      while (this.position < this.length && char.IsWhiteSpace(this.text[this.position]))
        ++this.position;
      int position = this.position;
      if (this.position < this.length && ",;:{}()".IndexOf(this.text[this.position]) >= 0)
      {
        this.current = this.text.Substring(this.position, 1);
        this.currentType = CSSParser.tokenTypes.SpecialOperator;
        ++this.position;
        return true;
      }
      if (this.position < this.length - 2)
      {
        string str = this.text.Substring(this.position, 2);
        if (str == "//")
        {
          while (this.position < this.length && this.text[this.position] != '\n')
            ++this.position;
          return this.MoveNext();
        }
        if (str == "/*")
        {
          for (; this.position < this.length; ++this.position)
          {
            if (this.position < this.length - 2 && this.text[this.position] == '*' && this.text[this.position + 1] == '/')
            {
              this.position += 2;
              break;
            }
          }
          return this.MoveNext();
        }
      }
      if (this.position < this.length && this.text[this.position] == '"')
      {
        ++this.position;
        while (this.position < this.length && this.text[this.position] != '"')
          ++this.position;
        this.current = this.text.Substring(position + 1, this.position - position - 1);
        this.currentType = CSSParser.tokenTypes.String;
        ++this.position;
        return true;
      }
      while (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]) && ",;:{}()".IndexOf(this.text[this.position]) < 0)
        ++this.position;
      if (this.position - position <= 0)
        return false;
      this.current = this.text.Substring(position, this.position - position);
      this.currentType = CSSParser.tokenTypes.Identifier;
      return true;
    }

    public CSSGroup this[string name]
    {
      get
      {
        foreach (CSSGroup group in this.groups)
        {
          if (group.name == name)
            return group;
        }
        return (CSSGroup) null;
      }
    }

    private enum tokenTypes
    {
      Unknown,
      Identifier,
      String,
      SpecialOperator,
    }
  }
}
