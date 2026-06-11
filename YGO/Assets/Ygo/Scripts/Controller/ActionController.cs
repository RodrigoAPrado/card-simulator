using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace Ygo.Controller
{
    public class ActionController : MonoBehaviour
    {
        [Serializable]
        public struct ActionAnchor
        {
            public PointOfView Key;
            public PointOfViewAnchor[] Anchor;
        }

        [Serializable]
        public struct PointOfViewAnchor
        {
            public Location Key;
            public Transform Transform;
        }
        
        [Header("Anchors")] 
        [field: SerializeField]
        private List<ActionAnchor> anchors;
        
        [Header("Actions")] 
        [field: SerializeField]
        private ButtonController[] buttons;

        public void Init()
        {
            Clear();
        }

        public void ShowCommands(
            IReadOnlyDictionary<string, Action> commands, 
            Transform position, 
            PointOfView pointOfView, 
            Location location
            )
        {
            Clear();
            var index = 0;
            foreach (var command in commands)
            {
                buttons[index].Init(() =>
                {
                    command.Value?.Invoke();
                    Clear();
                }, command.Key);
                index++;
            }

            buttons[index].Init(Clear, "Close");
            
            var anchorPos = anchors.FirstOrDefault(x => x.Key == pointOfView);
            if (anchorPos.Anchor != null)
            {
                var t = anchorPos.Anchor.FirstOrDefault(x => x.Key == location);
                if (t.Transform != null)
                {
                    transform.position = new Vector3(position.position.x, t.Transform.position.y, transform.position.z);
                    return;
                }
            }
            Debug.LogWarning("No anchor found");
            transform.position = position.position;
        }

        private void Clear()
        {
            foreach (var button in buttons)
            {
                button.Init(() => {}, "");
                button.Disable(true);
            }
        }
    }
}