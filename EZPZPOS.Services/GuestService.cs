using EZPZPOS.Data;
using EZPZPOS.Models.GuestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Services
{
    public class GuestService
    {
        private readonly string _userId;
        //private readonly ApplicationDbContext _db = new ApplicationDbContext(); for n-tier don't do this, using using statements

        public GuestService(string userId)
        {
            _userId = userId;
        }

        //Trying this
        public IEnumerable<GuestListItem> GetGuestByFullName()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Guests.ToList()
                    .Select(
                        e => new GuestListItem()
                        {
                            GuestId = e.GuestId,
                            FirstName = e.FirstName,
                            LastName = e.LastName
                        }
                    );

                return query.ToArray();
            }
        }

        // POST -- Create Guest
        public bool CreateGuest(GuestCreate model)
        {
            var entity =
                new Guest()
                {
                    ServerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ContactNumber = model.ContactNumber,
                    FullAddress = model.FullAddress,
                    Notes = model.Notes,
                    FirstTime = true
                    // May need to add a CreatedUtc here and to data and model
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Guests.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // GET -- All Guests
        public IEnumerable<GuestListItem> GetGuests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Guests
                        //.Where(e => e.ServerId == _userId) for when I want to return the guests a single server creates
                        .Select(
                            e =>
                                new GuestListItem
                                {
                                    GuestId = e.GuestId,
                                    ServerId = e.ServerId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    ContactNumber = e.ContactNumber,
                                    FirstTime = e.FirstTime,
                                    // May need to add a CreatedUtc here and to data and model for a stretch goal
                                }
                        );

                return query.ToArray();
            }
        }

        // GET -- Guests by ID
        public GuestDetail GetGuestById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Guests
                        .Single(e => e.GuestId == id);
                return
                    new GuestDetail
                    {
                        GuestId = entity.GuestId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        ContactNumber = entity.ContactNumber,
                        Notes = entity.Notes
                    };
            }
        }

        // Update -- Guest
        public bool UpdateGuest(GuestEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Guests
                        .Single(e => e.GuestId == model.GuestId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.ContactNumber = model.ContactNumber;
                entity.Notes = model.Notes;
       
                return ctx.SaveChanges() == 1;
            }
        }

        // Delete -- Guest
        public bool DeleteGuest(int guestId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Guests
                        .Single(e => e.GuestId == guestId && e.ServerId == _userId);

                ctx.Guests.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
