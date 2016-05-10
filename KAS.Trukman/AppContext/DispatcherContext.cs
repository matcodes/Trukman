using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KAS.Trukman.Storage;

namespace KAS.Trukman.AppContext
{
    #region DispatcherContext
    public class DispatcherContext 
    {
        private LocalStorage _localStorage = null;

        public DispatcherContext(LocalStorage localStorage) 
            : base()
        {
            _localStorage = localStorage;
        }

        public void Synchronize()
        {
        }
    }
    #endregion
}