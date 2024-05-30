using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System;
using UnityEngine.TextCore.Text;
using System.Linq;
using static UnityEditor.PlayerSettings;

public class WorkerMove : MonoBehaviour
{
    Animator animator;

    public float runDuration = 1f;

    public GameObject[] meleeColliders;

    public int activeCollider;

    public TileBase tb;
    public Tilemap tm;

    TileBase[] allTiles;

    Vector3 movePos;

    public Vector3[] tileMovePositions;
    public Vector3[] tempMovePositions = new Vector3[4];

    public enum Direction { North, East, South, West };
    public Direction myDirection;

    public Vector3Int cellDestination;

    public List<float> distToTargetCell;

    int directionIndex;

    void Start()
    {
        //myDirection = Direction.South;
        animator = GetComponent<Animator>();

        animator.SetFloat("hor", 0);
        animator.SetFloat("ver", -1); //tell animator character is facing down at level start

        BoundsInt bounds = tm.cellBounds;
        allTiles = tm.GetTilesBlock(bounds);

    }



    // if not arrived at cell destination
    // check each tilemoveposition
    // determine which tile is closest to celldestination
    // move to tilemoveposition
    // repeat until arrived at cell destination
}
