using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
public class GameController : MonoBehaviour
{
    private int max_players = 5;
    public int current_players;
    public enum SCENE_TYPE {
        InitialRoom,
        BreakRoom,
        ScoutRoom,
    };
    public SCENE_TYPE sceneType;
    public Transform goal;
    [SerializeField] private GameObject parentPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3[] posPlayer;
    
    private Button[] array;
    [Header("Party data")]
    public GameObject[] players;
    public int food = 7;
    public int survivedDays = 1;
    [Header("Objects")]
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnBackSetting;
    [Header("Panels")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelSetName;
    [SerializeField] private GameObject panelSettings;
    [Header("Prefab Player Icon")]
    [SerializeField] private GameObject prefabPlayer;
    [SerializeField] private Transform panelPlayers;

    Dictionary<string,bool> list_users = new Dictionary<string, bool>();
    private bool sapitoHuebon;

    public static GameController instance;
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        if(sceneType == SCENE_TYPE.InitialRoom){
            current_players = 3;
        }else{
            current_players = PlayerData.instance.current_players;
        }
        players  = new GameObject[current_players];
        array = new Button[current_players];
        InitPanels();
        InstantiateButton();
        sapitoHuebon = false;
    }
    int current_index = 0;
    void Start()
    {
        AddUserToDicc();
        InitPlayers();
        panelStats.SetActive(true);
        panelPlayers.gameObject.SetActive(true);
        panelSetName.SetActive(false);
        ButtonsFuncionality();
        sapitoHuebon = true;
        ShowPlayerStats(current_index);
        //StartGame();

    }

    void AddUserToDicc(){
        list_users.Add("Joseph", false);  
        list_users.Add("Lina",true);  
        list_users.Add("Chuck", false);
        list_users.Add("Deck", false);
        list_users.Add("Steward", false);
        list_users.Add("Alpha", false);
        list_users.Add("Omega", true);
        list_users.Add("Gamma", false);
        list_users.Add("Juana", true);
        list_users.Add("Alejandro", false);
        list_users.Add("Ana", true);
        list_users.Add("Karina", true);
        list_users.Add("Angel", false);
    }
    void ButtonsFuncionality(){
        
        for(int i = 0; i < array.Length; i++){
            int index = i;
            //current_index = i;
            array[i].transform.GetChild(0).GetComponent<Text>().text = players[i].GetComponent<Player>().name;
            array[i].onClick.AddListener(() => ChangeIndex(index));
        }

    }
    void ChangeIndex(int _index){
        current_index = _index;
        ShowPlayerStats(current_index);
    }

    void ShowPlayerStats(int index){
        // panelStats.transform.GetChild(1).GetComponent<Slider>().value = PlayerData.instance.players[index].GetComponent<Player>().sanity;
        panelStats.transform.GetChild(1).GetComponent<Slider>().value = players[index].GetComponent<Player>().sanity;
        panelStats.transform.GetChild(2).GetComponent<Slider>().value = players[index].GetComponent<Player>().hunger;
        panelStats.transform.GetChild(3).GetComponent<Slider>().value = players[index].GetComponent<Player>().salubrity;
        panelStats.transform.GetChild(4).GetComponent<Slider>().value = players[index].GetComponent<Player>().fatigue;
        string _sex = (players[index].GetComponent<Player>().sex)? " MUJER":"HOMBRE";
        panelStats.transform.GetChild(5).GetComponent<Text>().text="SEX: "+ _sex;
        
        string _status = " ";
        for (int i = 0; i < players[index].GetComponent<Player>().getStatus().Length; i++)
        {
            _status+= players[index].GetComponent<Player>().getStatus()[i] + " ";
        }
        panelStats.transform.GetChild(6).GetComponent<Text>().text="STATUS: "+ _status;
        panelStats.transform.GetChild(7).GetComponent<Text>().text="CAPACITY:"+ players[index].GetComponent<Player>().capacity.ToString();
        panelStats.transform.GetChild(8).GetComponent<Text>().text="WEIGHT MAX:"+ players[index].GetComponent<Player>().max_weight.ToString();
        panelStats.transform.GetChild(9).GetComponent<Text>().text= players[index].name;
    }
    void Update()
    {
        // if (sapitoHuebon)
            // ShowPlayerStats(current_index);
    }

