using UnityEngine;
using System;
public enum StatePlayer{
    SICKNESS,
    OPEN_WOUND,
    NORMAL
};

[System.Serializable]
public class Player: MonoBehaviour{
    public string name;
    public StatePlayer status = StatePlayer.NORMAL;
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
            else{
                probs[i+1]+=probs[i];
                return 0;
            }

        }
        return 0;
    }

    public bool CanEnterTheParty(Player otherPlayer){
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
        return(Convert.ToBoolean(ChooseWithProbability(probs)));
    }
    public bool HaveToLeaveTheParty(Player otherPlayer){
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
        return(Convert.ToBoolean(ChooseWithProbability(probs)));
    }
    public bool ChoosePath(){
        int probabilityChange = 0;
        //conditions
        //calculate prob
        int[] probs = new int[2];
        probs[0] = 50+probabilityChange;
        if(probs[0]>100)probs[0] = 100;
        if(probs[0]<0)probs[0] = 0;
        probs[1] = 100 - probs[0];
        return(Convert.ToBoolean(ChooseWithProbability(probs)));
    }
    public int WhoEat(Player[] otherPlayers){
        int[] probs = new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 0;
            if(otherPlayers[i].hunger>30)relevancyValue+=1;
            if(otherPlayers[i].hunger>60)relevancyValue+=2;
            if(otherPlayers[i].hunger>90)relevancyValue+=3;
            if(otherPlayers[i].name == name){
                relevancyValue+=2;
                if(gluttony)relevancyValue+=7;
            }
            if(otherPlayers[i].salubrity<35)relevancyValue+=2;
            if(otherPlayers[i].fatigue<40)relevancyValue+=1;
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }
    public int WhoPlay(Player[] otherPlayers){
        int[] probs= new int[otherPlayers.Length];
        int total=0;
        for(int i = 0; i < otherPlayers.Length; i++){
            int relevancyValue = 0;
            if(otherPlayers[i].sanity>30)relevancyValue+=1;
            if(otherPlayers[i].sanity>60)relevancyValue+=2;
            if(otherPlayers[i].sanity>90)relevancyValue+=3;
            if(otherPlayers[i].name == name){
                relevancyValue+=2;
                if(envy)relevancyValue+=7;
            }
            if(otherPlayers[i].salubrity<35)relevancyValue+=1;
            if(otherPlayers[i].fatigue<20)relevancyValue+=2;
            probs[i] = relevancyValue;
            total+=relevancyValue;
        }
        for(int i = 0; i < probs.Length; i++){
            probs[i]= (probs[i]*100)/total;
        }
        return(ChooseWithProbability(probs));
    }



}
