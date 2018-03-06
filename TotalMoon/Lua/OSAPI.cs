using MoonSharp.Interpreter;
using StudioForge.Engine.Integration;

namespace TotalMoon.Lua
{
    [MoonSharpUserData]
    public class OSAPI
    {
        private IOutputLog log;

        public OSAPI()
        {
            
        }
        
        public OSAPI(IOutputLog log)
        {
            this.log = log;
        }
        
        public void print(string message)
        {
            log?.WriteLine(message);
            Logger.Info(message);
        }
    }
}