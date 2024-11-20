using System;

namespace RiskOfTerraria.Content
{
    public class Debug
    {
        public static void WriteLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Debug/RiskOfTerraria] {text}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
