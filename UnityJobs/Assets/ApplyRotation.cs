using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class ApplyRotation : MonoBehaviour
{
    private JobHandle jobHandle;
    private TransformAccessArray transformArray;
    private NativeArray<Vector3> rotationAxis;
    RotationJob job = new RotationJob();

    void Start()
    {
        rotationAxis = new NativeArray<Vector3>(1, Allocator.Persistent);
        this.rotationAxis[0] = Vector3.up;
        Transform[] transforms = { transform };
        transformArray = new TransformAccessArray(transforms);
    }

    void OnDisable()
    {
        // Dispose long-term data for jobs
        // This includes access to transforms
        this.transformArray.Dispose();
        this.rotationAxis.Dispose();
    }

    void Update()
    {
        this.job.RotationalAxis = this.rotationAxis;
        this.jobHandle = job.Schedule(transformArray, this.jobHandle);
    }

    void LateUpdate()
    {
        this.jobHandle.Complete();
    }
}
