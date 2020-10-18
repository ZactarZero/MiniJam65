using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    public WhatToPickup whatToPickup;

    public override string Interact()
    {
        switch (whatToPickup)
        {
            case WhatToPickup.Screwdriver:
                Debug.Log("Picked up screwdriver");
                Destroy(gameObject);
                return "screwdriver";
            case WhatToPickup.Keycard:
                Debug.Log("Picked up keycard");
                Destroy(gameObject);
                return "keycard";
            case WhatToPickup.Password:
                Debug.Log("Picked up password");
                return "password";
            case WhatToPickup.Eye:
                Debug.Log("Picked up eye");
                Destroy(gameObject);
                return "eye";
            case WhatToPickup.Gun:
                Debug.Log("Picked up gun");
                Destroy(gameObject);
                return "gun";
            default:
                return "";
        }
    }
}

public enum WhatToPickup
{
    Screwdriver,
    Keycard,
    Password,
    Eye,
    Gun
}
