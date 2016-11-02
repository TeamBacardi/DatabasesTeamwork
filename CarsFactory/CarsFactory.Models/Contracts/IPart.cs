namespace CarsFactory.Models.Contracts
{
    public interface IPart
    {
        Car Car { get; set; }
        int? CarId { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Weight { get; set; }
    }
}