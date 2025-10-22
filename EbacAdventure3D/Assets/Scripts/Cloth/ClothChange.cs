using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;
public class ClothChange : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public Texture2D texture;

    public Texture2D defaultTexture;

    public string shaderIdName = "_EmissionMap";
    

    private void Start()
    {
        ApplyDefaultTexture();
    }

    
    public void ChangeTexture(Texture texture)
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
        SaveManager.instance.Setup.texture = texture;
        
    }

    public void ApplyDefaultTexture()
    {
        if (SaveManager.instance.Setup.texture != null)
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, SaveManager.instance.Setup.texture);
        }
        
        //mesh.sharedMaterials[0].SetTexture(shaderIdName, defaultTexture);
        
    }

    public void ChangeTexture(ClothSetup setup)
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, setup.texture);
    }
    
}
