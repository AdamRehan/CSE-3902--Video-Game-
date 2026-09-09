using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using DungeonSlime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class Player : IPlayer
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    private SpriteEffects currentDir = SpriteEffects.None;
    private AnimatedSprite idleAnimation;
    private AnimatedSprite walkAnimation;
    private AnimatedSprite currAnimation;

    public void Initialize()
    {
        currAnimation = idleAnimation;
    }
    
    public void LoadContent(TextureAtlas atlas) //Method taken from Google Gemini
{
    // The player loads its own assets from the shared atlas reference
    this.idleAnimation = atlas.CreateAnimatedSprite("knight-idle-animation");
    this.walkAnimation = atlas.CreateAnimatedSprite("knight-walk-animation");
    this.currAnimation = idleAnimation;
}


    public void Update(GameTime gameTime, KeyboardInfo keyboardInfo)
    {
        if(keyboardInfo.isLeft)
        {
            currentDir = SpriteEffects.FlipHorizontally;
        }
        else
        {
            currentDir = SpriteEffects.None;
        }
        if (Velocity == Vector2.Zero)
        {
            currAnimation = idleAnimation;
        }
        else
        {
            currAnimation = walkAnimation;
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currAnimation.Draw(spriteBatch, Position);
    }
}
