using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMolecule : MonoBehaviour {

    public GameObject molecule;
    public float rotateSpeed = 50f;
    bool rotateStatus = false;
    bool ScaleUpStatus = false;
    bool ScaleDownStatus = false;

    public void rotate()
    {

        if (rotateStatus == false)
        {
            rotateStatus = true;
        }
        else
        {
            rotateStatus = false;
        }
    }

    public void scaleUp()
    {
        if (ScaleUpStatus == false)
        {
            ScaleUpStatus = true;
        }
        else
        {
            ScaleUpStatus = false;
        }
    }


    public void scaleDown()
    {
        if (ScaleDownStatus == false)
        {
            ScaleDownStatus = true;
        }
        else
        {
            ScaleDownStatus = false;
        }
    }

    void Update()
    {
        if (rotateStatus == true)
        {
            molecule.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        if (ScaleUpStatus == true)
        {
            molecule.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (ScaleDownStatus == true)
        {
            molecule.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

}
