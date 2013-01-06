using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBash
{
    public class Button
    {
        public double CurrentCoolDown = 0;
        public double CoolDown = 1000;
        public int MinionType;
        public Keys ShortcutKey;

        public Button(int type, double cd, Keys shortcut)
        {
            MinionType = type;
            CoolDown = cd;
            CurrentCoolDown = 0;
            ShortcutKey = shortcut;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentCoolDown > 0) CurrentCoolDown -= gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }

    public class ButtonManager
    {
        public List<Button> Buttons = new List<Button>();

        public Vector2 Position;

        Vector2 buttonSize = new Vector2(96, 96);

        Texture2D buttonBG;

        public int SelectedButton = 0;

        public Rectangle pauseButton;
        public Rectangle heroCamButton;

        public bool pausePressed;

        public ButtonManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            Buttons.Add(new Button(0, 2000 - (100*(GameManager.CurrentPlaythrough-1)), Keys.D1));
            Buttons.Add(new Button(1, 3000 - (150 * (GameManager.CurrentPlaythrough - 1)), Keys.D2));
            Buttons.Add(new Button(2, 5000 - (250 * (GameManager.CurrentPlaythrough - 1)), Keys.D3));
            Buttons.Add(new Button(3, 8000 - (400 * (GameManager.CurrentPlaythrough - 1)), Keys.D4));
            Buttons.Add(new Button(4, 4000 - (200 * (GameManager.CurrentPlaythrough - 1)), Keys.D5));

            Position = new Vector2((GameManager.Camera.Width / 2) - ((Buttons.Count*buttonSize.X)/2), GameManager.Camera.Height - buttonSize.Y);

            pauseButton = new Rectangle((int)(GameManager.Camera.Width - buttonSize.X), (int)(GameManager.Camera.Height - buttonSize.Y), (int)buttonSize.X, (int)buttonSize.Y);
            heroCamButton = new Rectangle((int)(GameManager.Camera.Width - (buttonSize.X * 2)), (int)(GameManager.Camera.Height - buttonSize.Y), (int)buttonSize.X, (int)buttonSize.Y);

        }

        public void LoadContent(ContentManager content)
        {
            buttonBG = content.Load<Texture2D>("buttons");
        }

        public void Update(GameTime gameTime)
        {
            foreach (Button b in Buttons)
                b.Update(gameTime);
        }

        public bool HandleInput(InputState input)
        {
            bool handled = false;

            Vector2 tapPos = Vector2.Zero;
            if (input.TapPosition.HasValue)
            {
                tapPos = input.TapPosition.Value;
            }

            Vector2 testPos = Position;
            foreach (Button b in Buttons)
            {
                Rectangle testRect = new Rectangle((int)testPos.X, (int)testPos.Y, (int)buttonSize.X, (int)buttonSize.Y);

                if (testRect.Contains(new Point((int)tapPos.X, (int)tapPos.Y)))
                {
                    SelectedButton = Buttons.IndexOf(b);

                    handled = true;
                }

                if (pauseButton.Contains(new Point((int)tapPos.X, (int)tapPos.Y)))
                {
                    pausePressed = true;
                    handled = true;
                }
                if (heroCamButton.Contains(new Point((int)tapPos.X, (int)tapPos.Y)))
                {
                    GameManager.CameraFollowingHero = !GameManager.CameraFollowingHero;
                    handled = true;
                }

                testPos += new Vector2(buttonSize.X, 0);

                if(input.CurrentKeyboardStates[0].IsKeyDown(b.ShortcutKey)) SelectedButton = Buttons.IndexOf(b);
            }



            return handled;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawPos = Position;
            foreach (Button b in Buttons)
            {
                if (Buttons.IndexOf(b) == SelectedButton)
                {
                    spriteBatch.Draw(buttonBG, drawPos, new Rectangle((int)buttonSize.X, 0, (int)buttonSize.X, (int)buttonSize.Y), b.CurrentCoolDown > 0 ? new Color(50,50,50) : Color.White);
                    if (b.CurrentCoolDown > 0)
                    {
                        float cdAmount = buttonSize.Y - ((buttonSize.Y / (float)b.CoolDown) * (float)b.CurrentCoolDown);
                        spriteBatch.Draw(buttonBG, drawPos + new Vector2(0, buttonSize.Y - cdAmount), new Rectangle((int)buttonSize.X, (int)(buttonSize.Y - cdAmount), (int)buttonSize.X, (int)buttonSize.Y), new Color(150, 150, 150));
                    }
                }
                else
                {
                    spriteBatch.Draw(buttonBG, drawPos, new Rectangle(0, 0, (int)buttonSize.X, (int)buttonSize.Y), b.CurrentCoolDown > 0 ? new Color(50, 50, 50) : Color.White);
                    if (b.CurrentCoolDown > 0)
                    {
                        float cdAmount = buttonSize.Y - ((buttonSize.Y / (float)b.CoolDown) * (float)b.CurrentCoolDown);
                        spriteBatch.Draw(buttonBG, drawPos + new Vector2(0, buttonSize.Y - (int)cdAmount), new Rectangle(0, (int)(buttonSize.Y - cdAmount), (int)buttonSize.X, (int)buttonSize.Y), new Color(150, 150, 150));
                    }
                }
                spriteBatch.Draw(GameManager.MinionManager.SpriteSheets[b.MinionType], drawPos + (buttonSize / 2) + new Vector2(5,5), new Rectangle(0, 0, 64, 64), Color.Black*0.4f, 0f, new Vector2(32, 32), 1f, SpriteEffects.None, 1);
                spriteBatch.Draw(GameManager.MinionManager.SpriteSheets[b.MinionType], drawPos + (buttonSize / 2), new Rectangle(0, 0, 64, 64), Color.White, 0f, new Vector2(32,32), 1f, SpriteEffects.None, 1);
                drawPos += new Vector2(buttonSize.X, 0);
            }

            spriteBatch.Draw(buttonBG, pauseButton, new Rectangle(GameManager.GameIsPaused?(int)buttonSize.X:0, 0, (int)buttonSize.X, (int)buttonSize.Y), Color.White);
            spriteBatch.Draw(buttonBG, heroCamButton, new Rectangle(GameManager.CameraFollowingHero ? (int)buttonSize.X : 0, 0, (int)buttonSize.X, (int)buttonSize.Y), Color.White);
            spriteBatch.Draw(buttonBG, pauseButton, new Rectangle(192, 0, 96, 96), Color.White);
            spriteBatch.Draw(buttonBG, heroCamButton, new Rectangle(288, 0, 96, 96), Color.White);

            
        }
    }
}
