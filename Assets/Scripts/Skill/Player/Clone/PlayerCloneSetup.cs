using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneSetup : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.675f;

    private float cloneTimer;
    [SerializeField] private float losingColorSpeed = 1f;
    private Color oldColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        oldColor = sr.color;
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        sr.color = new Color(oldColor.r, oldColor.g, oldColor.b, sr.color.a - (Time.deltaTime * losingColorSpeed));
        if (cloneTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack, float _facingDir)
    {
        sr.color = oldColor;
        if (_canAttack)
            animator.SetInteger("AttackNumber", Random.Range(1, 3));
        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;
        transform.localScale = new Vector3(transform.localScale.x * _facingDir, transform.localScale.y, transform.localScale.z);
    }

    private void AnimationTrigger()
    {
        cloneTimer = -0.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
