using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigid;
    private CapsuleCollider2D boxCollider;
    private Animator animator;
    SpriteRenderer sprite;

    public int nextMove;
    private RaycastHit2D hit;

    private float x;
    private float dir;


    private float nowAngle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("MoveChange", 3);
    }
    private void OnDrawGizmos()
    {
        if (boxCollider != null)
            Gizmos.DrawWireCube(transform.position + transform.up * (boxCollider.offset.y - 0.01f), boxCollider.size);
    }

    void FixedUpdate()
    {
        bool groundCheck = Physics2D.BoxCast(transform.position + transform.up * (boxCollider.offset.y - 0.01f), new Vector2(boxCollider.size.x / 2, boxCollider.size.y), transform.rotation.eulerAngles.z, transform.right, 0.1f, LayerMask.GetMask("Ground"));
        bool groundCheckBack = directionRaycast(-transform.up, (transform.right * -0.3f));
        bool groundCheckFront = directionRaycast(-transform.up, (transform.right * 0.3f));
        bool groundCheckMiddle = directionRaycast(-transform.up, transform.up * (boxCollider.offset.y - 0.1f));

        speed = Mathf.Clamp(speed, 2, 10);

        Vector3 move = transform.right * x;

        animator.SetBool("Move", x != 0);

        if (!groundCheck || (!groundCheckBack && !groundCheckFront) || !groundCheckMiddle)
        {
            move += -transform.up * 12f;
            //rigid.velocity = -transform.up * 9.8f;
        }
        rigid.velocity = move;

        if (x > 0)
        {
            dir = 1f;
            sprite.flipX = false;
        }
        else if (x < 0)
        {
            dir = -1;
            sprite.flipX = true;
        }

        hit = Physics2D.Raycast(transform.position, transform.right * 0.4f * dir, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null && x != 0)
        {
            print(hit.collider.gameObject);
            if (directionAngleRaycast(transform.right * dir, -transform.up * 0.45f) != nowAngle)
            {
                nowAngle = directionAngleRaycast(transform.right * dir, -transform.up * 0.45f * dir);
            }
            transform.rotation = Quaternion.Euler(0, 0, nowAngle);
        }
        //rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Vector2 front = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        ////Debug.DrawRay(front, Vector3.down, new Color(0, 1, 0));
        //RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Ground"));
        //if(rayHit.collider == null)
        //{
        //    Turn();
        //}
    }

    private void Update()
    {
        //x = Input.GetAxisRaw("Horizontal") * speed;
        x = nextMove * speed;
    }

    void MoveChange()
    {
        nextMove = Random.Range(-1, 2);

        float nextRandMove = Random.Range(2f, 5f);
        Invoke("MoveChange", nextRandMove);
    }

    void Turn()
    {
        nextMove *= -1;
        CancelInvoke();
        Invoke("MoveChange", 2);
    }
    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

    private float directionAngleRaycast(Vector3 dir, Vector3 downDir)
    {
        if (hit.collider != null)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + downDir, dir, 1.5f, LayerMask.GetMask("Ground"));
            if (hit2.collider != null)
            {
                float dirAngle = GetAngle(hit2.point, hit.point);
                return dirAngle;
            }
            else
            {
                return nowAngle;
            }
        }
        else
        {
            return nowAngle;
        }
    }
    private bool directionRaycast(Vector2 dir, Vector3 originPos)
    {
        return Physics2D.Raycast(transform.position + originPos, dir, 0.5f, LayerMask.GetMask("Ground"));
    }
}

