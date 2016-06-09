using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConstructorIO;

namespace Sample_App__Add_Modify_Delete__
{
    public partial class frmSample : Form
    {
        public ConstructorIOAPI m_constructorClient;

        string m_sActionValue;

        public frmSample()
        {
            InitializeComponent();
            this.AlignText();
            pictureBoxLoading.BackColor = Color.Transparent;

            dataGridViewData.Rows.Add();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sActionValue = comboBoxAction.SelectedItem.ToString();

            bool isModify = m_sActionValue == "Modify ( name of item )";
            dataGridViewData.Columns["new_item_name"].Visible = isModify;
        }


        private void InsertSampleData()
        {
            if (chkRequired.Checked)
            {
                dataGridViewData.Rows[0].Cells["item_name"].Value = "item 1";
                dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = comboBoxType.SelectedItem.ToString();

                if (dataGridViewData.Rows[0].Cells["autocomplete_section"].Value.ToString() == "Products")
                    dataGridViewData.Rows[0].Cells["url"].Value = "www.item1.com";
            }

            if (chkOptional.Checked)
            {
                dataGridViewData.Rows[0].Cells["suggested_score"].Value = "1";
                dataGridViewData.Rows[0].Cells["keywords"].Value = "a,b,c";
                dataGridViewData.Rows[0].Cells["url"].Value = "www.item1.com";
                dataGridViewData.Rows[0].Cells["image_url"].Value = "www.item1.com/image";
                dataGridViewData.Rows[0].Cells["description"].Value = "description";
                dataGridViewData.Rows[0].Cells["id"].Value = "id1";
            }            
        }

        private ListItem GetListItem()
        {
            Dictionary<string, object> objDic = new Dictionary<string, object>();

            var dgr = dataGridViewData.Rows[0];

            var newListItem = new ListItem(dgr.Cells["item_name"].Value.ToString(), dgr.Cells["autocomplete_section"].Value.ToString())
            {
                Description = GetColumnValue(dgr, "description"),
                Url = GetColumnValue(dgr, "url"),
                ImageUrl = GetColumnValue(dgr, "image_url"),
                ID = GetColumnValue(dgr, "id")
            };

            //If modify, and new name column isn't empty, 
            if(m_sActionValue == "Modify ( name of item )" && GetColumnValue(dgr, "new_item_name") != null)
                newListItem.Name = GetColumnValue(dgr, "new_item_name");

            int suggestedScore;
            if (GetColumnValue(dgr, "suggested_score") != null)
                if (int.TryParse(GetColumnValue(dgr, "suggested_score"), out suggestedScore))
                    newListItem.SuggestedScore = suggestedScore;

            if(GetColumnValue(dgr, "keywords") != null)
                newListItem.Keywords = GetColumnValue(dgr, "keywords").Split(',').ToList();

            return newListItem;
        }

        private string GetColumnValue(DataGridViewRow dataGridRow, string columnName)
        {
            if (dataGridRow.DataGridView.Columns.Contains(columnName) && dataGridRow.Cells[columnName].Value != null)
            {
                var value = dataGridRow.Cells[columnName].Value.ToString();
                if (string.IsNullOrWhiteSpace(value)) return null;
                return value;
            }
            return null;
        }

        private void AlignText()
        {
            foreach (DataGridViewColumn col in dataGridViewData.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            pictureBoxLoading.Visible = true;

            try
            {
                bool bResult = false;

                m_constructorClient = new ConstructorIOAPI(txtAPI.Text, txtKey.Text);
                ListItem createdItem = GetListItem();

                switch (m_sActionValue)
                {
                    case "Add":
                        bResult = await m_constructorClient.AddAsync(createdItem);
                        break;
                    case "Modify ( name of item )":
                        bResult = await m_constructorClient.ModifyAsync(createdItem);
                        break;
                    case "Delete":
                        bResult = await m_constructorClient.RemoveAsync(createdItem);
                        break;
                }

                if(bResult)
                {
                    MessageBox.Show("Success.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            pictureBoxLoading.Visible = false;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dataGridViewData.Rows.Count > 0)
                dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = comboBoxType.SelectedItem.ToString();

            this.ClearData();
        }

        private void chkRequired_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearData();
        }

        private void ClearData()
        {
            if (dataGridViewData.Rows.Count > 0)
            {
                this.InsertSampleData();

                if (!chkRequired.Checked)
                {
                    dataGridViewData.Rows[0].Cells["item_name"].Value = null;
                    dataGridViewData.Rows[0].Cells["autocomplete_section"].Value = null ;

                    if (comboBoxType.SelectedItem.ToString() == "Products")
                        dataGridViewData.Rows[0].Cells["url"].Value = null;
                }

                if (!chkOptional.Checked)
                {
                    dataGridViewData.Rows[0].Cells["suggested_score"].Value = null;
                    dataGridViewData.Rows[0].Cells["keywords"].Value =null;
                    if (comboBoxType.SelectedItem.ToString() != "Products")
                        dataGridViewData.Rows[0].Cells["url"].Value = null;
                    dataGridViewData.Rows[0].Cells["image_url"].Value = null;
                    dataGridViewData.Rows[0].Cells["description"].Value = null;
                    dataGridViewData.Rows[0].Cells["id"].Value = null;
                }
            }

            dataGridViewData.Refresh();
        }

        private void chkOptional_CheckedChanged(object sender, EventArgs e)
        {
            this.ClearData();
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            m_constructorClient = new ConstructorIOAPI(txtAPI.Text, txtKey.Text);

            try
            {
                bool success = await m_constructorClient.VerifyAsync();
                if(success)
                {
                    MessageBox.Show("Valid Credentials");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }
    }
}
