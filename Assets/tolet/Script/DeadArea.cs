using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadArea : MonoBehaviour
{
    void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.name == "lowpoly toon characters animations")
            {
                Debug.Log("GameOver");
                SceneManager.instance.GameOver();
            }
        }
}
