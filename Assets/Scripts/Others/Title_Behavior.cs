using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Behavior : MonoBehaviour
{
    float clock;
    bool active;

    void Start()
    {
        active = true;    
    }

    void Update()
    {
        if (active)
        {
            clock += 0.05f * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(255,255,255,clock * 10);
        }
    }
}
