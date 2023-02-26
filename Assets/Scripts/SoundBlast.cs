using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBlast : MonoBehaviour
{

    public float speed = 50.0f;
    public float lifeTime = 0.5f;
    public int minDamage = 1;
    private Rigidbody2D body;

    private int damage = 1;
    private float size = 1.0f;

    [SerializeField] int maxDamage = 10;
    [SerializeField] int maxPenetration = 5;
    [SerializeField] float maxSize = 5.0f;


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

    public void AdjustPower(float resonance)
    {
        damage = Mathf.Max(minDamage, Mathf.RoundToInt(resonance * resonance * maxDamage));
        transform.localScale = transform.localScale * Mathf.Max(1.0f, (resonance * resonance * maxSize));
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Deal damage to enemy
        }
        else
        {
            CleanUp();
        }
    }

}
