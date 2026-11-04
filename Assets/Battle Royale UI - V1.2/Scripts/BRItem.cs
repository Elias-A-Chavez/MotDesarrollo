using UnityEngine;

namespace BattleRoyaleUI
{
    public class BRItem : MonoBehaviour
    {
        public enum ItemType
        {
            None, Armour, Cash
        }

        [Header("Item Type")]
        [SerializeField] private ItemType itemType;

        [Header("Item Parameters")]
        [SerializeField] private float itemValue;

        public void ObjectInteraction()
        {
            switch (itemType)
            {
                case ItemType.Armour:
                    BRVitalsManager.instance.UpdateArmourAmount(itemValue);
                    gameObject.SetActive(false);
                    break;
                case ItemType.Cash:
                    BRVitalsManager.instance.UpdateCashAmount(itemValue);
                    gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Invalid item type selected!");
                    break;
            }
        }
	}
}
