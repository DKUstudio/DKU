using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[AddComponentMenu("Skybox Blender/Skybox Blender")]
public class SkyboxBlender : MonoBehaviour
{
    [Tooltip("The materials you want to blend to linearly.")]
    public Material[] skyboxMaterials;     
    [Tooltip("Checking this will instantly make the first material your current skybox.")]                                         
    public bool makeFirstMaterialSkybox;                                            
    [Min(0), Tooltip("The speed of the blending between the skyboxes.")]
    public float blendSpeed = 0.5f;   
    [Min(0), Tooltip("The time to wait before blending the next skybox material.")]                                                                                                
    public float timeToWait = 0f;      
    [Tooltip("If enabled, will loop the materials list. When the blender reaches the last skybox in the list, it'll blend back to the first one.")]                                             
    public bool loop = true;       

    [Tooltip("If enabled, the lighting of the world will be updated to that of the skyboxes blending.")]
    public bool updateLighting;
    [Tooltip("If enabled, the reflections of the world will be updated to that of the skyboxes blending.")]
    public bool updateReflections;        
    [Range(1, 30), Tooltip("Set how many frames need to pass during blend before updating the reflections & lighting each time. Updating these take a toll on performance so the higher this number is, the more performant your game will be (during blend) but the less accurate the lighting & reflections update will be. The less this number is, the slower the game will be but the accuracy increases. By average the best performance/accuracy results is setting it between 5-10.")]
    public int updateEveryFrames = 5;                                       


    [Tooltip("Keep rotating the skybox infinetly while blending.")]
    public bool keepRotating;
    [Tooltip("if you would prefer a certain degree to rotate the skybox to during blending - 360 is a full turn.")]
    public float rotateToAngle = 180;                                                          
    [Min(0), Tooltip("The speed of the skybox rotation.")]
    public float rotationSpeed;  
    [Tooltip("If enabled, the rotation will stop when the blend finishes. If disabled, even after blending the skybox will continue rotating. TAKE NOTE: if loop is enabled in blend options. This will not take effect.")]
    public bool stopRotationOnBlendFinish;


    #region SYSTEM VARIABLES                                       
    
    Material defaultSkyboxMaterial;   
    Material skyboxBlenderMaterial;     
    Texture currentTexture; 

    float totalBlendValue = 1f;                                                                
    float blendValue;
    float defaultBlend;                                                             
    float defaultRotation;                                                          
    float rotationSpeedValue;                                                                          

    int index = 0;                                                               
    public int CurrentIndex {
        get { return index; }
    }

    int indexToBlend;  
    int usedBlend;
    int LightAndReflectionFrames;

    bool linearBlending;                                                         
    bool currentSkyboxNotFirstMaterialBlending;                                                                
    bool comingFromLoop;
    bool rotateSkybox;  
    bool oneTickBlend = false;                                                       
    bool stillRunning = false;                                                      
    bool singleBlend = false;                                                    
    bool stopped = false;
    bool blendByIndex;
    bool stopRotation;
    bool blendFinished;
    bool blendingCurrentSkyToListNotSingleBlend;
    bool isLinearBlend;
    bool isSetReflectionProbeOnStart;

    ReflectionProbe reflectionProbe;    
    Cubemap cubemap = null;    

    #endregion

    #region UNITY METHODS

    void Awake()
    {
        // load the material
        skyboxBlenderMaterial = Resources.Load("Material & Shader/Skybox Blend Material", typeof(Material)) as Material;

        if (skyboxBlenderMaterial) {
            defaultBlend = skyboxBlenderMaterial.GetFloat("_BlendCubemaps");
            defaultRotation = skyboxBlenderMaterial.GetFloat("_Rotation");
            defaultSkyboxMaterial = skyboxBlenderMaterial;
            InspectorAndAwakeChanges();
        }
        else {
            Debug.LogWarning("Can't find Skybox Blend Material in resources. Please re-import!");
        }

        if (updateLighting || updateReflections) {
            SetReflectionProbe();
            UpdateLightingAndReflections(true);
        }
    }

    void OnValidate()
    {
        if (skyboxBlenderMaterial == null) {
            skyboxBlenderMaterial = Resources.Load("Material & Shader/Skybox Blend Material", typeof(Material)) as Material;
        }

        InspectorAndAwakeChanges();
    }

