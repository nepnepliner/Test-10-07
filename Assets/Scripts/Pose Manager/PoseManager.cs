using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PoseManager : MonoBehaviour
{
    [SerializeField] private AnimationClip _pose;
    [SerializeField] public AvatarMask _avatarMask;

    private AnimationClip MathPose()
    {
        AnimationClip pose = new AnimationClip();
        pose.legacy = true;
        foreach (Transform child in transform)
        {
            SaveNextTransform(pose, child);
        }
        return pose;
    }

    public void CreatePose(string path, string name)
    {
        SavePoseAsset(MathPose(), $"{path}/{name}.anim");
    }

    public void SavePose()
    {
        SavePoseAsset(MathPose(), AssetDatabase.GetAssetPath(_pose));
    }

    private void SavePoseAsset(AnimationClip pose, string fullPath)
    {
        AssetDatabase.CreateAsset(pose, fullPath);
        AssetDatabase.SaveAssets();

        _pose = pose;

        Debug.Log("Pose " + fullPath + " saved");
    }

    private void SaveTransform(AnimationClip clip, Transform t)
    {
        string path = AnimationUtility.CalculateTransformPath(t, transform);

        // Save position
        AnimationCurve curveX = AnimationCurve.Linear(0, t.localPosition.x, 1, t.localPosition.x);
        AnimationCurve curveY = AnimationCurve.Linear(0, t.localPosition.y, 1, t.localPosition.y);
        AnimationCurve curveZ = AnimationCurve.Linear(0, t.localPosition.z, 1, t.localPosition.z);

        clip.SetCurve(path, typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve(path, typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve(path, typeof(Transform), "localPosition.z", curveZ);

        // Save rotation
        AnimationCurve curveRotX = AnimationCurve.Linear(0, t.localRotation.x, 1, t.localRotation.x);
        AnimationCurve curveRotY = AnimationCurve.Linear(0, t.localRotation.y, 1, t.localRotation.y);
        AnimationCurve curveRotZ = AnimationCurve.Linear(0, t.localRotation.z, 1, t.localRotation.z);
        AnimationCurve curveRotW = AnimationCurve.Linear(0, t.localRotation.w, 1, t.localRotation.w);

        clip.SetCurve(path, typeof(Transform), "localRotation.x", curveRotX);
        clip.SetCurve(path, typeof(Transform), "localRotation.y", curveRotY);
        clip.SetCurve(path, typeof(Transform), "localRotation.z", curveRotZ);
        clip.SetCurve(path, typeof(Transform), "localRotation.w", curveRotW);

        // Save scale
        AnimationCurve curveScaleX = AnimationCurve.Linear(0, t.localScale.x, 1, t.localScale.x);
        AnimationCurve curveScaleY = AnimationCurve.Linear(0, t.localScale.y, 1, t.localScale.y);
        AnimationCurve curveScaleZ = AnimationCurve.Linear(0, t.localScale.z, 1, t.localScale.z);

        clip.SetCurve(path, typeof(Transform), "localScale.x", curveScaleX);
        clip.SetCurve(path, typeof(Transform), "localScale.y", curveScaleY);
        clip.SetCurve(path, typeof(Transform), "localScale.z", curveScaleZ);
    }

    private void SaveNextTransform(AnimationClip pose, Transform t)
    {
        if (CheckAvatarMask(t))
            SaveTransform(pose, t);

        foreach (Transform child in t)
            SaveNextTransform(pose, child);
    }

    private bool CheckAvatarMask(Transform t)
    {
        if (_avatarMask == null)
            return true;

        string path = AnimationUtility.CalculateTransformPath(t, transform);
        for (int i = 0; i < _avatarMask.transformCount; i++)
            if (_avatarMask.GetTransformPath(i) == path)
                return _avatarMask.GetTransformActive(i);

        return false;
    }

    public void LoadPose()
    {
        _pose.SampleAnimation(gameObject, 0);
        Debug.Log("Pose " + _pose.name + " loaded.");
    }
}
