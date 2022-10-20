using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models.User
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public GenderType? Gender { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool IsFromSocialNetwork { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportExpDate { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public string DriversLicence { get; set; }
        public string NationalIdentity { get; set; }
        public string PersonalPhoneNoCountryCode { get; set; }
        public string PersonalPhoneNo { get; set; }
        public string Meal { get; set; }
        public string PersonalAddress { get; set; }
        public string BusinessAddress { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactEmail { get; set; }
    }
}
