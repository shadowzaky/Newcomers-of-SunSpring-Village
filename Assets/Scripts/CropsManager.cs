using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growthStage;
    public Crop crop;
    public SpriteRenderer spriteRenderer;
}

public class CropsManager : TimeAgent
{
    public TileBase plowed;
    public TileBase seeded;
    public Tilemap cropTilemap;
    public GameObject cropsSpritePrefab;

    Dictionary<Vector3Int, CropTile> crops;

    void Start()
    {
        Init();
        crops = new Dictionary<Vector3Int, CropTile>();
        onTimeTick += Tick;
    }

    public void Tick()
    {
        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop != null)
            {
                cropTile.growTimer += 1;
                if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growthStage])
                {
                    cropTile.spriteRenderer.gameObject.SetActive(true);
                    cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growthStage];
                    cropTile.growthStage++;
                }

                if (cropTile.growTimer >= cropTile.crop.timeToGrow)
                {
                    Debug.Log("I'm done growing");
                    cropTile.crop = null;
                }
            }
        }
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
        CropTile crop = new CropTile();
        crops.Add(position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = cropTilemap.GetCellCenterLocal(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();

        cropTilemap.SetTile(position, plowed);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        cropTilemap.SetTile(position, seeded);

        crops[position].crop = toSeed;
    }
}
