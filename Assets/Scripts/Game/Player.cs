using UnityEngine;
using System;

[System.Serializable]
public class Player: MonoBehaviour{
    private GameController gameController;
    public string name;
    public string[] status = new string[1];
    public bool sex;
    public int capacity;
    public int max_weight;
    public int sanity;
    public int hunger;
    public int salubrity;
    public int fatigue;
    public bool gluttony;
    public bool sloth;
    public bool greed;
    public bool lust;
    public bool pride;
    public bool envy;
    public bool wrath;
    //desicion variables
    public bool console;
    public bool guard;
    public bool eats;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public Player(){
        
    }
    public void setName(string _name){
        name = _name;
    }
    public string getName(){
        return name;
    }
    public bool getSex(){
        return sex;
    }
    public void setCapacity(int _capacity){
        capacity = _capacity;
    }
    public int getCapacity(){
        return capacity;
    }
    public void setMaxWeight(int _max_weight){
        max_weight = _max_weight;
    }
    public int getMaxWeight(){
        return max_weight;
    }
    public void setSanity(int _value){
        sanity +=_value;
    }
    public int getSanity(){
        return sanity;
    }
    public void setHunger(int _value){
        hunger +=_value;
    }
    public int getHunger(){
        return hunger;
    }
    public void setSalubrity(int _value){
        salubrity +=_value;
    }
    public int getSalubrity(){
        return salubrity;
    }
    public void setFatigue(int _value){
        fatigue +=_value;
    }
    public int getFatigue(){
        return fatigue;
    }
    public void setStatus(string _status,int _indice){
        status[_indice] = _status;
    }
    public void addStatus(string _newStatus){
        string[] _newArray = new string[status.Length + 1];
        for (int i = 0; i < status.Length; i++)
        {
            _newArray[i] = status[i];
            
        }
        _newArray[status.Length] = _newStatus;
        status = _newArray;
    }
    public void deleteStatus(int _posDelete){
        string[] _newArray = new string[status.Length];
        for (int i = _posDelete-1; i < status.Length-1; i++)
        {
            _newArray[i] =status[i+1];
        }
        status = _newArray;
    }
    public string[] getStatus(){
        return status;
    }
   
    public int ChooseWithProbability(int[] probs){
        int rand;

        rand = UnityEngine.Random.Range(1,101);
        for(int i = 0; i < probs.Length;i++){
            if(i == probs.Length-1){
                return i;
            }
            if(rand<=probs[i]){
                return i;
            }
            probs[i+1]+=probs[i];

        }
        return 0;
    }

