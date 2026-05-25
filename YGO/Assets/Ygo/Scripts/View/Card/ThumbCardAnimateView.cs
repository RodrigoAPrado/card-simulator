using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Ygo.View.Card
{
    public class ThumbCardAnimateView : MonoBehaviour
    {
        private Sequence _sequence;
        public async UniTask MoveCard(RectTransform thisPosition, RectTransform targetPosition)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence(); 
            _sequence.Join(thisPosition.DOSizeDelta(targetPosition.sizeDelta, 0.3f));
            _sequence.Join(thisPosition.DOAnchorPos(targetPosition.anchoredPosition, 0.3f));
            _sequence.SetEase(Ease.InOutQuad);
            await _sequence.Play();
        }
        
        public async UniTask MoveCardX(RectTransform thisPosition, RectTransform targetPosition)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence(); 
            _sequence.Join(thisPosition.DOAnchorPosX(targetPosition.anchoredPosition.x, 0.2f));
            _sequence.SetEase(Ease.InOutQuad);
            await _sequence.Play();
        }
        
        public async UniTask MoveCardField(Transform targetPosition,
            RectTransform thisPositionRect, RectTransform targetPositionRect)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence(); 
            _sequence.Join(transform.DOMove(targetPosition.position, 0.2f));
            _sequence.Join(thisPositionRect.DOSizeDelta(targetPositionRect.sizeDelta, 0.4f));
            _sequence.Join(transform.DORotate(targetPosition.eulerAngles, 0.4f));
            _sequence.SetEase(Ease.InOutQuad);
            await _sequence.Play();
        }
    }
}