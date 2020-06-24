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
                }
            }
            catch (Exception)
            {
                return;
            }

            if (authResult != null)
            {
                graphClient = new GraphServiceClient(authenticationProvider: null);
                bearerToken = authResult.AccessToken;
            }
        }

        public async static Task<string> CreateMeeting(DateTime? startDateTime, DateTime? endDateTime)
        {
            List<HeaderOption> requestHeaders = new List<HeaderOption>() { new HeaderOption("Authorization", "Bearer " + bearerToken) };

            var onlineMeeting = new OnlineMeeting
            {
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Subject = "Upcoming Health Appointment Notification"
            };

            var meeting = await graphClient.Me.OnlineMeetings
                .Request(requestHeaders)
                .AddAsync(onlineMeeting);

            return meeting.JoinWebUrl;
        }
    }
}
