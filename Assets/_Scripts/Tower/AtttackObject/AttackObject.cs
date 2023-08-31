using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackObject : MonoBehaviour
{

    [SerializeField] protected new Rigidbody rigidbody;
    [SerializeField] RecycleObject recycleObject;
    [SerializeField] AnimationCurve jumpEase;
    public Tween ParabolicAttack(Vector3 attackPoint, float objectSpeed)
    {
        return transform.DOJump(attackPoint, 3, 1, (transform.position - attackPoint).magnitude / objectSpeed).SetEase(jumpEase).SetLink(gameObject);
    }
    public void MoveForward(Vector3 dir, float speed)
    {
        rigidbody.position += dir * speed * Time.deltaTime;
    }
    public void Restore()
    {
        recycleObject.Restore();
    }
}
