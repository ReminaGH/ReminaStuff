using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    public AssetsSO Assets;

    [SerializeField] private MeshRenderer headMeshRenderer;
    [SerializeField] private MeshRenderer bodyMeshRenderer;
    

    private Material material;


    private void Awake() {
        material = new Material(headMeshRenderer.material);
        headMeshRenderer.material = material;
        bodyMeshRenderer.material = material;
    }
    

    public void SetPlayerColor(Color color) { 
        material.color = color;
    }

}
