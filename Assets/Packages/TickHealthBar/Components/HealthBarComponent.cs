// Copyright (c) 2018 Maxim Tiourin
// Please direct any bug reports/feedback to maximtiourin@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fizzik {
    /*
     * Author - Maxim Tiourin (maximtiourin@gmail.com)
     *
     * The Healthbar Component can be attached to a Game Object with a UnityEngine UI image, and it manipulates the image's attached HealthBarMaterial
     * to update the underlying shader with the values set on the component.
     */
    [ExecuteInEditMode]
    public class HealthBarComponent : MonoBehaviour {
        public UnityEngine.UI.Image image;
        public int healthPerTick = 1;
        public int healthPerBigTick = 3; 
        public int currentHealth = 3;
        public int totalHealth = 6;
        public float tickWidth = 1f;
        public float bigTickWidth = 2f;
        public float borderWidth = 1f;
        [Range(0,1)]
        public float tickHeightPercent = 1f;
        [Range(0, 1)]
        public float bigTickHeightPercent = 1f;
        public Color healthColor = Color.green;
        public Color damageColor = Color.red;
        public Color tickColor = Color.black;
        public Color bigTickColor = Color.black;
        public Color borderColor = Color.blue;
        public bool borderEnabled = true;
        public bool tickEnabled = true;
        public bool bigTickEnabled = false;

        public bool InstantiatedMaterial { get; set; } //Whether or not we have instantiated a copy of the image material so that modifications to it are not shared between all HealthBarMaterials

        void Update() {
            if (image == null) {
                //Assign the Image component if we don't already have one assigned
                image = transform.GetComponent<UnityEngine.UI.Image>();
            }
            else {
                if (image.material != null) {
                    //Make an instance of the image material if we haven't yet
                    if (!InstantiatedMaterial) {
                        Material mat = Instantiate(image.material);
                        image.material = mat;

                        InstantiatedMaterial = true;
                    }

                    //Set material properties
                    image.material.SetFloat("_HealthCurrent", currentHealth);

                    if (totalHealth > 0) {
                        image.material.SetFloat("_HealthTotal", totalHealth);
                    }

                    image.material.SetFloat("_HealthPerTick", healthPerTick);
                    image.material.SetFloat("_HealthPerBigTick", healthPerBigTick);

                    //Health Bar Width/Height
                    image.material.SetVector("_HealthbarSize", new Vector4(image.rectTransform.rect.width, image.rectTransform.rect.height, 0, 0));

                    //Enabled
                    image.material.SetFloat("_EnabledBorder", (borderEnabled) ? (1) : (0));
                    image.material.SetFloat("_EnabledTick", (tickEnabled) ? (1) : (0));
                    image.material.SetFloat("_EnabledBigTick", (bigTickEnabled) ? (1) : (0));

                    //Widths
                    image.material.SetFloat("_WidthTick", tickWidth);
                    image.material.SetFloat("_WidthBigTick", bigTickWidth);
                    image.material.SetFloat("_WidthBorder", borderWidth);

                    //Heights
                    image.material.SetFloat("_HeightPercentTick", tickHeightPercent);
                    image.material.SetFloat("_HeightPercentBigTick", bigTickHeightPercent);

                    //Colors
                    image.material.SetColor("_ColorHealth", healthColor);
                    image.material.SetColor("_ColorDamage", damageColor);
                    image.material.SetColor("_ColorTick", tickColor);
                    image.material.SetColor("_ColorBigTick", bigTickColor);
                    image.material.SetColor("_ColorBorder", borderColor);
                }
            }
        }
    }
}
