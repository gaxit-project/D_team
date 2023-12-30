using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpneingSE : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        AudioManager.GetInstance().PlaySound(6);
    }
}
