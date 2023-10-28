using UnityEngine;

namespace SkyboxBlenderSpace
{
    public class SpacebarClick3 : MonoBehaviour
    {
        public SkyboxBlender skyboxScript;
        bool isStopped;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                skyboxScript.Blend(2);
                isStopped = false;
            }

            // stop blending
            if (Input.GetKeyDown(KeyCode.E)) {
                if (isStopped) {
                    skyboxScript.Blend(2);
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
