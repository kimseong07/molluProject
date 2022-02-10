using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.isGoal = true;
            UIManager.Instance.boolCanvasGroup(UIManager.Instance.clearPanel, 1, true);
            Time.timeScale = 0;
        }
    }
}
