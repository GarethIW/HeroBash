using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HeroBash.HighScoresService;
using System.ComponentModel;

namespace HeroBash
{
    public enum ScoreBoardType
    {
        TopTen,
        NearbyOverall,
        WeeklyTopTen,
        NearbyWeekly,
        MyScores,
        MyNearbyScores
    }

    public enum Modes
    {
        Normal,
        Heroic,
        Legendary
    }

    public class ScoreBoard
    {
        public ScoreBoardType Type;

        public Vector2 Position;

        bool comparingScore = false;
        int comparePlaythrough;
        int compareStage;
        float compareLevel;
        decimal compareTime;
        int getScoreId;

        Score[] Scores;
        WeeklyScore[] WeeklyScores;

        bool scoresReturned = false;
        bool scoresError = false;

#if !WINRT
        BackgroundWorker bw;
#endif

        SpriteFont spriteFont;
        Texture2D texBG;

        public static int LastSubmittedOverallRank = -1;
        public static int LastSubmittedWeeklyRank = -1;

        public ScoreBoard(ScoreBoardType type, SpriteFont font, Texture2D bg)
        {
            Type = type;
            comparingScore = false;

            spriteFont = font;
            texBG = bg;

            scoresReturned = false;
#if !WINRT
            bw = new BackgroundWorker();
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
#endif
        }

        public ScoreBoard(ScoreBoardType type, SpriteFont font, Texture2D bg, int playthrough, int stage, int level, double time)
        {
            Type = type;
            comparingScore = true;

            spriteFont = font;
            texBG = bg;

            comparePlaythrough = playthrough;
            compareStage = stage;
            compareLevel = (float)level;
            compareTime = Convert.ToDecimal(time);

            scoresReturned = false;
#if !WINRT
            bw = new BackgroundWorker();
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
#endif
        }
        public ScoreBoard(ScoreBoardType type, SpriteFont font, Texture2D bg, int scoreId)
        {
            Type = type;
            comparingScore = false;

            spriteFont = font;
            texBG = bg;

            getScoreId = scoreId;

            scoresReturned = false;
#if !WINRT
            bw = new BackgroundWorker();
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
#endif
        }



        public void Update(GameTime gameTime)
        {
            

         
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texBG, new Rectangle((int)Position.X - 210, (int)Position.Y - 5, 420, 180), null, Color.White * 0.8f);

            float xOffset = -200f;
            Vector2 drawPos = Position + new Vector2(xOffset, 0);

            drawPos.Y += 15;

            //Title
            switch (Type)
            {
                case ScoreBoardType.NearbyWeekly:
                case ScoreBoardType.WeeklyTopTen:
                    DrawString(spriteBatch, "Weekly Global", new Vector2(Position.X, drawPos.Y), true, 1f, 1f);
                    break;
                case ScoreBoardType.MyScores:
                case ScoreBoardType.MyNearbyScores:
                    DrawString(spriteBatch, "My Best", new Vector2(Position.X, drawPos.Y), true, 1f, 1f);
                    break;
                default:
                    DrawString(spriteBatch, "All-Time Global", new Vector2(Position.X, drawPos.Y), true, 1f, 1f);
                    break;
            }

            drawPos.Y += 24;

            if (!scoresReturned)
                DrawString(spriteBatch, "Fetching scores", new Vector2(Position.X, drawPos.Y), true, 1f);

            if(scoresError)
                DrawString(spriteBatch, "Error!", new Vector2(Position.X, drawPos.Y), true, 1f);

