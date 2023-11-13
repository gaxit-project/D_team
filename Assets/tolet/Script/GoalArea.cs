using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalArea : MonoBehaviour
{
    void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.name == "lowpoly toon characters animations")
            {
                Debug.Log("Goal");
                SceneManager.instance.GameClear();
            }
        }
}