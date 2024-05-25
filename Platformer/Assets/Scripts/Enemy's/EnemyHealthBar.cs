using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    [SerializeField] private Transform target;
    void Update()
    {
        transform.position = new Vector3(target.position.x,target.position.y, target.position.z);
    }
}
