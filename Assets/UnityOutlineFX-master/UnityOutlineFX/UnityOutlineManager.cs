using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnityOutlineManager : MonoBehaviour
{
    [System.Serializable]
    public class OutlineData
    {
        public Renderer renderer;
    }

    public UnityOutlineFX outlinePostEffect;
    public OutlineData[] outliners;

    private void Start()
    {
        //UpdateRenderers();
    }

    public void UpdateRenderers()
    {
        foreach (var obj in outliners)
        {
            outlinePostEffect.AddRenderers(new List<Renderer>() { obj.renderer });
        }
    }
}

