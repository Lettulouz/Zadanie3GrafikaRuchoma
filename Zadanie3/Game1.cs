using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Zadanie3
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private Texture2D _background;
        VertexPositionColor[] sun;
        VertexPositionColor[] mercuryVertex;
        VertexPositionColor[] venusVertex;
        VertexPositionColor[] earthVertex;
        VertexPositionColor[] lunaVertex;
        VertexPositionColor[] marsVertex;
        VertexPositionColor[] gridLines;
        BasicEffect _basicEffect;
        Matrix world, view, proj, mercury, earth, luna, venus, mars;
        private const int mercuryRadius = 5;
        private const int venusRadius = 10;
        private const int earthRadius = 15;
        private const int lunaRadius = 3;
        private const int marsRadius = 25;
        private const float sunSize = 2f;
        Planet earthPlanet = new(earthRadius, lunaRadius, 3, 3, 25, 2);
        Planet mercuryPlanet = new(mercuryRadius, 1,3);
        Planet venusPlanet = new(venusRadius, 4,1);
        Planet marsPlanet = new(marsRadius, 1,7);
        float angleX = 1.0f;
        float angleY = 0.0f;
        float scale = 40.0f;
        const int gridSize = 30;
        bool gridOn = true;
        bool backgroundOn = true;
        bool rotationOn = true;
        float remainingTimeGridOn = 0.3f;
        float remainingTimeBackgroundOn = 0.3f;
        float remainingTimeRotationOn = 0.3f;
        float transform = 0.0f;
        Matrix rotationMatrixX;
        Matrix rotationMatrixY;
        Matrix rotationMatrixZ;
        Matrix prevX;
        Matrix prevY;
        Vector3 beforeTranslation;
        float test = 0.0f;
        float planeAngle = 0.0f;
        Color[] earthColors = { Color.White, Color.DarkGreen };
        Color[] mercuryColors = { Color.White, Color.Coral };
        Color[] venusColors = { Color.White, Color.DarkBlue };
        Color[] sunColors = { Color.White, Color.Gold };
        Color[] lunaColors = { Color.White, Color.Gray };
        Color[] marsColors = { Color.White, Color.Red };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("stars");
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;

            Planet planet = new(0, 0, 0);
            sun = planet.generatePlanet(0, sunSize, sunColors);
            mercuryVertex = mercuryPlanet.generatePlanet(0, 0.4f, mercuryColors);
            venusVertex = venusPlanet.generatePlanet(0, 0.8f, venusColors);
            earthVertex = earthPlanet.generatePlanet(0, 1.2f, earthColors);
            lunaVertex = earthPlanet.generatePlanet(0, 0.2f, lunaColors);
            marsVertex = marsPlanet.generatePlanet(0, 1.4f, marsColors);

            mercury = Matrix.Identity;
            mercury = Matrix.CreateTranslation(new Vector3(mercuryRadius, 0, 0));

            venus = Matrix.Identity;
            venus = Matrix.CreateTranslation(new Vector3(venusRadius, 0, 0));

            earth = Matrix.Identity;
            earth = Matrix.CreateTranslation(new Vector3(earthRadius, 0, 0));

            luna = Matrix.Identity;
            luna = Matrix.CreateTranslation(new Vector3(earth.M41 + lunaRadius, earth.M42, earth.M43));

            mars = Matrix.Identity;
            mars = Matrix.CreateTranslation(new Vector3(marsRadius, 0, 0));

            int axisLength = 1;

            gridLines = new VertexPositionColor[gridSize * 8 + 4];

            for (int i = 0; i < gridSize * 4 + 2; i = i + 2)
            {
                int side = gridSize - i / 2;
                gridLines[i] = new VertexPositionColor(axisLength * new Vector3(gridSize, 0, side), Color.Gray);
                gridLines[i + 1] = new VertexPositionColor(-axisLength * new Vector3(gridSize, 0, -side), Color.Gray);
            }


            int secondAxis = gridSize * 4 + 2;


            for (int i = 0; i < gridSize * 4 + 2; i = i + 2)
            {
                int side = gridSize - i / 2;
                gridLines[i + secondAxis] = new VertexPositionColor(axisLength * new Vector3(side, 0, gridSize), Color.Gray);
                gridLines[i + 1 + secondAxis] = new VertexPositionColor(-axisLength * new Vector3(-side, 0, gridSize), Color.Gray);
            }



            _basicEffect = new BasicEffect(GraphicsDevice);
            _basicEffect.VertexColorEnabled = true;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbd = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (kbd.IsKeyDown(Keys.Right)) angleY += 0.02f;
            if (kbd.IsKeyDown(Keys.Left)) angleY -= 0.02f;
            if (kbd.IsKeyDown(Keys.Up)) angleX -= 0.02f;
            if (kbd.IsKeyDown(Keys.Down)) angleX += 0.02f; 
            if (kbd.IsKeyDown(Keys.Q) && scale < gridSize*3) scale += 0.3f; 
            if (kbd.IsKeyDown(Keys.A) && scale>4*sunSize) scale -= 0.3f; 
            if (kbd.IsKeyDown(Keys.X) && remainingTimeGridOn == 0.0f) {

                gridOn = !gridOn;
                remainingTimeGridOn = 0.3f;
            }
            remainingTimeGridOn = MathHelper.Max(0, remainingTimeGridOn - (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (kbd.IsKeyDown(Keys.B) && remainingTimeBackgroundOn == 0.0f)
            {
                backgroundOn = !backgroundOn;
                remainingTimeBackgroundOn = 0.3f;
            }
            remainingTimeBackgroundOn = MathHelper.Max(0, remainingTimeBackgroundOn - (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (kbd.IsKeyDown(Keys.R) && remainingTimeRotationOn == 0.0f)
            {
                rotationOn = !rotationOn;
                remainingTimeRotationOn = 0.3f;
            }
            remainingTimeRotationOn = MathHelper.Max(0, remainingTimeRotationOn - (float)gameTime.ElapsedGameTime.TotalSeconds);

            world = Matrix.Identity;
        
            view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, scale), Vector3.Zero, Vector3.Up);

            view = Matrix.CreateRotationX(angleX) * Matrix.CreateRotationY(angleY) * view;      

            proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50), GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);





            if (rotationOn)
            {
                mercuryPlanet.ManagePlanet(ref mercury);
                venusPlanet.ManagePlanet(ref venus);
                earthPlanet.ManagePlanet(ref earth);
                earthPlanet.ManageLuna(ref luna, earth);
                marsPlanet.ManagePlanet(ref mars);

                transform = 7f;
                rotationMatrixX = Matrix.CreateRotationX(MathHelper.ToRadians(transform));
                transform = 5f;
                rotationMatrixY = Matrix.CreateRotationY(MathHelper.ToRadians(transform));
                transform = 3f;
                rotationMatrixZ = Matrix.CreateRotationZ(MathHelper.ToRadians(transform));

                for (int i = 0; i < sun.Length; i++)
                {
                    Vector3 originalPosition = sun[i].Position;
                    Vector3 transformedPositionX = Vector3.Transform(originalPosition, rotationMatrixX);
                    Vector3 transformedPositionXY = Vector3.Transform(transformedPositionX, rotationMatrixY);
                    Vector3 transformedPositionXYZ = Vector3.Transform(transformedPositionXY, rotationMatrixZ);
                    sun[i].Position = transformedPositionXYZ;
                }
            }


            _basicEffect.World = world;
            _basicEffect.View = view;
            _basicEffect.Projection = proj;           
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            if (backgroundOn)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_background,
                   new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                   Color.White);
                _spriteBatch.End();
            }

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            _basicEffect.CurrentTechnique.Passes[0].Apply();
            if (gridOn)
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, gridLines, 0, gridSize * 4 + 2);
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, sun, 0, sun.Length / 3);
            _basicEffect.World = mercury;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, mercuryVertex, 0, mercuryVertex.Length / 3);
            _basicEffect.World = venus;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, venusVertex, 0, venusVertex.Length / 3);
            _basicEffect.World = earth;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, earthVertex, 0, earthVertex.Length / 3);
            _basicEffect.World = luna;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, lunaVertex, 0, lunaVertex.Length / 3);
            _basicEffect.World = mars;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, marsVertex, 0, marsVertex.Length / 3);



            base.Draw(gameTime);
        }

       
    }
}