            if (scoresReturned && !scoresError)
            {
                
                DrawString(spriteBatch, "Rank", drawPos, false, 1f);
                DrawString(spriteBatch, "Name", drawPos + new Vector2(50,0), false, 1f);
                DrawString(spriteBatch, "Mode", drawPos + new Vector2(150, 0), false, 1f);
                DrawString(spriteBatch, "Stage", drawPos + new Vector2(250, 0), false, 1f);
                DrawString(spriteBatch, "Hero", drawPos + new Vector2(300, 0), false, 1f);
                DrawString(spriteBatch, "Time", drawPos + new Vector2(350, 0), false, 1f);
                drawPos.Y += 12;

                switch (Type)
                {
                    case ScoreBoardType.WeeklyTopTen:
                    case ScoreBoardType.NearbyWeekly:
                        if(WeeklyScores!=null)
                            foreach (WeeklyScore s in WeeklyScores)
                            {
                                DrawString(spriteBatch, s.Rank.ToString(), drawPos, false, 1f);
                                DrawString(spriteBatch, s.PlayerName!=null?s.PlayerName.ToString():GameManager.PlayerName, drawPos + new Vector2(50, 0), false, 1f);
                                DrawString(spriteBatch, Enum.GetName(typeof(Modes), s.IntScore.Value-1), drawPos + new Vector2(150, 0), false, 1f);
                                DrawString(spriteBatch, s.ExtraInt.Value>0?s.ExtraInt.ToString():"-", drawPos + new Vector2(250, 0), false, 1f);
                                DrawString(spriteBatch, "L" + s.FloatScore.ToString(), drawPos + new Vector2(300, 0), false, 1f);
                                DrawString(spriteBatch, MinutesAndSeconds(s.DecimalScore.Value), drawPos + new Vector2(350, 0), false, 1f);
                                drawPos.Y += 12;
                            }
                        break;
                    default:
                        if(Scores!=null)
                            foreach (Score s in Scores)
                            {
                                DrawString(spriteBatch, s.Rank.ToString(), drawPos, false, 1f);
                                DrawString(spriteBatch, s.PlayerName != null ? s.PlayerName.ToString() : GameManager.PlayerName, drawPos + new Vector2(50, 0), false, 1f);
                                DrawString(spriteBatch, Enum.GetName(typeof(Modes), s.IntScore.Value-1), drawPos + new Vector2(150, 0), false, 1f);
                                DrawString(spriteBatch, s.ExtraInt.Value > 0 ? s.ExtraInt.ToString() : "-", drawPos + new Vector2(250, 0), false, 1f);
                                DrawString(spriteBatch, "L" + s.FloatScore.ToString(), drawPos + new Vector2(300, 0), false, 1f);
                                DrawString(spriteBatch, MinutesAndSeconds(s.DecimalScore.Value), drawPos + new Vector2(350, 0), false, 1f);
                                drawPos.Y += 12;
                            }
                        break;
                }
            }
        }

        void DrawString(SpriteBatch sb, string s, Vector2 pos, bool centered, float alpha) { DrawString(sb, s, pos, centered, Color.White * alpha, 0.5f); }
        void DrawString(SpriteBatch sb, string s, Vector2 pos, bool centered, float alpha, float scale) { DrawString(sb, s, pos, centered, Color.White * alpha, scale); }
        void DrawString(SpriteBatch sb, string s, Vector2 pos, bool centered, Color color, float scale)
        {
            sb.DrawString(spriteFont, s, pos + new Vector2(1, 1), Color.Black * 0.4f * color.ToVector4().W, 0f, spriteFont.MeasureString(s) * new Vector2(centered ? 0.5f : 0f, 0.5f), scale, SpriteEffects.None, 1);
            sb.DrawString(spriteFont, s, pos + new Vector2(-1, -1), color, 0f, spriteFont.MeasureString(s) * new Vector2(centered?0.5f:0f,0.5f), scale, SpriteEffects.None, 1);
        }

        string MinutesAndSeconds(decimal totalsecs)
        {
            int mins = (int)(totalsecs/60);
            int secs = (int)(totalsecs % 60);

            return mins + "'" + secs + "\"";
        }

#if !WINDOWS_PHONE && !WINRT

        void FetchScores(object sender, DoWorkEventArgs e)
        {

            HeroBashHighScoresClient service = new HeroBashHighScoresClient();

            try
            {
                switch (Type)
                {
                    case ScoreBoardType.TopTen:
                        Scores = service.GetTopTenScores();
                        break;
                    case ScoreBoardType.NearbyOverall:
                        if (!comparingScore)
                            Scores = service.GetNearbyScores(getScoreId);
                        else
                            Scores = service.GetNearbyScoresInGame(comparePlaythrough, compareStage, compareLevel, compareTime);
                        break;
                    case ScoreBoardType.WeeklyTopTen:
                        WeeklyScores = service.GetTopTenWeeklyScores();
                        break;
                    case ScoreBoardType.NearbyWeekly:
                        if (!comparingScore)
                            WeeklyScores = service.GetNearbyWeeklyScores(getScoreId);
                        else
                            WeeklyScores = service.GetNearbyWeeklyScoresInGame(comparePlaythrough, compareStage, compareLevel, compareTime);
                        break;
                    case ScoreBoardType.MyScores:
                        Scores = service.GetMyPreviousScores(GameManager.PlayerID.ToString());
                        break;
                    case ScoreBoardType.MyNearbyScores:
                        if (!comparingScore)
                            Scores = service.GetMyNearbyPreviousScores(GameManager.PlayerID.ToString(), getScoreId);
                        else
                            Scores = service.GetMyNearbyPreviousScoresInGame(GameManager.PlayerID.ToString(), comparePlaythrough, compareStage, compareLevel, compareTime);
                        break;
                }

                scoresError = false;
                scoresReturned = true;
            }
            catch (Exception ex)
            {
                scoresError = true;
                scoresReturned = true;
            }
            finally
            {
                try { service.Close(); }
                catch (Exception ex) { }
            }

            
        }


