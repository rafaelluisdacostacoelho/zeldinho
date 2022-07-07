using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if ((Vector3.Distance(target.position,
                             transform.position) <= chaseRadius
            || boundary.bounds.Contains(target.transform.position))
            && Vector3.Distance(target.position,
                                transform.position) > attackRadius)
        {
            Debug.Log("ENTROU");
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk && currentState != EnemyState.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.Walk);
                anim.SetBool("WakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius
                 || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("WakeUp", false);
        }
    }
}
