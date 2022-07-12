using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentLog : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public override void CheckDistance()
    {

        if (currentState == EnemyState.Celebrate)
        {
            return;
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                 && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.Idle
                || currentState == EnemyState.Walk
                && currentState != EnemyState.Stagger)
            {
                if (canFire)
                {
                    Vector3 temp = target.transform.position - transform.position;
                    GameObject rockProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                    rockProjectile.GetComponent<RockProjectile>().Launch(temp);
                    canFire = false;
                    ChangeState(EnemyState.Walk);
                    anim.SetBool("WakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false);
        }
    }
}
