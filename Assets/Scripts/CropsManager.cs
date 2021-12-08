using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
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

public class CropsManager : MonoBehaviour
{
    public TilemapCropsManager cropsManager;

    void ValidateCropsManagerInitialized()
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager");
        }
    }
    public void PickUp(Vector3Int position)
    {
        ValidateCropsManagerInitialized();
        cropsManager?.PickUp(position);
    }

    public bool Check(Vector3Int position)
    {
        ValidateCropsManagerInitialized();
        return cropsManager?.Check(position) ?? false;
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        ValidateCropsManagerInitialized();
        cropsManager?.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        ValidateCropsManagerInitialized();
        cropsManager?.Plow(position);
    }
}
