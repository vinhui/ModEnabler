using FullSerializer;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace ModEnabler.Resource.DataObjects
{
    [Serializable]
    public struct ParticleSystemData
    {
#if UNITY_5_5_OR_NEWER
        public MainModule main;
#else
        public float gravityModifier;
        public bool loop;
        public int maxParticles;
        public float playbackSpeed;
        public bool playOnAwake;
        public ParticleSystemScalingMode scalingMode;
        public ParticleSystemSimulationSpace simulationSpace;
        public Color startColor;
        public float startDelay;
        public float startLifetime;
        public float startRotation;
        public Vector3 startRotation3D;
        public float startSize;
        public float startSpeed;
#endif
        public uint randomSeed;
        public float time;

        public CollisionModule collision;
        public ColorBySpeedModule colorBySpeed;
        public ColorOverLifetimeModule colorOverLifetime;
        public EmissionModule emission;
        public ExternalForcesModule externalForces;
        public ForceOverLifetimeModule forceOverLifetime;
        public InheritVelocityModule inheritVelocity;
        public LimitVelocityOverLifetimeModule limitVelocityOverLifetime;
        public RotationBySpeedModule rotationBySpeed;
        public RotationOverLifetimeModule rotationOverLifetime;
        public ShapeModule shape;
        public SizeBySpeedModule sizeBySpeed;
        public SizeOverLifetimeModule sizeOverLifetime;
        public SubEmittersModule subEmitters;
        public TextureSheetAnimationModule textureSheetAnimation;
        public VelocityOverLifetimeModule velocityOverLifetime;
        public Renderer renderer;

        public ParticleSystemData(ParticleSystem ps)
        {
#if UNITY_5_5_OR_NEWER
            main = ps.main;
#else
            gravityModifier = ps.gravityModifier;
            loop = ps.loop;
            maxParticles = ps.maxParticles;
            playbackSpeed = ps.playbackSpeed;
            playOnAwake = ps.playOnAwake;
            scalingMode = ps.scalingMode;
            simulationSpace = ps.simulationSpace;
            startColor = ps.startColor;
            startDelay = ps.startDelay;
            startLifetime = ps.startLifetime;
            startRotation = ps.startRotation;
            startRotation3D = ps.startRotation3D;
            startSize = ps.startSize;
            startSpeed = ps.startSpeed;
#endif

            collision = ps.collision;
            colorBySpeed = ps.colorBySpeed;
            colorOverLifetime = ps.colorOverLifetime;
            emission = ps.emission;
            externalForces = ps.externalForces;
            forceOverLifetime = ps.forceOverLifetime;
            inheritVelocity = ps.inheritVelocity;
            limitVelocityOverLifetime = ps.limitVelocityOverLifetime;
            randomSeed = ps.randomSeed;
            rotationBySpeed = ps.rotationBySpeed;
            rotationOverLifetime = ps.rotationOverLifetime;
            shape = ps.shape;
            sizeBySpeed = ps.sizeBySpeed;
            sizeOverLifetime = ps.sizeOverLifetime;
            subEmitters = ps.subEmitters;
            textureSheetAnimation = ps.textureSheetAnimation;
            time = ps.time;
            velocityOverLifetime = ps.velocityOverLifetime;
            renderer = new Renderer(ps.GetComponent<ParticleSystemRenderer>());
        }

        public ParticleSystem ToUnity(GameObject go)
        {
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (ps == null)
                ps = go.AddComponent<ParticleSystem>();

            ps.randomSeed = randomSeed;
            ps.time = time;

#if !UNITY_5_5_OR_NEWER
            ps.loop = loop;
            ps.maxParticles = maxParticles;
            ps.playbackSpeed = playbackSpeed;
            ps.playOnAwake = playOnAwake;
            ps.scalingMode = scalingMode;
            ps.simulationSpace = simulationSpace;
            ps.startColor = startColor;
            ps.startDelay = startDelay;
            ps.startLifetime = startLifetime;
            ps.startRotation = startRotation;
            ps.startRotation3D = startRotation3D;
            ps.startSize = startSize;
            ps.startSpeed = startSpeed;
            ps.randomSeed = randomSeed;
            ps.time = time;
#endif

            #region Modules

#if UNITY_5_5_OR_NEWER
            main.ToUnity(ps.main);
#endif
            collision.ToUnity(ps.collision);
            colorBySpeed.ToUnity(ps.colorBySpeed);
            colorOverLifetime.ToUnity(ps.colorOverLifetime);
            emission.ToUnity(ps.emission);
            externalForces.ToUnity(ps.externalForces);
            forceOverLifetime.ToUnity(ps.forceOverLifetime);
            inheritVelocity.ToUnity(ps.inheritVelocity);
            limitVelocityOverLifetime.ToUnity(ps.limitVelocityOverLifetime);
            rotationBySpeed.ToUnity(ps.rotationBySpeed);
            rotationOverLifetime.ToUnity(ps.rotationOverLifetime);
            shape.ToUnity(ps.shape, go);
            sizeBySpeed.ToUnity(ps.sizeBySpeed);
            sizeOverLifetime.ToUnity(ps.sizeOverLifetime);
            subEmitters.ToUnity(ps.subEmitters, go);
            textureSheetAnimation.ToUnity(ps.textureSheetAnimation);
            velocityOverLifetime.ToUnity(ps.velocityOverLifetime);
            renderer.ToUnity(ps.GetComponent<ParticleSystemRenderer>());

            #endregion Modules

            return ps;
        }

#if UNITY_5_5_OR_NEWER

        public struct MainModule
        {
            private Transform customSimulationSpace;
            private float duration;
            private MinMaxCurve gravityModifier;
            private float gravityModifierMultiplier;
            private bool loop;
            private int maxParticles;
            private bool playOnAwake;
            private bool prewarm;
            private float randomizeRotationDirection;
            private ParticleSystemScalingMode scalingMode;
            private ParticleSystemSimulationSpace simulationSpace;
            private float simulationSpeed;
            private MinMaxGradient startColor;
            private MinMaxCurve startDelay;
            private float startDelayMultiplier;
            private MinMaxCurve startLifetime;
            private float startLifetimeMultiplier;
            private MinMaxCurve startRotation;
            private bool startRotation3D;
            private float startRotationMultiplier;
            private MinMaxCurve startRotationX;
            private float startRotationXMultiplier;
            private MinMaxCurve startRotationY;
            private float startRotationYMultiplier;
            private MinMaxCurve startRotationZ;
            private float startRotationZMultiplier;
            private MinMaxCurve startSize;
            private bool startSize3D;
            private float startSizeMultiplier;
            private MinMaxCurve startSizeX;
            private float startSizeXMultiplier;
            private MinMaxCurve startSizeY;
            private float startSizeYMultiplier;
            private MinMaxCurve startSizeZ;
            private float startSizeZMultiplier;
            private MinMaxCurve startSpeed;
            private float startSpeedMultiplier;

            public static implicit operator MainModule(ParticleSystem.MainModule a)
            {
                return new MainModule
                {
                    customSimulationSpace = a.customSimulationSpace,
                    duration = a.duration,
                    gravityModifier = a.gravityModifier,
                    gravityModifierMultiplier = a.gravityModifierMultiplier,
                    loop = a.loop,
                    maxParticles = a.maxParticles,
                    playOnAwake = a.playOnAwake,
                    prewarm = a.prewarm,
                    randomizeRotationDirection = a.randomizeRotationDirection,
                    scalingMode = a.scalingMode,
                    simulationSpace = a.simulationSpace,
                    simulationSpeed = a.simulationSpeed,
                    startColor = a.startColor,
                    startDelay = a.startDelay,
                    startDelayMultiplier = a.startDelayMultiplier,
                    startLifetime = a.startLifetime,
                    startLifetimeMultiplier = a.startLifetimeMultiplier,
                    startRotation = a.startRotation,
                    startRotation3D = a.startRotation3D,
                    startRotationMultiplier = a.startRotationMultiplier,
                    startRotationX = a.startRotationX,
                    startRotationXMultiplier = a.startRotationXMultiplier,
                    startRotationY = a.startRotationY,
                    startRotationYMultiplier = a.startRotationYMultiplier,
                    startRotationZ = a.startRotationZ,
                    startRotationZMultiplier = a.startRotationZMultiplier,
                    startSize = a.startSize,
                    startSize3D = a.startSize3D,
                    startSizeMultiplier = a.startSizeMultiplier,
                    startSizeX = a.startSizeX,
                    startSizeXMultiplier = a.startSizeXMultiplier,
                    startSizeY = a.startSizeY,
                    startSizeYMultiplier = a.startSizeYMultiplier,
                    startSizeZ = a.startSizeZ,
                    startSizeZMultiplier = a.startSizeZMultiplier,
                    startSpeed = a.startSpeed,
                    startSpeedMultiplier = a.startSpeedMultiplier,
                };
            }

            public void ToUnity(ParticleSystem.MainModule a)
            {
                a.customSimulationSpace = customSimulationSpace;
                a.duration = duration;
                a.gravityModifier = ParticleSystemHelpers.Convert(gravityModifier);
                a.gravityModifierMultiplier = gravityModifierMultiplier;
                a.loop = loop;
                a.maxParticles = maxParticles;
                a.playOnAwake = playOnAwake;
                a.prewarm = prewarm;
                a.randomizeRotationDirection = randomizeRotationDirection;
                a.scalingMode = scalingMode;
                a.simulationSpace = simulationSpace;
                a.simulationSpeed = simulationSpeed;
                a.startColor = ParticleSystemHelpers.Convert(startColor);
                a.startDelay = ParticleSystemHelpers.Convert(startDelay);
                a.startDelayMultiplier = startDelayMultiplier;
                a.startLifetime = ParticleSystemHelpers.Convert(startLifetime);
                a.startLifetimeMultiplier = startLifetimeMultiplier;
                a.startRotation = ParticleSystemHelpers.Convert(startRotation);
                a.startRotation3D = startRotation3D;
                a.startRotationMultiplier = startRotationMultiplier;
                a.startRotationX = ParticleSystemHelpers.Convert(startRotationX);
                a.startRotationXMultiplier = startRotationXMultiplier;
                a.startRotationY = ParticleSystemHelpers.Convert(startRotationY);
                a.startRotationYMultiplier = startRotationYMultiplier;
                a.startRotationZ = ParticleSystemHelpers.Convert(startRotationZ);
                a.startRotationZMultiplier = startRotationZMultiplier;
                a.startSize = ParticleSystemHelpers.Convert(startSize);
                a.startSize3D = startSize3D;
                a.startSizeMultiplier = startSizeMultiplier;
                a.startSizeX = ParticleSystemHelpers.Convert(startSizeX);
                a.startSizeXMultiplier = startSizeXMultiplier;
                a.startSizeY = ParticleSystemHelpers.Convert(startSizeY);
                a.startSizeYMultiplier = startSizeYMultiplier;
                a.startSizeZ = ParticleSystemHelpers.Convert(startSizeZ);
                a.startSizeZMultiplier = startSizeZMultiplier;
                a.startSpeed = ParticleSystemHelpers.Convert(startSpeed);
                a.startSpeedMultiplier = startSpeedMultiplier;
            }
        }

#endif

        public struct Renderer
        {
            public ParticleSystemRenderMode renderMode;
            public float normalDirection;
            public string material;
            public string mesh;
            public ParticleSystemSortMode sortMode;
            public float sortingFudge;
            public ShadowCastingMode shadowCastingMode;
            public bool receiveShadows;
            public float minParticleSize;
            public float maxParticleSize;
            public string sortingLayerName;
            public int sortingOrder;
            public ParticleSystemRenderSpace alignment;
            public Vector3 pivot;

            public Renderer(ParticleSystemRenderer renderer)
            {
                renderMode = renderer.renderMode;
                normalDirection = renderer.normalDirection;
                if (renderer.sharedMaterial != null)
                {
                    material = renderer.sharedMaterial.name;
                    Debug.LogWarning("There is a material referenced for the renderer material but we can only guess the name for it is '" + material + "', be sure to check it!");
                }
                else
                    material = null;
                if (renderer.mesh != null)
                {
                    mesh = renderer.mesh.name;
                    Debug.LogWarning("There is a mesh referenced for the particlesystem renderer but we can only guess the name for it is '" + mesh + "', be sure to check it!");
                }
                else
                    mesh = null;
                sortMode = renderer.sortMode;
                sortingFudge = renderer.sortingFudge;
                shadowCastingMode = renderer.shadowCastingMode;
                receiveShadows = renderer.receiveShadows;
                minParticleSize = renderer.minParticleSize;
                maxParticleSize = renderer.maxParticleSize;
                sortingLayerName = renderer.sortingLayerName;
                sortingOrder = renderer.sortingOrder;
                alignment = renderer.alignment;
                pivot = renderer.pivot;
            }

            public void ToUnity(ParticleSystemRenderer a)
            {
                a.renderMode = renderMode;
                a.normalDirection = normalDirection;
                if (!string.IsNullOrEmpty(material))
                    a.material = ResourceManager.LoadMaterial(material);
                if (!string.IsNullOrEmpty(mesh))
                    a.mesh = ResourceManager.LoadMesh(mesh);
                a.sortMode = sortMode;
                a.sortingFudge = sortingFudge;
                a.shadowCastingMode = shadowCastingMode;
                a.receiveShadows = receiveShadows;
                a.minParticleSize = minParticleSize;
                a.maxParticleSize = maxParticleSize;
                a.sortingLayerName = sortingLayerName;
                a.sortingOrder = sortingOrder;
                a.alignment = alignment;
                a.pivot = pivot;
            }
        }

        public struct Burst
        {
            public short maxCount;
            public short minCount;
            public float time;

            public static implicit operator Burst(ParticleSystem.Burst a)
            {
                return new Burst
                {
                    maxCount = a.maxCount,
                    minCount = a.minCount,
                    time = a.time
                };
            }

            public static implicit operator ParticleSystem.Burst(Burst a)
            {
                return new ParticleSystem.Burst
                {
                    maxCount = a.maxCount,
                    minCount = a.minCount,
                    time = a.time
                };
            }
        }

        public struct CollisionModule
        {
            public MinMaxCurve bounce;
            public LayerMask collidesWith;
            public MinMaxCurve dampen;
            public bool enabled;
            public bool enableDynamicColliders;
            public bool enableInteriorCollisions;
            public MinMaxCurve lifetimeLoss;
            public int maxCollisionShapes;
            public float minKillSpeed;
            public ParticleSystemCollisionMode mode;
            public ParticleSystemCollisionQuality quality;
            public float radiusScale;
            public bool sendCollisionMessages;
            public ParticleSystemCollisionType type;
            public float voxelSize;

            public static implicit operator CollisionModule(ParticleSystem.CollisionModule a)
            {
                return new CollisionModule()
                {
                    bounce = a.bounce,
                    collidesWith = a.collidesWith,
                    dampen = a.dampen,
                    enabled = a.enabled,
                    enableDynamicColliders = a.enableDynamicColliders,
                    enableInteriorCollisions = a.enableInteriorCollisions,
                    lifetimeLoss = a.lifetimeLoss,
                    maxCollisionShapes = a.maxCollisionShapes,
                    minKillSpeed = a.minKillSpeed,
                    mode = a.mode,
                    quality = a.quality,
                    radiusScale = a.radiusScale,
                    sendCollisionMessages = a.sendCollisionMessages,
                    type = a.type,
                    voxelSize = a.voxelSize
                };
            }

            public void ToUnity(ParticleSystem.CollisionModule a)
            {
                a.enabled = enabled;
                a.bounce = ParticleSystemHelpers.Convert(bounce);
                a.collidesWith = collidesWith;
                a.dampen = ParticleSystemHelpers.Convert(dampen);
                a.enableDynamicColliders = enableDynamicColliders;
                a.enableInteriorCollisions = enableInteriorCollisions;
                a.lifetimeLoss = ParticleSystemHelpers.Convert(lifetimeLoss);
                a.maxCollisionShapes = maxCollisionShapes;
                a.minKillSpeed = minKillSpeed;
                a.mode = mode;
                a.quality = quality;
                a.radiusScale = radiusScale;
                a.sendCollisionMessages = sendCollisionMessages;
                a.type = type;
                a.voxelSize = voxelSize;
                a.radiusScale = radiusScale;
                a.radiusScale = radiusScale;
            }
        }

        public struct ColorBySpeedModule
        {
            public MinMaxGradient color;
            public bool enabled;
            public Vector2 range;

            public static implicit operator ColorBySpeedModule(ParticleSystem.ColorBySpeedModule a)
            {
                return new ColorBySpeedModule()
                {
                    color = a.color,
                    enabled = a.enabled,
                    range = a.range
                };
            }

            public void ToUnity(ParticleSystem.ColorBySpeedModule a)
            {
                a.color = ParticleSystemHelpers.Convert(color);
                a.enabled = enabled;
                a.range = range;
            }
        }

        public struct ColorOverLifetimeModule
        {
            public MinMaxGradient color;
            public bool enabled;

            public static implicit operator ColorOverLifetimeModule(ParticleSystem.ColorOverLifetimeModule a)
            {
                return new ColorOverLifetimeModule()
                {
                    color = a.color,
                    enabled = a.enabled
                };
            }

            public void ToUnity(ParticleSystem.ColorOverLifetimeModule a)
            {
                a.color = ParticleSystemHelpers.Convert(color);
                a.enabled = enabled;
            }
        }

        public struct EmissionModule
        {
            public bool enabled;
#if UNITY_5_5_OR_NEWER
            public MinMaxCurve rateOverDistance;
            public float rateOverDistanceMultiplier;
            public MinMaxCurve rateOverTime;
            public float rateOverTimeMultiplier;
#else
            public MinMaxCurve rate;
            public ParticleSystemEmissionType type;
            public Burst[] bursts;
#endif

            public static implicit operator EmissionModule(ParticleSystem.EmissionModule a)
            {
                ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[a.burstCount];
                a.GetBursts(bursts);

                return new EmissionModule()
                {
                    enabled = a.enabled,
#if UNITY_5_5_OR_NEWER
                    rateOverDistance = a.rateOverDistance,
                    rateOverDistanceMultiplier = a.rateOverDistanceMultiplier,
                    rateOverTime = a.rateOverTime,
                    rateOverTimeMultiplier = a.rateOverTimeMultiplier
#else
                    rate = a.rate,
                    type = a.type,
                    bursts = bursts.Cast<Burst>().ToArray(),
#endif
                };
            }

            public void ToUnity(ParticleSystem.EmissionModule a)
            {
                a.enabled = enabled;
#if UNITY_5_5_OR_NEWER
                a.rateOverDistance = ParticleSystemHelpers.Convert(rateOverDistance);
                a.rateOverDistanceMultiplier = rateOverDistanceMultiplier;
                a.rateOverTime = ParticleSystemHelpers.Convert(rateOverTime);
                a.rateOverTimeMultiplier = rateOverTimeMultiplier;
#else
                a.rate = ParticleSystemHelpers.Convert(rate);
                a.type = type;
                a.SetBursts(bursts.Cast<ParticleSystem.Burst>().ToArray());
#endif
            }
        }

        public struct EmitParams
        {
            public float angularVelocity;
            public Vector3 angularVelocity3D;
            public Vector3 axisOfRotation;
            public Vector3 position;
            public uint randomSeed;
            public float rotation;
            public Vector3 rotation3D;
            public Color32 startColor;
            public float startLifetime;
            public float startSize;
            public Vector3 startSize3D;
            public Vector3 velocity;

            public static implicit operator EmitParams(ParticleSystem.EmitParams a)
            {
                return new EmitParams()
                {
                    angularVelocity = a.angularVelocity,
                    angularVelocity3D = a.angularVelocity3D,
                    axisOfRotation = a.axisOfRotation,
                    position = a.position,
                    randomSeed = a.randomSeed,
                    rotation = a.rotation,
                    rotation3D = a.rotation3D,
                    startColor = a.startColor,
                    startLifetime = a.startLifetime,
                    startSize = a.startSize,
                    startSize3D = a.startSize3D,
                    velocity = a.velocity
                };
            }

            public void ToUnity(ParticleSystem.EmitParams a)
            {
                a.angularVelocity = angularVelocity;
                a.angularVelocity3D = angularVelocity3D;
                a.axisOfRotation = axisOfRotation;
                a.position = position;
                a.randomSeed = randomSeed;
                a.rotation = rotation;
                a.rotation3D = rotation3D;
                a.startColor = startColor;
                a.startLifetime = startLifetime;
                a.startSize = startSize;
                a.startSize3D = startSize3D;
                a.velocity = velocity;
            }
        }

        public struct ExternalForcesModule
        {
            public bool enabled;
            public float multiplier;

            public static implicit operator ExternalForcesModule(ParticleSystem.ExternalForcesModule a)
            {
                return new ExternalForcesModule()
                {
                    enabled = a.enabled,
                    multiplier = a.multiplier
                };
            }

            public void ToUnity(ParticleSystem.ExternalForcesModule a)
            {
                a.enabled = enabled;
                a.multiplier = multiplier;
            }
        }

        public struct ForceOverLifetimeModule
        {
            public bool enabled;
            public bool randomized;
            public ParticleSystemSimulationSpace space;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator ForceOverLifetimeModule(ParticleSystem.ForceOverLifetimeModule a)
            {
                return new ForceOverLifetimeModule()
                {
                    enabled = a.enabled,
                    randomized = a.randomized,
                    space = a.space,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.ForceOverLifetimeModule a)
            {
                a.enabled = enabled;
                a.randomized = randomized;
                a.space = space;
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }

        public struct InheritVelocityModule
        {
            public MinMaxCurve curve;
#if UNITY_5_5_OR_NEWER
            public float curveMultiplier;
#endif
            public bool enabled;
            public ParticleSystemInheritVelocityMode mode;

            public static implicit operator InheritVelocityModule(ParticleSystem.InheritVelocityModule a)
            {
                return new InheritVelocityModule()
                {
                    curve = a.curve,
#if UNITY_5_5_OR_NEWER
                    curveMultiplier = a.curveMultiplier,
#endif
                    enabled = a.enabled,
                    mode = a.mode
                };
            }

            public void ToUnity(ParticleSystem.InheritVelocityModule a)
            {
                a.curve = ParticleSystemHelpers.Convert(curve);
#if UNITY_5_5_OR_NEWER
                a.curveMultiplier = curveMultiplier;
#endif
                a.enabled = enabled;
                a.mode = mode;
            }
        }

        public struct LimitVelocityOverLifetimeModule
        {
            public float dampen;
            public bool enabled;
            public MinMaxCurve limit;
            public MinMaxCurve limitX;
            public MinMaxCurve limitY;
            public MinMaxCurve limitZ;
#if UNITY_5_5_OR_NEWER
            public float limitMultiplier;
            public float limitXMultiplier;
            public float limitYMultiplier;
            public float limitZMultiplier;
#endif
            public bool separateAxes;
            public ParticleSystemSimulationSpace space;

            public static implicit operator LimitVelocityOverLifetimeModule(ParticleSystem.LimitVelocityOverLifetimeModule a)
            {
                return new LimitVelocityOverLifetimeModule()
                {
                    dampen = a.dampen,
                    enabled = a.enabled,
                    limit = a.limit,
                    limitX = a.limitX,
                    limitY = a.limitY,
                    limitZ = a.limitZ,
#if UNITY_5_5_OR_NEWER
                    limitMultiplier = a.limitMultiplier,
                    limitXMultiplier = a.limitXMultiplier,
                    limitYMultiplier = a.limitYMultiplier,
                    limitZMultiplier = a.limitZMultiplier,
#endif
                    separateAxes = a.separateAxes,
                    space = a.space
                };
            }

            public void ToUnity(ParticleSystem.LimitVelocityOverLifetimeModule a)
            {
                a.dampen = dampen;
                a.enabled = enabled;
                a.limit = ParticleSystemHelpers.Convert(limit);
                a.limitX = ParticleSystemHelpers.Convert(limitX);
                a.limitY = ParticleSystemHelpers.Convert(limitY);
                a.limitZ = ParticleSystemHelpers.Convert(limitZ);
#if UNITY_5_5_OR_NEWER
                a.limitMultiplier = limitMultiplier;
                a.limitXMultiplier = limitXMultiplier;
                a.limitYMultiplier = limitYMultiplier;
                a.limitZMultiplier = limitZMultiplier;
#endif
                a.separateAxes = separateAxes;
                a.space = space;
            }
        }

        public struct MinMaxCurve
        {
            public float constantMax;
            public float constantMin;
            public AnimationClipData.AnimationCurveData curveMax;
            public AnimationClipData.AnimationCurveData curveMin;
            public float curveScalar;
            public ParticleSystemCurveMode mode;

            public static implicit operator MinMaxCurve(ParticleSystem.MinMaxCurve a)
            {
                return new MinMaxCurve()
                {
                    constantMax = a.constantMax,
                    constantMin = a.constantMin,
                    curveMax = new AnimationClipData.AnimationCurveData(a.curveMax),
                    curveMin = new AnimationClipData.AnimationCurveData(a.curveMin),
#if UNITY_5_5_OR_NEWER
                    curveScalar = a.curveMultiplier,
#else
                    curveScalar = a.curveScalar,
#endif
                    mode = a.mode
                };
            }
        }

        public struct MinMaxGradient
        {
            public Color colorMax;
            public Color colorMin;
            public Gradient gradientMax;
            public Gradient gradientMin;
            public ParticleSystemGradientMode mode;

            public static implicit operator MinMaxGradient(ParticleSystem.MinMaxGradient a)
            {
                return new MinMaxGradient()
                {
                    colorMax = a.colorMax,
                    colorMin = a.colorMin,
                    gradientMax = a.gradientMax,
                    gradientMin = a.gradientMin,
                    mode = a.mode
                };
            }
        }

        public struct Particle
        {
            public float angularVelocity;
            public Vector3 angularVelocity3D;
            public Vector3 axisOfRotation;
#if UNITY_5_5_OR_NEWER
            public float remainingLifeTime;
#else
            public float lifetime;
#endif
            public Vector3 position;
            public uint randomSeed;
            public float rotation;
            public Vector3 rotation3D;
            public Color32 startColor;
            public float startLifetime;
            public float startSize;
            public Vector3 velocity;
            public Vector3 startSize3D;

            public static implicit operator Particle(ParticleSystem.Particle a)
            {
                return new Particle()
                {
                    angularVelocity = a.angularVelocity,
                    angularVelocity3D = a.angularVelocity3D,
                    axisOfRotation = a.axisOfRotation,
#if UNITY_5_5_OR_NEWER
                    remainingLifeTime = a.remainingLifetime,
#else
                    lifetime = a.lifetime,
#endif
                    position = a.position,
                    randomSeed = a.randomSeed,
                    rotation = a.rotation,
                    rotation3D = a.rotation3D,
                    startColor = a.startColor,
                    startLifetime = a.startLifetime,
                    startSize = a.startSize,
                    startSize3D = a.startSize3D,
                    velocity = a.velocity,
                };
            }

            public void ToUnity(ParticleSystem.Particle a)
            {
                a.angularVelocity = angularVelocity;
                a.angularVelocity3D = angularVelocity3D;
                a.axisOfRotation = axisOfRotation;
#if UNITY_5_5_OR_NEWER
                a.remainingLifetime = remainingLifeTime;
#else
                a.lifetime = lifetime;
#endif
                a.position = position;
                a.randomSeed = randomSeed;
                a.rotation = rotation;
                a.rotation3D = rotation3D;
                a.startColor = startColor;
                a.startLifetime = startLifetime;
                a.startSize = startSize;
                a.startSize3D = startSize3D;
                a.velocity = velocity;
            }
        }

        public struct RotationBySpeedModule
        {
            public bool enabled;
            public Vector2 range;
            public bool separateAxes;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator RotationBySpeedModule(ParticleSystem.RotationBySpeedModule a)
            {
                return new RotationBySpeedModule()
                {
                    enabled = a.enabled,
                    range = a.range,
                    separateAxes = a.separateAxes,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.RotationBySpeedModule a)
            {
                a.enabled = enabled;
                a.range = range;
                a.separateAxes = separateAxes;
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }

        public struct RotationOverLifetimeModule
        {
            public bool enabled;
            public bool separateAxes;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator RotationOverLifetimeModule(ParticleSystem.RotationOverLifetimeModule a)
            {
                return new RotationOverLifetimeModule()
                {
                    enabled = a.enabled,
                    separateAxes = a.separateAxes,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.RotationOverLifetimeModule a)
            {
                a.enabled = enabled;
                a.separateAxes = separateAxes;
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }

        public struct ShapeModule
        {
            public float angle;
            public float arc;
            public Vector3 box;
            public bool enabled;
            public float length;
            public string mesh;
            public int meshMaterialIndex;
            public ParticleSystemMeshShapeType meshShapeType;
            public float normalOffset;
            public float radius;
#if !UNITY_5_5_OR_NEWER
            public bool randomDirection;
#endif
            public ParticleSystemShapeType shapeType;
            public bool useMeshColors;
            public bool useMeshMaterialIndex;
#if UNITY_5_5_OR_NEWER
            public bool alignToDirection;
            public float meshScale;
            public float sphericalDirectionAmount;
            public float randomDirectionAmount;
#endif

            public static implicit operator ShapeModule(ParticleSystem.ShapeModule a)
            {
                if (a.mesh != null)
                    Debug.LogWarning("There is a mesh assigned, but we don't know the name of it in the mod package. We assume it's called '" + a.mesh.name + ".json' but you might want to change it!");

                return new ShapeModule()
                {
                    angle = a.angle,
                    arc = a.arc,
                    box = a.box,
                    enabled = a.enabled,
                    length = a.length,
                    mesh = (a.mesh != null ? a.mesh.name + ".json" : string.Empty),
                    meshMaterialIndex = a.meshMaterialIndex,
                    meshShapeType = a.meshShapeType,
                    normalOffset = a.normalOffset,
                    radius = a.radius,
#if !UNITY_5_5_OR_NEWER
                    randomDirection = a.randomDirection,
#endif
                    shapeType = a.shapeType,
                    useMeshColors = a.useMeshColors,
                    useMeshMaterialIndex = a.useMeshMaterialIndex,
#if UNITY_5_5_OR_NEWER
                    alignToDirection = a.alignToDirection,
                    meshScale = a.meshScale,
                    sphericalDirectionAmount = a.sphericalDirectionAmount,
                    randomDirectionAmount = a.randomDirectionAmount,
#endif
                };
            }

            public void ToUnity(ParticleSystem.ShapeModule a, GameObject go)
            {
                a.angle = angle;
                a.arc = arc;
                a.box = box;
                a.enabled = enabled;
                a.length = length;
                if (!string.IsNullOrEmpty(mesh))
                    a.mesh = ResourceManager.LoadMesh(mesh);
                a.meshRenderer = go.GetComponent<MeshRenderer>();
                a.meshMaterialIndex = meshMaterialIndex;
                a.meshShapeType = meshShapeType;
                a.normalOffset = normalOffset;
                a.radius = radius;
#if !UNITY_5_5_OR_NEWER
                a.randomDirection = randomDirection;
#endif
                a.shapeType = shapeType;
                a.useMeshColors = useMeshColors;
                a.useMeshMaterialIndex = useMeshMaterialIndex;
#if UNITY_5_5_OR_NEWER
                a.alignToDirection = alignToDirection;
                a.meshScale = meshScale;
                a.randomDirectionAmount = randomDirectionAmount;
                a.sphericalDirectionAmount = sphericalDirectionAmount;
#endif
            }
        }

        public struct SizeBySpeedModule
        {
            public bool enabled;
            public Vector2 range;
            public bool seperateAxes;
            public MinMaxCurve size;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float sizeMultiplier;
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator SizeBySpeedModule(ParticleSystem.SizeBySpeedModule a)
            {
                return new SizeBySpeedModule()
                {
                    enabled = a.enabled,
                    range = a.range,
                    seperateAxes = a.separateAxes,
                    size = a.size,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    sizeMultiplier = a.sizeMultiplier,
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.SizeBySpeedModule a)
            {
                a.enabled = enabled;
                a.range = range;
                a.separateAxes = seperateAxes;
                a.size = ParticleSystemHelpers.Convert(size);
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.sizeMultiplier = sizeMultiplier;
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }

        public struct SizeOverLifetimeModule
        {
            public bool enabled;
            public bool seperateAxes;
            public MinMaxCurve size;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float sizeMultiplier;
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator SizeOverLifetimeModule(ParticleSystem.SizeOverLifetimeModule a)
            {
                return new SizeOverLifetimeModule()
                {
                    enabled = a.enabled,
                    seperateAxes = a.separateAxes,
                    size = a.size,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    sizeMultiplier = a.sizeMultiplier,
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.SizeOverLifetimeModule a)
            {
                a.enabled = enabled;
                a.separateAxes = seperateAxes;
                a.size = ParticleSystemHelpers.Convert(size);
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.sizeMultiplier = sizeMultiplier;
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }

        public struct SubEmittersModule
        {
            public bool enabled;
#if UNITY_5_5_OR_NEWER
            public Emitter[] emitters;
#else
            public string birth0;
            public string birth1;
            public string collision0;
            public string collision1;
            public string death0;
            public string death1;
#endif

            public static implicit operator SubEmittersModule(ParticleSystem.SubEmittersModule a)
            {
                if (a.enabled)
                    Debug.LogWarning("Sub emitters are not supported for exporting, you should manually assign it in the exported file! See http://modenabler.greenzonegames.com/wiki/resources.particle-systems.html for more information");

                return new SubEmittersModule();
            }

            public void ToUnity(ParticleSystem.SubEmittersModule a, GameObject go)
            {
                a.enabled = enabled;
#if UNITY_5_5_OR_NEWER
                foreach (var item in emitters)
                {
                    a.AddSubEmitter(ResourceManager.LoadParticleSystem(item.name, go), item.type, item.properties);
                }
#else
                if (!string.IsNullOrEmpty(birth0))
                    a.birth0 = ResourceManager.LoadParticleSystem(birth0, go);
                if (!string.IsNullOrEmpty(birth1))
                    a.birth1 = ResourceManager.LoadParticleSystem(birth1, go);
                if (!string.IsNullOrEmpty(collision0))
                    a.collision0 = ResourceManager.LoadParticleSystem(collision0, go);
                if (!string.IsNullOrEmpty(collision1))
                    a.collision1 = ResourceManager.LoadParticleSystem(collision1, go);
                if (!string.IsNullOrEmpty(death0))
                    a.death0 = ResourceManager.LoadParticleSystem(death0, go);
                if (!string.IsNullOrEmpty(death1))
                    a.death1 = ResourceManager.LoadParticleSystem(death1, go);
#endif
            }

#if UNITY_5_5_OR_NEWER
            public struct Emitter
            {
                public string name;
                public ParticleSystemSubEmitterType type;
                public ParticleSystemSubEmitterProperties properties;
            }
#endif
        }

        public struct TextureSheetAnimationModule
        {
            public ParticleSystemAnimationType animation;
            public int cycleCount;
            public bool enabled;
            public MinMaxCurve frameOverTime;
            public int numTilesX;
            public int numTilesY;
            public int rowIndex;
            public bool useRandomRow;
#if UNITY_5_5_OR_NEWER
            public float flipU;
            public float flipV;
            public float frameOverTimeMultiplier;
            public ParticleSystem.MinMaxCurve startFrame;
            public float startFrameMultiplier;
            public UVChannelFlags uvChannelMask;
#endif

            public static implicit operator TextureSheetAnimationModule(ParticleSystem.TextureSheetAnimationModule a)
            {
                return new TextureSheetAnimationModule()
                {
                    animation = a.animation,
                    cycleCount = a.cycleCount,
                    enabled = a.enabled,
                    frameOverTime = a.frameOverTime,
                    numTilesX = a.numTilesX,
                    numTilesY = a.numTilesY,
                    rowIndex = a.rowIndex,
                    useRandomRow = a.useRandomRow,
#if UNITY_5_5_OR_NEWER
                    flipU = a.flipU,
                    flipV = a.flipV,
                    frameOverTimeMultiplier = a.frameOverTimeMultiplier,
                    startFrame = a.startFrame,
                    startFrameMultiplier = a.startFrameMultiplier,
                    uvChannelMask = a.uvChannelMask,
#endif
                };
            }

            public void ToUnity(ParticleSystem.TextureSheetAnimationModule a)
            {
                a.animation = animation;
                a.cycleCount = cycleCount;
                a.enabled = enabled;
                a.frameOverTime = ParticleSystemHelpers.Convert(frameOverTime);
                a.numTilesX = numTilesX;
                a.numTilesY = numTilesY;
                a.rowIndex = rowIndex;
                a.useRandomRow = useRandomRow;
#if UNITY_5_5_OR_NEWER
                a.flipU = flipU;
                a.flipV = flipV;
                a.frameOverTimeMultiplier = frameOverTimeMultiplier;
                a.startFrame = startFrame;
                a.startFrameMultiplier = startFrameMultiplier;
                a.uvChannelMask = uvChannelMask;
#endif
            }
        }

        public struct VelocityOverLifetimeModule
        {
            public bool enabled;
            public ParticleSystemSimulationSpace space;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;
#if UNITY_5_5_OR_NEWER
            public float xMultiplier;
            public float yMultiplier;
            public float zMultiplier;
#endif

            public static implicit operator VelocityOverLifetimeModule(ParticleSystem.VelocityOverLifetimeModule a)
            {
                return new VelocityOverLifetimeModule()
                {
                    enabled = a.enabled,
                    space = a.space,
                    x = a.x,
                    y = a.y,
                    z = a.z,
#if UNITY_5_5_OR_NEWER
                    xMultiplier = a.xMultiplier,
                    yMultiplier = a.yMultiplier,
                    zMultiplier = a.zMultiplier,
#endif
                };
            }

            public void ToUnity(ParticleSystem.VelocityOverLifetimeModule a)
            {
                a.enabled = enabled;
                a.space = space;
                a.x = ParticleSystemHelpers.Convert(x);
                a.y = ParticleSystemHelpers.Convert(y);
                a.z = ParticleSystemHelpers.Convert(z);
#if UNITY_5_5_OR_NEWER
                a.xMultiplier = xMultiplier;
                a.yMultiplier = yMultiplier;
                a.zMultiplier = zMultiplier;
#endif
            }
        }
    }

    public static class ParticleSystemHelpers
    {
        public static ParticleSystem.MinMaxCurve Convert(ParticleSystemData.MinMaxCurve b)
        {
            switch (b.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return new ParticleSystem.MinMaxCurve(b.constantMax);

                case ParticleSystemCurveMode.Curve:
                    return new ParticleSystem.MinMaxCurve(b.curveScalar, b.curveMax.ToUnity());

                case ParticleSystemCurveMode.TwoCurves:
                    return new ParticleSystem.MinMaxCurve(b.curveScalar, b.curveMin.ToUnity(), b.curveMax.ToUnity());

                case ParticleSystemCurveMode.TwoConstants:
                    return new ParticleSystem.MinMaxCurve(b.constantMin, b.constantMax);

                default:
                    return new ParticleSystem.MinMaxCurve();
            }
        }

        public static ParticleSystem.MinMaxGradient Convert(ParticleSystemData.MinMaxGradient b)
        {
            switch (b.mode)
            {
                case ParticleSystemGradientMode.Color:
                    return new ParticleSystem.MinMaxGradient(b.colorMax);

                case ParticleSystemGradientMode.Gradient:
                    return new ParticleSystem.MinMaxGradient(b.gradientMax);

                case ParticleSystemGradientMode.TwoColors:
                    return new ParticleSystem.MinMaxGradient(b.colorMin, b.colorMax);

                case ParticleSystemGradientMode.TwoGradients:
                    return new ParticleSystem.MinMaxGradient(b.gradientMin, b.gradientMax);

                default:
                    return new ParticleSystem.MinMaxGradient();
            }
        }
    }
}