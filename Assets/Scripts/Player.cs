using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;

	[SerializeField]
	public float hp;
	[SerializeField]
	public float maxHp;
	private bool hpDown = true;

	private Rigidbody2D rigid;
	private CapsuleCollider2D boxCollider;
	private Animator animator;
	SpriteRenderer sprite;

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
	}
	private void Start()
	{
		hp = maxHp;
	}

	private void OnDrawGizmos()
	{
		if (boxCollider != null)
			Gizmos.DrawWireCube(transform.position + transform.up * (boxCollider.offset.y - 0.01f), boxCollider.size);
	}
	private void FixedUpdate()
	{
		bool groundCheck = Physics2D.BoxCast(transform.position + transform.up * (boxCollider.offset.y - 0.01f), boxCollider.size, transform.rotation.eulerAngles.z, transform.right, 0.1f, LayerMask.GetMask("Ground"));
		bool groundCheckBack = directionRaycast(-transform.up, (transform.right * -0.2f ));
		bool groundCheckFront = directionRaycast(-transform.up, (transform.right * 0.2f ));
		bool groundCheckMiddle = directionRaycast(-transform.up, transform.up * (boxCollider.offset.y - 0.1f));
		if (hpDown)
		{
			hp -= Time.deltaTime;
			hp = Mathf.Clamp(hp, 0, maxHp);
		}
		else
		{
			hp += Time.deltaTime * maxHp * 0.1f;
			hp = Mathf.Clamp(hp, 0, maxHp);
		}


		speed = (hp / maxHp) * 10;
		speed = Mathf.Clamp(speed, 2, 10);

		rigid.velocity = transform.right*x;
		animator.SetBool("Move", x != 0);

		if (!groundCheck || (!groundCheckBack && !groundCheckFront)|| !groundCheckMiddle)
		{
			rigid.velocity = -transform.up * 9.8f;
		}

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

		hit = Physics2D.Raycast(transform.position, transform.right*0.4f * dir, 0.51f, LayerMask.GetMask("Ground"));
		if (hit.collider != null && x != 0)
		{
			if (Input.GetKey(KeyCode.Space))
			{

			}
			else
			{
				if (directionAngleRaycast(transform.right * dir, -transform.up*0.45f) != nowAngle)
				{
					nowAngle = directionAngleRaycast(transform.right * dir, -transform.up*0.45f * dir);
				}
				transform.rotation = Quaternion.Euler(0, 0, nowAngle);
			}
		}
	}
	void Update()
	{
		x = Input.GetAxisRaw("Horizontal") * speed;


		//UIManager.Instance.flowHp(hp, maxHp);
		//GamaManager.Instance.DeadCheck(hp);
		//if(GamaManager.Instance.isGameOver == true)
  //      {
		//	Invoke("GamaSet", 1f);
		//	hp = maxHp;
  //      }

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Water"))
		{
			hpDown = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Water"))
		{
			hpDown = true;
		}
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
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position + downDir, dir, 1f, LayerMask.GetMask("Ground"));
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
	private bool directionRaycast(Vector2 dir,Vector3 originPos)
	{
		return Physics2D.Raycast(transform.position + originPos, dir, 0.5f, LayerMask.GetMask("Ground"));
	}

	void GamaSet()
    {
		GamaManager.Instance.GameSet();
    }
}
