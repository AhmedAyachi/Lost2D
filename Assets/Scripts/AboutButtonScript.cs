using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutButtonScript : MonoBehaviour{
    Animator anim;
    [SerializeField] GameObject infotext,abouttext;
    void Start(){
        GetComponent<Button>().onClick.AddListener(OnClick);
        anim=GetComponent<Animator>();
        infotext.SetActive(false);
    }
    void OnClick(){
        if(anim.GetBool("clicked")==false){
            anim.SetBool("clicked",true);
            Invoke("infotextappears",1f);
            abouttext.GetComponent<Text>().color=new Color(0.458f,0.067f,0.067f);
        }
        else{
            anim.SetBool("clicked",false);
            Invoke("infotextdisappears",1f);
        }
    } 
    void infotextappears(){
        infotext.SetActive(true);
    }
    void infotextdisappears(){
        infotext.SetActive(false);
        abouttext.GetComponent<Text>().color=new Color(1f,1f,1f);  
    }
    void tonormalcolor(GameObject go){
        go.GetComponent<Image>().color=new Color(1f,1f,1f);
    }
}
