using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Data;

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