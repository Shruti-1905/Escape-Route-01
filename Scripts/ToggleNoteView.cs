using UnityEngine;
using UnityEngine.UI;

public class ToggleNoteViewer : MonoBehaviour
{
    public GameObject notePanel; // UI Panel or Text object
    public KeyCode toggleKey = KeyCode.Space;

    private bool isVisible = false;

    void Start()
    {
        if (notePanel != null)
            notePanel.SetActive(false); // Start hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isVisible = !isVisible;
            if (notePanel != null)
                notePanel.SetActive(isVisible);
        }
    }
}

