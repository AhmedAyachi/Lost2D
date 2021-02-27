using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayButtonScript : MonoBehaviour{
    void LoadEntryScene(){
        SceneManager.LoadScene("EntryScene",LoadSceneMode.Single);
    }
// -------------------------------------------------------------------------
    Button quitbtn;
    public Animator blackfadeanimator;
    void LoadFirstScene(){
        SceneManager.LoadScene("IslandEdge0Scene",LoadSceneMode.Single);
    }
    
    void Start(){
        PlayerPrefs.DeleteAll();
        GetComponent<Button>().onClick.AddListener(()=>{
            blackfadeanimator.SetBool("fadeout",true);
            Invoke("LoadFirstScene",1f);
        });
        quitbtn=GameObject.Find("QuitButton").GetComponent<Button>();
        quitbtn.onClick.AddListener(()=>{
            blackfadeanimator.SetBool("fadeout",true);
            Invoke("LoadEntryScene",1f);
        });
    }
}
