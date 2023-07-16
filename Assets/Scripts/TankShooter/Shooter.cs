using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    public abstract void Shoot(GameObject tankShell1, float shellForce, float damageDone, float lifeSpan);

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

}