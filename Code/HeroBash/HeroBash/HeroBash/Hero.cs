using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using TiledLib;

namespace HeroBash
{
    public class Hero
    {
        static Random randomNumber = new Random();

        public Vector2 Position;
        public Vector2 Velocity = new Vector2(0,0);

        public int HP = 5;
        public int MaxHP = 6;

        public int Level = 1;
        public int XP = 0;
        public int XPTNL = 0;

        public float painAlpha = 0f;

        public float levelUpAlpha = 0f;
        public float levelUpTransition = 0f;

        public double frozenTime = 0;

        public bool ReachedPrincess = false;

        public double SpawnTime;
        float spawnAlpha = 0f;

        double animTime = 50;
        double currentFrameTime = 0;
        public int animFrame = 1;
        int numFrames = 9;
        bool onGround = true;

        bool winSoundPlayed = false;

        public Vector2 SpawnPoint;
        Vector2 Gravity = new Vector2(0, 0.5f);

        Vector2 frameSize = new Vector2(64, 64);
        Texture2D spriteSheet;

        public int MaxSwords = 3;
        public int numSwords = 3;
        double swordRefreshTime;
        double swordAttackTime;



        public Hero()
        {
            MaxHP = 5; // + (GameManager.Level * 2);
            MaxSwords = 2;         

            XPTNL = CalculateXPTNL(Level);
        }

        public void Initialize() 
        {
            Vector2 chosenSpawn = new Vector2(1000,0);
            // Try to find a spawn point
            var layer = GameManager.Map.Layers.Where(l => l.Name == "Spawn").First();
            if (layer!=null)
            {
                MapObjectLayer objectlayer = layer as MapObjectLayer;

                foreach (MapObject o in objectlayer.Objects)
                {
                    if (o.Location.Center.X<chosenSpawn.X) chosenSpawn = new Vector2(o.Location.Center.X, o.Location.Center.Y);
                    
                }

                SpawnPoint = chosenSpawn;
                Position = SpawnPoint;
                //Position.X = 0;
                spawnAlpha = 0f;
                SpawnTime = 4000 + (2*(GameManager.CurrentPlaythrough-1));
            }

            HP = MaxHP;
            numSwords = MaxSwords;
            winSoundPlayed = false;
            frozenTime = 0;

            ReachedPrincess = false;
        }

        public void Respawn()
        {
            Vector2 chosenSpawn = Vector2.Zero;
            float furthestX = 0f;

            var layer = GameManager.Map.Layers.Where(l => l.Name == "Spawn").First();
            if (layer != null)
            {
                MapObjectLayer objectlayer = layer as MapObjectLayer;

                foreach (MapObject o in objectlayer.Objects)
                {
                    Vector2 pos = new Vector2(o.Location.Center.X, o.Location.Center.Y);

                    if (pos.X <= Position.X && pos.X>furthestX)
                    {
                        furthestX = pos.X;
                        chosenSpawn = pos;
                    }
                }

                SpawnPoint = chosenSpawn;
                Position = SpawnPoint;
                spawnAlpha = 0f;
                frozenTime = 0;
                numSwords = 3;
                painAlpha = 0;
            }
        }

        public void LoadContent(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("hero");
        }

        public void Update(GameTime gameTime)
        {
            if (SpawnTime > 0)
            {
                SpawnTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
                return;
            }

            if (spawnAlpha < 1f) spawnAlpha += 0.1f;
            if (painAlpha > 0f) painAlpha -= 0.01f;

            CollisionCheck();

            if (HP <= 0)
            {
                Velocity.X = 0;

                if (!winSoundPlayed) { AudioController.PlaySFX("win", 0.8f, 1f); winSoundPlayed = true; }
                return;
            }

            // Anim
            currentFrameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentFrameTime >= animTime)
            {
                currentFrameTime = 0;
                animFrame++;
                if (animFrame > numFrames) animFrame = 1;
            }

            if (swordAttackTime > 0) swordAttackTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (swordRefreshTime > 0) swordRefreshTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (swordRefreshTime <= 0)
            {
                if (numSwords < MaxSwords) numSwords++;
                swordRefreshTime = 5000;
            }

