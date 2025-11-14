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

        Console.WriteLine(File.Exists(scriptsPath));
    }
}