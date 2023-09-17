using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractObject : MonoBehaviour, Interactable
{
    public List<GameObject> uis;
    public Vector4 _color;
    public Vector4 color
    {
        get { return _color; }
        set 
        {
            stencilMaterial.SetVector("_Color", _color);
            _color = value;
        }
    }
    Material stencilMaterial;
    bool _isSelected = false;
    bool isSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            if (_isSelected) SetSelect();
            else UnsetSelect();
        }
    }

    void SetSelect()
    {
        CameraController.instance.followTarget = transform.position;
        OutlineEffect.renderEvent += OnRender;
        foreach(GameObject ui in uis) ui.SetActive(true);
    }

    void UnsetSelect()
    {
        OutlineEffect.renderEvent -= OnRender;
        foreach (GameObject ui in uis) ui.SetActive(false);
    }

    void OnRender(CommandBuffer commandBuffer)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers) commandBuffer.DrawRenderer(renderer, stencilMaterial);
    }

    void Start()
    {
        stencilMaterial = new Material(Shader.Find("Custom/Outline/Stencil"));
        stencilMaterial.SetVector("_Color", _color);
        Canvas[] canvases = GetComponentsInChildren<Canvas>(true);
        foreach(Canvas canvas in canvases)
        {
            if (!uis.Contains(canvas.gameObject)) uis.Add(canvas.gameObject);
        }
    }

    void Update()
    {
        
    }

    public void Hit()
    {
        isSelected = !isSelected;
    }
}
