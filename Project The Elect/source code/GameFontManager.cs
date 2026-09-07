using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_The_Elect.source_code
{
    public class GameFontManager
    {
        public SpriteFont MainFont { get; private set; }
        public SpriteFont DialogueFont { get; private set; }

        private SpriteBatch _spriteBatch;

        private Vector2 _dialoguePos;
        private float _dialogueScale = 1.5f;

        private List<char> _dialogueline = new List<char>();
        private int _dialogueCharIndex = 0;

        private List<char> _dialoguedisplay = new List<char>();

        private bool _isPlayingTextAnim = false;

        private DialogueData _dialogueData;

        public GameFontManager(ContentManager content, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            //MainFont = content.Load<SpriteFont>("font/main");
            DialogueFont = content.Load<SpriteFont>("font/IBMPlexSansThaiLooped-SemiBold");

            _dialoguePos = new Vector2(270, 820);
        }

        public void Update(GameTime gameTime, DialogueData current, bool isNextDialogue)
        {
            // Update logic for font manager if needed
            if(isNextDialogue)
            {
                _dialoguePos = new Vector2(270, 820);
                _dialogueline.Clear();
                _dialoguedisplay.Clear();
                _dialogueCharIndex = 0;
                foreach (char c in current.text)
                {
                    _dialogueline.Add(c);
                }
                _isPlayingTextAnim = true;
            }

            if(_isPlayingTextAnim)
            {
                if(_dialogueCharIndex < current.text.Length)
                {            
                    _dialoguedisplay.Add(_dialogueline[0]);
                    _dialogueline.RemoveAt(0);
                    _dialogueCharIndex++;
                }
                else
                {
                    _isPlayingTextAnim = false;
                }
            }

        }

        public void Draw(GameTime gametime, DialogueData current)
        {
            //DRAW TEXT 
            _spriteBatch.DrawString(DialogueFont, new string(_dialoguedisplay.ToArray()), _dialoguePos, Color.White,0f, Vector2.Zero, _dialogueScale,SpriteEffects.None,0f);
        }
    }
}
