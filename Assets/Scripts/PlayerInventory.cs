using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int currentBullets = 10;
    [SerializeField] private int maxBullets = 40;

    public int CurrentBullets => currentBullets;
    public int MaxBullets => maxBullets;

    public bool AddAmmo(int amount)
    {
        if (amount <= 0 || currentBullets >= maxBullets)
        {
            return false;
        }

        currentBullets = Mathf.Clamp(currentBullets + amount, 0, maxBullets);

        return true;
    }
}
