using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Characters/Player")]
public class ScriptableObjectPlayer : ScriptableObject
{
    public string _name;
    public string[] _status = new string[1];
    public bool _sex;
    public int _capacity;
    public int _max_weight;
    public int _sanity;
    public int _hunger;
    public int _salubrity;
    public int _fatigue;
    public bool _gluttony;
    public bool _sloth;
    public bool _greed;
    public bool _lust;
    public bool _pride;
    public bool _envy;
    public bool _wrath;
}
