using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class TouchPlacement : MonoBehaviour
{
    public GameObject carPrefab;
    private GameObject spawnedCar;

    [Header("Assign UI and Managers")]
    public GameObject carUIPanel;
    public CarColorManager colorManager;
    public TyreColorManager tyreManager;
    public EngineManager engineManager;
    public DoorManager doorManager;
    public TurntableRotator turntable;

#if UNITY_EDITOR
    public Camera editorCamera;
#endif

    private ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
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

        spawnedCar = Instantiate(carPrefab, position, Quaternion.identity);

        // ✅ Set target car in each manager
        colorManager?.SetTargetCar(spawnedCar);
        tyreManager?.SetTargetCar(spawnedCar);
        engineManager?.SetTargetCar(spawnedCar);
        doorManager?.SetTargetCar(spawnedCar);
        turntable?.SetTargetCar(spawnedCar);

        if (carUIPanel != null) carUIPanel.SetActive(true);

        // ✅ Fix world-space canvases
        Canvas[] canvases = spawnedCar.GetComponentsInChildren<Canvas>();
        Camera cam = Camera.main;
        foreach (Canvas c in canvases)
        {
            if (c.renderMode == RenderMode.WorldSpace)
                c.worldCamera = cam;
        }
    }
}
