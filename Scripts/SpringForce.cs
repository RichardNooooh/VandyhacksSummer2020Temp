using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SpringForce : AbstractSimForce
{
    public float springConstant = 10.0f; //N/m
    public float equilibriumLength = 5.0f;

    /**
     * Updates the force every in-engine time interval
     */
    public void FixedUpdate()
    {
        Vector3 force = getForce();
        if (rb1 != null)
            rb1.AddForce(force * Time.fixedDeltaTime);
        if (rb2 != null)
            rb2.AddForce(-force * Time.fixedDeltaTime);
    }

    /**
     * Computes the force given by this game object based on its current conditions
     */
    public override Vector2 getForce()
    {
        Vector2 position1 = o1.transform.position;
        Vector2 position2 = o2.transform.position;

        Vector2 displacementVector = position2 - position1;
        float displacementLength = displacementVector.magnitude;

        setOrientationAndScale(position1, position2);

        if (displacementLength < equilibriumLength)
            return -displacementVector.normalized * springConstant * displacementLength;
        else if (displacementLength > equilibriumLength)
            return displacementVector.normalized * springConstant * displacementLength;
        else
            return Vector2.zero;
    }

    /**
     * Computes the orientation/scale/position of the sprite image.
     */
    private void setOrientationAndScale(Vector2 position1, Vector2 position2)
    {
        //set the position
        Vector2 midpoint = (position2 + position1) / 2;
        this.transform.position = midpoint;

        //rotate z axis to point at position 1
        float dotProduct = Vector2.Dot(Vector2.right, (position2 - position1));
        float magnitudeProduct = (position2 - position1).magnitude;


        float zRotation = 180 - Mathf.Acos(dotProduct / magnitudeProduct) * 180 / Mathf.PI;
        if (position2.y > position1.y)
            zRotation = -zRotation;

        this.transform.rotation = Quaternion.Euler(0, 0, zRotation);

        float scaleRatio = (position1 - position2).magnitude / 2;

        this.transform.localScale = new Vector3(scaleRatio , 1, 1);
    }
}
