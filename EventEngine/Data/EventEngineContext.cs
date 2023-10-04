using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventEngine.Models;

namespace EventEngine.Data
{
    public class EventEngineContext : DbContext
    {
        public EventEngineContext (DbContextOptions<EventEngineContext> options)
            : base(options)
        {
        }

        public DbSet<EventEngine.Models.Event> Event { get; set; } = default!;
    }
}
