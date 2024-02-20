using UnityEngine;

public class CharacterStateSaver : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // 초기 위치와 회전을 저장합니다.
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void SaveState()
    {
        // 현재 위치와 회전을 저장합니다.
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void RestoreState()
    {
        // 저장된 위치와 회전으로 복원합니다.
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}