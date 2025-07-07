using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Item code is required.")]
    [MinLength(2, ErrorMessage = "Item code must be at least 2 characters long.")]
    [MaxLength(50, ErrorMessage = "Item code cannot exceed 50 characters.")]
    public required string ItemCode { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [MinLength(2, ErrorMessage = "Description must be at least 2 characters long.")]
    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Unit of measure is required.")]
    [RegularExpression("^(PCS|CTN|ROLL)$", ErrorMessage = "Unit of measure must be PCS, CTN, or ROLL.")]
    public required string UnitOfMeasure { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression("^(ACTIVE|INACTIVE)$", ErrorMessage = "Status must be ACTIVE or INACTIVE.")]
    public required string Status { get; set; }
}
