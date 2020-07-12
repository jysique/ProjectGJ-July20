using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player[] players;

    public static PlayerData instance;
    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        players = new Player[GameController.instance.current_players];
    }

    public void SaveDataPlayer(){
        print("GUARDANDO DATA");
        for (int i = 0; i < GameController.instance.current_players;)
        {
            players[i] = GameController.instance.players[i].GetComponent<Player>();
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
