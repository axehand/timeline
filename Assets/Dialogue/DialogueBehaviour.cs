using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    public PlayableDirector director;
    public string characterName;
    public string dialogueLine;
    public int dialogueIndex;
    public GameObject playerPrefab;
    public TextAsset tsv;

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
    private Animator animator;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public List<string> ReadTSVFile(TextAsset tsvFile)
    {
        List<string> texts = new List<string>();
        string[] lines = tsvFile.text.Split('\n');
        
        foreach (var line in lines)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] fields = line.Split('\t');
                texts.Add(fields[1]); // 첫 번째 필드가 텍스트라고 가정
            }
        }
        
        return texts;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!clipPlayed && info.weight > 0f)
        {
            List<string> dialogueLines = ReadTSVFile(tsv);
            GameObject playerGameObject = GameObject.Find(characterName);
            if (playerGameObject != null)
            {
                Animator animator = playerGameObject.GetComponent<Animator>();
                if (animator != null)
                {
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
            
                // 현재 다이얼로그 인덱스에 따라 텍스트 업데이트
                if(dialogueLines.Count > dialogueIndex)
                {
                    string currentDialogue = dialogueLines[dialogueIndex];
                    UIManager.Instance.SetDialogue(currentDialogue);
                }
            }

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
