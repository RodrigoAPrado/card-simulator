using System;
using UnityEngine;
using Ygo.Core.Bridge;

namespace Ygo.Controller
{
    public class OcgCoreManager : MonoBehaviour
    {
        public void Awake()
        {
            var teste = new DuelBridge();
            //teste.Start();
        }
    }
}