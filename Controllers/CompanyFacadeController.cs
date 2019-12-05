using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightCenterProject;
using FlightCenterProject.Facade;

namespace FlightWebApplication.Controllers
{
   [BasicAuthenticationCompany]
    public class CompanyFacadeController : ApiController
    {
        static ApiFacade apiFacade;
        LoginToken<AirlineCompany> airlineLoginToken;

        static CompanyFacadeController()
        {          
            apiFacade = new ApiFacade();
        }

        public LoggedInAirlineFacade getAirlineFacade()
        {
            Request.Properties.TryGetValue("airlineToken", out object loginUser);
            airlineLoginToken = (LoginToken<AirlineCompany>)loginUser;
            Request.Properties.TryGetValue("airlineFacade", out object facade);

            return (LoggedInAirlineFacade)facade;
        }

        [ResponseType(typeof(List<Ticket>))]
        [HttpGet]
        [Route("api/companyFacade/getAllTickets")]       
        public IHttpActionResult GetAllTickets()
        {
            try
            {
                IList<Ticket> tickets = getAirlineFacade().GetAllTickets(airlineLoginToken);
                if (tickets.Count == 0)
                    return NotFound();
                return Ok(tickets);
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
           
        }

        [ResponseType(typeof(IList<Flight>))]
        [HttpGet]
        [Route("api/companyFacade/getAllFlights")]       
        public IHttpActionResult GetAllFlights()
        {
            try
            {
                IList<Flight> flights = getAirlineFacade().GetAllFlights(airlineLoginToken);
                if (flights.Count == 0)
                    return NotFound();
                return Ok(flights);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("api/CompanyFacade/CreateFlight")]
        public IHttpActionResult CreateFlight([FromBody] Flight flight)
        {
            try
            {
                if (flight == null)
                    return BadRequest(ModelState);
                getAirlineFacade().CreateFlight(airlineLoginToken, flight);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
           

        [HttpPut]
        [Route("api/CompanyFacade/updateFlight/{id}")]       
        public IHttpActionResult UpdateFlight([FromBody] Flight flight,[FromUri] long id)
        {
            try
            {
                if (apiFacade.GetMyFlight(id) == null)
                    return BadRequest(ModelState);
                else
                {
                    flight.Id = id;
                    getAirlineFacade().UpdateFlight(airlineLoginToken, flight);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("api/CompanyFacade/ChangeMyPassword/{oldPassword}/{newPassword}")]
        public IHttpActionResult ChangeMyPassword([FromUri] string oldPassword,[FromUri] string newPassword)
        {
            try
            {
                getAirlineFacade().ChangeMyPassword(airlineLoginToken, oldPassword, newPassword);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }

        [HttpPut]
        [Route("api/CompanyFacade/mofidyAirlineDetails")]        
        public IHttpActionResult MofidyAirlineDetails([FromBody] AirlineCompany airline,[FromUri] long id)
        {
            try
            {
                if (apiFacade.GetMyCompany(id) == null)
                    return BadRequest(ModelState);
                else
                {
                    airline.Id = id;
                    getAirlineFacade().MofidyAirlineDetails(airlineLoginToken, airline);
                    return Ok();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }

        [HttpDelete]
        [Route("api/CompanyFacade/CancelFlight/{id}")]
        public IHttpActionResult CancelFlight(long flightId)
        {
            try
            {
                Flight flight = apiFacade.GetMyFlight(flightId);
                if (flight == null)
                    return BadRequest(ModelState);
                getAirlineFacade().CancelFlight(airlineLoginToken, flight);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.ToString());
            }
        }
    }
}
