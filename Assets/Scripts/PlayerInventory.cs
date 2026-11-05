using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    
    public int currentBullets = 10;
    public int maxBullets = 40;
    
    public bool AddAmmo(int amount)
    {
        if (currentBullets >= maxBullets)
        {
            
            return false;
        }
        currentBullets += amount;

        
        currentBullets = Mathf.Clamp(currentBullets, 0, maxBullets);

        return true;
    }
}
