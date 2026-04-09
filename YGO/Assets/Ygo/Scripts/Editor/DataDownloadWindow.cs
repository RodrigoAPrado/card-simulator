#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Ygo.Data;
using Ygo.Data.Enums;
using Ygo.Editor.Model;

namespace Ygo.Editor
{
    public class DataDownloadWindow : EditorWindow
    {
        [MenuItem("Ygo/Yugioh Data Download")]
        public static void ShowWindow()
        {
            GetWindow<DataDownloadWindow>("Yugioh Data Download");
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Download Sets"))
            {
                DownloadSets();
            }
            if (GUILayout.Button("Download Cards"))
            {
                DownloadCards();
            }
        }

        private static async Task DownloadSets()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://db.ygoprodeck.com/api/v7/cardsets.php");
            await www.SendWebRequest();
 
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                return;
            }
        }

        private static async Task DownloadCards()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate=2000-01-01&enddate=2002-08-23&dateregion=tcg&type=Normal%20Monster");
            await request.SendWebRequest();
            
            while (!request.isDone)
                await Task.Yield();
            
            if (request.result != UnityWebRequest.Result.Success) {
                Debug.Log(request.error);
                return;
            }
            
            var result = JsonConvert.DeserializeObject<CardDataModel>(request.downloadHandler.text);
            var imgToDownload = new Dictionary<string, string>();

            ClearDataFiles();
            
            foreach (var model in result.Data)
            {
                var cardData = new CardData();
                cardData.Name = model.Name;
                
                var id = model.Id.ToString();
                while (id.Length < 8)
                {
                    id = "0" + id;
                }
                cardData.Id = id;
                
                
                switch (model.Type)
                {
                    case "Normal Monster":
                        cardData.CardType = CardType.Monster;
                        break;
                }

                MonsterData monsterData = null;
                if(cardData.CardType == CardType.Monster)
                    monsterData = GetMonsterData(model);

                cardData.MonsterData = monsterData;
                cardData.Validate();
                SaveCardData(cardData);
                imgToDownload.Add(id, model.CardImages.FirstOrDefault()?.ImageUrlCropped);
            }
            
            ClearImages();
            DownloadCardImages(imgToDownload);
        }

        private static async Task DownloadCardImages(Dictionary<string, string> urlDictionary)
        {
            var path = Application.dataPath + "/Resources/Card/Illustrations/";

            float index = 0;
            foreach (var value in urlDictionary)
            {
                using (UnityWebRequest request = UnityWebRequest.Get(value.Value))
                {
                    var operation = request.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();
                    
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Erro ao baixar: {value.Value} -> {request.error}");
                        continue;
                    }
                    
                    var data = request.downloadHandler.data;

                    var fileName = value.Key + ".jpg";

                    var fullPath = Path.Combine(path, fileName);

                    await File.WriteAllBytesAsync(fullPath, data);
                    index++;
                    var percent = index / (urlDictionary.Count) * 100;
                    percent = Mathf.Floor(percent * 100f) / 100f;
                    Debug.Log($"Baixado: {fileName} - Total: {percent}%");
                }
            }
        }
        
        
        private static void ClearImages()
        {
            var files = Directory.GetFiles(Application.dataPath + "/Resources/Card/Illustrations/");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private static void ClearDataFiles()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
                return;
            
            var files = Directory.GetFiles(Application.streamingAssetsPath + "/Ygo/Data/Cards/");
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private static void SaveCardData(CardData cardData)
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Ygo/");
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Ygo/Data/");
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Ygo/Data/Cards/");
            }
            var json = JsonConvert.SerializeObject(cardData, Formatting.Indented);
            var fileName = $"{cardData.Id}.json";
            var fullPath = Path.Combine(Application.streamingAssetsPath + "/Ygo/Data/Cards/", fileName);
            File.WriteAllText(fullPath, json);
        }

        private static MonsterData GetMonsterData(CardModel model)
        {
            var data = new MonsterData();
            
            foreach (var typeline in model.TypeLine)
            {
                Enum.TryParse(typeline.Replace("-", "").Replace(" ", ""), out MonsterType val);
                if (val == MonsterType.Unknown) 
                    continue;
                data.Type = val;
                break;
            }

            var capitalizedModelAttribute = model.Attribute.Substring(0, 1).ToUpper() + model.Attribute.Substring(1).ToLower();
            Enum.TryParse(capitalizedModelAttribute, out MonsterAttribute attribute);
            data.Attribute = attribute;

            data.Kinds = new List<MonsterKind>();
            foreach (var typeline in model.TypeLine)
            {
                Enum.TryParse(typeline.Replace("-", "").Replace(" ", ""), out MonsterKind val);
                if (val == MonsterKind.Unknown) 
                    continue;
                data.Kinds.Add(val);
                break;
            }
            data.Atk = model.Atk;
            data.Def = model.Def;
            data.Level = model.Level;

            if (data.Kinds.Contains(MonsterKind.Normal) && data.Kinds.Count == 1)
            {
                data.FlavorText = model.Desc;
            }

            return data;

        }
    }
}
#endif