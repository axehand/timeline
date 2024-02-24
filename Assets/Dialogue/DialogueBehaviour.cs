using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    public PlayableDirector director;
    public string characterName;
    public string dialogueLine;
    public int dialogueSize;
    public GameObject playerPrefab; // 이전에는 GameObject player였습니다.

    public enum AnimState
    {
        idle,
        run,
        jump
    }

    public AnimState animState = AnimState.idle;

    public bool hasToPause = false;

    private bool clipPlayed = false;
    private bool pauseScheduled = false;
    private Animator animator; // Animator 컴포넌트를 저장할 변수 추가

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
{
    if (!clipPlayed && info.weight > 0f)
    {
        // "Player"라는 이름의 GameObject를 찾습니다.
        GameObject playerGameObject = GameObject.Find(characterName);
        if (playerGameObject != null)
        {
            Animator animator = playerGameObject.GetComponent<Animator>();
            if (animator != null)
            {
                // 애니메이션 상태에 따라 Animator의 파라미터 값을 설정합니다.
                switch (animState)
                {
                    case AnimState.idle:
                        animator.SetBool("IsRunning", false);
                        animator.SetBool("IsJumping", false);
                        break;
                    case AnimState.run:
                        animator.SetBool("IsRunning", true);
                        animator.SetBool("IsJumping", false);
                        break;
                    case AnimState.jump:
                        animator.SetBool("IsJumping", true);
                        animator.SetBool("IsRunning", false);
                        break;
                }
            }
        }

        UIManager.Instance.SetDialogue(characterName, dialogueLine, dialogueSize);

        if (Application.isPlaying && hasToPause)
        {
            pauseScheduled = true;
        }

        clipPlayed = true;
    }
}

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (pauseScheduled)
        {
            pauseScheduled = false;
            GameManager.Instance.PauseTimeline(director);
        }
        else
        {
            UIManager.Instance.ToggleDialoguePanel(false);
        }

        clipPlayed = false;
    }
}
