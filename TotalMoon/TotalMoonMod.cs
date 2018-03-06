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
        }

        public void InitializeGame(ITMGame game)
        {
            Game = game;
            Logger.Logged += Logged;
            
            _ScriptPath = Path.Combine(Path.Combine(FileSystem.RootPath, game.World.WorldPath), "Scripts");
            
            if (!Directory.Exists(_ScriptPath))
            {
                Directory.CreateDirectory(_ScriptPath);
            }
            
            game.AddConsoleCommand((s, tmGame, arg3, arg4, arg5) =>
            {
                Script _s = new Script();
                _s.Globals["os"] = new OSAPI(arg5);
                try
                {
                    _s.DoString(s.Substring(3));
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                    arg5.WriteLine(e.ToString());
                }
            }, "lua", "Runs Lua.", "Runs Lua from the console.");
            
            game.AddConsoleCommand((s, tmGame, arg3, arg4, arg5) =>
            {
                string[] args = s.Split(' ');
                Script _s = new Script();
                _s.Globals["os"] = new OSAPI();
                try
                {
                    _s.DoFile(Path.Combine(_ScriptPath, args[1]));
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                    arg5.WriteLine(e.ToString());
                }
            }, "dolua", "Runs Lua from a file.", "Runs lua from a file globally.");
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