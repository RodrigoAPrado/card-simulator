using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
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
            Debug.Log(www.downloadHandler.text);
        }

        private static async Task DownloadCards()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate=2000-01-01&enddate=2002-08-23&dateregion=tcg");
            await www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                return;
            }
            Debug.Log(www.downloadHandler.text);
            var result = JsonConvert.DeserializeObject<CardDataModel>(www.downloadHandler.text);
            foreach (var card in result.Data)
            {
                
            }
        }
    }
}