using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region EmptyViewModel
    public class EmptyViewModel : PageViewModel
    {
        public EmptyViewModel()
            : base()
        {
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        public override void Appering()
        {
            base.Appering();

            this.IsBusy = true;
        }

        public override void Disappering()
        {
            this.IsBusy = false;

            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();
        }
    }
    #endregion
}
