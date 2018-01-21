using Microsoft.AspNetCore.Mvc;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeddingWebsiteApi.Configuration;

namespace WeddingWebsiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RSVPController
    {
        private readonly IDatabase _database;
        public RSVPController(IDatabaseContainer container)
        {
            _database = container.Database;
        }
        [HttpGet]
        public IList<RSVP> GetRSVPs()
        {
            return _database.FetchOneToMany<RSVP>(rsvp => (List<Guest>)rsvp.Guests,
                "select rsvp.*, guest.* from rsvp left outer join guest on rsvp.id = guest.rsvp_id");
        }
        [HttpGet]
        public RSVP GetRSVPById(int id)
        {
            return GetRSVPs().FirstOrDefault(m => m.Id == id);
        }

        [HttpGet]
        public HttpResponseMessage ResetUserResponseStatus(int id)
        {
            bool isSuccessfulSubmission = false;

            _database.BeginTransaction();
            try
            {
                isSuccessfulSubmission = UpdateUserResponseStatus(id, false);
                _database.CompleteTransaction();
            } catch (Exception e)
            {
                _database.AbortTransaction();
            }

            var response = new HttpResponseMessage();
            response.StatusCode = isSuccessfulSubmission ? HttpStatusCode.Created : HttpStatusCode.NotModified;
            return response;
        }

        [HttpDelete]
        public HttpResponseMessage RemoveGuest([FromBody] DeleteFilter filter)
        {
            bool isSuccessfulDelete = false;

            _database.BeginTransaction();
            try
            {
                var rsvp = GetRSVPById(filter.RsvpId);
                var guestToDelete = rsvp.Guests.FirstOrDefault(m => m.GuestRsvpId == filter.GuestId);
                if (guestToDelete != null)
                {
                    _database.Delete(guestToDelete);
                    isSuccessfulDelete = true;
                }
                _database.CompleteTransaction();
                
            } catch (Exception e)
            {
                _database.AbortTransaction();
            }

            return GetResponse(isSuccessfulDelete);
        }

        [HttpPost]
        public HttpResponseMessage SubmitRSVP([FromBody]RSVPSubmission submission)
        {
            bool isSuccessfulSubmission = false;

            if (submission.Guests == null)
                submission.Guests = new List<Guest>();

            _database.BeginTransaction();
            try
            {
                foreach (var guest in submission.Guests)
                {
                    guest.RSVPId = submission.Id;

                    if (!string.IsNullOrWhiteSpace(guest.FirstName) && !string.IsNullOrWhiteSpace(guest.LastName))
                    {
                        if (guest.IsToBeDeleted)
                            _database.Delete(guest);
                        else
                            _database.Save(guest);
                    }
                    //var createdGuest = _database.Query<Guest>()
                    //    .Where("guest_id_per_rsvp = @GuestRsvpId", new { guest.GuestRsvpId })
                    //    .Where("rsvp_id = @rsvpId", new { rsvpId = guest.RSVPId });
                    //if (createdGuest == null)
                    //    throw new ArgumentNullException($"Guest: RSVPId: {guest.RSVPId}, FirstName: {guest.FirstName}, LastName: {guest.LastName}, FoodAllergies: {guest.FoodAllergies}, Not created.");
                }

                isSuccessfulSubmission = UpdateUserResponseStatus(submission.Id, true);

                _database.CompleteTransaction();
                isSuccessfulSubmission = true;
            }
            catch (Exception e)
            {
                _database.AbortTransaction();
            }

            return GetResponse(isSuccessfulSubmission);
            
        }

        private HttpResponseMessage GetResponse(bool isSuccessful)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = isSuccessful ? HttpStatusCode.Created : HttpStatusCode.NotModified;
            return response;
        }
        private bool UpdateUserResponseStatus(int id, bool hasResponded)
        {
            var rsvpRecord = _database.SingleById<RSVP>(id);
            rsvpRecord.HasResponded = hasResponded;
            return _database.Update(rsvpRecord) > 0;
        }
    }
    public class RSVPSubmission {
        public int Id { get; set; }
        public IList<Guest> Guests { get; set; }
    }
    public class DeleteFilter
    {
        public int RsvpId { get; set; }
        public int? GuestId { get; set; }
    }

    [TableName("guest")]
    [PrimaryKey(new string[]{"rsvp_id", "guest_id_per_rsvp" })]
    public class Guest
    {
        [Column("rsvp_id")]
        public int RSVPId { get; set; }
        [Column("guest_id_per_rsvp")]
        public int GuestRsvpId { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("food_allergies")]
        public string FoodAllergies { get; set; }
        [Ignore]
        public bool IsToBeDeleted { get; set; }
    }
    [TableName("rsvp")]
    [PrimaryKey("id")]
    public class RSVP
    {

        [Column("id")]
        public int Id { get; set; }
        [Column("reference_name")]
        public string ReferenceName { get; set; }
        [Column("number_of_rsvps")]
        public int NumberOfGuests { get; set; }
        [Column("has_responded")]
        public bool HasResponded { get; set; }
        
        public IList<Guest> Guests {get;set;}
    }
}
