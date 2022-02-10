using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private bool isGoal;

    public void Goal()
    {
        if (isGoal)
        {
            UIManager.Instance.boolCanvasGroup(UIManager.Instance.clearPanel, 1, true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isGoal = true;
        }
    }
}
