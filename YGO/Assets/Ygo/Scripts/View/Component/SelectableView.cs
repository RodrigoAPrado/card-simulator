using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ygo.View.Component
{
    public class SelectableView : MonoBehaviour
    {
        [field: SerializeField]
        private Image image;
        [field: SerializeField]
        private Color colorNotAnimation;
        [field: SerializeField]
        private Color[] colorAnim;

        private Sequence curSequence;

        public void Init()
        {
            image.color = colorNotAnimation;
        }
        
        public void Animate()
        {
            curSequence?.Kill();
            if (colorAnim.Length <= 0)
                return;
            image.color = colorAnim[^1];
            curSequence = DOTween.Sequence();
            foreach (var color in colorAnim)
            {
                curSequence.Append(image.DOColor(color, 0.3f).SetEase(Ease.Linear));
            }
            curSequence.SetLoops(-1);
            curSequence.Play();
        }

        public void StopAnimating()
        {
            curSequence?.Kill();
            image.color = colorNotAnimation;
        }
    }
}