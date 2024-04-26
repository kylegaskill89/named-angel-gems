using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBillboard : MonoBehaviour
{
    public Camera thisCam;

    // Start is called before the first frame update
    void Start()
    {
        thisCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(thisCam.transform);
        
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
