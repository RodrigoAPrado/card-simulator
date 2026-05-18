using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Ygo.View
{
    public class AnnouncementView : MonoBehaviour
    {
        [field: SerializeField]
        private RectTransform announcementTexts;
        private Vector2 originalPosition;

        private void Awake()
        {
            originalPosition = announcementTexts.anchoredPosition;
        }

        public async UniTask Animate()
        {
            announcementTexts.DOKill();
            announcementTexts.anchoredPosition = new Vector2(-1000f, originalPosition.y);

            // Cria o tween normalmente
            Tween minhaAnimacao = announcementTexts.DOAnchorPos(originalPosition, 0.3f)
                .SetEase(Ease.OutQuad);

            await minhaAnimacao.AwaitForComplete();
        }
    }
}