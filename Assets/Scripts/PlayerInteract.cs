using Mono.Cecil;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        IInteractable iinteract = collision.gameObject.GetComponent<IInteractable>();
        {
            if (iinteract != null)
            {
                iinteract.Interact();
            }
        }
    }
}
