// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
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
            else {
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

        private void PatientDetails_Click(object sender, RoutedEventArgs e) {
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
            catch (Exception ex)
            {
                MessageBox.Show("Please select a Patient before performing this Action.");
            }
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
            catch (Exception ex)
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
                DateTime? startDate  = ScheduleAppontimentMeetingPopup_StartDate.SelectedDate;
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
    }
}
