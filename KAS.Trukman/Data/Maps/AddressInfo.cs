﻿using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Maps
{
    #region AddressInfo
    public class AddressInfo
    {
        public AddressInfo() 
            : base()
        {
        }

        public Position Position { get; set; }

        public string Address { get; set; }

        public IContractor Contractor { get; set; }
    }
    #endregion
}
