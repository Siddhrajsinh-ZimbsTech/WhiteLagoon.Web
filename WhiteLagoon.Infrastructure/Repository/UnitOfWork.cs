﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IVillaRepository Villa {  get; private set; }
        public IApplicationUserRepository User { get; private set; }
        public IAmenityRepository Amenity { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IVillaNumberRepository VillaNumber {  get; private set; }

        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Villa = new VillaRepository(_context);
            User = new ApplicationUserRepository(_context);
            Booking = new BookingRepository(_context);
            Amenity = new AmenityRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
        }

        public void save()
        {
           _context.SaveChanges();
        }
    }
}
