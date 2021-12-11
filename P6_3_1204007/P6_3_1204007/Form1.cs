using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace P6_3_1204007
{
    public partial class Form1 : Form
    {
        string prodi;
        public Form1()
        {
            InitializeComponent();
            rbLk.Checked = false;
            rbPr.Checked = false;

            //string myConnectionString = "integrated security=true;data source=.;initial catalog=DESKTOP-ASUS\\P6_1204007";
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-ASUS\P6_1204007;Initial Catalog=P6_1204007;Integrated Security=True");

            conn.Open();

            SqlCommand sc = new SqlCommand("SELECT * FROM msprodi", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_prodi", typeof(string));
            dt.Columns.Add("singkatan", typeof(string));
            dt.Load(reader);

            cbProgram.ValueMember = "id_prodi";
            cbProgram.DisplayMember = "singkatan";
            cbProgram.DataSource = dt;

            conn.Close();
        }

        private void tbNPM_TextChanged(object sender, EventArgs e)
        {
            if (tbNPM.TextLength < 7)
            {
                epWrong.SetError(tbNPM, "Format NPM salah!");
            }
            else if (tbNPM.TextLength == 7)
            {
                epWrong.SetError(tbNPM, "");
            }
            else if (tbNPM.TextLength == 0)
            {
                epWarning.SetError(tbNPM, "Tidak Boleh Kosong!");
            }
        }

        private void tbNama_TextChanged(object sender, EventArgs e)
        {
            if (tbNama.TextLength == 0)
            {
                epWarning.SetError(tbNama, "Tidak Boleh Kosong!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbNPM.Text != "" && tbNPM.TextLength == 7)
            {
                if (tbNama.Text != "")
                {
                    if (mcTanggal.Text != "")
                    {
                        if (rbLk.Checked || rbPr.Checked)
                        {
                            if (tbAlamat.Text != "")
                            {
                                if (tbTelepon.Text != "")
                                {
                                    if (cbProgram.Text != "- Pilih Program Studi -")
                                    {
                                        string npm = tbNPM.Text;
                                        string nama = tbNama.Text;
                                        string tanggal = mcTanggal.Text;
                                        string kelamin = "";
                                        if (rbLk.Checked)
                                        {
                                            kelamin = rbLk.Text;
                                        }
                                        if (rbPr.Checked)
                                        {
                                            kelamin = rbPr.Text;
                                        }
                                        string alamat = tbAlamat.Text;
                                        string nohp = tbTelepon.Text;
                                        string prodi = this.prodi;

                                        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-ASUS\P6_1204007;Initial Catalog=P6_1204007;Integrated Security=True");

                                        string sql = "insert into msmhs ([nim], [nama], [tgl_lahir], [jenis_kelamin], [alamat], " +
                                            "[telepon], [id_prodi]) values(@nim,@nama,@tanggal,@kelamin,@alamat,@nohp,@idprodi)";

                                        using (SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-ASUS\P6_1204007;Initial Catalog=P6_1204007;Integrated Security=True"))
                                        {
                                            try
                                            {
                                                cnn.Open();

                                                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                                                {
                                                    cmd.Parameters.Add("@nim", SqlDbType.NVarChar).Value = npm;
                                                    cmd.Parameters.Add("@nama", SqlDbType.NVarChar).Value = nama;
                                                    cmd.Parameters.Add("@tanggal", SqlDbType.NVarChar).Value = tanggal;
                                                    cmd.Parameters.Add("@kelamin", SqlDbType.NVarChar).Value = kelamin;
                                                    cmd.Parameters.Add("@alamat", SqlDbType.NVarChar).Value = alamat;
                                                    cmd.Parameters.Add("@nohp", SqlDbType.NVarChar).Value = nohp;
                                                    cmd.Parameters.Add("@idprodi", SqlDbType.NVarChar).Value = prodi;

                                                    int rowsAdded = cmd.ExecuteNonQuery();
                                                    if (rowsAdded > 0)
                                                        MessageBox.Show("Data berhasil disimpan");
                                                    else
                                                        MessageBox.Show("Tidak ada data yang disimpan");



                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("ERROR:" + ex.Message);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show
                                                    ("Prodi belum diisi!",
                                                    "Informasi Data Submit",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show
                                                ("No Telp belum diisi!",
                                                "Informasi Data Submit",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show
                                            ("Alamat belum diisi!",
                                            "Informasi Data Submit",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show
                                        ("Jenis Kelamin belum diisi!",
                                        "Informasi Data Submit",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show
                                    ("Tanggal Lahir belum diisi!",
                                    "Informasi Data Submit",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show
                                ("Nama belum diisi!",
                                "Informasi Data Submit",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show
                            ("NPM belum diisi!",
                            "Informasi Data Submit",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.prodi = cbProgram.SelectedValue.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbNPM.Text = null;
            tbNama.Text = null;
            tbAlamat.Text = null;
            tbTelepon.Text = null;
            rbLk.Checked = false;
            rbPr.Checked = false;
            cbProgram.SelectedIndex = 0;
        }
    }
}
