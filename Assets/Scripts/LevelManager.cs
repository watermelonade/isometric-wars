using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public FullLevel CurrentLevel;
    public List<FullLevel> LevelList = new List<FullLevel>();
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}


[System.Serializable]
public class FullLevel
{
    //public MapType type;
    //public CutScene cut;
    public string name;
    public float time;
    /*public string enemies;
    public int enemies_health;
    public int enemies_strenght;
    public string WaveOrder;
    //public List<Weapon> WeaponList;
    public int reward;*/
}