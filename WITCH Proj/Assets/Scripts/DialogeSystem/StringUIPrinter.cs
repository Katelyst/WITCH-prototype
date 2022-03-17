using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class StringUIPrinter : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI uiText;

    public void PrintToUI(string text)
    {
        uiText.text = text;
    }
}
