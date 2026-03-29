using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        Instance = this;
        noise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = 0f;

    }

    public void Shake(float intensity, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(intensity, duration));
    }
    private IEnumerator ShakeRoutine(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        noise.AmplitudeGain = 0f;
    }
}
