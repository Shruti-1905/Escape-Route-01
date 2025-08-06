using Unity.VisualScripting;
using UnityEngine;

public class FindableObject : MonoBehaviour
{
    bool triggered = false;
    
    private void Onenter() // Or use trigger/collision if needed
    {
        ObjectFinderManager manager = FindAnyObjectByType<ObjectFinderManager>();
        if (manager != null)
        {
            manager.ObjectFound(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")||other.CompareTag("MainCamera"))&&!triggered)
        {
            triggered = true;
            Onenter();
        }
    }
}