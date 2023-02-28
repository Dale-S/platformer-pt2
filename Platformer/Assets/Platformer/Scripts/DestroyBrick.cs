using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.Scripts
{
    public class DestroyBrick : MonoBehaviour
    {
        void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision Detected");
            DestroyGameObject();
            throw new NotImplementedException();
        }
    }
}
