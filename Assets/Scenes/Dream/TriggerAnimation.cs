using UnityEngine;
using UnityEngine.Playables;

public class TriggerAnimation : MonoBehaviour
{
    public Collider event1;
    //public Collider event2;
    //public Collider event3;
    //public Collider event4;
    public PlayableDirector timeline1;
    //public PlayableDirector timeline2;
    //public PlayableDirector timeline3;
    //public PlayableDirector timeline4;

    public Camera additionalCamera; // Дополнительная камера для кат-сцены

    public Camera mainCamera; // Основная камера игрока

    private void Start()
    {
        // Пауза таймлайна при старте игры
        timeline1.Pause();
        //timeline2.Pause();
        //timeline3.Pause();
        //timeline4.Pause();

        if (additionalCamera != null)
        {
            additionalCamera.enabled = false; // Отключаем дополнительную камеру при старте игры
        }

        mainCamera = Camera.main; // Получаем ссылку на основную камеру игрока
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayTimeline(timeline1);
        Debug.Log("sddvgfbf");
    }

    private void PlayTimeline(PlayableDirector director)
    {
        if (director != null)
        {
            if (additionalCamera != null)
            {
                additionalCamera.enabled = true; // Включаем дополнительную камеру перед воспроизведением кат-сцены
            }

            mainCamera.enabled = false; // Отключаем основную камеру игрока

            director.Play();

            director.stopped += OnTimelineStopped; // Подписываемся на событие окончания таймлайна
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director != null)
        {
            director.stopped -= OnTimelineStopped; // Отписываемся от события окончания таймлайна

            if (additionalCamera != null)
            {
                additionalCamera.gameObject.SetActive(false); // Отключаем дополнительную камеру после завершения кат-сцены
            }

            mainCamera.gameObject.SetActive(true); // Включаем основную камеру игрока
        }
    }
}
