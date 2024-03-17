using ASP_66Bit_Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP_66Bit_Test.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                if (context == null || context.Footballers == null || context.Teams == null || context.Countries == null)
                    throw new ArgumentNullException("Null AppDBContext");

                CreateData(context);
            }
        }

        private static void CreateData(AppDBContext context)
        {
            if (context.Countries.Any() 
                || context.Teams.Any() 
                || context.Footballers.Any())
                return;

            var counties = CreateCountries();
            context.Countries.AddRange(counties.Values);
            context.SaveChanges();

            var teams = CreateTeams();
            context.Teams.AddRange(teams.Values);
            context.SaveChanges();

            var footballers = CreateFootballers(teams, counties);
            context.Footballers.AddRange(footballers);
            context.SaveChanges();
        }

        private static Dictionary<string, Country> CreateCountries()
        {
            return new Dictionary<string, Country>()
            {
                {"Россия", new Country()
                    {
                        Id = new Guid("CA761232-ED42-11CE-BACD-00AA0057B223"),
                        Name = "Россия"
                    }
                },
                {"США",  new Country()
                    {
                        Id = new Guid("CA761232-ED42-11CE-BACD-00AA0057B224"),
                        Name = "США"
                    }
                },
                {"Италия",  new Country()
                    {
                        Id = new Guid("CA761232-ED42-11CE-BACD-00AA0057B225"),
                        Name = "Италия"
                    }
                },
            };
        }

        private static Dictionary<string, Team> CreateTeams()
        {
            return new Dictionary<string, Team>()
            {
                {"Спартак", new Team()
                    {
                        Id = new Guid("1A761232-ED42-11CE-BACD-00AA0057B225"),
                        Name = "Спартак"
                    }
                },
                {"Кардифф",  new Team()
                    {
                        Id = new Guid("2A761232-ED42-11CE-BACD-00AA0057B225"),
                        Name = "Кардифф"
                    }
                },
                {"Наполи",  new Team()
                    {
                        Id = new Guid("3A761232-ED42-11CE-BACD-00AA0057B225"),
                        Name = "Наполи"
                    }
                },
            };
        }

        private static List<Footballer> CreateFootballers(Dictionary<string, Team> teams, Dictionary<string, Country> counties)
        {
            return new List<Footballer>()
            {
                new Footballer
                {
                    Name = "Александр",
                    Family = "Максименко",
                    Gender = "man",
                    BirthDate = new DateOnly(1998, 3, 19),
                    Team = teams["Спартак"],
                    Country = counties["Россия"]
                },
                new Footballer
                {
                    Name = "Итан",
                    Family = "Хорват",
                    Gender = "man",
                    BirthDate = new DateOnly(1999, 6, 9),
                    Team = teams["Кардифф"],
                    Country = counties["США"]
                },
                new Footballer
                {
                    Name = "Алекс",
                    Family = "Мерет",
                    Gender = "man",
                    BirthDate = new DateOnly(1997, 3, 22),
                    Team = teams["Наполи"],
                    Country = counties["Италия"]
                }
            };
        }
    }
}
