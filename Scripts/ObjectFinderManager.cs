using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ObjectFinderManager : MonoBehaviour
{
    public string findableTag = "Findable";
    public TextMeshProUGUI messageText; // Assign a UI Text (e.g. TextMeshProUGUI or Text)

    private List<GameObject> allObjects = new List<GameObject>();
    private int foundCount = 0;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(findableTag);
        allObjects.AddRange(objs);
        foundCount = 0;

        if (messageText != null)
            messageText.text = "";
    }

    public void ObjectFound(GameObject obj)
    {
        if (allObjects.Contains(obj))
        {
            allObjects.Remove(obj);
            foundCount++;

            // Optionally disable or destroy the object
            obj.SetActive(false);

            if (allObjects.Count == 0)
            {
                if (messageText != null)
                    messageText.text = "Key Found!";
                Debug.Log("Key Found!");
            }
        }
    }
}
