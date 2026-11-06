using UnityEngine;

public class ScapeManager : MonoBehaviour
{
    [SerializeField] private GameObject Door1;
    [SerializeField] private GameObject Door2;
    [SerializeField] private GameObject Boss;

    void Update()
    {
        if (Boss.activeSelf)
        {
            Door1.SetActive(false);
            Door2.SetActive(false);
        }
        else
        {
            Door1.SetActive(true);
            Door2.SetActive(true);
        }
    }
}
