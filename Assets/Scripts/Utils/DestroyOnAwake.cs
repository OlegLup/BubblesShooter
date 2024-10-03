using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class DestroyOnAwake : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
