using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.View;

namespace Ygo.Controller
{
    public class AnnouncementController : MonoBehaviour
    {
        [field:SerializeField]
        private TextViewUI whiteText;
        [field:SerializeField]
        private TextViewUI blackText;
        [field: SerializeField] 
        private AnnouncementView view;
        

        public void Init()
        {
            whiteText.gameObject.SetActive(false);
            blackText.gameObject.SetActive(false);
        }
        
        public async UniTask DisplayAnnouncement(string announcement)
        {
            whiteText.gameObject.SetActive(true);
            blackText.gameObject.SetActive(true);
            whiteText.SetText(announcement);
            blackText.SetText(announcement);

            await view.Animate();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            whiteText.gameObject.SetActive(false);
            blackText.gameObject.SetActive(false);
            
        }
    }
}