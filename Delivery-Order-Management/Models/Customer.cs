using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [MinLength(5, ErrorMessage = "Address must be at least 5 characters long.")]
    [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [MinLength(7, ErrorMessage = "Phone number must be at least 7 digits.")]
    [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 digits.")]
    public required string Phone { get; set; }
}
