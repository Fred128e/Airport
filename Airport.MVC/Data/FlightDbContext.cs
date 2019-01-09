using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport.MVC.Data
{
    public class FlightDbContext:DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FlightViewModel> Flight { get; set; }
    }
}
