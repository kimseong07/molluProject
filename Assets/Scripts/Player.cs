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
		if (hp<=0)
		{
			return;
		}
		bool groundCheck = Physics2D.BoxCast(transform.position + transform.up * (boxCollider.offset.y - 0.01f), new Vector2(boxCollider.size.x/2, boxCollider.size.y), transform.rotation.eulerAngles.z, transform.right, 0.1f, LayerMask.GetMask("Ground"));
		bool groundCheckBack = directionRaycast(-transform.up, (transform.right * -0.3f ));
		bool groundCheckFront = directionRaycast(-transform.up, (transform.right * 0.3f ));
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

		Vector3 move = transform.right * x;

		//rigid.velocity = transform.right*x;
		animator.SetBool("Move", x != 0);
		if(animator.GetBool("Move") == true)
        {
			SoundManager.Instance.isMoving = true;
        }
		else
        {
			SoundManager.Instance.isMoving = false;
		}
		SoundManager.Instance.MoveSound(this.gameObject);

		if (!groundCheck || (!groundCheckBack && !groundCheckFront)|| !groundCheckMiddle)
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

		hit = Physics2D.Raycast(transform.position, transform.right*0.4f * dir, 1f, LayerMask.GetMask("Ground"));
		if (hit.collider != null && x != 0)
		{
			print(hit.collider.gameObject);
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
		UIManager.Instance.flowHp(hp, maxHp);
		if (hp<=0)
		{
			if (GameManager.Instance.isGameOver)
			{
				return;
			}
			GameOver();
			return;
		}
		x = Input.GetAxisRaw("Horizontal") * speed; 



   //     if (GamaManager.Instance.isGameOver == true)
   //     {
			//Invoke("GamaSet", GamaManager.Instance.deadTime);
   //         hp = maxHp;
   //     }

    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Water"))
		{
			hpDown = false;
		}
		if (collision.gameObject.CompareTag("Lava"))
		{
			hp = 0f;
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
	private bool directionRaycast(Vector2 dir,Vector3 originPos)
	{
		return Physics2D.Raycast(transform.position + originPos, dir, 0.5f, LayerMask.GetMask("Ground"));
	}
	private void GameOver()
	{
		animator.Play("Die");
		ChangePlayerSound(this.gameObject);
		rigid.velocity = Vector2.zero;
		Invoke("GamaSet", GameManager.Instance.deadTime);
		GameManager.Instance.isGameOver = true;
	}
	void GamaSet()
    {
		GameManager.Instance.isGameOver = false;
		StageManager.Instance.ReStartScene();
	}

	public void ChangePlayerSound(GameObject player)
	{
		AudioSource audios = player.GetComponent<AudioSource>();

		audios.clip = Resources.Load<AudioClip>("Die");
		audios.Play();
	}

}
