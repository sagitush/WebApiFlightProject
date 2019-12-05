using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightCenterProject;


namespace FlightWebApplication.Controllers
{
     public class AnonymousFacadeController : ApiController
     {
       static AnonymousUserFacade anonymous;

        static AnonymousFacadeController()
        {
            anonymous = new AnonymousUserFacade();
        }

        /// <summary>
        /// Get all flights
        /// </summary>
        /// <returns></returns>
        [Route("api/anonymousFacade/allFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
           IList<Flight> flights= anonymous.GetAllFlights();
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get all airlines
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IList<AirlineCompany>))]
        [Route("api/anonymousFacade/allAirlines")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> airlines = anonymous.GetAllAirlineCompanies();
            if (airlines.Count == 0)
                return NotFound();
            return Ok(airlines);
        }

        /// <summary>
        /// Get all flights vacancy
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Dictionary<Flight, int>))]
        [HttpGet]
        [Route("api/anonymousFacade/allFlightsVacancy")]       
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> vacancy = anonymous.GetAllFlightsVacancy();
            if (vacancy.Count == 0)
                return NotFound();
            return Ok(vacancy);           
        }

        /// <summary>
        /// Get specific flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsById/{id}")]      
        public IHttpActionResult GetFlightById([FromUri] int id)
        {
            Flight flight = anonymous.GetFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);          
        }

        /// <summary>
        /// Get Flights by origin country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByOriginCountry/{countryCode}")]     
        public IHttpActionResult GetFlightsByOriginCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByOriginCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights by destination country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByDestinationCountry/{countryCode}")]        
        public IHttpActionResult GetFlightsByDestinationCountry([FromUri] int countryCode)
        {
            IList<Flight> flights = anonymous.GetFlightsByDestinationCountry(countryCode);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights departure date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByDepatrureDate/{departureDate}")]    
        public IHttpActionResult GetFlightsByDepatrureDate([FromUri] DateTime departureDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByDepatrureDate(departureDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }

        /// <summary>
        /// Get flights landing date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/anonymousFacade/flightsByLandingDate/{landingDate}")]       
        public IHttpActionResult GetFlightsByLandingDate([FromUri] DateTime landingDate)
        {
            IList<Flight> flights = anonymous.GetFlightsByLandingDate(landingDate);
            if (flights.Count == 0)
                return NotFound();
            return Ok(flights);
        }
     }
}
