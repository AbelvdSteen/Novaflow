using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float BPM;
    public bool hasStarted;
    public bool isPaused;
    void Start()
    {
        BPM = BPM / 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted && !isPaused)
        {
            transform.position -= new Vector3(BPM * Time.deltaTime, 0);
        }  
    }
}
