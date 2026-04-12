using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ygo.View.Component
{
    public class HighlightView : MonoBehaviour
    {
        [field: SerializeField]
        private Image highlight;
        [field: SerializeField]
        private Color highlightStartColor;

        public void Init()
        {
            ToggleHighlight(false);
        }
        
        public void ToggleHighlight(bool value)
        {
            if (value)
            {
                AnimateHighlight();
            }
            else {
                StopHighlight();
            }
        }
        
        private void AnimateHighlight()
        {
            highlight.gameObject.SetActive(true);
            highlight.DOKill();
            highlight.color = highlightStartColor;

            highlight.DOFade(0f, 0.6f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopHighlight()
        {
            highlight.DOKill();
            highlight.gameObject.SetActive(false);
        }
    }
}