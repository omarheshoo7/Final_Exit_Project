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

    public void ToggleDoor(string doorName)
    {
        Transform door = targetCar.transform.Find(doorName);
        if (door != null)
        {
            Animator anim = door.GetComponent<Animator>();
            if (anim != null)
            {
                bool isOpen = anim.GetBool("Open");
                anim.SetBool("Open", !isOpen);
            }
        }
    }
}
