using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace DungeonSlime;

public class Game1 : Core
{
    // Defines the knight animated sprite.
    private AnimatedSprite knight;

    // Tracks the position of the knight.
    private Vector2 knightPosition;

    // Speed multiplier when moving.
    private const float MOVEMENT_SPEED = 5.0f;

    private bool isWalking;

    public Game1() : base("Dungeon Slime", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create the texture atlas from the XML configuration file.
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        // Create the knight animated sprite from the atlas.
        knight = atlas.CreateAnimatedSprite("knight-animation");
        knight.Scale = new Vector2(1.0f, 1.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        // Update the InputManger inside base.Update() right away.
        base.Update(gameTime);

        isWalking = false;

        // Check for keyboard input and handle it.
        CheckKeyboardInput();

        // Check for gamepad input and handle it.
        CheckGamePadInput();

        // Update the knight animated sprite.
        if(isWalking)
        {
        knight.Update(gameTime);
        }
    }

    private void CheckKeyboardInput()
    {
        // If the space key is held down, the movement speed increases by 1.5
        float speed = MOVEMENT_SPEED;
        if (Input.Keyboard.IsKeyDown(Keys.Space))
        {
            speed *= 1.5f;
        }

        // If the W or Up keys are down, move the knight up on the screen.
        if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
        {
            knightPosition.Y -= speed;
        }

        // if the S or Down keys are down, move the knight down on the screen.
        if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
        {
            knightPosition.Y += speed;
        }

        // If the A or Left keys are down, move the knight left on the screen.
        if (Input.Keyboard.IsKeyDown(Keys.A) || Input.Keyboard.IsKeyDown(Keys.Left))
        {
            isWalking = true;
            knightPosition.X -= speed;
        }

        // If the D or Right keys are down, move the knight right on the screen.
        if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
        {
            isWalking = true;
            knightPosition.X += speed;
        }
    }

    private void CheckGamePadInput()
    {
        GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

        // If the A button is held down, the movement speed increases by 1.5
        // and the gamepad vibrates as feedback to the player.
        float speed = MOVEMENT_SPEED;
        if (gamePadOne.IsButtonDown(Buttons.A))
        {
            speed *= 1.5f;
            gamePadOne.SetVibration(1.0f, TimeSpan.FromSeconds(1));
        }
        else
        {
            gamePadOne.StopVibration();
        }

        // Check thumbstick first since it has priority over which gamepad input
        // is movement.  It has priority since the thumbstick values provide a
        // more granular analog value that can be used for movement.
        if (gamePadOne.LeftThumbStick != Vector2.Zero)
        {
            knightPosition.X += gamePadOne.LeftThumbStick.X * speed;
            knightPosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
        }
        else
        {
            // If DPadUp is down, move the knight up on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadUp))
            {
                knightPosition.Y -= speed;
            }

            // If DPadDown is down, move the knight down on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadDown))
            {
                knightPosition.Y += speed;
            }

            // If DPapLeft is down, move the knight left on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
            {
                knightPosition.X -= speed;
            }

            // If DPadRight is down, move the knight right on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadRight))
            {
                knightPosition.X += speed;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the back buffer.
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw the knight sprite.
        knight.Draw(SpriteBatch, knightPosition);

        // Always end the sprite batch when finished.
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
