using System;

namespace Trukman.Droid
{
    public class Order
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string ReceiverAddress { get; set; }
        public string SenderAddress { get; set; }

        public String GetOrder() 
        {
            return string.Format("Sender: {0}\nReceiver: {1}\nLoad address: {2}\nReceiver address: {3}", 
                Sender, Receiver, SenderAddress, ReceiverAddress);
        }

        #region Constructors
        public Order() { }

        public Order(string sender, string receiver, string senderAddress, string receiverAddress)
        {
            Sender = sender;
            Receiver = receiver;
            ReceiverAddress = receiverAddress;
            SenderAddress = senderAddress;
        }

        public Order(string[] args) 
        {
            try {
                Sender = args[0];
                Receiver = args[1];
                ReceiverAddress = args[2];
                SenderAddress = args[3];
            } catch(IndexOutOfRangeException) {}
        }
        #endregion
    }
}

