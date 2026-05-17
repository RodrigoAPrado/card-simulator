#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Ygo.Editor
{
    public class DataDownloadWindow : EditorWindow
    {
        [MenuItem("Ygo/Yugioh Window Template")]
        public static void ShowWindow()
        {
            GetWindow<DataDownloadWindow>("Window Template");
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("Button"))
            {
                Debug.Log("Button Pressed!");
            }
        }
    }
}
#endif