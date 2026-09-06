using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using Project_The_Elect;
using Project_The_Elect.source_code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_The_Elect
{
    public class StateDialogue : GameState
    {
        private SpriteBatch _spriteBatch;
        private DialogueManager _dialogueManager;
        private DialogueSprite _dialoguesprite;

        private int _currentDialogue = 0;
        private ChapterData chapter;
        private DialogueData current;
        private int screenWidth;
        private int screenHeight;

        private KeyboardState _previousKeyboardState;
        private MouseState _previousMouseState;

        public StateDialogue(ContentManager content, GameStateManager gameStateManager, SpriteBatch spriteBatch, GameAudioManager audioManager, int screenWidth, int screenHeight)
        {
            _dialogueManager = new DialogueManager(content);
            _dialoguesprite = new DialogueSprite(content, spriteBatch, audioManager, screenWidth, screenHeight);
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            chapter = _dialogueManager.LoadChapter("Content/dialoguedata/chapter01.json");
            if (chapter?.dialogues != null && chapter.dialogues.Count > 0)
            {
                _dialogueManager._dialogues = chapter.dialogues;
            }
            current = _dialogueManager.GetDialogue(_currentDialogue);
        }    

        public void Update(GameTime gameTime)
        {
            current = _dialogueManager.GetDialogue(_currentDialogue);

            InputHandler(gameTime);
            AudioHandler(gameTime);
        }

        
        public void Draw(GameTime gameTime)
        {
            _dialoguesprite.Draw(gameTime, current);
        }

        public void InputHandler(GameTime gametime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            bool spacePressed = currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space);
            bool mouseClicked = currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;

            if (spacePressed || mouseClicked)
            {
                if (_currentDialogue < _dialogueManager._dialogues.Count - 1)
                {
                    _currentDialogue++;
                }
            }

            _previousKeyboardState = currentKeyboardState;
            _previousMouseState = currentMouseState;
        }
        public void AudioHandler(GameTime gameTime)
        {

        }
    }
}



