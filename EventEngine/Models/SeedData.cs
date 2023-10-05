using EventEngine.Data;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EventEngine.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EventEngineContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EventEngineContext>>()))
            {
                // Look for any movies.
                if (context.Event.Any())
                {
                    return;   // DB has been seeded
                }
                context.Event.AddRange(
                    new Event
                    {
                        Title = "Cosy Cafe",
                        Description = "A nice relaxing coffee",
                        Location = "La Cabra, Aarhus City Centre",
                        Cost = 7.99M,
                        IsIndoor = true,
                        Capacity = 8
                    },
                    
                    new Event 
                    {
                        Title = "Ice Skating",
                        Description = "Let's get frozen together!",
                        Location = "Skatehall, Aarhus North",
                        Cost = 0.00M,
                        IsIndoor = true,
                        Capacity = 20
                    },

                    new Event
                    {
                        Title = "Hike",
                        Description = "Join us for some fresh air",
                        Location = "Riiskov Forest",
                        Cost = 0.00M,
                        IsIndoor = false,
                        Capacity = 12
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
