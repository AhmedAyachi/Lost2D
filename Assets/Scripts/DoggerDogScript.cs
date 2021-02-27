using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggerDogScript : MonoBehaviour{
    float posx,posy;
    float speed=300f;
    string colname;
    Transform daylenpos;
    Rigidbody2D rb2d;
    void Start(){
        transform.position=new Vector2(-16f,1f);
        rb2d=GetComponent<Rigidbody2D>();
        daylenpos=GameObject.Find("Daylen").transform;
    }

    void OnCollisionEnter2D(Collision2D col){
        colname=col.gameObject.name;
        if((colname=="Daylen")||(colname=="RightBorder")){
            posx=Random.Range(-18f,-6f);
            posy=daylenpos.position.y;
            if(posy>1f){
                posx=Random.Range(-11f,-6f);
            }
            transform.position=new Vector2(posx,posy);
        }
    }
    void FixedUpdate(){
        rb2d.velocity=speed*Vector2.right*Time.deltaTime;
    }
}
