using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public OpeningType ot;
    private Animator animator;
    private BoxCollider hitBox;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hitBox = GetComponent <BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (ot)
            {
                case OpeningType.Regular:
                    OpenDoors();
                    break;
                case OpeningType.Keycard:

                    break;
                case OpeningType.Password:

                    break;
                case OpeningType.EyeScan:

                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (ot)
            {
                case OpeningType.Regular:
                    CloseDoors();
                    break;
                case OpeningType.Keycard:
                    
                    break;
                case OpeningType.Password:

                    break;
                case OpeningType.EyeScan:

                    break;
            }
        }
    }

    private void OpenDoors()
    {
        animator.SetTrigger("Open");
        hitBox.enabled = false;
    }

    private void CloseDoors()
    {
        animator.SetTrigger("Close");
        hitBox.enabled = true;
    }
}

public enum OpeningType
{
    Regular,
    Keycard,
    Password,
    EyeScan
}