    public int CanEnterTheParty(Player otherPlayer){
        int probabilityChange = 0;
        //conditions
        if(otherPlayer.sex != sex)probabilityChange-=5;
        if(lust)probabilityChange-=20;
        if(sanity>80)probabilityChange+=5;
        if(sanity<20)probabilityChange-=5;
        if(pride)probabilityChange-=20;
        if(fatigue>60)probabilityChange+=5;
        if(salubrity>70)probabilityChange+=5;
        if(salubrity<30)probabilityChange-=5;
        //calculate prob
        int[] probs = new int[2];
        probs[0] = 60+probabilityChange;
        if(probs[0]>100)probs[0] = 100;
        if(probs[0]<0)probs[0] = 0;
        probs[1] = 100 - probs[0];
        return(ChooseWithProbability(probs));
    }
    public int HaveToLeaveTheParty(Player otherPlayer){
        int probabilityChange = 0;
        //conditions
        if(otherPlayer.sex != sex)probabilityChange+=5;
        if(lust)probabilityChange+=20;
        if(sanity>80)probabilityChange-=10;
        if(sanity<20)probabilityChange+=10;
        if(pride)probabilityChange+=20;
        if(fatigue>60)probabilityChange-=5;
        if(salubrity>70)probabilityChange-=5;
        if(salubrity<30)probabilityChange+=5;
        //calculate prob
        int[] probs = new int[2];
        probs[0] = 50+probabilityChange;
        if(probs[0]>100)probs[0] = 100;
        if(probs[0]<0)probs[0] = 0;
        probs[1] = 100 - probs[0];
        return(ChooseWithProbability(probs));
    }
    public int ChoosePath(){
        int probabilityChange = 0;
        //conditions
        //calculate prob
        int[] probs = new int[2];
        probs[0] = 50+probabilityChange;
        if(probs[0]>100)probs[0] = 100;
        if(probs[0]<0)probs[0] = 0;
        probs[1] = 100 - probs[0];
        return(ChooseWithProbability(probs));
    }
    public int WhoEat(string[] alternatives){
        GameObject[] otherPlayers = gameController.players;
        int[] probs = new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 1;
            if(otherPlayers[i].GetComponent<Player>().hunger>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().hunger>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().hunger>90)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().name == name){
                relevancyValue+=2;
                if(gluttony)relevancyValue+=7;
            }
            if(otherPlayers[i].GetComponent<Player>().salubrity<35)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().fatigue<40)relevancyValue+=1;
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }
    public int KickSomeone(string[] alternatives){
        GameObject[] otherPlayers = gameController.players;
        int[] probs = new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 1;
            if(otherPlayers[i].GetComponent<Player>().hunger>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().hunger>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().hunger>90)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().fatigue>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().fatigue>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().fatigue>90)relevancyValue+=3;
            
            if(otherPlayers[i].GetComponent<Player>().salubrity<80)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().salubrity<40)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().salubrity<20)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().sanity<80)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().sanity<40)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().sanity<20)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().name == name){
                relevancyValue=0;
            }
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }
    public int WhoGuards(string[] alternatives){
        GameObject[] otherPlayers = gameController.players;
        int[] probs = new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 1;
            if(otherPlayers[i].GetComponent<Player>().salubrity>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().salubrity>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().salubrity>90)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().sanity>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().sanity>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().sanity>90)relevancyValue+=3;
            
            if(otherPlayers[i].GetComponent<Player>().hunger<80)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().hunger<40)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().hunger<20)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().fatigue<80)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().fatigue<40)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().fatigue<20)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().name == name){
                relevancyValue=0;
            }
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }
    public int WhoPlay(string[] alternatives){
        GameObject[] otherPlayers = gameController.players;
        int[] probs= new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 1;
            if(otherPlayers[i].GetComponent<Player>().sanity>30)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().sanity>60)relevancyValue+=2;
            if(otherPlayers[i].GetComponent<Player>().sanity>90)relevancyValue+=3;
            if(otherPlayers[i].GetComponent<Player>().name == name){
                relevancyValue+=2;
                if(envy)relevancyValue+=7;
            }
            if(otherPlayers[i].GetComponent<Player>().salubrity<35)relevancyValue+=1;
            if(otherPlayers[i].GetComponent<Player>().fatigue<20)relevancyValue+=2;
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }
    public int attackedInGuard(){
        int[] probs = new int[2];
        probs[0] = 20;
        probs[1] = 100 - probs[0];
        return(ChooseWithProbability(probs));
    }

    public int DoSomething(string[] events){//falta agregar logica
        int[] probs= new int[3];
        probs[0] = 10;
        probs[1] = 10;
        probs[2] = 80;
        return(ChooseWithProbability(probs));
    }

    public int Question(string[] alternatives,string question){
        switch(question){
            case "Do you want to do something?":
                return(DoSomething(alternatives));
                break;
            case "Choose who have to left the party":
                return(KickSomeone(alternatives));
                break;
            case "Choose who will have the console":
                return(WhoPlay(alternatives));
                break;
            case "Who should eat today?":
                return(WhoEat(alternatives));
                break;
            case "Who'll do guard today?":
                return(WhoGuards(alternatives));
                break;

            
        }
        return 0;
        
            
    }



}
