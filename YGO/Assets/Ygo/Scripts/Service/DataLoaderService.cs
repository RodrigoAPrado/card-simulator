using UnityEngine;

namespace Ygo.Service
{
    public class DataLoaderService
    {
        private static readonly string CardDataPath 
            = Application.streamingAssetsPath + "/Ygo/Data/Cards/";
        private static readonly string EffectDataPath 
            = Application.streamingAssetsPath + "/Ygo/Data/Effects/";

        public void LoadCards()
        {
            /*
            var files = Directory.GetFiles(CardDataPath);
            var cardDictionary = new Dictionary<string, object>();
            
            foreach (var file in files)
            {
                if (file.EndsWith(".meta"))
                    continue;

                var json = File.ReadAllText(file);
                var result = JsonConvert.DeserializeObject<object>(json);
                cardDictionary.Add(result.Id, result);
            }
            */
        }
    }
}