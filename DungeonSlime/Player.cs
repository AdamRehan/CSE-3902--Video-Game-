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
    public Vector2 position { get; set; }
    public Vector2 velocity { get; set; }

    private bool teleporting = false;
    private double teleportTimer = 0;
    private const double teleportDuration = 400;
    
    private Vector2 teleportDest = Vector2.Zero;

    //From Gemini
    public float width => currAnimation != null ? currAnimation.Width : 0f;
    public float height => currAnimation != null ? currAnimation.Height : 0f;


    private SpriteEffects currentDir = SpriteEffects.None;
    private AnimatedSprite idleAnimation;
    private AnimatedSprite walkAnimation;
    private AnimatedSprite teleportAnimation;
    private AnimatedSprite currAnimation;

    public void Initialize()
    {
        currAnimation = idleAnimation;
    }

    public void LoadContent(TextureAtlas atlas) //Method taken from Google Gemini
    {
        // The player loads its own assets from the shared atlas reference
        this.idleAnimation = atlas.CreateAnimatedSprite("hk-idle");
        this.walkAnimation = atlas.CreateAnimatedSprite("hk-walk");
        this.teleportAnimation = atlas.CreateAnimatedSprite("hk-teleport");
        this.currAnimation = idleAnimation;
    }

    public void teleport(Vector2 dest)
    {
        teleportDest = dest;
        velocity = Vector2.Zero;
        teleporting = true;
        teleportTimer = 0;
    }


    public void Update(GameTime gameTime, KeyboardInfo keyboardInfo)
    {
        if (teleporting)
        {
            currAnimation = teleportAnimation;
            teleportTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (teleportTimer >= teleportDuration)
            {
                teleporting = false;
                position = teleportDest;
                teleportDest = Vector2.Zero;
            }
        }
        else
        {
            if (keyboardInfo.isLeft)
            {
                currentDir = SpriteEffects.FlipHorizontally;
            }
            else if (keyboardInfo.isRight)
            {
                currentDir = SpriteEffects.None;
            }
            if (velocity == Vector2.Zero)
            {
                currAnimation = idleAnimation;
            }
            else
            {
                currAnimation = walkAnimation;
            }
        }
        //Taken from Google Gemini
        idleAnimation.Effects = currentDir;
        walkAnimation.Effects = currentDir;

        currAnimation.Effects = currentDir;
        currAnimation.Update(gameTime);

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currAnimation.Draw(spriteBatch, position);
    }
}
