using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WC_Language
{
    public class Translator
    {
        Parser parser;
        Lexer lexer;
        public Translator(Parser parser, Lexer lexer)
        {
            this.parser = parser;
            this.lexer = lexer;
        }

        public void TranslateToFile(string outputPath)
        {
            //File.Create(outputPath);
            using(StreamWriter streamWriter = new StreamWriter(outputPath))
            {
                for (int i = 0; i < lexer.realTokens.Count; i++)
                {
                    WriteTokenIntoFile(streamWriter, lexer.realTokens[i]);
                }
            }
        }

        void WriteTokenIntoFile(StreamWriter streamWriter, Token token)
        {
            if(token.GetType() == TokenType.NUMBERDEKL || token.GetType() == TokenType.STRINGDEKL || token.GetType() == TokenType.BOOLDEKL)
            {
                streamWriter.Write("let ");
            }
            else if(token.GetType() == TokenType.PLUS)
            {
                streamWriter.Write("+");
            }
            else if(token.GetType() == TokenType.MINUS)
            {
                streamWriter.Write("-");
            }
            else if (token.GetType() == TokenType.MUL)
            {
                streamWriter.Write("*");
            }
            else if (token.GetType() == TokenType.DIV)
            {
                streamWriter.Write("/");
            }
            else if(token.GetType() == TokenType.STRING)
            {
                streamWriter.Write( "\"" + (string)token.datatype + "\"");
            }
            else if(token.GetType() == TokenType.NEWLINE)
            {
                streamWriter.Write("\n");
            }
            else
            {
                if(token.datatype is string)
                {
                    streamWriter.Write((string)token.datatype);
                }
                else if (token.datatype is char)
                {
                    streamWriter.Write((char)token.datatype);
                }

            }

            //Console.WriteLine(token.datatype);

        }
    }
}
