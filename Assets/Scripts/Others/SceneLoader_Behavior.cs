using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Behavior : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(Transition(sceneIndex));
    }

    IEnumerator Transition(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneIndex);

        transition.SetBool("Start", false);
    }
}
