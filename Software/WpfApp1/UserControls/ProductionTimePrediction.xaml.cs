using Bussiness_Logic_Layer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentation_Layer.UserControls
{
    public partial class ProductionTimePrediction : UserControl
    {
        public ProductionTimePrediction()
        {
            InitializeComponent();
        }

        private void btnPredict_Click(object sender, RoutedEventArgs e)
        {
            txtPredictionResult.Text = string.Empty;
            txtPredictionResult.Text = "Procjena stvarnog trajanja proizvodnje:";
            try
            {
                var sampleData = new EstimateProductionTime.ModelInput()
                {
                    ProductType = radioWindow.IsChecked == true ? "Window" : "Door",
                    Material = radioMaterialPvc.IsChecked == true ? "PVC" : "ALU",
                    Width_mm = float.TryParse(txtWidth.Text, out var w) ? w : 0,
                    Height_mm = float.TryParse(txtHeight.Text, out var h) ? h : 0,
                    GlassType = radioGlassNone.IsChecked == true ? "None"
                                : radioGlassDouble.IsChecked == true ? "Double"
                                : "Triple",
                    Color = (cmbColor.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                    Quantity = float.TryParse(txtQuantity.Text, out var q) ? q : 0,
                    OperatorExperienceYears = float.TryParse(txtOperatorExperience.Text, out var oe) ? oe : 0,
                    EstimatedProductionTime_min = float.TryParse(txtEstimatedProductionTime.Text, out var ept) ? ept : 0,
                    ProductionCost_EUR = float.TryParse(txtProductionCost.Text, out var pc) ? pc : 0,
                    DeliveryTime_days = float.TryParse(txtDeliveryTime.Text, out var dt) ? dt : 0,
                };

                sampleData.Area_m2 = (sampleData.Width_mm * sampleData.Height_mm) / 1_000_000f;
                sampleData.AspectRatio = sampleData.Height_mm != 0 ? sampleData.Width_mm / sampleData.Height_mm : 0;
                    sampleData.TotalArea_m2 = sampleData.Area_m2 * sampleData.Quantity;

                var result = EstimateProductionTime.Predict(sampleData);
                txtPredictionResult.Text += "  " + result.Score.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}