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
            Service.LoadCards();
        }
    }
}