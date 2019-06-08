// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Lexer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  internal class Lexer
  {
    private const char Escape = '\\';
    private const char DecimalSeparator = '.';
    private const char ExponentL = 'e';
    private const char ExponentU = 'E';
    private ILexerClient client;
    private int startPos;
    private Token token;
    private Operator op;
    private Lexer.StringReader reader;

    public string TokenString
    {
      get
      {
        return this.reader.GetString(this.startPos, this.reader.Position - this.startPos);
      }
    }

    public Operator Operator
    {
      get
      {
        return this.op;
      }
    }

    public int StartPos
    {
      get
      {
        return this.startPos;
      }
    }

    public Lexer(ILexerClient client, string text)
    {
      this.reader = new Lexer.StringReader(text);
      this.client = client;
    }

    public void StartRead()
    {
      this.reader.Restart();
      this.startPos = 0;
    }

    public Token Read(Token token)
    {
      Token token1 = this.Read();
      this.CheckToken(token);
      return token1;
    }

    public Token Read()
    {
      this.op = (Operator) null;
      this.token = Token.None;
      bool flag;
      do
      {
        flag = true;
        this.startPos = this.reader.Position;
        char c = this.reader.Read();
        switch (c)
        {
          case char.MinValue:
            this.token = Token.EOF;
            break;
          case '\t':
          case '\n':
          case '\r':
          case ' ':
            this.ReadWhiteSpaces();
            flag = false;
            break;
          case '"':
            this.ReadString('"');
            this.CheckToken(Token.StringConst);
            break;
          case '#':
            this.ReadDate();
            this.CheckToken(Token.Date);
            break;
          case '%':
            this.op = Operator.Modulo;
            this.token = Token.BinaryOp;
            break;
          case '&':
            this.op = Operator.BitwiseAnd;
            this.token = Token.BinaryOp;
            break;
          case '\'':
            this.ReadString('\'');
            this.CheckToken(Token.StringConst);
            break;
          case '(':
            this.token = Token.LeftParen;
            break;
          case ')':
            this.token = Token.RightParen;
            break;
          case '*':
            this.op = Operator.Multiply;
            this.token = Token.BinaryOp;
            break;
          case '+':
            this.op = Operator.Plus;
            this.token = Token.BinaryOp;
            break;
          case ',':
            this.token = Token.ListSeparator;
            break;
          case '-':
            this.op = Operator.Minus;
            this.token = Token.BinaryOp;
            break;
          case '.':
            if (this.client.PrevOperand == OperandType.None)
            {
              this.ReadNumber();
              break;
            }
            this.token = Token.Dot;
            break;
          case '/':
            this.op = Operator.Divide;
            this.token = Token.BinaryOp;
            break;
          case '<':
            this.token = Token.BinaryOp;
            this.ReadWhiteSpaces();
            if ('=' == this.reader.Current)
            {
              this.op = Operator.LessOrEqual;
              int num = (int) this.reader.Read();
              break;
            }
            if ('>' == this.reader.Current)
            {
              this.op = Operator.NotEqual;
              int num = (int) this.reader.Read();
              break;
            }
            this.op = Operator.LessThen;
            break;
          case '=':
            this.op = Operator.EqualTo;
            this.token = Token.BinaryOp;
            break;
          case '>':
            this.token = Token.BinaryOp;
            this.ReadWhiteSpaces();
            if ('=' == this.reader.Current)
            {
              this.op = Operator.GreaterOrEqual;
              int num = (int) this.reader.Read();
              break;
            }
            this.op = Operator.GreaterThen;
            break;
          case '@':
            this.token = Token.Parameter;
            break;
          case '[':
            this.ReadName(']', '\\', "]\\");
            this.CheckToken(Token.Name);
            break;
          case '^':
            this.op = Operator.BitwiseXor;
            this.token = Token.BinaryOp;
            break;
          case '`':
            this.ReadName('`', '`', "`");
            this.CheckToken(Token.Name);
            break;
          case '|':
            this.op = Operator.BitwiseOr;
            this.token = Token.BinaryOp;
            break;
          case '~':
            this.op = Operator.BitwiseNot;
            this.token = Token.BinaryOp;
            break;
          default:
            if ('0' == c && ('x' == this.reader.Current || 'X' == this.reader.Current))
            {
              int num = (int) this.reader.Read();
              this.ReadHex();
              this.token = Token.NumericHex;
              break;
            }
            if (char.IsDigit(c))
            {
              this.ReadNumber();
              break;
            }
            this.ReadReserved();
            if (this.token == Token.None)
            {
              if (char.IsLetter(c) || '_' == c)
              {
                this.ReadName();
                if (this.token != Token.None)
                {
                  this.CheckToken(Token.Name);
                  break;
                }
              }
              this.token = Token.Unknown;
              throw LexicalException.UnknownToken(this.TokenString, this.startPos + 1);
            }
            break;
        }
      }
      while (!flag);
      return this.token;
    }

    private void ReadReserved()
    {
      if (!char.IsLetter(this.reader.Current))
        return;
      this.ReadName();
      Lexer.ReservedWord reservedWord = Lexer.ReservedWords.Lookup(this.TokenString);
      if (reservedWord == null)
        return;
      this.token = reservedWord.Token;
      this.op = reservedWord.Operator;
    }

    private void ReadHex()
    {
      while (char.IsLetterOrDigit(this.reader.Current))
      {
        int num = (int) this.reader.Read();
      }
      string text = this.reader.GetString(this.startPos + 2, this.reader.Position);
      foreach (char ch in text)
      {
        if (!Utils.IsHexDigit(ch))
          throw LexicalException.InvalidHex(text);
      }
    }

    private void ReadNumber()
    {
      bool flag1 = false;
      bool flag2 = false;
      this.ReadDigits();
      if ('.' == this.reader.Current)
      {
        flag1 = true;
        int num = (int) this.reader.Read();
      }
      this.ReadDigits();
      if ('e' == this.reader.Current || 'E' == this.reader.Current)
      {
        flag2 = true;
        int num1 = (int) this.reader.Read();
        if ('-' == this.reader.Current || '+' == this.reader.Current)
        {
          int num2 = (int) this.reader.Read();
        }
        this.ReadDigits();
      }
      if (flag2)
        this.token = Token.Float;
      else if (flag1)
        this.token = Token.Decimal;
      else
        this.token = Token.Numeric;
    }

    private void ReadDigits()
    {
      while (char.IsDigit(this.reader.Current))
      {
        int num = (int) this.reader.Read();
      }
    }

    private void ReadName()
    {
      while (char.IsLetterOrDigit(this.reader.Current) || '_' == this.reader.Current)
      {
        int num = (int) this.reader.Read();
      }
      this.token = Token.Name;
    }

    private void ReadName(char endChar, char escape, string charsToEscape)
    {
      do
      {
        if ((int) escape == (int) this.reader.Current && this.reader.CanRead && charsToEscape.IndexOf(this.reader.Peek()) != -1)
        {
          int num1 = (int) this.reader.Read();
        }
        int num2 = (int) this.reader.Read();
      }
      while (!this.reader.End && (int) this.reader.Current != (int) endChar);
      if (this.reader.End)
        throw LexicalException.InvalidName(this.TokenString);
      this.token = Token.Name;
      int num = (int) this.reader.Read();
    }

    private void ReadString(char escape)
    {
      while (!this.reader.End)
      {
        if ((int) this.reader.Read() == (int) escape)
        {
          if (!this.reader.End && (int) escape == (int) this.reader.Current)
          {
            int num = (int) this.reader.Read();
          }
          else
            break;
        }
      }
      if (this.reader.End)
        throw LexicalException.InvalidString(this.TokenString);
      this.token = Token.StringConst;
    }

    private void ReadDate()
    {
      do
      {
        int num1 = (int) this.reader.Read();
      }
      while (!this.reader.End && '#' != this.reader.Current);
      if (this.reader.End || '#' != this.reader.Current)
        throw LexicalException.InvalidDate(this.TokenString);
      this.token = Token.Date;
      int num2 = (int) this.reader.Read();
    }

    private void ReadWhiteSpaces()
    {
      while (!this.reader.End && char.IsWhiteSpace(this.reader.Current))
      {
        int num = (int) this.reader.Read();
      }
    }

    private void CheckToken(Token token)
    {
      if (token != this.token)
        throw LexicalException.UnexpectedToken(Utils.TokenToString(this.token), Utils.TokenToString(token), this.reader.Position);
    }

    private class StringReader
    {
      private int position;
      private char[] buffer;

      public char Current
      {
        get
        {
          return this.buffer[this.position];
        }
      }

      public int Position
      {
        get
        {
          return this.position;
        }
      }

      public int Length
      {
        get
        {
          return this.buffer.Length;
        }
      }

      public bool CanRead
      {
        get
        {
          return this.position + 1 < this.Length;
        }
      }

      public bool End
      {
        get
        {
          return this.position >= this.Length;
        }
      }

      public StringReader(string text)
      {
        this.LoadString(text);
      }

      public char Read()
      {
        return this.buffer[this.position++];
      }

      public char Peek()
      {
        return this.buffer[this.position + 1];
      }

      public void Restart()
      {
        this.position = 0;
      }

      public string GetString(int start, int length)
      {
        int num = this.buffer.Length - 1;
        if (start + length > num)
          length = num - start;
        return new string(this.buffer, start, length);
      }

      private void LoadString(string text)
      {
        int count = 0;
        if (string.IsNullOrEmpty(text))
        {
          this.buffer = new char[1];
        }
        else
        {
          count = text.Length;
          this.buffer = new char[count + 1];
          text.CopyTo(0, this.buffer, 0, count);
        }
        this.buffer[count] = char.MinValue;
      }
    }

    private class ReservedWord
    {
      public readonly string Word;
      public readonly Token Token;
      public readonly Operator Operator;

      public ReservedWord(string word, Token token, Operator op)
      {
        this.Word = word;
        this.Token = token;
        this.Operator = op;
      }
    }

    private static class ReservedWords
    {
      private static readonly List<Lexer.ReservedWord> list = new List<Lexer.ReservedWord>((IEnumerable<Lexer.ReservedWord>) new Lexer.ReservedWord[11]
      {
        new Lexer.ReservedWord("And", Token.BinaryOp, Operator.And),
        new Lexer.ReservedWord("Or", Token.BinaryOp, Operator.Or),
        new Lexer.ReservedWord("Between", Token.TernaryOp, Operator.Between),
        new Lexer.ReservedWord("In", Token.BinaryOp, Operator.In),
        new Lexer.ReservedWord("Is", Token.BinaryOp, Operator.Is),
        new Lexer.ReservedWord("Like", Token.BinaryOp, Operator.Like),
        new Lexer.ReservedWord("Not", Token.UnaryOp, Operator.Not),
        new Lexer.ReservedWord("True", Token.ZeroOp, Operator.True),
        new Lexer.ReservedWord("False", Token.ZeroOp, Operator.False),
        new Lexer.ReservedWord("Mod", Token.BinaryOp, Operator.Modulo),
        new Lexer.ReservedWord("Null", Token.ZeroOp, Operator.Null)
      });

      static ReservedWords()
      {
        Lexer.ReservedWords.list.Sort(new Comparison<Lexer.ReservedWord>(Lexer.ReservedWords.Compare));
      }

      private static int Compare(Lexer.ReservedWord reserved1, Lexer.ReservedWord reserved2)
      {
        return string.Compare(reserved1.Word, reserved2.Word, false, CultureInfo.InvariantCulture);
      }

      public static Lexer.ReservedWord Lookup(string text)
      {
        int num1 = 0;
        int num2 = Lexer.ReservedWords.list.Count - 1;
        do
        {
          int index = (num1 + num2) / 2;
          int num3 = string.Compare(Lexer.ReservedWords.list[index].Word, text, true, CultureInfo.InvariantCulture);
          if (num3 == 0)
            return Lexer.ReservedWords.list[index];
          if (num3 < 0)
            num1 = index + 1;
          else
            num2 = index - 1;
        }
        while (num1 <= num2);
        return (Lexer.ReservedWord) null;
      }
    }
  }
}
