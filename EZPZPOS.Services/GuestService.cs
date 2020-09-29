﻿using EZPZPOS.Data;
using EZPZPOS.Models.GuestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EZPZPOS.Services
{
    public class GuestService
    {
        private readonly string _userId;

        public GuestService(string userId)
        {
            _userId = userId;
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
                    Notes = model.Notes
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
                        .Where(e => e.ServerId == _userId)
                        .Select(
                            e =>
                                new GuestListItem
                                {
                                    GuestId = e.GuestId,
                                    ServerId = e.ServerId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    ContactNumber = e.ContactNumber,
                                    // May need to add a CreatedUtc here and to data and model
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
                        .Single(e => e.GuestId == id && e.ServerId == _userId);
                return
                    new GuestDetail
                    {
                        GuestId = entity.GuestId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        ContactNumber = entity.ContactNumber,
                        Notes = entity.Notes,
                        LastVisitUtc = entity.LastVisitUtc
                    };
            }
        }



    }
}
