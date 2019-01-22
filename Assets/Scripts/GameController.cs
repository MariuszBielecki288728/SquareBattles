using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject smallAllyTank;
    public GameObject smallEnemyTank;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEntity();
    }

    void SpawnEntity()
    {
        if (Input.GetKeyDown("1"))
        {
            Instantiate(smallAllyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(-34f, -37.5f), 0), Quaternion.identity);
        }
        if (Input.GetKeyDown("2"))
        {
            Instantiate(smallEnemyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(34f, 37.5f), 0), new Quaternion(0, 0, 180, 0));
        }
    }

	public void SpawnSmallTank()
	{
		Instantiate(smallAllyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(-34f, -37.5f), 0), Quaternion.identity);
	}

	public void SpawnMediumTank()
	{
		Instantiate(smallAllyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(-34f, -37.5f), 0), Quaternion.identity);
	}
	public void SpawnHeavyTank()
	{
		Instantiate(smallAllyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(-34f, -37.5f), 0), Quaternion.identity);
	}
	public void SpawnLeviathan()
	{
		Instantiate(smallAllyTank, new Vector3(Random.Range(-9f, 9f), Random.Range(-34f, -37.5f), 0), Quaternion.identity);
	}

}
