using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodetoHTMLMarkup
{
    public partial class Main : Form
    {
        private List<string> keywordsCsharp = new List<string>()
        {
           "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
           "checked", "class", "const","continue", "decimal", "default", "delegate","do",
           "double", "else", "enum","event", "explicit", "extern", "false", "finally",
           "fixed", "float", "for","foreach", "goto", "if", "implicit","in", "int",
           "interface", "internal","is", "lock", "long", "namespace", "new", "null",
           "object", "operator","out", "override", "params", "private","protected",
           "public", "readonly", "ref","return", "sbyte", "sealed", "short","sizeof",
           "stackalloc", "static", "string","struct", "switch", "this", "throw","true",
           "try", "typeof", "uint","ulong", "unchecked", "unsafe", "ushort","using",
           "static", "virtual", "void","volatile", "while",
        };

        private Random generator = new Random();

        public Main()
        {
            InitializeComponent();
        }

        private string SetColour(string str, string colour) 
        {
            string ToReturn = "<span style = \"color:" + colour + "\">";
            ToReturn += str;
            ToReturn += "</span>";
            return ToReturn;
        }

        private string SetStart(string Elementid , string title)
        {          
            string ToReturn = "";           
            ToReturn += "<div style = \"border:2px solid black \">" + System.Environment.NewLine;
            ToReturn += "    <div style = \"border-bottom:2px solid black; padding:5px\">" + System.Environment.NewLine;
            ToReturn += "        <div class=\"row\">" + System.Environment.NewLine;
            ToReturn += "            <div class=\"col-md-6\">" + System.Environment.NewLine;
            ToReturn += "           <h6>"+ title+ "</h6>" + System.Environment.NewLine;
            ToReturn += "       </div>" + System.Environment.NewLine;
            ToReturn += "       <div class=\"col-md-6\">" + System.Environment.NewLine;
            ToReturn += "           <button class=\"float-right\" onclick=\"Copy" + Elementid + "()\">Copy</button>" + System.Environment.NewLine;
            ToReturn += "       </div>" + System.Environment.NewLine;
            ToReturn += "   </div>" + System.Environment.NewLine;
            ToReturn += "</div>" + System.Environment.NewLine;
            ToReturn += "<div style = \"padding:5px\" >" + System.Environment.NewLine;
            ToReturn += "<pre><code id=\"" + Elementid + "\">" + System.Environment.NewLine;
            return ToReturn;
        }
        private string SetEnd(string Elementid)
        {
            string ToReturn = "";
            ToReturn += "</code></pre>" + System.Environment.NewLine;
            ToReturn += "</div>" + System.Environment.NewLine;
            ToReturn += "</div>" + System.Environment.NewLine;
            ToReturn += "<script>" + System.Environment.NewLine;
            ToReturn += " function Copy" + Elementid + " ()" + System.Environment.NewLine;
            ToReturn += " {" + System.Environment.NewLine; ;
            ToReturn += "  node = document.getElementById(\'" + Elementid + "\');" + System.Environment.NewLine;
            ToReturn += "  const selection = window.getSelection();" + System.Environment.NewLine;
            ToReturn += "  const range = document.createRange();" + System.Environment.NewLine;
            ToReturn += "  range.selectNodeContents(node);" + System.Environment.NewLine;
            ToReturn += "  selection.removeAllRanges();" + System.Environment.NewLine;
            ToReturn += "  selection.addRange(range);" + System.Environment.NewLine;
            ToReturn += "  document.execCommand(\"copy\");" + System.Environment.NewLine;
            ToReturn += " }" + System.Environment.NewLine;
            ToReturn += "</script>" + System.Environment.NewLine;
            return ToReturn;
        }


        private string convertToHtmlMarkup(string text) 
        {
            string toReturn = "";
            Boolean PassedFirstElement = false;
            Boolean OpenQuote = false;
            string lastChar = "";
            foreach (char ch in text)
            {
                switch (ch)
                {
                    case '<':
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("&lt;", "blue");
                        lastChar = "LessThan";
                        PassedFirstElement = true;
                        break;
                    case '>':
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("&gt;", "blue");
                        lastChar = "GreaterThan";
                        break;
                    case ' ':
                        if (lastChar == "Char" && !OpenQuote) { toReturn += "</span>"; }
                        toReturn += "&nbsp;";
                        lastChar = "Space";
                        break;
                    case '@':
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("&#64;", "blue");
                        lastChar = "At";
                        break;
                    case '=':
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("=", "blue");
                        lastChar = "Equal";
                        break;
                    case '"':
                        if (OpenQuote) { OpenQuote = false; } else { OpenQuote = true; }
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("\"", "blue");
                        lastChar = "Quote";
                        break;
                    case '{':

                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("{", "blue");
                        lastChar = "OpenBrace";
                        break;
                    case '}':
                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour("}", "blue");
                        lastChar = "CloseBrace";
                        break;
                    case ',':

                        if (lastChar == "Char") { toReturn += "</span>"; }
                        toReturn += SetColour(",", "blue");                      
                        lastChar = "comma";
                        break;
                    default:
                        if (PassedFirstElement)
                        {
                            switch (lastChar)
                            {
                                case "LessThan":
                                    toReturn += "<span style = \"color:brown\">";
                                    break;
                                case "GreaterThan":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                                case "Space":
                                    if (!OpenQuote)
                                    {
                                        toReturn += "<span style = \"color:black\">";
                                        break;
                                    }
                                    break;
                                case "At":
                                    toReturn += "<span style = \"color:blue\">";
                                    break;
                                case "Equal":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                                case "Quote":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                                case "OpenBrace":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                                case "CloseBrace":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                                case "comma":
                                    toReturn += "<span style = \"color:black\">";
                                    break;
                            }
                            lastChar = "Char";
                        }
                        toReturn += ch;
                        break;
                }
            }
            return toReturn;
        }

        private string convertCsharpToHtmlMarkup(string text) 
        {
            string toReturn = "";

            for (int ct = 0; ct <= text.Length - 1; ct++)
            {
                switch (text[ct])
                {
                    case ' ':
                        toReturn += "&nbsp;";
                        break;
                    case '"':
                        toReturn += SetColour("\"", "brown");
                        break;
                    case '{':
                        toReturn += SetColour("{", "brown"); 
                        break;
                    case '}':
                        toReturn += SetColour("}", "brown");
                        break;
                    case '[':
                        toReturn += SetColour("[", "brown");
                        break;
                    case ']':
                        toReturn += SetColour("]", "brown");
                        break;
                    case '(':
                        toReturn += SetColour("(", "brown");
                        break;
                    case ')':
                        toReturn += SetColour(")", "brown");
                        break;
                    default:
                        // now check is this the start of a keyword
                        string match = "";
                        foreach (string word in keywordsCsharp)
                        {
                            // if first characters match
                            if (text[ct] == word[0])
                            {
                                // if word does not exceed length of string from ct                              
                                if (word.Length <= (text.Length - ct))
                                {
                                    for (int i = 0; i <= word.Length - 1; i++)
                                    {
                                        if (word[i] != text[ct + i])
                                        {
                                            break;
                                        }
                                        if ((i == word.Length - 1) && (text[ct + word.Length] == ' '))
                                        {
                                            match = word;
                                        }
                                    }
                                }
                            }
                            if (match != "") { break; }
                        }
                        if (match.Length > 1)
                        {
                            toReturn += "<span style = \"color:blue\">" + match + "</span>";
                            ct = ct + match.Length - 1;
                        }
                        else
                        {
                            toReturn += text[ct];
                        }
                        break;
                }
            }
            return toReturn;
        }


        private string HTMlconverter(string text)
        {
            string toReturn = "";
            string Elementid = "HTMLid"+ generator.Next(1, 999999).ToString().PadLeft(5, '0');
            //+ Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            toReturn += SetStart(Elementid," HTMl Markup");
            toReturn += convertToHtmlMarkup(text);
            toReturn += SetEnd(Elementid);
            return toReturn;
        }

        private string cSharpConverter(string text)
        {
            string toReturn = "";
            string Elementid = "cSharpid" + generator.Next(1, 999999).ToString().PadLeft(5, '0');
            //Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            toReturn += SetStart(Elementid, " C# Markup");
            toReturn += convertCsharpToHtmlMarkup(text);
            toReturn += SetEnd(Elementid);
            return toReturn;

        }

        private void ConvertHTML(object sender, EventArgs e)
        {
            Result.Text = HTMlconverter(codeToConvert.Text);
        }
 
        private void ConvertCsharp(object sender, EventArgs e)
        {
            Result.Text = cSharpConverter(codeToConvert.Text);
        }

        private void resultToClipboard(object sender, EventArgs e)
        {
            Clipboard.SetText(Result.Text);
        }

        private void About(object sender, EventArgs e)
        {
            string text = "";
            text = "This app takes code in HTML or C#  and converts it to a form that can be shown "; 
            text += "in a HTML page.  ";
            var result = MessageBox.Show(text,"About", MessageBoxButtons.OK);
        }
    }
}
