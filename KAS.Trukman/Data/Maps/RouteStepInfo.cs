using KAS.Trukman.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Maps
{
    #region RouteStepInfo
    public class RouteStepInfo : BaseData
    {
        public RouteStepInfo()
            : base()
        {
        }

        public int StepIndex
        {
            get { return (int)this.GetValue("StepIndex", (int)-1); }
            set { this.SetValue("StepIndex", value); }
        }

        public string Text
        {
            get { return (string)this.GetValue("Text"); }
            set { this.SetValue("Text", value); }
        }

        public string Distance
        {
            get { return (string)this.GetValue("Distance"); }
            set { this.SetValue("Distance", value); }
        }

        public RouteStepTurn Turn
        {
            get { return (RouteStepTurn)this.GetValue("Turn", RouteStepTurn.None); }
            set { this.SetValue("Turn", value); }
        }
    }
    #endregion

    #region RouteStepTurn
    public enum RouteStepTurn
    {
        None,
        Left,
        Right
    }
    #endregion
}
