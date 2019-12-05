using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProjectWebAPITest
{
    class TestResource
    {
        public const string adminName = "admin";
        public const string adminPassWord = "9999";

        //Flight Data
        public static DateTime NewFlight_DEPARTURE_TIME = DateTime.ParseExact("2019-01-08 08:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime NewFlight_LANDING_TIME = DateTime.ParseExact("2019-01-08 11:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int NewFlight_REMANING_TICKETS = 19;
        public const int NewFlight_TOTAL_TICKETS = 100;
        public const long ORIGIN_COUNTRY_CODE = 3;
        public const long DESTINATION_COUNTRY_CODE = 2;
        public const long AIRLINE_COMPANY_ID = 2;
        public const string CUSTOMER_USERNAME = "222";
        public const string CUSTOMER_PASSWORD = "555";
        public const string FIRST_NAME = "Noam";
        public const string LAST_NAME = "Shemesh";
        public const string ADDRESS = "PT";
        public const string PHONE_NO = "052-222";
        public const string CREDIT_CARD_NUMBER = "444";
       

    }
}