    void AddStatus(){
        players[current_index].GetComponent<Player>().addStatus("ENFERMO");
    }
    void RemoveStatus(){
        players[current_index].GetComponent<Player>().deleteStatus(1);
    }
    void InstantiateButton(){
        foreach (Transform childTransform in panelPlayers.transform) {
            Destroy(childTransform.gameObject);
        }
        if (players.Length<= max_players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                GameObject a = Instantiate(prefabPlayer);
                a.transform.SetParent(panelPlayers.transform,false);
                array[i] = a.GetComponent<Button>();
            }
        }
    }
    void InitPlayers(){
        System.Random r = new System.Random();
        if (sceneType == SCENE_TYPE.InitialRoom)
        {
        for (int i = 0; i < players.Length; i++)
            {
                players[i] = Instantiate(player, posPlayer[i], Quaternion.identity, parentPlayer.transform);

                // players[i].GetComponent<Player>().name = 
                KeyValuePair<string,bool> pair = list_users.ElementAt(RandomValue(0,list_users.Count));
                players[i].name = pair.Key;
                players[i].transform.GetChild(0).GetComponent<TextMesh>().text = pair.Key;
                players[i].GetComponent<Player>().name = pair.Key;
                players[i].GetComponent<Player>().sex = pair.Value;
                players[i].GetComponent<Player>().capacity = 2;
                players[i].GetComponent<Player>().max_weight = RandomValue(60,100);
                players[i].GetComponent<Player>().sanity= RandomValue(80,100);
                players[i].GetComponent<Player>().hunger=RandomValue(0,30);
                players[i].GetComponent<Player>().salubrity=RandomValue(80,100);
                players[i].GetComponent<Player>().fatigue=RandomValue(0,30);
                players[i].GetComponent<Player>().status = new string[0];

                players[i].GetComponent<Player>().gluttony = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().sloth = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().greed = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().lust = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().pride = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().envy = Convert.ToBoolean(RandomValue(0,1));
                players[i].GetComponent<Player>().wrath = Convert.ToBoolean(RandomValue(0,1));

            }
        }else{
            for (int i = 0; i < players.Length; i++)
            {   
                players[i] = Instantiate(player, posPlayer[i], Quaternion.identity, parentPlayer.transform);
                players[i].transform.GetChild(0).GetComponent<TextMesh>().text = PlayerData.instance.players[i].name;
                players[i].GetComponent<Player>().name = PlayerData.instance.players[i].name;
                players[i].GetComponent<Player>().sex = PlayerData.instance.players[i].sex;
                players[i].GetComponent<Player>().capacity = PlayerData.instance.players[i].capacity;
                players[i].GetComponent<Player>().max_weight = PlayerData.instance.players[i].max_weight;
                players[i].GetComponent<Player>().sanity= PlayerData.instance.players[i].sanity;
                players[i].GetComponent<Player>().hunger=PlayerData.instance.players[i].hunger;
                players[i].GetComponent<Player>().salubrity=PlayerData.instance.players[i].salubrity;
                players[i].GetComponent<Player>().fatigue=PlayerData.instance.players[i].fatigue;
                players[i].GetComponent<Player>().status = PlayerData.instance.players[i].status;

                players[i].GetComponent<Player>().gluttony = PlayerData.instance.players[i].gluttony;
                players[i].GetComponent<Player>().sloth = PlayerData.instance.players[i].sloth;
                players[i].GetComponent<Player>().greed = PlayerData.instance.players[i].greed;
                players[i].GetComponent<Player>().lust = PlayerData.instance.players[i].lust;
                players[i].GetComponent<Player>().pride = PlayerData.instance.players[i].pride;
                players[i].GetComponent<Player>().envy = PlayerData.instance.players[i].envy;
                players[i].GetComponent<Player>().wrath = PlayerData.instance.players[i].wrath;

                players[i].name = PlayerData.instance.players[i].name;
            }
        }  
    }
    private int RandomValue(int _minValue,int _maxValue){
        return UnityEngine.Random.Range(_minValue,_maxValue);
    }
    void InitPanels(){
        panelPlayers.gameObject.SetActive(false);
        panelStats.SetActive(false);
        panelSettings.SetActive(false);
        
        //btnBackSetting.onClick.AddListener(()=>BackToGame());
        // btnSetting.onClick.AddListener(()=>GoSettings());
        //btnSetting.onClick.AddListener(()=>RemoveStatus());
        //btnSetting.onClick.AddListener(()=>ChangeScene());
        btnSetting.onClick.AddListener(()=>KickPlayerEvent(1));
    }
    void ChangeScene(){
        PlayerData.instance.SaveDataPlayer();
        PlayerPrefs.SetInt("current_players",current_players);
        SceneManager.LoadScene("Temp");
    }

    public void KickPlayerEvent(int _posDelete){
        if (_posDelete > 0 && _posDelete < players.Length)
        {
            Destroy(players[_posDelete]);
            GameObject[] _newArray = new GameObject[players.Length-1];
            Button[] _newArray2 = new Button[array.Length-1];

            _newArray = players.Where((val,idx)=>idx !=_posDelete).ToArray();
            _newArray2 = array.Where((val,idx)=>idx !=_posDelete).ToArray();
            players = _newArray;
            array = _newArray2;
        }
        current_players--;
        InstantiateButton();
        ButtonsFuncionality();
        ShowPlayerStats(current_index);
        //ChangeScene();
    }

    void SetName(){
        panelSetName.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>RegisterPlayer());
    }
    void StartGame(){
        panelSetName.SetActive(true);
        SetName();
    }
    void RegisterPlayer()
    {
        
        InputField _temp =  panelSetName.transform.GetChild(2).GetComponent<InputField>();
        Text temp = _temp.transform.GetChild(2).GetComponent<Text>();
        
        players[0].GetComponent<Player>().setName(_temp.text);
        panelPlayers.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _temp.text;
        
        players[0].name = _temp.text;
        current_index = 0;
        panelSetName.SetActive(false);
    }
    void GoSettings(){
        panelSettings.SetActive(true);
    }

    void BackToGame(){
        panelSettings.SetActive(false);
    }
}