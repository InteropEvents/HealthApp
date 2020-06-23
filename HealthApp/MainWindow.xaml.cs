// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Graph;
//using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;


namespace HealthApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object SelectedItem;

        private void EnableMainGrid(bool enable)
        {
            if (enable)
            {
                this.loginControl.Visibility = Visibility.Hidden;
                this.patientDataGrid.Visibility = Visibility.Visible;
                this.menu1.Visibility = Visibility.Visible;
                this.patientDataGrid.Height = 750;
            }
            else
            {
                this.loginControl.Visibility = Visibility.Visible;
                this.patientDataGrid.Visibility = Visibility.Hidden;
                this.menu1.Visibility = Visibility.Hidden;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            patientDataGrid.ItemsSource = PatientData.LoadCollectionData();
            EnableMainGrid(false);
        }

        private void SendEmailClick(object sender, RoutedEventArgs e)
        {
            SelectedItem = patientDataGrid.SelectedItem;
            Patient Patient = (Patient)SelectedItem;
            // MessageBox.Show("Selected Patient: " + Patient.Name);
            // GraphCalls.SignIn();
        }
        private void ScheduleTeamsMeeting(object sender, RoutedEventArgs e)
        {
            Patient Patient = (Patient)patientDataGrid.SelectedItem;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (UserNameInput.Text == "Health" && PasswordInput.Password == "Health")
                EnableMainGrid(true);
            else
                MessageBox.Show("Incorrect user name or password", "Error", MessageBoxButton.OK);
        }

        private void PatientDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;
                PatientNameBox.Text = Patient.Name;
                PatientEmailBox.Text = Patient.EmailId;
                PatientPhoneBox.Text = Patient.PhoneNo;
                PatientBloodBox.Text = Patient.BloodGrp;
                PatientDetailsPopup.IsOpen = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
            }
        }

        private void ReassignDoc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;
                ReassignDoctorPopup_Name.Content = Patient.Name;
                ReassignDoctorPopup.IsOpen = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
            }
        }

        private async void TeamsMeeting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GraphCalls.SignIn();
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;
                TeamsMeetingPopup_PatientName.Content = Patient.Name;

                // Updating UI with date time.
                string[] splitAppointment = Patient.AppointmentDateTime.Split(' ');
                if (splitAppointment.Length > 1)
                {
                    TeamsMeetingPopup_StartDate.SelectedDate = DateTime.Parse(splitAppointment[0]);
                    TeamsMeetingPopup_EndDate.SelectedDate = DateTime.Parse(splitAppointment[0]);

                    TeamsMeetingPopup_StartTime.Text = splitAppointment[1];
                    TeamsMeetingPopup_EndTime.Text = (Int16.Parse(splitAppointment[1].Split(':')[0]) + 1).ToString() + ":00";
                }

                TeamsMeetingPopup.IsOpen = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
            }
        }


        private async void TeamsMeetingPopup_CreateMeetingClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable the crate meeting button
                TeamsMeetingPopup_CreateMeeting.IsEnabled = false;
                string appointmentStartDate = "", appointmentEndDate = "", appointmentStartTime, appointmentEndTime;

                // Get Patient Details
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;

                // Create Teams Meeting
                Patient.TeamsMeetingLink = await GraphCalls.CreateMeeting(TeamsMeetingPopup_StartDate.SelectedDate, TeamsMeetingPopup_EndDate.SelectedDate);

                // Getting Date from UI
                DateTime? startDate = TeamsMeetingPopup_StartDate.SelectedDate;
                DateTime? endDate = TeamsMeetingPopup_EndDate.SelectedDate;

                if (startDate.HasValue)
                    appointmentStartDate = startDate.Value.ToString("MM.dd.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                if (endDate.HasValue)
                    appointmentEndDate = endDate.Value.ToString("MM.dd.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Getting Time from UI
                appointmentStartTime = TeamsMeetingPopup_StartTime.Text;
                appointmentEndTime = TeamsMeetingPopup_EndTime.Text;
                //TeamsMeetingPopup_EndTime.Text;
                // End Date is always equal to Start Date.
                // End time is always 1hr increment of Start Time.

                // Updating appointment date time in Patient List
                Patient.AppointmentDateTime = appointmentStartDate + " " + appointmentStartTime;
                TeamsMeetingPopup.IsOpen = false;

                string contentMessage = @"<p>Hello " + Patient.Name + ": <br> Your meeting with " + Patient.DoctorName + " is confirmed.";
                // Send appointment to Patient 
                Sendmail_With_IcsAttachment(Patient.TeamsMeetingLink, DateTime.Parse(appointmentStartDate + " " + appointmentStartTime), DateTime.Parse(appointmentEndDate + " " + appointmentEndTime), Patient.DoctorName, Patient.EmailId, contentMessage);

                contentMessage = @"<p>Hello " + Patient.DoctorName + ": <br> Your meeting with Patient: " + Patient.Name + " is confirmed.";
                // Send appointment to Doctor 
                Sendmail_With_IcsAttachment(Patient.TeamsMeetingLink, DateTime.Parse(appointmentStartDate + " " + appointmentStartTime), DateTime.Parse(appointmentEndDate + " " + appointmentEndTime), Patient.Name, Patient.DoctorEmailId, contentMessage);

                // Enable the crate meeting button
                TeamsMeetingPopup_CreateMeeting.IsEnabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
                TeamsMeetingPopup_CreateMeeting.IsEnabled = true;
            }

            // Update the Grid 
            patientDataGrid.Items.Refresh();
        }

        private async void O365Login_ClickAsync(object sender, RoutedEventArgs e)
        {
            await GraphCalls.SignIn();
            EnableMainGrid(true);
        }
        private void PatientDetailsPopup_SaveClick(object sender, RoutedEventArgs e)
        {
            PatientDetailsPopup.IsOpen = false;
            ((Patient)SelectedItem).Name = PatientNameBox.Text;
            ((Patient)SelectedItem).EmailId = PatientEmailBox.Text;
            ((Patient)SelectedItem).PhoneNo = PatientPhoneBox.Text;
            ((Patient)SelectedItem).BloodGrp = PatientBloodBox.Text;

            // Update the Grid 
            patientDataGrid.Items.Refresh();

        }
        private void PatientDetailsPopup_CloseClick(object sender, RoutedEventArgs e)
        {
            PatientDetailsPopup.IsOpen = false;
        }


        private void TeamsMeetingPopup_CloseClick(object sender, RoutedEventArgs e)
        {
            TeamsMeetingPopup.IsOpen = false;
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;
                ScheduleAppontiment_PatientName.Content = Patient.Name;

                // Updating UI with date time.
                string[] splitAppointment = Patient.AppointmentDateTime.Split(' ');
                if (splitAppointment.Length > 1)
                {
                    ScheduleAppontimentMeetingPopup_StartDate.SelectedDate = DateTime.Parse(splitAppointment[0]);
                    ScheduleAppontimentMeetingPopup_EndDate.SelectedDate = DateTime.Parse(splitAppointment[0]);

                    ScheduleAppontimentMeetingPopup_StartTime.Text = splitAppointment[1];
                    ScheduleAppontimentMeetingPopup_EndTime.Text = (Int16.Parse(splitAppointment[1].Split(':')[0]) + 1).ToString() + ":00";
                }

                ScheduleAppontimentMeetingPopup.IsOpen = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
            }
        }

        private void ScheduleAppointment_CreateAppointmentClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string appointmentStartDate = "", appointmentStartTime;

                // Getting Patient Details
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;

                // Getting Date from UI
                DateTime? startDate = ScheduleAppontimentMeetingPopup_StartDate.SelectedDate;
                DateTime? endDate = ScheduleAppontimentMeetingPopup_EndDate.SelectedDate;

                if (startDate.HasValue)
                    appointmentStartDate = startDate.Value.ToString("MM.dd.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Getting Time from UI
                appointmentStartTime = ScheduleAppontimentMeetingPopup_StartTime.Text;

                // End Date is always equal to Start Date.
                // End time is always 1hr increment of Start Time.

                // Updating appointment date time in Patient List
                Patient.AppointmentDateTime = appointmentStartDate + " " + appointmentStartTime;
                ScheduleAppontimentMeetingPopup.IsOpen = false;

                // Update the Grid 
                patientDataGrid.Items.Refresh();
            }
            catch (Exception)
            {
            }
        }

        private void ScheduleAppointment_CloseClick(object sender, RoutedEventArgs e)
        {
            ScheduleAppontimentMeetingPopup.IsOpen = false;
        }

        private void ReassignDoctorPopup_SaveClick(object sender, RoutedEventArgs e)
        {
            ReassignDoctorPopup.IsOpen = false;
        }

        private void ReassignDoctorPopup_CloseClick(object sender, RoutedEventArgs e)
        {
            ReassignDoctorPopup.IsOpen = false;
        }

        private void ReassignDoctorPopup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Updating Patient Details
                SelectedItem = patientDataGrid.SelectedItem;
                Patient Patient = (Patient)SelectedItem;
                Patient.DoctorName = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            }
            catch (Exception)
            {

            }

            // Refresh the Grid 
            patientDataGrid.Items.Refresh();
        }

        public void Sendmail_With_IcsAttachment(String meetingUrl, DateTime? startTime, DateTime? endTime, string name, string messageToEmail, string contentMessage)
        {
            string htmlString = contentMessage + " Please use following link to join the call:</p><p class=\"x_MsoNormal\"><span style=\"font - family:&quot; Segoe UI&quot;,sans - serif; color:#252424\"><a href=" + meetingUrl + " target=\"_blank\" rel=\"noopener noreferrer\" data-auth=\"NotApplicable\"><span style=\"\font-size:18.0pt; font-family:&quot;Segoe UI Semibold&quot;,sans-serif; color:#6264A7\">Join online now</span></a> </span></p><p>Thanks <br>Health Care Provider</p>";

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(App.SMTPEmail, "HealthApp");
            msg.To.Add(new MailAddress(messageToEmail, messageToEmail));
            msg.Subject = "Meeting with " + name;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Headers.Add("Content-class", "urn:content-classes:calendarmessage");

            // Now Contruct the ICS file using string builder
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Schedule a Meeting");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine("X - WR - TIMEZONE:America / Los_Angeles");
            str.AppendLine("TZID: America / Los_Angeles");

            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", startTime.Value.AddHours(7)));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", endTime.Value.AddHours(7)));
            str.AppendLine("LOCATION: " + "Teams meeting");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            // Now sending a mail with attachment ICS file.                     
            System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient(App.SMTPEmailHost, 587);
            smtpclient.UseDefaultCredentials = false;
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = new System.Net.NetworkCredential(App.SMTPEmail, App.SMTPPassword);
            var htmlContentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html);
            var avHtmlBody = AlternateView.CreateAlternateViewFromString(htmlString, htmlContentType);
            msg.AlternateViews.Add(avHtmlBody);

            System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
            contype.Parameters.Add("method", "REQUEST");
            contype.Parameters.Add("name", "Meeting.ics");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
            msg.AlternateViews.Add(avCal);

            smtpclient.Send(msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TokenCacheHelper.removeTokenFile();
        }
    }
}
