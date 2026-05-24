using UnityEngine;

namespace Ygo.View.Card
{
    public class CardFieldStatsView : MonoBehaviour
    {
        [field: SerializeField]
        private GameObject fieldStats;
        [field: SerializeField] 
        private StatsTextViewUI attackText;
        [field: SerializeField] 
        private StatsTextViewUI defenseText;
        [field: SerializeField] 
        private StatsTextViewUI levelText;

        public void Init()
        {
            fieldStats.SetActive(false);
            attackText.SetText("");
            defenseText.SetText("");
            levelText.SetText("");
        }

        public void ToggleAtkDefMode(bool isDefMode)
        {
            if (isDefMode)
            {
                attackText.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                defenseText.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                defenseText.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                attackText.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
        }
        
        public void SetStats(string attack, string defense, string level)
        {
            attackText.SetText(attack);
            defenseText.SetText(defense);
            levelText.SetText(level);
            fieldStats.SetActive(true);
        }
        
        public void Disable()
        {
            fieldStats.SetActive(false);
        }
    }
}