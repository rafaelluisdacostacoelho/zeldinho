using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                                                 target.position.y,
                                                 transform.position.z);

            targetPosition.x = Mathf.Clamp(target.position.x,
                                           minPosition.x,
                                           maxPosition.x);

            targetPosition.y = Mathf.Clamp(target.position.y,
                                           minPosition.y,
                                           maxPosition.y);

            transform.position = Vector3.Lerp(transform.position,
                                              targetPosition,
                                              smoothing);
        }
    }

    public void BeginKick()
    {
        anim.SetBool("KickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("KickActive", false);
    }
}
