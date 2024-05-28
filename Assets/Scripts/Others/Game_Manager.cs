using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject[] element;

    void Awake()
    {
        foreach (var item in element)
        {
            DontDestroyOnLoad(item);
        }
    }
}
