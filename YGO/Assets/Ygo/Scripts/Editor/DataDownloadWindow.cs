using System;
using System.Collections;
using System.Collections.Generic;
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
            UnityWebRequest www = UnityWebRequest.Get("https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate=2000-01-01&enddate=2002-08-23&dateregion=tcg&type=Normal%20Monster");
            await www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                return;
            }
            var result = JsonConvert.DeserializeObject<CardDataModel>(www.downloadHandler.text);
            var cardDatas = new List<CardData>();
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
                
                cardDatas.Add(cardData);
            }
            
            Debug.Log("success");
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