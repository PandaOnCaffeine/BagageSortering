using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class TextBox
    {
        private List<string> lines;
        private int maxWidth;
        private int maxHeight;


        private string _name;
        private char boxCharacter = ' ';
        private int _x;
        private int _y;

        private object _lock;
        public TextBox(string name, int width, int height, int x, int y, object lockobj)
        {
            _name = name;
            lines = new List<string>();
            maxWidth = width;
            maxHeight = height;
            _x = x;
            _y = y;
            _lock = lockobj;
            DrawBox();
        }
        private void DrawBox()
        {
            try
            {
                Monitor.Enter(_lock);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(_x - 1, _y - 1);

                // Draw top border
                Console.Write(new string(boxCharacter, maxWidth + 2));

                Console.SetCursorPosition(_x - 1, _y + maxHeight);
                // Draw bottom border
                Console.Write(new string(boxCharacter, maxWidth + 2));

                // Draw side borders and clear inside
                for (int i = 0; i < maxHeight; i++)
                {
                    Console.SetCursorPosition(_x - 1, _y + i);
                    Console.Write(boxCharacter);
                    Console.SetCursorPosition(_x + maxWidth, _y + i);
                    Console.Write(boxCharacter);
                }

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(_x, _y - 1);
                foreach (char ch in _name)
                {
                    Console.Write(ch);
                }


                // Reset the cursor position and color
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
            }
            finally
            {
                Monitor.Exit(_lock);
            }

        }
        public void WriteAt(string text, ConsoleColor color)
        {
            try
            {
                Monitor.Enter(_lock);

                // Add the new text to the list of lines
                lines.Add(text);

                // Trim the list if it exceeds the maximum height
                while (lines.Count > maxHeight)
                {
                    lines.RemoveAt(0);
                }

                // Clear the area inside the box
                Console.SetCursorPosition(_x, _y);
                Console.ForegroundColor = color;

                for (int i = 0; i < maxHeight; i++)
                {
                    Console.SetCursorPosition(_x, _y + i);
                    Console.Write(new string(' ', maxWidth - 1));
                }

                // Draw the updated lines
                for (int i = 0; i < lines.Count; i++)
                {
                    Console.SetCursorPosition(_x + 1, _y + i);
                    Console.Write(lines[i]);
                }

                // Reset the cursor position and color
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
            }
            finally
            {
                Monitor.Exit(_lock);
            }

        }
        public void RemoveBox()
        {
            try
            {
                Monitor.Enter(_lock);

                //Remove box and all that was in it
                for (int i = 0; i < maxHeight + 2; i++)
                {
                    Console.SetCursorPosition(_x - 1, _y - 1 + i);
                    Console.Write(new string(' ', maxWidth + 2));
                }
                Console.ResetColor();
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
    }
}
