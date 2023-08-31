using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationBulletAttackObject : AttackObject
{
    private Vector3 dir;
    private float speed;
    private Vector3 startPoint;
    private float moveDistance;
    System.Action<EnemyController> attack;
    public void Initialize(Vector3 dir, Vector3 startPoint, float speed, float moveDistance, System.Action<EnemyController> attack)
    {
        this.dir = dir;
        this.speed = speed;
        this.startPoint = startPoint;
        this.moveDistance = moveDistance;
        this.attack = attack;
        rigidbody.position = startPoint;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    private void FixedUpdate()
    {
        if (moveDistance <= (startPoint - rigidbody.position).magnitude)
        {
            Restore();
        }
        else
        {

            MoveForward(dir, speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        attack(other.GetComponent<EnemyController>());
    }
}
