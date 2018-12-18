using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBLib
{
    public class Terminator : MonoBehaviour
    {

        public float TimeToDeath;

        // Use this for initialization
        void Start() { 
            GameObject.Destroy(gameObject, TimeToDeath);
        }
    }
}

