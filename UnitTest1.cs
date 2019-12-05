using System;
using System.Net.Mail;
using System.Net.Sockets;
using FlightCenterProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FlightProjectWebAPITest
{
    [TestClass]
    public class UnitTest1
    {
        private const string URL = "http://localhost:59701";
        private string GetAllFlightsUrl = $"{URL}/api/anonymousFacade/allFlights";
        private string CreateNewAirlineUrl = $"{URL}/api/AdministratorFacade/CreateNewAirline";
        private string GetAllFlightsOfSpecificAirline = $"{URL}/api/companyFacade/getAllFlights";
        private string CreateCustomerUrl= $"{URL}/api/AdministratorFacade/CreateNewCustomer";
        private string GetAllCustomerFlightsUrl = $"{URL}/api/CustomerFacade/GetAllMyFlights";

        /// <summary>
        /// Test for anonymous controller- checks if the api gets all flights.
        /// </summary>
        [TestMethod]
        public void TestAnonymousController_GetAllFlights()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("");
                string ss = Convert.ToBase64String(plainTextBytes);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = client.GetAsync(GetAllFlightsUrl).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        IList<Flight> flights = content.ReadAsAsync<IList<Flight>>().Result;

                        Assert.AreNotEqual(null, flights);
                        Assert.AreEqual(TestResource.NewFlight_LANDING_TIME, flights[0].LandingTime);
                        Assert.AreEqual(TestResource.NewFlight_DEPARTURE_TIME, flights[0].DepartureTime);
                        Assert.AreEqual(TestResource.DESTINATION_COUNTRY_CODE, flights[0].DestinationCountryCode);
                        Assert.AreEqual(TestResource.AIRLINE_COMPANY_ID, flights[0].AirlineCompanyId);
                        Assert.AreEqual(TestResource.ORIGIN_COUNTRY_CODE, flights[0].OriginCountryCode);
                        Assert.AreEqual(TestResource.NewFlight_REMANING_TICKETS, flights[0].RemainingTickets);
                    }
                }
            }
        }

        /// <summary>
        ///  Test for Airline company controller- checks if the api gets all flights of the specific airline.
        /// </summary>

        [TestMethod]
        public void TestAirlineCompanyController_GetAllFlightsWebApi_FlightsReceived()
        {
         
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            var byteArray = Encoding.ASCII.GetBytes("888:999");
            
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",Convert.ToBase64String(byteArray));

           client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(GetAllFlightsOfSpecificAirline);

            using (HttpResponseMessage response = client.GetAsync(GetAllFlightsOfSpecificAirline).Result)
            {
                using (HttpContent content = response.Content)
                {
                    IList<Flight> flights = content.ReadAsAsync<IList<Flight>>().Result;

                    Assert.AreNotEqual(null, flights);
                    Assert.AreEqual(TestResource.NewFlight_LANDING_TIME, flights[0].LandingTime);
                    Assert.AreEqual(TestResource.NewFlight_DEPARTURE_TIME, flights[0].DepartureTime);
                    Assert.AreEqual(TestResource.DESTINATION_COUNTRY_CODE, flights[0].DestinationCountryCode);
                    Assert.AreEqual(TestResource.AIRLINE_COMPANY_ID, flights[0].AirlineCompanyId);
                    Assert.AreEqual(TestResource.ORIGIN_COUNTRY_CODE, flights[0].OriginCountryCode);
                    Assert.AreEqual(TestResource.NewFlight_REMANING_TICKETS, flights[0].RemainingTickets);
                }
            }
        }

        /// <summary>
        ///  Test for Administrator controller- checks if the api post new customer.
        /// </summary>

        [TestMethod]
        public void TestAdministratorController_CreateNewCustomer()
        {

            HttpClient client_post = new HttpClient();

            client_post.DefaultRequestHeaders.Accept.Clear();

            var byteArray = Encoding.ASCII.GetBytes("admin:9999");

            client_post.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            client_post.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            client_post.BaseAddress = new Uri(CreateCustomerUrl);

            Customer customer = new Customer
            {
                FirstName = TestResource.FIRST_NAME,
                LastName = TestResource.LAST_NAME,
                UserName = TestResource.CUSTOMER_USERNAME,
                Password = TestResource.CUSTOMER_PASSWORD,
                Address = TestResource.ADDRESS,
                PhoneNo = TestResource.PHONE_NO,
                CreditCardNumber = TestResource.CREDIT_CARD_NUMBER
            };
            using (HttpResponseMessage response = client_post.PostAsJsonAsync(CreateCustomerUrl, customer).Result)
            {
                int status = (int)response.StatusCode;
                Assert.AreEqual(200, status);
            }           
        }

        /// <summary>
        ///  Test for Customer controller- checks if the api gets all flights of the specific customer.
        /// </summary>

        [TestMethod]
        public void TestCustomerController_GetAllMyFlights()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            var byteArray = Encoding.ASCII.GetBytes("angrydog684:727272");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(GetAllCustomerFlightsUrl);

            using (HttpResponseMessage response = client.GetAsync(GetAllCustomerFlightsUrl).Result)
            {
                using (HttpContent content = response.Content)
                {
                    IList<Flight> flights = content.ReadAsAsync<IList<Flight>>().Result;

                    Assert.AreNotEqual(null, flights);
                    Assert.AreEqual(TestResource.NewFlight_LANDING_TIME, flights[0].LandingTime);
                    Assert.AreEqual(TestResource.NewFlight_DEPARTURE_TIME, flights[0].DepartureTime);
                    Assert.AreEqual(TestResource.DESTINATION_COUNTRY_CODE, flights[0].DestinationCountryCode);
                    Assert.AreEqual(TestResource.AIRLINE_COMPANY_ID, flights[0].AirlineCompanyId);
                    Assert.AreEqual(TestResource.ORIGIN_COUNTRY_CODE, flights[0].OriginCountryCode);
                    Assert.AreEqual(TestResource.NewFlight_REMANING_TICKETS, flights[0].RemainingTickets);
                }
            }
        }

    }
}
