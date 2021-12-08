using System;
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
    public Vector3Int position;
    public float damage;
    public bool completed
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growthStage = 0;
        damage = 0;
        crop = null;
        spriteRenderer.gameObject.SetActive(false);
    }
}

public class CropsManager : TimeAgent
{
    public TileBase plowed;
    public TileBase seeded;
    public Tilemap cropTilemap;
    Tilemap CropTilemap
    {
        get
        {
            if (cropTilemap == null)
            {
                cropTilemap = GameObject.Find("Tilemap_Crops")?.GetComponent<Tilemap>();
            }
            return cropTilemap;
        }
    }
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
        if (CropTilemap == null) { return; }

        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop != null)
            {
                cropTile.damage += 0.02f;
                if (cropTile.damage > 1f)
                {
                    cropTile.Harvested();
                }
                else if (!cropTile.completed)
                {
                    cropTile.growTimer += 1;
                    CropTilemap.SetTile(cropTile.position, plowed);
                    if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growthStage])
                    {
                        cropTile.spriteRenderer.gameObject.SetActive(true);
                        cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growthStage];
                        cropTile.growthStage++;
                    }
                }
            }
        }
    }

    public bool Check(Vector3Int position)
    {
        if (CropTilemap == null) { return false; }
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
        if (CropTilemap == null) { return; }
        CropTile crop = new CropTile();
        crop.position = position;
        crops.Add(position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = CropTilemap.GetCellCenterLocal(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.spriteRenderer = go.GetComponent<SpriteRenderer>();

        CropTilemap.SetTile(position, plowed);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (CropTilemap == null) { return; }
        CropTilemap.SetTile(position, seeded);

        crops[position].crop = toSeed;
    }

    public void PickUp(Vector3Int gridPosition)
    {
        if (crops.ContainsKey(gridPosition) == false || CropTilemap == null) { return; }

        CropTile cropTile = crops[gridPosition];
        if (cropTile.completed)
        {
            ItemSpawnManager.instance.SpawnItem(CropTilemap.GetCellCenterLocal(gridPosition), cropTile.crop.yield, cropTile.crop.quantity);

            cropTile.Harvested();
        }
    }
}
