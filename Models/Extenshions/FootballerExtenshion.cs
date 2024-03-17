using ASP_66Bit_Test.Data;
using ASP_66Bit_Test.Entities;

namespace ASP_66Bit_Test.Models.Extenshions
{
    public static class FootballerExtenshion
    {
        public static Footballer? FootballerViewModelToFootballer(this FootballerViewModel f, AppDBContext context)
        {
            var team = f.NewTeamCreate ?
                       new Team() { Id = Guid.NewGuid(), Name = f.NewTeam } :
                       context.Teams.FirstOrDefault(t => t.Id == f.TeamId);
            if (team == null)
                return null;
            if (f.NewTeamCreate) context.Teams.AddAsync(team);

            var country = context.Countries.FirstOrDefault(t => t.Id == f.CountryId);
            if (country == null)
                return null;

            return new Footballer()
            {
                Id = f.Id,
                Name = f.Name,
                Family = f.Family,
                BirthDate = f.BirthDate.Value,
                Gender = f.Gender,
                Team = team,
                Country = country
            };
        }

        public static FootballerViewModel FootballerToFootballerViewModel(this Footballer f)
        {
            return new FootballerViewModel()
            {
                Id = f.Id,
                Name = f.Name,
                Family = f.Family,
                BirthDate = f.BirthDate,
                Gender = f.Gender,
                TeamId = f.Team.Id,
                CountryId = f.Country.Id
            };
        }
    }
}
