using UnityEngine;
using UnityEngine.Playables;

public class AnimationControl : MonoBehaviour
{
    public PlayableDirector director; // Playable Director 컴포넌트 참조

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 공백 키를 입력받으면
        {
            director.Play(); // Timeline 재생 (기본적으로 idle 애니메이션이 루프됩니다)
            director.time = 5; // 'walk' 애니메이션 클립이 시작되는 시간으로 플레이 헤드 이동
            director.Evaluate(); // Timeline의 현재 상태 강제 업데이트
        }
    }
}
