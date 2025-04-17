using System.ComponentModel.DataAnnotations;

namespace Demp.PL.ViewModels
{
    public class DepartmentViewModel
    {

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name ="Creation Date")]
        public DateOnly CreationDate { get; set; }

    }
}
