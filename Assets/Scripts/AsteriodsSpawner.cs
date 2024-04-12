using UnityEngine;

public class AsteriodsSpawner : MonoBehaviour
{

    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;

    public Asteriods asteriodPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteriods asteriods = Instantiate(this.asteriodPrefab, spawnPoint, rotation);
            asteriods.size = Random.Range(asteriods.minSize, asteriods.maxSize);

            asteriods.setTrajectory(rotation * -spawnDirection);
        }
    }
}
