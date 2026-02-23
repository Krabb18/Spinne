using System;
using WC_Language;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer("test.wc");
            lexer.Tokenize();

            foreach(var t in lexer.tokens)
            {
                Console.WriteLine(t);
            }

            Parser parser = new Parser(lexer);
            parser.CheckIfValid();

            Translator translator = new Translator(parser, lexer);
            translator.TranslateToFile("output.js");
        }
    }
}