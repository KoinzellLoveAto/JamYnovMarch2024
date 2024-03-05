using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaExtension.ColorExtension
{

    public class ColorExtension
    {
        public Color GetRndColor()
        {
            Color rnd = new Color();
            rnd.r = Random.Range(0, 255f);
            rnd.g = Random.Range(0, 255f);
            rnd.b = Random.Range(0, 255f);
            return rnd;
        }

    }
}
