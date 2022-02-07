using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("MoveChange", 5);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 front = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        //Debug.DrawRay(front, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if(rayHit.collider == null)
        {
            Turn();
        }
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
}
