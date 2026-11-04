using UnityEngine;

namespace BattleRoyaleUI
{
    public class DamageArea : MonoBehaviour
    {
        [SerializeField][Range(0, 50)] private float damageMult;

        public enum DamageType { Gas, Bullets }
        [SerializeField] private DamageType damageType;


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BRVitalsManager.instance.SetDamageArea(true);

                BRVitalsManager.instance.DamageVitals(damageMult, damageType);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BRVitalsManager.instance.SetDamageArea(false);
            }
        }
    }
}
