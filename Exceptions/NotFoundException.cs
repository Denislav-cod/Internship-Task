namespace ConsoleApp2;

public class PartNumberNotFoundException : Exception
{ 
    public PartNumberNotFoundException(string message) : base(message)
    {
    }
    
}