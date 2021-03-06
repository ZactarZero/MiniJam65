﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    public OpeningType ot;
    private Animator animator;
    private BoxCollider hitBox;

    private PlayerController pc;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hitBox = GetComponent <BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pc = other.GetComponent<PlayerController>();
            switch (ot)
            {
                case OpeningType.Regular:
                    OpenDoors();
                    break;
                case OpeningType.Keycard:
                    if (pc.hasFirstKeycard)
                    {
                        OpenDoors();
                    }
                    break;
                case OpeningType.Password:
                    if (pc.hasPassword)
                    {
                        OpenDoors();
                    }
                    break;
                case OpeningType.EyeScan:
                    if (pc.hasEye)
                    {
                        OpenDoors();
                    }
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
                    if (pc.hasFirstKeycard)
                    {
                        CloseDoors();
                    }
                    break;
                case OpeningType.Password:
                    if (pc.hasPassword)
                    {
                        CloseDoors();
                    }
                    break;
                case OpeningType.EyeScan:
                    if (pc.hasEye)
                    {
                        CloseDoors();
                    }
                    break;
            }
        }
    }

    public void OpenDoors()
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
    EyeScan,
    Pod
}