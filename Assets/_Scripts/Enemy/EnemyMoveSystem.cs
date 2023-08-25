using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class EnemyMoveSystem : MonoBehaviour
{
    [SerializeField] new Rigidbody rigidbody;
    public float moveSpeed;
    public float rotateSpeed;
    Vector3[] pathPositions;
    [SerializeField] IntReactiveProperty pointIndex;
    [SerializeField] int maxPointIndex;
    float nextPointCheckDistance = 0.05f;
    public void Initialize(System.Action playerLifeDecrease)
    {
        pointIndex = new IntReactiveProperty();
        pathPositions = PathTileList.Instance.PathPositions;
        maxPointIndex = pathPositions.Length;
        pointIndex.Value = 1;
        pointIndex.DistinctUntilChanged().Subscribe(x =>
        {
            if (x == maxPointIndex)
            {
                playerLifeDecrease.Invoke();
            }
        });
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + (pathPositions[pointIndex.Value] - rigidbody.position).normalized * Time.deltaTime * moveSpeed);
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, Quaternion.LookRotation((pathPositions[pointIndex.Value] - rigidbody.position).normalized), Time.deltaTime * rotateSpeed);
        if ((pathPositions[pointIndex.Value] - rigidbody.position).magnitude <= nextPointCheckDistance)
        {
            pointIndex.Value++;
        }
    }
}
