using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _line, _circle, _cross;

        int[,] _gameTable;
        private bool _isCircleTurn;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            _gameTable = new int[3, 3];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load a Texture2D from a file
            _line = this.Content.Load<Texture2D>("Line");
            _circle = this.Content.Load<Texture2D>("Circle");
            _cross = this.Content.Load<Texture2D>("Cross");
        }

        protected override void Update(GameTime gameTime)
        {
            // Handle inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState state = Mouse.GetState();

            // Update values in game
            if (state.LeftButton == ButtonState.Pressed)
            {
                //TODO: do clicking
                int iPos = state.X / 200;
                int jPos = state.Y / 200;

                if (iPos >= 0 && iPos < 3 && jPos >= 0 && jPos < 3)
                {
                    //check feasibility
                    if (_gameTable[iPos, jPos] == 0)
                    {
                        if (_isCircleTurn)
                        {
                            _gameTable[jPos, iPos] = 1;
                            _isCircleTurn = false;
                        }
                        else
                        {
                            _gameTable[jPos, iPos] = -1;
                            _isCircleTurn = true;
                        }
                    }
                }
            }

            //TODO: check winning condition


            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            //TODO: Draw circle and Cross
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_gameTable[i, j] == 1)
                    {
                        //circle
                        _spriteBatch.Draw(_circle, new Vector2(200 * j, 200 * i),
                        null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else if (_gameTable[i, j] == -1)
                    {
                        //cross
                        _spriteBatch.Draw(_cross, new Vector2(200 * j, 200 * i), null,
                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }

            //Draw Lines for game board

            // X axis
            _spriteBatch.Draw(_line, new Vector2(0, 200), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(0, 400), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            // Y axis
            _spriteBatch.Draw(_line, new Vector2(200, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(_line, new Vector2(400, 0), null, Color.White, MathHelper.Pi / 2, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //TODO: Draw Finish line


            _spriteBatch.End();

            _graphics.BeginDraw();

            base.Draw(gameTime);
        }
    }
}
