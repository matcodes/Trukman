using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Messages
{
    #region ShowPointsAndRewardsPageMessage
    public class ShowPointsAndRewardsPageMessage
    {
        #region Static members
        private static readonly string MESSAGE_KEY = "ShowPointsAndRewardsPageMessage";

        public static void Send()
        {
            var message = new ShowPointsAndRewardsPageMessage();
            MessagingCenter.Send<ShowPointsAndRewardsPageMessage>(message, MESSAGE_KEY);
        }

        public static void Subscribe(object subscriber, Action<ShowPointsAndRewardsPageMessage> action)
        {
            MessagingCenter.Subscribe<ShowPointsAndRewardsPageMessage>(subscriber, MESSAGE_KEY, action);
        }

        public static void Unsubscribe(object subscriber)
        {
            MessagingCenter.Unsubscribe<ShowPointsAndRewardsPageMessage>(subscriber, MESSAGE_KEY);
        }
        #endregion

        public ShowPointsAndRewardsPageMessage()
        {
        }
    }
    #endregion
}
