using System.Collections.Generic;
using UnityEngine;

public class TyreColorManager : MonoBehaviour
{
    public Material Tyre_Black, Tyre_Silver, Tyre_Red, Tyre_Blue, Tyre_Green, Tyre_Orange;

    private GameObject currentCar; // Reference to the car

    private List<string> tyreNames = new List<string> { "FR", "FL", "RR", "RL" };

    // ✅ This is the missing method causing your compiler error
    public void SetTargetCar(GameObject car)
    {
        currentCar = car;
    }

    public void ChangeTyreColor(Material tyreMaterial)
    {
        if (currentCar == null)
        {
            Debug.LogWarning("No car assigned to TyreColorManager.");
            return;
        }

        foreach (string tyreName in tyreNames)
        {
            Transform tyre = currentCar.transform.Find(tyreName);
            if (tyre != null)
            {
                var renderer = tyre.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = tyreMaterial;
                }
            }
        }

        Debug.Log("Changed tyre color to: " + tyreMaterial.name);
    }
}
