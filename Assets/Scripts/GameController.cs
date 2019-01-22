using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject smallAllyTank;
	public GameObject smallEnemyTank;
	public GameObject mediumAllyTank;
	public GameObject mediumEnemyTank;
	public GameObject heavyAllyTank;
	public GameObject heavyEnemyTank;

	public ulong money;
	public uint BasicIncome;

	// Start is called before the first frame update
	void Start()
	{
		InvokeRepeating("IncreaseMoneyByBasicIncome", 0, 5f);
	}

	void IncreaseMoneyByBasicIncome()
	{
		money += BasicIncome;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("1"))
		{
			SpawnSmallTank(true);
		}
		if (Input.GetKeyDown("2"))
		{
			SpawnMediumTank(true);
		}
		if (Input.GetKeyDown("3"))
		{
			SpawnHeavyTank(true);
		}

		//TODO delete before release
		if (Input.GetKeyDown("q"))
		{
			SpawnTank(smallEnemyTank, false);
		}
		if (Input.GetKeyDown("w"))
		{
			SpawnTank(mediumEnemyTank, false);
		}
		if (Input.GetKeyDown("e"))
		{
			SpawnTank(heavyEnemyTank, false);
		}
	}

	public void SpawnSmallTank(bool isAlly) => SpawnTank(smallAllyTank, isAlly);
	public void SpawnMediumTank(bool isAlly) => SpawnTank(mediumAllyTank, isAlly);
	public void SpawnHeavyTank(bool isAlly) => SpawnTank(heavyAllyTank, isAlly);
	public void SpawnLeviathan(bool isAlly) => SpawnTank(smallAllyTank, isAlly); //TODO

	public void SpawnTank(GameObject tank, bool isAlly) =>
		Instantiate(
			tank,
			new Vector3(Random.Range(-9f, 9f),
			isAlly ? Random.Range(-34f, -37.5f) : Random.Range(34f, 37.5f), 0),
			isAlly ? Quaternion.identity : new Quaternion(0, 0, 180, 0)
		);

}
