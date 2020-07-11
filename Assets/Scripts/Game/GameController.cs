using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    private int max_players = 5;
    private int current_players = 3;
    [SerializeField] private GameObject parentPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3[] posPlayer;
    public GameObject[] players;
    private Button[] array;
    [Header("Objects")]
    [SerializeField] private Button btnSetting;
    [Header("Panels")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelSetName;
    [SerializeField] private GameObject panelSettings;
    [Header("Prefab Player Icon")]
    [SerializeField] private GameObject prefabPlayer;
    [SerializeField] private Transform panelPlayers;
    private bool sapitoHuebon;
    private void Awake() {
        players  = new GameObject[current_players];
        array = new Button[current_players];
        InitPanels();
        InstantiatePlayers();
        sapitoHuebon = false;
    }
    int current_index = 0;
    void Start()
    {
        InitPlayers();
        panelStats.SetActive(true);
        panelPlayers.gameObject.SetActive(true);
        panelSetName.SetActive(false);
        ButtonsFuncionality();
        sapitoHuebon = true;
        //StartGame();
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
        if (sapitoHuebon)
            ShowPlayerStats(current_index);
    }
    //=============
    void AddStatus(){
        players[current_index].GetComponent<Player>().addStatus("ENFERMO");
    }
    void RemoveStatus(){
        players[current_index].GetComponent<Player>().deleteStatus(1);
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
            players[i] = Instantiate(player, posPlayer[i], Quaternion.identity, parentPlayer.transform);
            players[i].GetComponent<Player>().sanity = 60;
            players[i].GetComponent<Player>().salubrity = 100;
            players[i].GetComponent<Player>().hunger = 100;
            players[i].GetComponent<Player>().fatigue = 100;
            players[i].GetComponent<Player>().status = new string[]{"N","ENF","XDD","AA"};
        }
        players[0].name = "Jose";
        players[1].name = "Juan";
        players[2].name = "Pedro";
    }
    void InitPanels(){
        panelPlayers.gameObject.SetActive(false);
        panelStats.SetActive(false);
        panelSettings.SetActive(false);
        

        // btnSetting.onClick.AddListener(()=>GoSettings());
        btnSetting.onClick.AddListener(()=>RemoveStatus());
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
}