    void OnApplicationQuit()
    {
        // return the material to original blend
        skyboxBlenderMaterial.SetFloat("_BlendCubemaps", defaultBlend);
        skyboxBlenderMaterial.SetFloat("_Rotation", defaultRotation);
        
        if (currentTexture != null) { 
            skyboxBlenderMaterial.SetTexture("_Tex", currentTexture);
        }
        
        RenderSettings.skybox = defaultSkyboxMaterial;
    }

    void Update() 
    {
        // when in editor mode, set the skybox material in the skybox linearBlendmaterial
        if (!Application.isPlaying) {
            if (RenderSettings.skybox == null) {
                return;
            }
            
            if (RenderSettings.skybox.HasProperty("_Tex")) {
                skyboxBlenderMaterial.SetTexture("_Tex", RenderSettings.skybox.GetTexture("_Tex"));
                skyboxBlenderMaterial.SetColor("_Tint", RenderSettings.skybox.GetColor("_Tint"));
            }

            return;
        }

        if (updateReflections && !isSetReflectionProbeOnStart) {
            if (!SetReflectionProbeTexture()) {
                return;
            }
        }


        // skybox blending linearly the list
        if (linearBlending && !stopped) {
            // set the type of the used blending
            usedBlend = 1;

            blendValue += Time.deltaTime * blendSpeed;
            skyboxBlenderMaterial.SetFloat("_BlendCubemaps", blendValue); 
            UpdateLightingAndReflections();
            
            if (skyboxBlenderMaterial.GetFloat("_BlendCubemaps") >= totalBlendValue) 
            {
                blendFinished = true;
                linearBlending = false;
                blendValue = 0f;
                StopAllCoroutines();

                skyboxBlenderMaterial.SetFloat("_BlendCubemaps", 0f);

                SetSkyBoxes(true, index, false, 0, true);
                UpdateLightingAndReflections(true);


                if (comingFromLoop) {
                    index = 0;
                }

                // increment index and linearBlend if not reached end
                if ((index + 1) < skyboxMaterials.Length) {
                    
                    if (!comingFromLoop) {
                        index++;
                    }


                    comingFromLoop = false;
                    SetSkyBoxes(true, index);


                    if (index + 1 < skyboxMaterials.Length) {
                        SetSkyBoxes(false, 0, true, index+1);
                    }
                    

                    if (index - (skyboxMaterials.Length - 1) > 0) {
                        if (!oneTickBlend) linearBlending = true;
                    }
                    else {
                        if (!oneTickBlend) StartCoroutine(WaitBeforeBlending());
                    }
                }

                // if reached end and loopable
                if (index >= skyboxMaterials.Length-1) {
                    if (loop) {
                        if (oneTickBlend) {
                            stillRunning = false;
                            return;
                        }

                        SetSkyBoxes(true, index, true, 0, true);
                        comingFromLoop = true;

                        StartCoroutine(WaitBeforeBlending());
                    }
                    else {
                        stillRunning = false;
                        
                        if (stopRotationOnBlendFinish) {
                            StopRotation();
                        }
                    }
                }
            }
            else {
                blendFinished = false;
            }
        }


        // single blending
        if (singleBlend && !stopped) {
            blendValue += Time.deltaTime * blendSpeed;
            skyboxBlenderMaterial.SetFloat("_BlendCubemaps", blendValue);
            UpdateLightingAndReflections();
            

            if (skyboxBlenderMaterial.GetFloat("_BlendCubemaps") >= totalBlendValue) {
                blendFinished = true;
                singleBlend = false;
                blendValue = 0f;
                StopAllCoroutines();

                
                if (blendByIndex) {
                    index = indexToBlend;
                    blendByIndex = false;
                }
                else {
                    if (index + 1 < skyboxMaterials.Length) index++;
                    else index = 0;
                }
                
                
                skyboxBlenderMaterial.SetFloat("_BlendCubemaps", 0f);
                
    
                SetSkyBoxes(true, index, false, 0, true);
                UpdateLightingAndReflections(true);


                stillRunning = false;

                
                if (stopRotationOnBlendFinish) {
                    StopRotation();
                }
            }
            else {
                blendFinished = false;
            }
        }
        

        // blending for if the current skybox is not the same as first material
        if (currentSkyboxNotFirstMaterialBlending && !stopped) {
            // set the type of the used blending
            usedBlend = 2;


            blendValue += Time.deltaTime * blendSpeed;
            skyboxBlenderMaterial.SetFloat("_BlendCubemaps", blendValue);
            UpdateLightingAndReflections();

            
            if (skyboxBlenderMaterial.GetFloat("_BlendCubemaps") >= totalBlendValue) {
                blendFinished = true;
                currentSkyboxNotFirstMaterialBlending = false;
                blendValue = 0f;
                StopAllCoroutines();


                int indexForFirst = 0;
                int indexForSecond = 1;


                skyboxBlenderMaterial.SetFloat("_BlendCubemaps", 0f);


                if (skyboxMaterials.Length == 1) {
                    indexForSecond = 0;
                }


                SetSkyBoxes(true, 0, true, indexForSecond, true);
                UpdateLightingAndReflections(true);


                if (oneTickBlend) {
                    stillRunning = false;
                }
                else {
                    StartCoroutine(WaitBeforeBlending());
                }


                if (stopRotationOnBlendFinish && !blendingCurrentSkyToListNotSingleBlend) {
                    StopRotation();
                }


                blendingCurrentSkyToListNotSingleBlend = false;
            }
            else {
                blendFinished = false;
            }
        }


        // skybox rotation
        if (rotateSkybox && !stopRotation) {
            rotationSpeedValue += Time.deltaTime * rotationSpeed;

            if (keepRotating) {
                skyboxBlenderMaterial.SetFloat("_Rotation", rotationSpeedValue);
            }
            else {
                if (skyboxBlenderMaterial.GetFloat("_Rotation") < rotateToAngle) {
                    skyboxBlenderMaterial.SetFloat("_Rotation", rotationSpeedValue);
                }
                else {
                    rotateSkybox = false;
                    skyboxBlenderMaterial.SetFloat("_Rotation", rotateToAngle);
                }
            }
        }
    }

