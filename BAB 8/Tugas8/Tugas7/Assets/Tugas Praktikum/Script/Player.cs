using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  Rigidbody2D rb;

  [SerializeField] Transform groundcheckCollider;
  [SerializeField] LayerMask groundLayer;

  const float groundCheckRadius = 0.2f; // +
  [SerializeField] float speed = 1;
  [SerializeField] float jumpPower = 100;

  float horizontalValue;

  [SerializeField] bool isGrounded; // +
  bool facingRight;
  bool jump;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update ()
  {
    horizontalValue = Input.GetAxisRaw("Horizontal");
    if (Input.GetButtonDown("Jump"))
      jump = true;
    else if (Input.GetButtonUp("Jump"))
      jump = false;
  }

  void FixedUpdate()
  {
    GroundCheck();
    Move(horizontalValue, jump);
  }

  void GroundCheck()
  {
    isGrounded = false;
    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundcheckCollider.position, groundCheckRadius, groundLayer);
      if (colliders.Length > 0)
      isGrounded = true;
  }

  void Move(float dir, bool jumpflag)
  {
    #region gerak kanan kiri
    float xVal = dir * speed * 100 * Time.fixedDeltaTime;
    Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
    rb.velocity = targetVelocity;

    if(isGrounded && jumpflag)
    {
      isGrounded = false;
      jumpflag = false;
      rb.AddForce(new Vector2(0f, jumpPower));
    }
    if (facingRight && dir < 0)
    {
      // ukuran player
      transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
      facingRight = false;
    }

    else if (!facingRight && dir > 0)
    {
      // ukuran player
      transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
      facingRight = true;
    }

    #endregion
  }
}
