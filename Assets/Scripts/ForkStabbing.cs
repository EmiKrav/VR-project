using System.Collections.Generic;
using UnityEngine;
public class ForkStabbing: MonoBehaviour
{
    [SerializeField] private List<RayCaster> rayCasters;
    private Vector3 lastPosition;
    private StabManager stabManager;

    void Start()
    {
        stabManager = new StabManager(gameObject, rayCasters);
    }

    void Update()
    {
        stabManager.Update();

        if (ForkIsGoingBack())
        {
            stabManager.StickObjects();
        }
        
        lastPosition = transform.position;
    }

    
    bool ForkIsGoingBack()
    {
        Vector3 direction = Vector3.Normalize(transform.position - lastPosition);
        Vector3 back = -transform.right;

        return Vector3.Dot(direction, back) > 0.7;
    }

    public void ProcessForkedCollision()
    {
        // FreeAxis(); // Kolkas neveikia kaip turėtų, kolkas tiks be šito... :(
    }
}