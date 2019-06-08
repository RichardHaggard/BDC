// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadAutoCompleteBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadAutoCompleteBoxElement : RadTextBoxControlElement
  {
    private static readonly Size AutoCompletePopupOffset = new Size(0, 2);
    private Point autoCompleteDropDownLocation;
    private readonly RadTokenizedTextItemCollection items;

    public RadAutoCompleteBoxElement()
    {
      this.AutoCompleteDropDown.KeyDown += new KeyEventHandler(this.OnAutoCompleteDropDownKeyDown);
      this.items = this.CreateTokenizedItemCollection();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.autoCompleteDropDownLocation = Point.Empty;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Navigator = (ITextBoxNavigator) new AutoCompleteTextNavigator(this);
      this.InputHandler = (ITextBoxInputHandler) new AutoCompleteBoxInputHandler(this);
      this.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.AutoCompleteDropDown.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
    }

    public override void CloseDropDown(RadPopupCloseReason reason)
    {
      if (reason == RadPopupCloseReason.CloseCalled && this.ListElement.SuggestedText != null && (this.ListElement.PatternText != null && this.ListElement.IsSuggestionMatched))
        return;
      base.CloseDropDown(reason);
    }

    protected override RadTextBoxListElement CreateListElement()
    {
      return (RadTextBoxListElement) new RadAutoCompleteBoxListElement();
    }

    protected override TextBoxViewElement CreateViewElement()
    {
      return (TextBoxViewElement) new AutoCompleteBoxViewElement();
    }

    protected virtual RadTokenizedTextItemCollection CreateTokenizedItemCollection()
    {
      return new RadTokenizedTextItemCollection(this);
    }

    public bool ShowRemoveButton
    {
      get
      {
        return this.AutoCompleteViewElement.ShowRemoveButton;
      }
      set
      {
        this.AutoCompleteViewElement.ShowRemoveButton = value;
      }
    }

    public string AutoCompleteValueMember
    {
      get
      {
        return this.ListElement.ValueMember;
      }
      set
      {
        this.ListElement.ValueMember = value;
      }
    }

    public char Delimiter
    {
      get
      {
        return this.AutoCompleteViewElement.Delimiter;
      }
      set
      {
        this.AutoCompleteViewElement.Delimiter = value;
      }
    }

    public RadTokenizedTextItemCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected AutoCompleteBoxViewElement AutoCompleteViewElement
    {
      get
      {
        return this.ViewElement as AutoCompleteBoxViewElement;
      }
    }

    protected internal override bool CanPerformAutoComplete
    {
      get
      {
        return this.AutoCompleteMode != AutoCompleteMode.None;
      }
    }

    protected Point AutoCompleteDropDownLocation
    {
      get
      {
        return this.autoCompleteDropDownLocation;
      }
      set
      {
        this.autoCompleteDropDownLocation = value;
      }
    }

    public event TokenValidatingEventHandler TokenValidating
    {
      add
      {
        this.AutoCompleteViewElement.TokenValidating += value;
      }
      remove
      {
        this.AutoCompleteViewElement.TokenValidating -= value;
      }
    }

    private void OnAutoCompleteDropDownKeyDown(object sender, KeyEventArgs e)
    {
      this.OnAutoCompleteDropDownKeyDown(e);
    }

    protected virtual void OnAutoCompleteDropDownKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Tab)
        return;
      e.Handled = true;
      this.AutoCompleteDropDown.ClosePopup(new PopupCloseInfo(RadPopupCloseReason.Keyboard, (object) e));
    }

    protected override void OnAutoCompleteDropDownClosed(RadAutoCompleteDropDownClosedEventArgs e)
    {
      MouseEventArgs inputArguments1 = e.InputArguments as MouseEventArgs;
      KeyEventArgs inputArguments2 = e.InputArguments as KeyEventArgs;
      this.autoCompleteDropDownLocation = Point.Empty;
      if (inputArguments1 == null && (inputArguments2 == null || inputArguments2.KeyCode != Keys.Return && inputArguments2.KeyCode != Keys.Tab))
        return;
      int selectedIndex = this.ListElement.SelectedIndex;
      if (selectedIndex != -1)
      {
        this.ListElement.SuspendSelectionEvents = true;
        this.ListElement.OnSelectedIndexChanged(selectedIndex);
        this.ListElement.SuspendSelectionEvents = false;
      }
      AutoCompleteBoxViewElement viewElement = this.ViewElement as AutoCompleteBoxViewElement;
      TextPosition endPosition = this.ListElement.EndPosition;
      viewElement.Insert(endPosition, viewElement.Delimiter.ToString());
      this.ListElement.StartPosition = this.GetFirstAutoCompletePosition();
      this.ListElement.EndPosition = this.GetLastAutoCompletePosition();
    }

    protected override bool IsValidAutoCompletePosition()
    {
      if (this.SelectionLength != 0)
        return false;
      TextPosition caretPosition = this.Navigator.CaretPosition;
      if (caretPosition == (TextPosition) null || caretPosition.TextBlock is TokenizedTextBlockElement)
        return false;
      TextPosition completePosition = this.GetLastAutoCompletePosition();
      return object.Equals((object) caretPosition, (object) completePosition);
    }

    protected override void PerformAutoCompleteOverride(EditOperation context)
    {
      if (this.autoCompleteDropDownLocation == Point.Empty)
      {
        this.autoCompleteDropDownLocation = new Point(this.Caret.Position.X, (int) this.Navigator.CaretPosition.Line.ControlBoundingRectangle.Bottom);
        this.autoCompleteDropDownLocation = this.ElementTree.Control.PointToScreen(this.autoCompleteDropDownLocation);
        this.autoCompleteDropDownLocation.Y += RadAutoCompleteBoxElement.AutoCompletePopupOffset.Height;
        this.autoCompleteDropDownLocation.X += RadAutoCompleteBoxElement.AutoCompletePopupOffset.Width;
      }
      base.PerformAutoCompleteOverride(context);
      bool completeDropDownOpen = this.IsAutoCompleteDropDownOpen;
      if (completeDropDownOpen && this.ListElement.SelectedIndex == -1)
      {
        this.ListElement.SuspendSuggestNotifications();
        this.ListElement.SelectedIndex = 0;
        this.ListElement.ResumeSuggestNotifications();
      }
      if (!completeDropDownOpen)
        this.autoCompleteDropDownLocation = Point.Empty;
      if (this.ListElement.Items.Count != 1)
        return;
      Point point = new Point(this.Caret.Position.X, (int) this.Navigator.CaretPosition.Line.ControlBoundingRectangle.Bottom);
      point = this.ElementTree.Control.PointToScreen(point);
      point.Y += RadAutoCompleteBoxElement.AutoCompletePopupOffset.Height;
      point.X += RadAutoCompleteBoxElement.AutoCompletePopupOffset.Width;
      this.ShowDropDown(point);
    }

    protected override Point GetAutoCompleteDropDownLocation()
    {
      return this.autoCompleteDropDownLocation;
    }

    protected internal override TextPosition GetFirstAutoCompletePosition()
    {
      AutoCompleteTextNavigator navigator = this.Navigator as AutoCompleteTextNavigator;
      return navigator.GetPreviousEditablePosition(navigator.CaretPosition);
    }

    protected internal override TextPosition GetLastAutoCompletePosition()
    {
      AutoCompleteTextNavigator navigator = this.Navigator as AutoCompleteTextNavigator;
      return navigator.GetNextEditablePosition(navigator.CaretPosition);
    }
  }
}
