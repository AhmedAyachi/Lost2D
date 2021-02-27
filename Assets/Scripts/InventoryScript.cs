using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour{
    public class inventoryitem{
        public string name{get;set;}
        public int quantity{get;set;}
        public GameObject go{get;set;}
        public inventoryitem(string Name,GameObject GO,int Quantity){
            name=Name;
            go=GO;
            quantity=Quantity;    
        }
    }
// -------------------------------------------------------------------------
    void Awake(){
        SceneManager.sceneLoaded+=OnSceneLoaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded-=OnSceneLoaded;
    }
// -------------------------------------------------------------------------
    public int indexof(string name,List<inventoryitem> list){
        bool found=false;
        int i=0;
        while(!found&&(i<list.Count)){
            found=(list[i].name==name)?true:false;
            i++;
        }
        i=!found?-1:i-1;
        return i;
    }
// -------------------------------------------------------------------------
    public List<inventoryitem> inventoryitems=new List<inventoryitem>();

    public GameObject openedmaterialsparchmentprefab,itemswindow,openedsleeppotionparchmentprefab,openedmaterialsparchment,itemsectionprefab,cobject;
    GameObject canvas,replywindow,itemscontent,itemsection,daylen,openedsleeppotionparchment,dogger,doggerdog;

    public Sprite masterfoxsprite,daylensprite,mrdarksprite,axesprite,sprite,doggersprite;

    Text replytext,speakername;
    Image speakerimage;
    Button nextbtn;
    AudioSource jaazsound,ambientsound;
    public bool daylenintrigger=false,addiblego=false,talkablego=false,checkitems=false;
    bool gotmaterials=false,gotbatwings=false,enteredcastle=false;

    public string objectname;
    string objecttag;
    //clicked Gameobject variables: cobject,talkablego,addiblego;
// -------------------------------------------------------------------------
    void Start(){
        daylen=GameObject.Find("Daylen");
        canvas=GameObject.Find("Canvas");
        GetComponent<Button>().onClick.AddListener(()=>{
            if(!itemswindow.activeSelf&&!replywindow.activeSelf){
                itemswindow.SetActive(true);
            }
            else{
                itemswindow.SetActive(false);
            }
        });
        itemscontent=itemswindow.transform.GetChild(0).GetChild(0).gameObject;
        itemsectionprefab=itemswindow.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        // -----------------------------------------------------------------
        replywindow=daylen.GetComponent<DaylenScript>().replywindow;
        replytext=daylen.GetComponent<DaylenScript>().replytext;
        speakername=daylen.GetComponent<DaylenScript>().speakername;
        speakerimage=daylen.GetComponent<DaylenScript>().speakerimage;
        nextbtn=daylen.GetComponent<DaylenScript>().nextbtn;
    }
// -------------------------------------------------------------------------
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        scene=SceneManager.GetActiveScene();
        switch(scene.name){
            case "IslandEdge0Scene":
                if((indexof("Axe",inventoryitems)!=-1)&&(indexof("Rope",inventoryitems)!=-1)){
                    int woodindex=indexof("Wood",inventoryitems);
                    if((woodindex!=-1)&&(inventoryitems[woodindex].quantity>=150)){
                        gotmaterials=true;
                    }
                }
                break;
            case "CastleInside0Scene":
                enteredcastle=true;
                int index=indexof("BatWing",inventoryitems)!=-1?indexof("BatWing",inventoryitems):indexof("Axe",inventoryitems);
                if(index!=-1){
                    int takenwings;
                    if(indexof("Axe",inventoryitems)!=-1){
                        takenwings=6;
                    }
                    else{
                        takenwings=inventoryitems[index].quantity;
                    }
                    for(int i=0;i<takenwings;i++){
                        GameObject batwing=GameObject.Find("BatWings("+i+")");
                        Destroy(batwing);
                    }
                }
                index=indexof("Sleep_Potion",inventoryitems);
                if(index!=-1){
                    Destroy(inventoryitems[index].go);
                    inventoryitems.Remove(inventoryitems[index]);
                }
                index=indexof("Rope",inventoryitems);
                if(index!=-1){
                    Destroy(GameObject.Find("Rope"));
                }
                break;
            case "CastleOutsideScene":
                if(enteredcastle){
                    Destroy(GameObject.Find("Knights"));
                }
                break;
            case "IslandInside0Scene":
                ambientsound=GameObject.Find("AmbientSound").GetComponent<AudioSource>();
                jaazsound=GameObject.Find("JaazSound").GetComponent<AudioSource>();
                dogger=GameObject.Find("Dogger");
                doggerdog=GameObject.Find("DoggerDog");
                int woodi=indexof("Wood",inventoryitems);
                if(woodi==-1){
                    dogger.SetActive(false);
                    doggerdog.SetActive(false);
                }
                else if((woodi!=-1)&&(inventoryitems[woodi].quantity==31)){
                    doggerdog.SetActive(false);
                }
                else if((woodi!=-1)&&(inventoryitems[woodi].quantity>31)){
                    ambientsound.mute=true;
                    jaazsound.Play(0);
                }
                break;
        }
    }
