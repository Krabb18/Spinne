using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WC_Language
{
    public enum TokenType
    {
        STRING,
        STRINGDEKL,
        NUMBER,
        NUMBERDEKL,
        EQUAL,
        VARNAME,
        GF,
        ENDOFFILE,

        WHILE,
        IF,
        ELSE,
        ELSEIF,

        BOOLDEKL,
        BOOL,

        OPENBRACKET,
        CLOSEDBRACKET,

        PLUS,
        MINUS,
        MUL,
        DIV,
        NEWLINE,
        SEMICOLON,

        OPEN,
        CLOSE,

        PRINT,

    }

    public class Token
    {
        public TokenType type;
        //public string name = "";

        public int varID = 0;
        public object datatype; 

        public TokenType GetType()
        {
            return type;
        }
    }

    public class Lexer
    {
        string sourceCode = "";
        int charPtr = 0;

        public List<TokenType> tokens = new List<TokenType>();
        public List<Token> realTokens = new List<Token>();

        public Lexer(string filePath)
        {
            sourceCode = File.ReadAllText(filePath);
            //Console.WriteLine(sourceCode);
        }

        public char CurrentChar()
        {
            if (charPtr < sourceCode.Length)
            {
                return sourceCode[charPtr];
            }
            else
            {
                return '\0';
            }
        }

        void SkipWhitespace()
        {
            while(CurrentChar() != '\0' && CurrentChar() == ' ')
            {
                charPtr++;
            }
        }

        public TokenType GetToken()
        {
            Token newToken = new Token();
            newToken.type = TokenType.NEWLINE;

            SkipWhitespace();
            if(CurrentChar() == '+')
            {
                newToken.type = TokenType.PLUS;
                newToken.datatype = '+';
                //return TokenType.PLUS;
            }
            else if (CurrentChar() == '-')
            {
                newToken.type = TokenType.MINUS;
                newToken.datatype = '-';
                //return TokenType.MINUS;
            }
            else if (CurrentChar() == '*')
            {
                newToken.type = TokenType.MUL;
                newToken.datatype = '*';
                //return TokenType.MUL;
            }
            else if (CurrentChar() == '/')
            {
                newToken.type = TokenType.DIV;
                newToken.datatype = '/';
                //return TokenType.DIV;
            }
            else if (CurrentChar() == '=')
            {
                newToken.type = TokenType.EQUAL;
                newToken.datatype = '=';
                //return TokenType.EQUAL;
            }
            else if(CurrentChar() == ';')
            {
                newToken.type = TokenType.SEMICOLON;
                newToken.datatype = ';';
                //return TokenType.SEMICOLON;
            }
            else if(CurrentChar() == '(')
            {
                newToken.type = TokenType.OPEN;
                newToken.datatype = '(';
                //return TokenType.OPEN;
            }
            else if(CurrentChar() == ')')
            {
                newToken.type = TokenType.CLOSE;
                newToken.datatype = ')';
                //return TokenType.CLOSE;
            }
            else if(CurrentChar() == '{')
            {
                newToken.type = TokenType.OPENBRACKET;
                newToken.datatype = '{';
                // return TokenType.OPENBRACKET;
            }
            else if(CurrentChar() == '}')
            {
                newToken.type = TokenType.CLOSEDBRACKET;
                newToken.datatype = '}';
                //return TokenType.CLOSEDBRACKET;
            }
            else if(CurrentChar() == '"')
            {
                charPtr++;
                string text = "";
                while (CurrentChar() != '\0' && CurrentChar() != '"')
                {
                    text += CurrentChar();
                    charPtr++;
                }

                newToken.type = TokenType.STRING;
                newToken.datatype = text;

                //return TokenType.STRING;
            }
            else if (Char.IsLetter(CurrentChar()))
            {
                string text = "";
                while(CurrentChar() != ' ' && CurrentChar() != '\0' && Char.IsLetterOrDigit(CurrentChar()))
                {
                    text += CurrentChar();
                    charPtr++;
                }
                charPtr--;
                //Console.WriteLine(text);

                if (text == "number") { newToken.type = TokenType.NUMBERDEKL; }
                else if (text == "string") { newToken.type = TokenType.STRINGDEKL; }
                else if(text == "while") { newToken.type = TokenType.WHILE;  }
                else if(text == "false" || text == "true") { newToken.type = TokenType.BOOL;  }
                else if (text == "bool") { newToken.type = TokenType.BOOLDEKL;  }
                else if(text == "if") { newToken.type = TokenType.IF;  }
                else if(text == "else") { newToken.type = TokenType.ELSE;  }
                else if(text == "while") { newToken.type = TokenType.WHILE; }
                else if(text == "elseif") { newToken.type = TokenType.ELSEIF; text = "else if"; }
                else if(text == "print") { newToken.type = TokenType.PRINT; text = "console.log"; }

                else { newToken.type = TokenType.VARNAME; }

                newToken.datatype = text;
            }
            else if (Char.IsDigit(CurrentChar()))
            {
                string text = "";
                while (CurrentChar() != ' ' && CurrentChar() != '\0' && Char.IsDigit(CurrentChar()))
                {
                    text += CurrentChar();
                    charPtr++;
                }

                //Console.WriteLine(text);
                charPtr--;
                newToken.datatype = text;
                newToken.type = TokenType.NUMBER;
            }

            realTokens.Add(newToken);
            return newToken.type;
        }

        public void Tokenize()
        {
            
            while(CurrentChar() != '\0')
            {
                TokenType newToken = GetToken();
                tokens.Add(newToken);
                charPtr++;
            }
        }
        
    }
}