    #endregion

    #region SYSTEM METHODS

    // set the skybox material
    void SetSkyBoxes(bool firstTex = false, int firstTexIndex = 0, bool secondTex = false, int secondTexIndex = 0, bool apply = false)
    {
        if (firstTex) {
            skyboxBlenderMaterial.SetTexture("_Tex", skyboxMaterials[firstTexIndex].GetTexture("_Tex"));
            skyboxBlenderMaterial.SetColor("_Tint", skyboxMaterials[firstTexIndex].GetColor("_Tint"));
        }

        if (secondTex) {
            skyboxBlenderMaterial.SetTexture("_Tex2", skyboxMaterials[secondTexIndex].GetTexture("_Tex"));
            skyboxBlenderMaterial.SetColor("_Tint2", skyboxMaterials[secondTexIndex].GetColor("_Tint"));
        }

        if (apply) {
            RenderSettings.skybox = skyboxBlenderMaterial;
        }
    }

    
    // setup the skybox material before blending
    void PrepareMaterialForBlend(int skyboxIndex)
    {
        // set texture
        skyboxBlenderMaterial.SetTexture("_Tex", RenderSettings.skybox.GetTexture("_Tex"));
        skyboxBlenderMaterial.SetTexture("_Tex2", skyboxMaterials[skyboxIndex].GetTexture("_Tex"));

        // set tint
        skyboxBlenderMaterial.SetColor("_Tint", RenderSettings.skybox.GetColor("_Tint"));
        skyboxBlenderMaterial.SetColor("_Tint2", skyboxMaterials[skyboxIndex].GetColor("_Tint"));
    }


    // wait for time for normal blending
    IEnumerator WaitBeforeBlending()
    {
        isLinearBlend = true;
        yield return new WaitForSeconds(timeToWait);
        linearBlending = true;
    }


    // change skyboxes and material textures on inspector change and script awake
    void InspectorAndAwakeChanges()
    {
        if (makeFirstMaterialSkybox) {
            if (skyboxMaterials.Length >= 1) {
                if (skyboxMaterials[0] != null) { 
                    skyboxBlenderMaterial.SetTexture("_Tex", skyboxMaterials[0].GetTexture("_Tex"));
                    skyboxBlenderMaterial.SetColor("_Tint", skyboxMaterials[0].GetColor("_Tint"));
                    RenderSettings.skybox = skyboxBlenderMaterial;  
                }
            }
            else {
                Debug.LogWarning("You need to set a material first to make it the skybox");
            }
        }

        if (skyboxMaterials != null) {
            if (skyboxMaterials.Length > 1) {
                if (skyboxMaterials[1] != null) {
                    skyboxBlenderMaterial.SetTexture("_Tex2", skyboxMaterials[1].GetTexture("_Tex"));
                    skyboxBlenderMaterial.SetColor("_Tint2", skyboxMaterials[1].GetColor("_Tint"));
                }
            }
        }
    }


