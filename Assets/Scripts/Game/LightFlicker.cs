using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [Header("Light Settings")]
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 1.5f; // Maximum light intensity
    public float flickerSpeed = 0.1f; // Speed of flickering (lower is faster)

    private float timer = 0f;
    private Light2D lightSource; // The light component to flicker

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
    }

    void Update()
    {
        if (lightSource != null)
        {
            // Randomize intensity over time
            timer += Time.deltaTime * flickerSpeed;
            lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(timer, 0f));
        }
    }
}
