using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private GameObject targetCar;

    public void SetTargetCar(GameObject car)
    {
        targetCar = car;
    }

    public void ToggleAllDoors()
    {
        ToggleDoor("S_LeftDoor_LOD2");
        ToggleDoor("S_RightDoor_LOD2");
    }

    private void ToggleDoor(string doorName)
    {
        Transform door = targetCar.transform.Find(doorName);
        if (door != null)
        {
            Animator anim = door.GetComponent<Animator>();
            if (anim != null)
            {
                bool isOpen = anim.GetBool("DoorisOpen");
                anim.SetBool("DoorisOpen", !isOpen);
            }
        }
    }
}
