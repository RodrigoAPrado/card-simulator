using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Ygo.Scripts.Cards;
using Ygo.Scripts.Cards.Enums;

namespace Ygo.Service
{
    public class CardLoaderService
    {
        private static readonly string CardDataPath 
            = Application.dataPath + "/Ygo/Data/Cards/";
        
        private CardDatabase CardDatabase { get; set; }

        public void LoadCards()
        {
            var files = Directory.GetFiles(CardDataPath);
            var cardDictionary = new Dictionary<int, MonsterCardData>();
            
            foreach (var file in files)
            {
                if (file.EndsWith(".meta"))
                    continue;

                var json = File.ReadAllText(file);
                var result = JsonConvert.DeserializeObject<MonsterCardData>(json);
                cardDictionary.Add(result.Id, result);
            }

            CardDatabase = new CardDatabase(cardDictionary);
        }

        public CardDatabase GetCardDatabase()
        {
            return CardDatabase;
        }
    }
}