using System.ComponentModel.DataAnnotations;

namespace Project_QLNCC.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}