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
    [SerializeField]
    private GameObject panelStats;

    [SerializeField]
    private GameObject prefabPlayer;
    [SerializeField]
    private Transform panelPlayers;
    private void Awake() {
        players  = new Player[current_players];
        InitPlayers();
        array = new Button[current_players];
        InstantiatePlayers();
        ButtonsFuncionality();

    }
    int current_index = 0;
    void Start()
    {
        panelStats.SetActive(false);
    }
    void ButtonsFuncionality(){
        for(int i = 0; i < array.Length; i++){
            int index = i;
            current_index = i;
            array[i].transform.GetChild(0).GetComponent<Text>().text = players[i].name;
            array[i].onClick.AddListener(() => ShowPlayerStats(index));
        }
    }
    void Update()
    {
    }

    void ShowPlayerStats(int index){
        panelStats.SetActive(true);
        panelStats.transform.GetChild(1).GetComponent<Slider>().value = players[index].sanity;
        panelStats.transform.GetChild(2).GetComponent<Slider>().value = players[index].hunger;
        panelStats.transform.GetChild(3).GetComponent<Slider>().value = players[index].salubrity;
        panelStats.transform.GetChild(4).GetComponent<Slider>().value = players[index].fatigue;
        string _sex = (players[index].sex)? "HOMBRE":" MUJER";
        panelStats.transform.GetChild(5).GetComponent<Text>().text="SEX: "+ _sex;
        panelStats.transform.GetChild(6).GetComponent<Text>().text="STATUS: "+ players[index].status;
        panelStats.transform.GetChild(7).GetComponent<Text>().text="CAPACITY:"+ players[index].capacity.ToString();
        panelStats.transform.GetChild(8).GetComponent<Text>().text="WEIGHT MAX:"+ players[index].max_weight.ToString();
        StartCoroutine(ClosePanelStats());
    }

    IEnumerator ClosePanelStats(){
        yield return new WaitForSeconds(5);
        panelStats.SetActive(false);
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
            players[i].sanity = 100;
            players[i].salubrity = 100;
            players[i].hunger = 100;
            players[i].fatigue = 100;
        }
        players[0].name = "Jose";
        players[1].name = "Juan";
        players[2].name = "Pedro";
    }
}
