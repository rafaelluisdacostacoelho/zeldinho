using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Animator")]
    public Animator anim;

    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    void Start()
    {
        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
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
