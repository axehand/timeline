using UnityEngine;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    public PlayableDirector director;
    private bool canProceed;

    private void Awake()
    {
        // 시그널 리시버를 구성합니다.
        director.playableAsset.CreatePlayable(director.gameObject.GetComponent<PlayableGraph>(), gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canProceed)
        {
            ProceedToNextClip();
        }
    }

    public void OnDialogueSignalReceived()
    {
        // 시그널이 수신되면 대화 진행 가능 상태로 설정합니다.
        canProceed = true;
    }

    private void ProceedToNextClip()
    {
        // PlayableDirector를 사용하여 다음 클립으로 넘어갑니다.
        // 이 부분은 프로젝트의 구체적인 로직에 맞게 구현해야 합니다.
        director.Play();
        canProceed = false;
    }
}