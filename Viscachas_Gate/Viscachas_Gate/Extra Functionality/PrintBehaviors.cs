namespace Viscachas_Gate
{
    internal class WritingStyles
    {
        /// <summary>
        /// writes a string by individually printing each character on the same line, and finally going to the next line
        /// </summary>
        /// <param name="input"></param>
        public void WriteLineCharactersSlowly(string input, int delay = 20)
        {
            foreach (char c in input)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// writes a string by individually printing each character on the same line, stays on the same line
        /// </summary>
        /// <param name="input"></param>
        public void WriteCharactersSlowly(string input, int delay = 20)
        {
            foreach (char c in input)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }



        /// <summary>
        /// writes a string by individually printing each character on the same line, and finally going to the next line
        /// </summary>
        /// <param name="input"></param>
        public void WriteLineCharactersSlowlyClamped(string input, int delay = 20, int lineLength = 70)
        {
            int cursorPosition = 0;
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i]);
                Thread.Sleep(delay);

                cursorPosition++;

                if (cursorPosition >= lineLength)
                {
                    if (input[i] == ' ')
                    {
                        Console.WriteLine();
                        cursorPosition = 0;
                    }
                }
            }
            Console.WriteLine();
        }



        /// <summary>
        /// allows you to overwrite the text on the previous line
        /// </summary>
        public void OverwriteLines(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }




        /// <summary>
        /// clears the key buffer from any stray keypresses
        /// </summary>
        public void ClearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }
    }
}
