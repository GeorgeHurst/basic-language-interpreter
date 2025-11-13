using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Language_Interpreter
{
    public static class Commands
    {
        static Dictionary<string, string> variables = new();

        public static void Print(string[] tokens)
        {
            if (tokens.Length < 2)
            {
                Console.WriteLine("<ERROR> Too little arguments. Usage: PRINT <value>");
                return;
            }

            // In future this should check for "" but may be more complicated that initially thought.
            if (variables.ContainsKey(tokens[1]))
            {
                Console.WriteLine(variables[tokens[1]]);
            }
            else
            {
                Console.WriteLine(string.Join(" ", tokens, 1, tokens.Length - 1));
            }
        }

        public static void Exit()
        {
            Console.WriteLine("Exiting the interpreter.");
            Environment.Exit(0);
        }

        public static void Set(string[] tokens)
        {
            if (tokens.Length < 3)
            {
                Console.WriteLine("<ERROR> Too little arguments. Usage: SET <var> <value>");
                return;
            }

            string varname = tokens[1];
            variables[varname] = tokens[2];
        }

        public static void Sleep(string[] tokens)
        {
            if (tokens.Length < 2)
            {
                Console.WriteLine("<ERROR> Too little arguments. Usage: SLEEP <duration>");
                return;
            }

            int duration = Int32.Parse(tokens[1]) * 1000;
            Task.Delay(duration).Wait();
        }

        public static Tuple<int, int> VerifyArgsForMaths(string[] tokens)
        {
            bool isVar1 = variables.ContainsKey(tokens[1]);
            bool isVar2 = variables.ContainsKey(tokens[2]);

            int var1 = isVar1
                ? Int32.Parse(variables[tokens[1]])
                : Int32.Parse(tokens[1]);

            int var2 = isVar2
                ? Int32.Parse(variables[tokens[2]])
                : Int32.Parse(tokens[2]);

            return new Tuple<int, int>(var1, var2);
        }

        public static void Add(string[] tokens)
        {
            if (tokens.Length < 4)
            {
                Console.WriteLine("<ERROR> Too little arguments. Usage: ADD <var1> <var2> <resultvar>");
                return;
            }

            Tuple<int, int> vals = VerifyArgsForMaths(tokens);

            string varname = tokens[3];

            variables[varname] = (vals.Item1 + vals.Item2).ToString();
        }

        public static void Subtract(string[] tokens)
        {
            if (tokens.Length < 4)
            {
                Console.WriteLine("<ERROR> Too little arguments. Usage: SUBTRACT <var1> <var2> <resultvar>");
                return;
            }

            Tuple<int, int> vals = VerifyArgsForMaths(tokens);

            string varname = tokens[3];

            variables[varname] = (vals.Item1 - vals.Item2).ToString();
        }

        public static void Help(string[] tokens)
        {
            Console.WriteLine(
                "Available functions:" +
                "\n\tPRINT <value>" +
                "\n\tSET <varname> <value>" +
                "\n\tSLEEP <duration>" +
                "\n\tADD <var1> <var2> <resultvar>" +
                "\n\tSUBTRACT <var1> <var2> <resultvar>" +
                "\n\tEXIT" +
                "\n\tCLEAR" +
                "\nFurther information can be found in the documentation."
                );
        }
    }
}
