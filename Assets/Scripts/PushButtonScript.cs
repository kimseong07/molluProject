using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonScript : MonoBehaviour
{
    public GameObject button;
    public GameObject door;

    [SerializeField]
    private float doorUpPos;

    public float upSpeed;

    private bool onButton;

    private Vector2 downPos, upPos, bDownPos, bUpPos;
    void Start()
    {
        downPos = new Vector2(door.transform.position.x, door.transform.position.y);
        upPos = new Vector2(door.transform.position.x, door.transform.position.y + doorUpPos);
        bUpPos = new Vector2(button.transform.position.x, button.transform.position.y);
        bDownPos = new Vector2(button.transform.position.x, button.transform.position.y - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(onButton)
        {
            button.transform.position = Vector2.Lerp(button.transform.position, bDownPos, Time.deltaTime * upSpeed);
            door.transform.position = Vector2.Lerp(door.transform.position, upPos, Time.deltaTime * upSpeed);
        }
        else if(!onButton)
        {
            button.transform.position = Vector2.Lerp(button.transform.position,bUpPos, Time.deltaTime * upSpeed);
            door.transform.position = Vector2.Lerp(door.transform.position, downPos, Time.deltaTime * upSpeed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            onButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            onButton = false;
        }
    }
}
