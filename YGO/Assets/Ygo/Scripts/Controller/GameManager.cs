using UnityEngine;
using Ygo.Scripts.Application;
using Ygo.Service;

namespace Ygo.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        private GameApplication Application { get; set; }
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            Application = new GameApplication(data);
        }
    }
}