using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArrowButtonScript : EventTrigger{
    void Awake(){
        SceneManager.sceneLoaded+=OnSceneLoaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded-=OnSceneLoaded;
    }
// -------------------------------------------------------------------------
    private bool pointerdown;
    private float speed=100f;
    Animator daylenanim;
    Rigidbody2D daylenrb;
    Vector2 direction;
    GameObject replywindow,nomovemsg,canvas;
    Text nomovemsgtext;
    float transparency=1f;
// -------------------------------------------------------------------------
    void Start(){
        replywindow=GameObject.Find("ReplyWindow");
        daylenrb=GameObject.Find("Daylen").GetComponent<Rigidbody2D>();
        daylenanim=GameObject.Find("Daylen").GetComponent<Animator>();
        nomovemsg=GameObject.Find("NoMoveMessage");
        nomovemsg.SetActive(true);
        nomovemsgtext=nomovemsg.GetComponent<Text>();
    }
    public override void OnPointerDown(PointerEventData data){
        pointerdown=true;
        if(replywindow==null){
            replywindow=GameObject.Find("ReplyWindow");
        }
        if(replywindow.activeInHierarchy){
            nomovemsg.SetActive(true); 
        }
    }
    public override void OnPointerUp(PointerEventData data){
        pointerdown=false;
        daylenrb.velocity=new Vector2(0,0);
        switch(gameObject.name){
                case "LeftArrow":
                    daylenanim.SetBool("left",false);
                    break;
                case "TopArrow":
                    daylenanim.SetBool("up",false);
                    break;
                case "RightArrow":
                    daylenanim.SetBool("right",false);
                    break;
                case "BottomArrow":
                    daylenanim.SetBool("down",false);
                    break;
            }
    }
// -------------------------------------------------------------------------
    void Update(){
        if(pointerdown&&!replywindow.activeInHierarchy){
            nomovemsg.SetActive(false);
            switch(gameObject.name){
                case "LeftArrow":
                    daylenanim.SetBool("left",true);
                    direction=Vector2.left;
                    break;
                case "TopArrow":
                    daylenanim.SetBool("up",true);
                    direction=Vector2.up;
                    break;
                case "RightArrow":
                    daylenanim.SetBool("right",true);
                    direction=Vector2.right;
                    break;
                case "BottomArrow":
                    daylenanim.SetBool("down",true);
                    direction=Vector2.down;
                    break;
            }
            daylenrb.velocity=direction*speed*Time.fixedDeltaTime;
        }
    }
    void FixedUpdate(){
        if(nomovemsg!=null){
            if(nomovemsg.activeInHierarchy&&(transparency>0)){
                nomovemsgtext.color=new Color(1f,1f,1f,transparency);
                transparency-=0.01f;
            }
            else{
                nomovemsg.SetActive(false);
                transparency=1f;
                nomovemsgtext.color=new Color(1f,1f,1f,1f);
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        scene=SceneManager.GetActiveScene();
        switch(scene.name){
            case "CastleInside0Scene":
                switch(gameObject.name){
                    case "TopArrow":
                    case "BottomArrow":
                        SceneManager.sceneLoaded+=OnSceneLoaded;
                        this.enabled=false;
                        break;
                }
                break;
            default:
                if(!this.enabled){
                    this.enabled=true;
                }
                break;
        }
    }
}
