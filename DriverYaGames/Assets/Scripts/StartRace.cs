using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    [SerializeField] private GameObject gameRuler;
    // Start is called before the first frame update
    void Start()
    {
        if(gameRuler.GetComponent<GameRules>().Regime == gameRuler.GetComponent<GameRules>().regimeRace[0])
        {
            //запускаем бесконечный режим
        }
        else
        {
            //запускаем заезд на время
        }
    }
}