    // set reflection probe for reflections
    void SetReflectionProbe()
    {
        reflectionProbe = GetComponent<ReflectionProbe>();

        if (reflectionProbe == null) {
            reflectionProbe = gameObject.AddComponent<ReflectionProbe>() as ReflectionProbe;
        }

        reflectionProbe.cullingMask = 0;
        reflectionProbe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        reflectionProbe.mode = ReflectionProbeMode.Realtime;
        reflectionProbe.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;

        if (updateReflections) {
            RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
            cubemap = new Cubemap(reflectionProbe.resolution, reflectionProbe.hdr ? TextureFormat.RGBAHalf : TextureFormat.RGBA32, true);
        }
    }


    // update the lighting and reflections of the world
    void UpdateLightingAndReflections(bool forceUpdate=false)
    {
        // exit if both options are off
        if (!updateReflections && !updateLighting) {
            LightAndReflectionFrames = 0;
            return;
        }

        if (!forceUpdate) {
            // run every set frames for performance
            if (LightAndReflectionFrames < updateEveryFrames) {
                LightAndReflectionFrames++;
                return;
            }
        }

        // update the lighting if set
        if (updateLighting) {
            DynamicGI.UpdateEnvironment();
        }

        // if update reflections is off then exit the function
        if (!updateReflections) {
            return;
        }

        LightAndReflectionFrames = 0;
        reflectionProbe.RenderProbe();
        
        if (reflectionProbe.texture != null) {
            Graphics.CopyTexture(reflectionProbe.texture, cubemap as Texture);
            RenderSettings.customReflection = cubemap;
            return;
        }
    }

    bool SetReflectionProbeTexture()
    {
        if (!isSetReflectionProbeOnStart) {
            SetReflectionProbe();
            UpdateLightingAndReflections(true);

            if (reflectionProbe.texture != null) {
                isSetReflectionProbeOnStart = true;
            }
        }

        return isSetReflectionProbeOnStart;
    }

    #endregion

    #region PUBLIC APIs

    // trigger the skybox blend
    public void Blend(bool singlePassBlend = false, bool rotate = true)
    {   
        if (currentSkyboxNotFirstMaterialBlending && !stopped) {
            return;
        }


        if (isLinearBlend && !stopped) {
            return;
        }


        if (rotate) {
            rotateSkybox = true;
            stopRotation = false;
        }


        if ((stopped || stillRunning) && !singlePassBlend) {
            stopped = false;
            
            if (blendFinished) {
                if ((usedBlend == 1 || usedBlend == 2) && !stillRunning) {
                    StartCoroutine(WaitBeforeBlending());
                    return;
                }
            }
        }

        
        stopped = false;
        blendByIndex = false;

        StopAllCoroutines();
        currentTexture = RenderSettings.skybox.GetTexture("_Tex");


        if (blendValue > 0) {
            oneTickBlend = singlePassBlend;
            return;
        }
        
        
        if (singlePassBlend) {
            if (index == 0 && currentTexture != skyboxMaterials[0].GetTexture("_Tex")) {
                PrepareMaterialForBlend(0);
                currentSkyboxNotFirstMaterialBlending = true;
            }
            else {
                int indexToTransition = index;

                if (!stopped) {
                    if (index >= skyboxMaterials.Length - 1) {
                        indexToTransition = 0;
                    }
                    else {
                        indexToTransition++;
                    }
                }

                PrepareMaterialForBlend(indexToTransition);
                singleBlend = true;
            }
            
            RenderSettings.skybox = skyboxBlenderMaterial;
            stillRunning = true;
        }
        else {
            // if only one element then linear blend from current scene skybox to the first material
            if (skyboxMaterials.Length == 1) {
                if (currentTexture != skyboxMaterials[0].GetTexture("_Tex")) {
                    PrepareMaterialForBlend(0);
                    RenderSettings.skybox = skyboxBlenderMaterial;
                }
            }
            else {
                if (index == 0 && skyboxMaterials[0] != null) {
                    if (currentTexture == skyboxMaterials[0].GetTexture("_Tex")) {
                        SetSkyBoxes(true, 0, false, 0, true);
                    }
                    else {
                        SetSkyBoxes(false, 0, true, 0, true);

                        currentSkyboxNotFirstMaterialBlending = true;
                        blendingCurrentSkyToListNotSingleBlend = true;
                    }
                }
            }


            // is this the last material
            if (index >= skyboxMaterials.Length - 1) {
                comingFromLoop = true;
            }


            // if the rotate parameter passed
            if (rotate) {
                rotateSkybox = true;
                stopRotation = false;
            }


            // flag some vars to start the blending in Update
            if (!currentSkyboxNotFirstMaterialBlending) {
                linearBlending = true;
                stillRunning = true;

                if (rotate) rotateSkybox = true;
            }


            isLinearBlend = true;
        }


        oneTickBlend = singlePassBlend;
    }

