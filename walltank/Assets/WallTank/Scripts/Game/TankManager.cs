using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankManager : SingletonMonoBehavior<TankManager> {

	private List<GameObject> tankObjects;
    public Color Color1 = new Color(0.6f, 0f, 0f, 1f);
    public Color Color2 = new Color(0f, 0f, 0.6f, 1f);
    public Color Color3 = new Color(0.6f, 0.6f, 0f, 1f);
    public Color Color4 = new Color(0f, 0.6f, 0f, 1f);


    // Use this for initialization
    void Start()
	{
		tankObjects = new List<GameObject>();
        
		GameObject playerPrefab = Resources.Load("Prefabs/Tanks/Player") as GameObject;
		GameObject cpu1Prefab = Resources.Load("Prefabs/Tanks/CPU1") as GameObject;
		GameObject cpu2Prefab = Resources.Load("Prefabs/Tanks/CPU2") as GameObject;
		GameObject cpu3Prefab = Resources.Load("Prefabs/Tanks/CPU3") as GameObject;
		GameObject player = Instantiate(playerPrefab) as GameObject;
		GameObject cpu1 = Instantiate(cpu1Prefab) as GameObject;
		GameObject cpu2 = Instantiate(cpu2Prefab) as GameObject;
		GameObject cpu3 = Instantiate(cpu3Prefab) as GameObject;

        /* 色の変更 */
        MeshRenderer[] renderers = player.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color1;

        renderers = cpu1.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color2;

        renderers = cpu2.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color3;

        renderers = cpu3.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color4;

        player.transform.parent = transform;
		cpu1.transform.parent = transform;
		cpu2.transform.parent = transform;
		cpu3.transform.parent = transform;
		tankObjects.Add(player);
		tankObjects.Add(cpu1);
		tankObjects.Add(cpu2);
		tankObjects.Add(cpu3);

        GameObject Yuka = Resources.Load("Prefabs/Tanks/Yuka") as GameObject;
        GameObject fl = Instantiate(Yuka);
        GameObject Jougen = Resources.Load("Prefabs/Tanks/Jougen") as GameObject;
        GameObject jo = Instantiate(Jougen);

        fl.transform.parent = TankManager.I.transform;
        jo.transform.parent = TankManager.I.transform;
        for (int i = 0; i < 4; ++i)
		{
			if (i < (int)GameInfoManager.I.numberOfPlayer) { tankObjects[i].GetComponent<Tank>().controllerType = Tank.ControllerType.Player; }
			else { tankObjects[i].GetComponent<Tank>().controllerType = Tank.ControllerType.CPU; }
			tankObjects[i].GetComponent<Tank>().Init(i + 1);
		}
	}

	// Update is called once per frame
	void Update () {
	}
    public GameObject get1st()
    {
        int maxIndex = 0;
        float maxHp = 0;
        for (int i = 0; i < TankObjects.Count; ++i)
        {
            Tank tank;
            while (tankObjects[i] == null)
            {
                 //tank= TankObjects[i].GetComponent<Tank>();
            }
            tank = TankObjects[i].GetComponent<Tank>();
            if (maxHp < tank.myStatus.ratioHP)
            {
                maxHp = tank.myStatus.ratioHP;
                maxIndex = i;
            }
        }
        return TankObjects[maxIndex];
    }
	public List<GameObject> TankObjects { get { return tankObjects; } set { tankObjects = value; } }
}
