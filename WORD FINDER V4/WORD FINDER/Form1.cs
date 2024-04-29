﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CenterMaskedTxtBxes((int)numericUpDown1.Value);
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Checker(string[] Words, char[] charsY, Regex charsR, int wordLength, int chRLen)//before output, important
        {//checks if code can safely proceed :-)
            int len = maskedTextBox6.Text.Length + chRLen;
            string[] CustomW = File.ReadAllLines(@"custom_words.txt");
            if (len > wordLength) { label6.Text = "INVALID INPUT"; label6.Visible = true; return; }
            if (!checkBox5.Checked) { Perfect_search(Words, charsR, charsY, wordLength); } else { Perfect_search(CustomW, charsR, charsY, wordLength); }
            if (!label6.Visible) { label6.Text = "WORD NOT FOUND"; label6.Visible = true; }
        }
        private void Add()//Counter add
        {
            label7.Text = (int.Parse(label7.Text) + 1).ToString();
        }
        private void Reset()//Counter reset
        {
            button1.Visible = true;
            StreamWriter A = new StreamWriter(@"temps\temp.txt");//Removes all text before written on a temp file
            A.Flush();//
            A.Close();//
            StreamWriter B = new StreamWriter(@"temps\Ptemp.txt");//Removes all text before written on a Ptemp file
            B.Flush();//
            B.Close();//
            label7.Text = "0";
        }
        private bool duplicate(string Word, char[] charsY, Regex charsR, int wordLength)//Better algoritm (opcional, advanced)
        {
            int[] probna = new int[wordLength];
            for (int r = 0; r < wordLength; r++) { probna[r] = 1; }//makes an array of 1's
            char[] charsYkopija = charsY;
            MaskedTextBox[] maskedTextBoxes = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5, maskedTextBox7, maskedTextBox8, maskedTextBox9, maskedTextBox10, maskedTextBox11 };
            for (int l = 0; l < wordLength && charsYkopija.Length != 0; l++)
            {
                if (maskedTextBoxes[l].Text != "") { probna[l] = 0; }
                if (probna[l] == 1 && charsYkopija.Contains(char.ToLower(Word[l])))
                {
                    int numIndex = Array.IndexOf(charsYkopija, char.ToLower(Word[l]));
                    char[] pr = new char[charsYkopija.Length - 1];
                    Array.Copy(charsYkopija, 0, pr, 0, numIndex);
                    Array.Copy(charsYkopija, numIndex + 1, pr, numIndex, charsYkopija.Length - 1 - numIndex);
                    charsYkopija = pr;
                    probna[l] = 0;
                }
            }
            for (int A = 0; A < wordLength; A++)
            {
                for (int B = A + 1; B < wordLength; B++)
                {
                    if ((probna[A] != probna[B] || probna[A] == 1 && probna[B] == 1) && char.ToLower(Word[A]) == char.ToLower(Word[B]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool Checkerv2(string Word, int wordLength)//second checkbox activates this checker :)
        {
            char[] nonochars = textBox1.Text.ToCharArray();
            for (int i = 0; i < wordLength; i++)
            {
                if (nonochars.Contains(char.ToLower(Word[i]))) { return false; }
            }
            return true;
        }
        private void Popunjavanje()//fill-upper
        {
            string[] Words = File.ReadAllLines(@"words_alpha.txt");
            int wrdL = WordLength();
            string U = "";
            Regex charsR;
            int chRLen = 0;
            if (wrdL > 10)
            {
                U = textBox2.Text.ToLower().Replace("_", "\\w");
                chRLen = textBox2.Text.Replace("_", "").Length;
                charsR = new Regex("\\b" + U + "\\b");
            }
            else
            {
                if (wrdL < 11 && textBox2.Visible == true) { label6.Visible = true; label6.Text = "INPUT ERROR"; return; }
                MaskedTextBox[] maskedTextBoxes = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5, maskedTextBox7, maskedTextBox8, maskedTextBox9, maskedTextBox10, maskedTextBox11 };
                for (int o = 0; o < wrdL; o++)
                {
                    try { U += maskedTextBoxes[o].Text[0]; chRLen++; }
                    catch (Exception) { U += "\\w"; }
                }
                charsR = new Regex("\\b" + U + "\\b");
            }
            char[] charsY = maskedTextBox6.Text.ToLower().ToCharArray();
            Checker(Words, charsY, charsR, wrdL, chRLen);
        }
        private int WordLength()//LENGTH OF THE WORD (IMPORTANT)
        {
            if (numericUpDown1.Value == 11) { return textBox2.Text.ToCharArray().Length; }
            else { return (int)numericUpDown1.Value; }
        }
        private void button1_Click(object sender, EventArgs e)//Output
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Popunjavanje();
        }
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox2.Visible) { maskedTextBox2.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox3.Visible) { maskedTextBox3.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox4.Visible) { maskedTextBox4.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox5.Visible) { maskedTextBox5.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox5_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox7.Visible) { maskedTextBox7.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox6_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
        }
        private void maskedTextBox7_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox8.Visible) { maskedTextBox8.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox8_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox9.Visible) { maskedTextBox9.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox9_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox10.Visible) { maskedTextBox10.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox10_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            if (maskedTextBox11.Visible) { maskedTextBox11.Select(); }
            else { maskedTextBox6.Select(); }
        }
        private void maskedTextBox11_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
            maskedTextBox6.Select();
        }
        private void checkBox1_CheckedStateChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
        }
        private void checkBox2_CheckedStateChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (checkBox2.Checked) { textBox1.Visible = true; }
            else { textBox1.Visible = false; textBox1.Text = ""; }
            if (button4.Visible) { button3_Click(sender, e); }
        }
        private void checkBox3_CheckedStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) { textBox3.Visible = true; button7.Visible = true; }
            else { textBox3.Visible = false; textBox3.Text = ""; button7.Visible = false; }
        }
        private void checkBox4_CheckedStateChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            if (button4.Visible) { button3_Click(sender, e); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Add();
            string[] U = File.ReadAllLines(@"temps\Ptemp.txt");
            try
            {
                if (checkBox4.Checked) { label6.Text = "WORD FOUND: " + U[int.Parse(label7.Text)]; }
                else { label6.Text = "WORD FOUND: " + (U[int.Parse(label7.Text)].Split(new string[] { " (" }, StringSplitOptions.None))[0]; }
            }
            catch (Exception) { return; }
            label6.Visible = true;
            button8.Visible = true;
            button2.Visible = true;
        }
        private void Perfect_search(string[] Words, Regex charsR, char[] charsY, int wordLength)//Searches the words based on the rank of the letters that are in canditate words (calculates most common letters in those words) (PERFECT ALGORITHM, i think???)
        {
            char[] charsYprobna = charsY;
            Word[] Canditates = new Word[Words.Length]; long p = 0;
            string WordsTEXT = File.ReadAllText("words_alpha.txt");
            MatchCollection KOMBS = charsR.Matches(WordsTEXT);
            foreach (Match A in KOMBS)
            {
                charsY = charsYprobna;
                if (A.Value.Length != wordLength) { continue; }
                for (int i = 0; i < wordLength; i++)
                {
                    if (charsY.Contains(char.ToLower(A.Value[i])))
                    {
                        int numIndex = Array.IndexOf(charsY, char.ToLower(A.Value[i]));
                        char[] probna = new char[charsY.Length - 1];
                        Array.Copy(charsY, 0, probna, 0, numIndex);
                        Array.Copy(charsY, numIndex + 1, probna, numIndex, charsY.Length - 1 - numIndex);
                        charsY = probna;
                    }
                }
                if (charsY.Length != 0) { continue; }
                if (checkBox1.Checked && !duplicate(A.Value.ToString().ToLower(), charsYprobna, charsR, wordLength)) { continue; }
                if (checkBox2.Checked && !Checkerv2(A.Value.ToString().ToLower(), wordLength)) { continue; }
                Canditates[p] = new Word(A.Value);
                p++;

            }
            long o = Array.IndexOf(Canditates, null);
            Word[] U = new Word[o];
            Array.Copy(Canditates, 0, U, 0, o);
            if (U.Length != 0)
            {
                using (StreamWriter A = new StreamWriter(@"temps\Ptemp.txt"))
                {
                    foreach (Word P in U) { A.WriteLine(P.NAME); }
                }
                Letter[] Letters = rankedLetterArray(1);//SPECIAL RANKED LETTERS
                foreach (Word UU in U) { UU.rank(Letters); }//NOW IS RANKING
                Array.Sort(U, Word.wordComparator);
                using (StreamWriter A = new StreamWriter(@"temps\Ptemp.txt"))
                {
                    foreach (Word P in U) { A.WriteLine(P.NAME + " (" + P.POPULARITY + ")"); }
                }
                string[] I = File.ReadAllLines(@"temps\Ptemp.txt");
                try
                {
                    if (checkBox4.Checked) { label6.Text = "WORD FOUND: " + I[0]; }
                    else { label6.Text = "WORD FOUND: " + (I[0].Split(new string[] { " (" }, StringSplitOptions.None))[0]; }
                    label6.Visible = true; button8.Visible = true; button2.Visible = true; button1.Visible = false;
                }
                catch (Exception) { label6.Text = "WORD NOT FOUND"; }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "a" && maskedTextBox2.Text == "d" && maskedTextBox3.Text == "m" && maskedTextBox4.Text == "i" && maskedTextBox5.Text == "n" && textBox1.Text == "admin" && numericUpDown1.Value == 5)
            {//DEVELOPER MODE :-)
                label6.Visible = true;
                button2.Visible = true;
                label7.Visible = true;
                textBox1.Visible = true;
                button4.Visible = true;
                button6.Visible = true;
                button8.Visible = true;
                return;
            }
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            maskedTextBox4.Text = "";
            maskedTextBox5.Text = "";
            maskedTextBox6.Text = "";
            maskedTextBox7.Text = "";
            maskedTextBox8.Text = "";
            maskedTextBox9.Text = "";
            maskedTextBox10.Text = "";
            maskedTextBox11.Text = "";
            label7.Visible = false;
            button4.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            button1.Visible = true;
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            Reset();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            MaskedTextBox[] maskedTextBoxes = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5, maskedTextBox7, maskedTextBox8, maskedTextBox9, maskedTextBox10, maskedTextBox11 };
            if (numericUpDown1.Value == 11)
            {
                foreach (MaskedTextBox M in maskedTextBoxes) { M.Visible = false; }
                textBox2.Visible = true;
                label3.Text = "Word length: 10+";
                numericUpDown1.Visible = false;
                button6.Visible = true;
            }
            else
            {
                CenterMaskedTxtBxes((int)numericUpDown1.Value);
                foreach (MaskedTextBox M in maskedTextBoxes) { M.Visible = false; }
                for (int p = 0; p < numericUpDown1.Value; p++)
                {
                    maskedTextBoxes[p].Visible = true;
                }
                textBox2.Visible = false;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            label3.Text = "Word length:";
            numericUpDown1.Visible = true;
            numericUpDown1.Value = 5;
            button6.Visible = false;
            button3_Click(sender, e);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button8.Visible = false;
            string U = textBox3.Text;
            checkBox3.Checked = false;
            string[] CustomWords = File.ReadAllLines(@"custom_words.txt");
            string[] WORDS = File.ReadAllLines(@"words_alpha.txt");
            Array.Sort(CustomWords);
            using (StreamWriter izlazC = new StreamWriter(@"custom_words.txt"))
            {
                foreach (string A in CustomWords)
                {
                    if (WORDS.Contains(A)) { continue; }
                    izlazC.WriteLine(A.ToLower());
                }
                if (CustomWords.Contains(U)||WORDS.Contains(U)) { return; }
                izlazC.WriteLine(U.ToLower());
            }
            //SAVED WORD
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Reset();
            label6.Visible = false;
            button2.Visible = false;
            button1_Click(sender, e);
        }
        private int FirstIndexLocation(int N, int WidthM, int Gap)
        {
            int margins = 8;//Windows form margins
            int Uprim = (N * WidthM) + ((N - 1) * Gap);
            return ((Size.Width - Uprim) / 2) - margins;
        }
        private void CenterMaskedTxtBxes(int N)
        {
            int WidthM = maskedTextBox1.Size.Width;
            int Gap = maskedTextBox2.Location.X - (maskedTextBox1.Location.X + WidthM);
            int locIndex = FirstIndexLocation(N, WidthM, Gap);
            MaskedTextBox[] maskedTextBoxes = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5, maskedTextBox7, maskedTextBox8, maskedTextBox9, maskedTextBox10, maskedTextBox11 };
            maskedTextBox1.Location = new Point(locIndex, maskedTextBox1.Location.Y);
            for (int l = 1; l < N; l++)
            {
                locIndex += (WidthM + Gap);
                maskedTextBoxes[l].Location = new Point(locIndex, maskedTextBoxes[l].Location.Y);
            }
        }
        private Letter[] rankedLetterArray(int perfect)//returns most common ltters sorted in an array
        {
            string[] Words;
            if (perfect == 1) { Words = File.ReadAllLines(@"temps\Ptemp.txt"); }//perfecr module
            else { Words = File.ReadAllLines(@"words_alpha.txt"); }//advanced (normal)
            string[] letterslength = File.ReadAllLines(@"letters_rank.txt");
            long[] letters = new long[letterslength.Length]; Letter[] Lettersv2 = new Letter[letterslength.Length];
            for (int z = 0; z < letterslength.Length; z++)
            {
                Lettersv2[z] = new Letter(letterslength[z].ToLower()[0], 0);
            }
            foreach (string Word in Words)
            {
                foreach (char letter in Word)
                {
                    if (!char.IsLetter(letter)) { continue; }
                    int charID = -1;
                    for (int u = 0; u < letterslength.Length; u++)
                    {
                        if (char.ToLower(letter) == Lettersv2[u].NAME) { charID = u; break; }
                    }
                    try { letters[charID]++; }
                    catch (Exception) { throw new Exception("Please type the letter of the alphabet again. Make sure that each letter is in it's own row"); }
                }
            }
            for (int abc = 0; abc < Lettersv2.Length; abc++)
            {
                Lettersv2[abc].POPULARITY = letters[abc];
            }
            Array.Sort(Lettersv2, Letter.letterComparator);
            return Lettersv2;
        }
        private void button4_Click(object sender, EventArgs e)//ONLY ACTIVE IN DEVELOPER MODE
        {
            AddLetters A = new AddLetters();
            A.Show();
        }
        private int[] word_ranks(object sender, EventArgs e)//returns back an array of numbers (ranks). The lesser, the more common it is!
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string[] Words = File.ReadAllLines(@"words_alpha.txt");
            string[] letterslength = File.ReadAllLines(@"letters_rank.txt");
            int[] pomocna = new int[Words.Length];
            Letter[] Letters = rankedLetterArray(0);
            long i = 0;
            foreach (string Word in Words)
            {
                int counter = 0;
                for (int A = 0; A < Word.Length; A++)
                {
                    for (int L = 0; L < letterslength.Length; L++)
                    {
                        if (Letters[L].NAME == char.ToLower(Word[A])) { counter += (L + 1); break; }
                    }
                }
                pomocna[i] = counter;
                i++;
            }
            return pomocna;
        }
    }
}