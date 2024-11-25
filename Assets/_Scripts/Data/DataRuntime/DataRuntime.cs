using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataRuntime
{
    [SerializeField] private int level;
    [SerializeField] private int weapon;
    [SerializeField] private int gold;
    [SerializeField] private int skin;
    [SerializeField] private List<int> skinsOwned;
    [SerializeField] private List<int> weaponsOwned;
    public DataRuntime(int level, int weapon,int gold, int skin) {
        this.level = level;
        this.weapon = weapon;
        this.gold = gold;
        this.skin = skin;
        this.skinsOwned = new List<int>(0);
        this.weaponsOwned = new List<int>(0);
    }
    public DataRuntime()
    {
        this.level = 1;
        this.weapon = 0;
        this.gold = 0;
        this.skin = 0;
        this.skinsOwned = new List<int>(0);
        this.weaponsOwned = new List<int>(0);
    }

    public List<int> GetListSkinsOwned()
    {
        return this.skinsOwned;
    }

    public List<int> GetListWeaponsOwned()
    {
        return this.weaponsOwned;
    }
    public int Level()
    {
        return this.level;
    }
    public int Weapon()
    {
        return this.weapon;
    }
    public int Gold()
    {
        return this.gold;
    }
    public int Skin()
    {
        return this.skin;
    }
    public void SetLevel(int level)
    {
        this.level = level;
    }
    public void SetWeapon(int weapon)
    {
        this.weapon = weapon;
    }
    public void SetSkin(int skin)
    {
        this.skin = skin;
    }
    public void SetData(int level,int weapon,int gold,int skin)
    {
        this.level = level;
        this.weapon = weapon;
        this.gold = gold;
        this.skin = skin;
    }
    public DataRuntime DeepCopy()
    {
        return new DataRuntime(level, weapon,gold, skin); 
    }
}
