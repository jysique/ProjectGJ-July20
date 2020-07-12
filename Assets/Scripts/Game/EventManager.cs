using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eventd{
    public string[] alternatives;
    public string question;
    public Eventd(string[] _alternatives, string _question){
        alternatives = _alternatives;
        question = _question;
    }

}

public class EventManager : MonoBehaviour
{
    private GameObject HUD;
    private GameObject gridObj;
    private GameController gameController;

    public string[] posibleEvents;
    public GameObject questionPanel;
    public GameObject btnOption;
    public bool doEvent = false;
    public bool kick = true;
    public bool gameboy = true;
    private Eventd desicionEvent;
    private Eventd kickEvent;
    private Eventd whoPlaysEvent;
    private Stack<Eventd> nextEvents;
    void Start()
    {
        //definitions
        gridObj =  GameObject.Find("Grid");
        HUD =  GameObject.Find("HUD");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        desicionEvent = new Eventd(posibleEvents, "Do you want to do something?");
        nextEvents = new Stack<Eventd>();
        
        nextEvents.Push(desicionEvent);
    }
    void Update()
    {
        if(!doEvent){
            StartEvent(nextEvents.Pop());
            doEvent = true;
        }
    }

    void StartEvent(Eventd eventd){
        questionPanel.transform.GetChild(0).GetComponent<Text>().text = eventd.question;
        questionPanel.SetActive(true);        
        for(int i = 0; i <eventd.alternatives.Length;i++){
            GameObject newBtn = Instantiate(btnOption);
            int index = i;
            newBtn.transform.SetParent(gridObj.transform,false);  
            print(eventd.alternatives[i]);         
            newBtn.transform.GetChild(0).GetComponent<Text>().text = eventd.alternatives[i];
            newBtn.GetComponent<Button>().onClick.AddListener(() => MakeDesicion(index, eventd.alternatives,eventd.question));
        }

        
    }
    public string[] GetPlayerNames(){
        string[] names = new string[gameController.players.Length];
        for(int i = 0; i < names.Length;i++){
            names[i] = gameController.players[i].GetComponent<Player>().name;
        }
        return names;

    }
    void MakeDesicion(int index, string[] alternatives,string question){
        
        int[] answers = new int[gameController.players.Length];
        answers[0] = index;
        questionPanel.SetActive(false);
        foreach (Transform child in gridObj.transform) {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 1; i<gameController.players.Length;i++){
            int des  = gameController.players[i].GetComponent<Player>().Question(alternatives,question);
            answers[i] = des;
        }
        if(question == "Do you want to do something?"){
            kick = true;
            gameboy = true;
            for(int i = 0;i<answers.Length;i++){
                switch(answers[i]){
                    case 0:
                        ;
                        if(kick){
                            print("se va a tener que echar a alguien de la party");
                            kickEvent = new Eventd(GetPlayerNames(), "Choose who have to left the party");
                            kick=false;
                            nextEvents.Push(kickEvent);
                        }
                        
                        break;
                    case 1:
                        if(gameboy){
                            print("se va a decidir quien tendra la consola");
                            whoPlaysEvent = new Eventd(GetPlayerNames(), "Choose who will have the console");
                            gameboy=false;
                            nextEvents.Push(whoPlaysEvent);
                        }
                        break;
                    
                }
            }
        }
        doEvent = false;

    }
    
}
