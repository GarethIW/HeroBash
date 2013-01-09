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
        MyScores
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

        BackgroundWorker bw;

        SpriteFont spriteFont;

        public static int LastSubmittedOverallRank = -1;
        public static int LastSubmittedWeeklyRank = -1;

        public ScoreBoard(ScoreBoardType type, SpriteFont font)
        {
            Type = type;
            comparingScore = false;

            spriteFont = font;

            scoresReturned = false;

            bw = new BackgroundWorker();
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
        }

        
        public ScoreBoard(ScoreBoardType type, SpriteFont font, int playthrough, int stage, int level, double time)
        {
            Type = type;
            comparingScore = true;

            spriteFont = font;

            comparePlaythrough = playthrough;
            compareStage = stage;
            compareLevel = (float)level;
            compareTime = Convert.ToDecimal(time);

            scoresReturned = false;
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
        }
        public ScoreBoard(ScoreBoardType type, SpriteFont font, int scoreId)
        {
            Type = type;
            comparingScore = false;

            spriteFont = font;

            getScoreId = scoreId;

            scoresReturned = false;
            bw.DoWork += FetchScores;
            bw.RunWorkerAsync();
        }



        public void Update(GameTime gameTime)
        {
            

         
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (!scoresReturned)
                DrawString(spriteBatch, "Fetching scores", Position, true, 1f);

            if(scoresError)
                DrawString(spriteBatch, "Error!", Position, true, 1f);

            if (scoresReturned && !scoresError)
            {
                float xOffset = -200f;
                Vector2 drawPos = Position + new Vector2(xOffset, 0);
                DrawString(spriteBatch, "Rank", drawPos, false, 1f);
                DrawString(spriteBatch, "Name", drawPos + new Vector2(50,0), false, 1f);
                DrawString(spriteBatch, "Mode", drawPos + new Vector2(150, 0), false, 1f);
                DrawString(spriteBatch, "Stage", drawPos + new Vector2(200, 0), false, 1f);
                DrawString(spriteBatch, "Hero", drawPos + new Vector2(250, 0), false, 1f);
                DrawString(spriteBatch, "Time", drawPos + new Vector2(300, 0), false, 1f);
                drawPos.Y += 12;

                switch (Type)
                {
                    case ScoreBoardType.WeeklyTopTen:
                    case ScoreBoardType.NearbyWeekly:
                        foreach (WeeklyScore s in WeeklyScores)
                        {
                            DrawString(spriteBatch, s.Rank.ToString(), drawPos, false, 1f);
                            DrawString(spriteBatch, s.PlayerName.ToString(), drawPos + new Vector2(50, 0), false, 1f);
                            DrawString(spriteBatch, s.IntScore.ToString(), drawPos + new Vector2(150, 0), false, 1f);
                            DrawString(spriteBatch, s.ExtraInt.ToString(), drawPos + new Vector2(200, 0), false, 1f);
                            DrawString(spriteBatch, "L" + s.FloatScore.ToString(), drawPos + new Vector2(250, 0), false, 1f);
                            DrawString(spriteBatch, MinutesAndSeconds(s.DecimalScore.Value), drawPos + new Vector2(300, 0), false, 1f);
                            drawPos.Y += 12;
                        }
                        break;
                    default:
                        foreach (Score s in Scores)
                        {
                            DrawString(spriteBatch, s.Rank.ToString(), drawPos, false, 1f);
                            DrawString(spriteBatch, s.PlayerName.ToString(), drawPos + new Vector2(50, 0), false, 1f);
                            DrawString(spriteBatch, s.IntScore.ToString(), drawPos + new Vector2(150, 0), false, 1f);
                            DrawString(spriteBatch, s.ExtraInt.ToString(), drawPos + new Vector2(200, 0), false, 1f);
                            DrawString(spriteBatch, "L" + s.FloatScore.ToString(), drawPos + new Vector2(250, 0), false, 1f);
                            DrawString(spriteBatch, MinutesAndSeconds(s.DecimalScore.Value), drawPos + new Vector2(300, 0), false, 1f);
                            drawPos.Y += 12;
                        }
                        break;
                }
            }
        }

        void DrawString(SpriteBatch sb, string s, Vector2 pos, bool centered, float alpha)
        {
            sb.DrawString(spriteFont, s, pos + new Vector2(1, 1), Color.Black * 0.4f * alpha, 0f, spriteFont.MeasureString(s) * new Vector2(centered ? 0.5f : 0f, 0.5f), 0.5f, SpriteEffects.None, 1);
            sb.DrawString(spriteFont, s, pos + new Vector2(-1, -1), Color.White * alpha, 0f, spriteFont.MeasureString(s) * new Vector2(centered?0.5f:0f,0.5f), 0.5f, SpriteEffects.None, 1);
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
        void FetchScores()
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
#endif

#if WINRT
        void FetchScores()
        {
            HeroBashHighScoresClient service = new HeroBashHighScoresClient();

            
        }
#endif


    }
}
