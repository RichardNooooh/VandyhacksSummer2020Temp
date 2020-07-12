using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SpringForce : AbstractSimForce
{
    public float springConstant = 10.0f; //N/m
    public float equilibriumLength = 5.0f;

    public void FixedUpdate()
    {
        Vector3 force = getForce();
        if (rb1 != null)
            rb1.AddForce(force * Time.fixedDeltaTime);
        if (rb2 != null)
            rb2.AddForce(-force * Time.fixedDeltaTime);
    }


    public override Vector2 getForce()
    {
        Vector2 position1 = o1.transform.position;
        Vector2 position2 = o2.transform.position;

        Vector2 displacementVector = position2 - position1;
        float displacementLength = displacementVector.magnitude;

        setOrientationAndScale(position1, position2);

        if (displacementLength < equilibriumLength)
            return - displacementVector.normalized * springConstant * displacementLength;
        else if (displacementLength > equilibriumLength)
            return displacementVector.normalized * springConstant * displacementLength;
        else
            return Vector2.zero;
    }

    private void setOrientationAndScale(Vector2 position1, Vector2 position2)
    {
        //set the position
        Vector2 midpoint = (position2 + position1) / 2;
        this.transform.position = midpoint;

        //rotate z axis to point at position 1
        float dotProduct = Vector2.Dot(Vector2.right, (position2 - position1));
        float magnitudeProduct = (position2 - position1).magnitude;


        float zRotation = Mathf.Acos(dotProduct / magnitudeProduct) * 180 / Mathf.PI;
        this.transform.rotation = Quaternion.Euler(Vector3.forward * zRotation);

        //set the proper scale so that the edges meet with o1 and o2

        float scaleRatio = (position1 - position2).magnitude / 2;
        UnityEngine.Debug.Log("Scale Ratio: " + scaleRatio + " (Position1 - Position2) Magnitude: " + (position1 - position2).magnitude);
        this.transform.localScale = new Vector3(scaleRatio , 1, 1);
    }
}
