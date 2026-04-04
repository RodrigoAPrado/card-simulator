using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Editor
{
    public class CardEditorWindow : EditorWindow
    {
        private int id = 0;
        private string cardName = "Dark Magician";
        private CardType cardType = CardType.Monster;
        private MonsterAttribute attribute = MonsterAttribute.Dark;
        private MonsterType monsterType = MonsterType.Spellcaster;
        private List<MonsterKind> monsterKinds = new List<MonsterKind> { MonsterKind.Normal };
        private int atk = 2500;
        private int def = 2100;
        private int? level = 7;
        private string flavorText = "The ultimate wizard in terms of attack and defence.";
        
        private Vector2 scrollPosition;

        [MenuItem("Ygo/Yugioh Card Editor")]
        public static void ShowWindow()
        {
            GetWindow<CardEditorWindow>("Yugioh Card Editor");
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.Label("Card Data", EditorStyles.boldLabel);
            
            // ID
            id = EditorGUILayout.IntField("ID", id);

            // Name
            cardName = EditorGUILayout.TextField("Name", cardName);

            // Card Type
            cardType = (CardType)EditorGUILayout.EnumPopup("Card Type", cardType);

            // Monster Data
            if (cardType == CardType.Monster)
            {
                GUILayout.Label("Monster Data", EditorStyles.boldLabel);

                // Attribute
                attribute = (MonsterAttribute)EditorGUILayout.EnumPopup("Attribute", attribute);

                // Monster Type
                monsterType = (MonsterType)EditorGUILayout.EnumPopup("Monster Type", monsterType);

                // Monster Kinds
                var kindsArray = (MonsterKind[])System.Enum.GetValues(typeof(MonsterKind));
                for (int i = 0; i < kindsArray.Length; i++)
                {
                    var kind = kindsArray[i];
                    if (EditorGUILayout.Toggle(kind.ToString(), monsterKinds.Contains(kind)))
                    {
                        if (!monsterKinds.Contains(kind)) monsterKinds.Add(kind);
                    }
                    else
                    {
                        if (monsterKinds.Contains(kind)) monsterKinds.Remove(kind);
                    }
                }

                // ATK and DEF
                atk = EditorGUILayout.IntField("ATK", atk);
                def = EditorGUILayout.IntField("DEF", def);

                // Level
                level = EditorGUILayout.IntField("Level", level ?? 0);

                // Flavor Text
                flavorText = EditorGUILayout.TextField("Flavor Text", flavorText);
            }

            if (GUILayout.Button("Generate JSON"))
            {
                GenerateJSON();
            }

            GUILayout.EndScrollView();
        }

        private void GenerateJSON()
        {
            // Criação do objeto MonsterData
            var monsterData = new MonsterData(
                attribute,
                monsterType,
                monsterKinds,
                atk,
                def,
                level,
                null,  // Rank não utilizado por enquanto
                null,  // PendulumScale não utilizado
                new List<int>(),  // LinkArrows não utilizado
                new List<int>(),  // EffectIds não utilizado
                flavorText
            );

            // Criação do objeto CardData
            var cardData = new CardData(id, cardType, cardName, monsterData);

            // Serialização para JSON
            string json = JsonConvert.SerializeObject(cardData, Formatting.Indented);

            // Salvar JSON em um arquivo
            string path = EditorUtility.SaveFilePanel("Save Card JSON", Application.dataPath + "/Ygo/Data/Cards/", getFileName(id.ToString()), "json");
            if (!string.IsNullOrEmpty(path))
            {
                System.IO.File.WriteAllText(path, json);
                AssetDatabase.Refresh();
                Debug.Log("JSON generated: " + path);
            }
        }
        
        private string getFileName(string id)
        {;
            var sb = new StringBuilder();
            for (var i = 6; i > id.Length; i--)
            {
                sb.Append("0");
            }

            sb.Append(id);
            return sb.ToString();
        }
    }
}
