using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public static class ColorExtensions
    {
        // Metodo per verificare se il colore è molto simile al bianco
        public static bool IsVerySimilarToWhite(this Color color, float threshold = 0.1f)
        {
            return color.r >= (1 - threshold) && color.g >= (1 - threshold) && color.b >= (1 - threshold);
        }


        public static void NameToColor(this Color color, string colorName)
        {
            if (string.IsNullOrEmpty(colorName))
                return;
            switch (colorName.ToLower())
            {
                case "white":
                    color = Color.white;
                    break;
                case "black":
                    color = Color.black;
                    break;
                case "red":
                    color = Color.red;
                    break;
                case "green":
                    color = Color.green;
                    break;
                case "blue":
                    color = Color.blue;
                    break;
                case "yellow":
                    color = Color.yellow;
                    break;
                case "orange":
                    color = new Color(1.0f, 0.5f, 0.0f); // Arancione
                    break;
                case "purple":
                    color = new Color(0.5f, 0.0f, 0.5f); // Viola
                    break;
                case "pink":
                    color = new Color(1.0f, 0.41f, 0.71f); // Rosa
                    break;
                case "brown":
                    color = new Color(0.59f, 0.29f, 0.0f); // Marrone
                    break;
                default:
                    Debug.LogError("Nome colore sconosciuto: " + colorName);
                    color = Color.white; // Ritorna il colore bianco in caso di nome sconosciuto
                    break;
            }
        }
    }
}
