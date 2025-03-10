using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header ("Seettings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    void Start()
    {
        
    }


    void Update()
    {
        Enemies closestEnemy = null;

        Enemies[] enemies = FindObjectsByType<Enemies>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
     
        float minDistance = 5000;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemies enemyChecked = enemies[i];
            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if(distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }

        if(closestEnemy == null)
        {
            transform.up = Vector3.up;
            return;
        }

        transform.up = (closestEnemy.transform.position - transform.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
       Gizmos.DrawWireSphere(transform.position, range);
    }
}
