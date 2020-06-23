// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace HealthApp
{
    class GraphCalls
    {
        private static string[] scopes = new string[] { "user.read OnlineMeetings.ReadWrite" };
        private static GraphServiceClient graphClient = new GraphServiceClient(authenticationProvider: null);
        private static string bearerToken;

        public async static Task SignIn(bool isSignout = false)
        {
            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;
            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            try
            {
                authResult = await app.AcquireTokenSilent(scopes, firstAccount)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent. 
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await app.AcquireTokenInteractive(scopes)
                        .WithAccount(accounts.FirstOrDefault())
                        //.WithParentActivityOrWindow(null/*new WindowInteropHelper(this).Handle*/) // optional, used to center the browser on the window
                        .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException)
                {
                    //ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                }
            }
            catch (Exception)
            {
                //ResultText.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                return;
            }

            if (authResult != null)
            {
                // ResultText.Text = await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken);
                // DisplayBasicTokenInfo(authResult);

                graphClient = new GraphServiceClient(authenticationProvider: null);
                bearerToken = authResult.AccessToken;

                // this.SignOutButton.Visibility = Visibility.Visible;
            }
        }

        public async static Task<string> CreateMeeting(DateTime? startDateTime, DateTime? endDateTime)
        {
            List<HeaderOption> requestHeaders = new List<HeaderOption>() { new HeaderOption("Authorization", "Bearer " + bearerToken) };

            var onlineMeeting = new OnlineMeeting
            {
                //StartDateTime = DateTimeOffset.Parse("2020-05-28T21:30:34.2444915+00:00"),
                //EndDateTime = DateTimeOffset.Parse("2020-07-17T21:30:34.2464912+00:00"),
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Subject = "Upcoming Health Appointment Notification"
            };

            var meeting = await graphClient.Me.OnlineMeetings
                .Request(requestHeaders)
                .AddAsync(onlineMeeting);

            return meeting.JoinWebUrl;
        }

        public async static void SendEmail()
        {
            List<HeaderOption> requestHeaders = new List<HeaderOption>() { new HeaderOption("Authorization", "Bearer " + bearerToken) };

            var message = new Message
            {
                Subject = "Meet for lunch?",
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = "The new cafeteria is open."
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = "tarunchopra@outlook.com"
                        }
                    }
                },
            };

            var saveToSentItems = false;
            await graphClient.Me
                .SendMail(message, saveToSentItems)
                .Request(requestHeaders)
                .PostAsync();
        }
    }
}
