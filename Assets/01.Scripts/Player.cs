using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
    Rigidbody2D rigid;
	CapsuleCollider2D boxCollider;

	RaycastHit2D hit;

	float x;
	float dir;

	float nowAngle;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<CapsuleCollider2D>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position + transform.up * (boxCollider.offset.y-0.01f), boxCollider.size);
	}
	private void FixedUpdate()
	{
		bool groundCheck = Physics2D.BoxCast(transform.position + transform.up * (boxCollider.offset.y - 0.01f), boxCollider.size, nowAngle, transform.right, 0.1f, LayerMask.GetMask("Ground"));
		bool groundCheck2 = directionRaycast(-transform.up);

		if (!groundCheck || !groundCheck2)
		{
			//transform.position -= transform.up * Time.deltaTime;
			rigid.velocity = -transform.up * 9.8f;
		}
	}
	void Update()
	{
		
		x = Input.GetAxisRaw("Horizontal")*speed;
		if (x > 0)
		{
			dir = 1f;
		}
		else
		{
			dir = -1;
		}

		transform.position += transform.right * x * Time.deltaTime;

		//else
		//{
		//}
		hit = Physics2D.Raycast(transform.position, transform.right*dir, 0.51f, LayerMask.GetMask("Ground"));
		if (hit.collider != null)
		{
			if (directionAngleRaycast(transform.right * dir, transform.up) != nowAngle)
			{
				nowAngle = directionAngleRaycast(transform.right * dir, transform.up * dir);
			}
			transform.rotation = Quaternion.Euler(0, 0, nowAngle);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
	}

	float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

	private float directionAngleRaycast(Vector3 dir,Vector3 downDir)
	{
		Debug.DrawRay(transform.position, dir * 0.51f, Color.red, 0.1f);
		if (hit.collider != null)
		{
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position - downDir * 0.1f, dir, 1f, LayerMask.GetMask("Ground"));
			Debug.DrawRay(transform.position - downDir * 0.1f, dir * 2f, Color.blue, 0.1f);
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
	private bool directionRaycast(Vector2 dir)
	{
		return Physics2D.Raycast(transform.position+(transform.right*-0.1f*this.dir), dir,0.51f,LayerMask.GetMask("Ground"));
	}
}
