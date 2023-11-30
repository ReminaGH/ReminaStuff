using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    

    [SerializeField] private SkinnedMeshRenderer headMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer bodyMeshRenderer;
    

    private Material material;


    private void Awake() {
        //material = new Material(headMeshRenderer.material);
        //headMeshRenderer.material = material;
        //bodyMeshRenderer.material = material;
    }
    

    public void SetPlayerColor(Color color) { 
        material.color = color;
    }

}
