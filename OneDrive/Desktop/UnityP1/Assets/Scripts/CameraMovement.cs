using System;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float zoomDistance = 10;
    public bool cameraEnabled = true;
    private float targetZoomDistance = 10;
    private CinemachineVirtualCamera playerCamera;
    private CinemachineCameraOffset cameraOffset;
    private float shakeDuration;
    private float shakeIntensity;
    void OnValidate() {
        float playerPosY = transform.position.y;
        GetComponent<CinemachineCameraOffset>().m_Offset.y = Math.Max(zoomDistance - playerPosY, 0);
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Math.Max(zoomDistance, 1);

        if (!cameraEnabled) GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 0;
    }

    // Start is called before the first frame update
    void Start() {
        cameraEnabled = true;
        targetZoomDistance = zoomDistance;
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        cameraOffset = GetComponent<CinemachineCameraOffset>();
    }

    // Update is called once per frame
    void Update() {
        if (targetZoomDistance != zoomDistance) zoomDistance += targetZoomDistance > zoomDistance ? 0.1f : -0.1f;
        if (Math.Abs(zoomDistance - targetZoomDistance) < 0.1) zoomDistance = targetZoomDistance;
        
        float playerPosY = transform.position.y;
        cameraOffset.m_Offset.y = Math.Max(zoomDistance - playerPosY, 0);
        if (playerPosY < 0) cameraOffset.m_Offset.y = 0;
        playerCamera.m_Lens.OrthographicSize = Math.Max(zoomDistance, 1);

        if (!cameraEnabled) playerCamera.m_Lens.OrthographicSize = 0;

        // Handle screen shaking
        if (Time.time - shakeDuration <= 0) {
            playerCamera.m_Lens.Dutch = UnityEngine.Random.Range(-shakeIntensity, shakeIntensity);
        } else playerCamera.m_Lens.Dutch = 0;
    }

    public void SetZoomDistance(float zoomDistance) {
        this.zoomDistance = zoomDistance;
        this.targetZoomDistance = zoomDistance;
    }

    public void ToggleCamera() {
        cameraEnabled = !cameraEnabled;
    }

    public void SetZoomDistanceSmoothly(float zoomDistance) {
        targetZoomDistance = zoomDistance;
    }

    public void ShakeScreen(float intensity, float duration) {
        shakeDuration = Time.time + duration;
        shakeIntensity = intensity;
    }

    private void HandleShakeScreen(float intensity, float duration) {
        
    }
}
