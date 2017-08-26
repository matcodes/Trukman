using System;
using UIKit;
using KAS.Trukman.Messages;
using Foundation;
using Parse;
using System.Threading.Tasks;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.AppContext;
using System.Text.RegularExpressions;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.iOS
{
    public class PushHelperIOS : NSObject
    {
        public PushHelperIOS()
        {
            ShowNotificationMessage.Subscribe(this, this.ShowLocalMessage);

            ParsePush.ParsePushNotificationReceived += (object sender, ParsePushNotificationEventArgs args) =>
            {
                string message = null;
                try
                {
                    var payload = args.Payload;
                    object aps;
                    if (payload.TryGetValue("aps", out aps))
                    {
                        string payloadStr = "";

                        try
                        {
                            payloadStr = aps.ToString();
                        }
                        catch (Exception e)
                        {
                            ShowToastMessage.Send(e.Message);
                        }

                        try
                        {
                            var match = Regex.Match(payloadStr, @"alert = (.*);\n");
                            if (match.Success)
                            {
                                string alertText = match.Groups[1].Value;
                                message = alertText;
                            }
                        }
                        catch (Exception e)
                        {
                            ShowToastMessage.Send(e.Message);

                        }
                    }
                }
                catch (Exception)
                {
                    message = "payload crash";
                }
                finally
                {
                    if (string.IsNullOrEmpty(message))
                    {

                    }
                    else
                    {
                        ShowToastMessage.Send(message);
                    }
                }

            };
        }

        public void Register(ShowSignUpOwnerWelcomePageMessage message)
        {
            this.InvokeOnMainThread(() =>
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                        new NSSet());

                    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                }
                else
                {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }
            });
        }

        public void SubscribeMessages()
        {
            ShowSignUpOwnerWelcomePageMessage.Subscribe(this, this.Register);
        }

        public void UnsubscribeMessages()
        {
            ShowSignUpOwnerWelcomePageMessage.Unsubscribe(this);
        }

        public void ShowLocalMessage(ShowNotificationMessage message)
        {
            this.InvokeOnMainThread(() =>
            {
                string text = message.MessageText;
                // create the notification
                var notification = new UILocalNotification();

                // set the fire date (the date time in which it will fire)
                notification.FireDate = NSDate.FromTimeIntervalSinceNow(2);

                // configure the alert
                notification.AlertBody = text;

                // set the sound to be the default sound
                notification.SoundName = UILocalNotification.DefaultSoundName;

                // schedule it
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            });
        }

        public void DidGetDeviceToken(NSData deviceToken)
        {
            // Get current device token
            var DeviceToken = deviceToken.Description;
            if (!string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = DeviceToken.Trim('<').Trim('>');
            }

            // Get previous device token

            ParseInstallation installation = ParseInstallation.CurrentInstallation;

            var oldDeviceToken = installation.DeviceToken;
            // Has the token changed?
            if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
            {
                NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");

                //TODO: Put your own logic here to notify your server that the device token has changed/been created!
                installation.SetDeviceTokenFromData(deviceToken);

                Task.Run(async () =>
                {
                    try
                    {
                        var company = (TrukmanContext.Company as Company); // await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);

                        if (company != null)
                        {
                            await ParsePush.SubscribeAsync(Regex.Replace(company.Name.ToLower(), @"\s+", ""));
                        }

                        await installation.SaveAsync();
                    }
                    catch
                    {

                    }
                }).LogExceptions("PushHelperIOS DidGetDeviceToken");
            }

            // Save new device token 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ShowNotificationMessage.Unsubscribe(this);
        }
    }
}

