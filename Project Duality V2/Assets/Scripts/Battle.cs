using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class Battle : MonoBehaviour
{
    [SerializeField]
    Volume BattleVolume;

    public List<GameObject> BattleObjects;

    [SerializeField]
    Transform CheckPoint;

    public int NumberEnemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartBattle();
    }


    void StartBattle()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        BattleVolume.weight = 1f;
        DOTween.To(() => BattleVolume.weight, x => BattleVolume.weight = x, 0f, 0.7f);

        foreach(GameObject gameObject in BattleObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void VerifyEnemies()
    {
        if(NumberEnemies<=0)
        {
            PlayerManager.instance.CheckPoint = CheckPoint;
            gameObject.SetActive(false);
        }
    }
    
}
