using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour
{
    public Vector3 rotationAxis;// = new Vector3(0,0,1);
    public Vector3 startOffset;
    //private Transform _transform;
    //public char _rotationAxis;

    public float minAngle;
    public float maxAngle;

    //private void Awake()
    //{
    //    _transform = this.transform;
    //    StartOffset = _transform.localPosition;
    //}

    public void SetStartOffset()
    {
        //_transform = this.transform;
        startOffset = transform.localPosition;
    }
}