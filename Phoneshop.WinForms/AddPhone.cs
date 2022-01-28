using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Entities;
using System;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    public partial class AddPhone : Form
    {
        private readonly IPhoneService _phoneService;
        private readonly ILogger _logger;
        public bool ApplyBtnClicked { get; set; }

        public AddPhone(IPhoneService phoneService, ILogger logger)
        {
            InitializeComponent();
            _phoneService = phoneService;
            _logger = logger;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtbxBrand.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at brand. This field is required");
                _logger.Error($"Wrong input at brand. This field is required");
                return;
            }
            if (txtbxType.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at type. This field is required");
                _logger.Error($"Wrong input at type. This field is required");
                return;
            }
            if (!double.TryParse(txtbxPrice.Text, out double price))
            {
                MessageBox.Show("Wrong input at price. This field is required"); //TODO Negatieve prijs
                _logger.Error($"Wrong input at price. This field is required");
                return;
            }
            if (price < 0)
            {
                MessageBox.Show("Price can't be negative");
                _logger.Error($"Price can't be negative");
                return;
            }
            if (!int.TryParse(txtbxStock.Text, out int stock))
            {
                MessageBox.Show("Wrong input at stock. This field is required"); //TODO Negatieve stock
                _logger.Error($"Wrong input at stock. This field is required");
                return;
            }
            if (stock < 0)
            {
                MessageBox.Show("Stock can't be negative");
                _logger.Error($"Stock can't be negative");
                return;
            }
            if (txtbxDescription.Text == string.Empty)
            {
                MessageBox.Show("Wrong input at description. This field is required");
                _logger.Error($"Wrong input at description. This field is required");
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
