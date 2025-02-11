using System.Collections;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public Transform[] carSpawnPoints;
    public GameObject[] carPrefabs;
    public GameObject cars;
    private float carDelay = 0.35f;
    void Start()
    {
        StartCoroutine(SpawnCar());
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator SpawnCar()
    {
        while (true) {
            Instantiate(carPrefabs[Random.Range(0, 4)], carSpawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity, cars.transform);
            yield return new WaitForSeconds(carDelay);
        }
    }
}
