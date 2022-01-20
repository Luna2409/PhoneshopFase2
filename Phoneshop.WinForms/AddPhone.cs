using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Objects;
using System;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class AddPhone : Form
    {
        private readonly IPhoneService _phoneService;
        public bool ApplyBtnClicked { get; set; }

        public AddPhone(IPhoneService phoneService)
        {
            InitializeComponent();
            _phoneService = phoneService;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtbxBrand.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at Brand. This field is required");
                return;
            }
            if (txtbxType.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at Type. This field is required");
                return;
            }
            if (!double.TryParse(txtbxPrice.Text, out double price))
            {
                MessageBox.Show("Wrong input at Price. This field is required");
                return;
            }
            if (!int.TryParse(txtbxStock.Text, out int stock))
            {
                MessageBox.Show("Wrong input at Stock. This field is required");
                return;
            }
            if (txtbxDescription.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at Description. This field is required");
                return;
            }

            var newPhone = new Phone()
            {
                Brand = new Brand() { Name = txtbxBrand.Text },
                Type = txtbxType.Text.ToString(),
                PriceWithTax = price,
                Stock = stock,
                Description = txtbxDescription.Text.ToString(),
            };

            _phoneService.Create(newPhone);
            ApplyBtnClicked = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ApplyBtnClicked = false;
            Close();
        }
    }
}
