using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // C# ODP.NET Oracle managed provider

namespace ListTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Con String
        public static string conString =
            "Data Source=ALPHADB;User Id=MMURO;Password=Noviembre2016;"; // C#


        //Abrimos una coneccion nueva con nuestro String de conexion
        //Cambiamos de OleDB a SQL

        OracleConnection conn = new OracleConnection(conString);

        String stringQuery = "SELECT DISTINCT PRODDTA_F58ABR.[ABAL$JOBN], PRODDTA_F3002.IXKITL AS Parent, PRODDTA_F3002.IXLITM AS [Component PN], PRODDTA_F4101.IMDSC1 AS Description\r\nFROM (PRODDTA_F4101 INNER JOIN PRODDTA_F58ABR ON PRODDTA_F4101.IMLITM = PRODDTA_F58ABR.[ABAL$FEAT]) INNER JOIN PRODDTA_F3002 ON PRODDTA_F4101.IMITM = PRODDTA_F3002.IXKIT\r\nWHERE (((PRODDTA_F58ABR.[ABAL$JOBN])=\"@JobID\") AND ((PRODDTA_F3002.IXTBM) Like \"%M%\") AND ((PRODDTA_F3002.IXMMCU) Like \"%3320%\"));\r\n";

        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;



        public void ListTesting()
        {

            OracleConnection conn = new OracleConnection(conString);
            conn.ConnectionString = conString;
            conn.Open(); // C#

            string sql = "select \r\n    r.IXKITL \"Parent Part Number\",\r\n    r.IXLITM \"Component Part Number\", \r\n    i.IMDSC1 \"Description\",\r\n    sum((r.IXQNTY/10000)) \"Qty per Harness\", \r\n    to_date(r.IXEFFF+1900000, \'YYYYDDD\') \"Effective From Date\", \r\n    to_date(r.IXEFFT+1900000, \'YYYYDDD\') \"Effective Thru Date\", \r\n    r.IXTBM \"Bill Type\", \r\n    r.IXMMCU \"Branch Plt\"\r\nfrom \r\nProddta.F3002 r, proddta.F4101 i\r\nwhere \r\n(i.Imlitm in\r\n(\r\n\'3688441C91-L\',\r\n)\r\nand i.IMITM = r.IXKIT)\r\nand \r\n(r.IXMMCU = \'        3320\' \r\nand r.IXTBM = \'M  \')\r\n\r\nand r.IXEFFF <= (TO_CHAR(SYSDATE, \'YYYYDDD\')-1900000)\r\nand r.IXEFFT >= (TO_CHAR(SYSDATE, \'YYYYDDD\')-1900000)\r\n\r\ngroup by \r\n    r.IXKITL, r.IXLITM, r.IXQNTY, r.IXTBM, r.IXMMCU, r.IXEFFF, r.IXEFFT, i.IMDSC1\r\norder by \r\n    r.IXKITL, r.IXLITM\r\n"; // C#
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;


            //SqlCommand command = new SqlCommand(stringQuery, con);
            //command.Parameters.Add("@JobID", SqlDbType.NVarChar).Value = textBoxX1.Text;

            for (int i = 0; i < textBoxX1.Lines.Length; i++)
            {
                MessageBox.Show("TRUE");
                //textBoxX2.Text += textBoxX1.Lines[i] + "\r\n";
            }


           

            OracleDataAdapter adb = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adb.Fill(ds);
            try
            {

                //Process the data in ds here

            }

            catch (Exception ex)

            {

                MessageBox.Show("Ex");
            }

            finally

            {



                cmd.Dispose();

                conn.Close();

            }


            textBoxX1.Text = textBoxX1.Text.Trim();
            

            labelX1.Text = textBoxX1.Lines.Length.ToString();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ListTesting();
        }


    }
}
