using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGateScript : MonoBehaviour
{
    public float Health;
    public float GateType = 1;
    
    void Start()
    {
        if (GateType == 1)
            Health = 100;
    }

    
    void Update()
    {
        if (Health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
