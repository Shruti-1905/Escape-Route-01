using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string text1;
    private Timer timer;

    void Start()
    {
        text.SetText(text1);
        Invoke(nameof(HideText), 10f);
    }
    void HideText()
    {
        text.gameObject.SetActive(false);
    }
}
