using UnityEngine;

public class PushedButtonScrip : MonoBehaviour
{
    public DoorData[] doorData;

    public GameObject button;
    public GameObject door;

    [SerializeField]
    private float btnUpDelay;

    private float time;
    
    public float upSpeed;

    private bool onButton;
    private bool startTime;

    private Vector2 bDownPos, bUpPos;
    void Start()
    {
		for (int i = 0; i < doorData.Length; i++)
		{
            doorData[i].objStartPos = doorData[i].door.transform.position;
		}
        bUpPos = new Vector2(button.transform.position.x, button.transform.position.y);
        bDownPos = new Vector2(button.transform.position.x, (button.transform.position.y - (button.transform.localScale.y)));
    }
	private void OnDrawGizmos()
	{
        for (int i = 0; i < doorData.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(doorData[i].objEndPos, new Vector3(1, 1));
        }
	}
	void Update()
    {
        if (onButton)
        {
            button.transform.position = Vector2.Lerp(button.transform.position, bDownPos, Time.deltaTime * 20);
            for (int i = 0; i < doorData.Length; i++)
            {
                doorData[i].door.transform.position = Vector3.MoveTowards(doorData[i].door.transform.position, doorData[i].objEndPos, upSpeed * Time.deltaTime);
            }
        }
        else if(!onButton)
        {
            button.transform.position = Vector2.Lerp(button.transform.position, bUpPos, Time.deltaTime * 20);
            for (int i = 0; i < doorData.Length; i++)
            {
                doorData[i].door.transform.position = Vector3.MoveTowards(doorData[i].door.transform.position, doorData[i].objStartPos, upSpeed * Time.deltaTime);
            }
        }

        if (startTime)
        {
            time += Time.deltaTime;
        }

        if(time >= btnUpDelay)
        {
            onButton = false;
            startTime = false;
            time = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {
            onButton = true;
            startTime = false;
            time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
        {

            startTime = true;
        }
    }
}
[System.Serializable]
public struct DoorData
{
    public GameObject door;
    public Vector2 objEndPos;
    [HideInInspector]
    public Vector2 objStartPos;
}