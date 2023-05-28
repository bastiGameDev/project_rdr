using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    public Collider triggerCollider;
    public PlayableDirector playableDirector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other == triggerCollider)
        {
            StartTimeline();
        }
    }

    private void StartTimeline()
    {
        playableDirector.Play(); // Запускаем проигрывание таймлайна
    }
}