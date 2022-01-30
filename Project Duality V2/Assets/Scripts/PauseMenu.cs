using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup PauseScreen;

    bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.instance?.playerState != PlayerState.death)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                IsPaused = !IsPaused;
                if (IsPaused)
                {
                    Time.timeScale = 0f;
                    PauseScreen.alpha = 1f;
                    PauseScreen.interactable = true;

                }
                else
                {
                    Time.timeScale = 1f;
                    PauseScreen.alpha = 0f;
                    PauseScreen.interactable = false;
                }

            }
        }
    }

}
