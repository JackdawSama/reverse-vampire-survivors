using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;

    [Header("Projectile One")]
    [SerializeField] private GameObject projectileOnePrefab;
    [SerializeField] private int projectileOnePoolSize = 150;
    [SerializeField] Transform projectileOneParent;
    private Queue<GameObject> projectileOnePool = new Queue<GameObject>();

    [Header("Projectile Two")]
    [SerializeField] private GameObject projectileTwoPrefab;
    [SerializeField] private int projectileTwoPoolSize = 150;
    [SerializeField] Transform projectileTwoParent;
    private Queue<GameObject> projectileTwoPool = new Queue<GameObject>();

    [Header("Projectile Three")]
    [SerializeField] private GameObject projectileThreePrefab;
    [SerializeField] private int projectileThreePoolSize = 150;
    [SerializeField] Transform projectileThreeParent;
    private Queue<GameObject> projectileThreePool = new Queue<GameObject>();

    [Header("Projectile Four")]
    [SerializeField] private GameObject projectileFourPrefab;
    [SerializeField] private int projectileFourPoolSize = 150;
    [SerializeField] Transform projectileFourParent;
    private Queue<GameObject> projectileFourPool = new Queue<GameObject>();

    [Header("Projectile Five")]
    [SerializeField] private GameObject projectileFivePrefab;
    [SerializeField] private int projectileFivePoolSize = 150;
    [SerializeField] Transform projectileFiveParent;
    private Queue<GameObject> projectileFivePool = new Queue<GameObject>();

    [Header("Projectile Six")]
    [SerializeField] private GameObject projectileSixPrefab;
    [SerializeField] private int projectileSixPoolSize = 150;
    [SerializeField] Transform projectileSixParent;
    private Queue<GameObject> projectileSixPool = new Queue<GameObject>();

    [Header("Projectile Seven")]
    [SerializeField] private GameObject projectileSevenPrefab;
    [SerializeField] private int projectileSevenPoolSize = 150;
    [SerializeField] Transform projectileSevenParent;
    private Queue<GameObject> projectileSevenPool = new Queue<GameObject>();

    [Header("Projectile Eight")]
    [SerializeField] private GameObject projectileEightPrefab;
    [SerializeField] private int projectileEightPoolSize = 150;
    [SerializeField] Transform projectileEightParent;
    private Queue<GameObject> projectileEightPool = new Queue<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        GenerateProjectilePool(projectileOnePrefab, projectileOnePool, projectileOnePoolSize, projectileOneParent);
        GenerateProjectilePool(projectileTwoPrefab, projectileTwoPool, projectileTwoPoolSize, projectileTwoParent);
        GenerateProjectilePool(projectileThreePrefab, projectileThreePool, projectileThreePoolSize, projectileThreeParent);
        GenerateProjectilePool(projectileFourPrefab, projectileFourPool, projectileFourPoolSize, projectileFourParent);
        GenerateProjectilePool(projectileFivePrefab, projectileFivePool, projectileFivePoolSize, projectileFiveParent);
        GenerateProjectilePool(projectileSixPrefab, projectileSixPool, projectileSixPoolSize, projectileSixParent);
        GenerateProjectilePool(projectileSevenPrefab, projectileSevenPool, projectileSevenPoolSize, projectileSevenParent);
        GenerateProjectilePool(projectileEightPrefab, projectileEightPool, projectileEightPoolSize, projectileEightParent);
    }

    public GameObject GetProjectile(int number)
    {
        switch(number)
        {
            case 1:
                if(projectileOnePool.Count > 0)
                {
                    GameObject projectile = projectileOnePool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileOnePrefab, projectileOneParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 2:
                if(projectileTwoPool.Count > 0)
                {
                    GameObject projectile = projectileTwoPool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileTwoPrefab, projectileTwoParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 3:
                if(projectileThreePool.Count > 0)
                {
                    GameObject projectile = projectileThreePool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileThreePrefab, projectileThreeParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 4:
                if(projectileFourPool.Count > 0)
                {
                    GameObject projectile = projectileFourPool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileFourPrefab, projectileFourParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 5:
                if(projectileFivePool.Count > 0)
                {
                    GameObject projectile = projectileFivePool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileFivePrefab, projectileFiveParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 6:
                if(projectileSixPool.Count > 0)
                {
                    GameObject projectile = projectileSixPool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileSixPrefab, projectileSixParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 7:
                if(projectileSevenPool.Count > 0)
                {
                    GameObject projectile = projectileSevenPool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileSevenPrefab, projectileSevenParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            case 8:
                if(projectileEightPool.Count > 0)
                {
                    GameObject projectile = projectileEightPool.Dequeue();
                    projectile.SetActive(true);
                    return projectile;
                }
                else
                {
                    GameObject projectile = Instantiate(projectileEightPrefab, projectileEightParent.transform);
                    projectile.SetActive(true);
                    return projectile;
                }
            default:
                return null;
        }
    }

    public void ReturnProjectile(GameObject projectile, int number)
    {
        projectile.SetActive(false);
        switch(number)
        {
            case 1:
                projectileOnePool.Enqueue(projectile);
                break;
            case 2:
                projectileTwoPool.Enqueue(projectile);
                break;
            case 3:
                projectileThreePool.Enqueue(projectile);
                break;
            case 4:
                projectileFourPool.Enqueue(projectile);
                break;
            case 5:
                projectileFivePool.Enqueue(projectile);
                break;
            case 6:
                projectileSixPool.Enqueue(projectile);
                break;
            case 7:
                projectileSevenPool.Enqueue(projectile);
                break;
            case 8:
                projectileEightPool.Enqueue(projectile);
                break;
            default:
                break;
        }
    }

    private void GenerateProjectilePool(GameObject prefab, Queue<GameObject> Pool, int poolSize, Transform parent)
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(prefab, parent.transform);
            Pool.Enqueue(projectile);
            projectile.SetActive(false);
        }
    }
}
