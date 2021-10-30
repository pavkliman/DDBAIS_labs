using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _5.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Display(Name = "Имя")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Дата регистрации")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Роль")]
        public string UserRole { get; set; }
        public CreateUserViewModel()
        {
            UserRole = "user";
            RegistrationDate = DateTime.Now.Date;
        }
    }
}
