using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DaylenScript : MonoBehaviour{
    void Awake(){
        //Called 4:
        //Debug.Log("Daylen OnEnable Called");
        SceneManager.sceneLoaded+=OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
        lasttwoscenes.Add("");
        lasttwoscenes.Add("FirstScene");
        PlayerPrefs.DeleteAll();
    }
    void OnDestroy(){
        SceneManager.sceneLoaded-=OnSceneLoaded;
    }
    void hidehitbg(){
        hitbg.SetActive(false);
    }
    
    void LoadEntryScene(){
        SceneManager.LoadScene("EntryScene",LoadSceneMode.Single);
    }  

    void LoadDeadScene(){
        SceneManager.LoadScene("DeadScene",LoadSceneMode.Single);
    }
    
    public void NextReply(string character){
        switch(character.ToUpper()){
            case "MASTERFOX":
                masterfoxri++;
                replytext.text=masterfoxreplies[masterfoxri];
                if(!gotmaterials){
                    if(masterfoxri<10){
                        if(masterfoxri==4){
                            masterfox.SetActive(true);
                        }
                        else if(masterfoxri>4){
                            if(masterfoxri%2==1){
                                speakerimage.sprite=masterfoxsprite;
                                speakername.text="MasterFox";
                                if(masterfoxri==7){
                                    closedmaterialsparchment.SetActive(true);
                                }
                            }
                            else{
                                speakerimage.sprite=daylensprite;
                                speakername.text="Daylen";
                            }
                        }
                    }
                    else if(masterfoxri==10){
                        replywindow.SetActive(false);
                        if(inventory==null){
                            inventory.SetActive(true);
                        }
                    }
                }
                else if(gotmaterials){
                    if(masterfoxri<masterfoxreplies.Length-1){
                        if(masterfoxri%2==0){
                            speakerimage.sprite=masterfoxsprite;
                            speakername.text="MasterFox";
                        }
                        else{
                            speakerimage.sprite=daylensprite;
                            speakername.text="Daylen"; 
                        }
                    }
                    else if(masterfoxri==masterfoxreplies.Length-1){
                        replywindow.SetActive(false);
                        blackfadeanimator.SetBool("fadeout",true);
                        Invoke("LoadEntryScene",1f);
                    }
                }
                break;
            case "MRDARK":
                mrdarkri++;
                if(mrdarkri<mrdarkreplies.Length){
                    replytext.text=mrdarkreplies[mrdarkri];
                }
                if(!gotallbatwings){
                    if(mrdarkri<9){
                        switch(mrdarkri%2){
                            case 0:
                                speakerimage.sprite=daylensprite;
                                speakername.text="Daylen";
                                break;
                            case 1:
                                speakerimage.sprite=mrdarksprite;
                                speakername.text="Mr  Dark";
                                if(mrdarkri==7){
                                    sleeppotion.SetActive(true);
                                    PlayerPrefs.SetInt("sleeppotiontaken",0);
                                }
                                break;
                        }
                    }
                    else if(mrdarkri==9){
                        replywindow.SetActive(false);
                        PlayerPrefs.SetInt("mrdarkri",mrdarkri);
                    }
                }
                else if(gotallbatwings){
                    switch(mrdarkri%2){
                        case 0:
                            speakerimage.sprite=mrdarksprite;
                            speakername.text="Mr  Dark";
                            break;
                        case 1:
                            speakerimage.sprite=daylensprite;
                            speakername.text="Daylen";
                            break;
                    }
                    if(mrdarkri==mrdarkreplies.Length){
                        replywindow.SetActive(false);
                        PlayerPrefs.SetInt("mrdarkri",mrdarkri);
                        inventory.GetComponent<InventoryScript>().openedmaterialsparchment.SetActive(true);
                    }
                }
                break;
        }
    }
    void PlaySeaSound(){
        seasound.Play(0);
    }
// -------------------------------------------------------------------------
    List<string> lasttwoscenes=new List<string>();
    List<string> cuttreesnames=new List<string>();
    public GameObject nomovemsg,nextscenebtnprefab,replywindow;
    GameObject nextscenebtn,inventory,closedmaterialsparchment,canvas,masterfox,sleeppotion,hitbg;

    public Text replytext,speakername;
    Text heatlhtext;

    AudioSource seasound;

    public Image speakerimage;
    public Button nextbtn;
    public Sprite daylensprite,masterfoxsprite,mrdarksprite,woodsprite;
    public bool gotmaterials=false,gotallbatwings=false,masterfoxclick=false,mrdarkclick=false;
    Rigidbody2D rb2d;
    SpriteRenderer renderer;
    Animator blackfadeanimator;
    int masterfoxri=0,mrdarkri=0,health=4;

    public string colname,coltag;
    string lastscenepos;
    
// -------------------------------------------------------------------------
    string[] masterfoxreplies={
      /*0*/ "ooh  !\nmy  head !\nhhhhhhhhmmmmmmmm...",
      /*1*/ "What  happened ?\nwhy  I  feel  so  tired ?",
      /*2*/ "ahhhhhh  !!!\nwhere  am  I  ?\nwhat  happened  to  my  boat  ?\nit's  all  broken !",
      /*3*/ "what  should  I  do  now  ?\nI  need  to  fix  it",
      /*4*/ "what ?\nhow  did  you  just...\nwho  are  you  ?",
      /*5*/ "they  call  me  masterfox\ni'm  the  oldest  fox  in  this  mysterious  island\nsorry  for  disturbing,  i  was  just  passing  by  and  i  heard  you  wondering  how  are  you  going  to  fix  your  boat",
      /*6*/ "You  mean  you  were  sneaking  !!! ",
      /*7*/ "no  no  no  !!!\ni  mean,  i  can  help  you  with  that,  take  this  parchment,  i  wrote  you  down  some  materials  to  bring  me  on  it\njust  go  to  the  forest  overthere  to  search  for  them  and  meet  me  here  when  you  get  them",
      /*8*/ "thank  you,  see  you  soon",
      /*9*/ "Good  Luck  little  boy\nhihihihi",
      /*10*/ "did  you  bring  the  maretials  i  asked  you  ?",
      /*11*/ "here  you  are  masterfox",
      /*12*/ "now  i  can  fix  the  boat  for  you",
      /*13*/ "thank  you"
    };
    string[] mrdarkreplies={
       /*0*/ "hello,  can  i  borrow  your  axe  for a  while  please",
       /*1*/ "how  dare  you  !!!\ndon't  you  know  me  !!!\nyou  must  be  a  stranger",
       /*2*/ "sorry,  i  am  actually  ,  my  boat  was  broken  and  i  am  trying  to  fix  it,  i  need  your  axe  to  get  some  wood",
       /*3*/ "hhhmmmmmm...\ni'm  mr  dark  the  famous  alchemist  of  this  island\nyou  need  an  axe?  no problem,  but  i  don't  give  my  special  to  strangers  unless  you  do  me  a  favor",
       /*4*/ "sure,  i'm  listening",
       /*5*/ "you  see  that  castle  overthere,\nit's  protected  by  some  bats  at  the  entrance,  just  bring  me  six  bat  wings  for  my  new  experiment,  then  the  axe  is  all  yours" ,
       /*6*/ "and  what  about  the  guards?",
       /*7*/ "use  this  potion  and  they'll  fall  a  sleep  immediately",
       /*8*/ "deal  !!!!!\nhihihihi",
       /*9*/ "here  we  go,  these  are  the  wings  you  asked  me",
       /*10*/ "thank  you,  and  this  is  the  axe\nhope  you  visit  me  when  my  experiment  is  ready  to  see  what  mr  dark  is  capable  of\ngood  luck ",
       /*11*/ "thank  you"
    };
// -------------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D col){
        string colname=col.gameObject.name;
        colname=(colname.Contains("("))?colname.Substring(0,colname.IndexOf("(")-1):colname;
        string coltag=col.gameObject.tag;
        if(coltag=="collectablebyenter"){
            inventory.GetComponent<InventoryScript>().cobject=col.gameObject;
            inventory.GetComponent<InventoryScript>().objectname=colname;
            inventory.GetComponent<InventoryScript>().sprite=col.gameObject.GetComponent<SpriteRenderer>().sprite;
            switch(colname){
                case "BatWing":
                    PlayerPrefs.SetInt("gotsomebatwings",1);
                    break;
                case "Rope":
                    PlayerPrefs.SetInt("gotrope",1);
                    inventory.GetComponent<InventoryScript>().openedmaterialsparchment.transform.GetChild(2).GetComponent<Text>().text="1";
                    inventory.GetComponent<InventoryScript>().openedmaterialsparchment.SetActive(true);
                    break;
            }
            inventory.GetComponent<InventoryScript>().addiblego=true;
            
        }  
    }
    
    void OnTriggerStay2D(Collider2D col){
        string colname=col.gameObject.name;
        colname=(colname.Contains("("))?colname.Substring(0,colname.IndexOf("(")-1):colname;
        string coltag=col.gameObject.tag;
        SpriteRenderer colsr=col.gameObject.GetComponent<SpriteRenderer>();
        float a=colsr.color.a;
        if(coltag=="collectablebystay"){
            if(a<=0){
                if(colname.Contains("WoodTree")){
                    cuttreesnames.Add(colname);
                    col.gameObject.GetComponent<Animator>().SetBool("cut",true);
                    inventory.GetComponent<InventoryScript>().objectname="Wood";
                    inventory.GetComponent<InventoryScript>().sprite=woodsprite;
                    Transform coltrans=col.transform;;
                    coltrans.position=new Vector2(coltrans.position.x,coltrans.position.y-1.7f);
                }
                else{
                    inventory.GetComponent<InventoryScript>().objectname=colname;
                    inventory.GetComponent<InventoryScript>().sprite=col.gameObject.GetComponent<SpriteRenderer>().sprite;
                }
                inventory.GetComponent<InventoryScript>().cobject=col.gameObject;
                inventory.GetComponent<InventoryScript>().addiblego=true;
                col.gameObject.tag="Untagged";
            }
            else{
                colsr.color-=new Color(0,0,0,0.01f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        colname=col.gameObject.name;
        coltag=col.gameObject.tag;
        switch(coltag){
            case "nextscene":
                nextscenebtn.SetActive(true);
                nextscenebtn.transform.position=col.contacts[0].point;
                if(colname.IndexOf("Top")>=0){
                    lastscenepos="Bottom";
                    nextscenebtn.transform.Rotate(new Vector3(0,0,90f));
                    nextscenebtn.transform.position=new Vector2(nextscenebtn.transform.position.x,nextscenebtn.transform.position.y-0.7f);
                }
                else if(colname.IndexOf("Right")>=0){
                    lastscenepos="Left";
                    nextscenebtn.transform.Rotate(new Vector3(0,0,0));
                    nextscenebtn.transform.position=new Vector2(nextscenebtn.transform.position.x-0.7f,nextscenebtn.transform.position.y);
                }
                else if(colname.IndexOf("Bottom")>=0){
                    lastscenepos="Top";
                    nextscenebtn.transform.Rotate(new Vector3(0,0,-90f));
                    nextscenebtn.transform.position=new Vector2(nextscenebtn.transform.position.x,nextscenebtn.transform.position.y+0.7f);
                }
                else if(colname.IndexOf("Left")>=0){
                    lastscenepos="Right";
                    nextscenebtn.transform.Rotate(new Vector3(0,0,180f));
                    nextscenebtn.transform.position=new Vector2(nextscenebtn.transform.position.x+0.7f,nextscenebtn.transform.position.y);
                }
                break;
            case "healthreducer":
                hitbg.SetActive(true);
                Invoke("hidehitbg",0.1f);
                switch(colname){
                    case "DoggerDog":
                    case "Bat":
                        health--;
                        heatlhtext.text=health.ToString();
                        break;
                }
                if(health<=0){
                    blackfadeanimator.SetBool("fadeout",true);
                    Invoke("LoadDeadScene",1f);
                }
                break;
        }
    }
    void OnCollisionExit2D(){
        nextscenebtn.SetActive(false);
        nextscenebtn.transform.Rotate(new Vector3(0,0,-nextscenebtn.transform.localEulerAngles.z));
    }
    void ToLeftStandPos(){
        GetComponent<Animator>().SetBool("left",false);
    }
    void ToFrontStandPos(){
        GetComponent<Animator>().SetBool("down",false);
    }
    void Start(){
        transform.position=new Vector2(14.17f,2.19f);
        rb2d=GetComponent<Rigidbody2D>();
        renderer=GetComponent<SpriteRenderer>();
        canvas=GameObject.Find("Canvas");
        heatlhtext=GameObject.Find("HealthNumber").GetComponent<Text>();
        heatlhtext.text=health.ToString();
        masterfox=GameObject.Find("MasterFox");
        masterfox.SetActive(false);
        replywindow=GameObject.Find("ReplyWindow");
        replywindow.SetActive(true);
        speakerimage=replywindow.transform.Find("SpeakerImage").GetComponent<Image>();
        speakerimage.sprite=daylensprite;
        speakername=replywindow.transform.Find("SpeakerName").GetComponent<Text>();
        speakername.text="Daylen";
        replytext=replywindow.transform.Find("ReplyText").GetComponent<Text>();
        replytext.text=masterfoxreplies[masterfoxri];
        nextbtn=replywindow.transform.Find("NextButton").GetComponent<Button>();
        nextbtn.onClick.AddListener(()=>{NextReply("masterfox");});
        hitbg=GameObject.Find("HitBackground");
        hitbg.SetActive(false);
        inventory=GameObject.Find("Inventory");  
        PlayerPrefs.SetInt("gotsomebatwings",0);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        blackfadeanimator=GameObject.Find("BlackBackground").GetComponent<Animator>();
        if((rb2d!=null)&&(rb2d.gravityScale!=0)){
            rb2d.gravityScale=0;
        }
        lasttwoscenes.Reverse();
        lasttwoscenes[1]=scene.name;
        if(nextscenebtn==null){
            nextscenebtn=GameObject.Find("NextSceneButton");
        }
        nextscenebtn.SetActive(false);
        nextscenebtn.transform.Rotate(new Vector3(0,0,-nextscenebtn.transform.localEulerAngles.z));
        switch(lastscenepos){
            case "Top":
                transform.position=new Vector2(transform.position.x,9f);
                break;
            case "Right":
                switch(lasttwoscenes[0]){
                    case "IslandEdge0Scene":
                        if(transform.position.y>3){
                            transform.position=new Vector2(18.8f,3f);
                        }
                        else{
                            transform.position=new Vector2(18f,transform.position.y);
                        }
                        break;
                    default:
                    transform.position=new Vector2(18f,transform.position.y);
                        break;
                }
                break;
            case "Bottom":
                transform.position=new Vector2(transform.position.x,-8.5f);
                break;
            case "Left":
                switch(lasttwoscenes[0]){
                    case "CastleInside0Scene":
                        transform.position=new Vector2(-7.3f,1.35f);
                        Invoke("ToFrontStandPos",0.5f);
                        GetComponent<Animator>().SetBool("down",true);
                        break;
                    default:
                        transform.position=new Vector2(-19,transform.position.y);
                        break;
                }
                break;
        }
        switch(scene.name){
            case "IslandEdge0Scene":
                seasound=GameObject.Find("SeaSound").GetComponent<AudioSource>();
                Invoke("PlaySeaSound",0.5f);
                if(masterfoxri>=10){
                    transform.position=new Vector2(-6.7f,transform.position.y);
                    masterfox=GameObject.Find("MasterFox");
                    masterfox.transform.localScale=new Vector3(1.5f,1.5f,0);
                }
                closedmaterialsparchment=GameObject.Find("Materials_Parchment");
                closedmaterialsparchment.SetActive(false);
                if(!PlayerPrefs.HasKey("materialsparchmenttaken")&&(masterfoxri>7)){
                    closedmaterialsparchment.SetActive(true); 
                }
                break;
            case "CastleOutsideScene":
                PlayerPrefs.SetInt("masterfoxri",masterfoxri);
                sleeppotion=GameObject.Find("Sleep_Potion");
                sleeppotion.SetActive(false);
                if(!PlayerPrefs.HasKey("sleeppotiontaken")&&(mrdarkri>7)){
                    sleeppotion.SetActive(true); 
                }
                break;
            case "CastleInside0Scene":
                PlayerPrefs.SetInt("entredcastleinside0",1);
                transform.position=new Vector2(18f,-5f);
                Invoke("ToLeftStandPos",0.5f);
                GetComponent<Animator>().SetBool("left",true);
                rb2d.gravityScale=1f;
                break;
            case "IslandInside0Scene":
                foreach(string cuttreename in cuttreesnames){
                    GameObject cuttree=GameObject.Find(cuttreename);
                    cuttree.tag="Untagged";
                    cuttree.GetComponent<Animator>().SetBool("cut",true);
                }
                break;
            case "EntryScene":
            case "DeadScene":
                Destroy(canvas);
                Destroy(nextscenebtn);
                Destroy(gameObject);
                break;
        }
    }     
}
