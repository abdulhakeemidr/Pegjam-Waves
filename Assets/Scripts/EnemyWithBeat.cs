using System.Collections;
using UnityEngine;

public class EnemyWithBeat : BeatResponder
{
    public Vector3 _direction = Vector3.left;
    private int _speed = 4;
    
    public override void OnBeat()
    {
        _direction = _direction == Vector3.left ? Vector3.right : Vector3.left;
        transform.position += _direction * _speed;
    }
}