            JumpsCheck();
            Combat();

            if (frozenTime > 0) frozenTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if(frozenTime<=0)
                Velocity = Vector2.Clamp(Velocity, new Vector2(-3f, -15), new Vector2(3f, 15));
            else
                Velocity = Vector2.Clamp(Velocity, new Vector2(-1.5f, -15), new Vector2(1.5f, 15));

           

            if (Position.Y > GameManager.Map.Height*GameManager.Map.TileHeight)
            {
                Velocity = Vector2.Zero;
                Respawn();
                HP -= 1;
                painAlpha = 1f;
                AudioController.PlaySFX("fall", ((float)AudioController.randomNumber.NextDouble() * 0.5f) - 0.25f);
            }

            // Levelling
            if (XP >= XPTNL)
            {
                AudioController.PlaySFX("levelup");

                Level++;
                MaxHP++;
                HP++;

                if (Level % 2 == 0) MaxSwords++;

                if (numSwords < MaxSwords) numSwords++;

                XP -= XPTNL;
                XPTNL = CalculateXPTNL(Level);

                levelUpTransition = 0f;
                levelUpAlpha = 1f;

            }

            levelUpTransition += 0.03f;
            if (levelUpTransition >= 1f)
                levelUpAlpha -= 0.02f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (levelUpAlpha >= 0f)
            {
                spriteBatch.DrawString(GameManager.Font, "Level Up!", (Position + new Vector2(0, -80f * levelUpTransition) + new Vector2(1,1)) - GameManager.Camera.Position, Color.Black * 0.4f * levelUpAlpha, 0f, GameManager.Font.MeasureString("Level Up!") / 2, levelUpTransition, SpriteEffects.None, 1);
                spriteBatch.DrawString(GameManager.Font, "Level Up!", (Position + new Vector2(0, -80f * levelUpTransition) + new Vector2(-1, -1)) - GameManager.Camera.Position, Color.White * levelUpAlpha, 0f, GameManager.Font.MeasureString("Level Up!") / 2, levelUpTransition, SpriteEffects.None, 1);
            }

            if (HP <= 0)
            {
                spriteBatch.Draw(spriteSheet, (Position + new Vector2(0,5)) - GameManager.Camera.Position, new Rectangle(10 * (int)frameSize.X, 0, (int)frameSize.X, (int)frameSize.Y), Color.White, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                if (painAlpha > 0f)
                {
                    spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)) - GameManager.Camera.Position, new Rectangle(10 * (int)frameSize.X, 65, (int)frameSize.X, (int)frameSize.Y), Color.White * painAlpha, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                }
                return;
            }

