using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 mousePos;
    public Vector3 up = Vector3.up;
    public GameObject aimPoint;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Input.mousePosition;
        mousePos = new Vector3(screenPos.x - 0.5f * Screen.width, screenPos.y - 0.5f * Screen.height, 0.0f);
        Vector3 direction = mousePos - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        
    }
}
