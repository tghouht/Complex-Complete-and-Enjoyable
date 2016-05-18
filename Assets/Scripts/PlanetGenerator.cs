using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlanetGenerator : NetworkBehaviour {
    public static string seed = "";

    //Universly Controlles Gravity Strengh
    static public float gravityConstant = .005f;
    //Minimum distance to attempt to keep between Planets
    static public float planetBuffer = 5f;

    static public int maxNumofPlanets = 7;

    static public int minNumofPlanets = 3;

    static public float maxSizeofPlanets = 50f;

    static public float minSizeofPlanets = 20f;

    static public float maxDensityofPlanets = 7f;

    static public float minDensityofPlanets = 3f;

    //Rectangle form -world_ to +world_ centered at 0,0,0
    static public float worldX = 50f;

    static public float worldY = 50f;

    static public float worldZ = 50f;

    static public int maxSpawnAttempts = 24;

    [SerializeField]
    private GameObject planetPrefab;

    [SerializeField]
    private GameObject planetParent;

    [SerializeField]
    private GameObject block;

    private List<Transform> planets = new List<Transform>();

    public override void OnStartServer ()
    {
        base.OnStartServer();

        //If no seed generate random one
        seed = System.DateTime.Now.ToString();

        //Variable for replicatable randomness
        System.Random pseudo = new System.Random(seed.GetHashCode());

        //Number of planets to generate
        int numofPlanets = pseudo.Next(minNumofPlanets, maxNumofPlanets + 1);

        for (int i = 0;i < numofPlanets;i++) {
            //Sets variables
            float density = (float) pseudo.NextDouble() * (maxDensityofPlanets - minDensityofPlanets) + minDensityofPlanets;
            float scale = (float) pseudo.NextDouble() * (maxSizeofPlanets - minSizeofPlanets) + minSizeofPlanets;

            Vector3 currentPosition = Vector3.zero;
            int numTries = 0;
            bool foundGoodPlace = true;

            while (numTries < maxSpawnAttempts) {
                numTries++;
                //Generates Random Coordniate to attempt to spawn
                float x = (float) pseudo.NextDouble() * 2f * worldX - worldX;
                float y = (float) pseudo.NextDouble() * 2f * worldY - worldY;
                float z = (float) pseudo.NextDouble() * 2f * worldZ - worldZ;
                currentPosition = new Vector3(x, y, z);

                if (i != 0) {
                    foreach (Transform planet in planets) {
                        if ((currentPosition - planet.position).magnitude < ((planet.GetComponent<PlanetGravity>().scale + scale) / 2 + planetBuffer)) {
                            foundGoodPlace = false;
                            break;
                        }
                    }
                    if (foundGoodPlace)
                        break;
                }
            }

            if (numTries == maxSpawnAttempts && !foundGoodPlace) {
                //print("Planet " + (i + 1) + " failed to place. Did not spawn.");
                continue;
            }

            GameObject planetGO = (GameObject)Instantiate(planetPrefab, currentPosition, transform.rotation);
            GameObject blockGO = (GameObject) Instantiate(block);
            float rand1 = Random.Range(0, 2 * Mathf.PI);
            float rand2 = Random.Range(0, 2 * Mathf.PI);
            blockGO.transform.position = new Vector3(currentPosition.x + (scale - 2f) * Mathf.Sin(rand1) * Mathf.Cos(rand2), currentPosition.y + (scale - 2f) * Mathf.Sin(rand1) * Mathf.Sin(rand2), currentPosition.z + (scale - 2f) * Mathf.Cos(rand1));

            planets.Add(planetGO.transform);
            planetGO.transform.SetParent(transform);

            planetGO.name = "Planet (" + (i + 1) + ")";

            PlanetGravity planetSC = planetGO.GetComponent <PlanetGravity>();
            planetSC.density = density;
            planetSC.scale = scale;

            NetworkServer.Spawn(planetGO);
            NetworkServer.Spawn(blockGO);
        }
    }
}