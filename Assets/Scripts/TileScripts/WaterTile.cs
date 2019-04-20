using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTile : TileBase
{
    [SerializeField]
    private Sprite[] waterSprites;

    [SerializeField]
    private Sprite preview;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if (HasWater(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);
        tileData.colliderType = Tile.ColliderType.Sprite;

        string composition = string.Empty;


        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0)
                {
                    if (HasWater(tileMap, new Vector3Int(location.x + x, location.y + y, location.z)))
                    {
                        composition += 'W';
                    }
                    else
                    {
                        composition += 'E';
                    }
                }
            }
        }

        int randomVal = Random.Range(0, 2);

        if (randomVal == 0)
        {
            tileData.sprite = waterSprites[46];
        }
        else
        {
            tileData.sprite = waterSprites[47];
        }


        List<TileAssign> waterBorders = new List<TileAssign>
            {
                // E - earth, W - water
                new TileAssign("1346", "EEEE", 0),
                new TileAssign("1346", "EWEW", 1),
                new TileAssign("13456", "EWEEW", 2),
                new TileAssign("1346", "WWEW", 3),
                new TileAssign("1346", "WWEE", 4),
                new TileAssign("01346", "EWWEE", 5),
                new TileAssign("01346", "EWWEW", 6),
                new TileAssign("1346", "EWWW", 7),
                new TileAssign("13456", "EWWEW", 8),
                new TileAssign("13467", "EWWWE", 9),
                new TileAssign("134567", "EWWEWE", 10),
                new TileAssign("1346", "WWWE", 12),
                new TileAssign("01346", "EWWWE", 11),
                new TileAssign("12346", "WEWWE", 13),
                new TileAssign("13456", "WWEEW", 14),
                new TileAssign("012346", "EWEWWE", 15),
                new TileAssign("013456", "EWWEEW", 16),
                new TileAssign("1346", "EEWW", 17),
                new TileAssign("13467", "EEWWE", 18),
                new TileAssign("1346", "WEWW", 19),
                new TileAssign("13467", "WEWWE", 20),
                new TileAssign("12346", "WEEWW", 21),
                new TileAssign("123467", "WEEWWE", 22),
                new TileAssign("1346", "WEWE", 23),
                new TileAssign("12346", "WEEWE", 24),
                new TileAssign("1346", "WEEE", 25),
                new TileAssign("1346", "EEEW", 26),
                new TileAssign("1346", "WEEW", 27),
                new TileAssign("1346", "EWWE", 28),
                new TileAssign("1346", "EEWE", 29),
                new TileAssign("1346", "EWEE", 30),
                new TileAssign("01234567", "EWWWWEWW", 31),
                new TileAssign("01234567", "EWEWWWWE", 32),
                new TileAssign("01234567", "EWEWWWWW", 33),
                new TileAssign("01234567", "WWWWWEWW", 34),
                new TileAssign("01234567", "WWEWWWWE", 35),
                new TileAssign("01234567", "WWWWWWWE", 36),
                new TileAssign("01234567", "EWWWWWWW", 37),
                new TileAssign("01234567", "WWEWWWWW", 38),
                new TileAssign("01234567", "EWWWWWWE", 39),
                new TileAssign("01234567", "EWWWWEWE", 40),
                new TileAssign("01234567", "WWWWWEWE", 41),
                new TileAssign("01234567", "WWEWWEWW", 42),
                new TileAssign("01234567", "EWEWWEWW", 43),
                new TileAssign("01234567", "WWEWWEWE", 44),
                new TileAssign("01234567", "EWEWWEWE", 45)
            };

        foreach (var t in waterBorders)
        {
            if (t.DoesMatch(composition))
            {
                tileData.sprite = waterSprites[t.Image];
            }
        }
    }

    private bool HasWater(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }


#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WaterTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Watertile", "New Watertile", "asset", "Save watertile", "Assets");
        if (path == "")
        {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WaterTile>(), path);
    }
#endif
}

public struct TileAssign
{
    public string Ids { get; set; }
    public string States { get; set; }
    public int Image { get; set; }

    public TileAssign(string ids, string states, int image)
    {
        this.Ids = ids;
        this.States = states;
        this.Image = image;
    }

    public bool DoesMatch(string composition)
    {
        for (int i = 0; i < Ids.Length; i++)
        {
            if (composition[int.Parse(Ids[i].ToString())] != States[i])
            {
                return false;
            }
        }

        return true;
    }
}