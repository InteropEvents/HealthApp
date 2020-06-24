## Register your app to support Microsoft accounts or work or school accounts

Register your application on the [Microsoft Azure portal](https://portal.azure.com/#home) to support Microsoft accounts or work or school accounts. If you’ve previously registered your application on the [Microsoft Application Portal](https://apps.dev.microsoft.com/), your existing apps will show up in the new and improved Azure portal experience.


## API permissions
Add the following permissions:
- User.Read - allows your application to sign-in your user
- OnlineMeetings.Read
- OnlineMeetings.ReadWrite

## Authentication
- Click 'Add a platform' and add 'Mobile and desktop applications'
- Select 'https://login.microsoftonline.com/common/oauth2/nativeclient' as Redirect URIs

## Overview
After making above changes, copy Application (client) ID and Directory (tenant) ID and paste in App.xaml.cs

![Optional Text](https://github.com/InteropEvents/HealthApp/blob/Teledoc/HealthApp/Images/Overview.PNG)



