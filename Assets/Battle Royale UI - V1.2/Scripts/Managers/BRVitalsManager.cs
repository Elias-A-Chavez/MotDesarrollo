using UnityEngine;
using UnityEngine.Events;

namespace BattleRoyaleUI
{
    public class BRVitalsManager : MonoBehaviour
    {
        [Header("Player Cash Parameters")]
        [SerializeField] private float playerCash;

        [Header("Health Parameters")]
        [Range(0, 100)] [SerializeField] private float currentHealthValue;

        [Header("Armour Parameters")]
        [Range(0, 3)] [SerializeField] private int startingArmourEquipped = 1;
        [SerializeField] private float armourStored;

        [Header("Equip Timers")]
        [SerializeField] private float maxHealTimer = 2;
        [SerializeField] private float maxEquipArmourTimer = 2;

        [Header("Armour Equip Key")]
        [SerializeField] private KeyCode equipArmourKey;

        [Header("Sounds")]
        [SerializeField] private Sound armourEquipSound;
        [SerializeField] private Sound armourPickupSound;
        [SerializeField] private Sound cashPickupSound;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        [Header("Death Event")]
        [SerializeField] private UnityEvent deathEvent;

        private float maximumHealthBars;
        private float currentArmourValue;
        private bool regenHealth;
        private bool inDamageArea;
        private bool isDead;
        private float currentHealthTimer;
        private float currentArmourTimer;
        private BRUIManager uiManager;

        public static BRVitalsManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        private void Start()
        {
            maximumHealthBars = 100f;
            currentArmourValue = startingArmourEquipped * 100;
            currentHealthTimer = maxHealTimer;
            currentArmourTimer = maxEquipArmourTimer;
            uiManager = BRUIManager.instance;
            uiManager.UpdateUI(playerCash, armourStored, (currentHealthValue / 100), startingArmourEquipped);

            regenHealth = currentHealthValue <= maximumHealthBars;
        }

        private void Update()
        {
            if (isDead) return; // Stop all updates if the player is dead

            EquipArmour();

            if (regenHealth)
            {
                RegenerateHealth();
            }

            //print(armourStored);
        }

        void EquipArmour()
        {
            if (Input.GetKey(equipArmourKey) && armourStored >= 1)
            {
                currentArmourTimer -= Time.deltaTime;
                uiManager.UpdateEquipProgressUI(1 - (currentArmourTimer / maxEquipArmourTimer));

                if (currentArmourTimer <= 0)
                {
                    armourStored--;
                    ArmourEquipAudio();

                    if (currentArmourValue < 100)
                    {
                        currentArmourValue = 100;
                    }
                    else if (currentArmourValue < 200)
                    {
                        currentArmourValue = 200;
                    }
                    else if (currentArmourValue < 300)
                    {
                        currentArmourValue = 300;
                    }

                    UpdateVitals();
                    currentArmourTimer = maxEquipArmourTimer;
                }
            }
            else if (currentArmourTimer < maxEquipArmourTimer)
            {
                currentArmourTimer += Time.deltaTime;
                uiManager.UpdateEquipProgressUI(1 - (currentArmourTimer / maxEquipArmourTimer));
            }

            if (Input.GetKeyUp(equipArmourKey) || currentArmourValue >= 300)
            {
                currentArmourTimer = maxEquipArmourTimer;
                uiManager.ResetEquipProgressUI();
            }
        }

        void RegenerateHealth()
        {
            if (currentHealthValue >= maximumHealthBars || isDead)
            {
                return;
            }

            currentHealthTimer -= Time.deltaTime;
            if (currentHealthTimer <= 0)
            {
                currentHealthValue += Time.deltaTime * 10;
                UpdateVitals();
                currentHealthTimer = 0;

                if (currentHealthValue >= maximumHealthBars)
                {
                    currentHealthTimer = maxHealTimer;
                    regenHealth = false;
                }
            }
        }

        public void SetDamageArea(bool isActive)
        {
            inDamageArea = isActive;
            regenHealth = !isActive && !isDead; // Prevent regeneration if dead
        }

        public void DamageVitals(float damageMult, DamageArea.DamageType damageType)
        {
            if (isDead) return; // Prevent further damage if the player is already dead

            float damage = Time.deltaTime * damageMult;

            switch (damageType)
            {
                case DamageArea.DamageType.Gas:
                    ApplyGasDamage(damage);
                    break;
                case DamageArea.DamageType.Bullets:
                    ApplyBulletDamage(damage);
                    break;
            }
            UpdateVitals();
        }

        private void ApplyGasDamage(float damage)
        {
            if (currentHealthValue > 0)
            {
                currentHealthValue -= damage;
                if (currentHealthValue <= 0)
                {
                    currentHealthValue = 0;
                    HandleDeath();
                }
            }
        }

        private void ApplyBulletDamage(float damage)
        {
            if (currentArmourValue > 0)
            {
                currentArmourValue -= damage;
                if (currentArmourValue <= 0)
                {
                    currentArmourValue = 0;
                    uiManager.TriggerBlueFlash();
                }
            }
            else if (currentHealthValue > 0)
            {
                currentHealthValue -= damage;
                if (currentHealthValue <= 0)
                {
                    currentHealthValue = 0;
                    HandleDeath();
                }
            }
        }

        void HandleDeath()
        {
            // Handle player death (e.g., trigger death animation, game over screen, etc.)
            // Or use the custom event in the inspector to enable functionality
            isDead = true;
            print("Player has died");
            deathEvent.Invoke();
        }

        void UpdateVitals()
        {
            uiManager.UpdateArmourAmountUI(armourStored);

            if (currentHealthValue >= 1)
            {
                uiManager.UpdateHealthUI(currentHealthValue, maximumHealthBars);
                regenHealth = !inDamageArea;

                if (currentHealthValue >= maximumHealthBars)
                {
                    regenHealth = false;
                    currentHealthTimer = maxHealTimer;
                }
            }
            else
            {
                uiManager.SetHealthZeroUI();
            }

            if (currentArmourValue >= 1)
            {
                uiManager.UpdateArmourUI(0, Mathf.Clamp(currentArmourValue / maximumHealthBars, 0, maximumHealthBars));
            }
            else
            {
                uiManager.SetArmourZeroUI(0);
            }

            if (currentArmourValue >= 101)
            {
                uiManager.UpdateArmourUI(1, Mathf.Clamp((currentArmourValue - 100) / maximumHealthBars, 0, maximumHealthBars));
            }
            else
            {
                uiManager.SetArmourZeroUI(1);
            }

            if (currentArmourValue >= 201)
            {
                uiManager.UpdateArmourUI(2, Mathf.Clamp((currentArmourValue - 200) / maximumHealthBars, 0, maximumHealthBars));
            }
            else
            {
                uiManager.SetArmourZeroUI(2);
            }
        }

        public void UpdateArmourAmount(float armourValue)
        {
            ArmourPickupAudio();
            armourStored += armourValue;
            uiManager.UpdateArmourAmountUI(armourStored);
        }

        public void UpdateCashAmount(float cashValue)
        {
            CashPickupAudio();
            playerCash += cashValue;
            uiManager.UpdateCashUI(playerCash);
        }

        void ArmourEquipAudio()
        {
            BRAudioManager.instance.Play(armourEquipSound);
        }

        void ArmourPickupAudio()
        {
            BRAudioManager.instance.Play(armourPickupSound);
        }

        void CashPickupAudio()
        {
            BRAudioManager.instance.Play(cashPickupSound);
        }
    }
}