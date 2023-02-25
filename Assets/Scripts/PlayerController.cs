using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 mousePos;
    public Vector3 up = Vector3.up;

    public Vector3 playerPosition = new Vector3(0.5f, 0.5f, 0.0f);
    public Weapon equippedWeapon;
    public GameObject hold;

    public Vector3 aimVector = Vector3.up;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 screenPos = Input.mousePosition;
        aimVector = new Vector3(screenPos.x - playerPosition.x * Screen.width, screenPos.y - playerPosition.y * Screen.height, playerPosition.z);
        Vector3 direction = mousePos - transform.position;

        gameObject.GetComponent<SpriteRenderer>().flipX = (aimVector.x < 0);
        

        hold.transform.rotation = Quaternion.FromToRotation(Vector3.up, aimVector);

        if (Input.GetMouseButtonDown(0))
        {
            equippedWeapon.shoot(aimVector);
        }
    }
}
