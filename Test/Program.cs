using Jabukufo.Audio.Structures.XMA;
using System;
using System.Diagnostics;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoleListener);
            Console.WindowWidth  = (int)(Console.LargestWindowWidth * 0.75f);
            Console.WindowHeight = (int)(Console.LargestWindowHeight * 0.75f);

            Console.WriteLine($"Enter the path to an XMA file to decode it.");
            while (true)
            {
                Console.Write(" >");
                var cmd = Console.ReadLine();
                switch (cmd.ToLower())
                {
                    case "exit":
                        Environment.Exit(0);
                        return;

                    case "clear":
                        Console.Clear();
                        break;

                    default:
                        Decode(cmd);
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void Decode(string path)
        {
            var baseColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"------------- Serializing Metadata -----------------------");
            Console.ForegroundColor = baseColor;

            var xmaData = File.ReadAllBytes(path);
            var xmaFile = new XMAFILE(xmaData);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"------------- MetaData Serializing Complete --------------");
        }
    }
}
