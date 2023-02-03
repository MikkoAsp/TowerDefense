using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI sellValueText;
    public GameObject nodeUI;
    private Node target;
    public Button upgradeButton;
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPos();
        UpdateUI();
        nodeUI.SetActive(true);
    }
    public void Hide()
    {
        nodeUI.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
    void UpdateUI()
    {
        if (!target.isFullyUpgraded)
        {
            upgradeCostText.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        if (target.isFullyUpgraded)
        {
            upgradeCostText.text = "Done";
            upgradeButton.interactable = false;
        }
        if (!target.isFullyUpgraded)
        {
            sellValueText.text = "$" + target.turretBluePrint.GetReturnValue();
        }
        if (target.isFullyUpgraded)
        {
            sellValueText.text = "$" + target.turretBluePrint.GetUpgradedReturnValue();
        }

    }
}
