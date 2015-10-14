using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public FullStage CurrentStage;
    public List<FullStage> StageList = new List<FullStage>();
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}


[System.Serializable]
public class FullStage
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