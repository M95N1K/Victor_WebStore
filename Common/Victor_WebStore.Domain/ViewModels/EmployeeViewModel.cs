using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [Display(Name = "Имя")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Имя должно быть длиной более 1-го символа")]
        public string Name { get; set; }
        [Range(18, 100, ErrorMessage = "Возраст работника от 18 до 100")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}
