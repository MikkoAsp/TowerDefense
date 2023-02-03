using UnityEngine;
using TMPro;
using DG.Tweening;
public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint betterTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laser;
    private Vector3 endpos;
    private Vector3 startpos;
    [Header("User Interface")]
    public GameObject CloseShopButton;
    public GameObject OpenShopButton;
    public float TransitionTime;


    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
        endpos = transform.position + new Vector3(250, 0, 0);
        startpos = transform.position;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectBetterTurret()
    {
        Debug.Log("Better Turret selected");
        buildManager.SelectTurretToBuild(betterTurret);

    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile launcher selected");
        buildManager.SelectTurretToBuild(missileLauncher);

    }
    public void SelectLaser()
    {
        Debug.Log("Laser selected");
        buildManager.SelectTurretToBuild(laser);

    }
    public void CloseShopInUI()
    {
        OpenShopButton.SetActive(true);
        CloseShopButton.SetActive(false);

        transform.DOMove(endpos, TransitionTime);
    }
    public void OpenShopInUI()
    {
        CloseShopButton.SetActive(true);
        OpenShopButton.SetActive(false);

        transform.DOMove(startpos, TransitionTime);
    }
}