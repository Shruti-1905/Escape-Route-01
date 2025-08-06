using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardCodePuzzle : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    private Collider colliderw;
    private string correctCode = "0001";
    private string currentInput = "";
    bool triggered = false;

    // void Update()
    // {
    //     OnTriggerEnter(colliderw);
    // }

    void AddDigit(string digit)
    {
        if (currentInput.Length < correctCode.Length)
        {
            currentInput += digit;
            UpdateDisplay();
        }
    }

    void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    void SubmitCode()
    {
        if (currentInput == correctCode)
        {
            displayText.text = "✅ Unlocked!";
            Debug.Log("Door opens!");
        }
        else
        {
            displayText.text = "❌ Wrong! Try again.";
            Debug.Log("Wrong code.");
            Invoke(nameof(ClearInput), 1.2f);
        }
    }

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("MainCamera")) && !triggered)
        {
            triggered = true;
            foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(k) && k >= KeyCode.Alpha0 && k <= KeyCode.Alpha9)
                {
                    string digit = k.ToString().Replace("Alpha", "");
                    AddDigit(digit);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitCode();
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ClearInput();
            }
        }
    }
}