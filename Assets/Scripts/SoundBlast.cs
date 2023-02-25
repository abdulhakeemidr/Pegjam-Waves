using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBlast : MonoBehaviour
{

    public float speed = 50.0f;
    public float lifeTime = 0.5f;
    public int damage = 1;
    private Rigidbody2D body;
    


    private void OnEnable()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        body.AddRelativeForce(new Vector2(1.0f, 0.0f) * speed, ForceMode2D.Impulse);
        Invoke("CleanUp", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