    // call using material index
    public void Blend(int skyboxIndex, bool rotate = true) 
    {
        stopped = false;

        if (stillRunning) return;
        
        
        if (index == skyboxIndex) {
            Debug.Log("Skybox material already set on the one you're trying to call.");
            return;
        }


        if (skyboxIndex > skyboxMaterials.Length - 1) {
            Debug.Log("The passed index is bigger than the length of the skybox materials list.");
            return;
        }


        if (skyboxIndex < 0) {
            skyboxIndex = skyboxMaterials.Length - 1;
        }


        if (skyboxMaterials[skyboxIndex] == null) {
            Debug.Log("There is no material in the list with the passed index.");
            return;
        }
        

        StopAllCoroutines();
        currentTexture = RenderSettings.skybox.GetTexture("_Tex");
        
        
        blendByIndex = true;
        indexToBlend = skyboxIndex;


        PrepareMaterialForBlend(skyboxIndex);
        singleBlend = true;


        RenderSettings.skybox = skyboxBlenderMaterial;
        
        
        if (rotate) {
            rotateSkybox = true;
            stopRotation = false;
        }


        stillRunning = true;
        oneTickBlend = true;
    }

    // cancel the current blend and reset the skybox to what it was before the blend
    public void Cancel()
    {
        StopAllCoroutines();


        linearBlending = false;
        singleBlend = false;
        currentSkyboxNotFirstMaterialBlending = false;
        blendingCurrentSkyToListNotSingleBlend = false;
        oneTickBlend = false;
        stopped = false;
        blendValue = 0;
        stillRunning = false;
        isLinearBlend = false;
        comingFromLoop = false;


        skyboxBlenderMaterial.SetFloat("_BlendCubemaps", 0f);
        

        SetSkyBoxes(true, index, false, 0, true);
        UpdateLightingAndReflections(true);
    }

    // stop the blending
    public void Stop(bool stopRot=true)
    {
        stopped = true;
        StopAllCoroutines();
        
        if (stopRot && rotateSkybox) {
            stopRotation = true;
        }
    }

    // resume the blending
    public void Resume(bool resumeRot=true)
    {
        stopped = false;
        
        if (resumeRot) {
            stopRotation = false;
        }

        if (usedBlend == 1 || usedBlend == 2) {
            if (blendFinished) {
                StartCoroutine(WaitBeforeBlending());
            }
        }
    }
    
    // check if blending is in process
    public bool IsBlending()
    {
        if (stopped) {
            return false;
        }

        if (linearBlending|| singleBlend || currentSkyboxNotFirstMaterialBlending) {
            return true;
        }

        return false;
    }

    // rotate the skybox only
    public void Rotate()
    {
        skyboxBlenderMaterial.SetTexture("_Tex", RenderSettings.skybox.GetTexture("_Tex"));
        skyboxBlenderMaterial.SetColor("_Tint", RenderSettings.skybox.GetColor("_Tint"));
        RenderSettings.skybox = skyboxBlenderMaterial;

        rotateSkybox = true;
        stopRotation = false;
    }

    // stop the rotation only
    public void StopRotation()
    {
        rotateSkybox = false;
        stopRotation = false;
    }

    #endregion
}