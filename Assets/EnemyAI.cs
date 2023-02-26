using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BeatResponder
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject explosionVFX;
    private float timeLeft;
    private bool interval = true;
    private float pauseTime = 0.5f;
    [SerializeField] private float moveTime = 0.1f;

    private Animator animator;

    public override void Start()
    {
        base.Start();
        timeLeft = moveTime;
        endPosition = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        //animationComponent.clip.AddEvent(new AnimationEvent { time = animationComponent.clip.length, functionReference = new Object(), functionName = "RemoveGameObject" });
    }

    // Update is called once per frame
    void Update()
    {
        if(interval) 
        {
            Vector3 moveDir = (endPosition.position - transform.position).normalized;
            transform.position += moveDir * Time.deltaTime * speed;

            Timer(moveTime);
        }
    }

    public override void OnBeat()
    {
        interval = true;
    }

    private void MoveOnUpdateInterval()
    {
        if(interval) 
        {
            Vector3 moveDir = (endPosition.position - transform.position).normalized;
            transform.position += moveDir * Time.deltaTime * speed;

            Timer(pauseTime);
        }
        else
        {
            Timer(moveTime);
        }
    }

    private void Timer(float time)
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0f)
        {
            interval = !interval;
            timeLeft = time;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player collision");
            animator.SetTrigger("Explosion");
            //Destroy(gameObject);
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 0f);
        }
    }

    private void OnDestroy() 
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
    }
}
