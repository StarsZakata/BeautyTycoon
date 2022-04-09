using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerControll : MonoBehaviour
{
    [SerializeField] private Transform[] movePointEnemy;
    [SerializeField] private Consumer Prefab;
    public GameObject enemy;

    private bool StartingSpawn = false;
    public void SetStartingSpawn(bool value) {
        StartingSpawn = value;
    }
    /// <summary>
    /// ������ ����������
    /// </summary>
    public void RunSpawnNewConsumer() {
        SpawnNewEnemy();
        StartingSpawn = true;
    }

    /// <summary>
    /// �������� ������� ���������� �� �����
    /// </summary>
    private void Update()
    {
        if (enemy == null && StartingSpawn == true) {
            SpawnNewEnemy();
        }
    }
    /// <summary>
    /// �������� ������ ����������
    /// </summary>
    private void SpawnNewEnemy() {
        Prefab.ByuPoint = movePointEnemy[1];
        Prefab.ByeByePoint = movePointEnemy[2];
        enemy = Instantiate(Prefab, movePointEnemy[0].position, Quaternion.identity).gameObject;
    }
}
