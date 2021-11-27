using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop
{

}

public class CropsManager : MonoBehaviour
{
    public TileBase plowed;
    public TileBase seeded;
    public Tilemap cropTilemap;

    Dictionary<Vector3Int, Crop> crops;

    void Start()
    {
        crops = new Dictionary<Vector3Int, Crop>();
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey(position);
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey(position))
        {
            //crop already present
            return;
        }

        CreatedPlowedTile(position);
    }

    private void CreatedPlowedTile(Vector3Int position)
    {
        Crop crop = new Crop();
        crops.Add(position, crop);

        cropTilemap.SetTile(position, plowed);
    }

    public void Seed(Vector3Int position)
    {
        cropTilemap.SetTile(position, seeded);
    }
}
