using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovementScript : MonoBehaviour{
    int direction;
    float speed;
    Rigidbody2D rb2d;
    void Start(){
        rb2d=GetComponent<Rigidbody2D>();
        speed=Random.Range(100,201);
        direction=Random.Range(1,3);
        if(direction==2){
            direction=-1;
        }
    }
    void OnCollisionEnter2D(Collision2D col){
        direction*=-1;
    }
    void FixedUpdate(){
        rb2d.velocity=Vector2.up*direction*speed*Time.deltaTime;      
    }
}
