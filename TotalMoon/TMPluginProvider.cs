using System;
using System.Reflection;
using StudioForge.TotalMiner.API;

namespace TotalMoon
{
    public class TMPluginProvider : ITMPluginProvider
    {
        public TMPluginProvider()
        {
            EmbeddedAssembly.Load("TotalMoon.Assemblies.MoonSharp.Interpreter.dll", "MoonSharp.Interpreter.dll");
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }
        
        private Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
        
        public ITMPlugin GetPlugin()
        {   
            return new TotalMoonMod();
        }

        public ITMPluginGUI GetPluginGUI()
        {
            return null;
        }

        public ITMPluginBlocks GetPluginBlocks()
        {
            return null;
        }

        public ITMPluginArcade GetPluginArcade()
        {
            return null;
        }

        public ITMPluginNet GetPluginNet()
        {
            return null;
        }
    }
}