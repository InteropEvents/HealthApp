using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Identity.Client;

//  StartupUri="MainWindow.xaml"

namespace HealthApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {           

        }

        private static string Instance = "https://login.microsoftonline.com/";

        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // The content of ClientID with the Application Id for your app registration
        private static string ClientId = "";
        // The content of Tenant where your above application is registered.
        private static string Tenant = "";

        // (Optional) - Fields used to send meeting invites.
        // SMTP host address which will be used to send meeting invite to Patient and Doctor
        public static string SMTPEmailHost = "smtp.office365.com";
        // Email address of the SMTP account which will be used to send the invite
        public static string SMTPEmail = "";
        // Password of the above SMTP account 
        public static string SMTPPassword = "";

        private static IPublicClientApplication _clientApp;

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
