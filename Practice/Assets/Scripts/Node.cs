using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour {
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 offset;

    [HideInInspector]
    public GameObject turretInNode;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isFullyUpgraded = false;
    private Color startColor;
    private Renderer _renderer;
    BuildManager buildManager;


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        startColor = _renderer.material.color;

        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPos()
    {
        return transform.position + offset;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //If the turret is already placed
        if (turretInNode != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        //If there isnt any turret selected
        if (!buildManager.CanBuild)
            return;
        BuildTurret(buildManager.GetTurretToBuild());
    }
    void BuildTurret(TurretBluePrint _turretBlueprint)
    {
        if (PlayerStats.money < _turretBlueprint.cost)
        {
            return;
        }
        PlayerStats.money -= _turretBlueprint.cost;
        GameObject turret = Instantiate(_turretBlueprint.prefab, GetBuildPos(), Quaternion.identity);
        turretInNode = turret;

        turretBluePrint = _turretBlueprint;

        GameObject effect = Instantiate(_turretBlueprint.buildEffect, GetBuildPos(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBluePrint.upgradeCost)
        {
            return;
        }
        PlayerStats.money -= turretBluePrint.upgradeCost;
        Destroy(turretInNode);
        GameObject turret = Instantiate(turretBluePrint.upgradedPrefab, GetBuildPos(), Quaternion.identity);
        turretInNode = turret;
        GameObject effect = Instantiate(turretBluePrint.buildEffect, GetBuildPos(), Quaternion.identity);
        Destroy(effect, 3f);
        isFullyUpgraded = true;
    }
    public void SellTurret()
    {
        if (!isFullyUpgraded)
        {
            PlayerStats.money += turretBluePrint.GetReturnValue();
        }
        if (isFullyUpgraded)
        {
            PlayerStats.money += turretBluePrint.GetUpgradedReturnValue();
            isFullyUpgraded = false;
        }
        GameObject effect = Instantiate(turretBluePrint.sellEffect, GetBuildPos(), Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(turretInNode);
        turretBluePrint = null;

    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.HasMoney)
        {
            _renderer.material.color = hoverColor;
        }
        else
        {
            _renderer.material.color = notEnoughMoneyColor;
        }

    }
    private void OnMouseExit()
    {
        _renderer.material.color = startColor;
    }
}
