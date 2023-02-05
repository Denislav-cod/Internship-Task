namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            string inp = " ";
            while(inp != "exit")
            {
                Console.WriteLine("\nType `exit` to close the program");
                Console.WriteLine(store.showInfo());
                Console.Write("Please enter part number(s): ");
                string input = Console.ReadLine();
                inp = input.ToLower();
                string[] inputs = input.Split(',');
                if (inputs.Length > 1)
                {
                    inp = input.ToLower();
                    Console.WriteLine(store.showComposition(inputs));
                }else if (inputs.Length == 1)
                {
                    inp = input.ToLower();
                    Console.WriteLine(store.showCompatibles(input));
                }
            };

        }
    }
}