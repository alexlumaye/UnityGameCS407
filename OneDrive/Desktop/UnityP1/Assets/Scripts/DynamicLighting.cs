
using UnityEngine;

public class DynamicLighting : MonoBehaviour
{
    public Light2D globalLight;
    public float flickerSpeed = 1f;
    public float flickerIntensity = 0.1f;
    private float baseIntensity;

    void Start()
    {
        if (globalLight != null)
        {
            baseIntensity = globalLight.intensity;
        }
    }

    void Update()
    {
        if (globalLight != null)
        {
            globalLight.intensity = baseIntensity + Mathf.Sin(Time.time * flickerSpeed) * flickerIntensity;
        }
    }
}
