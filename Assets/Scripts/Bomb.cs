using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float explodeSpeed;
    public float explodeScale;
    public GameObject explodeObject;
    public GameObject rubble;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer highlight;
    public SpriteRenderer shadow;
    public Tilemap tm;
    public TileBase tb;
    public Vector3[] explodeCells;
    public int flashes;
    public float flashDuration;

    private void Start()
    {
        
        tm = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
    }

    public void Boom()
    {
        DOTween.To(() => flashDuration, x => flashDuration = x, 0f, 2f);
        StartCoroutine(Explode());
    }


    IEnumerator Explode()
    {
        for (int i = 0; i < flashes; i++)
        {
            yield return new WaitForSeconds(flashDuration);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            GetComponent<SpriteRenderer>().color = Color.black;
        }

        explodeObject.transform.DOScale(explodeScale, explodeSpeed);

        Camera.main.DOShakePosition(explodeSpeed, 0.5f, 2, 90f);

        //add tiles in explosion radius 
        for (int i = 0; i < explodeCells.Length; i++)
        {
            Vector3Int p = tm.WorldToCell(transform.position + explodeCells[i]);
            if (!tm.GetTile(p))
            {
                tm.SetTile(p, tb);
                //Instantiate(rubble, transform.position + explodeCells[i],Quaternion.identity);
            }

            GameObject[] destructables=  GameObject.FindGameObjectsWithTag("Destructible");

            foreach (var d in destructables)
            {
                float distance = Vector2.Distance(transform.position, d.transform.position);
                if(distance <= 1)
                {
                    Destroy(d.gameObject);

                    if(d.GetComponent<Mushroom>())
                    {
                        Destroy(d.GetComponent<Mushroom>().shadow);
                    }
                }
            }
        }

        GetComponent<SpriteRenderer>().enabled = false;
        highlight.enabled = false;
        shadow.enabled = false;
        
        spriteRenderer.DOFade(0,explodeSpeed);
        yield return new WaitForSeconds(explodeSpeed);
        Destroy(this.gameObject);
    }

    public IEnumerator MoveToGround()
    {
        transform.DOMove(new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y - 0.5f,0f),0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.parent = null;
        Boom();
    }
}
