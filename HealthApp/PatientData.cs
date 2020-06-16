// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string BloodGrp { get; set; }
        public DateTime DOB { get; set; }
        public string DoctorName { get; set; }
        public string AppointmentDateTime { get; set; }

        public string TeamsMeetingLink { get; set; }

    }

    public class PatientData
    {
        /// List of Patients
        /// </summary>
        /// <returns></returns>
        public static List<Patient> LoadCollectionData()
        {
            List<Patient> Patients = new List<Patient>();
            Patients.Add(new Patient()
            {
                ID = 101,
                Name = "Aaryaman Chopra",
                DOB = new DateTime(1975, 2, 23),
                DoctorName = "Dr. Fauci",
                EmailId = "aaryamanchopra@gmail.com",
                PhoneNo = "425000007",
                BloodGrp="O+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 102,
                Name = "Mike Gold",
                DOB = new DateTime(1982, 4, 12),
                DoctorName = "Dr. Fauci",
                EmailId = "Mike@gmail.com",
                PhoneNo = "425000008",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 103,
                Name = "Mathew Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 104,
                Name = "Mathew Hall",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 104,
                Name = "Will Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 114,
                Name = "Henry Hill",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 115,
                Name = "Tom Mody",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 220,
                Name = "Patrick Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 230,
                Name = "Hall Kumar",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 240,
                Name = "Naveen Chopra",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 250,
                Name = "Tony Tom",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 260,
                Name = "Michael Bowen",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 261,
                Name = "Amy",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });



            Patients.Add(new Patient()
            {
                ID = 1001,
                Name = "Aaryaman Chopra",
                DOB = new DateTime(1975, 2, 23),
                DoctorName = "Dr. Fauci",
                EmailId = "aaryamanchopra@gmail.com",
                PhoneNo = "425000007",
                BloodGrp = "O+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1002,
                Name = "Mike Gold",
                DOB = new DateTime(1982, 4, 12),
                DoctorName = "Dr. Fauci",
                EmailId = "Mike@gmail.com",
                PhoneNo = "425000008",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1003,
                Name = "Mathew Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1004,
                Name = "Mathew Hall",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1005,
                Name = "Will Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1006,
                Name = "Henry Hill",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1007,
                Name = "Tom Mody",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1008,
                Name = "Patrick Cochran",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1009,
                Name = "Hall Kumar",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            Patients.Add(new Patient()
            {
                ID = 1010,
                Name = "Naveen Chopra",
                DOB = new DateTime(1985, 9, 11),
                DoctorName = "Dr. Fauci",
                EmailId = "Mathew@outlook.com",
                PhoneNo = "425000009",
                BloodGrp = "A+",
                TeamsMeetingLink = "",
                AppointmentDateTime = ""
            });
            //Patients.Add(new Patient()
            //{
            //    ID = 1011,
            //    Name = "Tony Tom",
            //    DOB = new DateTime(1985, 9, 11),
            //    DoctorName = "Dr. Fauci",
            //    EmailId = "Mathew@outlook.com",
            //    PhoneNo = "425000009",
            //    BloodGrp = "A+",
            //    TeamsMeetingLink = "",
            //    AppointmentDateTime = ""
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 1012,
            //    Name = "Michael Bowen",
            //    DOB = new DateTime(1985, 9, 11),
            //    DoctorName = "Dr. Fauci",
            //    EmailId = "Mathew@outlook.com",
            //    PhoneNo = "425000009",
            //    BloodGrp = "A+",
            //    TeamsMeetingLink = "",
            //    AppointmentDateTime = ""
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 1013,
            //    Name = "Amy",
            //    DOB = new DateTime(1985, 9, 11),
            //    DoctorName = "Dr. Fauci",
            //    EmailId = "Mathew@outlook.com",
            //    PhoneNo = "425000009",
            //    BloodGrp = "A+",
            //    TeamsMeetingLink = "",
            //    AppointmentDateTime = ""
            //});
            return Patients;
        }
    }
}
