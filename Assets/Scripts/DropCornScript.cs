using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCornScript : MonoBehaviour
{
    public GameObject corn;

    public GameObject cornPos;

    public bool isDrop = false;
    void Start()
    {
        cornPos.transform.position = corn.transform.position;
    }

    void Update()
    {
        if(isDrop)
        {
            corn.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        else
        {
            corn.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        //if (corn.transform.position.y <= -3)
        //{
        //    ResetPos();
        //}
    }

    void ResetPos()
    {
        corn.transform.position = cornPos.transform.position;

        corn.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isDrop = true;
        }
    }
}
