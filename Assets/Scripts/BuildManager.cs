using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    
    public GameObject getTurretToBuild() {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret) {
        turretToBuild = turret;
    }
}
