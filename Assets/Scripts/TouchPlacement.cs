using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class TouchPlacement : MonoBehaviour
{
    [Header("Car Prefabs")]
    public GameObject car1Prefab;
    public GameObject car2Prefab;

    private GameObject selectedCarPrefab;
    private GameObject spawnedCar;

    [Header("Assign UI and Managers")]
    public GameObject carUIPanel;
    public CarColorManager colorManager;
    public TyreColorManager tyreManager;
    public EngineManager engineManager;
    public DoorManager doorManager;
    public TurntableRotator turntable;

    [Header("Voice Over Clips")]
    public AudioClip car1VoiceOver;
    public AudioClip car2VoiceOver;

    private AudioSource audioSource;

#if UNITY_EDITOR
    public Camera editorCamera;
#endif

    private ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
        selectedCarPrefab = car1Prefab; // default selection
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = editorCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PlaceCar(hit.point);
            }
        }
#else
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                PlaceCar(hitPose.position);
            }
        }
#endif
    }

    void PlaceCar(Vector3 position)
    {
        if (spawnedCar != null)
            Destroy(spawnedCar);

        Vector3 adjustedPosition = position + new Vector3(0f, 0.05f, 0f); // Raise slightly above plane
        spawnedCar = Instantiate(selectedCarPrefab, adjustedPosition, Quaternion.identity);

        // Assign references to managers
        colorManager?.SetTargetCar(spawnedCar);
        tyreManager?.SetTargetCar(spawnedCar);
        engineManager?.SetTargetCar(spawnedCar);
        doorManager?.SetTargetCar(spawnedCar);
        turntable?.SetTargetCar(spawnedCar);

        if (carUIPanel != null)
            carUIPanel.SetActive(true);

        // Set canvas worldCamera
        Canvas[] canvases = spawnedCar.GetComponentsInChildren<Canvas>();
        Camera cam = Camera.main;
        foreach (Canvas c in canvases)
        {
            if (c.renderMode == RenderMode.WorldSpace)
                c.worldCamera = cam;
        }
    }

    // 🚗 Car Selector Buttons
    public void SelectCar1()
    {
        selectedCarPrefab = car1Prefab;
    }

    public void SelectCar2()
    {
        selectedCarPrefab = car2Prefab;
    }

    // 🔊 Voiceover Playback
    public void PlayVoiceOver()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            return;
        }

        if (selectedCarPrefab == car1Prefab && car1VoiceOver != null)
            audioSource.PlayOneShot(car1VoiceOver);
        else if (selectedCarPrefab == car2Prefab && car2VoiceOver != null)
            audioSource.PlayOneShot(car2VoiceOver);
    }
}
