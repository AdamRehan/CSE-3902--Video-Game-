using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace DungeonSlime;

public interface IPlayer
{
    Vector2 Position {get; set;}
    Vector2 Velocity {get; set;}

    void Initialize();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}