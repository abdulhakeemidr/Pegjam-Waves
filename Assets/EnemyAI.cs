using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BeatResponder
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private float speed = 1f;
    private float timeLeft;
    private bool interval = true;
    private float pauseTime = 0.5f;
    private float playTime = 0.2f;

    public Vector3 _direction = Vector3.left;
    private int _speed = 4;
    
    public override void OnBeat()
    {
        _direction = _direction == Vector3.left ? Vector3.right : Vector3.left;
        transform.position += _direction * _speed;
    }

    void Start()
    {
        timeLeft = playTime;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Timer(playTime);
        }
    }

    private void Timer(float time)
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0f)
        {
            interval = !interval;
            Debug.Log(interval);
            timeLeft = time;
        }
    }

    public IEnumerator Change()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
        }
    }
}
