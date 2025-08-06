using UnityEngine;

public class PatternMatchGame : MonoBehaviour
{
    [System.Serializable]
    public class PatternSlot
    {
        public SpriteRenderer renderer; 
        public Sprite[] patternSprites; 
        [HideInInspector] public int currentIndex = 0;
    }

    public PatternSlot[] slots; 
    public int[] correctPattern; 
    public GameObject doorToUnlock; 

    private bool isUnlocked = false;

    void Start()
    {
        foreach (var slot in slots)
        {
            slot.currentIndex = 0;
            slot.renderer.sprite = slot.patternSprites[0];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isUnlocked)
        {
            Vector2 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var slot in slots)
            {
                if (slot.renderer.bounds.Contains(mousePos))
                {
                    CyclePattern(slot);
                    CheckPatternMatch();
                    break;
                }
            }
        }
    }

    void CyclePattern(PatternSlot slot)
    {
        slot.currentIndex = (slot.currentIndex + 1) % slot.patternSprites.Length;
        slot.renderer.sprite = slot.patternSprites[slot.currentIndex];
    }

    void CheckPatternMatch()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].currentIndex != correctPattern[i])
                return;
        }

        isUnlocked = true;
        UnlockDoor();
    }

    void UnlockDoor()
    {
        if (doorToUnlock != null)
        {
            doorToUnlock.SetActive(false); 
            Debug.Log("Door unlocked!");
        }
    }
}