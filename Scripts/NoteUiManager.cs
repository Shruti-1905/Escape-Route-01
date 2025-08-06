using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteUiManager : MonoBehaviour
{
    public static NoteUiManager Instance;
    public GameObject notePanel;
    public TextMeshProUGUI noteText;
    bool triggered = false;
    public string noteID;

    [System.Serializable]
    public class Note
    {
        public string id;
        [TextArea(3, 10)]
        public string content;
    }

    public Note[] notes;

    void Awake()
    {
        if (Instance == null) Instance = this;
        Invoke(nameof(HideNotes), 2f);
    }

    public void ShowNote(string id)
    {
        foreach (Note n in notes)
        {
            if (n.id == id)
            {
                noteID = n.id;
                notePanel.SetActive(true);
                noteText.text = n.content;
                Debug.Log("true");
                return;
            }
        }
        Debug.LogWarning("Note not found: " + id);
    }

    public void HideNotes()
    {
        notePanel.SetActive(false);
        Invoke(nameof(HideNotes), 10f);
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player")&&!triggered)
    //     {
    //         triggered = true;
    //         ShowNote(noteID);
    //     }
    // }
}

