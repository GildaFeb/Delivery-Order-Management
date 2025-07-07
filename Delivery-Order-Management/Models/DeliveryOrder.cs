using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DeliveryOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DeliveryOrderId { get; set; }

    [Required(ErrorMessage = "Order number is required.")]
    [MinLength(3, ErrorMessage = "Order number must be at least 3 characters.")]
    [MaxLength(50, ErrorMessage = "Order number cannot exceed 50 characters.")]
    public required string OrderNumber { get; set; }

    [Required(ErrorMessage = "Order date is required.")]
    public required DateTime OrderDate { get; set; }

    [Required(ErrorMessage = "Delivery date is required.")]
    public required DateTime DeliveryDate { get; set; }

    [Required(ErrorMessage = "Customer is required.")]
    public required int CustomerId { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression("^(PENDING|READY TO SHIP|DELIVERED)$", ErrorMessage = "Status must be PENDING, READY TO SHIP, or DELIVERED.")]
    public required string Status { get; set; }

    [Required(ErrorMessage = "Delivery timing is required.")]
    [RegularExpression("^(AM|PM|ANY)$", ErrorMessage = "Delivery timing must be AM, PM, or ANY.")]
    public required string DeliveryTiming { get; set; }
    public Customer? Customer { get; set; }

    public ICollection<DeliveryOrderItem>? OrderItems { get; set; }
}
