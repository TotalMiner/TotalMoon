using System;
using System.IO;
using System.Reflection;
using MoonSharp.Interpreter;
using StudioForge.Engine.Core;
using StudioForge.TotalMiner;
using StudioForge.TotalMiner.API;
using TotalMoon.Lua;

namespace TotalMoon
{
    public class TotalMoonMod : ITMPlugin
    {
        public static ITMGame Game;

        private bool _UserDataRegistered;
        
        private string _ModPath;
        private string _ScriptPath;
        
        public void Initialize(ITMPluginManager mgr, string path)
        {
            if (!_UserDataRegistered)
            {
                UserData.RegisterAssembly();
                _UserDataRegistered = true;
            }

            _ModPath = Path.Combine(FileSystem.RootPath + path);
            _ScriptPath = Path.Combine(_ModPath, "Scripts");
            
            if (!Directory.Exists(_ScriptPath))
            {
                Directory.CreateDirectory(_ScriptPath);
            }
        }

        public void InitializeGame(ITMGame game)
        {
            Game = game;
            Logger.Logged += Logged;

            foreach (string script in Directory.GetFiles(_ScriptPath, "*.lua", SearchOption.AllDirectories))
            {
                Script _s = new Script();
                _s.Globals["os"] = new OSAPI();
                _s.DoFile(script);
            }
        }

        private void Logged(LogType type, string msg)
        {
            File.AppendAllText("Log.txt", $"{msg}\n");
            Game.AddNotification(msg, NotifyRecipient.Global);
        }

        public void UnloadMod()
        {
            Logger.Logged -= Logged;
        }

        public bool HandleInput(ITMPlayer player)
        {
            return false;
        }

        public void Update()
        {
        }

        public void Update(ITMPlayer player)
        {
        }

        public void Draw(ITMPlayer player, ITMPlayer virtualPlayer)
        {
        }

        public void WorldSaved(int version)
        {
        }

        public void PlayerJoined(ITMPlayer player)
        {
        }

        public void PlayerLeft(ITMPlayer player)
        {
        }
    }
}