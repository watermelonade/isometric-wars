using UnityEngine;
using System.Collections;
using System;

public class FootUnit : Unit
{

    //public Vector3 dest;
    bool act;

    void Update()
    {
        if (act)
        {
            if (!vEquals(transform.position, dest) )
            {
                transform.position = Vector3.Lerp(transform.position, dest, moveSpeed * Time.deltaTime);
            } else
            {
                act = false;
                
            }
        }

    }


    public override void Move()
    {
        act = true;
    }

    private bool vEquals(Vector3 x, Vector3 y)
    {
        bool ret = true;
        if (Mathf.Abs(transform.position.x - dest.x) > tolerance)
            ret = false;
        if (Mathf.Abs(transform.position.y - dest.y) > tolerance)
            ret = false;
        if (Mathf.Abs(transform.position.z - dest.z) > tolerance)
            ret = false;

        return ret;
    }

}