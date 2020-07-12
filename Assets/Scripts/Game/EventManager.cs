using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eventd{
    public string[] alternatives;
    public string question;
    public bool itIsWithPlayers;
    public Eventd(string[] _alternatives, string _question,bool _itIsWithPlayers){
        alternatives = _alternatives;
        question = _question;
        itIsWithPlayers = _itIsWithPlayers;
    }

}

public class EventManager : MonoBehaviour
{
    private GameObject HUD;
    private GameObject gridObj;
    private GameController gameController;
    private string resultDialogue;
    public string[] posibleEvents;
    public GameObject DialogPanels;
    public GameObject questionPanel;
    public GameObject btnOption;
    public GameObject dayPanel;
    public bool changeDay=false;
    public bool result = false;
    public bool changeEvent = false;
    int count = 0;
    public bool doEvent = false;
    public bool kick = true;
    public bool gameboy = true;
    private Eventd guardEvent;
    private Eventd eatEvent;
    private Eventd desicionEvent;
    private Eventd kickEvent;
    private Eventd whoPlaysEvent;
    private Stack<Eventd> nextEvents;
    private Stack<string> reports;
    private List<int> deletedPlayers;
    string reportMessage;
    string mes;
    void Start()
    {
        //definitions
        gridObj =  GameObject.Find("Grid");
        HUD =  GameObject.Find("HUD");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        desicionEvent = new Eventd(posibleEvents, "Do you want to do something?",false);
        nextEvents = new Stack<Eventd>();
        deletedPlayers = new List<int>();
        reports = new Stack<string>();
        CreateEvents();
        //if(gameController.sceneType == GameController.SCENE_TYPE.BreakRoom)CreateEvents();
    }
    void CreateEvents(){
        guardEvent=new Eventd(GetPlayerNames(), "Who'll do guard today?",true);
        nextEvents.Push(guardEvent);
        if(gameController.food<gameController.players.Length && gameController.food>0){
            eatEvent=new Eventd(GetPlayerNames(), "Who should eat today?",true);
            nextEvents.Push(eatEvent);
        }
        nextEvents.Push(desicionEvent);
    }
    void Update()
    {
        if(!doEvent){
            if(nextEvents.Count <= 0){
                questionPanel.SetActive(false);
                changeDay = true;
                dayPanel.SetActive(true);
                dayPanel.transform.GetChild(0).GetComponent<Text>().text = "Day " + gameController.survivedDays.ToString();
                reportMessage = "";
                for(int i = 0;i<gameController.players.Length;i++){
                    DialogPanels.transform.GetChild(i).gameObject.SetActive(true);
                }
                DamageReport();
                print("kha");
                foreach (string i in reports){
                    reportMessage +=  " " +i;
                }
                
                dayPanel.transform.GetChild(1).GetComponent<Text>().text = reportMessage;
                doEvent= true;
            }
            else{
                StartEvent(nextEvents.Pop());
                doEvent = true;
            }
            
            
        }
        if(result){
            count++;
            if(count>360){
                count=0;
                changeEvent=true;
                result = false;
                PrintResult();
            }
        }
        if(changeEvent){
            count++;
            if(count>360){
                count=0;
                doEvent=false;
                changeEvent = false;
            }
        }
        if(changeDay){
            count++;
            if(count>360){
                count=0;
                changeDay = false;
                dayPanel.SetActive(false);
                
            }
        }
    }
    void PrintResult(){
        questionPanel.SetActive(true);
        questionPanel.transform.GetChild(0).GetComponent<Text>().text =resultDialogue;
    }
    void DamageReport(){
        for(int i = 0; i <gameController.players.Length;i++){
            gameController.players[i].GetComponent<Player>().hunger += RandomValue(10,20);
            if(gameController.players[i].GetComponent<Player>().hunger>100)gameController.players[i].GetComponent<Player>().hunger=100;
            gameController.players[i].GetComponent<Player>().sanity -= RandomValue(5,10);
            if(gameController.players[i].GetComponent<Player>().sanity<0)gameController.players[i].GetComponent<Player>().sanity=0;
            gameController.players[i].GetComponent<Player>().fatigue -= RandomValue(10,20);
            if(gameController.players[i].GetComponent<Player>().fatigue<0)gameController.players[i].GetComponent<Player>().fatigue=0;
            gameController.players[i].GetComponent<Player>().salubrity -= RandomValue(1,10);
            if(gameController.players[i].GetComponent<Player>().salubrity<0)gameController.players[i].GetComponent<Player>().salubrity=0;
            if(gameController.players[i].GetComponent<Player>().eats){
                gameController.players[i].GetComponent<Player>().hunger -= RandomValue(20,30);
                if(gameController.players[i].GetComponent<Player>().hunger<0)gameController.players[i].GetComponent<Player>().hunger=0;
            }
            if(gameController.players[i].GetComponent<Player>().console){
                gameController.players[i].GetComponent<Player>().sanity += RandomValue(20,30);
                if(gameController.players[i].GetComponent<Player>().sanity>100)gameController.players[i].GetComponent<Player>().sanity=100;
            }
            if(gameController.players[i].GetComponent<Player>().guard){
                gameController.players[i].GetComponent<Player>().fatigue += RandomValue(30,40);
                if(gameController.players[i].GetComponent<Player>().attackedInGuard() == 0){
                    mes = gameController.players[i].GetComponent<Player>().name + " was attacked in guard";
                    reports.Push(mes);
                    gameController.players[i].GetComponent<Player>().salubrity -= RandomValue(20,30);
                    if(gameController.players[i].GetComponent<Player>().salubrity<0)gameController.players[i].GetComponent<Player>().salubrity=0;
                }
                if(gameController.players[i].GetComponent<Player>().fatigue>100)gameController.players[i].GetComponent<Player>().fatigue=100;
            }
            if(gameController.players[i].GetComponent<Player>().sanity < 40){
                mes = gameController.players[i].GetComponent<Player>().name + " is kinda mad";
                reports.Push(mes);
            }
            if(gameController.players[i].GetComponent<Player>().sanity < 10){
                mes = gameController.players[i].GetComponent<Player>().name + " commit suicide";
                reports.Push(mes);
                deletedPlayers.Add(i);

            }
            if(gameController.players[i].GetComponent<Player>().fatigue > 60){
                mes = gameController.players[i].GetComponent<Player>().name + " is really tired";
                reports.Push(mes);
            }
            if(gameController.players[i].GetComponent<Player>().fatigue> 90){
                mes = gameController.players[i].GetComponent<Player>().name + " died from fatigue";
                reports.Push(mes);
                deletedPlayers.Add(i);

            }
            if(gameController.players[i].GetComponent<Player>().hunger > 60){
                mes = gameController.players[i].GetComponent<Player>().name + " is really hungry";
                reports.Push(mes);
            }
            if(gameController.players[i].GetComponent<Player>().hunger> 90){
                mes = gameController.players[i].GetComponent<Player>().name + " died from starvation";
                reports.Push(mes);
                deletedPlayers.Add(i);

            }
            if(gameController.players[i].GetComponent<Player>().salubrity< 10){
                mes = gameController.players[i].GetComponent<Player>().name + " was really bad and died";
                reports.Push(mes);
                deletedPlayers.Add(i);

            }
        }
        //comida obtenida en el refugio
        int foodObtained = RandomValue(1,5);
        gameController.food += foodObtained;
        mes = "The party found " + foodObtained.ToString() + " food portions";
        reports.Push(mes);

    }
    private int RandomValue(int _minValue,int _maxValue){
        return UnityEngine.Random.Range(_minValue,_maxValue);
    }
    void StartEvent(Eventd eventd){
        questionPanel.transform.GetChild(0).GetComponent<Text>().text = eventd.question;
        questionPanel.SetActive(true);   
        if(eventd.itIsWithPlayers)eventd.alternatives = GetPlayerNames();     
        for(int i = 0; i <eventd.alternatives.Length;i++){
            GameObject newBtn = Instantiate(btnOption);
            int index = i;
            newBtn.transform.SetParent(gridObj.transform,false);  
                  
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
        for(int i = 0;i<answers.Length;i++){
            DialogPanels.transform.GetChild(i).gameObject.SetActive(true);
            DialogPanels.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = alternatives[answers[i]];

        }
        questionPanel.SetActive(false);
        //gridObj.SetActive(false);  
        if(question == "Do you want to do something?"){
            resultDialogue= "";
            kick = true;
            gameboy = true;
            int answerCount =0;
            for(int i = 0;i<answers.Length;i++){
                switch(answers[i]){
                    case 0:
                        if(kick){
                            if(answerCount>0)resultDialogue+= " y ";
                            answerCount++;
                            resultDialogue+="someone must be kicked from the party";
                            print("se va a tener que echar a alguien de la party");
                            kickEvent = new Eventd(GetPlayerNames(), "Choose who have to left the party",true);
                            kick=false;
                            nextEvents.Push(kickEvent);
                        }
                        
                        break;
                    case 1:
                        if(gameboy){
                            resultDialogue+="it will be decided who'll have the console";
                            print("se va a decidir quien tendra la consola");
                            whoPlaysEvent = new Eventd(GetPlayerNames(), "Choose who will have the console",false);
                            gameboy=false;
                            nextEvents.Push(whoPlaysEvent);
                        }
                        break;
                    
                }
            }
        }
        if(question == "Choose who have to left the party"){
            int[] resultCount = OrderByFrequency(answers);
            resultDialogue= gameController.players[resultCount[0]].GetComponent<Player>().name + " have to left the party this night :c";
            mes = gameController.players[resultCount[0]].GetComponent<Player>().name + " left the party";
            reports.Push(mes);
            deletedPlayers.Add(resultCount[0]);
        }
        if(question == "Choose who will have the console"){
            int[] resultCount = OrderByFrequency(answers);
            resultDialogue= gameController.players[resultCount[0]].GetComponent<Player>().name + " will play with the console today";
            for(int i = 0;i< gameController.players.Length;i++){
                gameController.players[i].GetComponent<Player>().console = false;
            }
            gameController.players[resultCount[0]].GetComponent<Player>().console = true;
        }
        if(question == "Who'll do guard today?"){
            int[] resultCount = OrderByFrequency(answers);
            resultDialogue= gameController.players[resultCount[0]].GetComponent<Player>().name + " will do guard today";
            for(int i = 0;i< gameController.players.Length;i++){
                gameController.players[i].GetComponent<Player>().guard = false;
            }
            gameController.players[resultCount[0]].GetComponent<Player>().guard = true;
        }
        if(question == "Who should eat today?"){
            int[] resultCount = OrderByFrequency(answers);
            resultDialogue= "";

            for(int i = 0;i< gameController.players.Length;i++){
                gameController.players[i].GetComponent<Player>().eats = false;
            }
            for(int i = 0;i< gameController.food;i++){
                gameController.players[resultCount[i]].GetComponent<Player>().eats = true;
                resultDialogue += gameController.players[resultCount[i]].GetComponent<Player>().name + " ";
            }
            resultDialogue += "will eat today";
        }

        result = true;

    }
    int[] OrderByFrequency(int[] answers){
        int[] count = new int[gameController.players.Length];
        for(int i = 0;i < answers.Length;i++){
            count[answers[i]]++;
            
        }
        for(int i = 0;i < answers.Length;i++){
            answers[i] = i;
            
        }
        for(int i = 0;i<answers.Length;i++){
            for(int j = 0;j<answers.Length;j++){
                if(count[answers[i]]>count[answers[j]]){
                    int temp = answers[i];
                    answers[i] = answers[j];
                    answers[j] = temp;
                }
            }

        }
        for(int i = 0;i < answers.Length;i++){
            print(answers[i]);
            
        }
        return(answers);

    }
    
}
