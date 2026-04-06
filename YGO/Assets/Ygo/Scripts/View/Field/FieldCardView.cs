using DG.Tweening;
using TMPro;
using UnityEngine;
using Ygo.View.ScriptableObjects;

namespace Ygo.View.Field
{
    public class FieldCardView : MonoBehaviour
    {
        [Header("Frame")] 
        [field: SerializeField]
        private SpriteRenderer frameImage;

        [field: SerializeField] 
        private GameObject highlight;
        
        [field: SerializeField] 
        private CardFrameDatabase frameDatabase;

        [Header("Name")] 
        [field: SerializeField]
        private TextMeshPro cardName;
        
        [Header("Icon")]
        [field: SerializeField] 
        private SpriteRenderer iconImage;

        [field: SerializeField] 
        private CardIconDatabase iconDatabase;
        
        [Header("Levels")]
        [field: SerializeField]
        private GameObject[] levelsContainer;

        [Header("Illustration")] 
        [field: SerializeField]
        private SpriteRenderer illustration;

        [field: SerializeField] 
        private Sprite emptyIllustrationSprite;

        [Header("CardBox")] 
        [field: SerializeField]
        private TextMeshPro monsterAtk;
        [field: SerializeField]
        private TextMeshPro monsterDef;
        [field: SerializeField]
        private TextMeshPro monsterLevel;

        [Header("CardMode")] 
        [field: SerializeField]
        private GameObject cardContent;
        [field: SerializeField]
        private GameObject cardFront;
        [field: SerializeField]
        private GameObject cardBack;
        [field: SerializeField]
        private RectTransform cardGraphics;
        [field: SerializeField]
        private RectTransform atkRect;
        [field: SerializeField]
        private RectTransform defRect;

        public void SetDefense(bool value)
        {
            if (value)
            {
                cardGraphics.localRotation = Quaternion.Euler(0, 0, 90);
                highlight.transform.localRotation = Quaternion.Euler(0, 0, 90);
                atkRect.localScale = new Vector3(0.8f, 0.8f, 1);
                defRect.localScale = new Vector3(1.2f, 1.2f, 1);
            }
            else
            {
                cardGraphics.localRotation = Quaternion.Euler(0, 0, 0);
                highlight.transform.localRotation = Quaternion.Euler(0, 0, 0);
                atkRect.localScale = new Vector3(1.2f, 1.2f, 1);
                defRect.localScale = new Vector3(0.8f, 0.8f, 1);
            }
        }

        public void AnimateFloating()
        {
            // 1. Garante que qualquer animação anterior no cardContent seja parada para não encavalar
            cardContent.transform.DOKill();

            // 2. Faz o movimento no eixo Z local
            // Supondo que 0.1f seja a altura da flutuação
            cardContent.transform.DOLocalMoveZ(-0.08f, 1.2f) 
                .SetEase(Ease.InOutSine) // Movimento suave nas extremidades
                .SetLoops(-1, LoopType.Yoyo); // -1 = Infinito, Yoyo = vai e volta
        }
        
        public void SetFrame(CardFrameType type)
        {
            if(frameDatabase != null)
            {
                frameImage.sprite = frameDatabase.GetFrame(type);
                frameImage.enabled = true;
            }
            else
            {
                frameImage.enabled = false;
            }
        }
        
        public void SetName(string value)
        {
            cardName.text = value;
        }
        
        public void SetIcon(CardIconType type)
        {
            if(iconDatabase != null)
            {
                iconImage.sprite = iconDatabase.GetIcon(type);
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }
        }

        public void SetLevel(int value)
        {
            monsterLevel.text = value.ToString();
            for (var i = 0; i < levelsContainer.Length; i++)
            {
                levelsContainer[i].SetActive(value > i);
            }
        }

        public void SetIllustration(string value)
        {
            var path = "Card/Illustrations/" + value;
            var sprite = Resources.Load<Sprite>(path);
            if(sprite != null)
                illustration.sprite = sprite;
            else
            {
                //Debug.LogWarning(path + " is not a valid illustration");
                illustration.sprite = emptyIllustrationSprite;
            }
        }

        public void SetMonsterAtk(string value)
        {
            monsterAtk.text = value;
        }

        public void SetMonsterDef(string value)
        {
            monsterDef.text = value;
        }

        public void ToggleHighlight(bool value)
        {
            highlight.SetActive(value);
        }

        public void Clear()
        {
            //TODO: Rotina de Clear.
        }
    }
}