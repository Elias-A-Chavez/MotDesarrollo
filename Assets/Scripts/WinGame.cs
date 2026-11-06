using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    
    public InputActionProperty interactAction;

   
    [SerializeField] private string victorySceneName = "victory";

    private bool isPlayerInside = false;

    void OnEnable()
    {
        interactAction.action?.Enable();
    }

    void OnDisable()
    {
        interactAction.action?.Disable();
    }

    void Update()
    {
        
        if (isPlayerInside && interactAction.action != null && interactAction.action.WasPressedThisFrame())
        {
            LoadVictoryScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            
        }
    }

    private void LoadVictoryScene()
    {
        
        SceneManager.LoadScene(victorySceneName);
    }
}