using System.Text;
using System.Text.Json;
using System.Linq;

namespace ConsoleApp2;

public class Store
{
    private readonly string _path = @"C:\Users\denis\RiderProjects\ConsoleApp2\ConsoleApp2\pc-store-inventory.json";

    //Root object
    private readonly Component _component;

    //Loads json data in _component property on initialize.
    public Store()
    {
        _component = JsonSerializer.Deserialize<Component>(
            File.ReadAllText(_path));
    }

    /*
     * This method show all possible compositions with the provided Item's Part Number.
     * Throw exception message if one of the components is null.
     */
    public string showCompatibles(string partNumber)
    {
        try
        {
            CPU cpu = null;
            Memory memory = null;
            Motherboard motherboard = null;
            if (cpu == null)
            {
                cpu = _component.CPUs.Find(cp => cp.PartNumber.Equals(partNumber));
            }

            if (memory == null)
            {
                memory = _component.Memory.Find(mem => mem.PartNumber.Equals(partNumber));
            }

            if (motherboard == null)
            {
                motherboard = _component.Motherboards
                    .Find(mb => mb.PartNumber.Equals(partNumber));
            }
            
            if (_component.CPUs.Contains(cpu))
            {
                return search(cpu);
            }
            else if (_component.Memory.Contains(memory))
            {
                return search(memory);
            }
            else if (_component.Motherboards.Contains(motherboard))
            {
                return search(motherboard);
            }
            else
            {
                throw new PartNumberNotFoundException("ERROR: This part number is not valid");
            }
        }
        catch (PartNumberNotFoundException e)
        {
            return e.Message;
        }
    }

    /*
     * Show useful information about the Components.
     */
    public string showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("CPU: ");
        foreach (var cpu in _component.CPUs)
        {
            sb.Append(cpu.PartNumber + "/" + cpu.SupportedMemory + "/" + cpu.Socket + "  ");
        }

        sb.AppendLine("\n");
        sb.AppendLine("Memories: ");
        foreach (var memory in _component.Memory)
        {
            sb.Append(memory.PartNumber + "/" + memory.Type + "  ");
        }

        sb.AppendLine("\n");
        sb.AppendLine("Motherboards: ");
        foreach (var motherboard in _component.Motherboards)
        {
            sb.Append(motherboard.PartNumber + "/" + motherboard.Socket + "  ");
        }

        sb.AppendLine("\n");

        return sb.ToString();
    }

    /*
     * With given array of Part Numbers search for items and show the composition
     * if there is not provided right Part Number it will throw exception message.
     */
    public string showComposition(string[] partNumbers)
    {
        try
        {
            CPU? cpu = null;
            Memory? memory = null;
            Motherboard? motherboard = null;

            foreach (var partNumber in partNumbers)
            {
                string trimed = partNumber.Trim();
                if (cpu == null)
                {
                    cpu = _component.CPUs.Find(cp => cp.PartNumber.Equals(trimed));
                }

                if (memory == null)
                {
                    memory = _component.Memory.Find(mem => mem.PartNumber.Equals(trimed));
                }

                if (motherboard == null)
                {
                    motherboard = _component.Motherboards
                        .Find(mb => mb.PartNumber.Equals(trimed));
                }
            }

            if (cpu == null || memory == null || motherboard == null)
            {
                throw new InvalidCompositionException("ERROR: Please choose different component types");
            }

            if (cpu.Socket != motherboard.Socket)
            {
                throw new NotCompatibleException("Motherboard with socket " + motherboard.Socket +
                                                 " is not compatible with the CPU");
            }

            if (cpu.SupportedMemory != memory.Type)
            {
                throw new NotCompatibleException("Motherboard with type " + memory.Type +
                                                 " is not compatible with the CPU");
            }


            return cpu.ToString() + "\n" + memory.ToString() + "\n" + motherboard.ToString() + "\n" + "Price: " +
                   calculatePrice(cpu.Price, memory.Price, motherboard.Price)
                   + "\n";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /*
     * Receive items price as argument.
     * Calculate total price of all provided components price.
     * return double.
     */
    private double calculatePrice(double cpuPrice, double memPrice, double mbPrice)
    {
        return cpuPrice + memPrice + mbPrice;
    }

    //Overload the function search

    /*
     * Search for equality between cpu-memory and cpu-motherboard and return all combinations.
     * return string.
     */
    private string search(CPU cpu)
    {
        StringBuilder sb = new StringBuilder();

       var memory = _component.Memory.FindAll(mem => cpu.SupportedMemory.Equals(mem.Type)).ToList();
       var motherboard = _component.Motherboards.FindAll(mb 
           => cpu.Socket.Equals(mb.Socket)).ToList();
       
        int comb = 0;
        int num = 0;
        foreach (var mem in memory)
        {
            foreach (var mb in motherboard)
            {
                num++;
                sb.AppendLine("Combination " + num + " ");
                sb.AppendLine(cpu.ToString() + " ");
                sb.AppendLine(mb.ToString() + " ");
                sb.AppendLine(mem.ToString());
                sb.AppendLine("Price: " + calculatePrice(cpu.Price, mem.Price, mb.Price) + "\n");
                comb++;
            }
        }

        return "There are " + comb + " possible combinations:\n" + sb.ToString();
    }

    private string search(Memory memory)
    {
        List<Motherboard> motherboard = new List<Motherboard>();
        StringBuilder sb = new StringBuilder();
        
        var cpu = _component.CPUs.FindAll(cp => cp.SupportedMemory.Equals(memory.Type)).ToList();
        foreach (var cp in cpu)
        {
            motherboard = _component.Motherboards.FindAll(mb 
                => cp.Socket.Equals(mb.Socket)).ToList();
        }

        int comb = 0;
        int num = 0;
        foreach (var cp in cpu)
        {
            foreach (var mb in motherboard)
            {
                if (cp.Socket.Equals(mb.Socket))
                {
                    num++;
                    sb.AppendLine("Combination " + num + " ");
                    sb.AppendLine(cp.ToString() + " ");
                    sb.AppendLine(mb.ToString() + " ");
                    sb.AppendLine(memory.ToString());
                    sb.AppendLine("Price: " + calculatePrice(cp.Price, memory.Price, mb.Price) + "\n");
                    comb++;
                }
            }
        }

        return "There are " + comb + " possible combinations:\n" + sb.ToString();
    }

    private string search(Motherboard motherboard)
    {
        List<Memory> memory = new List<Memory>();
        StringBuilder sb = new StringBuilder();
        
        var cpu = _component.CPUs.FindAll(cp => cp.Socket.Equals(motherboard.Socket)).ToList();

        foreach (var cp in cpu)
        {
            memory = _component.Memory.FindAll(mem => cp.SupportedMemory.Equals(mem.Type)).ToList();
        }

        int comb = 0;
        int num = 0;
        foreach (var cp in cpu)
        {
            foreach (var mem in memory)
            {
                if (cp.SupportedMemory.Equals(mem.Type))
                {
                    num++;
                    sb.AppendLine("Combination " + num + " ");
                    sb.AppendLine(cp.ToString() + " ");
                    sb.AppendLine(motherboard.ToString() + " ");
                    sb.AppendLine(mem.ToString());
                    sb.AppendLine("Price: " + calculatePrice(cp.Price, mem.Price, motherboard.Price) + "\n");
                    comb++;
                }
            }
        }

        return "There are " + comb + " possible combinations:\n" + sb.ToString();
    }
}