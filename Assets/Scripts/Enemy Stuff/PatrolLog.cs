using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, 
                             transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, 
                             transform.position) > attackRadius)
        {
            if (currentState == EnemyState.Idle ||
                currentState == EnemyState.Walk &&
                currentState != EnemyState.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                   target.position,
                                                   moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                anim.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position,
                                  transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, 
                                 path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                   path[currentPoint].position,
                                                   moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[currentPoint];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
