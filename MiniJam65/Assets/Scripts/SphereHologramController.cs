using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereHologramController : MonoBehaviour
{
    public float timeToChange;
    private Material mat;
    private float changeTime;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        changeTime = 0f;
        mat.SetColor("_EmissionColor", Color.red * 0);
    }

    void Update()
    {
        if (Time.time > changeTime)
        {
            if (mat.GetColor("_EmissionColor") == Color.red * 0)
            {
                mat.SetColor("_EmissionColor", Color.red * 1);
            }
            else
            {
                mat.SetColor("_EmissionColor", Color.red * 0);
            }
            changeTime = Time.time + timeToChange;
        }
        
    }
}
