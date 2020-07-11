using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    private int max_players = 5;
    private int current_players = 3;
    public Player[] players;
    private Button[] array;
    [Header("Objects")]
    [SerializeField]
    private Button btnSetting;
    [Header("Panels")]
    [SerializeField]
    private GameObject panelStats;
    [SerializeField]
    private GameObject panelSetName;
        [SerializeField]
    private GameObject panelSettings;
    [Header("Prefab Player Icon")]
    [SerializeField]
    private GameObject prefabPlayer;
    
    [SerializeField]
    private Transform panelPlayers;

    private bool _tempBool;
    private void Awake() {
        InitPanels();
    }
    int current_index = 0;
    void Start()
    {
        SetName();
    }
    void ButtonsFuncionality(){
        for(int i = 0; i < array.Length; i++){
            int index = i;
            current_index = i;
            array[i].transform.GetChild(0).GetComponent<Text>().text = players[i].name;
            array[i].onClick.AddListener(() => ChangeIndex(index));
        }
    }
    void ChangeIndex(int _index){
        current_index = _index;
    }

    void ShowPlayerStats(int index){
        panelStats.transform.GetChild(1).GetComponent<Slider>().value = players[index].sanity;
        panelStats.transform.GetChild(2).GetComponent<Slider>().value = players[index].hunger;
        panelStats.transform.GetChild(3).GetComponent<Slider>().value = players[index].salubrity;
        panelStats.transform.GetChild(4).GetComponent<Slider>().value = players[index].fatigue;
        string _sex = (players[index].sex)? " MUJER":"HOMBRE";
        panelStats.transform.GetChild(5).GetComponent<Text>().text="SEX: "+ _sex;
        
        string _status = " ";
        for (int i = 0; i < players[index].getStatus().Length; i++)
        {
            _status+= players[index].getStatus()[i] + " ";
        }
        panelStats.transform.GetChild(6).GetComponent<Text>().text="STATUS: "+ _status;
        panelStats.transform.GetChild(7).GetComponent<Text>().text="CAPACITY:"+ players[index].capacity.ToString();
        panelStats.transform.GetChild(8).GetComponent<Text>().text="WEIGHT MAX:"+ players[index].max_weight.ToString();
        panelStats.transform.GetChild(9).GetComponent<Text>().text= players[index].name;
    }
    void Update()
    {
        //print(current_index);
        if(_tempBool){
            ShowPlayerStats(current_index);
        }
        
    }
    //=============
    void AddStatus(){
        players[current_index].addStatus("ENFERMO");
    }
    void RemoveStatus(){
        players[current_index].deleteStatus(1);
    }
    void InstantiatePlayers(){
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
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Player();   
            players[i].sanity = 60;
            players[i].salubrity = 100;
            players[i].hunger = 100;
            players[i].fatigue = 100;
            players[i].status = new string[]{"N","ENF","XDD","AA"};
        }
        players[0].name = "Jose";
        players[1].name = "Juan";
        players[2].name = "Pedro";
    }
    void InitPanels(){
        panelPlayers.gameObject.SetActive(false);
        panelStats.SetActive(false);
        panelSettings.SetActive(false);
        panelSetName.SetActive(true);

        // btnSetting.onClick.AddListener(()=>GoSettings());
        btnSetting.onClick.AddListener(()=>RemoveStatus());
    }
    void SetName(){
        panelSetName.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>RegisterPlayer());
    }
    void RegisterPlayer(){
        players  = new Player[current_players];
        array = new Button[current_players];
        
        InitPlayers();
        InstantiatePlayers();
        ButtonsFuncionality();
        _tempBool = true;

        InputField _temp =  panelSetName.transform.GetChild(2).GetComponent<InputField>();
        players[0].setName(_temp.text);
        panelStats.SetActive(true);
        panelPlayers.gameObject.SetActive(true);
        panelSetName.SetActive(false);

        // ShowPlayerStats(0);
    }
    void GoSettings(){
        panelSettings.SetActive(true);
    }
}
