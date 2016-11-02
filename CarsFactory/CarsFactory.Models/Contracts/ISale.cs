namespace CarsFactory.Models.Contracts
{
    public interface ISale
    {
        Car Car { get; set; }
        int Id { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
        SaleReport SaleReport { get; set; }
        decimal Sum { get; set; }
    }
}