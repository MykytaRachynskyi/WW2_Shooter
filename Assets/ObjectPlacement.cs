using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllyTypes { Sniper = 0, Gunner = 1, Rifleman = 2 }

public class AllyData
{
    public GameObject prefab { get; set; }
    public int avaliableCount { get; set; }
    public AllyData(GameObject _prefab, int _count)
    {
        prefab = _prefab;
        avaliableCount = _count;
    }
}

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] GameObject sniperObject;
    [SerializeField] GameObject gunnerObject;
    [SerializeField] GameObject riflemanObject;
    [SerializeField] GameObject bombObject;
    [SerializeField] float scaleMultiplier;
    [SerializeField] Transform entranceArrow;
    [SerializeField] Menu allyMenu;
    [SerializeField] Menu bombMenu;

    public int availableSnipers;
    public int availableGunners;
    public int availableRiflemen;
    public int availableBombs;

    public Dictionary<int, AllyData> allies { get; private set; }

    public void Start()
    {
        allies = new Dictionary<int, AllyData>
        { { 0, new AllyData(sniperObject, availableSnipers) },
            { 1, new AllyData(gunnerObject, availableGunners) },
            { 2, new AllyData(riflemanObject, availableRiflemen) }
        };
    }

    public void PlaceAlly(int index)
    {
        AllyData data;
        allies.TryGetValue(index, out data);
        if (data.avaliableCount > 0)
        {
            GameObject go = Instantiate(data.prefab,
                allyMenu.currentTile.transform.position,
                Quaternion.identity);
            go.transform.localScale = Vector3.one * scaleMultiplier;
            go.transform.LookAt(entranceArrow);
            go.transform.eulerAngles = new Vector3(0f, go.transform.eulerAngles.y, 0f);
            allyMenu.currentTile.GetComponent<BoxCollider>().enabled = false;
            allyMenu.currentTile.GetComponent<MeshRenderer>().material.mainTexture = null;
            allyMenu.currentTile.GetComponent<sGridTile>().tileContent = TileContent.Ally;

            data.avaliableCount--;
        }
    }

    public void PlaceBomb()
    {
        if (availableBombs > 0)
        {
            GameObject go = Instantiate(bombObject,
                bombMenu.currentTile.transform.position,
                Quaternion.identity);
            go.transform.localScale = Vector3.one * scaleMultiplier;
            go.transform.LookAt(Vector3.zero);
            go.transform.eulerAngles = new Vector3(0f, go.transform.eulerAngles.y, 0f);
            bombMenu.currentTile.GetComponent<BoxCollider>().enabled = false;
            bombMenu.currentTile.GetComponent<MeshRenderer>().material.mainTexture = null;
            bombMenu.currentTile.GetComponent<sGridTile>().tileContent = TileContent.Bomb;

            availableBombs--;
        }
    }
}