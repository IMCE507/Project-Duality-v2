using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer Srenderer;

    Material BotMaterial;

    public float TimeToTransition;

    [SerializeField]
    AudioSource MenuSong;

    private void Awake()
    {
        BotMaterial = Srenderer.material;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneTransition.instace?.StartFadeIn();
        StartCoroutine(BotTransition(TimeToTransition));
    }


    public void StartPlay()
    {
        MenuSong.DOFade(0f, 1f);
        SceneTransition.instace?.StartFadeOut(() => SceneManager.LoadScene("level 01 v2"));
    }

    public void QUIT()
    {
        Application.Quit();
    }

    IEnumerator BotTransition(float TransitionDuration)
    {
        float EndValue = BotMaterial.GetFloat("Transition_Amount") == 0f ? 1f : 0f;

        yield return new WaitForSeconds(TransitionDuration);
        Tween tween = BotMaterial.DOFloat(EndValue, "Transition_Amount", 2f);
        yield return tween.WaitForCompletion();
        StartCoroutine(BotTransition(TransitionDuration));

    }
}
