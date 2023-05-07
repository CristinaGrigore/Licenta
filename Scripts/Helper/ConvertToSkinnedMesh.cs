using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConvertToSkinnedMesh : MonoBehaviour
{
	[ContextMenu("Convert to Skinned Mesh")]
	void Convert()
	{
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshFilter filter = GetComponent<MeshFilter>();
		SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();

		skinnedMeshRenderer.sharedMesh = filter.sharedMesh;
		skinnedMeshRenderer.sharedMaterials = meshRenderer.sharedMaterials;

		//destroy mesh renderer
		DestroyImmediate(meshRenderer);
		DestroyImmediate(this);
	}

}
