using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger,
    Celebrate
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealt;
    public float Health;
    public string EnemyName;
    public int BaseAttack;
    public float moveSpeed;
    public Vector3 homePosition;
    public Transform roomTransform;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private readonly float deathEffectDelay = 1f;

    [Header("Death Signal")]
    public GameSignal roomSignal;

    private void Awake()
    {
        Health = maxHealt.initialValue;
    }

    private void OnEnable()
    {
        transform.position = homePosition + roomTransform.position;
    }

    private void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DeathEffect();
            roomSignal.Raise();
            gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }

    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
