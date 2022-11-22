using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    [SerializeField] private int TargetFrameRate;

    private void Awake()
    {
        Application.targetFrameRate = TargetFrameRate;
    }
}
