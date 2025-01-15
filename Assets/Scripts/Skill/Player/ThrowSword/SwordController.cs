using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    public void SetupSword(Vector2 _dir, float _gravityScale)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
    }
}
