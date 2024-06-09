using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private Player _player;
    private ArrowSystem _arrowSystem;
    private TargetSystem _targetSystem;

    [Header("Connections")]
    [SerializeField] private CinemachineFreeLook thirdPersonCam;
    [SerializeField] private CinemachineInputProvider cameraInputProvider;
    [SerializeField] private CinemachineImpulseSource defaultImpulse;
    [SerializeField] private CinemachineImpulseSource perfectImpulse;

    [Header("Camera Settings")]
    [SerializeField] private float boostFieldOfView = 100;
    [SerializeField] private float runFieldOfView = 85;
    [SerializeField] private float defaultFieldOfView = 40;
    [SerializeField] private float orbitBoostingRadius = 2.5f;
    [SerializeField] private float orbitRunningRadius = 3.5f;
    [SerializeField] private float orbitDefaultRadius = 15;
    [SerializeField] private float cameraOffsetAmount = .25f;
    private float originalCameraOffsetAmount;
    [SerializeField] private float cameraOffsetLerp = 1;
    private float originalCameraOffsetLerp;

    void Start()
    {
        _player = GetComponent<Player>();
        _arrowSystem = GetComponent<ArrowSystem>();
        _targetSystem = TargetSystem.instance;

        _arrowSystem.OnTargetHit.AddListener(Shake);
        _arrowSystem.OnInputStart.AddListener(LockCamera);
        _arrowSystem.OnInputRelease.AddListener(UnlockCamera);
        _arrowSystem.OnTargetLost.AddListener(UnlockCamera);
        _arrowSystem.OnArrowRelease.AddListener(ArrowChargeCheck);

        originalCameraOffsetAmount = cameraOffsetAmount;
        originalCameraOffsetLerp = cameraOffsetLerp;
    }

    void Update()
    {
        //Replicate movement booleans
        bool isBoosting = _player.isBoosting;
        bool isRunning = _player.isRunning;
        bool finishedBoost = _player.finishedBoost;

        //Set variables for readability
        float fov = isBoosting ? boostFieldOfView : (isRunning ? runFieldOfView : defaultFieldOfView);
        float lerpAmount = finishedBoost ? .006f : .01f;

        thirdPersonCam.m_Lens.FieldOfView = Mathf.Lerp(thirdPersonCam.m_Lens.FieldOfView, fov,lerpAmount);

        // Loop through all the Cinemachine Camera orbits
        for (int i = 0; i < 3; i++)
        {
            float newRadius = isBoosting ? orbitBoostingRadius : (isRunning ? orbitRunningRadius : orbitDefaultRadius);
            thirdPersonCam.m_Orbits[i].m_Radius = Mathf.Lerp(thirdPersonCam.m_Orbits[i].m_Radius, newRadius, lerpAmount);

            CinemachineComposer composer = thirdPersonCam.GetRig(i).GetCinemachineComponent<CinemachineComposer>();
            float targetScreenPos = _targetSystem.lerpedTargetPos.x;
            float characterScreenPos = Camera.main.WorldToScreenPoint(transform.position).x;

            cameraOffsetAmount = _arrowSystem.isCharging ? originalCameraOffsetAmount * 3 : originalCameraOffsetAmount;
            float targetCharacterDistance = ExtensionMethods.Remap(targetScreenPos - characterScreenPos, -800, 800, -cameraOffsetAmount, cameraOffsetAmount);
            targetCharacterDistance = Mathf.Clamp(targetCharacterDistance, -cameraOffsetAmount, cameraOffsetAmount);

            cameraOffsetLerp = originalCameraOffsetLerp;
            composer.m_ScreenX = Mathf.Lerp(composer.m_ScreenX, isRunning ? .5f - targetCharacterDistance : .5f, cameraOffsetLerp * Time.deltaTime);
        }
    }

    void ArrowChargeCheck(float charge)
    {
        if(_arrowSystem.HalfCharge())
            perfectImpulse.GenerateImpulse();
    }

    void Shake()
    {
        if(_player.isRunning || _player.holdRunInput)
            defaultImpulse.GenerateImpulse();
    }

    void LockCamera()
    {
        if (cameraInputProvider != null)
            cameraInputProvider.enabled = false;
    }

    void UnlockCamera()
    {
        if (cameraInputProvider != null)
            cameraInputProvider.enabled = true;
    }

}
