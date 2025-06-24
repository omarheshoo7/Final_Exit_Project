using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColorManager : MonoBehaviour
{
    private GameObject targetCar;

    [Header("Assign Car Body Materials")]
    public Material Car_Black;
    public Material Car_White;
    public Material Car_Red;
    public Material Car_Blue;
    public Material Car_Green;
    public Material Car_Orange;

    public void SetTargetCar(GameObject car)
    {
        targetCar = car;
    }

    public void ApplyBlack() => ApplyMaterial(Car_Black);
    public void ApplyWhite() => ApplyMaterial(Car_White);
    public void ApplyRed() => ApplyMaterial(Car_Red);
    public void ApplyBlue() => ApplyMaterial(Car_Blue);
    public void ApplyGreen() => ApplyMaterial(Car_Green);
    public void ApplyOrange() => ApplyMaterial(Car_Orange);

    private void ApplyMaterial(Material mat)
    {
        if (targetCar == null || mat == null)
        {
            Debug.LogWarning("Missing car or material.");
            return;
        }

        // Only apply color to the main car body (Element 2 = Body1 in your prefab)
        MeshRenderer meshRenderer = targetCar.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material[] mats = meshRenderer.materials;

            if (mats.Length > 2)
            {
                mats[2] = mat; // Element 2 is the car body (Body1)
                meshRenderer.materials = mats;

                Debug.Log("✅ Car body color changed to: " + mat.name);
            }
            else
            {
                Debug.LogWarning("Not enough materials on the MeshRenderer.");
            }
        }
        else
        {
            Debug.LogWarning("No MeshRenderer found on car root object.");
        }
    }
}
