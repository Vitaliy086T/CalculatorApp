using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace CalculatorFrontend
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5218/") };
        }

        // ✅ One handler for all digit & operator buttons
        private void InputButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            textBoxDisplay.Text += button.Text;
        }

        // ✅ Decimal button
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += ".";
        }

        // ✅ Clear all
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = string.Empty;
            labelResult.Text = "Результат:";
            labelStatus.Text = "Готов";
        }

        // ✅ Backspace (удалить последний символ)
        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxDisplay.Text.Length > 0)
            {
                textBoxDisplay.Text = textBoxDisplay.Text.Substring(0, textBoxDisplay.Text.Length - 1);
            }
        }

        // ✅ Calculate using API
        private async void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDisplay.Text))
            {
                MessageBox.Show("Введите выражение", "Ошибка");
                return;
            }

            try
            {
                labelStatus.Text = "Вычисляем...";

                var request = new
                {
                    Expression = textBoxDisplay.Text
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ⚠️ Backend endpoint for full expressions
                var response = await httpClient.PostAsync("api/calculator/evaluate", content);

                if (!response.IsSuccessStatusCode)
                {
                    labelStatus.Text = "Ошибка сервера";
                    return;
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                CalculatorResponse result = null;
                try
                {
                    result = JsonSerializer.Deserialize<CalculatorResponse>(responseContent, options);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"JSON parse error: {ex.Message}\nResponse: {responseContent}", "Ошибка");
                    return;
                }

                if (result.Success)
                {
                    labelResult.Text = $"Результат: {result.Result}";
                    labelStatus.Text = "Успешно";
                    textBoxDisplay.Text = result.Result.ToString(); // show result directly
                }
                else
                {
                    labelResult.Text = "Ошибка";
                    labelStatus.Text = result.ErrorMessage;
                    MessageBox.Show(result.ErrorMessage, "Ошибка");
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Ошибка: {ex.Message}";
                MessageBox.Show($"Ошибка соединения: {ex.Message}", "Ошибка");
            }
        }
    }
}
