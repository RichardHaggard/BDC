// Decompiled with JetBrains decompiler
// Type: BuilderRED.My.MyProject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BuilderRED.My
{
  [StandardModule]
  [HideModuleName]
  [GeneratedCode("MyTemplate", "11.0.0.0")]
  internal sealed class MyProject
  {
    private static readonly MyProject.ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new MyProject.ThreadSafeObjectProvider<MyComputer>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new MyProject.ThreadSafeObjectProvider<MyApplication>();
    private static readonly MyProject.ThreadSafeObjectProvider<User> m_UserObjectProvider = new MyProject.ThreadSafeObjectProvider<User>();
    private static MyProject.ThreadSafeObjectProvider<MyProject.MyForms> m_MyFormsObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyForms>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices> m_MyWebServicesObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices>();

    [HelpKeyword("My.Computer")]
    internal static MyComputer Computer
    {
      [DebuggerHidden] get
      {
        return MyProject.m_ComputerObjectProvider.GetInstance;
      }
    }

    [HelpKeyword("My.Application")]
    internal static MyApplication Application
    {
      [DebuggerHidden] get
      {
        return MyProject.m_AppObjectProvider.GetInstance;
      }
    }

    [HelpKeyword("My.User")]
    internal static User User
    {
      [DebuggerHidden] get
      {
        return MyProject.m_UserObjectProvider.GetInstance;
      }
    }

    [HelpKeyword("My.Forms")]
    internal static MyProject.MyForms Forms
    {
      [DebuggerHidden] get
      {
        return MyProject.m_MyFormsObjectProvider.GetInstance;
      }
    }

    [HelpKeyword("My.WebServices")]
    internal static MyProject.MyWebServices WebServices
    {
      [DebuggerHidden] get
      {
        return MyProject.m_MyWebServicesObjectProvider.GetInstance;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
    internal sealed class MyForms
    {
      [ThreadStatic]
      private static Hashtable m_FormBeingCreated;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgBudget m_dlgBudget;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgCalculateArea m_dlgCalculateArea;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgCopySections m_dlgCopySections;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgCoverage m_dlgCoverage;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgInspectSections m_dlgInspectSections;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgLogin m_dlgLogin;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgOtherDistresses m_dlgOtherDistresses;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgTime m_dlgTime;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public dlgTimeoutMessage m_dlgTimeoutMessage;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmAboutBRED m_frmAboutBRED;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmADAInspection m_frmADAInspection;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmAddMissingComponent m_frmAddMissingComponent;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmAttachComponent m_frmAttachComponent;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmAttachSection m_frmAttachSection;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmChooseInspector m_frmChooseInspector;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmComment m_frmComment;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmCriteria m_frmCriteria;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmDatePicker m_frmDatePicker;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmDistresses m_frmDistresses;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmDoesNotContain m_frmDoesNotContain;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmEditLocation m_frmEditLocation;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmInspectionTally m_frmInspectionTally;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmMain m_frmMain;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmMissingComponents m_frmMissingComponents;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewBuilding m_frmNewBuilding;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewComponent m_frmNewComponent;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewFuncArea m_frmNewFuncArea;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewSample m_frmNewSample;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewSection m_frmNewSection;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmNewSystem m_frmNewSystem;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmReportsViewer m_frmReportsViewer;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmSecDetails m_frmSecDetails;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmSeismic m_frmSeismic;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmSplash m_frmSplash;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmTaskList m_frmTaskList;
      [EditorBrowsable(EditorBrowsableState.Never)]
      public frmWorkItems m_frmWorkItems;

      [DebuggerHidden]
      private static T Create__Instance__<T>(T Instance) where T : Form, new()
      {
        if ((object) Instance != null && !Instance.IsDisposed)
          return Instance;
        if (MyProject.MyForms.m_FormBeingCreated != null)
        {
          if (MyProject.MyForms.m_FormBeingCreated.ContainsKey((object) typeof (T)))
            throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate"));
        }
        else
          MyProject.MyForms.m_FormBeingCreated = new Hashtable();
        MyProject.MyForms.m_FormBeingCreated.Add((object) typeof (T), (object) null);
        TargetInvocationException invocationException;
        try
        {
          return new T();
        }
        catch (TargetInvocationException ex) when (
        {
          // ISSUE: unable to correctly present filter
          ProjectData.SetProjectError((Exception) ex);
          invocationException = ex;
          if (invocationException.InnerException != null)
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
          throw new InvalidOperationException(Utils.GetResourceString("WinForms_SeeInnerException", invocationException.InnerException.Message), invocationException.InnerException);
        }
        finally
        {
          MyProject.MyForms.m_FormBeingCreated.Remove((object) typeof (T));
        }
      }

      [DebuggerHidden]
      private void Dispose__Instance__<T>(ref T instance) where T : Form
      {
        instance.Dispose();
        instance = default (T);
      }

      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public MyForms()
      {
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override bool Equals(object o)
      {
        return base.Equals(RuntimeHelpers.GetObjectValue(o));
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      internal new System.Type GetType()
      {
        return typeof (MyProject.MyForms);
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      public override string ToString()
      {
        return base.ToString();
      }

      public dlgBudget dlgBudget
      {
        [DebuggerHidden] get
        {
          this.m_dlgBudget = MyProject.MyForms.Create__Instance__<dlgBudget>(this.m_dlgBudget);
          return this.m_dlgBudget;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgBudget)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgBudget>(ref this.m_dlgBudget);
        }
      }

      public dlgCalculateArea dlgCalculateArea
      {
        [DebuggerHidden] get
        {
          this.m_dlgCalculateArea = MyProject.MyForms.Create__Instance__<dlgCalculateArea>(this.m_dlgCalculateArea);
          return this.m_dlgCalculateArea;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgCalculateArea)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgCalculateArea>(ref this.m_dlgCalculateArea);
        }
      }

      public dlgCopySections dlgCopySections
      {
        [DebuggerHidden] get
        {
          this.m_dlgCopySections = MyProject.MyForms.Create__Instance__<dlgCopySections>(this.m_dlgCopySections);
          return this.m_dlgCopySections;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgCopySections)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgCopySections>(ref this.m_dlgCopySections);
        }
      }

      public dlgCoverage dlgCoverage
      {
        [DebuggerHidden] get
        {
          this.m_dlgCoverage = MyProject.MyForms.Create__Instance__<dlgCoverage>(this.m_dlgCoverage);
          return this.m_dlgCoverage;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgCoverage)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgCoverage>(ref this.m_dlgCoverage);
        }
      }

      public dlgInspectSections dlgInspectSections
      {
        [DebuggerHidden] get
        {
          this.m_dlgInspectSections = MyProject.MyForms.Create__Instance__<dlgInspectSections>(this.m_dlgInspectSections);
          return this.m_dlgInspectSections;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgInspectSections)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgInspectSections>(ref this.m_dlgInspectSections);
        }
      }

      public dlgLogin dlgLogin
      {
        [DebuggerHidden] get
        {
          this.m_dlgLogin = MyProject.MyForms.Create__Instance__<dlgLogin>(this.m_dlgLogin);
          return this.m_dlgLogin;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgLogin)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgLogin>(ref this.m_dlgLogin);
        }
      }

      public dlgOtherDistresses dlgOtherDistresses
      {
        [DebuggerHidden] get
        {
          this.m_dlgOtherDistresses = MyProject.MyForms.Create__Instance__<dlgOtherDistresses>(this.m_dlgOtherDistresses);
          return this.m_dlgOtherDistresses;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgOtherDistresses)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgOtherDistresses>(ref this.m_dlgOtherDistresses);
        }
      }

      public dlgTime dlgTime
      {
        [DebuggerHidden] get
        {
          this.m_dlgTime = MyProject.MyForms.Create__Instance__<dlgTime>(this.m_dlgTime);
          return this.m_dlgTime;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgTime)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgTime>(ref this.m_dlgTime);
        }
      }

      public dlgTimeoutMessage dlgTimeoutMessage
      {
        [DebuggerHidden] get
        {
          this.m_dlgTimeoutMessage = MyProject.MyForms.Create__Instance__<dlgTimeoutMessage>(this.m_dlgTimeoutMessage);
          return this.m_dlgTimeoutMessage;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_dlgTimeoutMessage)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<dlgTimeoutMessage>(ref this.m_dlgTimeoutMessage);
        }
      }

      public frmAboutBRED frmAboutBRED
      {
        [DebuggerHidden] get
        {
          this.m_frmAboutBRED = MyProject.MyForms.Create__Instance__<frmAboutBRED>(this.m_frmAboutBRED);
          return this.m_frmAboutBRED;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmAboutBRED)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmAboutBRED>(ref this.m_frmAboutBRED);
        }
      }

      public frmADAInspection frmADAInspection
      {
        [DebuggerHidden] get
        {
          this.m_frmADAInspection = MyProject.MyForms.Create__Instance__<frmADAInspection>(this.m_frmADAInspection);
          return this.m_frmADAInspection;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmADAInspection)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmADAInspection>(ref this.m_frmADAInspection);
        }
      }

      public frmAddMissingComponent frmAddMissingComponent
      {
        [DebuggerHidden] get
        {
          this.m_frmAddMissingComponent = MyProject.MyForms.Create__Instance__<frmAddMissingComponent>(this.m_frmAddMissingComponent);
          return this.m_frmAddMissingComponent;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmAddMissingComponent)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmAddMissingComponent>(ref this.m_frmAddMissingComponent);
        }
      }

      public frmAttachComponent frmAttachComponent
      {
        [DebuggerHidden] get
        {
          this.m_frmAttachComponent = MyProject.MyForms.Create__Instance__<frmAttachComponent>(this.m_frmAttachComponent);
          return this.m_frmAttachComponent;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmAttachComponent)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmAttachComponent>(ref this.m_frmAttachComponent);
        }
      }

      public frmAttachSection frmAttachSection
      {
        [DebuggerHidden] get
        {
          this.m_frmAttachSection = MyProject.MyForms.Create__Instance__<frmAttachSection>(this.m_frmAttachSection);
          return this.m_frmAttachSection;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmAttachSection)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmAttachSection>(ref this.m_frmAttachSection);
        }
      }

      public frmChooseInspector frmChooseInspector
      {
        [DebuggerHidden] get
        {
          this.m_frmChooseInspector = MyProject.MyForms.Create__Instance__<frmChooseInspector>(this.m_frmChooseInspector);
          return this.m_frmChooseInspector;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmChooseInspector)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmChooseInspector>(ref this.m_frmChooseInspector);
        }
      }

      public frmComment frmComment
      {
        [DebuggerHidden] get
        {
          this.m_frmComment = MyProject.MyForms.Create__Instance__<frmComment>(this.m_frmComment);
          return this.m_frmComment;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmComment)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmComment>(ref this.m_frmComment);
        }
      }

      public frmCriteria frmCriteria
      {
        [DebuggerHidden] get
        {
          this.m_frmCriteria = MyProject.MyForms.Create__Instance__<frmCriteria>(this.m_frmCriteria);
          return this.m_frmCriteria;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmCriteria)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmCriteria>(ref this.m_frmCriteria);
        }
      }

      public frmDatePicker frmDatePicker
      {
        [DebuggerHidden] get
        {
          this.m_frmDatePicker = MyProject.MyForms.Create__Instance__<frmDatePicker>(this.m_frmDatePicker);
          return this.m_frmDatePicker;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmDatePicker)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmDatePicker>(ref this.m_frmDatePicker);
        }
      }

      public frmDistresses frmDistresses
      {
        [DebuggerHidden] get
        {
          this.m_frmDistresses = MyProject.MyForms.Create__Instance__<frmDistresses>(this.m_frmDistresses);
          return this.m_frmDistresses;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmDistresses)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmDistresses>(ref this.m_frmDistresses);
        }
      }

      public frmDoesNotContain frmDoesNotContain
      {
        [DebuggerHidden] get
        {
          this.m_frmDoesNotContain = MyProject.MyForms.Create__Instance__<frmDoesNotContain>(this.m_frmDoesNotContain);
          return this.m_frmDoesNotContain;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmDoesNotContain)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmDoesNotContain>(ref this.m_frmDoesNotContain);
        }
      }

      public frmEditLocation frmEditLocation
      {
        [DebuggerHidden] get
        {
          this.m_frmEditLocation = MyProject.MyForms.Create__Instance__<frmEditLocation>(this.m_frmEditLocation);
          return this.m_frmEditLocation;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmEditLocation)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmEditLocation>(ref this.m_frmEditLocation);
        }
      }

      public frmInspectionTally frmInspectionTally
      {
        [DebuggerHidden] get
        {
          this.m_frmInspectionTally = MyProject.MyForms.Create__Instance__<frmInspectionTally>(this.m_frmInspectionTally);
          return this.m_frmInspectionTally;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmInspectionTally)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmInspectionTally>(ref this.m_frmInspectionTally);
        }
      }

      public frmMain frmMain
      {
        [DebuggerHidden] get
        {
          this.m_frmMain = MyProject.MyForms.Create__Instance__<frmMain>(this.m_frmMain);
          return this.m_frmMain;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmMain)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmMain>(ref this.m_frmMain);
        }
      }

      public frmMissingComponents frmMissingComponents
      {
        [DebuggerHidden] get
        {
          this.m_frmMissingComponents = MyProject.MyForms.Create__Instance__<frmMissingComponents>(this.m_frmMissingComponents);
          return this.m_frmMissingComponents;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmMissingComponents)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmMissingComponents>(ref this.m_frmMissingComponents);
        }
      }

      public frmNewBuilding frmNewBuilding
      {
        [DebuggerHidden] get
        {
          this.m_frmNewBuilding = MyProject.MyForms.Create__Instance__<frmNewBuilding>(this.m_frmNewBuilding);
          return this.m_frmNewBuilding;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewBuilding)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewBuilding>(ref this.m_frmNewBuilding);
        }
      }

      public frmNewComponent frmNewComponent
      {
        [DebuggerHidden] get
        {
          this.m_frmNewComponent = MyProject.MyForms.Create__Instance__<frmNewComponent>(this.m_frmNewComponent);
          return this.m_frmNewComponent;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewComponent)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewComponent>(ref this.m_frmNewComponent);
        }
      }

      public frmNewFuncArea frmNewFuncArea
      {
        [DebuggerHidden] get
        {
          this.m_frmNewFuncArea = MyProject.MyForms.Create__Instance__<frmNewFuncArea>(this.m_frmNewFuncArea);
          return this.m_frmNewFuncArea;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewFuncArea)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewFuncArea>(ref this.m_frmNewFuncArea);
        }
      }

      public frmNewSample frmNewSample
      {
        [DebuggerHidden] get
        {
          this.m_frmNewSample = MyProject.MyForms.Create__Instance__<frmNewSample>(this.m_frmNewSample);
          return this.m_frmNewSample;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewSample)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewSample>(ref this.m_frmNewSample);
        }
      }

      public frmNewSection frmNewSection
      {
        [DebuggerHidden] get
        {
          this.m_frmNewSection = MyProject.MyForms.Create__Instance__<frmNewSection>(this.m_frmNewSection);
          return this.m_frmNewSection;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewSection)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewSection>(ref this.m_frmNewSection);
        }
      }

      public frmNewSystem frmNewSystem
      {
        [DebuggerHidden] get
        {
          this.m_frmNewSystem = MyProject.MyForms.Create__Instance__<frmNewSystem>(this.m_frmNewSystem);
          return this.m_frmNewSystem;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmNewSystem)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmNewSystem>(ref this.m_frmNewSystem);
        }
      }

      public frmReportsViewer frmReportsViewer
      {
        [DebuggerHidden] get
        {
          this.m_frmReportsViewer = MyProject.MyForms.Create__Instance__<frmReportsViewer>(this.m_frmReportsViewer);
          return this.m_frmReportsViewer;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmReportsViewer)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmReportsViewer>(ref this.m_frmReportsViewer);
        }
      }

      public frmSecDetails frmSecDetails
      {
        [DebuggerHidden] get
        {
          this.m_frmSecDetails = MyProject.MyForms.Create__Instance__<frmSecDetails>(this.m_frmSecDetails);
          return this.m_frmSecDetails;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmSecDetails)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmSecDetails>(ref this.m_frmSecDetails);
        }
      }

      public frmSeismic frmSeismic
      {
        [DebuggerHidden] get
        {
          this.m_frmSeismic = MyProject.MyForms.Create__Instance__<frmSeismic>(this.m_frmSeismic);
          return this.m_frmSeismic;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmSeismic)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmSeismic>(ref this.m_frmSeismic);
        }
      }

      public frmSplash frmSplash
      {
        [DebuggerHidden] get
        {
          this.m_frmSplash = MyProject.MyForms.Create__Instance__<frmSplash>(this.m_frmSplash);
          return this.m_frmSplash;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmSplash)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmSplash>(ref this.m_frmSplash);
        }
      }

      public frmTaskList frmTaskList
      {
        [DebuggerHidden] get
        {
          this.m_frmTaskList = MyProject.MyForms.Create__Instance__<frmTaskList>(this.m_frmTaskList);
          return this.m_frmTaskList;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmTaskList)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmTaskList>(ref this.m_frmTaskList);
        }
      }

      public frmWorkItems frmWorkItems
      {
        [DebuggerHidden] get
        {
          this.m_frmWorkItems = MyProject.MyForms.Create__Instance__<frmWorkItems>(this.m_frmWorkItems);
          return this.m_frmWorkItems;
        }
        [DebuggerHidden] set
        {
          if (value == this.m_frmWorkItems)
            return;
          if (value != null)
            throw new ArgumentException("Property can only be set to Nothing");
          this.Dispose__Instance__<frmWorkItems>(ref this.m_frmWorkItems);
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
    internal sealed class MyWebServices
    {
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override bool Equals(object o)
      {
        return base.Equals(RuntimeHelpers.GetObjectValue(o));
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override int GetHashCode()
      {
        return base.GetHashCode();
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      internal new System.Type GetType()
      {
        return typeof (MyProject.MyWebServices);
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override string ToString()
      {
        return base.ToString();
      }

      [DebuggerHidden]
      private static T Create__Instance__<T>(T instance) where T : new()
      {
        if ((object) instance == null)
          return new T();
        return instance;
      }

      [DebuggerHidden]
      private void Dispose__Instance__<T>(ref T instance)
      {
        instance = default (T);
      }

      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public MyWebServices()
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [ComVisible(false)]
    internal sealed class ThreadSafeObjectProvider<T> where T : new()
    {
      internal T GetInstance
      {
        [DebuggerHidden] get
        {
          if ((object) MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
            MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = new T();
          return MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
        }
      }

      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public ThreadSafeObjectProvider()
      {
      }
    }
  }
}
