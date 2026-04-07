using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ygo.View.Field
{
    public class ZoneView : MonoBehaviour
    {
        [field: SerializeField]
        private Image border { get; set; }
        [field: SerializeField]
        private Color32 normalColor { get; set; }
        [field: SerializeField]
        private Color32 hoverColor { get; set; }
        
        [field: SerializeField]
        private Color32 highlightColor { get; set; }

        [field: SerializeField]
        private Image highlight;

        public void Init()
        {
            ToggleHover(false);
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
            highlight.color = highlightColor;
            highlight.DOKill();

            highlight.DOFade(0f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopHighlight()
        {
            highlight.DOKill();
            highlight.gameObject.SetActive(false);
        }
        
        public void ToggleHover(bool value)
        {
            border.color = value ? hoverColor : normalColor;
        }
    }
}