using System.ComponentModel.DataAnnotations;

public class DeliveryOrderItem
{
    [Key]
    public int DeliveryOrderItemId { get; set; }

    [Required(ErrorMessage = "Delivery Order ID is required.")]
    public int DeliveryOrderId { get; set; }

    [Required(ErrorMessage = "Item ID is required.")]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    public DeliveryOrder? DeliveryOrder { get; set; }
    public Item? Item { get; set; }
}
