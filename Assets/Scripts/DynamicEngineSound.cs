using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DynamicEngineSound : MonoBehaviour
{
    public float maxVolumeDistance = 5f;     // Full volume within this distance
    public float minVolumeDistance = 40f;    // Minimum audible volume beyond this
    public float maxVolume = 1f;
    public float minVolume = 0.1f;           // 🔊 Never fully silent

    private Transform playerCamera;
    private AudioSource engineAudio;

    void Start()
    {
        engineAudio = GetComponent<AudioSource>();
        playerCamera = Camera.main?.transform;
    }

    void Update()
    {
        if (playerCamera == null || engineAudio == null) return;

        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // Clamp volume based on distance
        if (distance <= maxVolumeDistance)
        {
            engineAudio.volume = maxVolume;
        }
        else if (distance >= minVolumeDistance)
        {
            engineAudio.volume = minVolume;
        }
        else
        {
            // Linear interpolation between max and min volume
            float t = Mathf.InverseLerp(minVolumeDistance, maxVolumeDistance, distance);
            engineAudio.volume = Mathf.Lerp(minVolume, maxVolume, 1f - t);
        }
    }
}
