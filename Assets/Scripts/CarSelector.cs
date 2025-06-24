using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarTouchSpawner : MonoBehaviour
{
    public GameObject car1Prefab;
    public GameObject car2Prefab;
    private GameObject spawnedCar;

    public int selectedCarIndex = 0; // 0 = car1, 1 = car2

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Destroy old car
                if (spawnedCar != null)
                    Destroy(spawnedCar);

                // Spawn selected car
                GameObject prefabToSpawn = selectedCarIndex == 0 ? car1Prefab : car2Prefab;
                spawnedCar = Instantiate(prefabToSpawn, hitPose.position, hitPose.rotation);
            }
        }
    }

    // Call from UI Buttons
    public void SelectCar1()
    {
        selectedCarIndex = 0;
    }

    public void SelectCar2()
    {
        selectedCarIndex = 1;
    }
}
