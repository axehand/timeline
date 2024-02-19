using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector director; // Playable Director 컴포넌트 참조
    private double clipEndTime = 0; // 현재 클립의 종료 시간
    private bool isWaitingForInput = false; // 입력 대기 상태

    void Start()
    {
        director.played += OnPlayed; // 클립 재생 시작 시 호출될 이벤트
        director.stopped += OnStopped; // 클립 재생 종료 시 호출될 이벤트
        PlayNextClip(); // 첫 번째 클립 재생 시작
    }

    void Update()
    {
        // 사용자 입력 확인 및 다음 클립으로 넘어가기
        if (isWaitingForInput && Input.anyKeyDown)
        {
            PlayNextClip();
        }

        // 현재 클립이 끝났는지 확인 후 일시 중지
        if (director.time >= clipEndTime && !isWaitingForInput)
        {
            director.Pause(); // 타임라인 일시 중지
            isWaitingForInput = true; // 입력 대기 상태로 전환
        }
    }

    void OnPlayed(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            // 현재 클립의 종료 시간을 설정 (예제에서는 하드코딩, 실제로는 계산 필요)
            clipEndTime = director.duration;
        }
    }

    void OnStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            // 필요한 경우 클립 재생 종료 후 처리
        }
    }

    void PlayNextClip()
    {
        if (director.state != PlayState.Playing)
        {
            director.Play();
            isWaitingForInput = false; // 입력 대기 상태 해제
        }
    }
}