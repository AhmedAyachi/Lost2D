using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour{
    public Animator blackbganimator,aboutbtnanimator;
    public Button speakerbtn,aboutbtn;
    public Image speakerbtnimage;
    public Sprite speakerbtnonsprite,speakerbtnoffsprite;
    public GameObject abouttextcontent;
    public AudioSource sweetdrakaudio;
    bool aboutbtnclicked,soundoff;
// -------------------------------------------------------------------------
    void Awake(){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("audio",1);
    }

    void LoadFirstScene(){
        SceneManager.LoadScene("IslandEdge0Scene",LoadSceneMode.Single);
    }

    void AppearAboutText(){
        if(aboutbtnclicked){
            abouttextcontent.SetActive(false);
        }
        else{
            abouttextcontent.SetActive(true);
        }
    }

    void FadeInSound(){
        sweetdrakaudio.Play(0);
        soundoff=false;
    }

    void Start(){
        Invoke("FadeInSound",2.5f);
        GetComponent<Button>().onClick.AddListener(()=>{
            soundoff=true;
            blackbganimator.SetBool("fadeout",true);
            Invoke("LoadFirstScene",1f);
        });
        aboutbtn.onClick.AddListener(()=>{
            aboutbtnclicked=aboutbtnanimator.GetBool("clicked");
            if(!aboutbtnclicked){
                aboutbtnanimator.SetBool("clicked",true);
            }
            else{
                aboutbtnanimator.SetBool("clicked",false);
            }
            Invoke("AppearAboutText",1f);
        });
        speakerbtn.onClick.AddListener(()=>{
           if(!soundoff){
               speakerbtnimage.rectTransform.sizeDelta=new Vector2(12.32f,18.38f);
               speakerbtnimage.sprite=speakerbtnoffsprite;
               soundoff=true;
               PlayerPrefs.SetInt("audio",0);
           }
           else{
               speakerbtnimage.rectTransform.sizeDelta=new Vector2(25f,20f);
               speakerbtnimage.sprite=speakerbtnonsprite;
               soundoff=false;
               PlayerPrefs.SetInt("audio",1);
           }
       });
    }
    void FixedUpdate(){
        if(soundoff&&sweetdrakaudio.volume>0){
            sweetdrakaudio.volume-=0.02f;
            if(sweetdrakaudio.volume==0){
                sweetdrakaudio.Pause();
            }
        }
        else if(!soundoff&&sweetdrakaudio.volume<1){
            sweetdrakaudio.volume+=0.02f;
            if(sweetdrakaudio.volume==0.02f){
                sweetdrakaudio.UnPause();
            }
        }
    }
}
