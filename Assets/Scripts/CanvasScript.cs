using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
