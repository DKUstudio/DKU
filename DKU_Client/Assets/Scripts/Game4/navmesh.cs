using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class navmesh : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public bool isMoving=false;

    public float loc_x;
    public float loc_z;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine( MoveTimer());

    }

    // Update is called once per frame
    void Update()
    {
        
        // if (navAgent.velocity.sqrMagnitude < 0.1f &&navAgent.remainingDistance <= 0.2f )
        // {
        //     MoveRandom();
        // }
    }

    IEnumerator MoveTimer()
    {
        while (true)
        {
            MoveRandom();
            yield return new WaitForSeconds(3f);
        }
    }
    
    void MoveRandom()
    {
        loc_x = Random.Range(-4f,4f);
        loc_z = Random.Range(-4f,4f);
        Vector3 pos = new Vector3(transform.position.x+loc_x, transform.position.y,transform.position.z+loc_z);
        
        NavMeshHit hit;
        NavMesh.SamplePosition(pos, out hit, 3f, NavMesh.AllAreas);

        navAgent.SetDestination(hit.position);
    }
}