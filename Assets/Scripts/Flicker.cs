using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    public float timeOn = 0.1f;
    private float changeTime = 0;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void Update()
    {
        if (Time.time > changeTime)
        {
            rend.enabled = !rend.enabled;
            if (rend.enabled)
            {
                changeTime = Time.time + timeOn;
            }
            else
            {
                changeTime = Time.time + timeOn;
            }
        }
    }

}


