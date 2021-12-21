using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    /*create new ground 
     * set destroy time and
     * set position for new ground
     */
    //GAME DESIGN
    [SerializeField] private float minCreatingDelay = 5f;
    [SerializeField] private float maxCreatingDelay = 20f;
    [SerializeField] private float lifeTimeExcludingCreatingDelayAddition = 7f;
    [SerializeField] private float minEnemyNumberForOneGround = 2f;
    [SerializeField] private float maxEnemyNumberForOneGround = 8f;

    [SerializeField] private GameObject groundGameObject;
    [SerializeField] GameObject enemy;
    [SerializeField] Transform enemyHolder;
    [SerializeField] Transform groundHolder;

    public static Vector3 lastGroundPosition;
    private Vector3 newGroundPosition;

    
    IEnumerator Start()
    {
        foreach(Transform child in groundHolder.transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(5);

        StartCoroutine(CreateNewGround());
    }

    IEnumerator CreateNewGround()
    {
        float growthDirection = Random.Range(0, 2);
        if (growthDirection == 0)
        {
             newGroundPosition = lastGroundPosition + new Vector3(10, 0, 0);
        }
        else
        {
             newGroundPosition = lastGroundPosition + new Vector3(0, 0, 10);
        }
        float creationDelay = Random.Range(minCreatingDelay, maxCreatingDelay);
        GameObject newGroundObject = Instantiate(groundGameObject, newGroundPosition, Quaternion.identity, groundHolder);
        newGroundObject.GetComponent<Ground>().LifeTime = creationDelay + lifeTimeExcludingCreatingDelayAddition;
       
        SpawnEnemiesToNewGround(newGroundObject);

        yield return new WaitForSeconds(creationDelay);

        StartCoroutine(CreateNewGround());
    }

    private void SpawnEnemiesToNewGround(GameObject newGround)
    {
        float totalEnemyCount = Random.Range(minEnemyNumberForOneGround, maxEnemyNumberForOneGround);
        for (int i=0;i< totalEnemyCount; i++)
        {
            Vector3 firstPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(-4.5f, 4.5f));
            Instantiate(enemy, newGround.transform.position + firstPosition, transform.rotation, enemyHolder);
        }
    }
}
