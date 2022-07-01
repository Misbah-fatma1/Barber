using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Barber.Models;

namespace Barber.Data
{
    public class BarberContext : DbContext
    {
        public BarberContext (DbContextOptions<BarberContext> options)
            : base(options)
        {
        }

        public DbSet<Barber.Models.Appoint>? Appoint { get; set; }
    }
}
