using Newtonsoft.Json;
using PatientAutoGenerator;
using System.Text;


var _httpClient = new HttpClient();

string apiUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:7000/api/patients";

for (int i = 0; i < 50; i++)
{
    var patient = PatientGenerator.GenerateManPatients();
    var json = JsonConvert.SerializeObject(patient);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    try
    {

        using (var response = await _httpClient.PostAsync(apiUrl, content))
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Patient {i + 1} added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add patient {i + 1}. Status code: {response.StatusCode}");
            }
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error adding patient {i + 1}: {ex.Message}");
    }
}

for (int i = 0; i < 50; i++)
{
    var patient = PatientGenerator.GenerateWomanPatients();
    var json = JsonConvert.SerializeObject(patient);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    try
    {
        using (var response = await _httpClient.PostAsync(apiUrl, content))
        {            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Patient {i + 1} added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add patient {i + 1}. Status code: {response.StatusCode}");
            }
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error adding patient {i + 1}: {ex.Message}");
    }
}


Console.WriteLine("Auto Generation completed.");