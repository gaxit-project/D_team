using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate1 : MonoBehaviour
{
    private int state = 4;
    private int tmp = 0;

    void Start(){
        
    }

    void Update()
    {
        Transform myTransform = this.transform;

        if (state == 1) // 'outward'
        {
            myTransform.Rotate (0f, 0f, -2f);
            tmp = tmp + 1;
            if (tmp == 45)
            {
                state = 2; // 'ostop'
                tmp = 0;
            }
        }

        if (state == 2) // 'ostop'
        {
            myTransform.Rotate (0f, 0f, 0f);
            tmp = tmp + 1;
            if (tmp == 100)
            {
                state = 3; // 're'
                tmp = 0;
            }
        }

        if (state == 3) // 're'
        {
            myTransform.Rotate (0f, 0f, 2f);
            tmp = tmp + 1;
            if (tmp == 45)
            {
                state = 4; // 'restop'
                tmp = 0;
            }
        }

        if (state == 4) // 'restop'
        {
            myTransform.Rotate (0f, 0f, 0f);
            tmp = tmp + 1;
            if (tmp == 200)
            {
                state = 1; // 'outward'
                tmp = 0;
            }
        }
    }
}
