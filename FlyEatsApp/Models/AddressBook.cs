using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class AddressBook
    {

        private const string ADDRESS_ID_COLUMN = "AddressId";
        private const string USER_ID_COLUMN = "UserId";
        private const string ADDRESS_LINE_1_COLUMN = "AddressLine1";
        private const string ADDRESS_LINE_2_COLUMN = "AddressLine2";
        private const string CITY_COLUMN = "City";
        private const string STATE_PROVINCE_COLUMN = "StateProvince";
        private const string ZIP_POSTAL_CODE_COLUMN = "ZipPostalCode";
        private const string COUNTRY_COLUMN = "Country";
        private const string LATITUDE_COLUMN = "Latitude";
        private const string LONGITUDE_COLUMN = "Longitude";
        private const string PHONE_NUMBER_COLUMN = "PhoneNumber";
        private const string ADDRESS_TYPE_COLUMN = "AddressType";
        private const string ACTIVE_COLUMN = "Active";
        private const string TIMESTAMP_COLUMN = "Timestamp";

        public int? AddressId { get; set; }
        public int? UserId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? StateProvince { get; set; }
        public string? ZipPostalCode { get; set; }
        public string? Country { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressType { get; set; }
        public bool? Active { get; set; }
        public DateTime? Timestamp { get; set; }

        public static AddressBook Extract(DataRow row)
        {
            return new AddressBook
            {
                AddressId = (int)row[ADDRESS_ID_COLUMN],
                UserId = (int)row[USER_ID_COLUMN],
                AddressLine1 = (string)row[ADDRESS_LINE_1_COLUMN],
                AddressLine2 = row.IsNull(ADDRESS_LINE_2_COLUMN) ? null : (string)row[ADDRESS_LINE_2_COLUMN],
                City = (string)row[CITY_COLUMN],
                StateProvince = (string)row[STATE_PROVINCE_COLUMN],
                ZipPostalCode = (string)row[ZIP_POSTAL_CODE_COLUMN],
                Country = (string)row[COUNTRY_COLUMN],
                Latitude = row.IsNull(LATITUDE_COLUMN) ? (decimal?)null : (decimal)row[LATITUDE_COLUMN],
                Longitude = row.IsNull(LONGITUDE_COLUMN) ? (decimal?)null : (decimal)row[LONGITUDE_COLUMN],
                PhoneNumber = row.IsNull(PHONE_NUMBER_COLUMN) ? null : (string)row[PHONE_NUMBER_COLUMN],
                AddressType = (string)row[ADDRESS_TYPE_COLUMN],
                Active = row.IsNull(ACTIVE_COLUMN) ? (bool?)null : (bool)row[ACTIVE_COLUMN],
                Timestamp = (DateTime)row[TIMESTAMP_COLUMN],
            };
        }

    }
}
