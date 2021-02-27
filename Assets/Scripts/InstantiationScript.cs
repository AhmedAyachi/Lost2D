using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationScript : MonoBehaviour{
    public GameObject daylenprefab,canvasprefab,nextscenebtnprefab;
    GameObject daylen,canvas,nextscenebtn;
    void Awake(){
        //Called 1:
        nextscenebtn=GameObject.Find("NextSceneButton");
        daylen=GameObject.Find("Daylen");
        canvas=GameObject.Find("Canvas");
        if(nextscenebtn==null){
            nextscenebtn=Instantiate(nextscenebtnprefab);
            nextscenebtn.gameObject.name="NextSceneButton";
            //Debug.Log("NextSceneButton Created");
        }
        if(daylen==null){
            daylen=Instantiate(daylenprefab);  
            daylen.gameObject.name="Daylen"; 
            //Debug.Log("Daylen Created");
        } 
        if(canvas==null){
            canvas=Instantiate(canvasprefab);
            canvas.gameObject.name="Canvas";
            //Debug.Log("Canvas Created");
        }
    }
}
