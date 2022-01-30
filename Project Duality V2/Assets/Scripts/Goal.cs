using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameOverMenu.instance?.ShowGameOverScreen(PlayerManager.instance.PointsData.Value);
            PlayerManager.instance.playerState = PlayerState.end;
            PlayerManager.instance.rb2d.velocity = Vector2.zero;
        }

    }
}
