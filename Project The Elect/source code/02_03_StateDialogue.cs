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

        private int _currentDialogueIndex = 0;
        private int _previousDialogueIndex = 0;

        private bool _isNextDialogue = true;

        private ChapterData chapter;
        private DialogueData current;
        private GameFontManager _fontManager;
        private GameAudioManager _audioManager;
        private int screenWidth;
        private int screenHeight;

        private KeyboardState _previousKeyboardState;
        private MouseState _previousMouseState;

        private bool _isPlayedVoiceline = false;

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
            current = _dialogueManager.GetDialogue(_currentDialogueIndex);
            _fontManager = new GameFontManager(content, spriteBatch);
            _spriteBatch = spriteBatch;
            _audioManager = audioManager;
        }    

        public void Update(GameTime gameTime)
        {
            current = _dialogueManager.GetDialogue(_currentDialogueIndex);

            _fontManager.Update(gameTime, current, _isNextDialogue);
            if(_isNextDialogue) {_isNextDialogue = false;}

            InputHandler(gameTime);
            AudioHandler(gameTime);
        }

        
        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            //DRAW PROFILE & BG
            _dialoguesprite.Draw(gameTime, current);

            //DRAW TEXT
            _fontManager.Draw(gameTime, current);
            
            //DRAW BUTTONS

            _spriteBatch.End();

        }

        

        public void InputHandler(GameTime gametime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();

            bool spacePressed = currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space);
            bool mouseClicked = currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;

            if (spacePressed || mouseClicked)
            {
                if (_currentDialogueIndex < _dialogueManager._dialogues.Count - 1)
                {
                    _currentDialogueIndex++;
                    _isNextDialogue = true;
                    _isPlayedVoiceline = false;
                }
            }

            _previousKeyboardState = currentKeyboardState;
            _previousMouseState = currentMouseState;
        }
        public void AudioHandler(GameTime gameTime)
        {
            if(_isPlayedVoiceline == false)
            {
                _audioManager.PlayVoicelines(_currentDialogueIndex);
                _isPlayedVoiceline = true;
            }
        }
    }
}



