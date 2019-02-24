using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public struct RotationJob : IJobParallelForTransform
{
    public NativeArray<Vector3> RotationalAxis;

    public void Execute(int index, TransformAccess transform)
    {
        transform.localRotation *= Quaternion.Euler(this.RotationalAxis[index]);
    }
}