using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class AddLetters : Form
    {
        public AddLetters()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            if (textBox1.Text == "") { MessageBox.Show("Please input letters inside the box seperated by commas!"); return; }
            //PROCESS
            using (StreamWriter process = new StreamWriter(@"letters_rank.txt"))
            {
                string[] l = textBox1.Text.Split(',');
                for (int p = 0; p < l.Length; p++)
                {
                    try
                    {
                        if (p == l.Length - 1) { process.Write(char.ToUpper(char.Parse(l[p]))); break; }
                        process.WriteLine(char.ToUpper(char.Parse(l[p])));
                    }
                    catch { MessageBox.Show("Please input letters separated by commas!"); return; }
                }
            }
            //OUTPUT
            string[] Words = File.ReadAllLines(@"words_alpha.txt");
            string[] letterslength = File.ReadAllLines(@"letters_rank.txt");
            long[] letters = new long[letterslength.Length]; Letter[] Lettersv2 = new Letter[letterslength.Length];
            for (int z = 0; z < letterslength.Length; z++)
            {
                Lettersv2[z] = new Letter(letterslength[z].ToLower()[0], 0);
            }
            StreamWriter A = new StreamWriter(@"letters_rank.txt");//Removes all text before written on a file
            A.Flush();//
            A.Close();//
            using (StreamWriter izlaz = new StreamWriter(@"letters_rank.txt"))
            {
                foreach (string Word in Words)
                {
                    foreach (char letter in Word)
                    {
                        if (!char.IsLetter(letter)) { continue; }//Samo ako je slovo :)
                        int charID = -1;
                        for (int u = 0; u < letterslength.Length; u++)
                        {
                            if (char.ToLower(letter) == Lettersv2[u].NAME) { charID = u; break; }
                        }
                        try { letters[charID]++; }
                        catch (Exception) { MessageBox.Show("Please input letters inside the box seperated by commas!"); return; }
                    }
                }
                for (int abc = 0; abc < Lettersv2.Length; abc++)
                {
                    Lettersv2[abc].POPULARITY = letters[abc];
                }
                Array.Sort(Lettersv2, Letter.letterComparator);
                for (int G = 0; G < Lettersv2.Length; G++)
                {
                    izlaz.WriteLine(Lettersv2[G].ToString() + " (" + (G + 1) + ")");
                }
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
        private void button2_Click(object sender, EventArgs e)
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            if (textBox2.Text == "") { MessageBox.Show("Please input your dictionary!"); return; }
            //INPUT
            using (StreamWriter process = new StreamWriter(@"words_alpha.txt"))
            {
                string[] l = textBox2.Text.Split(new string[] { "\r\n" }, options: StringSplitOptions.None);
                for (int p = 0; p < l.Length; p++)
                {
                    if (p == l.Length - 1) { process.Write(l[p]); break; }
                    process.WriteLine(l[p]);
                }
            }
            //RANKING WORDS
            string[] Words = File.ReadAllLines(@"words_alpha.txt");
            string[] letterslength = File.ReadAllLines(@"letters_rank.txt");
            Letter[] Letters = rankedLetterArray(0);
            using (StreamWriter izlaz = new StreamWriter(@"words_rank.txt"))
            {
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
                    izlaz.WriteLine(Word + " (" + counter + ")");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult U = MessageBox.Show("Are you sure? This action is irreversible!", "Clear custom words", MessageBoxButtons.YesNo);
            if (U == DialogResult.Yes)
            {
                var c = File.Open(@"custom_words.txt", FileMode.Create);
                c.Close();
                MessageBox.Show("Cleared!", "Clear history");
            }
        }
    }
}
