namespace ConsoleApp2;

public class CPU : IItemProperties
{
    
    public string ComponentType { get; set; }
    public string PartNumber { get; set; }
    public string Name { get; set; }
    public string SupportedMemory { get; set; }
    public string Socket { get; set; }
    public double Price { get; set; }
    
    //CPU - Intel® Core™ i9-12900K Processor –LGA1700, DDR5
    public override string ToString()
    {
        return ComponentType + " - " + Name + " -" + Socket + ", " + SupportedMemory ;
    }
}