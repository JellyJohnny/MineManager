using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rubble : MonoBehaviour
{
    public GameObject rubb;
    int numberRubble;

    private void Start()
    {
        numberRubble = Random.Range(10, 15);
        for (int i = 0; i < numberRubble; i++)
        {
            StartCoroutine(SpawnRubble());
        }
        
    }

    IEnumerator SpawnRubble()
    {
        GameObject r = Instantiate(rubb, transform.position, Quaternion.identity);
        float randScale = Random.Range(-0.05f, 0.05f);
        r.transform.localScale += new Vector3(randScale, randScale, 0);
        r.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * Random.Range(300f,500f));
        //r.transform.DOJump(transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0),
            //Random.Range(-2f, 2f), 1, Random.Range(0.5f, 1f));
        yield return new WaitForSeconds(2f);
        r.GetComponent<SpriteRenderer>().DOFade(0f, 1f);


        yield return new WaitForSeconds(3f);
        Destroy(r.gameObject);
        Destroy(this.gameObject);
    }

}
