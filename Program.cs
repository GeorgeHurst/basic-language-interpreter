using Basic_Language_Interpreter;

class Program
{
    static void Main()
    {
        Console.WriteLine("Basic Language Interpreter. Type EXIT to exit.");

        while (true)
        {
            Console.Write("> ");
            string line = Console.ReadLine()?.Trim() ?? "";

            if (line.ToUpper() == "EXIT") break;
            if (line.ToUpper() == "CLEAR")
            {
                Console.Clear();
                Console.WriteLine("Basic Language Interpreter. Type EXIT to exit");
                continue;
            }
            if (line == "") continue;

            Execute(line);

        }
    }

    // Function to decode the line and determine what command needs to be ran. 
    // contains fat switch statement linking to Commands class.
    public static void Execute(string line)
    {
        string[] tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string command = tokens[0].ToUpper();

        switch (command)
        {
            case "PRINT":
                Commands.Print(tokens);
                break;

            case "SET":
                Commands.Set(tokens);
                break;

            case "ADD":
                Commands.Add(tokens);
                break;

            case "SUBTRACT":
                Commands.Subtract(tokens);
                break;

            case "SLEEP":
                Commands.Add(tokens);
                break;
        }
    }
}