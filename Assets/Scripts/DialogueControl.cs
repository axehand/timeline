using UnityEngine;
using UnityEngine.Playables;

public class DialogueControl : MonoBehaviour
{
    public PlayableDirector director;
    private Vector3 savedPosition;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPaused)
        {
            // 'E' 키를 누르면 Timeline을 일시정지합니다.
            if (director.state == PlayState.Playing)
            {
                SaveCharacterPosition();
                director.Pause();
                isPaused = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPaused)
        {
            // 'E' 키를 누르면 Timeline을 재생합니다.
            RestoreCharacterPosition();
            director.Play();
            isPaused = false;
        }
    }

    void SaveCharacterPosition()
    {
        // 캐릭터의 현재 위치를 저장합니다.
        savedPosition = transform.position;
        Debug.Log(transform.position);
    }

    void RestoreCharacterPosition()
    {
        // 캐릭터를 저장된 위치로 이동시킵니다.
        transform.position = savedPosition;
    }
}