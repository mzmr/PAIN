using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite defaultTile;

    private Dictionary<Point, GameObject> waterTiles = new Dictionary<Point, GameObject>();

    [SerializeField]
    private SpriteAtlas waterAtlas;

    private Vector3 WorldStartPos
    {
        get => UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {
        int height = mapData[0].height;
        int width = mapData[0].width;

        for (int i = 0; i < mapData.Length; ++i)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color c = mapData[i].GetPixel(x, y);
                    MapElement newElement = Array.Find(mapElements, e => e.MyColor == c);

                    if (newElement != null)
                    {
                        float xPos = WorldStartPos.x + defaultTile.bounds.size.x * x;
                        float yPos = WorldStartPos.y + defaultTile.bounds.size.y * y;

                        GameObject go = Instantiate(newElement.MyElementPrefab);
                        go.transform.position = new Vector2(xPos, yPos);
                        go.transform.parent = map;

                        if (newElement.MyTileTag == "Water")
                        {
                            waterTiles.Add(new Point(x,y), go);
                        }

                        if (newElement.MyTileTag.Contains("Trees"))
                        {
                            go.GetComponent<SpriteRenderer>().sortingOrder = height*2 - y*2;
                        }
                    }
                }
            }
        }

        CheckWater();
    }

    private void CheckWater()
    {
        foreach (KeyValuePair<Point, GameObject> tile in waterTiles)
        {
            string composition = TileCheck(tile.Key);
            List<TileAssignn> waterBorders = new List<TileAssignn>
            {
                // E - earth, W - water
                new TileAssignn(2457, "EWEW", "water_border_top_left_oblique"),
                new TileAssignn(2457, "WWEW", "water_border_top"),
                new TileAssignn(2457, "WWEE", "water_border_top_right_oblique"),
                new TileAssignn(2457, "EWWW", "water_border_left"),
                new TileAssignn(2457, "WWWE", "water_border_right"),
                new TileAssignn(2457, "EEWW", "water_border_bottom_left_oblique"),
                new TileAssignn(2457, "WEWW", "water_border_bottom"),
                new TileAssignn(2457, "WEWE", "water_border_bottom_right_oblique"),
                new TileAssignn(2457, "WEEE", "water_border_right_top_bottom_oblique"),
                new TileAssignn(2457, "EEEW", "water_border_left_top_bottom_oblique"),
                new TileAssignn(2457, "WEEW", "water_border_top_bottom"),
                new TileAssignn(2457, "EWWE", "water_border_left_right"),
                new TileAssignn(2457, "EEWE", "water_border_bottom_left_right_oblique"),
                new TileAssignn(2457, "EWEE", "water_border_top_left_right_oblique"),
                new TileAssignn(2457, "EEEE", "water_border_all")
            };

            foreach (var t in waterBorders)
            {
                if (composition[t.Ids / 1000 % 10 - 1] == t.States[0] && 
                    composition[t.Ids / 100 % 10 - 1] == t.States[1] && 
                    composition[t.Ids / 10 % 10 - 1] == t.States[2] && 
                    composition[t.Ids % 10 - 1] == t.States[3])
                {
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite(t.Image);
                }
            }

            List<TileAssignn> waterReflexBorders = new List<TileAssignn>
            {
                new TileAssignn(235, "WEW", "water_border_top_left_reflex"),
                new TileAssignn(467, "WEW", "water_border_bottom_right_reflex"),
                new TileAssignn(578, "WWE", "water_border_top_right_reflex"),
                new TileAssignn(124, "EWW", "water_border_bottom_left_reflex")
            };

            foreach (var t in waterReflexBorders)
            {
                if (composition[t.Ids / 100 % 10 - 1] == t.States[0] &&
                    composition[t.Ids / 10 % 10 - 1] == t.States[1] &&
                    composition[t.Ids % 10 - 1] == t.States[2])
                {
                    GameObject go = Instantiate(tile.Value, tile.Value.transform.position, Quaternion.identity, map);
                    go.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite(t.Image);
                    go.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
            }

            if (composition[1] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[6] == 'W')
            {
                int randomChance = UnityEngine.Random.Range(0, 2);

                if (randomChance == 0)
                {
                    tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("water2");
                }
            }
        }
    }

    private string TileCheck(Point currentPoint)
    {
        string composition = string.Empty;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (waterTiles.ContainsKey(new Point(currentPoint.X + x, currentPoint.Y + y)))
                    {
                        composition += "W";
                    }
                    else
                    {
                        composition += "E";
                    }
                }
            }
        }

        return composition;
    }
}

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject elementPrefab;

    public GameObject MyElementPrefab { get => elementPrefab; }

    public Color MyColor { get => color; }

    public string MyTileTag { get => tileTag; }
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public struct TileAssignn
{
    public int Ids { get; set; }
    public string States { get; set; }
    public string Image { get; set; }

    public TileAssignn(int ids, string states, string image)
    {
        this.Ids = ids;
        this.States = states;
        this.Image = image;
    }
}