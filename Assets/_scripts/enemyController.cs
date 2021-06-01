// //   enemy roamming por : dave game development disponivel em: https://youtu.be/UjkSFoLxesw
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class enemyController : MonoBehaviour {
//     void Start()
//     {
        
//     }

//     void Update()
//     {
        
//     }
    
//     private void patrulha()
//     {
//         if (!walkPointSet) pontosPatrulha();

//         if (walkPointSet)
//             agent.SetDestination(walkPoint);

//         Vector3 distanceToWalkPoint = transform.position - walkPoint;

//         //Walkpoint reached
//         if (distanceToWalkPoint.magnitude < 1f)
//             walkPointSet = false;
//     }
//     private void pontosPatrulha()
//     {
        
//         float randomZ = Random.Range(-walkPointRange, walkPointRange);
//         float randomX = Random.Range(-walkPointRange, walkPointRange);

//         walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

//         if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
//             walkPointSet = true;
//     }
// }
