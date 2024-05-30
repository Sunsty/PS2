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
    [SerializeField] int patternToGo;

    int index;

    private void Awake()
    {
        GetComponent<TextMeshPro>().fontSize = fontSize;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            index++;
        }

        if (texts[index] == "X")
        {

            if (gameObject.GetComponentInParent<Boss2Range_Patterns>() == null && gameObject.GetComponentInParent<Boss1_Patterns>() == null && gameObject.GetComponentInParent<Boss2Melee_Patterns>() == null) 
            {
                gameObject.GetComponentInParent<BossTuto_Patterns>().currentPattern = patternToGo;
            }

            if (gameObject.GetComponentInParent<Boss2Range_Patterns>() == null && gameObject.GetComponentInParent<BossTuto_Patterns>() == null && gameObject.GetComponentInParent<Boss2Melee_Patterns>() == null) 
            {
                gameObject.GetComponentInParent<Boss1_Patterns>().currentPattern = patternToGo;
            }

            if (gameObject.GetComponentInParent<Boss1_Patterns>() == null && gameObject.GetComponentInParent<BossTuto_Patterns>() == null && gameObject.GetComponentInParent<Boss2Melee_Patterns>() == null) 
            {
                gameObject.GetComponentInParent<Boss2Range_Patterns>().currentPattern = patternToGo;
            }

            if (gameObject.GetComponentInParent<Boss1_Patterns>() == null && gameObject.GetComponentInParent<BossTuto_Patterns>() == null && gameObject.GetComponentInParent<Boss2Range_Patterns>() == null) 
            {
                gameObject.GetComponentInParent<Boss2Melee_Patterns>().currentPattern = patternToGo;
            }

            Destroy(gameObject);
        }

        GetComponent<TextMeshPro>().text = texts[index];
    }
}