        public static void Submit(int playthrough, int stage, int level, double time)
        {
            LastSubmittedOverallRank = -1;
            LastSubmittedWeeklyRank = -1;

            try
            {
                HeroBashHighScoresClient service = new HeroBashHighScoresClient();
                LastSubmittedOverallRank = service.AddScore(GameManager.PlayerName, GameManager.PlayerID.ToString(), playthrough, stage, (float)level, Convert.ToDecimal(time));
                LastSubmittedWeeklyRank = service.AddWeeklyScore(GameManager.PlayerName, GameManager.PlayerID.ToString(), playthrough, stage, (float)level, Convert.ToDecimal(time));
            }
            catch (Exception ex) { }
        }

#endif

#if WINDOWS_PHONE
        void FetchScores(object sender, DoWorkEventArgs e)
        {
            HeroBashHighScoresClient service = new HeroBashHighScoresClient();
            
            service.GetLastWeekScoreCompleted += service_GetLastWeekScoreCompleted;
            service.GetMyPreviousScoresCompleted += service_GetMyPreviousScoresCompleted;
            service.GetNearbyScoresCompleted += service_GetNearbyScoresCompleted;
            service.GetNearbyScoresInGameCompleted += service_GetNearbyScoresInGameCompleted;
            service.GetNearbyWeeklyScoresCompleted += service_GetNearbyWeeklyScoresCompleted;
            service.GetNearbyWeeklyScoresInGameCompleted += service_GetNearbyWeeklyScoresInGameCompleted;
            service.GetTopTenScoresCompleted += service_GetTopTenScoresCompleted;
            service.GetTopTenWeeklyScoresCompleted += service_GetTopTenWeeklyScoresCompleted;

            switch (Type)
            {
                case ScoreBoardType.NearbyOverall:
                    if (!comparingScore)
                        service.GetNearbyScoresAsync(getScoreId);
                    else
                        service.GetNearbyScoresInGameAsync(comparePlaythrough, compareStage, compareLevel, compareTime);
                    break;
                case ScoreBoardType.NearbyWeekly:
                    if (!comparingScore)
                        service.GetNearbyWeeklyScoresAsync(getScoreId);
                    else
                        service.GetNearbyWeeklyScoresInGameAsync(comparePlaythrough, compareStage, compareLevel, compareTime);
                    break;
                case ScoreBoardType.MyScores:
                    service.GetMyPreviousScoresAsync(GameManager.PlayerID.ToString());
                    break;
            }
        }

        void service_GetTopTenWeeklyScoresCompleted(object sender, GetTopTenWeeklyScoresCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetTopTenScoresCompleted(object sender, GetTopTenScoresCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetNearbyWeeklyScoresInGameCompleted(object sender, GetNearbyWeeklyScoresInGameCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetNearbyWeeklyScoresCompleted(object sender, GetNearbyWeeklyScoresCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetNearbyScoresInGameCompleted(object sender, GetNearbyScoresInGameCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetNearbyScoresCompleted(object sender, GetNearbyScoresCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetMyPreviousScoresCompleted(object sender, GetMyPreviousScoresCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void service_GetLastWeekScoreCompleted(object sender, GetLastWeekScoreCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void Submit(int playthrough, int stage, int level, double time)
        {
            LastSubmittedOverallRank = -1;
            LastSubmittedWeeklyRank = -1;

            try
            {
                HeroBashHighScoresClient service = new HeroBashHighScoresClient();
                //LastSubmittedOverallRank = service.AddScore(GameManager.PlayerName, GameManager.PlayerID.ToString(), playthrough, stage, (float)level, Convert.ToDecimal(time));
                //LastSubmittedWeeklyRank = service.AddWeeklyScore(GameManager.PlayerName, GameManager.PlayerID.ToString(), playthrough, stage, (float)level, Convert.ToDecimal(time));
            }
            catch (Exception ex) { }
        }
#endif

#if WINRT
        void FetchScores()
        {
            HeroBashHighScoresClient service = new HeroBashHighScoresClient();

            
        }
#endif


    }
}
