﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;

namespace AuthzReadWriteProperty
{
  [Serializable]
  public class Root : BusinessBase<Root>
  {
    #region Business Methods

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }

    public static readonly PropertyInfo<int> Num1Property = RegisterProperty<int>(c => c.Num1, "Num1", 9);
    public int Num1
    {
      get { return GetProperty(Num1Property); }
      set { SetProperty(Num1Property, value); }
    }


    public static readonly PropertyInfo<int> Num2Property = RegisterProperty<int>(c => c.Num2);
    public int Num2
    {
      get { return GetProperty(Num2Property); }
      set { SetProperty(Num2Property, value); }
    }

    #endregion

    #region Validation Rules

    protected override void AddBusinessRules()
    {
      // call base class implementation to add data annotation rules to BusinessRules 
      base.AddBusinessRules();

      // add authorization rules 
      BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, NameProperty, "nobody"));
      BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, Num1Property, "nobody"));
      BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, Num2Property, "nobody"));
      BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, Num1Property, "nobody"));
      BusinessRules.AddRule(new IsInRole(AuthorizationActions.ReadProperty, Num2Property, "nobody"));
    }

    #endregion

    #region Factory Methods

    public static Root NewEditableRoot()
    {
      return DataPortal.Create<Root>();
    }


    #endregion

    protected override void DataPortal_Create()
    {
      using (BypassPropertyChecks)
      {
        Name = "Rocky Lhotka";
        Num1 = 1001;
        Num2 = 666;
      }
      base.DataPortal_Create();
    }


  }
}