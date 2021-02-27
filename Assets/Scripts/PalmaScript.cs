using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmaScript : MonoBehaviour{  
    SpriteRenderer sr;
    private int originallayer,daylenlayer;
    private string colname,coltag,targets="DaylenDoggerDog",gotag;
    private bool tonormala=true;

    void Start(){
        sr=GetComponent<SpriteRenderer>();
        originallayer=sr.sortingOrder;
        daylenlayer=GameObject.Find("Daylen").GetComponent<SpriteRenderer>().sortingOrder;
        gotag=gameObject.tag;
    }

    void OnTriggerEnter2D(Collider2D col){
        colname=col.gameObject.name;
        if(targets.Contains(colname)){
            GetComponent<SpriteRenderer>().sortingOrder=daylenlayer+1;
        }
        if(gotag=="collectablebystay"){
            tonormala=false;
        } 
    }

    void OnTriggerExit2D(Collider2D col){
        GetComponent<SpriteRenderer>().sortingOrder=originallayer;
        if(gotag=="collectablebystay"){
            tonormala=true;
        }
    }

    void FixedUpdate(){
        if(tonormala&&(sr.color.a<1f)){
            sr.color+=new Color(0,0,0,0.01f);
        }
    }
}
