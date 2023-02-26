using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target) transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
