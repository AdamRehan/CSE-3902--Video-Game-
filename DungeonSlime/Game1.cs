using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace DungeonSlime;

public class Game1 : Core
{
    // Defines the knight animated sprite.
    private Player player;

    // Tracks the position of the knight.
    private Vector2 knightPosition;

    //Menu font stuff
    SpriteFont font1;
    Vector2 fontPos1;

    // Speed multiplier when moving.
    private const float MOVEMENT_SPEED = 5.0f;



    public Game1() : base("Dungeon Slime", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        player = new Player();
        player.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        font1 = Content.Load<SpriteFont>("MenuFont");

        fontPos1 = new Vector2(600f, 600f);

        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        player.LoadContent(atlas);
    }


    protected override void Update(GameTime gameTime)
    {
        // Update the InputManger inside base.Update() right away.
        base.Update(gameTime);
        CheckKeyboardInput();
        checkMouseInput();
        player.Update(gameTime, Input.Keyboard);
    }

    private void CheckKeyboardInput()
    {
        // If the space key is held down, the movement speed increases by 1.5
        float speed = MOVEMENT_SPEED;
        if (Input.Keyboard.IsKeyDown(Keys.Space))
        {
            speed *= 1.5f;
        }

        Vector2 newVelocity = player.velocity;
        // If the W or Up keys are down, move the knight up on the screen.
        if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
        {
            newVelocity.Y = -speed;
        }
        // if the S or Down keys are down, move the knight down on the screen.
        else if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
        {
            newVelocity.Y = speed;
        }
        else
        {
            newVelocity.Y = 0;
        }

        // If the A or Left keys are down, move the knight left on the screen.
        if (Input.Keyboard.isLeft)
        {
            newVelocity.X = -speed;
        }
        // If the D or Right keys are down, move the knight right on the screen.
        else if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
        {
            newVelocity.X = speed;
        }
        else
        {
            newVelocity.X = 0;
        }
        player.velocity = newVelocity;
        player.position += player.velocity;
    }

    private void checkMouseInput()
    {
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Left))
        {
            Vector2 dest = new Vector2(Input.Mouse.Position.X - (player.width / 2), Input.Mouse.Position.Y - ((player.height / 2)));
            player.teleport(dest);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        player.Draw(SpriteBatch);

        if (font1 != null)
        {
            string line1 = "Credits\nProgram Made By: Adam Rehan\nSprites From NOTE: DO NOT CLICK ON LINK. \nFor some reason this website is super sketchy. \nThe sprite sheet is perfect but the website is very buggy. \nFeel free to reach out to me for me to show this. https://utpaqp.edu.pe/search/the-best-hollow-knight-sprite-sheet-kemprot-blog-mobile-legends/attack-sprite-sheet/";
            Vector2 origin1 = font1.MeasureString(line1) / 2f;

            SpriteBatch.DrawString(
                font1,
                line1,
                fontPos1,
                Color.Black,
                0f,
                origin1,
                1.0f,
                SpriteEffects.None,
                0f
            );
        }
        SpriteBatch.End();

        base.Draw(gameTime);
    }

}
