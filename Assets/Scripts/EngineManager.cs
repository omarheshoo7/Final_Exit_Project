using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EngineManager : MonoBehaviour
{
    public Button startStopButton;
    public TextMeshProUGUI buttonText;
    private GameObject targetCar;

    private AudioSource engineAudio;
    private bool isEngineOn = false;

    public void SetTargetCar(GameObject car)
    {
        targetCar = car;
        engineAudio = targetCar.GetComponent<AudioSource>();

        // Ensure audio is stopped initially
        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.Stop();
        }

        isEngineOn = false;
        UpdateButtonText();
    }

    void Start()
    {
        if (startStopButton != null)
        {
            startStopButton.onClick.AddListener(ToggleEngine);
        }
        UpdateButtonText();
    }

    void ToggleEngine()
    {
        if (targetCar == null || engineAudio == null)
            return;

        isEngineOn = !isEngineOn;

        if (isEngineOn)
        {
            engineAudio.Play();
        }
        else
        {
            engineAudio.Stop();
        }

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isEngineOn ? "Stop Engine" : "Start Engine";
        }
    }
}
