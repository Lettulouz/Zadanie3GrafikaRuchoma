using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    internal class Planet
    {
        float angle = 0.0f;
        float angleLuna = 0.0f;
        float _planetOrbitRadius = 0.0f;
        float _lunaOrbitRadius = 0.0f;
        float _rotationSpeedAroundSun = 0.0f;
        float _rotationSpeedOnOwnAxis = 0.0f;
        float _lunaRotationSpeedAroundPlanet = 0.0f;
        float _lunaRotationSpeedOnOwnAxis = 0.0f;
        private float _center;
        Vector3 _planetOrbitPosition;

        public Planet(float planetOrbitRadius, float rotationSpeedAroundSun, float rotationSpeedOnOwnAxis)
        {
            _center = planetOrbitRadius;
            _planetOrbitRadius = planetOrbitRadius;
            _rotationSpeedAroundSun = rotationSpeedAroundSun;
            _rotationSpeedOnOwnAxis = rotationSpeedOnOwnAxis;
        }

        public Planet(float planetOrbitRadius, float lunaOrbitRadius, float rotationSpeedAroundSun, float rotationSpeedOnOwnAxis, float lunaRotationSpeedAroundPlanet, float lunaRotationSpeedOnOwnAxis)
        {
            _center = planetOrbitRadius;
            _lunaOrbitRadius = lunaOrbitRadius;
            _planetOrbitRadius = planetOrbitRadius;
            _rotationSpeedAroundSun = rotationSpeedAroundSun;
            _rotationSpeedOnOwnAxis = rotationSpeedOnOwnAxis;
            _lunaRotationSpeedAroundPlanet = lunaRotationSpeedAroundPlanet;
            _lunaRotationSpeedOnOwnAxis = lunaRotationSpeedOnOwnAxis;
        }

        public VertexPositionColor[] generatePlanet(float dist, float size, Color[] colors)
        {
            VertexPositionColor[] planet = new VertexPositionColor[36];
            planet = new VertexPositionColor[36];
            planet[0] = new VertexPositionColor(new Vector3(- size*1, size*-1, size * 1), colors[0]);
            planet[1] = new VertexPositionColor(new Vector3(- size*1, size*1, size * 1), colors[1]);
            planet[2] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size * 1), colors[1]);
            planet[3] = new VertexPositionColor(new Vector3(- size*1, size*1, size * 1), colors[1]);
            planet[4] = new VertexPositionColor(new Vector3(+ size*1, size*1, size * 1), colors[0]);
            planet[5] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size * 1), colors[1]);
                                                                           
            planet[6] = new VertexPositionColor(new Vector3(- size*1, size*1, size * -1), colors[0]);
            planet[7] = new VertexPositionColor(new Vector3(- size*1, size*1, size * 1), colors[1]);
            planet[8] = new VertexPositionColor(new Vector3(+ size*1, size*1, size * -1), colors[1]);
            planet[9] = new VertexPositionColor(new Vector3(- size*1, size*1, size * 1), colors[1]);
            planet[10] = new VertexPositionColor(new Vector3(+ size * 1, size * 1, size * 1), colors[0]);
            planet[11] = new VertexPositionColor(new Vector3(+ size * 1, size * 1, size * -1), colors[1]);

            planet[12] = new VertexPositionColor(new Vector3(- size*1, size*-1,size* -1), colors[0]);
            planet[13] = new VertexPositionColor(new Vector3(- size*1, size*1, size*-1), colors[1]);
            planet[14] = new VertexPositionColor(new Vector3(+ size*1, size*-1,size* -1), colors[1]);
            planet[15] = new VertexPositionColor(new Vector3(- size*1, size*1, size * -1), colors[1]);
            planet[16] = new VertexPositionColor(new Vector3(+ size*1, size * 1, size * -1), colors[0]);
            planet[17] = new VertexPositionColor(new Vector3(+ size * 1, size * -1, size * -1), colors[1]);
                                                             
            planet[18] = new VertexPositionColor(new Vector3(- size*1, size*-1, size*-1), colors[0]);
            planet[19] = new VertexPositionColor(new Vector3(- size*1, size*-1, size*1), colors[1]);
            planet[20] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size*-1), colors[1]);
            planet[21] = new VertexPositionColor(new Vector3(- size*1, size*-1, size * 1), colors[1]);
            planet[22] = new VertexPositionColor(new Vector3(+ size*1, size * -1, size * 1), colors[0]);
            planet[23] = new VertexPositionColor(new Vector3(+ size * 1, size * -1, size * -1), colors[1]);
                                                             
            planet[24] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size*-1), colors[0]);
            planet[25] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size*1), colors[1]);
            planet[26] = new VertexPositionColor(new Vector3(+ size*1, size*1, -size*1), colors[1]);
            planet[27] = new VertexPositionColor(new Vector3(+ size*1, size*-1, size * 1), colors[1]);
            planet[28] = new VertexPositionColor(new Vector3(+ size*1, size * 1, size * 1), colors[0]);
            planet[29] = new VertexPositionColor(new Vector3(+ size * 1, size * 1, size * -1), colors[1]);
                                                             
            planet[30] = new VertexPositionColor(new Vector3(- size*1, size*-1, size * -1), colors[0]);
            planet[31] = new VertexPositionColor(new Vector3(- size*1, size*-1,size* 1), colors[1]);
            planet[32] = new VertexPositionColor(new Vector3(- size*1, size*1, size*-1), colors[1]);
            planet[33] = new VertexPositionColor(new Vector3(- size*1, size*-1, size * 1), colors[1]);
            planet[34] = new VertexPositionColor(new Vector3(- size*1, size * 1, size * 1), colors[0]);
            planet[35] = new VertexPositionColor(new Vector3(- size * 1, size * 1, size * -1), colors[1]);
            return planet;
        }

        public void ManagePlanet(ref Matrix planet) {

            Vector3 center = new Vector3(_center, 0, 0);
            Matrix translationToCenter = Matrix.CreateTranslation(-center);
            Matrix translationFromCenter = Matrix.CreateTranslation(center);

            Matrix rotationOnOwnAxis = Matrix.CreateRotationY(MathHelper.ToRadians(_rotationSpeedOnOwnAxis));
            planet = Matrix.Multiply(translationToCenter, planet);
            planet = Matrix.Multiply(rotationOnOwnAxis, planet); 
            planet = Matrix.Multiply(translationFromCenter, planet); 

            _planetOrbitPosition = new Vector3(
                _planetOrbitRadius * (float)Math.Cos(angle),
                0,
                _planetOrbitRadius * (float)Math.Sin(angle)
            );

            angle -= MathHelper.ToRadians(_rotationSpeedAroundSun); 

            planet.Translation = _planetOrbitPosition;

            Matrix rotationAroundSun = Matrix.CreateRotationY(MathHelper.ToRadians(_rotationSpeedAroundSun));
            planet = Matrix.Multiply(rotationAroundSun, planet);

            rotationAroundSun = Matrix.CreateRotationX(MathHelper.ToRadians(_rotationSpeedAroundSun));
            planet = Matrix.Multiply(rotationAroundSun, planet);
        }

        public void ManageLuna(ref Matrix luna, Matrix planet)
        {
            Vector3 planetCenter = new Vector3(planet.M41, planet.M42, planet.M43);
        
            Matrix translationToPlanetCenter = Matrix.CreateTranslation(-planetCenter);

            Matrix translationFromPlanetCenter = Matrix.CreateTranslation(planetCenter);

            Matrix rotationOnOwnAxis = Matrix.CreateRotationY(MathHelper.ToRadians(_lunaRotationSpeedOnOwnAxis));

            luna = Matrix.Multiply(translationToPlanetCenter, luna);
            luna = Matrix.Multiply(rotationOnOwnAxis, luna);
            luna = Matrix.Multiply(translationFromPlanetCenter, luna);

            Vector3 orbitPosition = new Vector3(
                _lunaOrbitRadius * (float)Math.Cos(angleLuna),
                0,
                _lunaOrbitRadius * (float)Math.Sin(angleLuna)
            );

            angleLuna -= MathHelper.ToRadians(_lunaRotationSpeedAroundPlanet);

            luna.Translation = orbitPosition + planetCenter;

            Matrix rotationAroundPlanet = Matrix.CreateRotationY(MathHelper.ToRadians(_lunaRotationSpeedAroundPlanet));
            luna = Matrix.Multiply(rotationAroundPlanet, luna);


        }
    }
}
