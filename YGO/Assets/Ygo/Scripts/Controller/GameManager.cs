using UnityEngine;
using Ygo.Service;

namespace Ygo.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        private CardLoaderService Service { get; set; }
        public void Awake()
        {
            Service = new CardLoaderService();
            var data = Service.LoadCards();
            
        }
    }
}