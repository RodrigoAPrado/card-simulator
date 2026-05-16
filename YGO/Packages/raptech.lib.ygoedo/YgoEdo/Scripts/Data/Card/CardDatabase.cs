using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Data.Sqlite;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;

namespace YgoSoul.RapTech.Lib.YgoEdo.Data.Card
{
    public class CardDatabase
    {
        private static string _connString;

        public static void Init(string dbPath)
        {
            _connString = $"Data Source={dbPath}";
        }

        public static void LoadCards()
        {
            using var connection = new SqliteConnection(_connString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM datas JOIN texts ON datas.id = texts.id";
            using var reader = command.ExecuteReader();
            var cardsLoaded = 0;
            while (reader.Read())
            {
                var setCodeValue = (ulong)reader.GetInt64(3);

                var setcodePtr = Marshal.AllocHGlobal(sizeof(ulong));
                Marshal.WriteInt64(setcodePtr, (long)setCodeValue);
                var rawLevel = (uint)reader.GetInt32(7);
                var type = (uint)reader.GetInt32(4);
                var isLink = (type & (uint)OCG_CardType.Link) != 0;
                var rawDefense = (uint)reader.GetInt32(6);
                var ocgCardData = new OCG_CardData
                {
                    code = (uint)reader.GetInt32(0),
                    alias = (uint)reader.GetInt32(2),
                    setcode = setcodePtr,
                    type = type,
                    attack = reader.GetInt32(5),
                    defense = isLink ? 0 : (int)rawDefense,
                    level = rawLevel & 0xFF,
                    race = (ulong)reader.GetInt64(8),
                    attribute = (uint)reader.GetInt32(9),
                    lscale = (rawLevel >> 24) & 0xFF,
                    rscale = (rawLevel >> 16) & 0xFF,
                    link_marker = isLink ? rawDefense : 0
                };

                var strings = new List<string>();

                for (var i = 1; i <= 16; i++) strings.Add(reader.GetString(13 + i));

                CardLibrary.AddCard(ocgCardData, reader.GetString(12), reader.GetString(13), strings,
                    (ulong)reader.GetInt64(10));
                cardsLoaded++;
            }

            Console.WriteLine($"{cardsLoaded} cards loaded...");
            connection.Close();
        }

        public static OCG_CardData GetCardData(uint code)
        {
            var card = CardLibrary.InternalGetCard(code);
            return card.Data;
        }
    }
}