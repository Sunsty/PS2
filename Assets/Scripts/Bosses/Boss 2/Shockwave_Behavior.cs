using System.Collections;
using UnityEngine;

public class Shockwave_Behavior : MonoBehaviour
{
    [SerializeField] GameObject cameraShake;

    [SerializeField] GameObject groundWave;
    [SerializeField] int shockwaveLenght;
    [SerializeField] float posOffset;
    [SerializeField] float timeOffset;
    [SerializeField] float shakeDuration;
    private bool shockwave;
    

    private void Awake()
    {
        cameraShake = GameObject.FindGameObjectWithTag("Camera Shake");

        shockwave = true;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            CameraShake(1f, 0.7f, 1f);

            if (shakeDuration < 0)
            {
                shakeDuration = 0;
            }
        }

        if (shockwave)
        {
            for (int i = 1; i <= shockwaveLenght; i++)
            {
                StartCoroutine(Waiter(i));
                if (i == shockwaveLenght)
                {
                    StartCoroutine(DestroyEnd(shockwaveLenght));
                }
            }
            shockwave = false;
        }
    }

    IEnumerator Waiter(float i)
    {
        yield return new WaitForSeconds(i / timeOffset);

        Vector3 offset = new Vector3(i * posOffset, 0, 0);
        Instantiate(groundWave, transform.position + offset, Quaternion.identity);
        Instantiate(groundWave, transform.position - offset, Quaternion.identity);

    }

    IEnumerator DestroyEnd(float i)
    {
        yield return new WaitForSeconds(i / timeOffset);

        Destroy(gameObject);
    }

    private void CameraShake(float shake, float shakeAmount, float decreaseFactor)
    {

        if (shake > 0)
        {
            cameraShake.transform.localPosition = Random.insideUnitCircle * shakeAmount;
            
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0f;
        }

    }
}
