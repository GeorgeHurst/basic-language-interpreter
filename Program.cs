using Basic_Language_Interpreter;

class Program
{

    public static string[] validCommands =
    {
        "SET", "PRINT", "SLEEP", "START", "END", "ADD", "SUBTRACT"
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

            if (line.ToUpper().StartsWith("SCRIPT"))
            {

                // move this to function vvv
                string[] tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (tokens[1].ToUpper() == "VIEW")
                {
                    string path = Path.GetFullPath(
                        Path.Combine(AppContext.BaseDirectory, @"..\..\..\scripts")
                    );
                    string[] scripts = Directory.GetFiles(path, "*.bli");

                    Console.WriteLine("The following LOCAL scripts can be ran:");
                    foreach (string longpath in scripts)
                    {
                        string script = Path.GetFileName(longpath);
                        Console.WriteLine("\t" + script);
                    }
                    continue;
                }


                bool isRelative = (tokens[1].ToUpper() == "LOCAL");

                int offset;
                if (isRelative)
                {
                    offset = 2;
                }
                else
                {
                    offset = 1;
                }

                ExecuteScript(isRelative, tokens[offset]);
            }
            else
            {
                Execute(line);
            }

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
                Commands.Sleep(tokens);
                break;

            case "HELP":
                Commands.Help(tokens);
                break;

            case "START":
                break;

            case "END":
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
            ) + path;
        }
        else
        {
            scriptsPath = path;
        }

        if (!File.Exists(scriptsPath)) return;

        using (StreamReader sr = new StreamReader(scriptsPath))
        {
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                string[] tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0].ToUpper();

                if (!validCommands.Contains(command))
                {
                    Console.WriteLine($"<ERROR> '{command}' is not a valid command.");
                    return;
                }

            }
        }

        bool isStarted = false;

        using (StreamReader sr = new StreamReader(scriptsPath))
        {
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;
                //if (!isStarted) continue;
                // need to add starting program

                if (line == "START") isStarted = true;
                if (line == "END") isStarted = false;

                if (isStarted) Execute(line);
            }
        }

    }
}