            if (ReachedPrincess)
            {
                spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 5)) - GameManager.Camera.Position, new Rectangle((11 * (int)frameSize.X)+1, 0, (int)frameSize.X, (int)frameSize.Y), Color.White, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                return;
            }

            if (onGround)
            {
                spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)) - GameManager.Camera.Position, new Rectangle(animFrame * (int)frameSize.X, 0, (int)frameSize.X, (int)frameSize.Y), Color.White * spawnAlpha, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                if (frozenTime > 0)
                {
                    spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6) + new Vector2((int)(randomNumber.NextDouble() * 4f) - 2f, 0)) - GameManager.Camera.Position, new Rectangle(animFrame * (int)frameSize.X, 65, (int)frameSize.X, (int)frameSize.Y), new Color(100, 100, 255) * 0.8f, 0f, frameSize / 2, 1.1f, SpriteEffects.None, 1);
                }
                if (painAlpha > 0f)
                {
                    spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)) - GameManager.Camera.Position, new Rectangle(animFrame * (int)frameSize.X, 65, (int)frameSize.X, (int)frameSize.Y), Color.White * painAlpha, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                }
            }
            else
            {
                spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)) - GameManager.Camera.Position, new Rectangle(0 * (int)frameSize.X, 0, (int)frameSize.X, (int)frameSize.Y), Color.White * spawnAlpha, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                if (frozenTime > 0)
                {
                    spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)+new Vector2((int)(randomNumber.NextDouble() * 4f)-2f,0)) - GameManager.Camera.Position, new Rectangle(0 * (int)frameSize.X, 65, (int)frameSize.X, (int)frameSize.Y), new Color(100, 100, 255) * 0.8f, 0f, frameSize / 2, 1.1f, SpriteEffects.None, 1);
                }
                if (painAlpha > 0f)
                {
                    spriteBatch.Draw(spriteSheet, (Position + new Vector2(0, 6)) - GameManager.Camera.Position, new Rectangle(0 * (int)frameSize.X, 65, (int)frameSize.X, (int)frameSize.Y), Color.White * painAlpha, 0f, frameSize / 2, 1f, SpriteEffects.None, 1);
                }
            }
        }

        void Combat()
        {
            if (ReachedPrincess) return;

            var t = GameManager.Map.Layers.Where(l => l.Name == "FG").First();
            TileLayer tileLayer = t as TileLayer;

            foreach (Minion m in GameManager.MinionManager.Minions)
            {
                if (!m.Active || m.Squished || m.Impaled) continue;
                // Check collision
                if ((Position - m.Position).Length() < 55)
                {
                    if ((Position.Y - m.Position.Y) < -40 && Velocity.Y > 0f && m.spawnAlpha >= 1f)
                    {
                        // Jumped on top of minion
                        m.Squished = true;
                        XP += 10;
                        AudioController.PlaySFX("crush", ((float)AudioController.randomNumber.NextDouble() * 0.5f) - 0.25f);
                    }
                    else
                    {
                        if (painAlpha <= 0f && m.spawnAlpha>=1f && !(m.Type==2||m.Type==3))
                        {
                            HP -= 1;
                            painAlpha = 1f;
                            AudioController.PlaySFX("herohurt", ((float)AudioController.randomNumber.NextDouble() * 0.5f) - 0.25f);
                        }
                    }
                }

                // Check jump distance
                if ((m.Position.X - Position.X) > 0 && (m.Position.X - Position.X) < 400 &&
                   (m.Position.Y - Position.Y) > -10 && (m.Position.Y - Position.Y) < 10  &&
                   (GameManager.princessPosition - Position).Length()>300 &&
                    onGround)
                {
                    // Check that there is no pit or obstacle in front
                    bool found = false;

                    Point tileP = new Point((int)((Position.X + 150) / GameManager.Map.TileWidth), (int)((Position.Y + (((frameSize.Y / 2) + 5))) / GameManager.Map.TileHeight));
                    if (tileP.X >= tileLayer.Tiles.GetLowerBound(0) && tileP.X <= tileLayer.Tiles.GetUpperBound(0) &&
                        tileP.Y >= tileLayer.Tiles.GetLowerBound(1) && tileP.Y <= tileLayer.Tiles.GetUpperBound(1))
                        if (tileLayer.Tiles[tileP.X, tileP.Y] == null)
                            found = true;
                        
                    tileP = new Point((int)((Position.X + 64) / GameManager.Map.TileWidth), (int)((Position.Y) / GameManager.Map.TileHeight));
                    if (tileP.X >= tileLayer.Tiles.GetLowerBound(0) && tileP.X <= tileLayer.Tiles.GetUpperBound(0) &&
                        tileP.Y >= tileLayer.Tiles.GetLowerBound(1) && tileP.Y <= tileLayer.Tiles.GetUpperBound(1))
                        if (tileLayer.Tiles[tileP.X, tileP.Y] != null)
                            found = true;


                    if (!found) 
                    {
                        if (randomNumber.Next(5) == 1)
                        {

                            Velocity.Y = -11f;
                            Position.Y += Velocity.Y;
                            AudioController.PlaySFX("jump", ((float)AudioController.randomNumber.NextDouble() * 0.5f) - 0.25f);
                            onGround = false;
                        }
                    }
                    
                }

                // Check projectile distance
                if ((m.Position.X - Position.X) > 0 && (m.Position.X - Position.X) < 800 &&
                   (m.Position.Y - Position.Y) > -10 && (m.Position.Y - Position.Y)<10)
                {
                    // Check that there is no pit or obstacle in front
                    bool found = false;

                    for (int x = (int)Position.X; x < m.Position.X; x += 64)
                    {
                        Point tileP = new Point((int)((x) / GameManager.Map.TileWidth), (int)((Position.Y) / GameManager.Map.TileHeight));
                        if (tileP.X >= tileLayer.Tiles.GetLowerBound(0) && tileP.X <= tileLayer.Tiles.GetUpperBound(0) &&
                            tileP.Y >= tileLayer.Tiles.GetLowerBound(1) && tileP.Y <= tileLayer.Tiles.GetUpperBound(1))
                            if (tileLayer.Tiles[tileP.X, tileP.Y] != null)
                                found = true;
                    }

                    if (!found)
                    {
                        if (numSwords > 0 && swordAttackTime<=0 && m.spawnAlpha>=1f)
                        {
                            swordAttackTime = 1000;
                            if(swordRefreshTime<=0)
                                swordRefreshTime += 5000;
                            numSwords--;
                            GameManager.ProjectileManager.Add(Position + new Vector2(25, 0), new Vector2(10, 0), true, 0);
                            if (Position.X > GameManager.Camera.Position.X && Position.X < GameManager.Camera.Position.X + GameManager.Camera.Width)
                                AudioController.PlaySFX("swordthrow", ((float)AudioController.randomNumber.NextDouble() * 0.5f) + 0.25f, 0.5f);
                        }
                    }

                }
            }

            

        }

        bool CollisionCheck()
        {
            //if (HP == 0 && onGround)
            //{
            //    return false;
            //}

            if (ReachedPrincess) return false;

           

            bool collidedx = false;
            bool collidedy = false;

            var t = GameManager.Map.Layers.Where(l => l.Name == "FG").First();
            TileLayer tileLayer = t as TileLayer;

            int x, y;

            for (x = -1; x <= 0; x++)
            {
                Point tileP = new Point((int)((Position.X + (x * ((frameSize.X / 2))) + (x * -5)) / GameManager.Map.TileWidth), (int)((Position.Y + (((frameSize.Y / 2) + 5))) / GameManager.Map.TileHeight));
                if (tileP.X >= tileLayer.Tiles.GetLowerBound(0) && tileP.X <= tileLayer.Tiles.GetUpperBound(0) &&
                    tileP.Y >= tileLayer.Tiles.GetLowerBound(1) && tileP.Y <= tileLayer.Tiles.GetUpperBound(1))
                {
                    if (tileLayer.Tiles[tileP.X, tileP.Y] == null)
                    {
                        onGround = false;
                    }
                    else
                    {
                        onGround = true;
                        Position.Y = (tileP.Y * GameManager.Map.TileHeight) - 37;
                    }
                }
            }

            // Check left
            x=-1;
            for (y = 0; y <= 1; y++)
            {
                Point tilePos = new Point((int)((Position.X + (x * ((frameSize.X / 2)))) / GameManager.Map.TileWidth), (int)((Position.Y + (y * ((frameSize.Y / 2)))) / GameManager.Map.TileHeight));

                if (tilePos.X < tileLayer.Tiles.GetLowerBound(0) || tilePos.X > tileLayer.Tiles.GetUpperBound(0)) continue;
                if (tilePos.Y < tileLayer.Tiles.GetLowerBound(1) || tilePos.Y > tileLayer.Tiles.GetUpperBound(1)) continue;

                if (tileLayer.Tiles[tilePos.X, tilePos.Y] != null)
                {
                    if (Velocity.X < 0)
                    {
                        if (!onGround)
                        {
                            if (y == 1)
                                collidedx = true;
                        }
                        else
                        {
                            collidedx = true;
                        }
                    }
                    //Velocity.X = 0;
                }
                
            }

            // Check right
            x = 1;
            for (y = 0; y <= 1; y++)
            {
                Point tilePos = new Point((int)((Position.X + (x * ((frameSize.X / 2)))) / GameManager.Map.TileWidth), (int)((Position.Y + (y * ((frameSize.Y / 2)))) / GameManager.Map.TileHeight));

                if (tilePos.X < tileLayer.Tiles.GetLowerBound(0) || tilePos.X > tileLayer.Tiles.GetUpperBound(0)) continue;
                if (tilePos.Y < tileLayer.Tiles.GetLowerBound(1) || tilePos.Y > tileLayer.Tiles.GetUpperBound(1)) continue;

                if (tileLayer.Tiles[tilePos.X, tilePos.Y] != null)
                {
                    if (Velocity.X > 0)
                    {
                        if (!onGround)
                        {
                            if (y == 1)
                                collidedx = true;
                        }
                        else
                        {
                            collidedx = true;
                        }
                    }
                }

            }

            // Check down
            if (!onGround)
            {
                y = 1;
                for (x = -1; x <= 1; x++)
                {
                    Point tilePos = new Point((int)((Position.X + (x * ((frameSize.X / 2))) + (x * -10)) / GameManager.Map.TileWidth), (int)((Position.Y + (y * ((frameSize.Y / 2) + 2))) / GameManager.Map.TileHeight));

                    if (tilePos.X < tileLayer.Tiles.GetLowerBound(0) || tilePos.X > tileLayer.Tiles.GetUpperBound(0)) continue;
                    if (tilePos.Y < tileLayer.Tiles.GetLowerBound(1) || tilePos.Y > tileLayer.Tiles.GetUpperBound(1)) continue;

                    if (tileLayer.Tiles[tilePos.X, tilePos.Y] != null)
                    {
                        collidedy = true;

                        if (Velocity.Y > 0)
                        {
                            Velocity.Y = 0;

                        }
                        Position.Y -= 1f;
                        //Position.Y = (tilePos.Y * GameManager.Map.TileHeight) - 37;
                    }

                }
            }

            // Check up
            y = -1;
            for (x = -1; x <= 1; x++)
            {
                Point tilePos = new Point((int)((Position.X + (x * ((frameSize.X / 2))) + (x * -10)) / GameManager.Map.TileWidth), (int)((Position.Y + (y * ((frameSize.Y / 2) - 63))) / GameManager.Map.TileHeight));

                if (tilePos.X < tileLayer.Tiles.GetLowerBound(0) || tilePos.X > tileLayer.Tiles.GetUpperBound(0)) continue;
                if (tilePos.Y < tileLayer.Tiles.GetLowerBound(1) || tilePos.Y > tileLayer.Tiles.GetUpperBound(1)) continue;

                if (tileLayer.Tiles[tilePos.X, tilePos.Y] != null)
                {
                    //collidedy = true;

                    if (Velocity.Y < 0)
                    {
                        Velocity.Y = -Velocity.Y;

                    }
                    //Position.Y -= 1f;
                }

            }

            

            if ((GameManager.princessPosition - Position).Length() < 64)
            {
                collidedx = true;
                ReachedPrincess = true;
                AudioController.PlaySFX("lose");
            }

            if (!collidedx)
            {
                Position.X += Velocity.X;
                if (Velocity.X < 4) Velocity.X += 0.5f;
            }
            if (!collidedy && !onGround)
            {
                Velocity += Gravity;
                Position.Y += Velocity.Y;

            }

            return collidedx || collidedy;
        }

        void JumpsCheck()
        {
            var layer = GameManager.Map.Layers.Where(l => l.Name == "Jumps").First();
            if (layer != null)
            {
                MapObjectLayer objectlayer = layer as MapObjectLayer;

                foreach (MapObject o in objectlayer.Objects)
                {
                    if (o.Location.Contains(new Point((int)Position.X-10, (int)(Position.Y + (frameSize.Y/2)))))
                    {
                        if(randomNumber.Next(10)==1 || (o.Properties["MustJump"].ToLower()=="true") && Velocity.Y>=0f)
                        {
                            onGround = false;
                            if(o.Type=="Full")
                                Velocity.Y=-13.5f;

                            if (o.Type == "Half")
                                Velocity.Y = -11f;

                            Position.Y += Velocity.Y;

                            if (Position.X > GameManager.Camera.Position.X && Position.X < GameManager.Camera.Position.X + GameManager.Camera.Width)
                                AudioController.PlaySFX("jump", ((float)AudioController.randomNumber.NextDouble()*0.5f) - 0.25f);
                         }
                    }
                }
            }
        }

        int CalculateXPTNL(int lev)
        {
            return 50 * lev;
        }
    }
}
