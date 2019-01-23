using System;
using UnityEngine;
using UnityEngine.UI;

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

	public Text moneyText;

	public uint smallPrice;
	public uint mediumPrice;
	public uint heavyPrice;
	public uint leviathanPrice;

	private System.Random rnd;
	private uint aiStep = 0;

	private Action[] spawningMethods;

	// Start is called before the first frame update
	void Start()
	{
		spawningMethods = new Action[]
		{
			() => SpawnTank(smallEnemyTank, false),
			() => SpawnTank(mediumEnemyTank, false),
			() => SpawnTank(heavyEnemyTank, false)
		};
		rnd = new System.Random();
		InvokeRepeating("IncreaseMoneyByBasicIncome", 0, 5f);
		InvokeRepeating("RunEnemyAI", 0, 5f);


	}

	void IncreaseMoneyByBasicIncome()
	{
		money += BasicIncome;
	}
	void RunEnemyAI() //TODO improve AI
	{
		aiStep++;
		double[] probs;
		if (aiStep < 10)
		{
			probs = new double[] { 0.8, 0.2, 0 };
			spawningMethods[SelectFromRoulette(probs, rnd)]();
		}
		else if (aiStep >= 10 && aiStep < 30)
		{
			probs = new double[] { 0.3, 0.6, 0.1 };
			spawningMethods[SelectFromRoulette(probs, rnd)]();
			spawningMethods[SelectFromRoulette(probs, rnd)]();
		}
		else
		{
			probs = new double[] { 0.1, 0.3, 0.6 };
			spawningMethods[SelectFromRoulette(probs, rnd)]();
			spawningMethods[SelectFromRoulette(probs, rnd)]();
			spawningMethods[SelectFromRoulette(probs, rnd)]();
		}
		
	}

	int SelectFromRoulette(double[] weight, System.Random rng)
	{
		double total = 0;
		double amount = rng.NextDouble();
		for (int a = 0; a < weight.Length; a++)
		{
			total += weight[a];
			if (amount <= total)
			{
				return a;
			}
		}
		return -1;
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
		moneyText.text = "$ " + money.ToString();
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

	public void SpawnSmallTank(bool isAlly)
	{
		if (money >= smallPrice)
		{
			SpawnTank(smallAllyTank, isAlly);
			money -= smallPrice;
		}
	}

	public void SpawnMediumTank(bool isAlly)
	{
		if (money >= mediumPrice)
		{
			SpawnTank(mediumAllyTank, isAlly);
			money -= mediumPrice;
		}
	}

	public void SpawnHeavyTank(bool isAlly)
	{
		if (money >= heavyPrice)
		{
			SpawnTank(heavyAllyTank, isAlly);
			money -= heavyPrice;
		}
	}

	public void SpawnLeviathan(bool isAlly) => SpawnTank(smallAllyTank, isAlly); //TODO

	public void SpawnTank(GameObject tank, bool isAlly) =>
		Instantiate(
			tank,
			new Vector3(UnityEngine.Random.Range(-9f, 9f),
			isAlly ? UnityEngine.Random.Range(-34f, -37.5f) : UnityEngine.Random.Range(34f, 37.5f), 0),
			isAlly ? Quaternion.identity : new Quaternion(0, 0, 180, 0)
		);

}
