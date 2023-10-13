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
                serviceProvider.GetRequiredService<DbContextOptions<EventEngineContext>>()))
            {
                // Seed categories first
                SeedCategories(context);

                // Seed events after categories
                SeedEvents(context);
            }
        }

        private static void SeedCategories(EventEngineContext context)
        {
            if (context.Category.Any())
            {
                return; // Categories have already been seeded
            }

            context.Category.AddRange(
                new Category { Name = "Sport" },
                new Category { Name = "Creative" },
                new Category { Name = "Food and Drink" },
                new Category { Name = "Dance" }
            );

            context.SaveChanges();
        }
        private static void SeedEvents(EventEngineContext context)
        {
            if (context.Event.Any())
            {
                return; // Events have already been seeded
            }

            var sportCategory = context.Category.FirstOrDefault(c => c.Name == "Sport");
            var creativeCategory = context.Category.FirstOrDefault(c => c.Name == "Creative");
            var foodCategory = context.Category.FirstOrDefault(c => c.Name == "Food and Drink");
            var danceCategory = context.Category.FirstOrDefault(c => c.Name == "Dance");

            context.Event.AddRange(
                 new Event
                 {
                     Title = "Cosy Cafe",
                     Description = "Enjoy a nice relaxing coffee and catch up",
                     Location = "La Cabra, Aarhus City Centre",
                     Cost = 7.99M,
                     IsIndoor = true,
                     Capacity = 8,
                     Category = foodCategory
                 },

                    new Event
                    {
                        Title = "Ice Skating",
                        Description = "Let's get frozen together!",
                        Location = "Skatehall, Aarhus North",
                        Cost = 0.00M,
                        IsIndoor = true,
                        Capacity = 20,
                        Category = sportCategory
                    },

                    new Event
                    {
                        Title = "Hike",
                        Description = "Join us for some fresh air",
                        Location = "Riiskov Forest",
                        Cost = 0.00M,
                        IsIndoor = false,
                        Capacity = 12,
                        Category = sportCategory
                    },

                    new Event
                    {
                        Title = "Paint 'n' Sip",
                        Description = "Get creative while enjoying a glass of wine",
                        Location = "Vin Danmark",
                        Cost = 40.00M,
                        IsIndoor = true,
                        Capacity = 25,
                        Category = creativeCategory
                    },

                    new Event
                    {
                        Title = "Bouldering",
                        Description = "Challenge yourself mentally and physically - not a fan of heights? Maybe skip this one",
                        Location = "Aarhus Boulders",
                        Cost = 15.00M,
                        IsIndoor = true,
                        Capacity = 10,
                        Category = sportCategory
                    },

                    new Event
                    {
                        Title = "Ceramic Painting",
                        Description = "Decorate your own piece of pottery",
                        Location = "Aart Studio",
                        Cost = 60.00M,
                        IsIndoor = true,
                        Capacity = 30,
                        Category = creativeCategory
                    },

                    new Event
                    {
                        Title = "Delicious Dinner",
                        Description = "Share some tasty dishes from around the world",
                        Location = "Aarhus Streetfood",
                        Cost = 10.00M,
                        IsIndoor = true,
                        Capacity = 16,
                        Category = foodCategory
                    },

                    new Event
                    {
                        Title = "Burlesque",
                        Description = "Gain some confidence with a burlesque dance class",
                        Location = "Shake it Studio",
                        Cost = 25.00M,
                        IsIndoor = true,
                        Capacity = 12,
                        Category = danceCategory
                    },

                    new Event
                    {
                        Title = "Yoga in the Park",
                        Description = "Bring your own mat and join us for some stretching",
                        Location = "Marselisborg Park",
                        Cost = 0.00M,
                        IsIndoor = false,
                        Capacity = 20,
                        Category = sportCategory
                    },

                    new Event
                    {
                        Title = "Salsa Dancing",
                        Description = "Learn some sexy moves at salsa dancing",
                        Location = "Shake it Studio",
                        Cost = 15.00M,
                        IsIndoor = true,
                        Capacity = 15,
                        Category = danceCategory
                    },

                    new Event
                    {
                        Title = "Game Night",
                        Description = "Get competitive with some new board games",
                        Location = "Boardgame Cafe",
                        Cost = 5.00M,
                        IsIndoor = true,
                        Capacity = 30,
                        Category = creativeCategory
                    }
            );

            context.SaveChanges();
        }
    }
}
