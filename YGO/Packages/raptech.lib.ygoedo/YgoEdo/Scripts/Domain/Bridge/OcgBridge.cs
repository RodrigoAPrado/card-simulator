using System;
using System.IO;
using System.Runtime.InteropServices;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Native;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;

namespace YgoSoul.RapTech.Lib.YgoEdo.Domain.Bridge
{
    public static class OcgBridge
    {
        private static string _scriptsPath;
        public static OCG_Delegates.DataReader CardReader { get; private set; }
        public static OCG_Delegates.ScriptReader ScriptReader { get; private set; }
        public static OCG_Delegates.LogHandler LogHandler { get; private set; }
        public static OCG_Delegates.DataReaderDone ReaderDone { get; private set; }

        public static bool Initialized { get; private set; }

        public static void Init(string scriptsPath)
        {
            if (Initialized)
                return;
            Initialized = true;
            _scriptsPath = scriptsPath;

            CardReader = OnCardRead;
            ScriptReader = OnScriptLoad;
            LogHandler = OnLogReceived;
            ReaderDone = OnReaderDone;
        }

        public static int LoadScript(IntPtr pDuel, string name)
        {
            return OnScriptLoad(IntPtr.Zero, pDuel, name);
        }

        private static int OnScriptLoad(IntPtr payload, IntPtr pDuel, string name)
        {
            var normalizedName = name.Replace("\\", "/");

            if (!normalizedName.StartsWith("script/"))
                normalizedName = "script/" + normalizedName;

            var fileName = Path.GetFileName(normalizedName);
            var path = Path.Combine(_scriptsPath + "scripts", fileName);

            if (!File.Exists(path))
            {
                Console.WriteLine($"[ERROR] Script not found: {normalizedName}");
                return 0;
            }

            var content = File.ReadAllBytes(path);

            Console.WriteLine($"[LOAD] {normalizedName}");

            return OCG_Api.Setup.OCG_LoadScript(
                pDuel,
                content,
                (uint)content.Length,
                normalizedName
            );
        }

        private static void OnCardRead(IntPtr payload, uint code, IntPtr data)
        {
            var cardInfo = CardDatabase.GetCardData(code); // Seu método que busca no SQLite
            Marshal.StructureToPtr(cardInfo, data, false);
        }

        private static void OnLogReceived(IntPtr payload, string message, int type)
        {
            // O tipo geralmente indica se é erro (2), aviso (1) ou info (0)
            Console.WriteLine($"[OCGCORE TYPE {type}]: {message}");
        }

        private static void OnReaderDone(IntPtr payload, IntPtr data)
        {
        }
    }
}