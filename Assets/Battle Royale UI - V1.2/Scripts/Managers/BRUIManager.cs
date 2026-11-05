using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace BattleRoyaleUI
{
    public class BRUIManager : MonoBehaviour
    {
        [Header("Generics Parameters")]
        [SerializeField] private string playerName;
        [SerializeField] private Color nameColour;

        [Header("Value UI References")]
        [SerializeField] private TMP_Text playerNameUI;
        [SerializeField] private TMP_Text cashAmountUI;
        [SerializeField] private TMP_Text armourAmountUI;

        [Header("Vitals UI References")]
        [SerializeField] private Image mainHealthUI;
        [Space(10)]
        [SerializeField] private Image[] armourHealthUI;

        [Header("Crosshair")]
        [SerializeField] private Image equipArmourProgressUI;
        [SerializeField] private Image crosshairUI;

        [Header("Flash Panel")]
        [SerializeField] private Image blueFlashPanel;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public static BRUIManager instance;

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
            FieldNullCheck();
        }

        public void HighlightCrosshair(bool on)
        {
            crosshairUI.color = on ? Color.red : Color.white;
        }

        public void UpdateEquipProgressUI(float progress)
        {
            equipArmourProgressUI.fillAmount = progress;
        }

        public void ResetEquipProgressUI()
        {
            equipArmourProgressUI.fillAmount = 0;
        }

        public void UpdateArmourAmountUI(float armourValue)
        {
            armourAmountUI.text = armourValue.ToString("0");
        }

        public void UpdateCashUI(float cashValue)
        {
            cashAmountUI.text = cashValue.ToString("0");
        }

        public void UpdateHealthUI(float currentHealthValue, float maximumHealthBars)
        {
            mainHealthUI.fillAmount = currentHealthValue / maximumHealthBars;
        }

        public void SetHealthZeroUI()
        {
            mainHealthUI.fillAmount = 0;
        }

        public void UpdateArmourUI(int index, float currentArmourValue)
        {
            if (index < 0 || index >= armourHealthUI.Length)
            {
                Debug.LogError("Invalid Armour UI Index!");
                return;
            }

            armourHealthUI[index].fillAmount = currentArmourValue;
        }

        public void SetArmourZeroUI(int index)
        {
            if (index < 0 || index >= armourHealthUI.Length)
            {
                Debug.LogError("Invalid Armour UI Index!");
                return;
            }

            armourHealthUI[index].fillAmount = 0;
        }

        public void TriggerBlueFlash()
        {
            StartCoroutine(BlueFlashEffect());
        }

        IEnumerator BlueFlashEffect()
        {
            blueFlashPanel.color = new Color(0, 0, 1, 0.5f); // Semi-transparent blue
            yield return new WaitForSeconds(0.1f); // Flash duration
            blueFlashPanel.color = new Color(0, 0, 1, 0); // Fully transparent
        }

        public void UpdateUI(float playerCash, float playerArmourAmount, float currentHealth, float startingArmourEquipped)
        {
            playerNameUI.text = playerName;
            playerNameUI.color = nameColour;
            cashAmountUI.text = playerCash.ToString("0");
            armourAmountUI.text = playerArmourAmount.ToString("0");
            mainHealthUI.fillAmount = currentHealth;

            for (int i = 0; i < armourHealthUI.Length; i++)
            {
                if (i < startingArmourEquipped)
                {
                    armourHealthUI[i].fillAmount = 1;
                }
                else
                {
                    armourHealthUI[i].fillAmount = 0;
                }
            }
        }

        void FieldNullCheck()
        {
            // Checking each field and logging an error if it is null
            CheckField(playerNameUI, "PlayerNameUI");
            CheckField(cashAmountUI, "CashAmountUI");
            CheckField(armourAmountUI, "ArmourAmountUI");

            CheckField(mainHealthUI, "MainHealthUI");
            CheckField(equipArmourProgressUI, "EquipArmourProgressUI");
            CheckField(crosshairUI, "CrosshairUI");
        }

        void CheckField(Object field, string fieldName)
        {
            if (field == null)
            {
                Debug.LogError(gameObject + " " + $"FieldNullCheck: {fieldName} is not set in the inspector!");
            }
        }
    }
}