// -------------------------------------------------------------------------
    void Update(){
    // Clicked Gameobject Detection:
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit=Physics2D.Raycast(touchpos,Vector2.zero);
            if(hit.collider!=null&&!replywindow.activeInHierarchy){
                objecttag=hit.collider.tag;
                objectname=hit.collider.name;
                switch(objecttag){
                    case "collectablebyclick":
                        cobject=hit.collider.gameObject;
                        objectname=(objectname.Contains("("))?objectname.Substring(0,objectname.IndexOf("(")-1):objectname;
                        sprite=cobject.GetComponent<SpriteRenderer>()!=null?cobject.GetComponent<SpriteRenderer>().sprite:null;
                        addiblego=true;
                        break;
                    case "character":
                        talkablego=true;
                        break;
                }  
            }
        }
    // -----------------------------------------------------------------
        if(addiblego){
            if(addiblego&&(indexof(objectname,inventoryitems)==-1)){
                // Creation of the item section:
                itemsection=Instantiate(itemsectionprefab) as GameObject;
                itemsection.SetActive(true);
                itemsection.transform.SetParent(itemscontent.transform,false);
                itemsection.gameObject.name=objectname+"Item";
                itemsection.transform.GetChild(0).GetComponent<Text>().text=objectname;
                itemsection.transform.GetChild(1).GetComponent<Text>().text="quantity: 1";
                // -------------------------------------------------
                inventoryitem item=new inventoryitem(objectname,itemsection,1);
                inventoryitems.Add(item);
                itemsection.transform.GetChild(0).GetComponent<Text>().text=objectname.Replace("_","\n");
                itemsection.transform.GetChild(2).GetComponent<Image>().sprite=sprite;
                switch(objectname){
                    case "Materials_Parchment":
                        PlayerPrefs.SetInt("materialsparchmenttaken",1);
                        openedmaterialsparchment=Instantiate(openedmaterialsparchmentprefab) as GameObject;
                        openedmaterialsparchment.transform.SetParent(canvas.transform,false);
                        itemsection.GetComponent<Button>().onClick.AddListener(()=>{openedmaterialsparchment.SetActive(true);});
                        break;
                    case "Sleep_Potion":
                        PlayerPrefs.SetInt("sleeppotiontaken",1);
                        openedsleeppotionparchment=Instantiate(openedsleeppotionparchmentprefab) as GameObject;
                        openedsleeppotionparchment.transform.SetParent(canvas.transform,false);
                        itemsection.GetComponent<Button>().onClick.AddListener(()=>{openedsleeppotionparchment.SetActive(true);});
                        break;
                    case "Wood":
                        speakername.text="Dogger";
                        replytext.text="Hey!!!\nget  away  from  my  trees!!!";
                        speakerimage.sprite=doggersprite;
                        nextbtn.onClick.RemoveAllListeners();
                        nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);});
                        dogger.SetActive(true);
                        replywindow.SetActive(true);
                        int i=indexof(objectname,inventoryitems);
                        inventoryitems[i].quantity+=30;
                        inventoryitems[i].go.transform.GetChild(1).GetComponent<Text>().text="quantity: "+(inventoryitems[i].quantity).ToString();
                        openedmaterialsparchment.transform.GetChild(1).GetComponent<Text>().text=(inventoryitems[i].quantity).ToString();
                        break;
                } 
            }
            else if(addiblego&&(indexof(objectname,inventoryitems)>=0)){
                int i=indexof(objectname,inventoryitems);
                switch(objectname){
                    case "Wood":
                        if(inventoryitems[i].quantity==31){
                            speakername.text="Dogger";
                            replytext.text="You  want  to  see  the  hard  way  little  boy!!!!!!";
                            speakerimage.sprite=doggersprite;
                            nextbtn.onClick.RemoveAllListeners();
                            nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);doggerdog.SetActive(true);});
                            replywindow.SetActive(true);
                        }
                        int woodtoadd=Random.Range(30,70);
                        inventoryitems[i].quantity+=woodtoadd;
                        inventoryitems[i].go.transform.GetChild(1).GetComponent<Text>().text="quantity: "+(inventoryitems[i].quantity).ToString();
                        openedmaterialsparchment.transform.GetChild(1).GetComponent<Text>().text=(inventoryitems[i].quantity).ToString();
                        jaazsound=GameObject.Find("JaazSound").GetComponent<AudioSource>();
                        ambientsound.Pause();
                        jaazsound.Play(0);
                        break;
                    default:
                        inventoryitems[i].go.transform.GetChild(1).GetComponent<Text>().text="quantity: "+(++(inventoryitems[i].quantity)).ToString();
                        break;
                }
            }
            switch(objectname){
                case "Wood":
                    Animator animator=cobject.GetComponent<Animator>();
                    animator.SetBool("cut",true);
                    cobject.GetComponent<BoxCollider2D>().size=new Vector2(0.8f,0.55f);
                    cobject.GetComponent<SpriteRenderer>().color+=new Color(0,0,0,1f);
                    break;
                default:
                    Destroy(cobject);
                    break;
            }
            addiblego=false;
        }
    // -----------------------------------------------------------------
        if(talkablego){
            switch(objectname){
                case "MasterFox":
                    daylen.GetComponent<DaylenScript>().gotmaterials=gotmaterials;
                    //daylen.GetComponent<DaylenScript>().masterfoxclick=true;
                    if(!gotmaterials){
                        speakername.text="MasterFox";
                        replytext.text="Go  get  the  materials  please,  I'm  waiting";
                        speakerimage.sprite=masterfoxsprite;
                        nextbtn.onClick.RemoveAllListeners();
                        nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);});
                    }
                    else if(gotmaterials){
                        speakername.text="MasterFox";
                        replytext.text="did  you  bring  the  maretials  i  asked  you  ?";
                        speakerimage.sprite=masterfoxsprite;
                        nextbtn.onClick.RemoveAllListeners();
                        nextbtn.onClick.AddListener(()=>{daylen.GetComponent<DaylenScript>().NextReply("MASTERFOX");});
                        gotmaterials=false;
                    }
                    break;
                case "MrDark":
                    if(!PlayerPrefs.HasKey("mrdarkri")){
                        speakername.text="Daylen";
                        speakerimage.sprite=daylensprite;
                        replytext.text="hello,  can  i  borrow  your  axe  for a  while  please";
                        nextbtn.onClick.RemoveAllListeners();
                        nextbtn.onClick.AddListener(()=>{daylen.GetComponent<DaylenScript>().NextReply("MRDARK");});
                    }
                    else if((PlayerPrefs.GetInt("mrdarkri")>=9)&&(PlayerPrefs.GetInt("gotsomebatwings")==0)/*!gotbatwings*/){
                        speakername.text="mr  dark";
                        speakerimage.sprite=mrdarksprite;
                        replytext.text="No  bat wings  No  axe,  We  had  a  deal  little  boy !!!";
                        nextbtn.onClick.RemoveAllListeners();
                        nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);});
                    }
                    else if(PlayerPrefs.GetInt("gotsomebatwings")==1){
                        int batwingsnumber=7,batwingsindex=indexof("BatWing",inventoryitems);
                        if(batwingsindex!=-1){
                            batwingsnumber=inventoryitems[batwingsindex].quantity;
                        }
                        if(batwingsnumber<6){
                            speakername.text="mr  dark";
                            speakerimage.sprite=mrdarksprite;
                            replytext.text="I  said  six  little  boy  !!!\nno  six  batwings  no  axe";
                            nextbtn.onClick.RemoveAllListeners();
                            nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);}); 
                        }
                        else if((batwingsnumber==6)){
                            daylen.GetComponent<DaylenScript>().gotallbatwings=true;
                            if(batwingsindex!=-1){
                                inventoryitems[batwingsindex].name="Axe";
                                inventoryitems[batwingsindex].quantity=1;
                                inventoryitems[batwingsindex].go.transform.GetChild(0).GetComponent<Text>().text="Axe";
                                inventoryitems[batwingsindex].go.transform.GetChild(1).GetComponent<Text>().text="quantity: 1";
                                inventoryitems[batwingsindex].go.transform.GetChild(2).GetComponent<Image>().sprite=axesprite;
                                inventoryitems[batwingsindex].go.gameObject.name="AxeItem";
                                openedmaterialsparchment.transform.GetChild(0).GetComponent<Text>().text="1";
                            }
                            speakername.text="Daylen";
                            speakerimage.sprite=daylensprite;
                            replytext.text="here  we  go,  these  are  the  wings  you  asked  me  for";
                            nextbtn.onClick.RemoveAllListeners();
                            nextbtn.onClick.AddListener(()=>{daylen.GetComponent<DaylenScript>().NextReply("MRDARK");});
                        }
                        else{
                            speakername.text="mr  dark";
                            speakerimage.sprite=mrdarksprite;
                            replytext.text="Thank  you  for  helping,  expect  seeing  you  when  my  new  potion  is  ready";
                            nextbtn.onClick.RemoveAllListeners();
                            nextbtn.onClick.AddListener(()=>{replywindow.SetActive(false);}); 
                        }
                    }
                    break;
            }
            talkablego=false;
            replywindow.SetActive(true);
        }
    }
}
