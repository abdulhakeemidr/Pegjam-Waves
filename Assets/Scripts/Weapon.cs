using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject projectile;
    public GameObject muzzlePosition;

    //The amount of time (seconds) that it takes before you can shoot again;
    public float loadDelay = 0.25f;
    
    [SerializeField] bool readyToShoot = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(Vector3 aim)
    {
        if (readyToShoot)
        {
            GameObject bullet = Instantiate(projectile, (muzzlePosition.transform.position), projectile.transform.rotation);
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, aim);
            bullet.transform.Rotate(Vector3.forward, 90.0f);
            bullet.SetActive(true);
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(loadDelay);
        readyToShoot = true;
    }

    
}
