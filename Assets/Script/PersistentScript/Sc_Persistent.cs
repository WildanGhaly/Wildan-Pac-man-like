using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Persistent : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
