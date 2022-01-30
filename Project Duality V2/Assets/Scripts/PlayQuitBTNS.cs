using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayQuitBTNS : MonoBehaviour
{
    public virtual void Restart()
    {
        SceneTransition.instace?.StartFadeOut(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public virtual void QuitGame()
    {
        SceneTransition.instace?.StartFadeOut(() => SceneManager.LoadScene("Main Menu"));
    }
}
