using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using DungeonSlime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class Player : IPlayer
{
    public Vector2 Position {get; set;}
    public Vector2 Velocity {get; set;}
    private Animation idleAnimation;
    private Animation walkAnimation;
    private Animation currAnimation;

    public void Initialize()
    {
        currAnimation = idleAnimation;
    }

    public void Update(GameTime gameTime)
    {
        if (Velocity == Vector2.Zero)
        {
            currAnimation = idleAnimation;
        }
        else
        {
            currAnimation = walkAnimation;
        }
    }

    public void Draw(SpriteBatch spireBatch)
    {
        SpriteBatch.Draw(currAnimation.Texture, Position, currAnimation.GetCurrentSourceRectangle(), Color.White);
    }
}
