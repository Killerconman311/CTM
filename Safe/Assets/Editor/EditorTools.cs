using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EditorTools : MonoBehaviour
{
[MenuItem("Light Brigade/Debug/Force Cleanup NavMesh")]
        public static void ForceCleanupNavMesh()
        {
            if (Application.isPlaying)
                return;
 
            NavMesh.RemoveAllNavMeshData();
            Debug.Log("NavMesh data removed");
        }
}
