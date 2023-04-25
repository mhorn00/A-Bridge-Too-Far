using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    
    public int lifespan = 10; 

    // Start is called before the first frame update
    void Start() {
        Invoke("DestroySelf", lifespan);    
    }

    void DestroySelf() {
        Destroy(gameObject);
    }


}
