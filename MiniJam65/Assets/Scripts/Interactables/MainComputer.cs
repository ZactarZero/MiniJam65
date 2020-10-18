using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComputer : Interactable
{
    public DoorsController podDoors;

    public override string Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        podDoors.OpenDoors();
        return "";
    }
}
