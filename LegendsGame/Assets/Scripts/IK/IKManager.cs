using UnityEngine;

// ReSharper disable once InconsistentNaming
public class IKManager : MonoBehaviour
{
    [SerializeField]
    public GameObject targetTransform;
    public IKJoint[] joints;
    public float[] angles;

    //sampling distance is 10 for best result discovered through trial and error,
    // if you really feel like editing this, don't go less than 6
    private const float samplingDistance = 10f;
    private const float learningRate = 100f; //Having this be values like 1000 produces fucky results
    private const float distanceThreshold = 0.01f;


    /*private void Start()
    {
        //Target transform set in dragoncontroller script
        //targetTransform =  GameObject.FindGameObjectWithTag("DragonTarget").transform;

        float[] angles = new float[joints.Length];

        //For our purposes, the for loop shouldn't edit the dragon's head's angle.  Hence we stop the for loop 1 before it
        for (int i = 0; i < joints.Length; i++)
        {
            // Since the game is 2d, we only need to worry about z rotation.  I have left the rest of the logic commented out for reference sake i guess
            // this also means the _rotationAxis variable is unneeded
            angles[i] = joints[i].transform.localRotation.eulerAngles.z;

            //if (Joints[i]._rotationAxis == 'x')
            //{
            //    angles[i] = Joints[i].transform.localRotation.eulerAngles.x;
            //}
            //else if (Joints[i]._rotationAxis == 'y')
            //{
            //    angles[i] = Joints[i].transform.localRotation.eulerAngles.y;
            //}
            //else if (Joints[i]._rotationAxis == 'z')
            //{
            //}
        }
        this.angles = angles;
    }*/

    public void StartDragon()
    {
        //Target transform set in dragoncontroller script
        //targetTransform =  GameObject.FindGameObjectWithTag("DragonTarget").transform;

        float[] angles = new float[joints.Length];

        //For our purposes, the for loop shouldn't edit the dragon's head's angle.  Hence we stop the for loop 1 before it
        for (int i = 0; i < joints.Length; i++)
        {
            // Since the game is 2d, we only need to worry about z rotation.  I have left the rest of the logic commented out for reference sake i guess
            // this also means the _rotationAxis variable is unneeded
            angles[i] = joints[i].transform.localRotation.eulerAngles.z;

            //if (Joints[i]._rotationAxis == 'x')
            //{
            //    angles[i] = Joints[i].transform.localRotation.eulerAngles.x;
            //}
            //else if (Joints[i]._rotationAxis == 'y')
            //{
            //    angles[i] = Joints[i].transform.localRotation.eulerAngles.y;
            //}
            //else if (Joints[i]._rotationAxis == 'z')
            //{
            //}
        }
        this.angles = angles;
    }

    private void Update()
    {
        //updating the IK multiple times per frame
        for(int i = 0; i < 10; i++)
        {
            InverseKinematics(targetTransform.transform.position, angles);
        }
    }

    public Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < joints.Length; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].rotationAxis);
            Vector3 nextPoint = prevPoint + rotation * joints[i].startOffset;
            prevPoint = nextPoint;
        }
        return prevPoint;
    }

    public float DistanceFromTarget(Vector3 target, float[] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }

    public float PartialGradient(Vector3 target, float[] angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += samplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / samplingDistance;

        // Restores
        angles[i] = angle;

        return gradient;
    }

    public void InverseKinematics(Vector3 target, float[] angles)
    {
        if (DistanceFromTarget(target, angles) < distanceThreshold)
            return;

        for (int i = joints.Length - 1; i >= 0; i--)
        {
            // Gradient descent
            // Update : Solution -= LearningRate * Gradient
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= learningRate * gradient;

            angles[i] = Mathf.Clamp(angles[i], joints[i].minAngle, joints[i].maxAngle);

            // Early termination
            if (DistanceFromTarget(target, angles) < distanceThreshold)
                return;
            //if(joints.Length == 3 && i == 2)
            //{
            //    angles[i] = 10;
            //}
            //Similar to above, since we are in 2d we only need to worry about the Z axis
            joints[i].transform.localEulerAngles = new Vector3(0, 0, angles[i]);

            //switch (Joints[i]._rotationAxis)
            //{
            //    case 'x':
            //        Joints[i].transform.localEulerAngles = new Vector3(angles[i], 0, 0);
            //        break;
            //    case 'y':
            //        Joints[i].transform.localEulerAngles = new Vector3(0, angles[i], 0);
            //        break;
            //    case 'z':
            //                      Joints[i].transform.localEulerAngles = new Vector3(0, 0, angles[i]);
            //        break;
            //}
        }
    }
}