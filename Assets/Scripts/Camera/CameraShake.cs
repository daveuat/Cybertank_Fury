using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    public void Shake()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
