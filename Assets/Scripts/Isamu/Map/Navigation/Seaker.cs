using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isamu.Map.Navigation;

public class Seaker : MonoBehaviour
{



    NavigationNode GetCurrentNode() 
    {
        NavigationNode node = null;

        Vector3 start = Vector3.zero;
        Vector3 direction = Vector3.down;
        RaycastHit hit;
        Physics.Raycast(start, direction, out hit);

        NavigationNode n = hit.collider.gameObject.GetComponent<NavigationNode>();
        if (n != null)
        {
            node = n;
        }
        else 
        {
            Debug.Log("I'm hanging in the air");
        }

        return node;
    
    }

}
