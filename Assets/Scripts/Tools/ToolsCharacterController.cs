using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    public float offsetDistance = 1f;
    public float sizeOfInteractableArea = 1.2f;
    public TilemapMarkerManager markerManager;
    public TileMapReadController tileMapReadController;
    public float maxDistance = 2.5f;
    public CropsManager cropsManager;
    public TileData plowableTile;
    public ToolbarController toolbarController;

    Character4D character;
    Rigidbody2D body2d;
    Vector3Int selectedTilePosition;
    bool selectable;

    void Awake()
    {
        character = GetComponent<Character4D>();
        body2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
    }

    void Update()
    {
        SelectTile();
        CanSelectTileCheck();
        MarkTile();
        if (Input.GetMouseButtonDown(0))
        {
            if (!UseToolWorld())
            {
                UseToolGrid();
            }
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void CanSelectTileCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void MarkTile()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = body2d.position + character.Direction * offsetDistance;
        GameItem item = toolbarController.GetItem;
        if (item == null || item.onAction == null)
        {
            return false;
        }

        character.AnimationManager.Slash2H();
        return item.onAction.OnApply(position);
    }

    private void UseToolGrid()
    {
        if (selectable)
        {
            TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            if (tileBase != null)
            {
                TileData tileData = tileMapReadController.GetTileData(tileBase);
                if (tileData != plowableTile) { return; }
                if (cropsManager.Check(selectedTilePosition))
                {
                    cropsManager.Seed(selectedTilePosition);
                }
                else
                {
                    cropsManager.Plow(selectedTilePosition);
                }
            }
        }
    }
}