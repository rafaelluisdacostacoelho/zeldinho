using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position,
                             transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
                                transform.position) > attackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     target.position, 
                                                     moveSpeed * Time.deltaTime);
        }
    }
}
