// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DateInput
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Telerik.WinControls.UI
{
  public class DateInput
  {
    private int defaultValue = 2029;
    private CultureInfo cultureInfo;
    private string format;
    private DateTime minDate;
    private DateTime maxDate;

    public DateInput(CultureInfo cultureInfo)
    {
      this.CultureInfo = cultureInfo;
      this.MinDate = new DateTime(1980, 1, 1);
      this.MaxDate = new DateTime(2099, 12, 31);
    }

    public DateInput()
      : this(Thread.CurrentThread.CurrentCulture)
    {
    }

    [Category("Behavior")]
    [Description("Date and time format used by RadDateInput.")]
    public virtual string DateFormat
    {
      get
      {
        if (this.format == null)
          this.format = "d";
        return DateInput.MapDateFormatShortCuts(this.format, this.CultureInfo.DateTimeFormat);
      }
      set
      {
        this.format = value;
      }
    }

    public CultureInfo CultureInfo
    {
      get
      {
        return this.cultureInfo;
      }
      set
      {
        this.cultureInfo = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [Description("Indicates the end of the century that is used to interpret the year value when a short year is entered in the input.")]
    [DefaultValue(2029)]
    public int ShortYearCenturyEnd
    {
      get
      {
        return this.defaultValue;
      }
      set
      {
        this.defaultValue = value;
      }
    }

    [NotifyParentProperty(true)]
    [Category("Behavior")]
    [Description("Indicates the start of the century that is used to interpret the year value when a short year is entered in the input.")]
    [DefaultValue(1930)]
    public int ShortYearCenturyStart
    {
      get
      {
        return this.ShortYearCenturyEnd - 99;
      }
    }

    public DateTime MinDate
    {
      get
      {
        return this.minDate;
      }
      set
      {
        this.minDate = value;
      }
    }

    public DateTime MaxDate
    {
      get
      {
        return this.maxDate;
      }
      set
      {
        this.maxDate = value;
      }
    }

    internal Hashtable DateSlots
    {
      get
      {
        string[] strArray = Regex.Split(DateInput.WhitespacePreservingEscape(this.DateFormat), "[^dMy]+");
        List<string> stringList = new List<string>();
        foreach (string str in strArray)
        {
          if (str.Length > 0)
            stringList.Add(str);
        }
        string[] array = stringList.ToArray();
        Hashtable hashtable = (Hashtable) null;
        if (array.Length == 3)
          hashtable = DateInput.GetDateSlots(array);
        if (hashtable == null)
          hashtable = DateInput.GetDateSlots(Regex.Split(DateInput.WhitespacePreservingEscape(this.CultureInfo.DateTimeFormat.ShortDatePattern), DateInput.WhitespacePreservingEscape(this.CultureInfo.DateTimeFormat.DateSeparator)));
        return hashtable;
      }
    }

    internal bool TimeInputOnly
    {
      get
      {
        string[] strArray = Regex.Split(Regex.Escape(DateInput.MapDateFormatShortCuts(this.DateFormat, this.CultureInfo.DateTimeFormat)), Regex.Escape(this.CultureInfo.DateTimeFormat.DateSeparator));
        bool flag = false;
        foreach (string str in strArray)
        {
          if (str.IndexOf("M") != -1)
          {
            flag = true;
            break;
          }
          if (str.IndexOf("d") != -1)
          {
            flag = true;
            break;
          }
          if (str.IndexOf("y") != -1)
          {
            flag = true;
            break;
          }
        }
        return !flag;
      }
    }

    internal bool MonthYearOnly
    {
      get
      {
        string[] strArray = Regex.Split(Regex.Escape(DateInput.MapDateFormatShortCuts(this.DateFormat, this.CultureInfo.DateTimeFormat)), Regex.Escape(this.CultureInfo.DateTimeFormat.DateSeparator));
        bool flag = false;
        foreach (string str in strArray)
        {
          if (str.IndexOf("d") != -1)
          {
            flag = true;
            break;
          }
        }
        return !flag;
      }
    }

    public DateTime? ParseDate(string value, DateTime? baseDate)
    {
      if (string.IsNullOrEmpty(value))
        return new DateTime?();
      try
      {
        return new DateTime?(new DateInput.DateTimeParser(this).Parse(new DateInput.DateTimeLexer(this).GetTokens(value)).Evaluate(this.GetParsingBaseDate(baseDate)));
      }
      catch (Exception ex)
      {
        return new DateTime?();
      }
    }

    private static Hashtable GetDateSlots(string[] dateParts)
    {
      Hashtable hashtable = new Hashtable();
      int num = 0;
      foreach (string datePart in dateParts)
      {
        if (datePart.IndexOf("M", StringComparison.InvariantCulture) != -1)
        {
          hashtable[(object) "Month"] = (object) num;
          ++num;
        }
        else if (datePart.IndexOf("d", StringComparison.InvariantCulture) != -1)
        {
          hashtable[(object) "Day"] = (object) num;
          ++num;
        }
        else if (datePart.IndexOf("y", StringComparison.InvariantCulture) != -1)
        {
          hashtable[(object) "Year"] = (object) num;
          ++num;
        }
      }
      if (hashtable.Keys.Count != 3)
        return (Hashtable) null;
      return hashtable;
    }

    private static string WhitespacePreservingEscape(string input)
    {
      input = input.Replace("\\", "\\\\");
      input = input.Replace("/", "\\/");
      input = input.Replace(".", "\\.");
      return input;
    }

    private DateTime GetParsingBaseDate(DateTime? currentDate)
    {
      DateTime? nullable1 = currentDate;
      if (!nullable1.HasValue)
        nullable1 = new DateTime?(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
      DateTime? nullable2 = nullable1;
      DateTime minDate = this.MinDate;
      if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
      {
        nullable1 = new DateTime?(this.MinDate);
      }
      else
      {
        DateTime? nullable3 = nullable1;
        DateTime maxDate = this.MaxDate;
        if ((nullable3.HasValue ? (nullable3.GetValueOrDefault() > maxDate ? 1 : 0) : 0) != 0)
          nullable1 = new DateTime?(this.MaxDate);
      }
      return nullable1.Value;
    }

    private static string EnsureEndWithSemiColon(string value)
    {
      if (value != null)
      {
        int length = value.Length;
        if (length > 0 && value[length - 1] != ';')
          return value + ";";
      }
      return value;
    }

    private static string MapDateFormatShortCuts(
      string format,
      DateTimeFormatInfo dateTimeFormatInfo)
    {
      if (format.Length > 1)
        return format;
      switch (format)
      {
        case "d":
          return dateTimeFormatInfo.ShortDatePattern;
        case "z":
          return dateTimeFormatInfo.YearMonthPattern;
        case "D":
          return dateTimeFormatInfo.LongDatePattern;
        case "f":
          return dateTimeFormatInfo.FullDateTimePattern;
        case "F":
          return dateTimeFormatInfo.FullDateTimePattern;
        case "g":
          return dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
        case "G":
          return dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.LongTimePattern;
        case "m":
          return dateTimeFormatInfo.MonthDayPattern;
        case "M":
          return dateTimeFormatInfo.MonthDayPattern;
        case "r":
          return dateTimeFormatInfo.RFC1123Pattern;
        case "R":
          return dateTimeFormatInfo.RFC1123Pattern;
        case "s":
          return dateTimeFormatInfo.SortableDateTimePattern;
        case "t":
          return dateTimeFormatInfo.ShortTimePattern;
        case "T":
          return dateTimeFormatInfo.LongTimePattern;
        case "y":
          return dateTimeFormatInfo.MonthDayPattern;
        case "Y":
          return dateTimeFormatInfo.MonthDayPattern;
        default:
          return format;
      }
    }

    public class DateTimeLexer
    {
      private string letterRegexStringPattern = "[A-Za-zªµºÀ-ÖØ-öø-ȟȢ-ȳɐ-ʭʰ-ʸʻ-ˁːˑˠ-ˤˮͺΆΈ-ΊΌΎ-ΡΣ-ώϐ-ϗϚ-ϳЀ-ҁҌ-ӄӇӈӋӌӐ-ӵӸӹԱ-Ֆՙա-ևא-תװ-ײء-غـ-يٱ-ۓەۥۦۺ-ۼܐܒ-ܬހ-ޥअ-हऽॐक़-ॡঅ-ঌএঐও-নপ-রলশ-হড়ঢ়য়-ৡৰৱਅ-ਊਏਐਓ-ਨਪ-ਰਲਲ਼ਵਸ਼ਸਹਖ਼-ੜਫ਼ੲ-ੴંઅ-ઋઍએ-ઑઓ-નપ-રલળવ-હઽ-ૂેો્ૐૠଅ-ଌଏଐଓ-ନପ-ରଲଳଶ-ହଽଡ଼ଢ଼ୟ-ୡஅ-ஊஎ-ஐஒ-கஙசஜஞடணதந-பம-வஷ-ஹఅ-ఌఎ-ఐఒ-నప-ళవ-హౠౡಅ-ಌಎ-ಐಒ-ನಪ-ಳವ-ಹೞೠೡഅ-ഌഎ-ഐഒ-നപ-ഹൠൡඅ-ඖක-නඳ-රලව-ෆก-ะาำเ-ๆກຂຄງຈຊຍດ-ທນ-ຟມ-ຣລວສຫອ-ະາຳຽເ-ໄໆໜໝༀཀ-ཇཉ-ཪྈ-ྋက-အဣ-ဧဩဪၐ-ၕႠ-Ⴥა-ჶᄀ-ᅙᅟ-ᆢᆨ-ᇹሀ-ሆለ-ቆቈቊ-ቍቐ-ቖቘቚ-ቝበ-ኆኈኊ-ኍነ-ኮኰኲ-ኵኸ-ኾዀዂ-ዅወ-ዎዐ-ዖዘ-ዮደ-ጎጐጒ-ጕጘ-ጞጠ-ፆፈ-ፚᎠ-Ᏼᐁ-ᙬᙯ-ᙶᚁ-ᚚᚠ-ᛪក-ឳᠠ-ᡷᢀ-ᢨḀ-ẛẠ-ỹἀ-ἕἘ-Ἕἠ-ὅὈ-Ὅὐ-ὗὙὛὝὟ-ώᾀ-ᾴᾶ-ᾼιῂ-ῄῆ-ῌῐ-ΐῖ-Ίῠ-Ῥῲ-ῴῶ-ῼⁿℂℇℊ-ℓℕℙ-ℝℤΩℨK-ℭℯ-ℱℳ-ℹ々〆〱-〵ぁ-ゔゝゞァ-ヺー-ヾㄅ-ㄬㄱ-ㆎㆠ-ㆷ㐀-䶵一-龥ꀀ-ꒌ가-힣豈-鶴ﬀ-ﬆﬓ-ﬗיִײַ-ﬨשׁ-זּטּ-לּמּנּסּףּפּצּ-ﮱﯓ-ﴽﵐ-ﶏﶒ-ﷇﷰ-ﷻﹰ-ﹲﹴﹶ-ﻼＡ-Ｚａ-ｚｦ-ﾾￂ-ￇￊ-ￏￒ-ￗￚ-ￜ][̀-͎͠-҃͢-҆҈҉֑-֣֡-ֹֻ-ֽֿׁׂًׄ-ٰٕۖ-۪ۤۧۨ-ܑۭܰ-݊ަ-ްँ-ः़ा-्॑-॔ॢॣঁ-ঃ়া-ৄেৈো-্ৗৢৣਂ਼ਾ-ੂੇੈੋ-੍ੰੱઁ-ઃ઼ા-ૅે-ૉો-્ଁ-ଃ଼ା-ୃେୈୋ-୍ୖୗஂஃா-ூெ-ைொ-்ௗఁ-ఃా-ౄె-ైొ-్ౕౖಂಃಾ-ೄೆ-ೈೊ-್ೕೖംഃാ-ൃെ-ൈൊ-്ൗංඃ්ා-ුූෘ-ෟෲෳัิ-ฺ็-๎ັິ-ູົຼ່-ໍ༹༘༙༵༷༾༿ཱ-྄྆྇ྐ-ྗྙ-ྼ࿆ာ-ဲံ-္ၖ-ၙ឴-៓ᢩ⃐-⃣〪-゙゚ﬞ〯︠-︣]?";
      private string digitRegexStringPattern = "[0-9]";
      private Regex letterMatcher;
      private Regex digitMatcher;
      private int current;
      private string characters;
      private DateInput parent;

      public DateTimeLexer(DateInput parent)
      {
        this.letterMatcher = new Regex(this.letterRegexStringPattern);
        this.digitMatcher = new Regex(this.digitRegexStringPattern);
        this.parent = parent;
      }

      internal List<DateInput.Token> GetTokens(string inputString)
      {
        this.current = 0;
        this.characters = inputString;
        List<string> values = new List<string>();
        while (this.current < inputString.Length)
        {
          string str1 = this.ReadCharacters(DateInput.InputType.Number);
          if (str1.Length > 0)
            values.Add(str1);
          string str2 = this.ReadCharacters(DateInput.InputType.Letter);
          if (str2.Length > 0)
            values.Add(str2);
          string str3 = this.ReadCharacters(DateInput.InputType.Separator);
          if (str3.Length > 0 && str3.ToLower() == this.parent.CultureInfo.DateTimeFormat.TimeSeparator.ToLower())
            values.Add(str3);
        }
        return this.CreateTokens(values);
      }

      internal List<DateInput.Token> CreateTokens(List<string> values)
      {
        List<DateInput.Token> tokenList = new List<DateInput.Token>();
        for (int index = 0; index < values.Count; ++index)
        {
          foreach (DateInput.TokenType type in Enum.GetValues(typeof (DateInput.TokenType)))
          {
            DateInput.Token token = this.CreateToken(type, values[index]);
            if (token != null)
            {
              tokenList.Add(token);
              break;
            }
          }
        }
        return tokenList;
      }

      private DateInput.Token CreateToken(DateInput.TokenType type, string input)
      {
        switch (type)
        {
          case DateInput.TokenType.NUMBER:
            int result = 0;
            if (int.TryParse(input, out result))
              return (DateInput.Token) new DateInput.NumberToken(input, this.parent);
            return (DateInput.Token) null;
          case DateInput.TokenType.MONTHNAME:
            if (string.IsNullOrEmpty(input))
              return (DateInput.Token) null;
            string lower1 = input.ToLower();
            int index1 = DateInput.Token.FindIndex(this.parent.CultureInfo.DateTimeFormat.MonthNames, lower1);
            if (index1 < 0)
              index1 = DateInput.Token.FindIndex(this.parent.CultureInfo.DateTimeFormat.AbbreviatedMonthNames, lower1);
            if (index1 >= 0)
              return (DateInput.Token) new DateInput.MonthNameToken(lower1, this.parent);
            return (DateInput.Token) null;
          case DateInput.TokenType.WEEKDAYNAME:
            if (string.IsNullOrEmpty(input))
              return (DateInput.Token) null;
            string lower2 = input.ToLower();
            int index2 = DateInput.Token.FindIndex(this.parent.CultureInfo.DateTimeFormat.DayNames, lower2);
            if (index2 < 0)
              index2 = DateInput.Token.FindIndex(this.parent.CultureInfo.DateTimeFormat.AbbreviatedDayNames, lower2);
            if (index2 >= 0)
              return (DateInput.Token) new DateInput.WeekDayNameToken(lower2, this.parent);
            return (DateInput.Token) null;
          case DateInput.TokenType.TIMESEPARATOR:
            if (input == this.parent.CultureInfo.DateTimeFormat.TimeSeparator)
              return (DateInput.Token) new DateInput.TimeSeparatorToken(input, this.parent);
            return (DateInput.Token) null;
          case DateInput.TokenType.AMPM:
            string lower3 = input.ToLower();
            bool flag = lower3 == this.parent.CultureInfo.DateTimeFormat.AMDesignator.ToLower();
            bool isPm = lower3 == this.parent.CultureInfo.DateTimeFormat.PMDesignator.ToLower();
            if (flag || isPm)
              return (DateInput.Token) new DateInput.AMPMToken(lower3, this.parent, isPm);
            return (DateInput.Token) null;
          default:
            return new DateInput.Token(DateInput.TokenType.AMPM, "stop", this.parent);
        }
      }

      private bool IsNumber(char character)
      {
        return this.digitMatcher.IsMatch(character.ToString());
      }

      private bool IsLetter(char character)
      {
        if (!this.IsAmPmWithDots(character))
          return this.letterMatcher.IsMatch(character.ToString());
        return true;
      }

      private bool IsAmPmWithDots(char character)
      {
        StringBuilder stringBuilder = new StringBuilder(this.characters.Length);
        if (this.IsIndexInRange(this.current - 1))
          stringBuilder.Append(this.characters[this.current - 1]);
        stringBuilder.Append(character);
        if (this.IsIndexInRange(this.current + 1))
          stringBuilder.Append(this.characters[this.current + 1]);
        if (this.IsIndexInRange(this.current + 2))
          stringBuilder.Append(this.characters[this.current + 2]);
        string input1 = stringBuilder.ToString();
        stringBuilder.Length = 0;
        if (this.IsIndexInRange(this.current - 3))
          stringBuilder.Append(this.characters[this.current - 3]);
        if (this.IsIndexInRange(this.current - 2))
          stringBuilder.Append(this.characters[this.current - 2]);
        if (this.IsIndexInRange(this.current - 1))
          stringBuilder.Append(this.characters[this.current - 1]);
        stringBuilder.Append(character);
        string input2 = stringBuilder.ToString();
        Regex regex = new Regex("a.m.|A.M.|p.m.|P.M.");
        return regex.IsMatch(input1) || regex.IsMatch(input2);
      }

      private bool IsIndexInRange(int index)
      {
        return index >= 0 && index <= this.characters.Length - 1;
      }

      private bool IsSeparator(char character)
      {
        if (!this.IsNumber(character))
          return !this.IsLetter(character);
        return false;
      }

      private string ReadCharacters(DateInput.InputType inputType)
      {
        List<string> stringList = new List<string>();
        while (this.current < this.characters.Length)
        {
          char character = this.characters[this.current];
          if (inputType == DateInput.InputType.Number && this.IsNumber(character))
          {
            stringList.Add(character.ToString());
            ++this.current;
          }
          else if (inputType == DateInput.InputType.Letter && this.IsLetter(character))
          {
            stringList.Add(character.ToString());
            ++this.current;
          }
          else if (inputType == DateInput.InputType.Separator && this.IsSeparator(character))
          {
            stringList.Add(character.ToString());
            ++this.current;
          }
          else
            break;
        }
        return string.Join("", stringList.ToArray());
      }
    }

    public enum InputType
    {
      Number,
      Letter,
      Separator,
    }

    public enum TokenType
    {
      NUMBER,
      MONTHNAME,
      WEEKDAYNAME,
      TIMESEPARATOR,
      AMPM,
    }

    public class Token
    {
      private DateInput.TokenType type;
      private string value;
      private DateInput parent;

      public DateInput Parent
      {
        get
        {
          return this.parent;
        }
        set
        {
          this.parent = value;
        }
      }

      public DateInput.TokenType Type
      {
        get
        {
          return this.type;
        }
        set
        {
          this.type = value;
        }
      }

      public string Value
      {
        get
        {
          return this.value;
        }
        set
        {
          this.value = value;
        }
      }

      public Token(DateInput.TokenType tokenType, string value, DateInput parent)
      {
        this.Type = tokenType;
        this.Value = value;
        this.Parent = parent;
      }

      public static int FindIndex(string[] array, string target)
      {
        if (target.Length < 2 && target != "i" && (target != "v" && target != "x"))
          return -1;
        for (int index = 0; index < array.Length; ++index)
        {
          if (array[index].ToLower().IndexOf(target) == 0)
            return index;
        }
        return -1;
      }

      public override string ToString()
      {
        return this.Value;
      }
    }

    public class NumberToken : DateInput.Token
    {
      public NumberToken(string value, DateInput parent)
        : base(DateInput.TokenType.NUMBER, value, parent)
      {
      }
    }

    public class MonthNameToken : DateInput.Token
    {
      public MonthNameToken(string value, DateInput parent)
        : base(DateInput.TokenType.MONTHNAME, value, parent)
      {
      }

      public int GetMonthIndex()
      {
        int index = DateInput.Token.FindIndex(this.Parent.CultureInfo.DateTimeFormat.MonthNames, this.Value);
        if (index >= 0)
          return index;
        return DateInput.Token.FindIndex(this.Parent.CultureInfo.DateTimeFormat.AbbreviatedMonthNames, this.Value);
      }
    }

    public class WeekDayNameToken : DateInput.Token
    {
      public WeekDayNameToken(string value, DateInput parent)
        : base(DateInput.TokenType.WEEKDAYNAME, value, parent)
      {
      }

      public int GetWeekDayIndex()
      {
        int index = DateInput.Token.FindIndex(this.Parent.CultureInfo.DateTimeFormat.DayNames, this.Value);
        if (index >= 0)
          return index;
        return DateInput.Token.FindIndex(this.Parent.CultureInfo.DateTimeFormat.AbbreviatedDayNames, this.Value);
      }
    }

    public class TimeSeparatorToken : DateInput.Token
    {
      public TimeSeparatorToken(string value, DateInput parent)
        : base(DateInput.TokenType.TIMESEPARATOR, value, parent)
      {
      }
    }

    public class AMPMToken : DateInput.Token
    {
      private bool isPm;

      public bool IsPm
      {
        get
        {
          return this.isPm;
        }
        set
        {
          this.isPm = value;
        }
      }

      public AMPMToken(string value, DateInput parent, bool isPm)
        : base(DateInput.TokenType.AMPM, value, parent)
      {
        this.IsPm = isPm;
      }
    }

    public class DateTimeParser
    {
      private DateInput parent;
      private bool timeInputOnly;
      private bool monthYearOnly;
      private List<DateInput.Token> tokens;
      private int currentTokenIndex;

      public DateTimeParser(DateInput parent)
      {
        this.parent = parent;
        this.timeInputOnly = parent.TimeInputOnly;
        this.monthYearOnly = parent.MonthYearOnly;
      }

      public DateInput.DateEntry Parse(List<DateInput.Token> inputTokens)
      {
        if (inputTokens.Count == 0)
          throw new Exception();
        this.tokens = inputTokens;
        this.currentTokenIndex = 0;
        DateInput.DateEntry date = this.ParseDate();
        DateInput.DateEntry time = this.ParseTime();
        if (date == null && time == null)
          throw new Exception();
        if (time == null)
          return date;
        return (DateInput.DateEntry) new DateInput.DateTimeEntry(this.parent) { Date = (date ?? (DateInput.DateEntry) new DateInput.EmptyDateEntry(this.parent)), Time = time };
      }

      private DateInput.DateEntry ParseDate()
      {
        if (this.timeInputOnly)
          return (DateInput.DateEntry) new DateInput.EmptyDateEntry(this.parent);
        return (((this.Triplet() ?? (DateInput.DateEntry) this.Pair()) ?? (DateInput.DateEntry) this.Month()) ?? (DateInput.DateEntry) this.Number()) ?? (DateInput.DateEntry) this.WeekDay();
      }

      private DateInput.DateEntry ParseTime()
      {
        return (DateInput.DateEntry) (((this.TimeTriplet() ?? this.TimePair()) ?? this.AMPMTimeNumber()) ?? this.TimeNumber());
      }

      private DateInput.TimeEntry TimeTriplet()
      {
        DateInput.TimeEntry timeEntry = (DateInput.TimeEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("TimeNumber", "TimePair", out first, out second))
        {
          List<DateInput.Token> tokens = new List<DateInput.Token>((IEnumerable<DateInput.Token>) (first as DateInput.TimeEntry).Tokens);
          tokens.AddRange((IEnumerable<DateInput.Token>) (second as DateInput.TimeEntry).Tokens);
          timeEntry = new DateInput.TimeEntry(tokens, this.parent);
        }
        return timeEntry;
      }

      private DateInput.TimeEntry TimePair()
      {
        DateInput.TimeEntry timeEntry = (DateInput.TimeEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("TimeNumber", "AMPMTimeNumber", out first, out second))
        {
          List<DateInput.Token> tokens = new List<DateInput.Token>((IEnumerable<DateInput.Token>) (first as DateInput.TimeEntry).Tokens);
          tokens.AddRange((IEnumerable<DateInput.Token>) (second as DateInput.TimeEntry).Tokens);
          timeEntry = new DateInput.TimeEntry(tokens, this.parent);
        }
        else if (this.MatchTwoRules("TimeNumber", "TimeNumber", out first, out second))
        {
          List<DateInput.Token> tokens = new List<DateInput.Token>((IEnumerable<DateInput.Token>) (first as DateInput.TimeEntry).Tokens);
          tokens.AddRange((IEnumerable<DateInput.Token>) (second as DateInput.TimeEntry).Tokens);
          timeEntry = new DateInput.TimeEntry(tokens, this.parent);
        }
        return timeEntry;
      }

      private DateInput.TimeEntry TimeNumber()
      {
        DateInput.TimeEntry timeEntry = (DateInput.TimeEntry) null;
        if (this.CurrentIs(DateInput.TokenType.AMPM))
          this.StepForward(1);
        if (this.CurrentIs(DateInput.TokenType.NUMBER) && !this.NextIs(DateInput.TokenType.AMPM) || this.CurrentIs(DateInput.TokenType.NUMBER) && this.FirstIs(DateInput.TokenType.AMPM))
        {
          timeEntry = new DateInput.TimeEntry(new List<DateInput.Token>()
          {
            this.CurrentToken()
          }, this.parent);
          if (this.NextIs(DateInput.TokenType.TIMESEPARATOR))
            this.StepForward(2);
          else
            this.StepForward(1);
        }
        return timeEntry;
      }

      private DateInput.TimeEntry AMPMTimeNumber()
      {
        DateInput.TimeEntry timeEntry = (DateInput.TimeEntry) null;
        if (this.CurrentIs(DateInput.TokenType.NUMBER) && this.FirstIs(DateInput.TokenType.AMPM))
        {
          timeEntry = new DateInput.TimeEntry(new List<DateInput.Token>()
          {
            this.CurrentToken(),
            this.FirstToken()
          }, this.parent);
          this.StepForward(2);
        }
        if (this.CurrentIs(DateInput.TokenType.NUMBER) && this.NextIs(DateInput.TokenType.AMPM))
        {
          timeEntry = new DateInput.TimeEntry(new List<DateInput.Token>()
          {
            this.CurrentToken(),
            this.NextToken()
          }, this.parent);
          this.StepForward(2);
        }
        return timeEntry;
      }

      private DateInput.DateEntry Triplet()
      {
        return ((DateInput.DateEntry) this.NoSeparatorTriplet() ?? (DateInput.DateEntry) this.PairAndNumber()) ?? (DateInput.DateEntry) this.NumberAndPair();
      }

      private DateInput.NoSeparatorDateEntry NoSeparatorTriplet()
      {
        DateInput.NoSeparatorDateEntry separatorDateEntry = (DateInput.NoSeparatorDateEntry) null;
        if (this.CurrentIs(DateInput.TokenType.NUMBER) && (this.tokens.Count == 1 || this.tokens.Count == 2) && (this.CurrentToken().Value.Length == 6 || this.CurrentToken().Value.Length == 8))
        {
          separatorDateEntry = new DateInput.NoSeparatorDateEntry(this.CurrentToken(), this.parent);
          this.StepForward(1);
        }
        return separatorDateEntry;
      }

      private DateInput.PairEntry Pair()
      {
        DateInput.PairEntry pairEntry = (DateInput.PairEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("Number", "Number", out first, out second))
          pairEntry = new DateInput.PairEntry((first as DateInput.SingleEntry).Token, (second as DateInput.SingleEntry).Token, this.parent);
        else if (this.MatchTwoRules("Number", "Month", out first, out second))
          pairEntry = new DateInput.PairEntry((first as DateInput.SingleEntry).Token, (second as DateInput.SingleEntry).Token, this.parent);
        else if (this.MatchTwoRules("Month", "Number", out first, out second))
          pairEntry = new DateInput.PairEntry((first as DateInput.SingleEntry).Token, (second as DateInput.SingleEntry).Token, this.parent);
        return pairEntry;
      }

      private DateInput.TripletEntry PairAndNumber()
      {
        DateInput.TripletEntry tripletEntry = (DateInput.TripletEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("Pair", "Number", out first, out second))
          tripletEntry = new DateInput.TripletEntry((first as DateInput.PairEntry).First, (first as DateInput.PairEntry).Second, (second as DateInput.SingleEntry).Token, this.parent);
        return tripletEntry;
      }

      private DateInput.TripletEntry NumberAndPair()
      {
        DateInput.TripletEntry tripletEntry = (DateInput.TripletEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("Number", "Pair", out first, out second))
          return new DateInput.TripletEntry((first as DateInput.SingleEntry).Token, (second as DateInput.PairEntry).First, (second as DateInput.PairEntry).Second, this.parent);
        return tripletEntry;
      }

      private DateInput.PairEntry WeekDayAndPair()
      {
        DateInput.PairEntry pairEntry = (DateInput.PairEntry) null;
        DateInput.DateEntry first;
        DateInput.DateEntry second;
        if (this.MatchTwoRules("WeekDay", "Pair", out first, out second))
          pairEntry = second as DateInput.PairEntry;
        return pairEntry;
      }

      private DateInput.SingleEntry Month()
      {
        if (this.CurrentIs(DateInput.TokenType.MONTHNAME))
        {
          DateInput.SingleEntry singleEntry = new DateInput.SingleEntry(this.CurrentToken(), this.parent);
          this.StepForward(1);
          return singleEntry;
        }
        if (!this.CurrentIs(DateInput.TokenType.WEEKDAYNAME))
          return (DateInput.SingleEntry) null;
        this.StepForward(1);
        DateInput.SingleEntry singleEntry1 = this.Month();
        if (singleEntry1 == null)
          this.StepBack(1);
        return singleEntry1;
      }

      private DateInput.SingleEntry WeekDay()
      {
        if (!this.CurrentIs(DateInput.TokenType.WEEKDAYNAME))
          return (DateInput.SingleEntry) null;
        DateInput.SingleEntry singleEntry = new DateInput.SingleEntry(this.CurrentToken(), this.parent);
        this.StepForward(1);
        return singleEntry;
      }

      private DateInput.SingleEntry Number()
      {
        if (this.NextIs(DateInput.TokenType.TIMESEPARATOR))
          return (DateInput.SingleEntry) null;
        if (this.CurrentIs(DateInput.TokenType.NUMBER))
        {
          if (this.CurrentToken().Value.Length > 4)
            throw new Exception();
          DateInput.SingleEntry singleEntry = new DateInput.SingleEntry(this.CurrentToken(), this.parent);
          this.StepForward(1);
          return singleEntry;
        }
        if (!this.CurrentIs(DateInput.TokenType.WEEKDAYNAME))
          return (DateInput.SingleEntry) null;
        this.StepForward(1);
        DateInput.SingleEntry singleEntry1 = this.Number();
        if (singleEntry1 == null)
          this.StepBack(1);
        return singleEntry1;
      }

      private bool CurrentIs(DateInput.TokenType tokenType)
      {
        if (this.CurrentToken() != null)
          return this.CurrentToken().Type == tokenType;
        return false;
      }

      private DateInput.Token CurrentToken()
      {
        if (this.currentTokenIndex > this.tokens.Count - 1 || this.currentTokenIndex < 0)
          return (DateInput.Token) null;
        return this.tokens[this.currentTokenIndex];
      }

      private void StepForward(int step)
      {
        this.currentTokenIndex += step;
      }

      private void StepBack(int step)
      {
        this.currentTokenIndex -= step;
      }

      private bool FirstIs(DateInput.TokenType tokenType)
      {
        if (this.FirstToken() != null)
          return this.FirstToken().Type == tokenType;
        return false;
      }

      private DateInput.Token FirstToken()
      {
        return this.tokens[0];
      }

      private bool NextIs(DateInput.TokenType tokenType)
      {
        if (this.NextToken() != null)
          return this.NextToken().Type == tokenType;
        return false;
      }

      private DateInput.Token NextToken()
      {
        if (this.currentTokenIndex + 1 > this.tokens.Count - 1 || this.currentTokenIndex < 0)
          return (DateInput.Token) null;
        return this.tokens[this.currentTokenIndex + 1];
      }

      private bool MatchTwoRules(
        string firstRule,
        string secondRule,
        out DateInput.DateEntry first,
        out DateInput.DateEntry second)
      {
        int currentTokenIndex = this.currentTokenIndex;
        first = this.ExecuteMethod(firstRule);
        second = (DateInput.DateEntry) null;
        if (first != null)
        {
          second = this.ExecuteMethod(secondRule);
          if (second != null)
            return true;
        }
        this.currentTokenIndex = currentTokenIndex;
        return false;
      }

      private DateInput.DateEntry ExecuteMethod(string methodName)
      {
        switch (methodName)
        {
          case "Number":
            return (DateInput.DateEntry) this.Number();
          case "Month":
            return (DateInput.DateEntry) this.Month();
          case "Pair":
            return (DateInput.DateEntry) this.Pair();
          case "TimeNumber":
            return (DateInput.DateEntry) this.TimeNumber();
          case "TimePair":
            return (DateInput.DateEntry) this.TimePair();
          case "AMPMTimeNumber":
            return (DateInput.DateEntry) this.AMPMTimeNumber();
          case "WeekDay":
            return (DateInput.DateEntry) this.WeekDay();
          default:
            return (DateInput.DateEntry) null;
        }
      }
    }

    public abstract class DateEntry
    {
      private DateInput parent;
      private DateInput.DateEntryType type;

      public DateInput Parent
      {
        get
        {
          return this.parent;
        }
        set
        {
          this.parent = value;
        }
      }

      public DateInput.DateEntryType Type
      {
        get
        {
          return this.type;
        }
        set
        {
          this.type = value;
        }
      }

      public DateEntry(DateInput.DateEntryType type, DateInput parent)
      {
        this.Type = type;
        this.Parent = parent;
      }

      public abstract DateTime Evaluate(DateTime date);
    }

    public class PairEntry : DateInput.DateEntry
    {
      private DateInput.Token first;
      private DateInput.Token second;

      public DateInput.Token First
      {
        get
        {
          return this.first;
        }
        set
        {
          this.first = value;
        }
      }

      public DateInput.Token Second
      {
        get
        {
          return this.second;
        }
        set
        {
          this.second = value;
        }
      }

      public PairEntry(DateInput.Token first, DateInput.Token second, DateInput parent)
        : base(DateInput.DateEntryType.DATEPAIR, parent)
      {
        this.first = first;
        this.second = second;
      }

      public override DateTime Evaluate(DateTime date)
      {
        return new DateInput.DateEvaluator(this.Parent) { MonthYearOnly = this.Parent.MonthYearOnly }.GetDate(new List<DateInput.Token>() { this.first, this.second }, date);
      }
    }

    public class TripletEntry : DateInput.DateEntry
    {
      private DateInput.Token first;
      private DateInput.Token second;
      private DateInput.Token third;

      public TripletEntry(
        DateInput.Token first,
        DateInput.Token second,
        DateInput.Token third,
        DateInput parent)
        : base(DateInput.DateEntryType.DATETRIPLET, parent)
      {
        this.first = first;
        this.second = second;
        this.third = third;
      }

      public override DateTime Evaluate(DateTime date)
      {
        return new DateInput.DateEvaluator(this.Parent).GetDate(new List<DateInput.Token>() { this.first, this.second, this.third }, date);
      }
    }

    public class SingleEntry : DateInput.DateEntry
    {
      private DateInput.Token token;

      public DateInput.Token Token
      {
        get
        {
          return this.token;
        }
        set
        {
          this.token = value;
        }
      }

      public SingleEntry(DateInput.Token token, DateInput parent)
        : base(DateInput.DateEntryType.SINGLE, parent)
      {
        this.token = token;
      }

      public override DateTime Evaluate(DateTime date)
      {
        return new DateInput.DateEvaluator(this.Parent).GetDateFromSingleEntry(this.token, date);
      }
    }

    public class EmptyDateEntry : DateInput.DateEntry
    {
      private DateInput.Token token;

      public EmptyDateEntry(DateInput.Token token, DateInput parent)
        : base(DateInput.DateEntryType.EMPTYDATE, parent)
      {
        this.token = token;
      }

      public EmptyDateEntry(DateInput parent)
        : base(DateInput.DateEntryType.EMPTYDATE, parent)
      {
      }

      public override DateTime Evaluate(DateTime date)
      {
        return date;
      }
    }

    public class DateTimeEntry : DateInput.DateEntry
    {
      private DateInput.DateEntry date;
      private DateInput.DateEntry time;

      public DateInput.DateEntry Date
      {
        get
        {
          return this.date;
        }
        set
        {
          this.date = value;
        }
      }

      public DateInput.DateEntry Time
      {
        get
        {
          return this.time;
        }
        set
        {
          this.time = value;
        }
      }

      public DateTimeEntry(DateInput parent)
        : base(DateInput.DateEntryType.DATETIME, parent)
      {
      }

      public override DateTime Evaluate(DateTime date)
      {
        return this.Time.Evaluate(this.Date.Evaluate(DateTime.Now.AddMilliseconds(7200000.0)));
      }
    }

    public class TimeEntry : DateInput.DateEntry
    {
      private List<DateInput.Token> tokens;

      public List<DateInput.Token> Tokens
      {
        get
        {
          return this.tokens;
        }
        set
        {
          this.tokens = value;
        }
      }

      public TimeEntry(List<DateInput.Token> tokens, DateInput parent)
        : base(DateInput.DateEntryType.TIME, parent)
      {
        this.tokens = tokens;
      }

      public override DateTime Evaluate(DateTime date)
      {
        bool flag1 = false;
        bool flag2 = false;
        if (this.tokens[this.tokens.Count - 1].Type == DateInput.TokenType.AMPM)
        {
          flag2 = true;
          flag1 = (this.tokens[this.tokens.Count - 1] as DateInput.AMPMToken).IsPm;
          this.tokens.RemoveAt(this.tokens.Count - 1);
        }
        if (this.tokens[this.tokens.Count - 1].Value.Length > 2)
        {
          string str = this.tokens[this.tokens.Count - 1].Value;
          this.tokens[this.tokens.Count - 1].Value = str.Substring(0, str.Length - 2);
          this.tokens.Add((DateInput.Token) new DateInput.NumberToken(str.Substring(str.Length - 2, str.Length), this.Parent));
        }
        int hour = 0;
        int minute = 0;
        int second = 0;
        if (this.tokens.Count > 0)
          hour = DateInput.DateEvaluator.ParseDecimalInt(this.tokens[0].Value);
        if (this.tokens.Count > 1)
          minute = DateInput.DateEvaluator.ParseDecimalInt(this.tokens[1].Value);
        if (this.tokens.Count > 2)
          second = DateInput.DateEvaluator.ParseDecimalInt(this.tokens[2].Value);
        if (hour < 24)
        {
          if (hour < 12 && flag1)
            hour += 12;
          else if (hour == 12 && !flag1 && flag2)
            hour = 0;
        }
        else
          hour = 0;
        return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, 0);
      }
    }

    public class NoSeparatorDateEntry : DateInput.DateEntry
    {
      private DateInput.Token Token;

      public NoSeparatorDateEntry(DateInput.Token token, DateInput parent)
        : base(DateInput.DateEntryType.NO_SEPARATOR_DATE, parent)
      {
        this.Token = token;
      }

      public override DateTime Evaluate(DateTime date)
      {
        string str = this.Token.Value;
        string[] strArray = new string[3];
        if (str.Length == 6)
        {
          strArray[0] = str.Substring(0, 2);
          strArray[1] = str.Substring(2, 2);
          strArray[2] = str.Substring(4, 2);
        }
        else
        {
          if (str.Length != 8)
            throw new Exception();
          Hashtable dateSlots = this.Parent.DateSlots;
          int startIndex = 0;
          for (int index = 0; index < 3; ++index)
          {
            if (index == (int) dateSlots[(object) "Year"])
            {
              strArray[index] = str.Substring(startIndex, 4);
              startIndex += 4;
            }
            else
            {
              strArray[index] = str.Substring(startIndex, 2);
              startIndex += 2;
            }
          }
        }
        List<DateInput.Token> tokens = new DateInput.DateTimeLexer(this.Parent).CreateTokens(new List<string>((IEnumerable<string>) strArray));
        return new DateInput.TripletEntry(tokens[0], tokens[1], tokens[2], this.Parent).Evaluate(date);
      }
    }

    public enum DateEntryType
    {
      DATEPAIR,
      DATETRIPLET,
      EMPTYDATE,
      DATETIME,
      TIME,
      NO_SEPARATOR_DATE,
      SINGLE,
    }

    public class DateEvaluator
    {
      private DateInput.Token[] buckets = new DateInput.Token[3];
      private bool monthYearOnly;
      private DateInput parent;

      public DateInput Parent
      {
        get
        {
          return this.parent;
        }
        set
        {
          this.parent = value;
        }
      }

      public DateInput.Token[] Buckets
      {
        get
        {
          return this.buckets;
        }
        set
        {
          this.buckets = value;
        }
      }

      public bool MonthYearOnly
      {
        get
        {
          return this.monthYearOnly;
        }
        set
        {
          this.monthYearOnly = value;
        }
      }

      public DateEvaluator(DateInput parent)
      {
        this.Parent = parent;
      }

      internal DateTime GetDate(List<DateInput.Token> tokens, DateTime date)
      {
        DateTime result1 = date;
        if (this.MonthYearOnly)
          this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]] = (DateInput.Token) new DateInput.NumberToken(date.Day.ToString(), this.Parent);
        this.Distribute(tokens);
        if (!this.MonthYearOnly && this.NumericSpecialCase(tokens))
          throw new Exception();
        int year = this.GetYear();
        if (year == -1)
          year = date.Year;
        DateTime result2 = this.SetYear(result1, year);
        int month = this.GetMonth();
        if (month == -1)
          month = date.Month;
        DateTime result3 = this.SetMonth(result2, month);
        int day = this.GetDay();
        if (day == -1)
          day = date.Day;
        return this.SetDay(result3, day);
      }

      private void Distribute(List<DateInput.Token> originalTokens)
      {
        List<DateInput.Token> tokenList = new List<DateInput.Token>((IEnumerable<DateInput.Token>) originalTokens);
        while (tokenList.Count > 0)
        {
          DateInput.Token token = tokenList[0];
          tokenList.RemoveAt(0);
          if (this.IsYear(token))
          {
            if (this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]] != null)
            {
              DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]];
              if (this.IsYear(bucket))
                throw new Exception();
              tokenList.Insert(0, bucket);
            }
            this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]] = token;
            if (!this.MonthYearOnly)
            {
              DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
              if (bucket != null)
              {
                this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]] = (DateInput.Token) null;
                tokenList.Insert(0, bucket);
              }
            }
          }
          else if (this.IsMonth(token))
          {
            if (this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]] != null)
              tokenList.Insert(0, this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]]);
            this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]] = token;
            DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
            if (bucket != null && !this.MonthYearOnly)
            {
              this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]] = (DateInput.Token) null;
              tokenList.Insert(0, bucket);
            }
          }
          else
          {
            int availablePosition = this.GetFirstAvailablePosition(token, this.Buckets);
            if (availablePosition != -1)
              this.Buckets[availablePosition] = token;
            else if (token.Type == DateInput.TokenType.NUMBER && this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]] == null && this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]] != null)
            {
              DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
              if (DateInput.DateEvaluator.ParseDecimalInt(bucket.Value) <= 12)
              {
                this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]] = token;
                this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]] = bucket;
              }
            }
          }
        }
      }

      private int TransformShortYear(int year)
      {
        if (year >= 100)
          return year;
        int num1 = this.Parent.ShortYearCenturyEnd - 99;
        int num2 = num1 % 100;
        int num3 = year - num2;
        if (num3 < 0)
          num3 += 100;
        return num1 + num3;
      }

      private int GetYear()
      {
        DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]];
        if (bucket == null)
          return -1;
        int year = DateInput.DateEvaluator.ParseDecimalInt(bucket.Value);
        if (bucket.Value.Length < 3)
          year = this.TransformShortYear(year);
        return year;
      }

      private int GetMonth()
      {
        if (this.IsYearDaySpecialCase())
          return -1;
        return this.GetMonthIndex();
      }

      private int GetMonthIndex()
      {
        DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]];
        if (bucket != null)
        {
          if (bucket.Type == DateInput.TokenType.MONTHNAME)
            return (bucket as DateInput.MonthNameToken).GetMonthIndex() + 1;
          if (bucket.Type == DateInput.TokenType.NUMBER)
            return DateInput.DateEvaluator.ParseDecimalInt(bucket.Value);
        }
        return -1;
      }

      private int GetDay()
      {
        if (this.IsYearDaySpecialCase())
          return DateInput.DateEvaluator.ParseDecimalInt(this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]].Value);
        DateInput.Token bucket = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
        if (bucket != null)
          return DateInput.DateEvaluator.ParseDecimalInt(bucket.Value);
        return -1;
      }

      private bool IsYearDaySpecialCase()
      {
        DateInput.Token bucket1 = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
        DateInput.Token bucket2 = this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]];
        DateInput.Token bucket3 = this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]];
        if (bucket2 != null && this.IsYear(bucket2) && (bucket3 != null && bucket3.Type == DateInput.TokenType.NUMBER))
          return bucket1 == null;
        return false;
      }

      private bool IsYear(DateInput.Token token)
      {
        if (token.Type != DateInput.TokenType.NUMBER)
          return false;
        int decimalInt = DateInput.DateEvaluator.ParseDecimalInt(token.Value);
        if (decimalInt <= (this.MonthYearOnly ? 12 : 31) || decimalInt > 9999)
          return token.Value.Length == 4;
        return true;
      }

      private bool IsMonth(DateInput.Token token)
      {
        return token.Type == DateInput.TokenType.MONTHNAME;
      }

      private int GetFirstAvailablePosition(DateInput.Token token, DateInput.Token[] buckets)
      {
        for (int index = 0; index < buckets.Length; ++index)
        {
          if ((index != (int) this.Parent.DateSlots[(object) "Month"] || token.Type != DateInput.TokenType.NUMBER || DateInput.DateEvaluator.ParseDecimalInt(token.Value) <= 12) && buckets[index] == null)
            return index;
        }
        return -1;
      }

      private bool NumericSpecialCase(List<DateInput.Token> tokens)
      {
        for (int index = 0; index < tokens.Count; ++index)
        {
          if (tokens[index].Type != DateInput.TokenType.NUMBER)
            return false;
        }
        DateInput.Token bucket1 = this.Buckets[(int) this.Parent.DateSlots[(object) "Day"]];
        DateInput.Token bucket2 = this.Buckets[(int) this.Parent.DateSlots[(object) "Year"]];
        DateInput.Token bucket3 = this.Buckets[(int) this.Parent.DateSlots[(object) "Month"]];
        int num = 0;
        if (bucket1 == null)
          ++num;
        if (bucket2 == null)
          ++num;
        if (bucket3 == null)
          ++num;
        return tokens.Count + num != this.Buckets.Length;
      }

      internal static int ParseDecimalInt(string number)
      {
        return int.Parse(number);
      }

      internal DateTime GetDateFromSingleEntry(DateInput.Token token, DateTime date)
      {
        DateTime result = date;
        if (token.Type == DateInput.TokenType.MONTHNAME)
          result = this.SetMonth(result, (token as DateInput.MonthNameToken).GetMonthIndex() + 1);
        else if (token.Type == DateInput.TokenType.WEEKDAYNAME)
        {
          int num = (7 - date.Day + ((token as DateInput.WeekDayNameToken).GetWeekDayIndex() + 1)) % 7;
          result = this.SetDay(result, result.Day + (num < 0 ? num + 7 : num));
        }
        else if (this.IsYear(token))
        {
          int year = this.TransformShortYear(DateInput.DateEvaluator.ParseDecimalInt(token.Value));
          int month = result.Month;
          int day = DateTime.DaysInMonth(year, month);
          if (day > result.Day)
            day = result.Day;
          result = new DateTime(year, result.Month, day, result.Hour, result.Minute, result.Second, result.Millisecond);
        }
        else
        {
          if (token.Type != DateInput.TokenType.NUMBER)
            throw new Exception();
          int decimalInt = DateInput.DateEvaluator.ParseDecimalInt(token.Value);
          if (decimalInt > 10000)
            throw new Exception();
          int day = DateTime.DaysInMonth(result.Year, result.Month);
          if (day > decimalInt)
            day = decimalInt;
          result = new DateTime(result.Year, result.Month, day, result.Hour, result.Minute, result.Second, result.Millisecond);
        }
        return result;
      }

      private DateTime SetMonth(DateTime result, int monthIndex)
      {
        if (monthIndex == 0)
          monthIndex = 12;
        int day = DateTime.DaysInMonth(result.Year, monthIndex);
        if (day > result.Day)
          day = result.Day;
        return new DateTime(result.Year, monthIndex, day, result.Hour, result.Minute, result.Second, result.Millisecond);
      }

      private DateTime SetDay(DateTime result, int day)
      {
        int day1 = DateTime.DaysInMonth(result.Year, result.Month);
        if (day1 > day && day != 0)
          day1 = day;
        return new DateTime(result.Year, result.Month, day1, result.Hour, result.Minute, result.Second, result.Millisecond);
      }

      private DateTime SetYear(DateTime result, int year)
      {
        int day = DateTime.DaysInMonth(year, result.Month);
        if (day > result.Day)
          day = result.Day;
        return new DateTime(year, result.Month, day, result.Hour, result.Minute, result.Second, result.Millisecond);
      }
    }
  }
}
