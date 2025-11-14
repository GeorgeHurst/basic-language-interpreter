using Basic_Language_Interpreter;

class Program
{

    public static string[] validCommands =
    {
        "SET", "PRINT", "PRINTLINE", "SLEEP", "START", "END", "ADD", "SUBTRACT"
    };

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

            case "PRINTLINE":
                Commands.PrintLine(tokens);
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
                Commands.Sleep(tokens);
                break;

            case "HELP":
                Commands.Help(tokens);
                break;

            case "START":
                break;

            case "END":
                break;

            case "SCRIPT":
                Commands.RunScript(tokens);
                break;

            default:
                Console.WriteLine($"<ERROR> {command} is an invalid command. Type HELP if needed.");
                break;
        }
    }

    public static void ExecuteScript(bool isRelative, string path)
    {
        string scriptsPath;
        if (isRelative)
        {
            scriptsPath = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, @"..\..\..\scripts\")
            ) + path + ".bli";
        }
        else
        {
            scriptsPath = path;
        }

        if (!File.Exists(scriptsPath)) return;

        using (StreamReader sr = new StreamReader(scriptsPath))
        {
            bool startFound = false;
            bool endFound = false;

            string? line;

            int lineNumber = 0;
            while ((line = sr.ReadLine()) != null)
            {
                ++lineNumber;

                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith('#')) continue;

                string[] tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0].ToUpper();

                if (!validCommands.Contains(command))
                {
                    Console.WriteLine($"<ERROR> Line {lineNumber}: '{command}' is not a valid command.");
                    return;
                }

                if (command == "START") startFound = true;
                if (command == "END")   endFound = true;

            }

            if (!startFound && !endFound)
            {
                Console.WriteLine("<ERROR> No program entry or exit point located. Use START and END in your program.");
                return;
            }
            else if (!startFound)
            {
                Console.WriteLine("<ERROR> No program entry point located. Use START in your program.");
                return;
            }
            else if (!endFound)
            {
                Console.WriteLine("<ERROR> No program exit point located. Use END in your program.");
                return;
            }
        }

        bool isStarted = false;

        using (StreamReader sr = new StreamReader(scriptsPath))
        {
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith('#')) continue;

                if (line == "START") isStarted = true;
                if (line == "END") isStarted = false;

                if (isStarted) Execute(line);
            }
        }

    }
}