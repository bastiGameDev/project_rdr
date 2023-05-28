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

    public Camera additionalCamera; // �������������� ������ ��� ���-�����

    public Camera mainCamera; // �������� ������ ������

    private void Start()
    {
        // ����� ��������� ��� ������ ����
        timeline1.Pause();
        //timeline2.Pause();
        //timeline3.Pause();
        //timeline4.Pause();

        if (additionalCamera != null)
        {
            additionalCamera.enabled = false; // ��������� �������������� ������ ��� ������ ����
        }

        mainCamera = Camera.main; // �������� ������ �� �������� ������ ������
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
                additionalCamera.enabled = true; // �������� �������������� ������ ����� ���������������� ���-�����
            }

            mainCamera.enabled = false; // ��������� �������� ������ ������

            director.Play();

            director.stopped += OnTimelineStopped; // ������������� �� ������� ��������� ���������
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director != null)
        {
            director.stopped -= OnTimelineStopped; // ������������ �� ������� ��������� ���������

            if (additionalCamera != null)
            {
                additionalCamera.gameObject.SetActive(false); // ��������� �������������� ������ ����� ���������� ���-�����
            }

            mainCamera.gameObject.SetActive(true); // �������� �������� ������ ������
        }
    }
}
