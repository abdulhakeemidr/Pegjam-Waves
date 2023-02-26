using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector3 mousePos;
    public Vector3 up = Vector3.up;

    public Vector3 playerPosition = new Vector3(0.5f, 0.5f, 0.0f);
    public float runSpeed = 5.0f;
    public float acceleration = 1f;
    public float currSpeed = 0;
    public Weapon equippedWeapon;
    public GameObject hold;

    public Vector3 aimVector = Vector3.up;
    public float synchronisty = 0.5f;

    private Conductor conductor;
    private Unit _unit;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        _unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_unit.IsAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate acceleration
            if (horizontalInput != 0 || verticalInput != 0)
            {
                currSpeed = Mathf.Min(currSpeed + acceleration, runSpeed);
            }
            else
            {
                currSpeed = 0f;
            }

            // Move the GameObject based on input and current speed
            transform.position += new Vector3(horizontalInput, verticalInput, 0) * currSpeed * Time.deltaTime;
            
            Vector3 screenPos = Input.mousePosition;
            aimVector = new Vector3(screenPos.x - playerPosition.x * Screen.width,
                screenPos.y - playerPosition.y * Screen.height, playerPosition.z);
            gameObject.GetComponent<SpriteRenderer>().flipX = (aimVector.x < 0);
            hold.transform.rotation = Quaternion.FromToRotation(Vector3.up, aimVector);

            if (Input.GetMouseButtonDown(0))
            {
                equippedWeapon.shoot(aimVector, conductor.BeatOffset + 0.5f);
            }
        }
    }
}
