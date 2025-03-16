using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that Limit the camera's Y co-ordinate
/// </summary>
public class LimitCameraY : CinemachineExtension
{
    public float MinY = 0;
    public float MaxY = 0;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            Vector3 pos = state.RawPosition;

            if (pos.y < MinY)
            {
                pos.y = MinY;
            }
            else if (pos.y > MaxY)
            {
                pos.y = MaxY;
            }

            state.RawPosition = pos;
        }
    }
}