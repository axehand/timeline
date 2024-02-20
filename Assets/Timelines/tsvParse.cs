using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using JetBrains.Annotations;
using UnityEngine.Animations;

public class tsvParse : MonoBehaviour
{
    // TSV 파일의 경로
     private string tsvFilePath = "Timelines/Cutscene_Test_KO.tsv";

    // 대사를 저장할 구조체

    public PlayableDirector director;
    
    public struct DialogueEntry
    {
        public string ActorCode;
        public string DialogueText;
    }

    // 대사 리스트
    private List<DialogueEntry> dialogueList = new List<DialogueEntry>();

    public void ReceiveSignal(){
        Debug.Log("next anim start");
        
        PauseTimeline();
    }

    // 게임 시작 시 실행
    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, tsvFilePath);
        ReadTSVFile(filePath);
        StartCoroutine(DisplayDialogues());
    }
    void update()
    {
    
    }
    // TSV 파일을 읽고 파싱하는 함수
    void ReadTSVFile(string fullPath)
    {
        string[] lines = File.ReadAllLines(fullPath);
        foreach (string line in lines)
        {
            string[] fields = line.Split('\t');
            DialogueEntry entry = new DialogueEntry
            {
                ActorCode = fields[0],
                DialogueText = fields[1]
            };
            dialogueList.Add(entry);
        }
    }

    IEnumerator DisplayDialogues()
    {
        foreach (var dialogue in dialogueList)
        {
            Debug.Log(dialogue.ActorCode + ": " + dialogue.DialogueText);
        
            // 사용자가 키를 누를 때까지 기다립니다.
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // 입력 버퍼를 비워서 연속 입력을 방지합니다.
            Input.ResetInputAxes();
            ResumeTimeline();
            // 키 입력 후에 다음 대사로 바로 넘어가지 않도록 지연시간을 줍니다.
            // 지연시간은 대사가 충분히 보여질 수 있는 시간이어야 합니다.
            yield return new WaitForSeconds(0.5f);
        }
    }


    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }
    private Vector3 savedPosition;
    private Quaternion savedRotation;

    // 타임라인을 일시 중지하고 오브젝트의 위치를 저장합니다.
    public void PauseTimeline()
    {
        // 현재 오브젝트의 위치와 회전을 저장합니다.
        savedPosition = transform.position;
        savedRotation = transform.rotation;

        // 타임라인을 일시 중지합니다.
        director.Pause();
    }

    // 타임라인을 재개하고 오브젝트의 위치를 복원합니다.
    public void ResumeTimeline()
    {
        // 저장된 위치와 회전을 오브젝트에 적용합니다.
        transform.position = savedPosition;
        transform.rotation = savedRotation;

        // 타임라인을 재개합니다.
        director.Resume();
    }
}
