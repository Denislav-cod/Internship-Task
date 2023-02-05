namespace ConsoleApp2;

public class Motherboard : IItemProperties
{
    public string ComponentType { get; set; }
    public string PartNumber { get; set; }
    public string Name { get; set; }
    public string Socket { get; set; }
    public double Price { get; set; }
    
    // Motherboard - MSI MAG Z690 TORPEDO - LGA1700

    public override string ToString()
    {
        return ComponentType + " - " + Name + " - " + Socket;
    }
}