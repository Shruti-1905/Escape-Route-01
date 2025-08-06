using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    //public GameObject puzzleUI;
    public string noteID;

    bool triggered = false;
    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")||other.CompareTag("MainCamera"))&&!triggered)
        {
            triggered = true;
            //puzzleUI.SetActive(true); // Activates puzzle
            NoteUiManager.Instance.ShowNote(noteID); // Show related note
        }
    }
}
