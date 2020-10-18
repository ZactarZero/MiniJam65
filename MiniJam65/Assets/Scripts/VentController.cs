using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider hitBox;

    void Start()
    {
        animator = GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc.hasScrewDriver)
            {
                Open();
            }
        }
    }

    public void Open()
    {
        animator.SetTrigger("Open");
        hitBox.enabled = false;
    }
}
