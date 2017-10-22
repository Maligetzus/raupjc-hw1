using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zad3;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public List<Wall> Walls { get; set; }
        public List<Wall> Goals { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this){
                PreferredBackBufferHeight = 900,
                PreferredBackBufferWidth = 500
            };
            Content.RootDirectory = "Content";
        }

        /// <summary >
        /// Bottom paddle object
        /// </ summary >
        public Paddle PaddleBottom;
        public Paddle paddleBottom
        {
            get { return PaddleBottom; }
            private set { PaddleBottom = value; }
        }

        /// <summary >
        /// Top paddle object
        /// </ summary >
        public Paddle PaddleTop;
        public Paddle paddleTop
        {
            get { return PaddleTop; }
            private set { PaddleTop = value; }
        }

        /// <summary >
        /// Ball object
        /// </ summary >
        public Ball Ball;
        public Ball ball
        {
            get { return Ball; }
            private set { Ball = value; }
        }

        /// <summary >
        /// Background image
        /// </ summary >
        public Background Background;
        public Background background
        {
            get { return Background; }
            private set { Background = value; }
        }

        /// <summary >
        /// Sound when ball hits an obstacle .
        /// SoundEffect is a type defined in Monogame framework
        /// </ summary >
        public SoundEffect HitSound;
        public SoundEffect hitSound
        {
            get { return HitSound; }
            private set { HitSound = value; }
        }

        /// <summary >
        /// Background music . Song is a type defined in Monogame framework
        /// </ summary >
        public Song Music;
        public Song music
        {
            get { return Music; }
            private set { Music = value; }
        }
        /// <summary >
        /// Generic list that holds Sprites that should be drawn on screen
        /// </ summary >
        private IGenericList<Sprite> SpritesForDrawList = new Zad3.GenericList<Sprite>();

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Screen bounds details . Use this information to set up game objects positions.
            var screenBounds = GraphicsDevice.Viewport.Bounds;
            PaddleBottom = new Paddle(GameConstants.PaddleDefaultWidth, GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleBottom.X = screenBounds.Width / 2f - PaddleBottom.Width / 2f;
            PaddleBottom.Y = screenBounds.Bottom - 2*PaddleBottom.Height;
            PaddleTop = new Paddle(GameConstants.PaddleDefaultWidth, GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleTop.X = screenBounds.Width / 2f - PaddleTop.Width / 2f;
            PaddleTop.Y = screenBounds.Top + PaddleTop.Height;
            Ball = new Ball(100000, 1, 0.00001f)
            {
                X = 250,
                Y = 450
            };
            Background = new Background(screenBounds.Width, screenBounds.Height);
            Walls = new List<Wall>()
            {
                // try with 100 for default wall size !
                new Wall ( - GameConstants . WallDefaultSize ,0 , GameConstants . WallDefaultSize , screenBounds . Height ) ,
                new Wall ( screenBounds . Right ,0 , GameConstants . WallDefaultSize , screenBounds . Height )
            };
            Goals = new List<Wall>()
            {
                new Wall (0 , screenBounds . Height , screenBounds . Width , GameConstants . WallDefaultSize ) ,
                new Wall ( screenBounds . Top , - GameConstants . WallDefaultSize , screenBounds . Width , GameConstants . WallDefaultSize )
            };
            // Add our game objects to the sprites that should be drawn collection .
            SpritesForDrawList.Add(Background);
            SpritesForDrawList.Add(PaddleBottom);
            SpritesForDrawList.Add(PaddleTop);
            SpritesForDrawList.Add(Ball);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize new SpriteBatch object which will be used to draw textures .
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Set textures
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            PaddleBottom.Texture = paddleTexture;
            PaddleTop.Texture = paddleTexture;
            Ball.Texture = Content.Load<Texture2D>("ball");
            Background.Texture = Content.Load<Texture2D>("background");
            // Load sounds
            // Start background music
            HitSound = Content.Load<SoundEffect>("hit");
            Music = Content.Load<Song>("music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Music);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var touchState = Keyboard.GetState();
            if (touchState.IsKeyDown(Keys.Left))
            {
                PaddleBottom.X = PaddleBottom.X - (float) (PaddleBottom.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (touchState.IsKeyDown(Keys.Right))
            {
                PaddleBottom.X = PaddleBottom.X + (float)(PaddleBottom.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (touchState.IsKeyDown(Keys.A))
            {
                PaddleTop.X = PaddleTop.X - (float)(PaddleTop.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (touchState.IsKeyDown(Keys.D))
            {
                PaddleTop.X = PaddleTop.X + (float)(PaddleTop.Speed * gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            PaddleBottom.X = MathHelper.Clamp(PaddleBottom.X, 0, 500 - PaddleBottom.Width);
            PaddleTop.X = MathHelper.Clamp(PaddleTop.X, 0, 500 - PaddleTop.Width);
            
            var ballPositionChange = Ball.Direction * (float)(gameTime.ElapsedGameTime.TotalMilliseconds * Ball.Speed);
            Ball.X += ballPositionChange.X;
            Ball.Y += ballPositionChange.Y;

            // return true if overlaps , false otherwise ...
            // Ball - side walls
            // If ball has collision with any of the side wall
            // Reverse X direction of the ball
            // Increase the ball speed by bump speed increase factor
            if (CollisionDetector.Overlaps(Ball, Walls[0]) || CollisionDetector.Overlaps(Ball, Walls[1]))
            {
                Ball.Direction = -Ball.Direction*new Vector2(-1,1);
                Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
            }
            // If ball has collision with winning walls ( goals )
            // Move ball to the center
            // Reset ball speed
            // Play hit sound with : HitSound . Play ();
            else if (CollisionDetector.Overlaps(Ball, Goals[0]) || CollisionDetector.Overlaps(Ball, Goals[1]))
            {
                Ball.X = 250;
                Ball.Y = 450;
                Ball.Speed= GameConstants.DefaultInitialBallSpeed;
                HitSound.Play();
            }
            // If ball has collision with paddles ( with appropriate movement direction !!)
            // Reverse Y direction of the ball
            // Increase the ball speed by bump speed increase factor
            else if (CollisionDetector.Overlaps(Ball, PaddleBottom) || CollisionDetector.Overlaps(Ball, PaddleTop))
            {
                Ball.Direction = Ball.Direction*new Vector2(1,-1);
                Ball.Speed *= Ball.BumpSpeedIncreaseFactor;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Start drawing .
            spriteBatch.Begin();
            for (int i = 0; i < SpritesForDrawList.Count; i++)
            {
                SpritesForDrawList.GetElement(i).DrawSpriteOnScreen(spriteBatch);
            }
            // End drawing .
            // Send all gathered details to the graphic card in one batch .
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
