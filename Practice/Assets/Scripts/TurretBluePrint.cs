using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;
    public int cost;
    public int upgradeCost;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public float returnPrecentage;

    public float GetReturnValue()
    {
        if(returnPrecentage > 1)
        {
            returnPrecentage = returnPrecentage / 100;
        }
        return cost * returnPrecentage;
    }
    public float GetUpgradedReturnValue()
    {
        return GetReturnValue() + (upgradeCost * returnPrecentage); 
    }
}
