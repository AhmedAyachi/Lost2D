using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosingButtonsScript : MonoBehaviour{
    GameObject parent;
    void Start(){
        parent=transform.parent.gameObject;
        GetComponent<Button>().onClick.AddListener(()=>{
            parent.SetActive(false);
        });
    }
}
