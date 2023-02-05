namespace ConsoleApp2;

public interface IItemProperties
{
    string ComponentType { get; set; }
    string PartNumber { get; set; }
    string Name { get; set; }
    double Price { get; set; }
}