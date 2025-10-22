using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = 0.1f;

    private Color defaultColor;

    private Tween _currTween;

    public string colorParameter = "_EmissionColor";
    private void OnValidate()
    {
        if(meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        if(skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }
    //private void Start()
    //{
    //    defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    //}
    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (meshRenderer != null && !_currTween.IsActive())
        {
            meshRenderer.material.DOColor(Color.red, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
        }

        if(skinnedMeshRenderer != null && !_currTween.IsActive())
        {
            _currTween = skinnedMeshRenderer.material.DOColor(color,colorParameter,duration).SetLoops(2,LoopType.Yoyo);
        }   
        
    }
}
