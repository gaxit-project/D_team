using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train2 : MonoBehaviour
{
    private int state = 4;
    private int tmp = 1;

    void Start(){
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (state == 1) // 'outward'
        {
            transform.position += new Vector3(0, 0, 5) * Time.deltaTime;
            if (pos.z >= 316.5)
            {
                state = 2; // 'ostop'
            }
        }

        if (state == 2) // 'ostop'
        {
            transform.position += new Vector3(0, 0, 0) * Time.deltaTime;
            tmp = tmp + 1;
            if (tmp >= 200)
            {
                state = 3; // 're'
                tmp = 0;
            }
        }

        if (state == 3) // 're'
        {
            transform.position += new Vector3(0, 0, -5) * Time.deltaTime;
            if (pos.z <= 225)
            {
                state = 4; // 'restop'
            }
        }

        if (state == 4) // 'restop'
        {
            transform.position += new Vector3(0, 0, 0) * Time.deltaTime;
            tmp = tmp + 1;
            if (tmp >= 200)
            {
                state = 1; // 'outward'
                tmp = 0;
            }
        }
    }
}
