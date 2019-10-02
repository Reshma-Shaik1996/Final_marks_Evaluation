using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
namespace FinalExam_1831841
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dir = @"C:/Summer19/";
        string path = @"C:/Summer19/Final.txt"; 
        

        Grade gradeObj = new Grade(); // Creating object for the class
          
        // =======Class to caluclate Percentages 
        public class Grade
        {
          public  double   midterm;
          public  double project;
          public  double final;
           public double TotalMarks;
         public   double midtermPercentage;
         public   double projectPercentage;
          public  double finalPercentage;
          public  char finalgrade;
            //Method to caluclate total marks
            public double Caluclate_totalMarks( )
            {
                midtermPercentage = midterm * 0.3;
                projectPercentage = project * 0.3;
                finalPercentage = final * 0.4;
                TotalMarks = midtermPercentage + projectPercentage + finalPercentage;
                
                return TotalMarks;

            }
            //Method to  caluclate Grade
            public char Caluclate_Grade(double TotalMarks)
            {
                if(TotalMarks >= 90 && TotalMarks <= 100)
                {
                   
                    return 'A';
                }

                else if (TotalMarks >= 80 && TotalMarks <=  89.9)
                {
                    return 'B';
                }
                 else if (TotalMarks >= 70 && TotalMarks <= 79.9)
                {
                    return 'C';
                }
                if (TotalMarks >= 60 && TotalMarks <= 69.9)
                {
                    return 'D';
                }
                else
                {
                    return 'F';
                }
            }

        } // end of class Grade


      

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Btn_1_Click(object sender, EventArgs e)
        {

            var stuName = txt_1.Text;
            var courseNo = txt_2.Text;
            var year = txt_3.Text;
            //===================================Validation
            Regex rgxName = new Regex(@"^[A-Za-z0-9]{4,30}$");
            Regex rgxCourseNo = new Regex(@"[0-9]{3}\-[A-Z0-9]{3}\-[A-Z]");
            Regex rgxYear = new Regex(@"^[0-9]{4}$");


            if (rgxName.IsMatch(txt_1.Text) == true)
            {
                if (rgxCourseNo.IsMatch(txt_2.Text) == true)
                {
                    if (rgxYear.IsMatch(txt_3.Text) == true)
                    {


                        //============================== Caluclating data





                        gradeObj.midterm = Convert.ToDouble(txt_4.Text);
                        gradeObj.project = Convert.ToDouble(txt_5.Text);

                        gradeObj.final = Convert.ToDouble(txt_6.Text);

                        double totalPerc = (gradeObj.Caluclate_totalMarks ());

                        txt_7.Text = totalPerc.ToString();
                        txt_8.Text = gradeObj.midtermPercentage.ToString();
                        txt_9.Text = gradeObj.projectPercentage.ToString();
                        txt_10.Text = gradeObj.finalPercentage.ToString();

                        txt_11.Text = gradeObj.Caluclate_Grade(totalPerc).ToString();


                    }
                    else
                    {
                        MessageBox.Show("Enter valid Year");
                    }
                }
                else
                {
                    MessageBox.Show("Enter valid Course example:420-P16-AS, 420-DW1-AS");
                }
            }
            else
            {
                MessageBox.Show("Enter valid Name between 4 and 30");

            }
        }

        private void Btn_5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }

        private void Btn_2_Click(object sender, EventArgs e)
        {


            var stuName = txt_1.Text;
            var courseNo = txt_2.Text;
            var year = txt_3.Text;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(file);

            textOut.Write(stuName + "|");
            textOut.Write(courseNo + "|");
            textOut.Write(comboBox1.SelectedItem + "|");
            textOut.Write(year + "|");
            textOut.Write(txt_4.Text + "|");
            textOut.Write(txt_5.Text + "|");
            textOut.Write(txt_6.Text + "|");
            textOut.Write(txt_7.Text + "|");
            textOut.Write(txt_8.Text + "|");
            textOut.Write(txt_9.Text + "|");
            textOut.Write(txt_10.Text + "|");
            textOut.WriteLine(txt_11.Text);
            textOut.Close();
        }

        private void Btn_3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; settings.IndentChars = (" ");

            int i = 0;
            XmlWriter xmlOut = XmlWriter.Create(dir + "Final.xml", settings);

            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("Root");
            {
                while (textIn.Peek() != -1)
                {
                    string row = textIn.ReadLine();
                    string[] columns = row.Split('|');
                    xmlOut.WriteStartElement("Grade");
                    xmlOut.WriteElementString("StudentName", columns[0]);
                    xmlOut.WriteElementString("CourseNumber", columns[1]);
                    xmlOut.WriteElementString("Session", columns[2]);
                    xmlOut.WriteElementString("Year", columns[3]);
                    xmlOut.WriteElementString("MidTermMarks", columns[4]);
                    xmlOut.WriteElementString("ProjectMarks", columns[5]);
                    xmlOut.WriteElementString("FinalMarks", columns[6]);
                    xmlOut.WriteElementString("MidTermPercentage", columns[7]);
                    xmlOut.WriteElementString("ProjectPercentage", columns[8]);
                    xmlOut.WriteElementString("FinalPercentage", columns[9]);
                    xmlOut.WriteElementString("TotalPercentage", columns[10]);
                    xmlOut.WriteElementString("FinalGrade", columns[11]);

                    xmlOut.WriteEndElement();
                    i++;
                }
            }

            xmlOut.WriteEndElement();
            xmlOut.Close();
            textIn.Close();


        }

        private void Btn_4_Click(object sender, EventArgs e)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlIn = XmlReader.Create(dir + "Final.xml", settings);
            string Value1, Value2, Value3, Value4, Value5, Value6, Value7, Value8, Value9, Value10, Value11, Value12, tempStr;
            Value1 = Value2 = Value3 = Value4 = Value5 = Value6 = Value7 = Value8 = Value9 = Value10 = Value11 = Value12 = tempStr = "";
            if (xmlIn.ReadToDescendant("Grade"))
            {
                do
                {
                    xmlIn.ReadStartElement("Grade");
                    Value1 = xmlIn.ReadElementContentAsString();
                    Value2 = xmlIn.ReadElementContentAsString();
                    Value3 = xmlIn.ReadElementContentAsString();
                    Value4 = xmlIn.ReadElementContentAsString();
                    Value5 = xmlIn.ReadElementContentAsString();
                    Value6 = xmlIn.ReadElementContentAsString();
                    Value7 = xmlIn.ReadElementContentAsString();
                    Value8 = xmlIn.ReadElementContentAsString();
                    Value9 = xmlIn.ReadElementContentAsString();
                    Value10 = xmlIn.ReadElementContentAsString();
                    Value11 = xmlIn.ReadElementContentAsString();
                    Value12 = xmlIn.ReadElementContentAsString();
                    tempStr += "Student Name: " + Value1 + "\nCourse Number: " + Value2 + "\nSession: " + Value3 + "\nYear: " + Value4 + "\nMid term marks: " + Value5 + "\nProject marks: " + Value6 + "\nFinal marks: " + Value7 + "\nMid term percentage: " + Value8 + "\nProject percentage: " + Value9 + "\nFinal percentage: " + Value10 + "\nTotal: " + Value11 + "\nTotal Grade: " + Value12 + "\r\n\n";
                } while (xmlIn.ReadToNextSibling("Grade"));
            }
            MessageBox.Show(tempStr);
            xmlIn.Close();
        }
    }
}
