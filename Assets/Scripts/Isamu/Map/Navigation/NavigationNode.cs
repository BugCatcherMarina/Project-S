using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode:MonoBehaviour
{
    private bool IsBlocked = false;
    private Dictionary<NavigationNode, int> Links = new Dictionary<NavigationNode, int>();

    void SetVisible(bool isVisible = true) {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)  renderer.enabled = isVisible; 
    }

    // Start is called before the first frame update
    void Start()
    {
 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
