using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonSlime;

public interface IPlayer
{
    void MoveLeft();
    void MoveRight();
    void Jump();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}