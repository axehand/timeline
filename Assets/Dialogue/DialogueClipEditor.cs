using UnityEngine;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine.Timeline;
using TMPro;

[CustomEditor(typeof(DialogueClip))]
public class DialogueClipEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // 기본 인스펙터 UI 그리기

        DialogueClip clip = target as DialogueClip;

        // 변경사항이 있을 경우 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(clip);
        }
    }
}
