using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Ygo.Scripts.Cards;
using Ygo.Scripts.Cards.Enums;
using Ygo.Scripts.Core;

namespace Ygo.Service
{
    public class CardLoaderService
    {
        private static readonly string CardDataPath 
            = Application.dataPath + "/Ygo/Data/Cards/";

        public ICardRepository LoadCards()
        {
            var files = Directory.GetFiles(CardDataPath);
            var cardDictionary = new Dictionary<int, CardData>();
            
            foreach (var file in files)
            {
                if (file.EndsWith(".meta"))
                    continue;

                var json = File.ReadAllText(file);
                var result = JsonConvert.DeserializeObject<CardData>(json);
                cardDictionary.Add(result.Id, result);
            }

            return new CardRepository(cardDictionary);
        }
    }
}