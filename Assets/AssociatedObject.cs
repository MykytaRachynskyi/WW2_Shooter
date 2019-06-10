using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

[System.Serializable]
public class OnHold : UnityEvent<float> { }

public class AssociatedObject : MonoBehaviour
{
    public GameObject associatedObject;
    public Vector3 colliderSize;

    private void Start()
    {/*
        if (associatedObject == null)
            return;

        Texture2D tex;
        if (associatedObject != null)
        {
            tex = AssetPreview.GetAssetPreview(associatedObject);
            GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

            byte[] bytes = tex.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/../" + associatedObject.name + ".png", bytes);
        }*/
    }
}
