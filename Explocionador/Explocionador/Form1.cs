using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Explocionador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void StringQuery()
        {

        }


        OracleConnection con;

        void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = "User Id=MMURO;Password=Noviembre2016;Data Source=ALPHADB";
            con.Open();

        }

        void Close()
        {
            con.Close();
            con.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void buscarF_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            listBox1.Items.Add(textBoxX1.Text);

            foreach (string s in listBox1.Items)
            {

                string OraSql = @"SELECT   r.ixkitl ""Parent Part Number"",
         r.ixlitm ""Component Part Number"",
         i.imdsc1 ""Description"",
         SUM((r.ixqnty/10000)) ""Qty per Harness"",
         To_date(r.ixefff+1900000, 'YYYYDDD') ""Effective From Date"",
         To_date(r.ixefft+1900000, 'YYYYDDD') ""Effective Thru Date"",
         r.ixtbm ""Bill Type"",
         r.ixmmcu ""Branch Plt""
FROM     proddta.f3002 r,
         proddta.f4101 i";//                string OraSql2 = @"
//WHERE    (i.imlitm IN '" + textBoxX1.Text.ToString() + "'AND      i.imitm = r.ixkit)";

                string OraSql2 = @"
WHERE    (i.imlitm IN '" + s + "'AND      i.imitm = r.ixkit)";

                string orasql3 = @"
            AND      (r.ixmmcu = '        3320' AND      r.ixtbm = 'M  ')";
                string OraSql4 = @"
AND      r.ixefff <= (to_char(SYSDATE, 'YYYYDDD')-1900000)
AND      r.ixefft >= (to_char(SYSDATE, 'YYYYDDD')-1900000)
GROUP BY r.ixkitl,
         r.ixlitm,
         r.ixqnty,
         r.ixtbm,
         r.ixmmcu,
         r.ixefff,
         r.ixefft,
         i.imdsc1
ORDER BY r.ixkitl,
         r.ixlitm";

                Connect();
                OracleCommand cmd = new OracleCommand();
                OracleDataAdapter adapter = new OracleDataAdapter();
                DataTable dt = new DataTable();
                cmd = new OracleCommand(OraSql + OraSql2 + orasql3 + OraSql4, con);
                adapter = new OracleDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {                 
                    dataGridView1.Rows.Add
                        (
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),row[3].ToString(),
                            row[4].ToString(),
                            row[5].ToString()
                        );

                }
                listBox1.Items.Remove(s);
            }




        }


    }
}
    
