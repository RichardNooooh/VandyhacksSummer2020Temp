﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ExternalForce : AbstractSimForce
{
    public float forceMagnitude = 10.0f;

    private bool hasSetLength = false;

    public void FixedUpdate()
    {
        Vector3 force = getForce();

        rb2.AddForce(force * Time.fixedDeltaTime);
    }

    public override Vector2 getForce()
    {
        Vector2 position1 = o1.transform.position;
        Vector2 position2 = o2.transform.position;

        Vector2 forceVector = (position2 - position1).normalized;

        setOrientationAndScale(position1, position2);

        return forceVector * forceMagnitude;
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
        if (!hasSetLength)
        {
            hasSetLength = true;
            float scaleRatio = (position1 - position2).magnitude / (this.transform.localScale.x * 2);
            this.transform.localScale = new Vector3(scaleRatio, 1, 1);
        }

    }
}
