using UnityEngine;

namespace Ygo.View.Field
{
    public class DeckView : MonoBehaviour
    {
        [field: SerializeField] 
        private GameObject[] deckGraphics;
        [field: SerializeField] 
        private TextViewUI deckCountText;

        public void SetSize(int deckCount)
        {
            deckCountText.SetText(deckCount.ToString());
            switch (deckCount)
            {
                case >30:
                    ToggleDeckGraphics(3);
                    break;
                case >20:
                    ToggleDeckGraphics(2);
                    break;
                case >10:
                    ToggleDeckGraphics(1);
                    break;
                case >0:
                    ToggleDeckGraphics(0);
                    break;
                default:
                    ToggleDeckGraphics(-1);
                    break;
            }
        }

        private void ToggleDeckGraphics(int deckGraphicsIndex)
        {
            foreach (var deck in deckGraphics)
            {
                deck.SetActive(false);
            }

            if (deckGraphicsIndex < 0)
                return;
            
            deckGraphics[deckGraphicsIndex].SetActive(true);
        }
    }
}