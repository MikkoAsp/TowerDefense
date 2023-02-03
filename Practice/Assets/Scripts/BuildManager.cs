using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public bool isGameOver;
    private void Awake()
    {
        if(instance != null)
        {
            print("One or more buildmanagers already!");
            return;
        }
        instance = this;
    }
    //THIS HAS TO BE PRIVATE DONT CHANGE
    private TurretBluePrint turretTobuild;
    private Node selectedNode;
    private NodeUI nodeUI;
    private void Start()
    {
        nodeUI = FindObjectOfType<NodeUI>();
    }
    //Returns false if there is not a selected turret
    public bool CanBuild { get {return turretTobuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= turretTobuild.cost; } }
    //Actual turret instantiation and the filling of the nodes slot

    private void Update()
    {
        isGameOver = PlayerStats.gameOver;
    }
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretTobuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    //Selecting the desired turret
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretTobuild = turret;
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretTobuild;
    }

}
