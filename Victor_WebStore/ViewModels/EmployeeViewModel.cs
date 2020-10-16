using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [Display(Name =  "Имя")]
        public string Name { get; set; }
        [Range(18,100,ErrorMessage ="Возраст работника от 18 до 100")]
        public int Age { get; set; }
    }
}
