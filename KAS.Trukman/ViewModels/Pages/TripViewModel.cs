using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using KAS.Trukman.AppContext;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.ViewModels.Pages
{
    #region TripViewModel
    public class TripViewModel : PageViewModel
    {
        public TripViewModel()
            : base()
        {
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SelectItemCommand = new VisualCommand(this.SelectItem);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            var trip = (parameters != null && parameters.Length > 0 ? (parameters[0] as Trip) : null);
            this.Shipper = (trip != null ? trip.Shipper : null);
            this.Receiver = (trip != null ? trip.Receiver : null);

            this.FindAddress();
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "SelectedItem")
                this.FindAddress();

            base.DoPropertyChanged(propertyName);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.TripPageName;
        }

        private void FindAddress()
		{
			var position = (this.SelectedItem == TripContractorItems.Shipper ? TrukmanContext.Driver.ShipperPosition : TrukmanContext.Driver.ReceiverPosition);
			if ((position.Latitude == 0) && (position.Longitude == 0))
				position = TrukmanContext.Driver.Location;
			this.ContractorPosition = position;
		}

        private void ShowHomePage(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowPrevPage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectItem(object parameter)
        {
            if (parameter != null)
                this.SelectedItem = (TripContractorItems)parameter;
        }

        public Contractor Shipper
        {
            get { return (this.GetValue("Shipper") as Contractor); }
            set { this.SetValue("Shipper", value); }
        }

        public Contractor Receiver
        {
            get { return (this.GetValue("Receiver") as Contractor); }
            set { this.SetValue("Receiver", value); }
        }

        public TripContractorItems SelectedItem
        {
            get { return (TripContractorItems)this.GetValue("SelectedItem", TripContractorItems.Shipper); }
            set { this.SetValue("SelectedItem", value); }
        }

        public Position ContractorPosition
        {
            get { return (Position)this.GetValue("ContractorPosition", new Position(0, 0)); }
            set { this.SetValue("ContractorPosition", value); }
        }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand SelectItemCommand { get; private set; }
    }
    #endregion

    #region TripContractorItems
    public enum TripContractorItems
    {
        Shipper = 0,
        Receiver = 1
    }
    #endregion
}
