using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble_Behavior : MonoBehaviour
{
    [SerializeField] string[] texts;

    [Space(20)]

    [Header("Settings"), Space(10)]

    [SerializeField] float waitTime;
    [SerializeField] float fontSize;

    private void Awake()
    {
        GetComponent<TextMeshPro>().fontSize = fontSize;

        for (int i = 0; i < texts.Length; i++)
        {
            StartCoroutine(DisplayText(i, texts[i]));
        }
    }

    IEnumerator DisplayText(float i, string text)
    {
        yield return new WaitForSeconds(i * waitTime);

        GetComponent<TextMeshPro>().text = text;
    }
}
