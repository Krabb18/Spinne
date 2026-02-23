using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace WC_Language
{
    public class Parser
    {
        Lexer lexer;
        int index = 0;
        public bool succcess = true;
        public bool compiled = true;
        public Parser(Lexer lexer) 
        {
            this.lexer = lexer;
        }

        public void CheckIfValid()
        {
            succcess = true;
            compiled = true;
            string log = "Compiled Succesfully :)";
            for(int i = 0; i<lexer.tokens.Count-1; i++)
            {
                if(index < lexer.tokens.Count)
                {
                   
                    if (lexer.tokens[index] == TokenType.STRINGDEKL)
                    {
                        succcess = CheckValidStringDekl();
                        
                    }
                    else if (lexer.tokens[index] == TokenType.NUMBERDEKL)
                    {
                        succcess = CheckValidNumberDekl();
                    }
                    else if (lexer.tokens[index] == TokenType.PRINT)
                    {
                        succcess = CheckValidPrintDekl();
                    }
                    else if (lexer.tokens[index] == TokenType.BOOLDEKL)
                    {
                        succcess = CheckValidBoolDekl();
                    }
                    else if (lexer.tokens[index] == TokenType.WHILE)
                    {
                        succcess = CheckValidWhileDekl();
                    }

                    if (!succcess) { log = "Compilation failed :(";compiled = false; }

                  
                }

                index++;
            }

            Console.WriteLine(log);
        }

        bool CheckValidPrintDekl() 
        {
            bool valid = true;

            index++;
            if (lexer.tokens[index] == TokenType.OPEN)
            {
                index++;
                if(lexer.tokens[index] == TokenType.VARNAME || lexer.tokens[index] == TokenType.NUMBER 
                    || lexer.tokens[index] == TokenType.STRING)
                {
                    index++;
                    if(lexer.tokens[index] != TokenType.CLOSE)
                    {
                        valid = false;
                        Console.WriteLine("Missing )");
                    }

                    index++;
                    if(lexer.tokens[index] != TokenType.SEMICOLON)
                    {
                        valid = false;
                        Console.WriteLine("no ;. Maybe you forgot it ;)");
                    }
                }
            }
            else
            {
                Console.WriteLine("You need to deklare function");
            }

            return valid;
        }

        bool CheckValidStringDekl()
        {
            bool valid = true;
            index++;
            if(lexer.tokens[index] == TokenType.VARNAME)
            {
                index++;
                //Case1: string wird einfach deklariert wie: string var1;
                if (lexer.tokens[index] == TokenType.SEMICOLON) 
                {
                    return valid; 
                }

                //Case2: string wird deklariert und initialisiert mit value: string var1 = "Hello";
                if (lexer.tokens[index] == TokenType.EQUAL) 
                {
                    index++;
                    if(lexer.tokens[index] == TokenType.STRING || lexer.tokens[index] == TokenType.VARNAME)
                    {
                        index ++;
                        if (lexer.tokens[index] != TokenType.SEMICOLON)
                        {
                            Console.WriteLine("no ;. Maybe you forgot it ;)");
                            valid = false;
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Need to deklare variable as a string");
                        valid = false;
                    }

                }
                else
                {
                    Console.WriteLine("Need to deklare variable or set ;");
                    valid = false;
                }

            }
            else
            {
                Console.WriteLine("Need variablename after string deklaration");
                valid = false;
            }

            return valid;
        }

        bool CheckValidNumberDekl()
        {
            bool valid = true;
            index++;
            if (lexer.tokens[index] == TokenType.VARNAME)
            {
                index++;
                //Case1: string wird einfach deklariert wie: string var1;
                if (lexer.tokens[index] == TokenType.SEMICOLON)
                {
                    return valid;
                }

                //Case2: string wird deklariert und initialisiert mit value: string var1 = "Hello";
                if (lexer.tokens[index] == TokenType.EQUAL)
                {
                    index++;
                    if (lexer.tokens[index] == TokenType.NUMBER || lexer.tokens[index] == TokenType.VARNAME)
                    {
                        index ++;
                        if (lexer.tokens[index] != TokenType.SEMICOLON)
                        {
                            Console.WriteLine("no ;. Maybe you forgot it ;)");
                            valid = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Need to deklare variable as a number");
                        valid = false;
                    }

                }
                else
                {
                    Console.WriteLine("Need to deklare variable or set ;");
                    valid = false;
                }

            }
            else
            {
                Console.WriteLine("Need variablename after string deklaration");
                valid = false;
            }

            return valid;
        }

        bool CheckValidBoolDekl()
        {
            bool valid = true;

            index++;
            if (lexer.tokens[index] == TokenType.VARNAME)
            {
                index++;
                if (lexer.tokens[index] == TokenType.SEMICOLON)
                {
                    return valid;
                }
                
                if(lexer.tokens[index] == TokenType.EQUAL)
                {
                    index++;
                    if (lexer.tokens[index] == TokenType.BOOL || lexer.tokens[index] == TokenType.VARNAME)
                    {
                        index++;
                        if (lexer.tokens[index] != TokenType.SEMICOLON)
                        {
                            Console.WriteLine("no ;. Maybe you forgot it ;)");
                            valid = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Need to deklare variable as a number");
                        valid = false;
                    }
                }
                else
                {
                    Console.WriteLine("Need to deklare variable or set ;");
                }
            }
            else
            {
                Console.WriteLine("Need variablename after bool deklaration");
            }

            return valid;
        }

        bool CheckValidWhileDekl()
        {
            bool valid = true;

            index++;
            if (lexer.tokens[index] == TokenType.OPEN)
            {
                index++;

                if (lexer.tokens[index] == TokenType.BOOL)
                {
                    index++;
                    if (lexer.tokens[index] == TokenType.CLOSE)
                    {
                        index++;

                        //Solange warten bis das open bracket kommt
                        while (lexer.tokens[index] == TokenType.NEWLINE) { index++; }
                        if(lexer.tokens[index] == TokenType.OPENBRACKET)
                        {
                            //schauen ob es das passende close bracket gibt
                        }
                        else
                        {
                            valid = false;
                            Console.WriteLine("You need an open bracket!!!");
                        }
                    }
                }
                else
                {
                    valid = false;
                    Console.WriteLine("The While loop need an condition");
                }
            }
            else
            {
                valid = false;
                Console.WriteLine("Did you forgot the ( ?");
            }

            return valid;
        }
    }
}
