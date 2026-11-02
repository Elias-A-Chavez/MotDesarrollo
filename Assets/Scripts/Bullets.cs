using UnityEngine;

public class Bullets : MonoBehaviour, IInteractable



{
    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        Destroy(gameObject, 5);
    }

 
    void Update()
    {
        transform.Translate(40 * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
