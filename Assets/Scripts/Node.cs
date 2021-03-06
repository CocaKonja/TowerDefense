﻿using UnityEngine;
using UnityEngine.EventSystems

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (buildManager.getTurretToBuild() == null) {
            return;
        }

        if(turret != null) {
            Debug.Log("Cannot Build There - TODO: Display on Screen");
            return;
        }

        GameObject turretToBuild = buildManager.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if (buildManager.getTurretToBuild() == null) {
            return;
        }
        rend.material.color = hoverColor;
    }
    private void OnMouseExit() {
        rend.material.color = startColor;
    }
}
