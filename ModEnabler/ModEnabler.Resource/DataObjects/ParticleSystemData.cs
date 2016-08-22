using System;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    [Serializable]
    public struct ParticleSystemData
    {
        public CollisionModule collision;
        public ColorBySpeedModule colorBySpeed;
        public ColorOverLifetimeModule colorOverLifetime;
        public EmissionModule emission;
        public ExternalForcesModule externalForces;
        public ForceOverLifetimeModule forceOverLifetime;
        public float gravityModifier;
        public InheritVelocityModule inheritVelocity;
        public LimitVelocityOverLifetimeModule limitVelocityOverLifetime;
        public bool loop;
        public int maxParticles;
        public float playbackSpeed;
        public bool playOnAwake;
        public uint randomSeed;
        public RotationBySpeedModule rotationBySpeed;
        public RotationOverLifetimeModule rotationOverLifetime;
        public ParticleSystemScalingMode scalingMode;
        public ShapeModule shape;
        public ParticleSystemSimulationSpace simulationSpace;
        public SizeBySpeedModule sizeBySpeed;
        public SizeOverLifetimeModule sizeOverLifetime;
        public Color startColor;
        public float startDelay;
        public float startLifetime;
        public float startRotation;
        public Vector3 startRotation3D;
        public float startSize;
        public float startSpeed;
        public SubEmittersModule subEmitters;
        public TextureSheetAnimationModule textureSheetAnimation;
        public float time;
        public VelocityOverLifetimeModule velocityOverLifetime;
        public Renderer renderer;

        public ParticleSystemData(ParticleSystem ps)
        {
            collision = ps.collision;
            colorBySpeed = ps.colorBySpeed;
            colorOverLifetime = ps.colorOverLifetime;
            emission = ps.emission;
            externalForces = ps.externalForces;
            forceOverLifetime = ps.forceOverLifetime;
            gravityModifier = ps.gravityModifier;
            inheritVelocity = ps.inheritVelocity;
            limitVelocityOverLifetime = ps.limitVelocityOverLifetime;
            loop = ps.loop;
            maxParticles = ps.maxParticles;
            playbackSpeed = ps.playbackSpeed;
            playOnAwake = ps.playOnAwake;
            randomSeed = ps.randomSeed;
            rotationBySpeed = ps.rotationBySpeed;
            rotationOverLifetime = ps.rotationOverLifetime;
            scalingMode = ps.scalingMode;
            shape = ps.shape;
            simulationSpace = ps.simulationSpace;
            sizeBySpeed = ps.sizeBySpeed;
            sizeOverLifetime = ps.sizeOverLifetime;
            startColor = ps.startColor;
            startDelay = ps.startDelay;
            startLifetime = ps.startLifetime;
            startRotation = ps.startRotation;
            startRotation3D = ps.startRotation3D;
            startSize = ps.startSize;
            startSpeed = ps.startSpeed;
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

            var a = ps.collision;
            a.enabled = collision.enabled;
            a.bounce = Helpers.Convert(collision.bounce);
            a.collidesWith = collision.collidesWith;
            a.dampen = Helpers.Convert(collision.dampen);
            a.enableDynamicColliders = collision.enableDynamicColliders;
            a.enableInteriorCollisions = collision.enableInteriorCollisions;
            a.lifetimeLoss = Helpers.Convert(collision.lifetimeLoss);
            a.maxCollisionShapes = collision.maxCollisionShapes;
            a.minKillSpeed = collision.minKillSpeed;
            a.mode = collision.mode;
            a.quality = collision.quality;
            a.radiusScale = collision.radiusScale;
            a.sendCollisionMessages = collision.sendCollisionMessages;
            a.type = collision.type;
            a.voxelSize = collision.voxelSize;
            a.radiusScale = collision.radiusScale;
            a.radiusScale = collision.radiusScale;

            var b = ps.colorBySpeed;
            b.enabled = colorBySpeed.enabled;
            b.color = Helpers.Convert(colorBySpeed.color);
            b.range = colorBySpeed.range;

            var c = ps.colorOverLifetime;
            c.enabled = colorOverLifetime.enabled;
            c.color = Helpers.Convert(colorOverLifetime.color);

            var d = ps.emission;
            d.enabled = emission.enabled;
            d.rate = Helpers.Convert(emission.rate);
            d.type = emission.type;

            var e = ps.externalForces;
            e.enabled = externalForces.enabled;
            e.multiplier = externalForces.multiplier;

            var f = ps.forceOverLifetime;
            f.enabled = forceOverLifetime.enabled;
            f.randomized = forceOverLifetime.randomized;
            f.space = forceOverLifetime.space;
            f.x = Helpers.Convert(forceOverLifetime.x);
            f.y = Helpers.Convert(forceOverLifetime.y);
            f.z = Helpers.Convert(forceOverLifetime.z);
            ps.gravityModifier = gravityModifier;

            var g = ps.inheritVelocity;
            g.enabled = inheritVelocity.enabled;
            g.curve = Helpers.Convert(inheritVelocity.curve);
            g.mode = inheritVelocity.mode;

            var h = ps.limitVelocityOverLifetime;
            h.enabled = limitVelocityOverLifetime.enabled;
            h.dampen = limitVelocityOverLifetime.dampen;
            h.limit = Helpers.Convert(limitVelocityOverLifetime.limit);
            h.limitX = Helpers.Convert(limitVelocityOverLifetime.limitX);
            h.limitY = Helpers.Convert(limitVelocityOverLifetime.limitY);
            h.limitZ = Helpers.Convert(limitVelocityOverLifetime.limitZ);
            h.separateAxes = limitVelocityOverLifetime.separateAxes;
            h.space = limitVelocityOverLifetime.space;

            ps.loop = loop;
            ps.maxParticles = maxParticles;
            ps.playbackSpeed = playbackSpeed;
            ps.playOnAwake = playOnAwake;
            ps.randomSeed = randomSeed;

            var i = ps.rotationBySpeed;
            i.enabled = rotationBySpeed.enabled;
            i.range = rotationBySpeed.range;
            i.separateAxes = rotationBySpeed.separateAxes;
            i.x = Helpers.Convert(rotationBySpeed.x);
            i.y = Helpers.Convert(rotationBySpeed.y);
            i.z = Helpers.Convert(rotationBySpeed.z);

            var j = ps.rotationOverLifetime;
            j.enabled = rotationOverLifetime.enabled;
            j.separateAxes = rotationOverLifetime.separateAxes;
            j.x = Helpers.Convert(rotationOverLifetime.x);
            j.y = Helpers.Convert(rotationOverLifetime.y);
            j.z = Helpers.Convert(rotationOverLifetime.z);

            ps.scalingMode = scalingMode;

            var k = ps.shape;
            k.enabled = shape.enabled;
            k.angle = shape.angle;
            k.arc = shape.arc;
            k.box = shape.box;
            k.length = shape.length;
            if (!string.IsNullOrEmpty(shape.mesh))
                k.mesh = ResourceManager.LoadMesh(shape.mesh);
            k.meshRenderer = go.GetComponent<MeshRenderer>();
            k.meshShapeType = shape.meshShapeType;
            k.normalOffset = shape.normalOffset;
            k.radius = shape.radius;
            k.randomDirection = shape.randomDirection;
            k.shapeType = shape.shapeType;
            k.useMeshColors = shape.useMeshColors;
            k.useMeshMaterialIndex = shape.useMeshMaterialIndex;

            ps.simulationSpace = simulationSpace;

            var l = ps.sizeBySpeed;
            l.enabled = sizeBySpeed.enabled;
            l.range = sizeBySpeed.range;
            l.size = Helpers.Convert(sizeBySpeed.size);

            var m = ps.sizeOverLifetime;
            m.enabled = sizeOverLifetime.enabled;
            m.size = Helpers.Convert(sizeOverLifetime.size);

            ps.startColor = startColor;
            ps.startDelay = startDelay;
            ps.startLifetime = startLifetime;
            ps.startRotation = startRotation;
            ps.startRotation3D = startRotation3D;
            ps.startSize = startSize;
            ps.startSpeed = startSpeed;

            var n = ps.subEmitters;
            if (!string.IsNullOrEmpty(subEmitters.birth0))
                n.birth0 = ResourceManager.LoadParticleSystem(subEmitters.birth0, go);
            if (!string.IsNullOrEmpty(subEmitters.birth1))
                n.birth1 = ResourceManager.LoadParticleSystem(subEmitters.birth1, go);
            if (!string.IsNullOrEmpty(subEmitters.collision0))
                n.collision0 = ResourceManager.LoadParticleSystem(subEmitters.collision0, go);
            if (!string.IsNullOrEmpty(subEmitters.collision1))
                n.collision1 = ResourceManager.LoadParticleSystem(subEmitters.collision1, go);
            if (!string.IsNullOrEmpty(subEmitters.death0))
                n.death0 = ResourceManager.LoadParticleSystem(subEmitters.death0, go);
            if (!string.IsNullOrEmpty(subEmitters.death1))
                n.death1 = ResourceManager.LoadParticleSystem(subEmitters.death1, go);
            n.enabled = subEmitters.enabled;

            var o = ps.textureSheetAnimation;
            o.enabled = textureSheetAnimation.enabled;
            o.animation = textureSheetAnimation.animation;
            o.cycleCount = textureSheetAnimation.cycleCount;
            o.frameOverTime = Helpers.Convert(textureSheetAnimation.frameOverTime);
            o.numTilesX = textureSheetAnimation.numTilesX;
            o.numTilesY = textureSheetAnimation.numTilesY;
            o.rowIndex = textureSheetAnimation.rowIndex;
            o.useRandomRow = textureSheetAnimation.useRandomRow;

            ps.time = time;

            var p = ps.velocityOverLifetime;
            p.enabled = velocityOverLifetime.enabled;
            p.space = velocityOverLifetime.space;
            p.x = Helpers.Convert(velocityOverLifetime.x);
            p.y = Helpers.Convert(velocityOverLifetime.y);
            p.z = Helpers.Convert(velocityOverLifetime.z);

            var pr = ps.GetComponent<ParticleSystemRenderer>();
            pr.renderMode = renderer.renderMode;
            pr.normalDirection = renderer.normalDirection;
            if (!string.IsNullOrEmpty(renderer.material))
                pr.material = ResourceManager.LoadMaterial(renderer.material);
            if (!string.IsNullOrEmpty(renderer.mesh))
                pr.mesh = ResourceManager.LoadMesh(renderer.mesh);
            pr.sortMode = renderer.sortMode;
            pr.sortingFudge = renderer.sortingFudge;
            pr.shadowCastingMode = renderer.shadowCastingMode;
            pr.receiveShadows = renderer.receiveShadows;
            pr.minParticleSize = renderer.minParticleSize;
            pr.maxParticleSize = renderer.maxParticleSize;
            pr.sortingLayerName = renderer.sortingLayerName;
            pr.sortingOrder = renderer.sortingOrder;
            pr.alignment = renderer.alignment;
            pr.pivot = renderer.pivot;

            return ps;
        }

        public struct Renderer
        {
            public ParticleSystemRenderMode renderMode;
            public float normalDirection;
            public string material;
            public string mesh;
            public ParticleSystemSortMode sortMode;
            public float sortingFudge;
            public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
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
        }

        public struct EmissionModule
        {
            public bool enabled;
            public MinMaxCurve rate;
            public ParticleSystemEmissionType type;

            public static implicit operator EmissionModule(ParticleSystem.EmissionModule a)
            {
                return new EmissionModule()
                {
                    enabled = a.enabled,
                    rate = a.rate,
                    type = a.type
                };
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
                    velocity = a.velocity
                };
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
        }

        public struct ForceOverLifetimeModule
        {
            public bool enabled;
            public bool randomized;
            public ParticleSystemSimulationSpace space;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;

            public static implicit operator ForceOverLifetimeModule(ParticleSystem.ForceOverLifetimeModule a)
            {
                return new ForceOverLifetimeModule()
                {
                    enabled = a.enabled,
                    randomized = a.randomized,
                    space = a.space,
                    x = a.x,
                    y = a.y,
                    z = a.z
                };
            }
        }

        public struct InheritVelocityModule
        {
            public MinMaxCurve curve;
            public bool enabled;
            public ParticleSystemInheritVelocityMode mode;

            public static implicit operator InheritVelocityModule(ParticleSystem.InheritVelocityModule a)
            {
                return new InheritVelocityModule()
                {
                    curve = a.curve,
                    enabled = a.enabled,
                    mode = a.mode
                };
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
                    separateAxes = a.separateAxes,
                    space = a.space
                };
            }
        }

        public struct MinMaxCurve
        {
            public float constantMax;
            public float constantMin;
            public AnimCurve curveMax;
            public AnimCurve curveMin;
            public float curveScalar;
            public ParticleSystemCurveMode mode;

            public static implicit operator MinMaxCurve(ParticleSystem.MinMaxCurve a)
            {
                return new MinMaxCurve()
                {
                    constantMax = a.constantMax,
                    constantMin = a.constantMin,
                    curveMax = a.curveMax,
                    curveMin = a.curveMin,
                    curveScalar = a.curveScalar,
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
            public float lifetime;
            public Vector3 position;
            public uint randomSeed;
            public float rotation;
            public Vector3 rotation3D;
            public Color32 startColor;
            public float startLifetime;
            public float startSize;
            public Vector3 velocity;

            public static implicit operator Particle(ParticleSystem.Particle a)
            {
                return new Particle()
                {
                    angularVelocity = a.angularVelocity,
                    angularVelocity3D = a.angularVelocity3D,
                    axisOfRotation = a.axisOfRotation,
                    lifetime = a.lifetime,
                    position = a.position,
                    randomSeed = a.randomSeed,
                    rotation = a.rotation,
                    rotation3D = a.rotation3D,
                    startColor = a.startColor,
                    startLifetime = a.startLifetime,
                    startSize = a.startSize,
                    velocity = a.velocity
                };
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

            public static implicit operator RotationBySpeedModule(ParticleSystem.RotationBySpeedModule a)
            {
                return new RotationBySpeedModule()
                {
                    enabled = a.enabled,
                    range = a.range,
                    separateAxes = a.separateAxes,
                    x = a.x,
                    y = a.y,
                    z = a.z
                };
            }
        }

        public struct RotationOverLifetimeModule
        {
            public bool enabled;
            public bool separateAxes;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;

            public static implicit operator RotationOverLifetimeModule(ParticleSystem.RotationOverLifetimeModule a)
            {
                return new RotationOverLifetimeModule()
                {
                    enabled = a.enabled,
                    separateAxes = a.separateAxes,
                    x = a.x,
                    y = a.y,
                    z = a.z
                };
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
            public MeshRenderer meshRenderer;
            public ParticleSystemMeshShapeType meshShapeType;
            public float normalOffset;
            public float radius;
            public bool randomDirection;
            public ParticleSystemShapeType shapeType;
            public SkinnedMeshRenderer skinnedMeshRenderer;
            public bool useMeshColors;
            public bool useMeshMaterialIndex;

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
                    meshRenderer = a.meshRenderer,
                    meshShapeType = a.meshShapeType,
                    normalOffset = a.normalOffset,
                    radius = a.radius,
                    randomDirection = a.randomDirection,
                    shapeType = a.shapeType,
                    skinnedMeshRenderer = a.skinnedMeshRenderer,
                    useMeshColors = a.useMeshColors,
                    useMeshMaterialIndex = a.useMeshMaterialIndex
                };
            }
        }

        public struct SizeBySpeedModule
        {
            public bool enabled;
            public Vector2 range;
            public MinMaxCurve size;

            public static implicit operator SizeBySpeedModule(ParticleSystem.SizeBySpeedModule a)
            {
                return new SizeBySpeedModule()
                {
                    enabled = a.enabled,
                    range = a.range,
                    size = a.size
                };
            }
        }

        public struct SizeOverLifetimeModule
        {
            public bool enabled;
            public MinMaxCurve size;

            public static implicit operator SizeOverLifetimeModule(ParticleSystem.SizeOverLifetimeModule a)
            {
                return new SizeOverLifetimeModule()
                {
                    enabled = a.enabled,
                    size = a.size
                };
            }
        }

        public struct SubEmittersModule
        {
            public string birth0;
            public string birth1;
            public string collision0;
            public string collision1;
            public string death0;
            public string death1;
            public bool enabled;

            public static implicit operator SubEmittersModule(ParticleSystem.SubEmittersModule a)
            {
                if (a.enabled)
                    Debug.LogWarning("Sub emitters are not supported for exporting, you should manually assing it in the exported file!");

                return new SubEmittersModule();
            }
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
                    useRandomRow = a.useRandomRow
                };
            }
        }

        public struct VelocityOverLifetimeModule
        {
            public bool enabled;
            public ParticleSystemSimulationSpace space;
            public MinMaxCurve x;
            public MinMaxCurve y;
            public MinMaxCurve z;

            public static implicit operator VelocityOverLifetimeModule(ParticleSystem.VelocityOverLifetimeModule a)
            {
                return new VelocityOverLifetimeModule()
                {
                    enabled = a.enabled,
                    space = a.space,
                    x = a.x,
                    y = a.y,
                    z = a.z
                };
            }
        }

        public struct AnimCurve
        {
            public KeyFrame[] keys;

            public static implicit operator AnimationCurve(AnimCurve a)
            {
                if (a.keys == null)
                    return null;

                AnimationCurve b = new AnimationCurve();
                foreach (var item in a.keys)
                    b.AddKey(item);

                return b;
            }

            public static implicit operator AnimCurve(AnimationCurve a)
            {
                AnimCurve b = new AnimCurve();
                if (a != null && a.keys != null && a.keys.Length > 0)
                {
                    b.keys = new KeyFrame[a.length];
                    for (int i = 0; i < b.keys.Length; i++)
                        b.keys[i] = a.keys[i];
                }

                return b;
            }
        }

        public struct KeyFrame
        {
            public float time;
            public float value;
            public int tangentMode;
            public float inTangent;
            public float outTangent;

            public static implicit operator UnityEngine.Keyframe(KeyFrame a)
            {
                return new Keyframe
                {
                    inTangent = a.inTangent,
                    outTangent = a.outTangent,
                    tangentMode = a.tangentMode,
                    time = a.time,
                    value = a.value
                };
            }

            public static implicit operator KeyFrame(UnityEngine.Keyframe a)
            {
                return new KeyFrame
                {
                    inTangent = a.inTangent,
                    outTangent = a.outTangent,
                    tangentMode = a.tangentMode,
                    time = a.time,
                    value = a.value
                };
            }
        }
    }

    public static class Helpers
    {
        public static ParticleSystem.MinMaxCurve Convert(ParticleSystemData.MinMaxCurve b)
        {
            switch (b.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    return new ParticleSystem.MinMaxCurve(b.constantMax);

                case ParticleSystemCurveMode.Curve:
                    return new ParticleSystem.MinMaxCurve(b.curveScalar, b.curveMax);

                case ParticleSystemCurveMode.TwoCurves:
                    return new ParticleSystem.MinMaxCurve(b.curveScalar, b.curveMin, b.curveMax);

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