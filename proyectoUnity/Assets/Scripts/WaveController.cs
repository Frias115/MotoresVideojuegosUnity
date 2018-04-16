using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public GameObject[] wavePrefabs;
    public int numberOfWaves = 5;
    public float timeBetweenWaves = 2.5f;
    public int numberOfWavesAtTheSameTime = 1;

    private float nextWaveTime = 0.0f;
    private int _numberOfWaves;

    // Use this for initialization
    void Start() {
        _numberOfWaves = numberOfWaves;
    }

    // Update is called once per frame
    void Update () {
        if(_numberOfWaves > 0) {
            if (nextWaveTime > timeBetweenWaves)
            {
                for (int i = 0; i < numberOfWavesAtTheSameTime; i++)
                {
                    int rand = Random.Range(0, wavePrefabs.Length);
                    Instantiate(wavePrefabs[rand]);
                    _numberOfWaves--;
                }
                nextWaveTime = 0;
            }

            nextWaveTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("Nivel completado");
        }
    }
}
