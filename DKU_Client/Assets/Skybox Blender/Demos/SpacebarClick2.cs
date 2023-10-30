using UnityEngine;

namespace SkyboxBlenderSpace
{
    public class SpacebarClick2 : MonoBehaviour
    {
        public SkyboxBlender skyboxScript;
        bool isStopped;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                skyboxScript.Blend(true);
                isStopped = false;
            }

            // stop blending
            if (Input.GetKeyDown(KeyCode.E)) {
                if (isStopped) {
                    skyboxScript.Blend(true);
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
