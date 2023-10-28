using UnityEngine;

namespace SkyboxBlenderSpace
{
    public class SpacebarClick : MonoBehaviour
    {
        public SkyboxBlender skyboxScript;
        bool isStopped;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                skyboxScript.Blend();
                isStopped = false;
            }

            // stop blending
            if (Input.GetKeyDown(KeyCode.E)) {
                if (isStopped) {
                    skyboxScript.Resume();
                    isStopped = false;
                }
                else {
                    skyboxScript.Stop();
                    isStopped = true;
                }
            }
        }
    }
}
