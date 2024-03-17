using ASP_66Bit_Test.Data;
using ASP_66Bit_Test.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ASP_66Bit_Test.Models
{
    public class FootballerViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Длина имени должна составлять 2-50 символов", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, ErrorMessage = "Длина фамилии должна составлять 2-50 символов", MinimumLength = 2)]
        public string? Family { get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        public string? Gender { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateOnly? BirthDate { get; set; } = default!;

        public bool NewTeamCreate { get; set; }
        public Guid? TeamId { get; set; }
        public string? NewTeam { get; set; }

        [Required(ErrorMessage = "Выберите страну")]
        public Guid? CountryId { get; set; }

        public bool IsValid(AppDBContext context, ModelStateDictionary modelState)
        {
            var result = true;

            if (NewTeamCreate)
            {
                if (string.IsNullOrEmpty(NewTeam))
                {
                    modelState.AddModelError("Team", $"Выберите или введите команду.");
                    result = false;
                }
                else if (context.Teams.Any(t => t.Name == NewTeam))
                {
                    modelState.AddModelError("Team", $"Введеная команда уже существует, выберите её из списка.");
                    result = false;
                }
            }

            if (!NewTeamCreate)
            {
                if (TeamId == null)
                {
                    modelState.AddModelError("Team", $"Выберите или введите команду.");
                    result = false;
                }
                else if (!context.Teams.Any(t => t.Id == TeamId))
                {
                    modelState.AddModelError("Team", $"Выбранная команда не найдена.");
                    result = false;
                }
            }

            return result;
        }
    }
}
