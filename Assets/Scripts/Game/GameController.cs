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
        array = new Button[current_players];
    }

    void Start()
    {
        panelStats.SetActive(false);
        InstantiatePlayers();
        for(int i = 0; i < array.Length; i++){
            int index = i;
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

        panelStats.transform.GetChild(5).GetComponent<Text>().text="SEX: "+ players[index].sex.ToString();
        panelStats.transform.GetChild(6).GetComponent<Text>().text="STATUS: "+ players[index].status;
        panelStats.transform.GetChild(7).GetComponent<Text>().text="CAPACITY:"+ players[index].capacity.ToString();
        panelStats.transform.GetChild(8).GetComponent<Text>().text="WEIGHT MAX:"+ players[index].max_weight.ToString();
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
}
