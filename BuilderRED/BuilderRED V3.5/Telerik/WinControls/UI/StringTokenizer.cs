// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StringTokenizer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class StringTokenizer
  {
    private LinkedList<string> tokens;
    private string sourceString;
    private string delimiter;
    private IEnumerator<string> enumerator;

    public StringTokenizer(string source, string delimiter)
    {
      this.tokens = new LinkedList<string>();
      this.enumerator = (IEnumerator<string>) this.tokens.GetEnumerator();
      this.sourceString = source;
      this.delimiter = delimiter;
      if (delimiter.Length == 0)
        this.delimiter = " ";
      this.Tokenize();
    }

    private void Tokenize()
    {
      string str1 = this.sourceString;
      string empty = string.Empty;
      this.tokens.Clear();
      if (string.IsNullOrEmpty(str1))
        return;
      if (str1.IndexOf(this.delimiter) < 0)
      {
        this.tokens.AddLast(str1);
        this.enumerator = (IEnumerator<string>) this.tokens.GetEnumerator();
      }
      else
      {
        int length;
        while ((length = str1.IndexOf(this.delimiter)) >= 0)
        {
          if (length == 0)
          {
            str1 = str1.Length <= this.delimiter.Length ? string.Empty : str1.Substring(this.delimiter.Length);
          }
          else
          {
            string str2 = str1.Substring(0, length);
            this.tokens.AddLast(str2);
            this.enumerator = (IEnumerator<string>) this.tokens.GetEnumerator();
            str1 = str1.Length <= this.delimiter.Length + str2.Length ? string.Empty : str1.Substring(this.delimiter.Length + str2.Length);
          }
        }
        if (str1.Length <= 0)
          return;
        this.tokens.AddLast(str1);
        this.enumerator = (IEnumerator<string>) this.tokens.GetEnumerator();
      }
    }

    public int Count()
    {
      return this.tokens.Count;
    }

    public string NextToken()
    {
      if (this.enumerator.MoveNext())
        return this.enumerator.Current;
      return (string) null;
    }
  }
}
