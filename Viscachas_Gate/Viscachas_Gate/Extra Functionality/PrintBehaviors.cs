namespace Viscachas_Gate
{
    [Serializable]
    internal class PrintBehaviors
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
                if (Console.CursorTop > 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
                // Handle the case when CursorTop is already at the top
                else
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
        }




        /// <summary>
        /// clears the key buffer from any stray keypresses
        /// </summary>
        public void ClearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
