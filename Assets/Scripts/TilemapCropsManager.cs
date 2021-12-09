using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    public TileBase plowed;
    public TileBase seeded;

    public Tilemap cropTilemap;

    public GameObject cropsSpritePrefab;

    public CropsContainer cropsContainer;

    void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        cropTilemap = GetComponent<Tilemap>();
        Init();
        onTimeTick += Tick;
        VisualizeMap();
    }

    void VisualizeMap()
    {
        for (int i = 0; i < cropsContainer.crops.Count; i++)
        {
            VisualizeTile(cropsContainer.crops[i]);
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < cropsContainer.crops.Count; i++)
        {
            cropsContainer.crops[i].spriteRenderer = null;
        }
    }

    public void Tick()
    {
        if (cropTilemap == null) { return; }

        foreach (CropTile cropTile in cropsContainer.crops)
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
                    cropTilemap.SetTile(cropTile.position, plowed);
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

    internal bool Check(Vector3Int position)
    {
        return cropsContainer.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        if (Check(position)) { return; }
        CreatedPlowedTile(position);
    }

    private void CreatedPlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        crop.position = position;
        cropsContainer.Add(crop);

        VisualizeTile(crop);
        cropTilemap.SetTile(position, plowed);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = cropsContainer.Get(position);
        if (tile != null)
        {
            cropTilemap.SetTile(position, seeded);
            tile.crop = toSeed;
        }
    }

    public void PickUp(Vector3Int gridPosition)
    {
        CropTile tile = cropsContainer.Get(gridPosition);
        if (tile != null && tile.completed)
        {
            ItemSpawnManager.instance.SpawnItem(cropTilemap.GetCellCenterLocal(gridPosition), tile.crop.yield, tile.crop.quantity);

            tile.Harvested();
            VisualizeTile(tile);
        }
    }

    public void VisualizeTile(CropTile cropTile)
    {
        cropTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

        if (cropTile.spriteRenderer == null)
        {
            GameObject go = Instantiate(cropsSpritePrefab, transform);
            go.transform.position = cropTilemap.GetCellCenterLocal(cropTile.position);
            go.transform.position -= Vector3.forward * 0.01f;
            cropTile.spriteRenderer = go.GetComponent<SpriteRenderer>();
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];

        cropTile.spriteRenderer.gameObject.SetActive(growing);
        if (growing)
        {
            cropTile.spriteRenderer.sprite = cropTile.crop.sprites[cropTile.growthStage - 1];
        }
    }
}
