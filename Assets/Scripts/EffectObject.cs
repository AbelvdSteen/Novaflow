using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 0.5f;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
