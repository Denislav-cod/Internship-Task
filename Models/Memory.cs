namespace ConsoleApp2;

public class Memory : IItemProperties
{
    public string ComponentType { get; set; }
    public string PartNumber { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return ComponentType + " - " + Name + " - " + Type;
    }
}