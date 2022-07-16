/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2021 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

using UnityEngine;
namespace Tegridy.Effects
{
    public class Lightning : MonoBehaviour
    {
        public float randomness;
        public LineRenderer linePrefab;
        public Transform[] lineTranforms;

        private Vector3[] points;
        private LineRenderer lRend;
        private readonly string mainTexture = "_MainTex";
        private Vector2 mainTextureScale = Vector2.one;
        private Vector2 mainTextureOffset = Vector2.one;

        private void Start()
        {
            GameObject gameObject = Instantiate(linePrefab.gameObject);
            gameObject.transform.parent = this.transform;
            lRend = gameObject.GetComponent<LineRenderer>();
            lRend.gameObject.SetActive(true);

            points = new Vector3[lineTranforms.Length];
            lRend.positionCount = lineTranforms.Length;
        }

        private void FixedUpdate()
        {
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            float distance = Vector3.Distance(lineTranforms[0].position, lineTranforms[lineTranforms.Length - 1].position) / points.Length;

            mainTextureScale.x = distance;
            mainTextureOffset.x = Random.Range(-randomness, randomness);
            lRend.material.SetTextureScale(mainTexture, mainTextureScale);
            lRend.material.SetTextureOffset(mainTexture, mainTextureOffset);

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = lineTranforms[i].position;
                if (i != 0 && i != points.Length - 1)
                {
                    points[i].x += Random.Range(-randomness, randomness);
                    points[i].y += Random.Range(-randomness, randomness);
                    points[i].z += Random.Range(-randomness, randomness);
                }
            }
            lRend.SetPositions(points);
        }
    }
}