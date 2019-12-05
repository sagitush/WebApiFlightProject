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
    [BasicAuthenticationCustomer]
    public class CustomerFacadeController : ApiController
    {       
        static ApiFacade apiFacade;
        LoginToken<Customer> custLoginToken;
        static CustomerFacadeController()
        {
            apiFacade = new ApiFacade();
        }

        public LoggedInCustomerFacade getCustomerFacade()
        {
            Request.Properties.TryGetValue("customerToken", out object loginUser);
            custLoginToken = (LoginToken<Customer>)loginUser;
            Request.Properties.TryGetValue("customerFacade", out object facade);

            return (LoggedInCustomerFacade)facade;
        }

        /// <summary>
        /// Get all the flight of this customer
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IList<Flight>))]
        [HttpGet]
        [Route("api/CustomerFacade/GetAllMyFlights")]
        public IHttpActionResult GetAllMyFlights()
        {
            try
            {
                IList<Flight> flights = getCustomerFacade().GetAllMyFlights(custLoginToken);
                if (flights.Count == 0)
                    return NotFound();
                return Ok(flights);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        /// <summary>
        /// Purchase ticket to this customer
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/CustomerFacade/PurchaseTicket")]
        public IHttpActionResult PurchaseTicket(long flightId)
        {
            try
            {
                Flight flight = apiFacade.GetMyFlight(flightId);
                if (flight == null)
                {
                    return BadRequest(ModelState);
                }
                Ticket ticket = getCustomerFacade().PurchaseTicket(custLoginToken, flight);
                return Ok(ticket);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Cancel ticket for this customer
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/CustomerFacade/CancelTicket")]
        public IHttpActionResult CancelTicket(long ticketId)
        {
            try
            {
                Ticket ticket = apiFacade.GetMyTicket(ticketId);
                if (ticket == null)
                    return BadRequest(ModelState);
                getCustomerFacade().CancelTicket(custLoginToken, ticket);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

    }
}
