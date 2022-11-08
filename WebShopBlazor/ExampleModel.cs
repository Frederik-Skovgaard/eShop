using System.ComponentModel.DataAnnotations;

namespace BlazorApp1
{
    public class ExampleModel
    {
        [Required(ErrorMessage = "First name must be given"), StringLength(10, ErrorMessage = "First Name is too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must be given"), StringLength(10, ErrorMessage = "Last Name is too long")]
        public string LastName { get; set; }
    }
}
