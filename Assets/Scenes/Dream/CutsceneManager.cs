using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector.Play(); // Запускаем проигрывание кат-сцены
    }
}