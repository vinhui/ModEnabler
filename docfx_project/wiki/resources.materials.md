---
uid: resources.materials.md
title: Materials
---

# Materials

Make sure the shader is included into the game build. To make sure it is included, do one of the following.

1.  Reference it in a [material] used in one of your scenes
2.  Add it under "Always Included Shaders" list in ProjectSettings/Graphics
3.  Put the shader or something that references it (e.g. a Material) into a "Resources" folder

Required material properties:

-   [Shader]

Optional material properties:

-   color ([Color])
-   mainTexture (string, path to the texture)
-   mainTextureOffset ([Vector2])
-   mainTextureScale ([Vector2])
-   renderQueue (int)
-   shaderKeywords (string array)
-   globalIlluminationFlags (int)
-   enableKeywords (string array)
-   disableKeywords (string array)
-   shaderProperties (ShaderProperty array)

The ShaderProperty object has 3 properties:

-   name (name of the property, e.g.: '\_MainTex')
-   type (the type of the object, e.g.: 'texture')
-   value

The [property name] is the name used in the shader, for example '\_MainTex' in the Standard shader to set the Albedo of the material. The type defines what type the value is. The following types are available:

-   color
-   float
-   int
-   matrix
-   texture
-   textureOffset
-   textureScale
-   vector

Some of the values need to be parsed since the value property is a string. The formats for these types are:

### Color

`r,g,b,a` or `r,g,b`
(the alpha is optional)

### Matrix

`pos:x,y,x; rot:x,y,z,w; scale:x,y,z`

### TextureOffset & TextureScale

`x,y`

### Vector

`x,y,z,w`

*The r/g/b/a/x/y/z/w values all need to be floats.*

An example of a material:

``` json
{
    "shader" : "Standard",
    "color" : {
        "r" : 1.0,
        "g" : 1.0,
        "b" : 1.0,
        "a" : 1.0
    },
    "mainTextureOffset" : {
        "x" : 0.0,
        "y" : 0.0
    },
    "mainTextureScale" : {
        "x" : 1.0,
        "y" : 1.0
    },
    "passCount" : 6,
    "renderQueue" : 2000,
    "shaderKeywords" : [],
    "globalIlluminationFlags" : 5,
    "shaderProperties" : [{
            "name" : "_Metallic",
            "type" : "float",
            "value" : "0"
        }, {
            "name" : "_Glossiness",
            "type" : "float",
            "value" : ".25"
        }, {
            "name" : "_MainTex",
            "type" : "texture",
            "value" : "bricks.png"
        }
    ]
}
```

  [material]: https://docs.unity3d.com/ScriptReference/Material.html
  [Shader]: http://docs.unity3d.com/ScriptReference/Material-shader.html
  [Color]: http://docs.unity3d.com/ScriptReference/Color.html
  [Vector2]: http://docs.unity3d.com/ScriptReference/Vector2.html
  [property name]: http://docs.unity3d.com/Manual/SL-Properties.html