using MoonSharp.Interpreter;

namespace TotalMoon.Lua
{
    [MoonSharpUserData]
    public class OSAPI
    {
        public static void print(string message)
        {
            TotalMoonMod.Game.AddNotification(message);
            Logger.Info(message);
        }
    }
}