namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < parentheses.Length; i++)
            {
                var bracket = parentheses[i];
                if (bracket == LeftBracket 
                    || bracket == LeftCurlyBracket 
                    || bracket == LeftSquareBracket)
                {
                    stack.Push(bracket);
                }
                else if (bracket == RightBracket 
                    || bracket == RightCurlyBracket 
                    || bracket == RightSquareBracket)
                {
                    if (bracket == RightBracket)
                    {
                        if (stack.Count > 0 && stack.Peek() == LeftBracket)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (bracket == RightCurlyBracket)
                    {
                        if (stack.Count > 0 && stack.Peek() == LeftCurlyBracket)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (bracket == RightSquareBracket)
                    {
                        if (stack.Count > 0 && stack.Peek() == LeftSquareBracket)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private const char LeftBracket = '(';
        private const char LeftSquareBracket = '[';
        private const char LeftCurlyBracket = '{';
        private const char RightBracket = ')';
        private const char RightCurlyBracket = '}';
        private const char RightSquareBracket = ']';
    }
}
