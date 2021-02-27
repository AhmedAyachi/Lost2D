using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextSceneButtonScript : MonoBehaviour{
    void OnEnable(){
        //Called 2:
        //Debug.Log("NextSceneButtton Awake Called");
        SceneManager.sceneLoaded+=OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }
    void OnDestroy(){
        SceneManager.sceneLoaded-=OnSceneLoaded;
    }
// -------------------------------------------------------------------------
    /*void OnEnable(){
        //Called 3:
        Debug.Log("NextSceneButton OnEnable Called");
    }*/
// -------------------------------------------------------------------------
    GameObject daylen,replywindow,knights,inventory;
    Text replytext,speakername;
    Image speakerimage;
    Button nextbtn;
    [SerializeField] Sprite guardsprite;
    Animator blackfadeanimator;

    public string topscene,rightscene,bottomscene,leftscene;
    string colname,coltag,nextscene=null;
// -------------------------------------------------------------------------
    void LoadNextScene(){
        SceneManager.LoadScene(nextscene,LoadSceneMode.Single);
    }
// -------------------------------------------------------------------------
    void Start(){
        //Debug.Log("NextSceneButtton Start Called");
        daylen=GameObject.Find("Daylen");
        replywindow=daylen.GetComponent<DaylenScript>().replywindow;
        replytext=daylen.GetComponent<DaylenScript>().replytext;
        speakername=daylen.GetComponent<DaylenScript>().speakername;
        speakerimage=daylen.GetComponent<DaylenScript>().speakerimage;
        nextbtn=daylen.GetComponent<DaylenScript>().nextbtn;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //Called 6:
        nextscene=null;
        blackfadeanimator=GameObject.Find("BlackBackground").GetComponent<Animator>();
        switch(scene.name){
            case "IslandEdge0Scene":
                leftscene="CastleOutsideScene";
                break;
            case "CastleOutsideScene":
                rightscene="IslandEdge0Scene";
                topscene="CastleInside0Scene";
                leftscene="IslandInside0Scene";
                break;
            case "CastleInside0Scene":
                rightscene="CastleOutsideScene";
                break;
            case "IslandInside0Scene":
                rightscene="CastleOutsideScene";
                break;
        }
    }
// -------------------------------------------------------------------------
    void OnMouseDown(){
        colname=daylen.GetComponent<DaylenScript>().colname;
        coltag=daylen.GetComponent<DaylenScript>().coltag;
        if(coltag=="nextscene"&&!replywindow.activeInHierarchy){
            if(colname.Contains("Top")){
                switch(colname){
                    case "TopCastle":
                        if(PlayerPrefs.GetInt("sleeppotiontaken")==0){
                            replytext.text="Go  away  little  boy,  strangers  are  not  welcome  here";
                            speakername.text="Guard";
                            speakerimage.sprite=guardsprite;
                            nextbtn.onClick.RemoveAllListeners();
                            nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);});
                            replywindow.SetActive(true);
                        }
                        else if(PlayerPrefs.GetInt("sleeppotiontaken")==1){
                            nextscene=topscene;
                        }
                        break;
                    default:
                        nextscene=topscene;
                        break;
                }
            }
            else if(colname.Contains("Right")){
                switch(colname){
                    default:
                        nextscene=rightscene;
                        break;
                }

            }
            else if(colname.Contains("Bottom")){
                switch(colname){
                    default:
                        nextscene=bottomscene;
                        break;
                }

            }
            else if(colname.Contains("Left")){
                switch(colname){
                    default:
                        nextscene=leftscene;
                        break;
                }
            }
        }
        if(nextscene!=null){
            blackfadeanimator.SetBool("fadeout",true);
            Invoke("LoadNextScene",1f);
        }
    }
}
